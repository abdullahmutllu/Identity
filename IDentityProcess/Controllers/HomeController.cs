using IDentityProcess.Models;
using IDentityProcess.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace IDentityProcess.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(userManager, signInManager)  // buradaki userManager depency injecktion sayseinde dolucak ve üst satırdaki alana atıcak
        {
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Member");
            }
            return View();
        }

        public IActionResult LogIn(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;  // tempdata actionlar arasında verilere ulaşmak için ( sayfalar arası taşıma )

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(userLogin.Email);
                if (user != null)
                {
                    if (await userManager.IsLockedOutAsync(user))  // user kilitli olup oladığını kontrol ederim
                    {
                        ModelState.AddModelError("", "Hesabınız bir süreliğine kilitlidir.Lütfen daha sonra tekrar deneyiniz.");
                        return View(userLogin);
                    }
                    await signInManager.SignOutAsync(); // benim yazdığım kullanıcı hakkında bir cookie varsa onu siler
                    SignInResult result = await signInManager.PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, false);
                    if (result.Succeeded)
                    {
                        await userManager.ResetAccessFailedCountAsync(user); // kullanıcının accesfailedcountunu sıfırlar...
                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }

                        return RedirectToAction("Index", "Member");
                    }
                    else
                    {
                        await userManager.AccessFailedAsync(user); // +1 ekler
                        int fail = await userManager.GetAccessFailedCountAsync(user); // userin kaç yanlış giriş yaptığını alırım
                        ModelState.AddModelError("", $" {fail} Hatalı giriş");
                        if (fail == 3)
                        {
                            await userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(20)));
                            ModelState.AddModelError("", "Hesabınız 3 başarısız girişten dolayı 20 dkika kitlenmiştir.Lütfen daha sonra tekrar deneyiniz");
                        }
                        else
                        {
                            ModelState.AddModelError("", " Email adresi veya şifre yanlış.");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Bu email adresine kayıtlı kullanıcı bulunamamıştır.");
                }
            }
            return View(userLogin);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            // kullanıcı eğer client tarafında js kapatırsa benim back tarafındada doğrulama yapmam gerekir

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                IdentityResult result = await userManager.CreateAsync(user, userViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    AddModelError(result);
                }

            }

            return View(userViewModel);  // eğerki işlem olmassa form tekrar gonderilir onceden girili alanlar kalır , hata mesajları gonderirlir 
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(PasswordResetViewModel passwordResetViewModel)
        {
            // E mail kontorlü(kullanıcı kontrolu)
            AppUser user = userManager.FindByEmailAsync(passwordResetViewModel.Email).Result;
            if (user != null)
            {
                string passwordResetToken = userManager.GeneratePasswordResetTokenAsync(user).Result;
                string passwordResetLink = Url.Action("ResetPasswordConfirm", "Home", new
                {
                    userId = user.Id,
                    token = passwordResetToken

                }, HttpContext.Request.Scheme);
                Helper.PasswordReset.PasswordResetSendEmail(passwordResetLink);
                ViewBag.status = "success";

            }
            else
            {
                ModelState.AddModelError("","Sistemde kayıtlı email adresi bulunamamıştır");
            }

            return View(passwordResetViewModel);
        }
        public IActionResult ResetPasswordConfirm(string userId,string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm([Bind("PasswordNew")]PasswordResetViewModel passwordResetViewModel)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                IdentityResult result = await userManager.ResetPasswordAsync(user,token,passwordResetViewModel.PasswordNew);
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                   
                    ViewBag.status = "success";
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("","hata meydana gelmiştir");
            }
            return View(passwordResetViewModel);
        }
        public IActionResult FacebookLogin(string ReturnUrl)  // giriş yaptıktan sonra erişmek istediği sayfaya yonlendircem
        {
            string RedicetUrl = Url.Action("ExternalResponse", "Home", new { ReturnUrl = ReturnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Facebook", RedicetUrl);
            return new ChallengeResult("Facebook", properties);
        }
        public async Task<IActionResult> ExternalResponse(string ReturnUrl = "/")
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync(); // kullanıcının login olduğuna dair bazı bilgileri alırım
            if (info==null) // giriş başarısız
            {
                return RedirectToAction("LogIn");
            }
            else
            {
                // çakışmayı engellemek için namespaci ekledim
                Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
                if (result.Succeeded)//kulanıcı daha once facebookdan giriş yapmış
                {
                    return Redirect(ReturnUrl);
                }
                else // kayıt işlemi 
                {
                    AppUser user = new AppUser();
                    user.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    string ExternalUserId = info.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (info.Principal.HasClaim(x => x.Type == ClaimTypes.Name)) // username
                    {
                        string userName = info.Principal.FindFirst(ClaimTypes.Name).Value;
                        userName = userName.Replace(' ','-').ToLower() + ExternalUserId.Substring(0, 5).ToString(); // isim çakışmasını önlemek için bu işlemi yaparız
                        user.UserName = userName;
                    }
                    else
                    {
                        user.UserName = info.Principal.FindFirst(ClaimTypes.Email).Value;  // üstteki if bloğundan name bilgisi alamaz ise username kısmına e posta atarız (çok düşük ihtimal)
                    }
                    IdentityResult createResult = await userManager.CreateAsync(user);
                    if (createResult.Succeeded)
                    {
                        IdentityResult loginResult = await userManager.AddLoginAsync(user, info); // bu kısım veri tabanında aspnetuserlogins tablosunu dolduracak :  LoginProvider:faceeok ProviderKey : Facebooktaki key   userıd:userın ıd si 
                        if (loginResult.Succeeded) // artık giriş işleimini gerçekleştirebiliriz
                        {
                            //  await signInManager.SignInAsync(user, true); // persistent değeri true olursa default 60 gün cookie değerini tutar
                            await signInManager.ExternalLoginSignInAsync(info.LoginProvider,info.ProviderKey,true);  // ıdentity apı arka tarafta claimslere facebooktan geldiğini belirtsin(kullanıcın nerden geldiğini claimslerden gorebilecceğim)
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            AddModelError(loginResult);
                        }
                    }
                    else
                    {
                        AddModelError(createResult);
                    }
                }
                List<string> errors = ModelState.Values.SelectMany(x=>x.Errors).Select(y=>y.ErrorMessage).ToList();
                return View("Error",errors);
            }
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}

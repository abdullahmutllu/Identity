using IDentityProcess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using IDentityProcess.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using IDentityProcess.Enums;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Claims;

namespace IDentityProcess.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {
      
        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager):base(userManager,signInManager)  // buradaki userManager depency injecktion sayseinde dolucak ve üst satırdaki alana atıcak
        {
            
        }
        public IActionResult Index()
        {

            AppUser user = CurrentUser;
            UserViewModel userViewModel = user.Adapt<UserViewModel>(); // userin içindeki prop ları , UserViewModel içindeki proplarla eşleneşnleri UservİEWModele aktarır.

            return View(userViewModel);
        }
        public IActionResult UserEdit()
        {
            AppUser user = CurrentUser;
            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));
            UserViewModel userViewModel = user.Adapt<UserViewModel>();


            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserViewModel userViewModel,IFormFile userPicture)
        {
            ModelState.Remove("Password");
            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));
            if (ModelState.IsValid) // eğer doğruysa
            {
                AppUser user = CurrentUser;
                if (userPicture!=null && userPicture.Length>0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);  // getectension gelen userPicturenin uzantısı alacak
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/UserPicture", fileName);  // veritabanı kontrolü yapar var mı yok mu
                    using (var stream = new FileStream(path,FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);
                        user.Picture = "/UserPicture/" + fileName;
                    }
                }
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.City = userViewModel.City;
                user.BirthDay = userViewModel.BirthDay;
                user.Gender = (int)userViewModel.Gender;
              IdentityResult result =   await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // kulanıcının cookisini güncelleme (securityStamp kısmını günceller)
                  await  userManager.GetSecurityStampAsync(user);
                  await  signInManager.SignOutAsync();
                    await signInManager.SignInAsync(user,true);  // persistence kısmı cookinin 60 ün geçerli olduğunu belirtiyor.
                    ViewBag.succsess = "true";
                }
                else
                {
                    AddModelError(result);
                }
            }
            return View(userViewModel);
        }
        public IActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PasswordChange(PasswordChangeViewModel passwordChangeViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = CurrentUser; // Buradaki name bilgisi cookiden geliyor
                
                    bool exist = userManager.CheckPasswordAsync(user,passwordChangeViewModel.PasswordOld).Result;
                    if (exist)
                    {
                        IdentityResult result = userManager.ChangePasswordAsync(user,passwordChangeViewModel.PasswordOld,passwordChangeViewModel.PasswordNew).Result;
                        if (result.Succeeded)
                        {
                        userManager.UpdateSecurityStampAsync(user);
                        signInManager.SignOutAsync();
                        signInManager.PasswordSignInAsync(user,passwordChangeViewModel.PasswordNew,true,false);
                        ViewBag.succsess = "true";
                        }
                        else
                        {
                        AddModelError(result);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("","Eski şifreniz yanlış");
                    }
                    
                
            }
            return View(passwordChangeViewModel);
        }
        public void LogOut()
        {
            signInManager.SignOutAsync();
            
        }
        public IActionResult AccsessDenied(string ReturnUrl)
        {
            if (ReturnUrl.Contains("ViolancePage"))
            {
                ViewBag.message = "Erişmeye çalıştığın sayfa şiddet videoları içerdiğinden dolayı 15 yaşından büyük olmanız gerekmektedir";
            }
            else if(ReturnUrl.Contains("AnkaraPage"))
            {
                ViewBag.message = "Bu sayfaya sadece şehir alanı ankara olan kullanıcılar erişebilir";
            }
            else if (ReturnUrl.Contains("Exchange"))
            {
                ViewBag.message = "30 günlük ücretsiz deneme hakkınız bitmiştir";
            }
            else
            {
                ViewBag.message = "Bu sayfaya erişim izniniz yoktur.";
            }
            return View();
        }
        [Authorize(Roles ="editor,Admin")]
        public IActionResult Editor()
        {
            return View();
        }
        [Authorize(Roles = "manager,Admin")]
        public IActionResult Manager()
        {
            return View();
        }
        [Authorize(Policy="AnkaraPolicy")]
        public IActionResult AnkaraPage()
        {
            return View();
        }
        [Authorize(Policy = "ViolancePolicy")]
        public IActionResult ViolancePage()
        {
            return View();
        }


        public async Task<IActionResult> ExchangeRedicet()
        {
            bool result = User.HasClaim(x=>x.Type=="ExpireDateExchange");
            if (!result)
            {
                Claim ExpireDateExchange = new Claim("ExpireDateExchange",DateTime.Now.AddDays(30).Date.ToShortDateString(),ClaimValueTypes.String,"Internal");
               await userManager.AddClaimAsync(CurrentUser, ExpireDateExchange);
                await signInManager.SignOutAsync();
                await signInManager.SignInAsync(CurrentUser,true);
            }
            return RedirectToAction("Exchange");
        }
        [Authorize(Policy="ExchangePolicy")]
        public IActionResult Exchange()
        {
            return View();
        }
    }
}

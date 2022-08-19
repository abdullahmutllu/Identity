using IDentityProcess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IDentityProcess.ClaimProvider
{
    public class ClaimProvider : IClaimsTransformation
    {
        public UserManager<AppUser> userManager { get; set; }

        public ClaimProvider(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal) // ClaimsPrincipal => Benim kullanıcılarım ile ilgili kimlik bilgilerini tutan kap
        {
            if (principal!=null && principal.Identity.IsAuthenticated) // üye olup olmadığını kontrol ederim
            {
                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
                AppUser user = await userManager.FindByNameAsync(identity.Name);
                if (user!=null)
                {
                    if (user.BirthDay!=null)
                    {
                        var today = DateTime.Today;
                        var age = today.Year - user.BirthDay?.Year;
                       
                        if (age>15)
                        {
                            Claim ViolanceClaim = new Claim("vioalance",true.ToString(),ClaimValueTypes.String,"Internal");
                            identity.AddClaim(ViolanceClaim);
                        }
                    }

                    if (user.City!=null)
                    {
                        if (!principal.HasClaim(x=>x.Type=="city")) // city adında bi claim var mı kontrol ederiz.
                        {
                            Claim CityClaim = new Claim("city",user.City,ClaimValueTypes.String,"Internal");
                            identity.AddClaim(CityClaim);
                        }
                    }
                }
                
            }
            return principal;
        }
    }
}

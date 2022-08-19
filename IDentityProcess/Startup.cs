using IDentityProcess.CustomValidation;
using IDentityProcess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDentityProcess
{
    public class Startup
    {
        public IConfiguration configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;   // =>  configuration sayesinde appsettings.json dosyasýna eriþebilcez
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, ExpireDateExchangeHandler>(); // IAuthorizationHandler interface i ile her karþýlaþtýðýnda ExpireDateExchangeHandler clasý oluþturur
            services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                opt.UseSqlServer(configuration["ConnectionStrings:DefaultConncetionString"]);
            });
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AnkaraPolicy", policy =>
                {
                    policy.RequireClaim("city", "ankara");

                });
                opts.AddPolicy("ViolancePolicy", policy =>
                {
                    policy.RequireClaim("vioalance");
                });
                opts.AddPolicy("ExchangePolicy", policy =>
                {
                    policy.AddRequirements(new ExpireDateExchangeRequirement());  // gereksinim ekleriz

                });

            });
            services.AddAuthentication().AddFacebook(opts =>
            {
                opts.AppId = configuration["Authentication:AppId"];  // once appsetting.jsona gider bu alaný bulamaz ise , screts.json gider
                opts.AppSecret = configuration["Authentication:AppScret"];

            });

            services.AddIdentity<AppUser, AppRole>(opts =>
            {

                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefgðhijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._çðýþü";
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddPasswordValidator<CustomPasswordValidator>().AddUserValidator<CustomUserValidator>().AddErrorDescriber<CustomIdentityDescriber>().AddEntityFrameworkStores<AppIdentityDbContext>
            ().AddDefaultTokenProviders();  // frameweok store ile  nereye kaydeileceðini  bildiririiz ,




            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "MyBlog";
            cookieBuilder.HttpOnly = false;          // client tarafýnda cookie okuma iþlemi yapýlmýyaacksa güvenlik açýsýndan false çekmek lazým, 
            cookieBuilder.SameSite = SameSiteMode.Lax; // bu default ayarý bu kýsma tekrar bak !!!  17.video
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest; // hem http  hemde https 
            services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = new PathString("/Home/Login");
                opts.LogoutPath = new PathString("/Member/LogOut");
                opts.Cookie = cookieBuilder;
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                opts.SlidingExpiration = true; // cookie süresinin yarýsýna geldiðinde tekrardan 60 gün daha uzatýr 
                opts.AccessDeniedPath = new PathString("/Member/AccsessDenied"); // kullanýcýn sayfaya eiriþim izni yoksa , kullanýcý belirtmiþ olduðum path gondericek

            });
            services.AddScoped<IClaimsTransformation, ClaimProvider.ClaimProvider>();  // Her bir request iþleminde  (1 istek boyunca) 1 kere nesne oluþturur ve onu kullanýr



            services.AddMvc(option => option.EnableEndpointRouting = false); //  mvc ile ilgili tüm servisleri kurar 
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage(); // sayfada hata alýndýðýnda hatayla ilgili açýklayýcý bilgi sunar .

            app.UseStatusCodePages(); // içerik donmeyen sayfalarda bilgilendirici yazý doner 
            app.UseStaticFiles(); // js , css gibi dosyalarýn yüklenebilmesi için 
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute(); // Mvc  => Conroller/Action/{id}


        }
    }
}

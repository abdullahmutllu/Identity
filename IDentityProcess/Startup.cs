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
            this.configuration = configuration;   // =>  configuration sayesinde appsettings.json dosyasına erişebilcez
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, ExpireDateExchangeHandler>(); // IAuthorizationHandler interface i ile her karşılaştığında ExpireDateExchangeHandler clası oluşturur
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
                opts.AppId = configuration["Authentication:AppId"];  // once appsetting.jsona gider bu alanı bulamaz ise , screts.json gider
                opts.AppSecret = configuration["Authentication:AppScret"];

            });

            services.AddIdentity<AppUser, AppRole>(opts =>
            {

                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefgğhijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._çğışü";
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddPasswordValidator<CustomPasswordValidator>().AddUserValidator<CustomUserValidator>().AddErrorDescriber<CustomIdentityDescriber>().AddEntityFrameworkStores<AppIdentityDbContext>
            ().AddDefaultTokenProviders();  // frameweok store ile  nereye kaydeileceğini  bildiririiz ,




            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "MyBlog";
            cookieBuilder.HttpOnly = false;          // client tarafında cookie okuma işlemi yapılmıyaacksa güvenlik açısından false çekmek lazım, 
            cookieBuilder.SameSite = SameSiteMode.Lax; // bu default ayarı bu kısma tekrar bak !!!  17.video
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest; // hem http  hemde https 
            services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = new PathString("/Home/Login");
                opts.LogoutPath = new PathString("/Member/LogOut");
                opts.Cookie = cookieBuilder;
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                opts.SlidingExpiration = true; // cookie süresinin yarısına geldiğinde tekrardan 60 gün daha uzatır 
                opts.AccessDeniedPath = new PathString("/Member/AccsessDenied"); // kullanıcın sayfaya eirişim izni yoksa , kullanıcı belirtmiş olduğum path gondericek

            });
            services.AddScoped<IClaimsTransformation, ClaimProvider.ClaimProvider>();  // Her bir request işleminde  (1 istek boyunca) 1 kere nesne oluşturur ve onu kullanır



            services.AddMvc(option => option.EnableEndpointRouting = false); //  mvc ile ilgili tüm servisleri kurar 
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage(); // sayfada hata alındığında hatayla ilgili açıklayıcı bilgi sunar .

            app.UseStatusCodePages(); // içerik donmeyen sayfalarda bilgilendirici yazı doner 
            app.UseStaticFiles(); // js , css gibi dosyaların yüklenebilmesi için 
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute(); // Mvc  => Conroller/Action/{id}


        }
    }
}

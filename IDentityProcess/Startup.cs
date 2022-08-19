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
            this.configuration = configuration;   // =>  configuration sayesinde appsettings.json dosyas�na eri�ebilcez
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, ExpireDateExchangeHandler>(); // IAuthorizationHandler interface i ile her kar��la�t���nda ExpireDateExchangeHandler clas� olu�turur
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
                opts.AppId = configuration["Authentication:AppId"];  // once appsetting.jsona gider bu alan� bulamaz ise , screts.json gider
                opts.AppSecret = configuration["Authentication:AppScret"];

            });

            services.AddIdentity<AppUser, AppRole>(opts =>
            {

                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefg�hijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._�����";
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddPasswordValidator<CustomPasswordValidator>().AddUserValidator<CustomUserValidator>().AddErrorDescriber<CustomIdentityDescriber>().AddEntityFrameworkStores<AppIdentityDbContext>
            ().AddDefaultTokenProviders();  // frameweok store ile  nereye kaydeilece�ini  bildiririiz ,




            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "MyBlog";
            cookieBuilder.HttpOnly = false;          // client taraf�nda cookie okuma i�lemi yap�lm�yaacksa g�venlik a��s�ndan false �ekmek laz�m, 
            cookieBuilder.SameSite = SameSiteMode.Lax; // bu default ayar� bu k�sma tekrar bak !!!  17.video
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest; // hem http  hemde https 
            services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = new PathString("/Home/Login");
                opts.LogoutPath = new PathString("/Member/LogOut");
                opts.Cookie = cookieBuilder;
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                opts.SlidingExpiration = true; // cookie s�resinin yar�s�na geldi�inde tekrardan 60 g�n daha uzat�r 
                opts.AccessDeniedPath = new PathString("/Member/AccsessDenied"); // kullan�c�n sayfaya eiri�im izni yoksa , kullan�c� belirtmi� oldu�um path gondericek

            });
            services.AddScoped<IClaimsTransformation, ClaimProvider.ClaimProvider>();  // Her bir request i�leminde  (1 istek boyunca) 1 kere nesne olu�turur ve onu kullan�r



            services.AddMvc(option => option.EnableEndpointRouting = false); //  mvc ile ilgili t�m servisleri kurar 
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage(); // sayfada hata al�nd���nda hatayla ilgili a��klay�c� bilgi sunar .

            app.UseStatusCodePages(); // i�erik donmeyen sayfalarda bilgilendirici yaz� doner 
            app.UseStaticFiles(); // js , css gibi dosyalar�n y�klenebilmesi i�in 
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute(); // Mvc  => Conroller/Action/{id}


        }
    }
}

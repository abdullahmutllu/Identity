#pragma checksum "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Member\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5122c1b55f3074ac8e1329e5d997ac23397fac20"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Member_Index), @"mvc.1.0.view", @"/Views/Member/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 3 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\_ViewImports.cshtml"
using IDentityProcess.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\_ViewImports.cshtml"
using IDentityProcess.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5122c1b55f3074ac8e1329e5d997ac23397fac20", @"/Views/Member/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b0bdd3835684eaf50ab59700c2e7d375f6a88d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Member_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Member\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Member/_MemberLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h3 class=\"text-center\">Kullanıcı Bilgileri</h3>\r\n<div class=\"row\">\r\n    <div class=\"col-sm-3\">\r\n        <img style=\"width:100%; height:100%\"");
            BeginWriteAttribute("src", " src=\"", 259, "\"", 279, 1);
#nullable restore
#line 10 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Member\Index.cshtml"
WriteAttributeValue("", 265, Model.Picture, 265, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    </div>\r\n    <div class=\"col-sm-9\">\r\n        <table class=\"table table-bordered table-striped\">\r\n            <tr>\r\n                <th>Kullanıcı adı</th>\r\n                <td>");
#nullable restore
#line 16 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Member\Index.cshtml"
               Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Email </th>\r\n                <td>");
#nullable restore
#line 20 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Member\Index.cshtml"
               Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Tel No</th>\r\n                <td>");
#nullable restore
#line 24 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Member\Index.cshtml"
               Write(Model.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Şehir</th>\r\n                <td>");
#nullable restore
#line 28 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Member\Index.cshtml"
               Write(Model.City);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Doğum tarihi</th>\r\n                <td>");
#nullable restore
#line 32 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Member\Index.cshtml"
               Write(Model.BirthDay?.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Cinsiyet</th>\r\n                <td>");
#nullable restore
#line 36 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Member\Index.cshtml"
               Write(Model.Gender);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n\r\n        </table>\r\n    </div>\r\n\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
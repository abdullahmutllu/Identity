#pragma checksum "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Admin\Claims.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b6736403f6d53d028283c401907d987907d7f16a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Claims), @"mvc.1.0.view", @"/Views/Admin/Claims.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b6736403f6d53d028283c401907d987907d7f16a", @"/Views/Admin/Claims.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b0bdd3835684eaf50ab59700c2e7d375f6a88d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Claims : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<System.Security.Claims.Claim>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Admin\Claims.cshtml"
  
    ViewData["Title"] = "Claims";
    Layout = "~/Views/Admin/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>CLaims</h2>\r\n<hr/>\r\n<table class=\"table table-bordered table-striped table-responsive\">\r\n    <tr>\r\n        <th>Kim</th>\r\n        <th>Da????t??c??</th>\r\n        <th>Ad??</th>\r\n        <th>De??er</th>\r\n    </tr>\r\n");
#nullable restore
#line 16 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Admin\Claims.cshtml"
     foreach (System.Security.Claims.Claim item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<tr>\r\n    <td>");
#nullable restore
#line 19 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Admin\Claims.cshtml"
   Write(item.Subject.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    <td>");
#nullable restore
#line 20 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Admin\Claims.cshtml"
   Write(item.Issuer);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    <td>");
#nullable restore
#line 21 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Admin\Claims.cshtml"
   Write(item.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    <td>");
#nullable restore
#line 22 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Admin\Claims.cshtml"
   Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n</tr>\r\n");
#nullable restore
#line 24 "C:\Users\abdullah\Desktop\Relationships\IDentity\IDentityProcess\IDentityProcess\Views\Admin\Claims.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n</table>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<System.Security.Claims.Claim>> Html { get; private set; }
    }
}
#pragma warning restore 1591

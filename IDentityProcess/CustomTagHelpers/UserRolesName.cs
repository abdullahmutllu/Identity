using IDentityProcess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDentityProcess.CustomTagHelpers
{
    [HtmlTargetElement("td",Attributes="user-roles")]
    public class UserRolesName:TagHelper
    {
        public UserManager<AppUser> UserManager { get; set; }

        public UserRolesName(UserManager<AppUser> UserManager)
        {
            this.UserManager = UserManager;
        }
        [HtmlAttributeName("user-roles")]
        public string UserId { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            AppUser user = await UserManager.FindByIdAsync(UserId);
          IList<string> roles = await  UserManager.GetRolesAsync(user);
            string html = string.Empty;
            roles.ToList().ForEach(x=>
            {
                html += $"<span class='badge badge-light'> {x} </span>";
            });
            output.Content.SetHtmlContent(html);
        }
        
    }
}

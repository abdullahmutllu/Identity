using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDentityProcess.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name="Role ismi")]
        [Required(ErrorMessage ="Role işlemi gereklidir")]
        public string Name { get; set; }
        public string Id { get; set; }
    }
}

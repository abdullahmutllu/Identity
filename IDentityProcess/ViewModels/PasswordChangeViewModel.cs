using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDentityProcess.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Display(Name ="Eski Şifreniz")]
        [Required(ErrorMessage ="Eski şifreniz gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage ="Şifreniz en az 4 karakterli olamak zorundadır")]
        public string PasswordOld { get; set; }
        [Display(Name = "Yeni Şifreniz")]
        [Required(ErrorMessage = "Yeni şifreniz gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olamak zorundadır")]
        public string PasswordNew { get; set; }
        [Display(Name = "Tekrar Yeni Şifreniz")]
        [Required(ErrorMessage = "Yeni şifreniz tekrar gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olamak zorundadır")]
        [Compare("PasswordNew",ErrorMessage ="Şifreler uyuşmamaktadır")]
        public string PasswordConfirm { get; set; }
    }
}

using IDentityProcess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDentityProcess.ViewModels
{
    public class UserViewModel
    {
        // kayıt olma işlemi
        [Required(ErrorMessage ="Kullanıcı adı gereklidir.")]
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name ="Telefon No:")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Email adresiniz doğru formatta değil.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre gereklidir.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Doğum tarihi")]
        public DateTime? BirthDay { get; set; }
      
        public string Picture { get; set; }
        [Display(Name = "Şehir")]

        public string City { get; set; }
        [Display(Name = "Cinsiyet")]
        public Gender Gender { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100)]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200)]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }


        [Display(Name = "کلمه ی عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Display(Name = "تکرار کلمه ی عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200)]
        [Compare("Password", ErrorMessage = "عبارت وارد شده با کلمه ی عبور مطابقت ندارد")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
    }
}

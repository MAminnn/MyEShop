using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(300)][Display(Name="نام کاربری")][Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="لطفا {0} را وارد کنید")][Display(Name="ایمیل")][MaxLength(550)]
        public string Email { get; set; }

        [Display(Name="رمز عبور")][Required(ErrorMessage ="لطفا {0} را وارد کنید")][DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="شماره تلفن")][DataType(DataType.PhoneNumber)]
        public DataType PhoneNumber{ get; set; }

        public Cart Cart { get; set; }

        [Display(Name ="نقش")]
        public Role Role { get; set; }

        public int RoleId { get; set; }
    }
}

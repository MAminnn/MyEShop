using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2
{ 
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام نمایشی نقش")]
        public string RoleTitle { get; set; }

        public List<User> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="لطفا {0} را وارد کنید")][Display(Name="عنوان گروه")][MaxLength(250)]
        public string Title { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "عنوان دوره")][MaxLength(300,ErrorMessage ="{0} نباید بیشتر از {1} کاراکتر باشد")][Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Required(ErrorMessage ="لطفا {0} را وارد کنید")][MaxLength(300,ErrorMessage ="{0} نباید بیشتر از {1} کاراکتر باشد")][Display(Name="قیمت")]
        public int Price { get; set; }

        [Display(Name="تعداد موجود در انبار")][Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int QuantityInStock { get; set; }

        public string ImageExtension { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}

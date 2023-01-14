using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public Item Item { get; set; }

        [Display(Name="تعداد")]
        public int Count { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        public Cart Cart { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2
{
    public class Cart
    {
        public int CartId { get; set; }

        public User User { get; set; }

        public List<CartItem> CartItems { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Display(Name = "جمع کل")]
        public int SumOfPrice
        {
            get
            {
                return CartItems.Sum(ci=>ci.Count*ci.Item.Product.Price);
            }   
        }

        public Cart()
        {

        }
    }
}

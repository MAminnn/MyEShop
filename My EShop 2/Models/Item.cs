using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2
{
    public class Item
    {
        public int ItemId { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public CartItem CartItem { get; set; }

        [ForeignKey("CartItem")]
        public int CartItemId { get; set; }
        public UInt64 Quantity { get; set; }
    }
}

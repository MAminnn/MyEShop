using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using My_EShop_2.Models.Context;

namespace My_EShop_2.Controllers
{
    public class CartController : Controller
    {
        private MyEShop_DB _context;
        public CartController(MyEShop_DB context)
        {
            _context = context;
        }
        [Authorize()]
        public async Task<IActionResult> AddToCart(int Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            var currentUserId = int.Parse(User.Claims.SingleOrDefault(u => u.Type == "UserId").Value);
            var user = await _context.Users.Include(u => u.Cart).ThenInclude(u => u.CartItems).ThenInclude(u => u.Item).ThenInclude(u => u.Product).SingleOrDefaultAsync(u => u.UserId == currentUserId);

            if (user.Cart == null)
            {

                Cart cart = new Cart()
                {
                    UserId = user.UserId,
                };

                _context.Add(cart);
                _context.SaveChanges();
                var debugging1 = cart;
                CartItem cartItem = new CartItem()
                {
                    Count = 1,
                    CartId = cart.CartId
                };
                _context.Add(cartItem);
                _context.SaveChanges();
                var debugging2 = cartItem;
                Item item = new Item()
                {
                    ProductId = Id,
                    CartItemId = cartItem.CartItemId
                };
                _context.Add(item);
                _context.SaveChanges();

            }

            else if (user.Cart.CartItems.SingleOrDefault(ci => ci.Item.ProductId == Id) == null)
            {
                CartItem cartItem = new CartItem()
                {
                    Count = 1,
                    Cart = user.Cart,
                };
                _context.Add(cartItem);
                _context.SaveChanges();
                Item item = new Item()
                {
                    ProductId = Id,
                    CartItemId = cartItem.CartItemId,
                };
                _context.Add(item);
                _context.SaveChanges();
            }
            else if (user.Cart.CartItems.SingleOrDefault(u => u.Item.ProductId == Id) != null)
            {
                user.Cart.CartItems.SingleOrDefault(u => u.Item.ProductId == Id).Count += 1;
                _context.SaveChanges();
            }
            return RedirectToAction("ShowCart");
        }
        public IActionResult RemoveOneItem(int Id)
        {
            var cartitem = _context.CartItems.Find(Id);
            if (cartitem.Count > 1)
            {
                cartitem.Count -= 1;
            }
            else
            {
                _context.CartItems.Remove(cartitem);
            }
            _context.SaveChanges();
            return RedirectToAction("ShowCart");
        }
        public IActionResult RemoveAllItems(int Id)
        {
            var cartitem = _context.CartItems.Find(Id);
            _context.Remove(cartitem);
            _context.SaveChanges();
            return RedirectToAction("ShowCart");
        }
        public async Task<IActionResult> ShowCart()
        {
            int currentUserId = int.Parse(User.Claims.SingleOrDefault(c => c.Type == "UserId").Value);
            if (!_context.Carts.Any(u => u.UserId == currentUserId))
            {
                return View("NoCartExists");
            }
            Cart cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(i => i.Item).ThenInclude(p => p.Product).SingleOrDefaultAsync(c => c.UserId == currentUserId);
            return View("Cart", cart);
        }
    }
}
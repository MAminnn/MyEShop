using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_EShop_2.Models.Context;
using My_EShop_2.Models.ViewModels;

namespace My_EShop_2.Controllers
{
    public class AccountController : Controller
    {
        private MyEShop_DB _context;
        public AccountController(MyEShop_DB context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            if (!await _context.Users.AnyAsync(u=>u.Email.ToLower()==registerVM.Email||u.UserName==registerVM.UserName))
            {

                User user = new User()
                {
                    Email = registerVM.Email,
                    UserName = registerVM.UserName,
                    Password = registerVM.Password,
                    Role = _context.Roles.SingleOrDefault(role=>role.RoleName=="User")
                };
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction("Login", "Account") ;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            User user = await _context.Users.Include(u => u.Role).SingleOrDefaultAsync(user => user.UserName == loginVM.UserName&&user.Password.Equals(loginVM.Password));
            if (user == null)
            {
                ModelState.AddModelError("UserName", "کاربری با این مشخصات یافت نشد");
                return View(loginVM);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId",user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = loginVM.RememberMe
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2.Models.Context
{
    public class MyEShop_DB:DbContext
    {
        public MyEShop_DB(DbContextOptions<MyEShop_DB> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role() {RoleTitle="کاربر",RoleName="User",RoleId=2 },new Role() {RoleId=1,RoleName="Admin",RoleTitle="مدیر"});
            modelBuilder.Entity<User>().HasData(new User() {UserId = 1,RoleId = 1 , Password = "callofdutyblackops4",UserName = "aminq2w3" , Email="aminkarvizi1384@gmail.com",Cart=null});
            base.OnModelCreating(modelBuilder);

        }
    }
}

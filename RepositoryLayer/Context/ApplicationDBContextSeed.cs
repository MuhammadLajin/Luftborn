using DomainLayer.Models;
using SharedDTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace RepositoryLayer.Context
{
    public static class ApplicationDBContextSeed
    {
        public static void BuildEnums(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedRoles();
            modelBuilder.SeedUsers();
            modelBuilder.SeedProducts();
        }

        private static void SeedRoles(this ModelBuilder modelBuilder)
        {
            #region Users
            modelBuilder.Entity<Role>().HasData(new Role() { Id = 1, Name = $"{nameof(SharedEnums.Roles.Seller)}", CreatedAt = DateTime.Now, IsDeleted = false });
            
            #endregion
        }
        private static void SeedUsers(this ModelBuilder modelBuilder)
        {
            #region Users
            modelBuilder.Entity<User>().HasData(new User() {Id = 1, Name = "Seller One", RoleId = (int)(SharedEnums.Roles.Seller), CreatedAt=DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<User>().HasData(new User() {Id = 2, Name = "Seller Two", RoleId = (int)(SharedEnums.Roles.Seller), CreatedAt=DateTime.Now, IsDeleted = false });
           
            #endregion
        }

        private static void SeedProducts(this ModelBuilder modelBuilder)
        {
            #region Users
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 1, Name = "Product One", SellerId = 1, AmountAvailable = 10, Cost = 10, CreatedAt = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 2, Name = "Product Two", SellerId = 1, AmountAvailable = 10, Cost = 20, CreatedAt = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 3, Name = "Product Three", SellerId = 1, AmountAvailable = 10, Cost = 30, CreatedAt = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 4, Name = "Product Four", SellerId = 1, AmountAvailable = 10, Cost = 40, CreatedAt = DateTime.Now, IsDeleted = false });
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 5, Name = "Product Five", SellerId = 1, AmountAvailable = 10, Cost = 50, CreatedAt = DateTime.Now, IsDeleted = false });
            
            #endregion
        }


    }
}

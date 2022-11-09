using Microsoft.EntityFrameworkCore;
using PorscheMarket.Domain.Entity;
using PorscheMarket.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorscheMarket.Dal
{
    public class ApplicationDbContext:DbContext
    {
        //Entities Porsches.
        public DbSet<Porsche> Porsches { get; set; }
        //Entities Users.
        public DbSet<User> Users { get; set; }
        //Entities Baskets.
        public DbSet<Basket> Baskets { get; set; }
        //Entities Orders.
        public DbSet<Order> Orders { get; set; }
        //Construktor.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        //Create Standart Entities in DataBase (Admin:).
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);
                builder.HasData(new User
                {
                    Id=1,
                    Name="Urry",
                    Password=HashPasswordHelper.HashPassword("12345678"),
                    Role=Domain.Enum.Role.Admin
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Basket)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Basket>(builder =>
            {
                builder.ToTable("Baskets").HasKey(x => x.Id);

                builder.HasData(new Basket()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Id);

                builder.HasOne(r => r.Basket).WithMany(t => t.Orders)
                    .HasForeignKey(r => r.BasketId);
            });
        }
    }
}

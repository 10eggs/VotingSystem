using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sandbox.EFCore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<Address> Addresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("Database");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //ShadowProperties - props exist in db rather than in model 
            modelBuilder.Entity<Fruit>().Property<int>("Id");
            modelBuilder.Entity<Address>().Property<int>("FruitId");
        }
    }

    public class Fruit
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public Address Address { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public int FruitId { get; set; }
        public string PostCode { get; set; }
    }
    public class FruitVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello EFCore Sandbox!");

            int orangeId;
            using (var ctx = new AppDbContext())
            {
                var orange = new Fruit { Name = "Orange", Weight = 200 };
                var apple = new Fruit { Name = "Orange", Weight = 200 };
                
                ctx.Fruits.Add(orange);
                orangeId = ctx.Entry(orange).Property<int>("Id").CurrentValue;

                ctx.Fruits.Add(new Fruit { Name = "Apple", Weight = 200 });

                //When we dispose this block, we wont have this address object anymore ?

                ctx.SaveChanges();

            }

            using (var ctx = new AppDbContext())
            {
                var address = new Address { PostCode = "Moon"};
                ctx.Entry(address).Property<int>("FruitId").CurrentValue = orangeId;
                ctx.Addresses.Add(address);
                ctx.SaveChanges();


            }
            using (var ctx = new AppDbContext())
            {
                var fruits = ctx.Fruits
                    .Include(x=>x.Address)
                    .ToList();

                var addresses = ctx.Addresses.ToList();

            }




            //var fruit = ctx.Fruits
            //    .Select(x => new FruitVm
            //    {
            //        Id = EF.Property<int>(x, "Id"),
            //        Name = x.Name,

            //    });
            ////We can add conditional part to query
            //if (true)
            //{
            //    fruit = fruit.Where(x => x.Weight > 1);
            //}



        }
    }
}

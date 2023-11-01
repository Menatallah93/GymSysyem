using GymSysyemWpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class Context : DbContext
    {
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BuyProducts> BuyProducts { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<DailyProfit> DailyProfits { get; set; }
        public DbSet<MonthlyIncome> MonthlyProfits { get; set; }
        public DbSet<OrderItems> OrderItem { get; set; }
        public DbSet<DailyTrainee> DailyTrainees { get; set; }
        public DbSet<TraineeDateAttend> TraineeDateAttend { get; set; }
        public DbSet<AddintionalBill> AddintionalBills { get; set; }





        public Context() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=GymQR;Integrated Security=True; trust server certificate = true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Admin>().HasData(

                    new Admin
                    {
                        ID = 1,
                        UserName = "mario",
                        Password = "12345",
                        EDitAndDeletPassword = "1234",
                        FinancialPassword = "1234",

                    });
        }

    }


}


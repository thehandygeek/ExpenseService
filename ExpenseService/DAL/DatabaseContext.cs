using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using ExpenseService.Models;


namespace ExpenseService.DAL
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext()
           : base("name=Main")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<RecieptImage> Reciepts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>().Property(expense => expense.Amount).HasPrecision(12, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
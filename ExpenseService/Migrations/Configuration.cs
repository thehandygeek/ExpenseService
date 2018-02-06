namespace ExpenseService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ExpenseService.Models;
    using ExpenseService.Utilities;

    internal sealed class Configuration : DbMigrationsConfiguration<ExpenseService.DAL.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExpenseService.DAL.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var adminPasswordHash = PasswordUtility.Hash("HK/e2thqtbetPT");
            context.Users.AddOrUpdate(x => x.Id,
                new User() { Name = "admin", PasswordHash = adminPasswordHash, Type = UserType.Admin }
                );
        }
    }
}

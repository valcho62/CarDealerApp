using CarDealer.Data;

namespace CarDealerApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarDealer.Data.CarDealerContext>
    {
        
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "CarDealer.Data.CarDealerContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CarDealer.Data.CarDealerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
namespace Restauracja.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Restauracja.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Restauracja.Models.RestaurantContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Restauracja.Models.RestaurantContext";
        }

        protected override void Seed(Restauracja.Models.RestaurantContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            SeedRoles(context);
            SeedUsers(context);
            SeedMeals(context);
            SeedOrders(context);
            SeedOrder_Meal(context);
        }

        private void SeedRoles(RestaurantContext context)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Waiter"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Waiter";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Chef"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Chef";
                roleManager.Create(role);
            }
        }

        private void SeedUsers(RestaurantContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            if (!context.Users.Any(u => u.UserName == "Admin@ASP.pl"))
            {
                var user = new User { UserName = "Admin@ASP.pl" };
                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "Waitress@ASP.pl"))
            {
                var user = new User { UserName = "Waitress@ASP.pl" };
                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "Waiter");
            }

            if (!context.Users.Any(u => u.UserName == "Chef@ASP.pl"))
            {
                var user = new User { UserName = "Chef@ASP.pl" };
                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "Chef");
            }
        }

        private void SeedMeals(RestaurantContext context)
        {
            for (float i = 1; i <= 10; i++)
            {
                var meal = new Meal()
                {
                    Id = (int)i,
                    Name = "Nazwa posi³ku" + i.ToString(),
                    Description = "opis posi³ku opis posi³ku min 20 znaków" + i.ToString(),
                    Ingredients = "sk³adniki posi³ku" + i.ToString(),
                    Price = (decimal)(i + i / 10),
                    Allergens = "przyk³adowe alergeny" + (10 - i).ToString(),
                };
                context.Set<Meal>().AddOrUpdate(meal);
            }
            context.SaveChanges();
        }

        private void SeedOrders(RestaurantContext context)
        {
            var userId = context.Set<User>()
                .Where(u => u.UserName == "Waitress")
                .FirstOrDefault().Id;

            for (int i = 1; i <= 10; i++)
            {
                var order = new Order()
                {
                    Id = i,
                    WaiterId = userId,
                    Table = 1,
                    OrderTime = DateTime.Now.AddHours(-i),
                    MealTime = DateTime.Now
                };
                context.Set<Order>().AddOrUpdate(order);
            }
            context.SaveChanges();
        }

        private void SeedOrder_Meal(RestaurantContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                var omeal = new Order_Meal()
                {
                    Id = i,
                    OrderId = 10 - i,
                    MealId = i / 2 + 1,
                };
                context.Set<Order_Meal>().AddOrUpdate(omeal);
            }
            context.SaveChanges();
        }

    }
}

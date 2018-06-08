using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Restauracja.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.


    public class RestaurantContext : IdentityDbContext
    {
        public RestaurantContext()
            : base("DefaultConnection")
        {
        }

        public static RestaurantContext Create()
        {
            return new RestaurantContext();
        }

        public DbSet<Meal> Meal { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Order_Meal> Order_Meal { get; set; }
        public DbSet<User> User { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //modelBuilder.Conventions.Remove< PluralizingTableNameConvention > ();
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //    modelBuilder.Entity<Order>().HasRequired
        //        (x => x.Meal).WithMany(x => x.Order)
        //        .HasForeignKey(x => x.UzytkownikId)
        //        .WillCascadeOnDelete(true);
        //}
    }
}
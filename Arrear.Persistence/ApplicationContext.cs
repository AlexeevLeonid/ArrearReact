using Arrear.Domain.AbstractCore;
using Arrear.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Sep> Seps { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Manager>()
            .HasDiscriminator<string>("workshop")
            .HasValue<ManagerFirstWorkshop>("1")
            .HasValue<ManagerSecondWorkshop>("2");
            mb.Entity<Sep>().HasKey(x => x.Id);
            mb.Entity<Sep>().
                HasOne(x => x.Customer).
                WithMany(x => x.seps).
                HasForeignKey(x => x.CustomerID).
                OnDelete(DeleteBehavior.Restrict);
            mb.Entity<Manager>().
                HasMany(x => x.seps).
                WithOne(x => x.Manager).
                HasForeignKey(x => x.ManagerID).
                IsRequired(false);

        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebDuLich.Models.Entities
{
    public partial class WebDuLich_ConText : DbContext
    {
        internal object seriesRepository;

        public WebDuLich_ConText()
            : base("name=Model115")
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Info> Infoes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(e => e.Infoes)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Info>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Info)
                .WillCascadeOnDelete(false);
        }
    }
}

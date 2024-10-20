using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entities
{
    public partial class CAFEModel : DbContext
    {
        public CAFEModel()
            : base("name=CAFEModel")
        {
        }

        public virtual DbSet<ACCOUNT> ACCOUNT { get; set; }
        public virtual DbSet<BILL> BILL { get; set; }
        public virtual DbSet<BILLINFO> BILLINFO { get; set; }
        public virtual DbSet<COFFEETYPE> COFFEETYPE { get; set; }
        public virtual DbSet<DISCOUNT> DISCOUNT { get; set; }
        public virtual DbSet<DISCOUNTMENU> DISCOUNTMENU { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEE { get; set; }
        public virtual DbSet<INVENTORY> INVENTORY { get; set; }
        public virtual DbSet<MENU> MENU { get; set; }
        public virtual DbSet<TABLECOFFEE> TABLECOFFEE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BILL>()
                .HasMany(e => e.BILLINFO)
                .WithRequired(e => e.BILL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<COFFEETYPE>()
                .HasMany(e => e.INVENTORY)
                .WithRequired(e => e.COFFEETYPE)
                .HasForeignKey(e => e.IDCOFFEE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<COFFEETYPE>()
                .HasMany(e => e.MENU)
                .WithRequired(e => e.COFFEETYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCOUNT>()
                .HasMany(e => e.DISCOUNTMENU)
                .WithRequired(e => e.DISCOUNT)
                .HasForeignKey(e => e.IDDISCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MENU>()
                .HasMany(e => e.BILLINFO)
                .WithRequired(e => e.MENU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MENU>()
                .HasMany(e => e.DISCOUNTMENU)
                .WithRequired(e => e.MENU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TABLECOFFEE>()
                .HasMany(e => e.BILL)
                .WithRequired(e => e.TABLECOFFEE)
                .WillCascadeOnDelete(false);
        }
    }
}

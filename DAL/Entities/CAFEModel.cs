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

        public virtual DbSet<ACCOUNT> ACCOUNTs { get; set; }
        public virtual DbSet<BILL> BILLs { get; set; }
        public virtual DbSet<BILLINFO> BILLINFOes { get; set; }
        public virtual DbSet<COFFEETYPE> COFFEETYPEs { get; set; }
        public virtual DbSet<DISCOUNT> DISCOUNTs { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public virtual DbSet<INVENTORY> INVENTORies { get; set; }
        public virtual DbSet<MENU> MENUs { get; set; }
        public virtual DbSet<TABLECOFFEE> TABLECOFFEEs { get; set; }
        public virtual DbSet<TYPEACCOUNT> TYPEACCOUNTs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BILL>()
                .HasMany(e => e.BILLINFOes)
                .WithRequired(e => e.BILL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<COFFEETYPE>()
                .HasMany(e => e.INVENTORies)
                .WithRequired(e => e.COFFEETYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<COFFEETYPE>()
                .HasMany(e => e.MENUs)
                .WithRequired(e => e.COFFEETYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MENU>()
                .HasMany(e => e.BILLINFOes)
                .WithRequired(e => e.MENU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TABLECOFFEE>()
                .HasMany(e => e.BILLs)
                .WithRequired(e => e.TABLECOFFEE)
                .WillCascadeOnDelete(false);
        }
    }
}

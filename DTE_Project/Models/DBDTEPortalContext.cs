using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DTE_Project.Models
{
    public partial class DBDTEPortalContext : DbContext
    {
        public DBDTEPortalContext()
        {
        }

        public DBDTEPortalContext(DbContextOptions<DBDTEPortalContext> options)
            : base(options)
        {
        }

     
        public virtual DbSet<MstBlock> MstBlocks { get; set; } = null!;
        public virtual DbSet<MstDistrict> MstDistricts { get; set; } = null!;
        public virtual DbSet<MstDivision> MstDivisions { get; set; } = null!;      
        public virtual DbSet<MstState> MstStates { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-2K28FFC;Database=DBDTEPortal;Trusted_Connection=True;TrustServerCertificate=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<MstBlock>(entity =>
            {
                entity.HasKey(e => e.BlockId)
                    .HasName("PK__MstBlock__144215F12235BEB8");

                entity.ToTable("MstBlock");

                entity.Property(e => e.BlockCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.BlockNameEng)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.BlockNameHin).HasMaxLength(65);
            });

            modelBuilder.Entity<MstDistrict>(entity =>
            {
                entity.HasKey(e => e.DistrictId)
                    .HasName("PK__MstDistr__85FDA4C6D56BCE7F");

                entity.Property(e => e.DistrictCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictNameEng)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictNameHin).HasMaxLength(65);
            });

            modelBuilder.Entity<MstDivision>(entity =>
            {
                entity.HasKey(e => e.DivisionId)
                    .HasName("PK__MstDivis__20EFC6A8B719FC58");

                entity.ToTable("MstDivision");

                entity.Property(e => e.DivisionCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.DivisionNameEng)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.DivisionNameHin).HasMaxLength(65);
            });

          

            modelBuilder.Entity<MstState>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__MstState__C3BA3B3AA51C89B1");

                entity.ToTable("MstState");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StateNameEng)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.StateNameHin).HasMaxLength(65);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

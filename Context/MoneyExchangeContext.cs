using System;
using System.Collections.Generic;
using APIBillExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIBillExchange.Context
{
    public partial class MoneyExchangeContext : DbContext
    {
        public MoneyExchangeContext()
        {
        }

        public MoneyExchangeContext(DbContextOptions<MoneyExchangeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Divisa> Divisa { get; set; } = null!;
        public virtual DbSet<Operacion> Operacion { get; set; } = null!;
        public virtual DbSet<TipoDivisa> TipoDivisa { get; set; } = null!;
        public virtual DbSet<TransaccionCambio> TransaccionCambio { get; set; } = null!;

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MoneyExchange;Integrated Security=True");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Divisa>(entity =>
            {
                entity.HasKey(e => e.IdDivisa);

                entity.ToTable("Divisa");

                entity.Property(e => e.IdDivisa).HasColumnName("ID_Divisa");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdTipoDivisa).HasColumnName("ID_Tipo_Divisa");

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdTipoDivisaNavigation)
                    .WithMany(p => p.Divisas)
                    .HasForeignKey(d => d.IdTipoDivisa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Divisa_Tipo_Divisa");
            });

            modelBuilder.Entity<Operacion>(entity =>
            {
                entity.HasKey(e => e.IdOperacion);

                entity.ToTable("Operacion");

                entity.Property(e => e.IdOperacion).HasColumnName("ID_Operacion");

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

                entity.Property(e => e.MontoApagar)
                    .HasColumnType("decimal(20, 2)")
                    .HasColumnName("MontoAPagar");

                entity.Property(e => e.MontoPagado).HasColumnType("decimal(20, 2)");
            });

            modelBuilder.Entity<TipoDivisa>(entity =>
            {
                entity.HasKey(e => e.IdTipoDivisa);

                entity.ToTable("Tipo_Divisa");

                entity.Property(e => e.IdTipoDivisa).HasColumnName("ID_Tipo_Divisa");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransaccionCambio>(entity =>
            {
                entity.HasKey(e => e.IdTransaccionCambio);

                entity.ToTable("Transaccion_Cambio");

                entity.Property(e => e.IdTransaccionCambio).HasColumnName("ID_Transaccion_Cambio");

                entity.Property(e => e.IdDivisa).HasColumnName("ID_Divisa");

                entity.Property(e => e.IdOperacion).HasColumnName("ID_Operacion");

                entity.HasOne(d => d.IdDivisaNavigation)
                    .WithMany(p => p.TransaccionCambios)
                    .HasForeignKey(d => d.IdDivisa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaccion_Cambio_Divisa");

                entity.HasOne(d => d.IdOperacionNavigation)
                    .WithMany(p => p.TransaccionCambios)
                    .HasForeignKey(d => d.IdOperacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaccion_Cambio_Operacion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

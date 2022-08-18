using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Concessionaria.Model
{
    public partial class ConcessionariaContext : DbContext
    {
        public ConcessionariaContext()
        {}

        public ConcessionariaContext(DbContextOptions<ConcessionariaContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Acessorio> Acessorios{get;set;} =null!;
        public virtual DbSet<AcessorioVeiculo> AcessorioVeiculos{get;set;}=null!;
        public virtual DbSet<Proprietario> Proprietarios{get;set;}=null!;
        public virtual DbSet<Veiculo> Veiculos{get;set;}=null!;
        public virtual DbSet<Vendedor> Vendedors{get;set;}=null!;
        public virtual DbSet<Venda> Venda{get;set;}=null!;
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
                optionsBuilder.UseSqlServer("Server=.\\;Database=Concessionaria;Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acessorio>(entity =>
            {
                entity.HasKey(e => e.IdAcessorio)
                    .HasName("PK__Acessori__510EE2691AE6CA80");

                entity.ToTable("Acessorio");

                entity.Property(e => e.IdAcessorio).HasColumnName("idAcessorio");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<AcessorioVeiculo>(entity =>
            {
                entity.HasKey(e => e.IdAcessorioVeiculo)
                    .HasName("PK__Acessori__76F617970486B056");

                entity.ToTable("AcessorioVeiculo");

                entity.Property(e => e.IdAcessorioVeiculo).HasColumnName("idAcessorioVeiculo");

                entity.Property(e => e.IdAcessorio).HasColumnName("idAcessorio");

                entity.Property(e => e.IdVeiculo).HasColumnName("idVeiculo");

                entity.HasOne(d => d.IdAcessorioNavigation)
                    .WithMany(p => p.AcessorioVeiculos)
                    .HasForeignKey(d => d.IdAcessorio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkAcessorioVeiculo");

                entity.HasOne(d => d.IdVeiculoNavigation)
                    .WithMany(p => p.AcessorioVeiculos)
                    .HasForeignKey(d => d.IdVeiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkVeiculoAcessorio");
            });
            modelBuilder.Entity<Proprietario>(entity =>
            {
                
                entity.HasKey(e => e.IdProprietario)
                    .HasName("PK__Propriet__D285281F7D49EEF9");

                entity.ToTable("Proprietario");

                entity.HasIndex(e => e.Documento, "UQ__Propriet__A25B3E61E1642EDE")
                    .IsUnique();

                entity.Property(e => e.IdProprietario).HasColumnName("idProprietario");

                entity.Property(e => e.Cidade)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cidade");

                entity.Property(e => e.Documento)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("documento");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Endereco)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("endereco");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefone");

                entity.Property(e => e.TipoDocumento).HasColumnName("tipoDocumento");
            });

            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.HasKey(e => e.IdVeiculo)
                    .HasName("PK__Veiculo__8178EBE869A1B6CD");

                entity.ToTable("Veiculo");

                entity.Property(e => e.IdVeiculo).HasColumnName("idVeiculo");

                entity.Property(e => e.Ano).HasColumnName("ano");

                entity.Property(e => e.Cor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cor");

                entity.Property(e => e.IdVenda).HasColumnName("idVenda");

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("modelo");

                entity.Property(e => e.NumChassi)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("numChassi");

                entity.Property(e => e.Quilometragem).HasColumnName("quilometragem");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("valor");

                entity.Property(e => e.VersaoSistema)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("versaoSistema");

                entity.HasOne(d => d.IdVendaNavigation)
                    .WithMany(p => p.Veiculos)
                    .HasForeignKey(d => d.IdVenda)
                    .HasConstraintName("fkVendaVeiculo");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.IdVendedor)
                    .HasName("PK__Vendedor__A6693F93E3D6119D");

                entity.ToTable("Vendedor");

                entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Venda>(entity =>
            {
                entity.HasKey(e => e.IdVenda)
                    .HasName("PK__Venda__077BEC2826EECF48");

                entity.Property(e => e.IdVenda).HasColumnName("idVenda");

                entity.Property(e => e.DataVenda)
                    .HasColumnType("date")
                    .HasColumnName("dataVenda")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdProprietario).HasColumnName("idProprietario");

                entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");


                entity.Property(e => e.ValordaVenda)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("valordaVenda")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdProprietarioNavigation)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.IdProprietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkVendaProprietario");

                entity.HasOne(d => d.IdVendedorNavigation)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.IdVendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkVendaVendedor");
            });

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
    }
}

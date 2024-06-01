using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prog3_practica_08.Models;

public partial class Prg3EfPr1Context : DbContext
{
    public Prg3EfPr1Context()
    {
    }

    public Prg3EfPr1Context(DbContextOptions<Prg3EfPr1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Alquilere> Alquileres { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Copia> Copias { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PRG3_EF_PR1;Integrated Security=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alquilere>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkAlquileres");

            entity.ToTable("alquileres");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaAlquiler)
                .HasColumnType("datetime")
                .HasColumnName("fechaAlquiler");
            entity.Property(e => e.FechaEntregada)
                .HasColumnType("datetime")
                .HasColumnName("fechaEntregada");
            entity.Property(e => e.FechaTope)
                .HasColumnType("datetime")
                .HasColumnName("fechaTope");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdCopia).HasColumnName("idCopia");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Alquileres)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkIdClienteAlquileres");

            entity.HasOne(d => d.IdCopiaNavigation).WithMany(p => p.Alquileres)
                .HasForeignKey(d => d.IdCopia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkIdCopiaAlquileres");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkClientes");

            entity.ToTable("clientes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.DocumentoIdentidad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("documentoIdentidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Copia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkCopias");

            entity.ToTable("copias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deteriorada).HasColumnName("deteriorada");
            entity.Property(e => e.Formato)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("formato");
            entity.Property(e => e.IdPelicula).HasColumnName("idPelicula");
            entity.Property(e => e.PrecioAlquiler).HasColumnName("precioAlquiler");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Copia)
                .HasForeignKey(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkIdPelicula");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkPeliculas");

            entity.ToTable("peliculas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

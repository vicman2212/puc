using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Webpuc;

public partial class AccesoFircoContext : DbContext
{
    public AccesoFircoContext()
    {
    }

    public AccesoFircoContext(DbContextOptions<AccesoFircoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=dbacceso");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo);

            entity.ToTable("Modulo", tb => tb.HasComment("Aplicaciones del sistema para Acceso"));

            entity.HasIndex(e => e.CodigoModulo, "UK_Modulo_CodigoModulo").IsUnique();

            entity.Property(e => e.IdModulo).ValueGeneratedNever();
            entity.Property(e => e.CodigoModulo).HasMaxLength(10);
            entity.Property(e => e.Descripcion).HasMaxLength(150);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.Modulo1)
                .HasMaxLength(50)
                .HasColumnName("Modulo");
            entity.Property(e => e.NombreModulo).HasMaxLength(50);
            entity.Property(e => e.Ruta).HasMaxLength(50);
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.IdPerfil);

            entity.ToTable("Perfil");

            entity.HasIndex(e => e.CodigoPerfil, "UK_Perfil_CodigoPerfil").IsUnique();

            entity.Property(e => e.IdPerfil).ValueGeneratedNever();
            entity.Property(e => e.CodigoPerfil)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Perfil1)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("Perfil");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso);

            entity.ToTable("Permiso");

            entity.HasIndex(e => e.CodigoPermiso, "UK_Permiso_CodigoPermiso").IsUnique();

            entity.Property(e => e.IdPermiso).ValueGeneratedNever();
            entity.Property(e => e.CodigoPermiso).HasMaxLength(10);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

            entity.HasOne(d => d.Modulo).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.ModuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permiso_Modulo");

            entity.HasOne(d => d.Perfil).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.PerfilId)
                .HasConstraintName("FK_Permiso_Perfil");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.CodigoRol);

            entity.ToTable("Rol");

            entity.HasIndex(e => e.CodigoRol, "UK_Rol_CodigoRol").IsUnique();

            entity.Property(e => e.CodigoRol).HasMaxLength(10);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Rol1)
                .HasMaxLength(20)
                .HasColumnName("Rol");

            entity.HasOne(d => d.Perfil).WithMany(p => p.Rols)
                .HasForeignKey(d => d.PerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rol_Perfil");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable(tb => tb.HasComment("Usuarios de acceso a los modulos"));

            entity.HasIndex(e => e.NoEmpleado, "UK_Usuarios_NoEmpleado").IsUnique();

            entity.HasIndex(e => e.Usuario1, "UK_Usuarios_Usuario").IsUnique();

            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Pass).HasMaxLength(300);
            entity.Property(e => e.UltimoAcceso).HasColumnType("datetime");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .HasColumnName("Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

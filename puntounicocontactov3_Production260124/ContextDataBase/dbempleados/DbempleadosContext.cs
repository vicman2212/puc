using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Webpuc;

public partial class DbempleadosContext : DbContext
{
    public DbempleadosContext()
    {
    }

    public DbempleadosContext(DbContextOptions<DbempleadosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatalogoEncargado> CatalogoEncargados { get; set; }

    public virtual DbSet<Delegado> Delegados { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EncargadoDepartamento> EncargadoDepartamentos { get; set; }

    public virtual DbSet<EntidadFederativa> EntidadFederativas { get; set; }

    public virtual DbSet<Gerencium> Gerencia { get; set; }

    public virtual DbSet<Localidad> Localidads { get; set; }

    public virtual DbSet<MetaEmpleado> MetaEmpleados { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Nomempfirco> Nomempfircos { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<TipoAsentamiento> TipoAsentamientos { get; set; }

    public virtual DbSet<TituloProfesional> TituloProfesionals { get; set; }

    public virtual DbSet<ViewEmpleado> ViewEmpleados { get; set; }

    public virtual DbSet<ViewGerencium> ViewGerencia { get; set; }

    public virtual DbSet<ViewMetaEmpleado> ViewMetaEmpleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=dbempleados");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogoEncargado>(entity =>
        {
            entity.HasKey(e => e.IdCatalogoEncargado);

            entity.ToTable("CatalogoEncargado", tb => tb.HasComment("Catatogo del status que guarda el pemplado en cada area"));

            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(30);
        });

        modelBuilder.Entity<Delegado>(entity =>
        {
            entity.HasKey(e => e.IdDelegado);

            entity.ToTable("Delegado");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK_areas");

            entity.ToTable("Departamento");

            entity.Property(e => e.IdDepartamento).ValueGeneratedNever();
            entity.Property(e => e.NivelP).HasMaxLength(50);
            entity.Property(e => e.NombreDepartamento).HasMaxLength(100);

            entity.HasOne(d => d.DepartamentoNavigation).WithMany(p => p.InverseDepartamentoNavigation)
                .HasForeignKey(d => d.DepartamentoId)
                .HasConstraintName("FK_Departamento_Departamento");

            entity.HasOne(d => d.Gerencia).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.GerenciaId)
                .HasConstraintName("FK_area_gerencia");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado);

            entity.ToTable("Empleado");

            entity.HasIndex(e => e.Correo, "UK_Correo").IsUnique();

            entity.HasIndex(e => e.NoEmpleado, "UK_NoEmpleado").IsUnique();

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(50);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Curp)
                .HasMaxLength(70)
                .HasColumnName("CURP");
            entity.Property(e => e.Delegado).HasComment("0, el empleado esta delegado en el departamento; 1, El Empleado no esta delegado, su gerencia es la original");
            entity.Property(e => e.Extension).HasMaxLength(30);
            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Rfc)
                .HasMaxLength(50)
                .HasColumnName("RFC");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_Departamento");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.PuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_Puesto");

            entity.HasOne(d => d.Titulo).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.TituloId)
                .HasConstraintName("FK_Empleado_TituloProfesional");
        });

        modelBuilder.Entity<EncargadoDepartamento>(entity =>
        {
            entity.HasKey(e => e.IdEncargadoDepartamento).HasName("PK_EncargadoArea");

            entity.ToTable("EncargadoDepartamento");

            entity.Property(e => e.FechaEncargo).HasColumnType("datetime");
        });

        modelBuilder.Entity<EntidadFederativa>(entity =>
        {
            entity.HasKey(e => e.IdEntidadFederativa).HasName("PK_entidadFederativa");

            entity.ToTable("EntidadFederativa");

            entity.Property(e => e.IdEntidadFederativa).ValueGeneratedNever();
            entity.Property(e => e.EntidadFederativa1)
                .HasMaxLength(100)
                .HasColumnName("EntidadFederativa");
        });

        modelBuilder.Entity<Gerencium>(entity =>
        {
            entity.HasKey(e => e.IdGerencia).HasName("PK_gerencia");

            entity.Property(e => e.IdGerencia).ValueGeneratedNever();
            entity.Property(e => e.Calle).HasMaxLength(100);
            entity.Property(e => e.Conmutador).HasMaxLength(100);
            entity.Property(e => e.MarcRapida).HasMaxLength(50);
            entity.Property(e => e.Numero).HasMaxLength(50);

            entity.HasOne(d => d.Localidad).WithMany(p => p.Gerencia)
                .HasForeignKey(d => d.LocalidadId)
                .HasConstraintName("FK_gerencia_localidad");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.IdLocalidad).HasName("PK_localidad");

            entity.ToTable("Localidad");

            entity.Property(e => e.IdLocalidad).ValueGeneratedNever();
            entity.Property(e => e.AsentaCpconsId).HasColumnName("Asenta_cpconsId");
            entity.Property(e => e.CodigoPostal).HasMaxLength(10);
            entity.Property(e => e.Localidad1)
                .HasMaxLength(100)
                .HasColumnName("Localidad");
            entity.Property(e => e.Zona).HasMaxLength(30);

            entity.HasOne(d => d.TipoAsentamiento).WithMany(p => p.Localidads)
                .HasForeignKey(d => d.TipoAsentamientoId)
                .HasConstraintName("FK_localidad_tipoAsenta");

            entity.HasOne(d => d.Municipio).WithMany(p => p.Localidads)
                .HasForeignKey(d => new { d.MunicipioId, d.EntidadFederativaId })
                .HasConstraintName("FK_localidad_municipio");
        });

        modelBuilder.Entity<MetaEmpleado>(entity =>
        {
            entity.HasKey(e => e.NoEmpleado);

            entity.ToTable("MetaEmpleado");

            entity.Property(e => e.NoEmpleado).ValueGeneratedNever();
            entity.Property(e => e.Metadata).HasMaxLength(800);
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => new { e.IdMunicipio, e.EntidadFederativaId }).HasName("PK_CMunicipio");

            entity.ToTable("Municipio");

            entity.Property(e => e.Municipio1)
                .HasMaxLength(100)
                .HasColumnName("Municipio");

            entity.HasOne(d => d.EntidadFederativa).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.EntidadFederativaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_municipio_entidadFederativa");
        });

        modelBuilder.Entity<Nomempfirco>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("NOMEMPFIRCO");

            entity.Property(e => e.Nafil).HasColumnName("NAFIL");
            entity.Property(e => e.Nagrupacion)
                .HasMaxLength(100)
                .HasColumnName("NAGRUPACION");
            entity.Property(e => e.Nantig)
                .HasMaxLength(50)
                .HasColumnName("NANTIG");
            entity.Property(e => e.Nbpf).HasColumnName("NBPF");
            entity.Property(e => e.Nccto).HasColumnName("NCCTO");
            entity.Property(e => e.Ncl).HasColumnName("NCL");
            entity.Property(e => e.Ncontratista).HasColumnName("NCONTRATISTA");
            entity.Property(e => e.Ncostosp).HasColumnName("NCOSTOSP");
            entity.Property(e => e.Ncpfiscal).HasColumnName("NCPFISCAL");
            entity.Property(e => e.Ncurp)
                .HasMaxLength(50)
                .HasColumnName("NCURP");
            entity.Property(e => e.NdatosAdicionales)
                .HasMaxLength(100)
                .HasColumnName("NDATOS_ADICIONALES");
            entity.Property(e => e.Ndep).HasColumnName("NDEP");
            entity.Property(e => e.Ndig).HasColumnName("NDIG");
            entity.Property(e => e.Ndiscapacidad).HasColumnName("NDISCAPACIDAD");
            entity.Property(e => e.Ndisp1).HasColumnName("NDISP1");
            entity.Property(e => e.Ndisp10)
                .HasMaxLength(100)
                .HasColumnName("NDISP10");
            entity.Property(e => e.Ndisp2).HasColumnName("NDISP2");
            entity.Property(e => e.Ndisp3).HasColumnName("NDISP3");
            entity.Property(e => e.Ndisp4)
                .HasMaxLength(100)
                .HasColumnName("NDISP4");
            entity.Property(e => e.Ndisp5)
                .HasMaxLength(100)
                .HasColumnName("NDISP5");
            entity.Property(e => e.Ndisp6)
                .HasMaxLength(100)
                .HasColumnName("NDISP6");
            entity.Property(e => e.Ndisp7)
                .HasMaxLength(100)
                .HasColumnName("NDISP7");
            entity.Property(e => e.Ndisp8)
                .HasMaxLength(100)
                .HasColumnName("NDISP8");
            entity.Property(e => e.Ndisp9)
                .HasMaxLength(100)
                .HasColumnName("NDISP9");
            entity.Property(e => e.Nemail)
                .HasMaxLength(750)
                .HasColumnName("NEMAIL");
            entity.Property(e => e.Nemailpersonal)
                .HasMaxLength(100)
                .HasColumnName("NEMAILPERSONAL");
            entity.Property(e => e.Nempfeca).HasColumnName("NEMPFECA");
            entity.Property(e => e.Nempfecb).HasColumnName("NEMPFECB");
            entity.Property(e => e.Nentnac)
                .HasMaxLength(50)
                .HasColumnName("NENTNAC");
            entity.Property(e => e.Nfeca).HasColumnName("NFECA");
            entity.Property(e => e.Nfecant).HasColumnName("NFECANT");
            entity.Property(e => e.Nfecinc).HasColumnName("NFECINC");
            entity.Property(e => e.Nfecs).HasColumnName("NFECS");
            entity.Property(e => e.Nfecv).HasColumnName("NFECV");
            entity.Property(e => e.Nfinfec).HasColumnName("NFINFEC");
            entity.Property(e => e.Nfintipo).HasColumnName("NFINTIPO");
            entity.Property(e => e.Nfnacimiento).HasColumnName("NFNACIMIENTO");
            entity.Property(e => e.Nfpnum).HasColumnName("NFPNUM");
            entity.Property(e => e.Nfpnum2).HasColumnName("NFPNUM2");
            entity.Property(e => e.Ngrat).HasColumnName("NGRAT");
            entity.Property(e => e.Nimpef).HasColumnName("NIMPEF");
            entity.Property(e => e.Nimpei).HasColumnName("NIMPEI");
            entity.Property(e => e.Nimss)
                .HasMaxLength(50)
                .HasColumnName("NIMSS");
            entity.Property(e => e.NimssMovtos)
                .HasMaxLength(250)
                .HasColumnName("NIMSS_MOVTOS");
            entity.Property(e => e.Nimssd)
                .HasMaxLength(50)
                .HasColumnName("NIMSSD");
            entity.Property(e => e.Nimssp)
                .HasMaxLength(50)
                .HasColumnName("NIMSSP");
            entity.Property(e => e.Nincpe).HasColumnName("NINCPE");
            entity.Property(e => e.NinfCredito).HasColumnName("NINF_CREDITO");
            entity.Property(e => e.Niof).HasColumnName("NIOF");
            entity.Property(e => e.Nispt)
                .HasMaxLength(50)
                .HasColumnName("NISPT");
            entity.Property(e => e.Nisptd)
                .HasMaxLength(50)
                .HasColumnName("NISPTD");
            entity.Property(e => e.NmodFecha).HasColumnName("NMOD_FECHA");
            entity.Property(e => e.NmodUsuario).HasColumnName("NMOD_USUARIO");
            entity.Property(e => e.Nnom)
                .HasMaxLength(50)
                .HasColumnName("NNOM");
            entity.Property(e => e.Nnomfiscal)
                .HasMaxLength(100)
                .HasColumnName("NNOMFISCAL");
            entity.Property(e => e.Nnomi).HasColumnName("NNOMI");
            entity.Property(e => e.Npass)
                .HasMaxLength(100)
                .HasColumnName("NPASS");
            entity.Property(e => e.Npgarantia).HasColumnName("NPGARANTIA");
            entity.Property(e => e.Nprops).HasColumnName("NPROPS");
            entity.Property(e => e.NpuestoDesc)
                .HasMaxLength(50)
                .HasColumnName("NPUESTO_DESC");
            entity.Property(e => e.NpuestoE)
                .HasMaxLength(50)
                .HasColumnName("NPUESTO_E");
            entity.Property(e => e.NpuestoNo).HasColumnName("NPUESTO_NO");
            entity.Property(e => e.Npvac).HasColumnName("NPVAC");
            entity.Property(e => e.Nrfc)
                .HasMaxLength(50)
                .HasColumnName("NRFC");
            entity.Property(e => e.Nsali).HasColumnName("NSALI");
            entity.Property(e => e.Nsalic).HasColumnName("NSALIC");
            entity.Property(e => e.Nsalif).HasColumnName("NSALIF");
            entity.Property(e => e.Nsaliv).HasColumnName("NSALIV");
            entity.Property(e => e.Nsdo).HasColumnName("NSDO");
            entity.Property(e => e.Nsdoa1).HasColumnName("NSDOA1");
            entity.Property(e => e.Nsdoa2).HasColumnName("NSDOA2");
            entity.Property(e => e.Nsdop1).HasColumnName("NSDOP1");
            entity.Property(e => e.Nsdop2).HasColumnName("NSDOP2");
            entity.Property(e => e.Nsexo)
                .HasMaxLength(50)
                .HasColumnName("NSEXO");
            entity.Property(e => e.NshcpInsc).HasColumnName("NSHCP_INSC");
            entity.Property(e => e.Nsm).HasColumnName("NSM");
            entity.Property(e => e.Nssdo).HasColumnName("NSSDO");
            entity.Property(e => e.Nssdoa).HasColumnName("NSSDOA");
            entity.Property(e => e.Ntarjeta)
                .HasMaxLength(50)
                .HasColumnName("NTARJETA");
            entity.Property(e => e.Ntarjeta2)
                .HasMaxLength(50)
                .HasColumnName("NTARJETA2");
            entity.Property(e => e.Ntipocontrato).HasColumnName("NTIPOCONTRATO");
            entity.Property(e => e.Ntipojornada).HasColumnName("NTIPOJORNADA");
            entity.Property(e => e.Ntiporegimen).HasColumnName("NTIPOREGIMEN");
            entity.Property(e => e.Ntrab).HasColumnName("NTRAB");
            entity.Property(e => e.Nturno).HasColumnName("NTURNO");
            entity.Property(e => e.Numf).HasColumnName("NUMF");
            entity.Property(e => e.Nvacpe).HasColumnName("NVACPE");
            entity.Property(e => e.Nvigente).HasColumnName("NVIGENTE");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.IdPuesto);

            entity.ToTable("Puesto");

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Nivel).HasMaxLength(50);
            entity.Property(e => e.Puesto1)
                .HasMaxLength(50)
                .HasColumnName("Puesto");
        });

        modelBuilder.Entity<TipoAsentamiento>(entity =>
        {
            entity.HasKey(e => e.IdTipoAsentamiento).HasName("PK_tipoAsenta");

            entity.ToTable("TipoAsentamiento");

            entity.Property(e => e.TipoAsentamiento1)
                .HasMaxLength(50)
                .HasColumnName("TipoAsentamiento");
        });

        modelBuilder.Entity<TituloProfesional>(entity =>
        {
            entity.HasKey(e => e.IdTituloProfesional).HasName("PK_TituloProfesional_1");

            entity.ToTable("TituloProfesional");

            entity.Property(e => e.IdTituloProfesional).HasColumnName("id_tituloProfesional");
            entity.Property(e => e.Siglas).HasMaxLength(50);
            entity.Property(e => e.Titulo).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewEmpleado");

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(50);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(50);
            entity.Property(e => e.Calle).HasMaxLength(100);
            entity.Property(e => e.CodigoPostal).HasMaxLength(10);
            entity.Property(e => e.Conmutador).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.EntidadFederativa).HasMaxLength(100);
            entity.Property(e => e.Extension).HasMaxLength(30);
            entity.Property(e => e.Localidad).HasMaxLength(100);
            entity.Property(e => e.Municipio).HasMaxLength(100);
            entity.Property(e => e.NombreDepartamento).HasMaxLength(100);
            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Numero).HasMaxLength(50);
            entity.Property(e => e.Puesto).HasMaxLength(50);
            entity.Property(e => e.Siglas).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewGerencium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGerencia");

            entity.Property(e => e.AsentaCpconsId).HasColumnName("Asenta_cpconsId");
            entity.Property(e => e.Calle).HasMaxLength(100);
            entity.Property(e => e.CodigoPostal).HasMaxLength(10);
            entity.Property(e => e.Conmutador).HasMaxLength(100);
            entity.Property(e => e.EntidadFederativa).HasMaxLength(100);
            entity.Property(e => e.Localidad).HasMaxLength(100);
            entity.Property(e => e.MarcRapida).HasMaxLength(50);
            entity.Property(e => e.Municipio).HasMaxLength(100);
            entity.Property(e => e.Numero).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewMetaEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewMetaEmpleados");

            entity.Property(e => e.Metadata).HasMaxLength(4000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

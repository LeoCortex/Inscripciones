using System;
using System.Collections.Generic;
using InscripcionesApi.src.Data.Models;
using Microsoft.EntityFrameworkCore;    

namespace InscripcionesApi.src.Data;

public partial class InscripcionesContext : DbContext
{
    public InscripcionesContext()
    {
    }

    public InscripcionesContext(DbContextOptions<InscripcionesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asignatu__3214EC071C535AB0");

            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clases__3214EC076A59C27E");

            entity.Property(e => e.Codigo).HasMaxLength(20);

            entity.HasOne(d => d.IdAsignaturasNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdAsignaturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clases__IdAsigna__3E52440B");

            entity.HasOne(d => d.IdProfesoresNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdProfesores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clases__IdProfes__3D5E1FD2");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estudian__3214EC07D2FA46D4");

            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Identificacion).HasMaxLength(20);
            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inscripc__3214EC0714D403EE");

            entity.Property(e => e.Codigo).HasMaxLength(20);

            entity.HasOne(d => d.IdClasesNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdClases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__IdCla__4222D4EF");

            entity.HasOne(d => d.IdEstudiantesNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdEstudiantes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__IdEst__412EB0B6");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profesor__3214EC07985C3622");

            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Identificacion).HasMaxLength(20);
            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

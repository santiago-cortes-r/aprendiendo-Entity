using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using proyectoEF.Models;

namespace proyectoEF;

public class TareasContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) :base(options) { } 
    protected override void OnModelCreating(ModelBuilder moldelbuilder)
    {
        List<Categoria> categoriasIniciales= new List<Categoria>();

        categoriasIniciales.Add(new Categoria() 
        {
            CategoriaId = Guid.Parse("d3b07384-d113-4632-b568-123456789abc"),
            Nombre = "estudio",
            Peso = 10 
        });

        categoriasIniciales.Add(new Categoria() 
        { 
            CategoriaId = Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
            Nombre = "personal",
            Peso = 8 
        });


        moldelbuilder.Entity<Categoria>(categoria=>
        {
            categoria.ToTable("categoria");
            categoria.HasKey(p => p.CategoriaId);

            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p => p.Descripcion).IsRequired(false);
            categoria.Property(p => p.Peso);
            categoria.HasData(categoriasIniciales);
        });

        List<Tarea> tareasIniciales= new List<Tarea>();

        tareasIniciales.Add(new Tarea() 
        { 
            TareaId = Guid.Parse("f1e2d3c4-b5a6-7890-0987-654321abcdef"), 
            CategoriaId = Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0"), 
            Titulo = "remesa",
            PrioridadTarea = Prioridad.alta,
            FechaCreacion = DateTime.Now
        });

        tareasIniciales.Add(new Tarea() {
            TareaId = Guid.Parse("abcdef12-3456-7890-abcd-ef1234567890"),
            CategoriaId = Guid.Parse("d3b07384-d113-4632-b568-123456789abc"),
            Titulo = "curso entity",
            PrioridadTarea = Prioridad.alta,
            FechaCreacion = DateTime.Now 
        });



        moldelbuilder.Entity<Tarea>(Tarea =>
        {
            Tarea.ToTable("Tarea");
            Tarea.HasKey(p => p.TareaId);
            Tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

            Tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
            Tarea.Property(p => p.Descripcion).IsRequired(false);
            Tarea.Property(p => p.PrioridadTarea);
            Tarea.Property(p => p.FechaCreacion);
            // Tarea.Property(p => p.FechaEntrega);
            Tarea.Ignore(p=> p.Resumen);
            Tarea.HasData(tareasIniciales);
        });
    }

}






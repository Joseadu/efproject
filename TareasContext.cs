using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efproyecto.Models;
using Microsoft.EntityFrameworkCore;

namespace efproyecto
{
    public class TareasContext: DbContext
    {
        public DbSet<Category> Categorias { get; set; }
        public DbSet<Todo> Todos { get; set; }

        public TareasContext(DbContextOptions<TareasContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Datos iniciales en la tabla categoría
            List<Category> categoriasInit = new List<Category>();
            categoriasInit.Add(new Category() {
                CategoryId = Guid.Parse("4afb6792-125d-4a8d-8727-d756a620b611"),
                Title = "Deporte",
                Description = "Categoría de deportes"
            });
            categoriasInit.Add(new Category() {
                CategoryId = Guid.Parse("4afb6792-125d-4a8d-8727-d756a620b612"),
                Title = "Estudios",
                Description = "Categoría de estudios"
            });
            // Se definen los atributos y data annotations
            modelBuilder.Entity<Category>(categoria=>
            {
                categoria.ToTable("Categoria");

                categoria.HasKey(p => p.CategoryId);
                categoria.Property(p => p.Title).IsRequired().HasMaxLength(32);
                categoria.Property(p => p.Description).IsRequired().HasMaxLength(180);

                // Se inicializa la tabla con la lista creada arriba
                categoria.HasData(categoriasInit);
            });

            // Datos iniciales en la tabla tareas
            List<Todo> tareasInit = new List<Todo>();
            tareasInit.Add(new Todo() {
                TodoId = Guid.Parse("09aa00ba-6e45-49ab-819a-c6ed88d51991"),
                CategoryId = Guid.Parse("4afb6792-125d-4a8d-8727-d756a620b611"),
                Title = "Ir al gimnasio",
                Description = "Cinta y ejercicios de hombros",
                TodoPriority = Priority.Low,
                Weight = Weight.High,
                CreationDate = DateTime.Now
            });
            tareasInit.Add(new Todo() {
                TodoId = Guid.Parse("09aa00ba-6e45-49ab-819a-c6ed88d51992"),
                CategoryId = Guid.Parse("4afb6792-125d-4a8d-8727-d756a620b612"),
                Title = "Repasar curso APIs con .NET",
                Description = "Repasar para entender mejor las bases",
                TodoPriority = Priority.High,
                Weight = Weight.Medium,
                CreationDate = DateTime.Now
            });
            // Se definen los atributos y data annotations
            modelBuilder.Entity<Todo>(tarea => {
                tarea.ToTable("Tarea");

                tarea.HasKey(t => t.TodoId);
                tarea.HasOne(t => t.Category).WithMany(t => t.Todos).HasForeignKey(t => t.CategoryId);
                tarea.Property(t => t.Title).IsRequired().HasMaxLength(40);
                tarea.Property(t => t.Description).IsRequired().HasMaxLength(180);
                tarea.Property(t => t.TodoPriority);
                tarea.Property(t => t.Weight);
                tarea.Property(t => t.CreationDate);
                tarea.Ignore(t => t.Resumen);

                // Se inicializa la tabla con la lista creada arriba
                tarea.HasData(tareasInit);
            });
        }
    }
}
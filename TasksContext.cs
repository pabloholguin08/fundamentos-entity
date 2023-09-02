using Microsoft.EntityFrameworkCore;
using projectef.Models;

namespace projectef;

public class TasksContext : DbContext
{
    public DbSet<Category> Categorias { get; set; }
    public DbSet<Models.Job> Tareas { get; set; }

    public TasksContext (DbContextOptions<TasksContext> options) : base(options) { }

    //Sobrescribir el metodo con el modelbuilder
    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        List<Category> categoriesInit = new List<Category>();
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("3871496c-0a27-45f9-b3fe-e7342d33b982"), Name = "Actividades Pendientes",  Weight = 20 });
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("3871496c-0a27-45f9-b3fe-e7342d33b9ef"), Name = "Actividades Personales",  Weight = 50 });

        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Category");

            category.HasKey(p => p.CategoryId);
            category.Property(p => p.Name).IsRequired().HasMaxLength(150);
            category.Property(p => p.Description).IsRequired(false);
            category.Property(p => p.Weight);

            category.HasData(categoriesInit);
        });

        List<Job> jobsInit = new List<Job>();
        jobsInit.Add(new Job() { JobId = Guid.Parse("3871496c-0a27-45f9-b3fe-e7342d33b345"), CategoryId= Guid.Parse("3871496c-0a27-45f9-b3fe-e7342d33b982"), JobPriority  = Priority.Medium, Title = "Pago de Servicios Publicos", CreationDate = DateTime.Now });
        jobsInit.Add(new Job() { JobId = Guid.Parse("3871496c-0a27-45f9-b3fe-e7342d33c345"), CategoryId= Guid.Parse("3871496c-0a27-45f9-b3fe-e7342d33b9ef"), JobPriority  = Priority.Low, Title = "Pago de Cuota Pase", CreationDate = DateTime.Now });

        modelBuilder.Entity<Job>(job =>
        {
            job.ToTable("Task");

            job.HasKey(p => p.JobId);
            job.HasOne(p => p.Category).WithMany(p => p.Jobs).HasForeignKey(p => p.CategoryId);
            job.Property(p => p.Title).IsRequired().HasMaxLength(200);
            job.Property(p => p.Description).IsRequired(false);
            job.Property(p => p.JobPriority);
            job.Property(p => p.CreationDate);
            job.Ignore(p => p.Summary);

            job.HasData(jobsInit);
        });
    
    }
}
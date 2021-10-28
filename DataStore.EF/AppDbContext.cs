using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataStore.EF
{
    // 48.1 Наследуемся от базового класса DbContext
    public class AppDbContext : DbContext
    {
        // 48.2 Конфигурируем через получения настроек через конструктор базового класса
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // 48.3 В классе Program сконфигурируем позже, а сейчас добавляем сеты
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        // 48.4 Ради примера рассмотрим использования Fluent API Framework'а
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 48.5 Создаём связь между таблицами с помощью Fluent API
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tickets)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            // 48.6 Инициализируем модели начальными данными
            modelBuilder.Entity<Project>().HasData(
                    new Project { Id = 1, Name = "Project 1"},
                            new Project { Id = 2, Name = "Project 2" }
                );

            modelBuilder.Entity<Ticket>().HasData(
                    new Ticket { Id = 1, Title = "Ticket 1", ProjectId = 1, Description = "Ticket for Project 1" },
                            new Ticket { Id = 2, Title = "Ticket 2", ProjectId = 1, Description = "Ticket for Project 1" },
                            new Ticket { Id = 3, Title = "Ticket 3", ProjectId = 2, Description = "Ticket for Project 2" }
                );
        }
        // **********************************************************************************
    }
}

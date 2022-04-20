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

        // 69.1 / 69.2 Создаём контекст данных новой модели
        public DbSet<EventAdministrator> EventAdministrators { get; set; }

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
                    new Project { Id = 1, Name = "Project 1" },
                            new Project { Id = 2, Name = "Project 2" }
                );

            modelBuilder.Entity<Ticket>().HasData(
                    new Ticket { Id = 1, Title = "Ticket 1", ProjectId = 1, Description = "Ticket for Project 1", EventDate = new DateTime(2022, 2, 2), EnteredDate = new DateTime(2022, 2, 2), Owner = "Michael" },
                            new Ticket { Id = 2, Title = "Ticket 2", ProjectId = 1, Description = "Ticket for Project 1", EventDate = new DateTime(2022, 2, 3), EnteredDate = new DateTime(2022, 2, 3), Owner = "Gabriel" },
                            new Ticket { Id = 3, Title = "Ticket 3", ProjectId = 2, Description = "Ticket for Project 2", EventDate = new DateTime(2022, 3, 5), EnteredDate = new DateTime(2022, 3, 5), Owner = "Rafael" }
                );

            // 69.2 / 69.2 Имплементируем начальные данные в базу
            modelBuilder.Entity<EventAdministrator>().HasData(
                    new EventAdministrator { Id = 1, FirstName = "Admin 1", LastName = "Adminov 1", Age = 34, Phone = "0409612987", Address = "Somestreet 1", ProjectId = 1 },
                            new EventAdministrator { Id = 2, FirstName = "Admin 2", LastName = "Adminov 2", Age = 23, Phone = "0419397987", Address = "Somestreet 2", ProjectId = 1 },
                            new EventAdministrator { Id = 3, FirstName = "Admin 3", LastName = "Adminov 3", Age = 40, Phone = "0459697145", Address = "Somestreet 3", ProjectId = 2 }
                );
        }
        // **********************************************************************************
    }
}

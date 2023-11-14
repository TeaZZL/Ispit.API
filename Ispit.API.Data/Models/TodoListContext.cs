using Microsoft.EntityFrameworkCore;

namespace Ispit.API.Data.Models
{
    public partial class TodoListContext : DbContext
    {
        public TodoListContext() { }

        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
        {
        }
        public virtual DbSet<TodoList> TodoLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS01;database=TodoListDB;" +
                    "integrated security=true;MultipleActiveResultSets=true;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Description)
                    .HasMaxLength(50);

            }
                );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

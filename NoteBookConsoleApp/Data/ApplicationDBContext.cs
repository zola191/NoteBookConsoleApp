using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NoteBookApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Notebook> NoteBooks { get; set; }
        public string _config { get; set; }

        public ApplicationDBContext()
        {
            _config = GetConnectionString();
            Database.Migrate();
        }

        // сделать метод down руками
        // отменить последнюю миграцию внутри entity

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private string GetConnectionString()
        {
            string workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var path = Path.Combine(projectDirectory, @"appsettings.json");

            using (var sr = new StreamReader(path))
            {
                var x = sr.ReadToEnd();
                var anonymous = JsonConvert.DeserializeAnonymousType(x, new
                {
                    ConnectionStrings = new
                    {
                        DefaultConnection = string.Empty,
                    },
                });
                return anonymous.ConnectionStrings.DefaultConnection;
            };
        }
    }
}

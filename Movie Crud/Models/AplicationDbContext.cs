using Microsoft.EntityFrameworkCore;

namespace Car_Works.Models
{
    public class AplicationDbContext:DbContext
    {
        public AplicationDbContext() : base() { }      
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MOVIES; Integrated Security=True;Encrypt=False;Trust Server Certificate=False;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Genre> Genres { get; set; }    
        public DbSet<Movie> Movies { get; set; }
    }
}

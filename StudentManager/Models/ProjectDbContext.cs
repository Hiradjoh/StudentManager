


using Microsoft.EntityFrameworkCore;
using StudentManager.BackEnd.Models.DomainModels.StudentAggregates;

namespace StudentManager.BackEnd.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
          
            
        }
    }
}

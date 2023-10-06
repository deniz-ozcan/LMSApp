using Microsoft.EntityFrameworkCore;
using lmsapp.entity;

namespace lmsapp.data.Concrete
{
    public class LMSContext : DbContext
    {
        public LMSContext(DbContextOptions options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Content> Contents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new CourseConfiguration());
        }
    }
}
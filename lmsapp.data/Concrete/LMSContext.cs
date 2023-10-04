using Microsoft.EntityFrameworkCore;
using lmsapp.data.Configurations;
using lmsapp.entity;

namespace lmsapp.data.Concrete
{
    public class LMSContext : DbContext
    {
        public LMSContext(DbContextOptions options) : base(options) { }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.Seed();
        }
    }
}
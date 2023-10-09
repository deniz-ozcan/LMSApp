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
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Assignee> Assignees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new CourseConfiguration());
        }
    }
}
using Microsoft.EntityFrameworkCore;
using lmsapp.entity;

namespace lmsapp.data.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            var courses = new List<Course>();
            var enrollments = new List<Enrollment>();
            var assignments = new List<Assignment>();
            builder.Entity<Course>().HasData(courses.ToArray());
            builder.Entity<Enrollment>().HasData(enrollments.ToArray());
            builder.Entity<Assignment>().HasData(assignments.ToArray());
        }
    }
}
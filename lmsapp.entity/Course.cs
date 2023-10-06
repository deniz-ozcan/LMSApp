using System.ComponentModel.DataAnnotations;
namespace lmsapp.entity
{
    public class Course
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public User Instructor { get; set; }
        [Required]
        public string InstructorId { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public float Rate { get; set; }
        [Required]
        public bool isUpdated { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int TotalHours { get; set; }
        [Required]
        public string Level { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Section> Sections { get; set; }
        public List<User> Students { get; set; }
    }
}
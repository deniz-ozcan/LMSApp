﻿using System.ComponentModel.DataAnnotations;
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
        public string InstructorId { get; set; }
        [Required]
        public string Category { get; set; }
        public int EnrollmentCount { get; set; }
        public float Rate { get; set; }
        public bool isUpdated { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int TotalHours { get; set; }
        [Required]
        public string Level { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Section> Sections { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
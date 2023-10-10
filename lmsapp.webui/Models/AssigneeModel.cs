using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lmsapp.webui.Models
{
    public class AssigneeModel
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string CourseTitle { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
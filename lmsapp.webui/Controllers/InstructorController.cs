using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using lmsapp.business.Abstract;
using lmsapp.entity;
using lmsapp.webui.Models;
using Microsoft.AspNetCore.Authorization;

namespace lmsapp.webui.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController  : Controller
    {
        private ICourseService _courseService;
        public InstructorController(ICourseService courseService)
        {
            _courseService = courseService;
        }
    }
}
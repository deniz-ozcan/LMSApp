using Microsoft.AspNetCore.Identity;
using lmsapp.business.Abstract;
using lmsapp.entity;

namespace lmsapp.webui.Identity
{
    public static class SeedData
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ICourseService courseService, IConfiguration configuration, IEnrollmentService enrollmentService, IAssignmentService assignmentService, IContentService contentService, ISectionService sectionService)
        {
            var roles = configuration.GetSection("Data:Roles").GetChildren().Select(x => x.Value).ToArray();
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var sectionTitles = new List<string> { "Algorithms", "Data Structures", "Database Management Systems", "Datababase Architecture", "Agile", "Scrum", "Soft Skill" };
            var contents = File.ReadAllLines("../lmsapp.webui/Identity/InitialData/contents.csv");
            var courses = File.ReadAllLines("../lmsapp.webui/Identity/InitialData/courses.csv");
            var assignments = File.ReadAllLines("../lmsapp.webui/Identity/InitialData/assignments.csv");
            var users = configuration.GetSection("Data:Users");
            int step = 1;
            foreach (var section in users.GetChildren())
            {
                var username = section.GetValue<string>("username");
                var password = section.GetValue<string>("password");
                var email = section.GetValue<string>("email");
                var role = section.GetValue<string>("role");
                var firstName = section.GetValue<string>("firstName");
                var lastName = section.GetValue<string>("lastName");
                if (await userManager.FindByNameAsync(username) == null)
                {
                    var user = new User()
                    {
                        UserName = username,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        EmailConfirmed = true,
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        if (role == "Instructor")
                        {
                            var courseCount = section.GetValue<int>("courseCount");
                            for (int i = step; i < step + courseCount; i++)
                            {
                                var f1 = courses[i].Split('|');
                                await courseService.CreateAsync(new Course
                                {
                                    InstructorId = user.Id,
                                    Id = i,
                                    Title = f1[1],
                                    Description = f1[2],
                                    Rate = float.Parse(f1[3]),
                                    TotalHours = int.Parse(f1[4]),
                                    Level = f1[5],
                                    Category = f1[6],
                                    ImageUrl = f1[7],
                                    isUpdated = false
                                });
                            }
                            if (step == 1)
                            {
                                for (int j = 0; j < sectionTitles.Count(); j++)
                                {
                                    await sectionService.CreateAsync(new Section { Title = sectionTitles[j], CourseId = 1 });
                                }
                                var q = 0;
                                for (int j = 1; j < contents.Count(); j++)
                                {
                                    var f2 = contents[j].Split(',');
                                    if (sectionTitles.IndexOf(f2[0]) > q)
                                    {
                                        q = sectionTitles.IndexOf(f2[0]);
                                    }
                                    await contentService.CreateAsync(new Content { SectionId = q + 1, Title = f2[2], VideoUrl = f2[1] });
                                }
                                for (int i = 1; i < assignments.Count(); i++)
                                {
                                    var f3 = assignments[i].Split(',');
                                    await assignmentService.CreateAsync(new Assignment
                                    {
                                        AssignmentId = i,
                                        Title = f3[0],
                                        Description = f3[1],
                                        DueDate = DateTime.Parse(f3[2]),
                                        CourseId = i,
                                        IsSubmitted = false
                                    });
                                }
                            }
                            step += courseCount;
                        }
                        if (role == "Student")
                        {
                            var enrollments = section.GetValue<string>("Enrollments").Split(',').Select(x => int.Parse(x)).ToArray();
                            foreach (var id in enrollments)
                            {
                                await enrollmentService.CreateAsync(new Enrollment() { CourseId = id, UserId = user.Id });
                                Course crs = await courseService.GetCourseByIdAsync(id);
                                crs.EnrollmentCount++;
                                await courseService.UpdateAsync(crs);
                            }
                        }
                        await userManager.AddToRoleAsync(user, role);
                    }
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }
        }
    }
}
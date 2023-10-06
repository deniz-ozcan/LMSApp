using Microsoft.AspNetCore.Identity;
using lmsapp.entity;
using lmsapp.business.Abstract;

namespace lmsapp.webui.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ICourseService courseService, IConfiguration configuration)
        {
            var roles = configuration.GetSection("Data:Roles").GetChildren().Select(x => x.Value).ToArray();
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
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
                        await userManager.AddToRoleAsync(user, role);
                        if (role == "Instructor")
                        {
                            var courseCount = section.GetValue<int>("courseCount");
                            var datas = File.ReadAllLines("../lmsapp.webui/Identity/InitialData/courses.csv");
                            for (int i = step; i < step + courseCount; i++)
                            {
                                var line = datas[i];
                                var fields = line.Split('|');
                                await courseService.CreateAsync(new Course
                                {
                                    Instructor = user,
                                    InstructorId = user.Id,
                                    Id = int.Parse(fields[0]),
                                    Title = fields[1],
                                    Description = fields[2],
                                    Rate = float.Parse(fields[3]),
                                    TotalHours = int.Parse(fields[4]),
                                    Level = fields[5],
                                    Category = fields[6],
                                    ImageUrl = fields[7],
                                    isUpdated = false
                                });
                            }
                            step += courseCount;
                        }
                    }
                }
            }
        }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using lmsapp.business.Abstract;
using lmsapp.business.Concrete;
using lmsapp.data.Abstract;
using lmsapp.data.Concrete;
using lmsapp.webui.EmailServices;
using lmsapp.webui.Identity;
using lmsapp.entity;

namespace lmsapp.webui
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite(_configuration.GetConnectionString("SqliteConnection")));
            services.AddDbContext<LMSContext>(options => options.UseSqlite(_configuration.GetConnectionString("SqliteConnection")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                // Lockout                
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;
                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".LMSApp.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICourseService, CourseManager>();
            services.AddScoped<ISectionService, SectionManager>();
            services.AddScoped<IContentService, ContentManager>();
            services.AddScoped<IAssignmentService, AssignmentManager>();
            services.AddScoped<IEnrollmentService, EnrollmentManager>();

            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
                new SmtpEmailSender(
                    _configuration["EmailSender:Host"],
                    _configuration.GetValue<int>("EmailSender:Port"),
                    _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    _configuration["EmailSender:UserName"],
                    _configuration["EmailSender:Password"])
                );

            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ICourseService courseService, IEnrollmentService enrollmentService, IAssignmentService assignmentService, IContentService contentService, ISectionService sectionService)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Course}/{action=LandingPage}/{id?}");
                endpoints.MapControllerRoute(name: "Course", pattern: "Course", defaults: new { controller = "Course", action = "Index" });
                endpoints.MapControllerRoute(name: "CourseDetails", pattern: "{url}", defaults: new { controller = "Course", action = "Detail" });
                endpoints.MapControllerRoute(name: "Collection", pattern: "Collection", defaults: new { controller = "Student", action = "Enrollments" });
                endpoints.MapControllerRoute(name: "SearchCourses", pattern: "Search/Courses", defaults: new { controller = "Course", action = "Search" });
                endpoints.MapControllerRoute(name: "AdminCourses", pattern: "Admin/Courses", defaults: new { controller = "Admin", action = "Courses" });
                endpoints.MapControllerRoute(name: "AdminPanel", pattern: "Admin/Panel", defaults: new { controller = "Admin", action = "AdminPanel" });
                endpoints.MapControllerRoute(name: "AdminUserEdit", pattern: "Admin/User/{id?}", defaults: new { controller = "Admin", action = "UserUpdate" });
                endpoints.MapControllerRoute(name: "AdminRoleEdit", pattern: "Admin/Role/{id?}", defaults: new { controller = "Admin", action = "RoleUpdate" });
                endpoints.MapControllerRoute(name: "AdminRoleCreate", pattern: "Admin/Role/Create", defaults: new { controller = "Admin", action = "RoleCreate" });
            });
            SeedData.Seed(userManager, roleManager, courseService, configuration, enrollmentService, assignmentService, contentService, sectionService).Wait();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using lmsapp.data.Concrete;
using lmsapp.webui.Identity;

namespace lmsapp.webui.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var LMSContext = scope.ServiceProvider.GetRequiredService<LMSContext>();
                try
                {
                    LMSContext.Database.Migrate();
                }
                catch (Exception)
                {
                    Console.WriteLine("LMSContext migration failed");
                }
                using (var ApplicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    try
                    {
                        ApplicationContext.Database.Migrate();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Application context migration failed");
                    }
                }
            }
            return host;
        }
    }
}
using System.Reflection;

namespace Vehicle_Mono
{
    public static class MapingService
    {
        public static void ConfigureMapingService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }

    }
}
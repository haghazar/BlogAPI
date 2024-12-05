using BlogApi.Repository.Implementations;
using BlogApi.Services.Implementations;
using BlogApi.Services.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Services.Extensions
{
    public static class ServiceInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            AddProFiles(services);
            services.AddScoped<PostRepository>();
            services.AddScoped<TagRepository>();


            services.AddScoped<PostService>();
            services.AddScoped<TagService>();
        }
        private static void AddProFiles(this IServiceCollection services)
        {
            services.AddAutoMapper(m =>
            {
                m.AddProfile<PostProfile>();
                m.AddProfile<TagProfile>();
            },Assembly.GetExecutingAssembly());
        }
    }
}

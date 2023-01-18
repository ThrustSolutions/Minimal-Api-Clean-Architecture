using Application.Abstractions;
using Application.Posts.Queries;
using DataAccess.Repositories;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Abstractions;

namespace MinimalApi.Extensions
{
    public static class MinimalApiExtensions
    { 
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("Default");

            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SocialDbContext>(options => options.UseSqlServer(cs));
            // repositories
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            // MediatR
            builder.Services.AddMediatR(typeof(CreatePost));
        }

        public static void RegisterEndpointDefinitions(this WebApplication app) {
            var endpointsDefinitions = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition))
                    && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach (var endpointDef in endpointsDefinitions)
            {
                endpointDef.RegisterEndpoints(app);

            }
        }
    }
}

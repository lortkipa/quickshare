using Data;
using Dal.Interfaces;
using Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Services;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ProjectDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
            });
            builder.Services.AddScoped<DbContext,  ProjectDBContext>();

            builder.Services.AddScoped<ILinkRepository, LinkRepository>();
            builder.Services.AddScoped<ILinkService, LinkService>();

            builder.Services.AddScoped<ILinkFileRepository, LinkFileRepository>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

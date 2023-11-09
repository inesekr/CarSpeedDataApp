using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NexallApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //   options.UseSqlServer(builder.Configuration.GetConnectionString("nexallDbServer")));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("nexallDbSqlite")));

            builder.Services.AddScoped<DataLoaderService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace NexallApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("nexallDb")));

            // Register DataLoaderService
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


            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var dataLoader = services.GetRequiredService<DataLoaderService>();
            //    dataLoader.LoadDataFromFile("speed.txt");
            //}

            app.Run();
        }
    }
}
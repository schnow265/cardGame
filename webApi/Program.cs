using System.Reflection;
using Microsoft.OpenApi.Models;

namespace webApi
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("cardGame", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "cardGame WebAPI",
                    Description = "The server component of the cardGame Project.",
                    Contact = new OpenApiContact
                    {
                        Name = "schnow265",
                        Email = "thesnowbox@icloud.com",
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var app = builder.Build();

// Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/cardGame/swagger.json", "cardGame");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();


            app.Run();
        }
    }
}
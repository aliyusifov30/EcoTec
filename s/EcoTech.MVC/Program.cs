using Business.Helpers;
using Business.Services;
using Business.Services.Abstractions;
using Business.Services.Concrete;
using Business.Validations;
using Core.Entities;
using DataAccess;
using FluentValidation.AspNetCore;

using System;

namespace EcoTech.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDataAccessLayerServices();

            builder.Services.AddScoped<FileManager>();
            builder.Services.AddScoped<LayoutService>();
            builder.Services.AddScoped<SettingService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(Business.Validations.SliderValidator)));

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();


            //builder.Services.AddValidatorsFromAssemblyContaining<SliderValidator>();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=account}/{action=login}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}
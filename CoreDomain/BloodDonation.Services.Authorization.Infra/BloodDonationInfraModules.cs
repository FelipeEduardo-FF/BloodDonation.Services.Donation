﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using BloodDonation.Services.Donations.Infra.Persistence;
using BloodDonation.Services.Donations.Domain.Repositories;
using BloodDonation.Services.Donations.Infra.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BloodDonation.Services.Donations.Infra.ExternalServices;

namespace BloodDonation.Services.Donations.Infra
{
    public static class BloodDonationInfraModules
    {
        public static IServiceCollection AddBloodDonationInfraModules(this IServiceCollection services)
        {
            services.AddDatabase();
            services.AddRepositories();
            services.AddServices();
            return services;
        }       
        
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IDonationRepository, DonationRepository>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<IDonorService, DonorService>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var _configuration = serviceProvider.GetRequiredService<IConfiguration>();

            services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(_configuration.GetConnectionString("DefaultConnection"),
                            ServerVersion.AutoDetect(_configuration.GetConnectionString("DefaultConnection"))));

            return services;
        }




        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            return app;
        }

      

    }
}

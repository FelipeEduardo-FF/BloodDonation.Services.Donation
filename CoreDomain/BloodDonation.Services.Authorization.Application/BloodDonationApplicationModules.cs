using BloodDonation.Services.Donations.Application.DTO;
using BloodDonation.Services.Donations.Application.DTO.Validations;
using BloodDonation.Services.Donations.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Services.Donations.Application
{
    public static class  BloodDonationApplicationModules
    {
        public static IServiceCollection AddBloodDonationApplicationModules(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EntityToDTOMapper));
            services.AddValidator();
            services.AddServices();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDonationService, DonationService>();
            return services;
        }

        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<DonationInputModelValidator>();
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}

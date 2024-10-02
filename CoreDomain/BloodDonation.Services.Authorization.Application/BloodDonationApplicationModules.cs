using BloodDonation.Services.Donations.Application.DTO;
using BloodDonation.Services.Donations.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Services.Donations.Application
{
    public static class  BloodDonationApplicationModules
    {
        public static IServiceCollection AddBloodDonationApplicationModules(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EntityToDTOMapper));
            services.AddServices();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDonationService, DonationService>();
            return services;
        }
    }
}

using BloodDonation.Services.Donations.Application.DTO;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Services.Donations.Application
{
    public static class  BloodDonationApplicationModules
    {
        public static IServiceCollection AddBloodDonationApplicationModules(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EntityToDTOMapper));
            return services;
        }
    }
}

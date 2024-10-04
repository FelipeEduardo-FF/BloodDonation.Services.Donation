using BloodDonation.Services.Donations.Domain.Entities;
using Shared.Infra.DTO;

namespace BloodDonation.Services.Donations.Infra.ExternalServices
{
    public interface IDonorService
    {
        Task<ApiResponse<Donor>> GetById(int Id);
    }
}
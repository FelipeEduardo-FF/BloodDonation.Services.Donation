using BloodDonation.Services.Donations.Application.DTO.InputModels;
using BloodDonation.Services.Donations.Application.DTO.ViewModels;
using Shared.Infra.Results;

namespace BloodDonation.Services.Donations.Application.Services
{
    public interface IDonationService
    {
        Task<Result<int>> CreateAsync(DonationInputModel inputModel);
        Task<Result> DeleteAsync(int id);
        Task<Result<List<DonationViewModel>>> GetAllAsync();
        Task<Result<DonationViewModel>> GetByIdAsync(int id);
    }
}
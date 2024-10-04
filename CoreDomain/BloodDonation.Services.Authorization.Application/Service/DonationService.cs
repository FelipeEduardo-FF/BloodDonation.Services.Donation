using BloodDonation.Services.Donations.Domain.Repositories;
using Shared.Infra.Results.Errors;
using Shared.Infra.Results;
using BloodDonation.Services.Donations.Domain.Entities;
using BloodDonation.Services.Donations.Application.DTO.InputModels;
using BloodDonation.Services.Donations.Application.DTO.ViewModels;
using AutoMapper;
using BloodDonation.Services.Donations.Infra.ExternalServices;

namespace BloodDonation.Services.Donations.Application.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepository; 
        private readonly IDonorService _donorService; 
        private readonly IMapper _mapper;

        public DonationService(IDonationRepository donationRepository, IMapper mapper, IDonorService donorService)
        {
            _donationRepository = donationRepository;
            _mapper = mapper;
            _donorService = donorService;
        }

        public async Task<Result<DonationViewModel>> GetByIdAsync(int id)
        {
            var donation = await _donationRepository.GetByIdAsync(id);
            if (donation == null)
            {
                return OperationResult.NotFound<DonationViewModel>("Donation not found");
            }

            var donationViewModel = _mapper.Map<DonationViewModel>(donation);
            return OperationResult.Ok(donationViewModel);
        }

        public async Task<Result<List<DonationViewModel>>> GetAllAsync()
        {
            var donations = await _donationRepository.GetAllAsync();
            if (donations == null || !donations.Any())
            {
                return OperationResult.NotFound<List<DonationViewModel>>("No donations found");
            }

            var donationViewModels = donations
                .Select(donation => _mapper.Map<DonationViewModel>(donation))
                .ToList();

            return OperationResult.Ok(donationViewModels);
        }
        //TODO:adiicionar chain of respon
        public async Task<Result<int>> CreateAsync(DonationInputModel inputModel)
        {
            var resultDonor = await _donorService.GetById(inputModel.DonorId);
            if (!resultDonor.Success)
                return OperationResult.BadRequest<int>(resultDonor.Message!);

            var donor = resultDonor.Data;
            if(donor is null)
                return OperationResult.NotFound<int>("Donor is not found");

            if (donor.Weight>50)
                return OperationResult.BadRequest<int>("Donor weight below 50 kg");

            if (donor.BirthDate > DateTime.UtcNow.AddYears(-18))
                return OperationResult.BadRequest<int>("Donor must be over 18 years old.");

            var lastDonation = await _donationRepository.GetLastDonationByDonorIdAsync(inputModel.DonorId);

            if (lastDonation is not null)
            {
                int daysSinceLastDonation = (DateTime.UtcNow - lastDonation.DonationDate).Days;

                if (donor.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase) && (daysSinceLastDonation < 60))
                {
                        return OperationResult.BadRequest<int>("Men can donate every 60 days.");
                }
                else if (donor.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase) && (daysSinceLastDonation < 90))
                {
                        return OperationResult.BadRequest<int>("Women can donate every 90 days.");
                }
            }

            var donation = _mapper.Map<Donation>(inputModel);
            await _donationRepository.AddAsync(donation);
            return OperationResult.Ok(donation.Id);
        }


        public async Task<Result> DeleteAsync(int id)
        {
            var donation = await _donationRepository.GetByIdAsync(id);
            if (donation == null)
            {
                return OperationResult.NotFound("Donation not found");
            }

            await _donationRepository.DeleteAsync(donation);
            return OperationResult.Ok();
        }
    }
}

using BloodDonation.Services.Donations.Domain.Repositories;
using Shared.Infra.Results.Errors;
using Shared.Infra.Results;
using BloodDonation.Services.Donations.Domain.Entities;
using BloodDonation.Services.Donations.Application.DTO.InputModels;
using BloodDonation.Services.Donations.Application.DTO.ViewModels;
using AutoMapper;
using BloodDonation.Services.Donations.Infra.ExternalServices;
using BloodDonation.Services.Donations.Application.ChainOfResponsibility;

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

        public async Task<Result<int>> CreateAsync(DonationInputModel inputModel)
        {          
            var resultDonor = await _donorService.GetById(inputModel.DonorId);

            if (!resultDonor.Success)
                return OperationResult.NotFound<int>(resultDonor.Message!);

            var lastDonation = await _donationRepository.GetLastDonationByDonorIdAsync(inputModel.DonorId);

            var donorExists= new DonorExistsHandler();
            var weightHandler = new DonorWeightHandler();
            var ageHandler = new DonorAgeHandler();
            var lastDonationHandler = new LastDonationHandler(lastDonation);

            donorExists
                .SetNext(weightHandler)
                .SetNext(ageHandler)
                .SetNext(lastDonationHandler);

            var donor = resultDonor.Data;
            var result = donorExists.Handle(donor);

            if (!result.Success)
                return OperationResult.Fail<int>(result.Errors);

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

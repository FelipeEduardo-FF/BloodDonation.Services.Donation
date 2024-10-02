using BloodDonation.Services.Donations.API.Extensions;
using BloodDonation.Services.Donations.Application.DTO.InputModels;
using BloodDonation.Services.Donations.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Infra.Results;

namespace BloodDonation.Services.Donations.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet("{id}")]
        [Authorize("staff,adm")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _donationService.GetByIdAsync(id);

            return result.Match(
                onSuccess: (donation) => Ok(donation),
                onFailure: (error) => error.ToProblemDetails()
            );
        }

        [HttpGet]
        [Authorize("staff,adm")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _donationService.GetAllAsync();

            return result.Match(
                onSuccess: (donations) => Ok(donations),
                onFailure: (error) => error.ToProblemDetails()
            );
        }

        [HttpPost]
        [Authorize("staff,adm")]
        public async Task<IActionResult> Create(DonationInputModel inputModel)
        {
            var result = await _donationService.CreateAsync(inputModel);

            return result.Match(
                onSuccess: (id) => Ok(new { id = result.Value }),
                onFailure: (error) => error.ToProblemDetails()
            );
        }

        [HttpDelete("{id}")]
        [Authorize("staff,adm")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donationService.DeleteAsync(id);

            return result.Match(
                onSuccess: NoContent,
                onFailure: (error) => error.ToProblemDetails()
            );
        }
    }
}

using BloodDonation.Services.Donations.Application.DTO.InputModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Services.Donations.Application.DTO.Validations
{
    public class DonationInputModelValidator:AbstractValidator<DonationInputModel>
    {
        public DonationInputModelValidator()
        {

            RuleFor(d => d.DonorId)
                .NotEmpty().WithMessage("Donor ID cannot be empty.")
                .GreaterThan(0).WithMessage("Donor ID must be greater than 0.");

            RuleFor(d => d.DonorName)
                .NotEmpty().WithMessage("Donor Name cannot be empty.");

            RuleFor(d => d.QuantityML)
                .InclusiveBetween(420, 470).WithMessage("The amount of blood donated must be between 420ml and 470ml.");
        }
    }
}

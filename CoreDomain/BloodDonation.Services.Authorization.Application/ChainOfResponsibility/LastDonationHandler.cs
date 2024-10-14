using BloodDonation.Services.Donations.Domain.Entities;
using Shared.Infra.Results;
using Shared.Infra.Results.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Services.Donations.Application.ChainOfResponsibility
{
    public class LastDonationHandler: DonorHandlerBase
    {
        private readonly Donation? _lastDonation;
        public LastDonationHandler( Donation? lastDonation)
        {
            _lastDonation = lastDonation;
        }

        public override Result Handle(Donor? model)
        {
            if (_lastDonation is not null)
            {
                int daysSinceLastDonation = (DateTime.UtcNow - _lastDonation.DonationDate).Days;

                if (model!.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase) && daysSinceLastDonation < 60)
                {
                    return OperationResult.BadRequest("Men can donate every 60 days.");
                }
                else if (model.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase) && daysSinceLastDonation < 90)
                {
                    return OperationResult.BadRequest("Women can donate every 90 days.");
                }
            }

            return base.Handle(model);
        }
    }
}

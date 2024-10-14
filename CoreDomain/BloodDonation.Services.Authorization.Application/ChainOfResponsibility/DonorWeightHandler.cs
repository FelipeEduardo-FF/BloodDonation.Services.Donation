using BloodDonation.Services.Donations.Domain.Entities;
using Shared.Infra.Results;
using Shared.Infra.Results.Errors;

namespace BloodDonation.Services.Donations.Application.ChainOfResponsibility
{
    public class DonorWeightHandler : DonorHandlerBase
    {
        public override Result Handle(Donor? model)
        {
            if (model!.Weight < 50)
                return OperationResult.BadRequest("Donor's weight must be at least 50 kg.");

            return base.Handle(model);
        }
    }
}

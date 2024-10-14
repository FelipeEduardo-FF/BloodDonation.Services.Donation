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
    public class DonorAgeHandler : DonorHandlerBase
    {
        public override Result Handle(Donor? model)
        {
            if (model!.BirthDate > DateTime.UtcNow.AddYears(-18))
                return OperationResult.BadRequest("Donor must be at least 18 years old.");

            return base.Handle(model);
        }
    }
}

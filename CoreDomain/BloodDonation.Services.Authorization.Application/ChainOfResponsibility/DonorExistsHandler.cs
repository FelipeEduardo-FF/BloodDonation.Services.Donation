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
    public class DonorExistsHandler : DonorHandlerBase
    {
        public override Result Handle(Donor? model)
        {
            if (model == null)
                return OperationResult.NotFound("Donor is not found");

            return base.Handle(model);
        }
    }
}

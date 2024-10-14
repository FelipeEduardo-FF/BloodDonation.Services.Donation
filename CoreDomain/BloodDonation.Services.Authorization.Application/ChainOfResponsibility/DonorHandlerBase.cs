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
    public class DonorHandlerBase : IDonorHandler
    {
        private IDonorHandler? _nextHandler;
        public virtual Result Handle(Donor? model)
        {
            if (_nextHandler == null)
                return OperationResult.Ok();

            var result = _nextHandler.Handle(model);

            return result;
        }

        public IDonorHandler SetNext(IDonorHandler step)
        {
            _nextHandler = step;

            return step;
        }
    }
}

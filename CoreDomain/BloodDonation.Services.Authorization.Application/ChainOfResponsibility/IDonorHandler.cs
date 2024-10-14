using BloodDonation.Services.Donations.Domain.Entities;
using Shared.Infra.Results;

namespace BloodDonation.Services.Donations.Application.ChainOfResponsibility
{
    public interface IDonorHandler
    {
        Result Handle(Donor? model);
        IDonorHandler SetNext(IDonorHandler handler);
    }
}

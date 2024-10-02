using BloodDonation.Services.Donations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Services.Donations.Domain.Repositories
{
    public interface IDonationRepository
    {
        Task<Donation?> GetByIdAsync(int id);
        Task<IEnumerable<Donation>> GetAllAsync();
        Task AddAsync(Donation donation);
        Task DeleteAsync(Donation donation);
    }

}

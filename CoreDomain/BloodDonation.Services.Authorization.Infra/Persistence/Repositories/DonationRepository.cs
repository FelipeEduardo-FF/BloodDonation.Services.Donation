using BloodDonation.Services.Donations.Domain.Entities;
using BloodDonation.Services.Donations.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Services.Donations.Infra.Persistence.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private readonly AppDbContext _context;

        public DonationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Donation?> GetByIdAsync(int id)
        {
            return await _context.Donations.FindAsync(id);
        }

        public async Task<Donation?> GetLastDonationByDonorIdAsync(int id)
        {
            return await _context.Donations.OrderBy(donation=>donation.Id).LastOrDefaultAsync(donation=>donation.DonorId==id);
        }

        public async Task<IEnumerable<Donation>> GetAllAsync()
        {
            return await _context.Donations.ToListAsync();
        }

        public async Task AddAsync(Donation donation)
        {
            await _context.Donations.AddAsync(donation);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Donation donation)
        {

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
            
        }
    }

}

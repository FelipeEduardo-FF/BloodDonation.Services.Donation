using BloodDonation.Services.Donations.Domain.Entities;
using Shared.Infra.DTO;
using Shared.Infra.HttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Services.Donations.Infra.ExternalServices
{
    public class DonorService : IDonorService
    {
        private readonly IHttpService _httpService;
        private const string url = "";

        public DonorService(IHttpService httpService)
        {
            _httpService = httpService;
        }


        public async Task<ApiResponse<Donor>> GetById(int Id)
        {
            //TODO:tratar erros
            var response = await _httpService.Get<ApiResponse<Donor>>(url + Id);
            return response.Response;
        }
    }
}

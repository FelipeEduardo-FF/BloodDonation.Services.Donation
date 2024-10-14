using BloodDonation.Services.Donations.Domain.Entities;
using Shared.Infra.DTO;
using Shared.Infra.HttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Services.Donations.Infra.ExternalServices
{
    public class DonorService : IDonorService
    {
        private readonly IHttpService _httpService;
        private const string url = "https://localhost:7228/api/donor/";

        public DonorService(IHttpService httpService,ICurrentUserService currentUserService)
        {
            _httpService = httpService;
            _httpService.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentUserService.Token);
        }


        public async Task<ApiResponse<Donor>> GetById(int Id)
        {
            var response = await _httpService.Get<ApiResponse<Donor>>(url + Id);
            return response.Response;
        }
    }
}

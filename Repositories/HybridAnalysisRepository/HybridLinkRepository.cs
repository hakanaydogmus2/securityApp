using RestSharp;
using RestSharp.Authenticators;
using securityApp.Helper;
using securityApp.Interfaces.IHybridAnalysesRepository;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace securityApp.Repositories.HybridAnalysesRepository
{
    public class HybridLinkRepository : IHybridLinkRepository
    {
        
        private readonly HybridAnalysisSettings _hybridAnalysisSettings;
        public HybridLinkRepository(HybridAnalysisSettings hybridAnalysisSettings)
        {
            _hybridAnalysisSettings = hybridAnalysisSettings;
        }
        public async Task<RestResponse> GetUrlResultAsync(string encodedUrl)
        {
            Console.WriteLine($"{_hybridAnalysisSettings.OverviewUrl}{encodedUrl}");
            var options = new RestClientOptions($"{_hybridAnalysisSettings.OverviewUrl}{encodedUrl}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("api-key", _hybridAnalysisSettings.ApiKey);

            var response = await client.GetAsync(request);
            Console.WriteLine(response.Content);
            return response;
        }

        public async Task<RestResponse> PostUrlAsync(string url)
        {
            return null;
        }
    }
}

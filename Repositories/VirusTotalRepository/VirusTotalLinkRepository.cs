using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using securityApp.Data;
using securityApp.Helper;
using securityApp.Interfaces;
using securityApp.Interfaces.VirusTotalInterfaces;
using securityApp.Models;

namespace securityApp.Repositories.VirusTotalRepository
{
    public class VirusTotalLinkRepository : IVirusTotalLinkRepository
    {
        private readonly VirusTotalSettings _totalSettings;
        private readonly ScanBuilder _scanBuilder;
        private readonly IScanRepository _scanRepository;
        private readonly DataContext _context;
        private const string getLinkUri = "https://www.virustotal.com/api/v3/urls/";
        public VirusTotalLinkRepository(VirusTotalSettings virusTotalSettings, DataContext dataContext, IScanRepository scanRepository, ScanBuilder scanBuilder)
        {
            _totalSettings = virusTotalSettings;
            _context = dataContext;
            _scanRepository = scanRepository;
            _scanBuilder = scanBuilder;
        }

        public async Task<RestResponse> GetUrlScanResultAsync(string encodedUrl)
        {
            var options = new RestClientOptions($"{getLinkUri}{encodedUrl}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _totalSettings.ApiKey);
            var response = await client.GetAsync(request);
            
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await Task.Delay(3000);
                return await GetUrlScanResultAsync(encodedUrl);
            }
            else
            {
                JObject result = JObject.Parse(response.Content);
                var lastAnalysisResult = result["data"]["attributes"]["last_analysis_results"];
                if (lastAnalysisResult.ToString() == "{}")
                {
                    await Task.Delay(3000);
                    return await GetUrlScanResultAsync(encodedUrl);
                }
            }

            var scan = _scanBuilder.CreateScan(response, encodedUrl);
            _scanRepository.AddScan(scan);
            //Console.WriteLine(response.Content);
            return response;
        }

        public async Task<RestResponse> PostUrlScanAsync(string url)
        {
            var options = new RestClientOptions(_totalSettings.UrlLink);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _totalSettings.ApiKey);
            Console.WriteLine(url);
            request.AddParameter("url", url);
            var response = await client.PostAsync(request);
            //Console.WriteLine(response.Content);
            return response;
        }
    }
}

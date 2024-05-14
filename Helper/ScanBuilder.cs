using Newtonsoft.Json.Linq;
using RestSharp;
using securityApp.Models;
namespace securityApp.Helper
{
    public class ScanBuilder
    {
        public Scan CreateScan(RestResponse response, string encodedUrl)
        {
            var scan = new Scan()
            {
                sha256 = encodedUrl,
                result = JObject.Parse(response.Content)["data"]["attributes"]["last_analysis_stats"].ToString(),
                isMalicious = (int)JObject.Parse(response.Content)["data"]["attributes"]["last_analysis_stats"]["malicious"] > 1 ? true : false
            };
            return scan;
        }
    }
}

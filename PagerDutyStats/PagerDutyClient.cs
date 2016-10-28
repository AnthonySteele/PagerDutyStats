using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PagerDutyStats
{
    public class PagerDutyClient
    {
        private readonly StartupParameters _parameters;

        public PagerDutyClient(StartupParameters parameters)
        {
            _parameters = parameters;
        }

        public async Task<string> GetAbilities()
        {
            using (var client = new HttpClient())
            {
                var request = MakeRequest(_parameters.ApiKey, "abilities");

                return await GetSuccessResponseBody(client, request);
            }
        }

        public async Task<string> TestAbility(string ability)
        {
            using (var client = new HttpClient())
            {
                var request = MakeRequest(_parameters.ApiKey, $"abilities/{ability}");

                return await GetSuccessResponseBody(client, request);
            }
        }


        public async Task<string> GetDataForRange(DateRange range)
        {
            var requestParams = GenerateRequestParams(range);

            using (var client = new HttpClient())
            {
                var request = MakeRequest(_parameters.ApiKey, "incidents?" + requestParams);
                return await GetSuccessResponseBody(client, request);
            }
        }

        private string GenerateRequestParams(DateRange range)
        {
            var since = DateFunctions.AsIso8601Date(range.Start);
            var until = DateFunctions.AsIso8601Date(range.End);

            var resultParams = $"since={since}&until={until}&time_zone=UTC&total=true";
            var ids = _parameters.Services.Split(',');
            foreach (var id in ids)
            {
                resultParams += $"&service_ids[]={id}";
            }
            return resultParams;
        }

        private static HttpRequestMessage MakeRequest(string apiKey, string path)
        {
            const string baseAddress = "https://api.pagerduty.com/";

            return new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(baseAddress + path),
                Headers =
                {
                    {"Accept", "application/vnd.pagerduty+json;version=2"},
                    {"Authorization", $"Token token={apiKey}"}
                }
            };
        }

        private static async Task<string> GetSuccessResponseBody(HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return responseBody;
        }
    }
}

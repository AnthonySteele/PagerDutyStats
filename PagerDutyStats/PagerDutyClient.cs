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
            var since = DateFunctions.AsIso8601Date(range.Start);
            var until = DateFunctions.AsIso8601Date(range.End);

            var requestParams = $"since={since}&until={until}&team_ids[]={_parameters.TeamId}&time_zone=UTC";

            using (var client = new HttpClient())
            {
                var request = MakeRequest(_parameters.ApiKey, "incidents?" + requestParams);
                return await GetSuccessResponseBody(client, request);
            }
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

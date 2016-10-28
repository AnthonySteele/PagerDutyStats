using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PagerDutyStats
{
    public class IncidentCounter
    {
        private readonly PagerDutyClient _client;

        public IncidentCounter(PagerDutyClient client)
        {
            _client = client;
        }

        public async Task<int> CountIncidents(DateRange range)
        {
            var data = await _client.GetDataForRange(range);

            var jsonResponse = JObject.Parse(data);
            var total = jsonResponse.Value<int>("total");

            //var incidents = (JArray)jsonResponse.GetValue("incidents");
            //var hasMore = jsonResponse.Value<bool>("more");
            //Console.WriteLine($"Range: {range}. Incidents: {incidents.Count} of {total}, more: {hasMore}");

            return total;
        }
    }
}

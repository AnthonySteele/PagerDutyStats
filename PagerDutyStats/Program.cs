using System;
using System.Threading.Tasks;
using CommandLine;

namespace PagerDutyStats
{
    public class Program
    {
        public static int Main(string[] args)
        {
            StartupParameters startParams = new StartupParameters();
            if (!Parser.Default.ParseArguments(args, startParams))
            {
                Console.WriteLine("Missing required arguments, exiting...");
                return 1;
            }

            var task = GenerateStats(startParams);
            task.Wait();
            var result = task.Result;

            Console.ReadLine();
            return result;
        }

        private static async Task<int> GenerateStats(StartupParameters startParams)
        {
            try
            {
                var start = DateTime.Today.AddDays(-10);
                start = DateFunctions.NextFriday(start);
                var weekend = DateFunctions.MakeWeekendRange(start);

                var pagerDutyClient = new PagerDutyClient(startParams);
                var incidentCounter = new IncidentCounter(pagerDutyClient);

                var count = await incidentCounter.CountIncidents(weekend);
                Console.WriteLine("Count is " + count);

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Run failed: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                return 1;
            }
        }

    }
}

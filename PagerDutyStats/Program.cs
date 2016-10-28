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
                var monthsBack = startParams.MonthsBack ?? 6;
                var start = DateTime.Today.AddMonths(- monthsBack);
                start = DateFunctions.NextFriday(start);
                var startWeekend = DateFunctions.MakeWeekendRange(start);
                Console.WriteLine($"Starting at {startWeekend}");

                var pagerDutyClient = new PagerDutyClient(startParams);
                var incidentCounter = new IncidentCounter(pagerDutyClient);

                await ReadWeekends(incidentCounter, startWeekend);

                Console.WriteLine("Done");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Run failed: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                return 1;
            }
        }

        private static async Task ReadWeekends(IncidentCounter incidentCounter, DateRange range)
        {
            while (range.End <= DateTime.Today)
            {
                var count = await incidentCounter.CountIncidents(range);
                Console.WriteLine($"Count at {range} is {count}");

                range = DateFunctions.NextWeekend(range);
            }
        }
    }
}

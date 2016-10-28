using CommandLine;

namespace PagerDutyStats
{
    public class StartupParameters
    {
        [Option("ApiKey", HelpText = "The pager duty Api key", Required = true)]
        public string ApiKey { get; set; }

        [Option("Services", HelpText = "The services to read", Required = true)]
        public string Services { get; set; }

        [Option("MonthsBack", HelpText = "How many monthss back")]
        public int? MonthsBack { get; set; }
    }
}

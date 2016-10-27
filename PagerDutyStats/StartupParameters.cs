using CommandLine;

namespace PagerDutyStats
{
    public class StartupParameters
    {
        [Option("ApiKey", HelpText = "The pager duty Api key", Required = true)]
        public string ApiKey { get; set; }

        [Option("TeamId", HelpText = "The id of the team to read", Required = true)]
        public string TeamId { get; set; }
    }
}

# PagerDutyStats

This simple program uses the [PagerDuty Api](https://v2.developer.pagerduty.com/page/api-reference) to answer the question: How many times are these services called out over per weekend, over time.
Where the weekend is arbitrarily defined as 5pm Friday to 9am Monday.

CSV data is written to file `out.txt`, containing pairs of `StartDate, Count` for each weekend in the range.

Command line options are:

 * `--ApiKey` An Api key for the PagerDuty v2 Api. Required.  
* `--Services` A comma-separated list of services to read. Required.  
* `--MonthsBack` Number of months to go back. Optional, default is 6.   

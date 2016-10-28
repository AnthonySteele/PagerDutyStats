# PagerDutyStats

This simple program uses the [PagerDuty Api](https://v2.developer.pagerduty.com/page/api-reference) to anwer the question: How many times are these services called out over per weekend, over time.
Where the weekend is arbitrarily defined as 5pm Friday tyo 9am Monday.

CSV data is written to file, containing pairs of `StartDate, Count` for each weekend in range.

Commmandline options

`--ApiKey` An Api key for the PagerDuty v2 api. Required.  
`--Services` A comma-seperated list of services to read. Required.  
`--MonthsBack` Number of months to go back. Optional, default is 6.   

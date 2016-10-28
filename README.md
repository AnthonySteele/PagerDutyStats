# PagerDutyStats

This simple pogram uses the pagerDuty Api to anwer the question: How many times are these services called out over per weekend, over time.
Where the weekend is arbitrarily defined as 5pm Friday tyo 9am Monday.

CVS data is written to file.

Commmandline options

`--ApiKey` An Api key for the PagerDuty v2 api. Required.  
`--Services` A comma-seperated list of services to read. Required.  
`--MonthsBack` Number of months to go back. Optional, default is 6.   

@webapi
Feature: Order Details

Rule: Pizza should be ordered for today by default

@login
Scenario: Pizza is ordered without specifying delivery time
	Given the client has items in the basket
	When the client checks the my order page
	Then the order should indicate that the delivery date is today

Rule: Pizza delivery time can be specified

@login
Scenario Outline: Pizza is ordered for different dates and times
	Given the client has items in the basket
	When the client specifies <date> at <time> as delivery time
	Then the order should indicate that the delivery date is <date>
	And the delivery time should be <time>

Examples: 
	| description         | date         | time  |
	| later today         | today        | 5pm   |
	| tomorrow lunch      | tomorrow     | noon  |
	| meeting in two days | 2 days later | 13:30 |

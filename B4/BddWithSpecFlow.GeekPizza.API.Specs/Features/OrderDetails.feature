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
Scenario: Pizza is ordered for tomorrow
	Given the client has items in the basket
	When the client specifies tomorrow at 13:30 as delivery time
	Then the order should indicate that the delivery date is tomorrow

@login
Scenario: Pizza is ordered for a different date
	Given the client has items in the basket
	When the client specifies 5 days later at 12:00 as delivery time
	Then the order should indicate that the delivery date is 5 days later

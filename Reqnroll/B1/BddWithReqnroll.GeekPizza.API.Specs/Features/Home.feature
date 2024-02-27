Feature: Home

Rule: The user name should be shown on the home page if logged in

@webapi
Scenario: The logged-in user name is shown on home page
	Given the client is logged in
	When the client checks the home page
	Then the user name of the client should be on the home page
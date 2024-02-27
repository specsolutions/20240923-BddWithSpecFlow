Feature: Home

Rule: A positive message should be shown on the home page

Scenario: Welcome message is shown on home page
	When the client checks the home page
	Then the home page main message should be: "Welcome to Geek Pizza!"

Rule: The user name should be shown on the home page if logged in

Scenario: The logged-in user name is shown on home page
	Given the client is logged in
	When the client checks the home page
	Then the user name of the client should be on the home page
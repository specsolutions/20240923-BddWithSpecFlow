Feature: Home

Rule: A positive message should be shown on the home page

Scenario: Welcome message is shown on home page
	When the client checks the home page
	Then the home page main message should be: "Welcome to Geek Pizza!"

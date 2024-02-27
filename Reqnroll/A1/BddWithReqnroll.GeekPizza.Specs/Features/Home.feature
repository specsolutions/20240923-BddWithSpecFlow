Feature: Home

Rule: A positive message should be shown on the home page

Scenario: Welcome message is shown on home page
	When the client checks the home page
	Then the home page main message should be: "Welcome to Geek Pizza!"

Rule: Inactive pizzas should not be listed

Scenario: Only active pizza menu items are listed on the menu page
	Given the menu has been configured to contain 5 active and 2 inactive pizzas
	When the client checks the menu page
	Then there should be 5 pizzas listed

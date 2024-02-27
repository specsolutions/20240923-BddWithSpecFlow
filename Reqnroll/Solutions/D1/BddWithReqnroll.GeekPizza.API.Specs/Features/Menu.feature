Feature: Menu

Rule: Inactive pizzas should not be listed

@webapi
Scenario: Only active pizza menu items are listed on the menu page
	Given the menu has been configured to contain 5 active and 2 inactive pizzas
	When the client checks the menu page
	Then there should be 5 pizzas listed

Rule: Pizzas on menu should be listed alphabetically by name

Scenario: The pizzas are listed in alphabetically on the menu page
	Given the menu has been configured to contain the following pizzas
		| name             | 
		| Margherita       | 
		| Fitness          | 
		| BBQ              | 
	When the client checks the menu page
	Then the following pizzas should be listed in this order
		| name             |
		| BBQ              |
		| Fitness          | 
		| Margherita       |

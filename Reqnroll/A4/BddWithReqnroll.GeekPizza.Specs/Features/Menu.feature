Feature: Menu

Rule: Inactive pizzas should not be listed

Scenario: Only active pizza menu items are listed on the menu page
	Given the menu has been configured to contain 5 active and 2 inactive pizzas
	When the client checks the menu page
	Then there should be 5 pizzas listed

Rule: Pizzas on menu should be listed alphabetically by name

Scenario: The pizzas are listed in alphabetically on the menu page
	# Ignore the incidental details in the next step for now
	Given the menu has been configured to contain the following pizzas
		| name             | ingredients                                          | calories | inactive |
		| Margherita       | tomato slices, oregano, mozzarella                   | 1920     | false    |
		| Fitness          | sweetcorn, broccoli, feta cheese, mozzarella         | 1340     | false    |
		| BBQ              | BBQ sauce, bacon, chicken breast strips, onions      | 1580     | false    |
		| Mexican          | taco sauce, bacon, beans, sweetcorn, mozzarella      | 2340     | false    |
		| Quattro formaggi | blue cheese, parmesan, smoked mozzarella, mozzarella | 2150     | false    |
	When the client checks the menu page
	Then the following pizzas should be listed in this order
		| name             |
		| BBQ              |
		| Fitness          | 
		| Margherita       |
		| Mexican          |
		| Quattro formaggi |

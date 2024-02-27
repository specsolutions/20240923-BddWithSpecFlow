@webapi @interface
Feature: Web API for Menu

Rule: Menu items can be retrieved by ID

Scenario: A menu item is retrieved by ID
	Given the menu has been configured to contain the following pizzas
		| # | name       | ingredients                                     | calories |
		| 1 | Margherita | tomato slices, oregano, mozzarella              | 1920     |
		| 2 | Fitness    | sweetcorn, broccoli, feta cheese, mozzarella    | 1340     |
		| 3 | BBQ        | BBQ sauce, bacon, chicken breast strips, onions | 1580     |
	When the menu item #1 is retrieved from the menu API resource by ID
	Then the retrieved menu item should contain
		| name       | ingredients                        | calories |
		| Margherita | tomato slices, oregano, mozzarella | 1920     |

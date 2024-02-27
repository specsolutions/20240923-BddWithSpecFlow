@webapi
Feature: My Order

Rule: The pizzas the client has put into the basket are listed on the My Order page

Scenario: Customer has a few different pizzas in the basket
	Given the menu has been configured to contain the following pizzas
		| name             | ingredients                                          | calories | inactive |
		| Margherita       | tomato slices, oregano, mozzarella                   | 1920     | false    |
		| Fitness          | sweetcorn, broccoli, feta cheese, mozzarella         | 1340     | false    |
		| BBQ              | BBQ sauce, bacon, chicken breast strips, onions      | 1580     | false    |
		| Mexican          | taco sauce, bacon, beans, sweetcorn, mozzarella      | 2340     | false    |
		| Quattro formaggi | blue cheese, parmesan, smoked mozzarella, mozzarella | 2150     | false    |
	And there is a user registered with user name 'Ford' and password '1423'
	And the client is logged in with user name 'Ford' and password '1423'
	And the client has the following items in the basket
		| name       | size   |
		| Margherita | Small  |
		| BBQ        | Medium |
	When the client checks the my order page
	Then the following items should be listed on the my order page
		| name       | size   |
		| Margherita | Small  |
		| BBQ        | Medium |


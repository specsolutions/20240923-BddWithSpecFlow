Feature: Pricing

Rule: Item price depends only on the size

@login
Scenario Outline: Pizza cost is $10, $15, $25 for small, medium and large pizza
	Given the client has a <size> item in the basket
	When the client checks the price
	Then the subtotal should be <price>
Examples: 
	| size   | price |
	| small  | $10   |
	| medium | $15   |
	| large  | $25   |

Rule: Subtotal is the sum of item prices

@login
Scenario: Subtotal is calculated of a mix of order items
	Given the client has a small item in the basket
	And the client has 2 medium items in the basket
	When the client checks the price
	Then the subtotal should be $40
	#alternative without depending on the exact prices
	Then the subtotal should be the sum of the order item prices

Rule: Total is the sum of Subtotal and Delivery Costs

@login
Scenario: The Total is calculated of an order with delivery cost
	Given the client has a small item in the basket
	When the client checks the price
	Then the total should be $15
	#alternative without depending on the exact prices
	Then the delivery should not be free
	And the total should be the sum of subtotal and delivery costs

Rule: Delivery cost is $5 below $40

@login
Scenario: Delivery costs depends on Subtotal
	Given the client has items in the basket with subtotal of <subtotal>
	When the client checks the price
	Then the delivery costs should be <delivery costs>
Examples: 
	| description | subtotal | delivery costs |
	| below $40   | $30      | $5             |
	| exactly $40 | $40      | $5             |
	| above $40   | $50      | $0             |

Rule: buy 3, get 1 free

@login
Scenario: Promotion for same pizza size
	Given the client has <number> small item in the basket
	When the client checks the price
	Then the subtotal should be <price>
Examples: 
	| description         | number | price |
	| no free pizza yet   | 3      | $30   |
	| first free          | 4      | $30   |
	| still only one free | 7      | $60   |
	| second free         | 8      | $60   |

@login
Scenario: Always the cheapest is free
	Given the client has <small> small item in the basket
	And the client has <medium> medium item in the basket
	When the client checks the price
	Then the subtotal should be <price>
Examples: 
	| description       | small | medium | price |
	| single cheapest   | 1     | 3      | $45   |
	| multiple cheapest | 2     | 2      | $40   |

@login
Scenario: Always the cheapest is free (alternative without depending on the exact prices)
	Given the client has <small> small item in the basket
	And the client has <medium> medium item in the basket
	When the client checks the price
	Then the subtotal should be for <paid small> small and <paid medium> medium pizzas
Examples: 
	| description       | small | medium | paid small | paid medium |
	| single cheapest   | 1     | 3      | 0          | 3           |
	| multiple cheapest | 2     | 2      | 1          | 2           |


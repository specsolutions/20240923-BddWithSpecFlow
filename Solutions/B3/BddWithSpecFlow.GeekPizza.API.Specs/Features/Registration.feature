@webapi
Feature: Registration

Describes the behavior of the registration process. 

There are many rules involved (e.g. related to user name and password validity 
or password and re-entered password match). For the sake of the course we only
work with a single rule: Should be able to register with user name and password.

Rule: Should be able to register with user name and password

Scenario: Customer registers successfully
	When the client attempts to register with user name "Trillian" and password "139139"
	Then the registration should be successful

	
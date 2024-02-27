Feature: Authentication

Rule: Successful login authorizes for member-only services

Scenario: Client logs in with valid credentials
	Given there is a user registered with user name 'Trillian' and password '139139'
	When the client attempts to log in with user name "Trillian" and password "139139"
	Then the login attempt should be successful
	And the client should be able to access member-only services

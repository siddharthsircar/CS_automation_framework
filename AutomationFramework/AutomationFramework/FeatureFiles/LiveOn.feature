Feature: LiveOn


@mytag
Scenario: VerifyLogin
	Given I have logged into the application
	When I am on the dashboard page
	Then I press logout button

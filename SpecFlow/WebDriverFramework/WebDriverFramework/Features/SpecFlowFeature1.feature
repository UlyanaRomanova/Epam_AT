Feature: Test Create Product
	As a user 
	I want to log in to the site "http://localhost:5000" and go to the tab "All Products"
	So I can create a new product and check it availability


Scenario: Autorization and create new product
	Given I open "http://localhost:5000" url
	When I log in with the "user", "user" parameters
	And I click the All products link
	And I click the Create new button
	And I create product with values "Coffee", "Beverages", "Exotic Liquids", "25", "5 boxes x 10 bags", "32", "3", "5", check Discontinued
	Then the product "Coffee" must be created
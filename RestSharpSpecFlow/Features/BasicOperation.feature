Feature: Basic Operation
	Covers Basic API Test
@smoke
Scenario: Get Products
	Given I perform a GET operation of "/Product/GetProductById/{id}"
	| ProductID |
	| 1         |
	Then I validate the Name as "Keyboard"
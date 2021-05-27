@all
Feature: RestApi

	@2305211842
	Scenario: Post/Get
		* create post reqest
		* check get/post

	@2305211842
	Scenario: Validate date
		* create post reqest
		* check validate date

	@2305211843
	Scenario: Validate date (not valid)
		* create post reqest
		* update with not valid date
		* check validate date not valid

	@2305211844
	Scenario: Put
		* create post reqest
		* check put

	@2305211846
	Scenario: Delete
		* create post reqest
		* check delete

	@2305211846
	Scenario: Get uncorrect guid
		* check get error

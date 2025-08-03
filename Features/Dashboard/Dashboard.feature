@Dashboard
Feature: OrangeHRM Dashboard

  Background:
    Given I navigate to OrangeHRM login page
    When I login with valid OrangeHRM credentials

  Scenario: Dashboard is visible after login
    Then I should see key dashboard widgets
#And I should see the user profile dropdown
#And I should see the quick launch panel
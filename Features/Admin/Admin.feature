@Admin
Feature: Admin Dashboard and functionality

  Background:
    Given I navigate to OrangeHRM login page
    When I login with valid OrangeHRM credentials


  Scenario: Dashboard is visible after login
    Then I should see key dashboard widgets
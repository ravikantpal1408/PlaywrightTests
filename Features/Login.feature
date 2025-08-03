Feature: OrangeHRM Login

  Scenario: Successful login to OrangeHRM
    Given I navigate to OrangeHRM login page
    When I login with valid OrangeHRM credentials
    Then I should be redirected to the OrangeHRM dashboard

  Scenario: Failed login to OrangeHRM
    Given I navigate to OrangeHRM login page
    When I login with invalid OrangeHRM credentials
    Then I should be error message
    

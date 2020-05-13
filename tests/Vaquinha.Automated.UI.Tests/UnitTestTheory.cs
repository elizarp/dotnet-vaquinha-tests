using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
    public class UnitTestTheory : IDisposable
    {
        private DriverFactory _driverFactory = new DriverFactory();
        private IWebDriver    _driver;

        public void Dispose()
        {
            _driverFactory.Close();            
        }

        [Theory]
        [InlineData ("pesquisando alguma coisa no google.")]
        [InlineData ("pesquisando outra coisa no google.")]
        public void TestTheory(String text)
        {
            //Arrange
            _driverFactory.NavigateToUrl("https://www.google.com.br");
            _driver = _driverFactory.GetWebDriver();

            //Act
            IWebElement webElement = null;
            webElement = _driver.FindElement(By.Name("q"));                       
            webElement.SendKeys(text);  

            //Assert
                   
        }
    }
}
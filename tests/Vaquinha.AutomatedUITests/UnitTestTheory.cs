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

        // Test Escrito com Theory, as duas outras notations (InlineData), encontem os valoes para chamada do teste. 
        // Como neste exemplo apenas tem o parâmetro String text ficou [InlineData ("texto de parâmetro")], 
        // Caso fosse necessário informar mais que um parâmetro, basta separar por , os valores, por exemplo:
        // [InlineData ("Parâmetro 1", "Parâmetro 2", 3)] e assim por diante. Como uma chamada de método.
        [Theory]
        [InlineData ("pesquisando alguma coisa no google.")]
        [InlineData ("pesquisando outra coisa no google.")]
        public void TestTheory(String text)
        {
            _driverFactory.NavigateToUrl("https://www.google.com.br");
            _driver = _driverFactory.GetWebDriver();

            IWebElement webElement = null;
            webElement = _driver.FindElement(By.Name("q"));                       
            webElement.SendKeys(text);         
        }
    }
}
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;
using FluentAssertions;

namespace Vaquinha.AutomatedUITests
{
	// IDisposable para chamar Dispose() e fechar o browser no término do teste.
	public class UnitTestFact : IDisposable
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void TestFact()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
	}
}
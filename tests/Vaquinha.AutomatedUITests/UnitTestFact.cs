using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
	// Class com herança do IDisposable para chamar Dispose() e fechar o browser quando terminar tudo.
	public class UnitTestFact : IDisposable
	{
		// Cria DriverFactory
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		// Fecha browser.
		public void Dispose()
		{
			_driverFactory.Close();
		}

		// Test Escrito com Fact
		[Fact]
		public void TestFact()
		{
			// abre url no driver factory
			_driverFactory.NavigateToUrl("http://localhost:5000/");
			// obtem driver do factory.
			_driver = _driverFactory.GetWebDriver();

			// IWebElement é utilizado para armazenar os elementos identificados das páginas.
			IWebElement webElement = null;
			// Procura no html da pagina um elemento com ID "hplogo" (Logo do Goole)
			webElement = _driver.FindElement(By.ClassName("container"));

			// Verifica se o elemento foi encontrado e se está exibido.
			Assert.True(webElement.Displayed);
		}
	}
}
using Microsoft.Extensions.Logging;
using Moq;
using Vaquinha.MVC.Controllers;
using Vaquinha.Domain;

namespace Vaquinha.Unit.Tests.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly IHomeInfoService _homeInfoService;
        private readonly Mock<ILogger<HomeController>> _logger;

        public HomeControllerTests()
        {

        }
    }
}

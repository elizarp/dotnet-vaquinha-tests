using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Vaquinha.MVC;

namespace Vaquinha.Unit.Tests.Extensions
{
    public static class IActionExtensions
    {
       /* public static ResponseModel Content(this Task<IActionResult> objectResult)
        {
            return (objectResult.Result as ObjectResult).Value as ResponseModel;
        }*/

        public static void StatusCodeShouldBe(this Task<IActionResult> objectResult, HttpStatusCode httpStatusCode)
        {
            (objectResult.Result as ObjectResult).StatusCode.Should().Be((int)httpStatusCode);
        }
    }
}
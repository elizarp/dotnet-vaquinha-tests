// Referencias do Framework
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// Referencias de Pacotes
using FluentAssertions;
using AutoMapper;
using Moq;
using Xunit;
using NToastNotify;

// Referencias de Projeto
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.ViewModels;
using Vaquinha.MVC.Controllers;
using Vaquinha.Service;
using Vaquinha.Tests.Common.Fixtures;



namespace Vaquinha.Unit.Tests.ControllerTests
{
	[Collection(nameof(CausaFixtureCollection))]
	public class CausaControllerTests : IClassFixture<CausaFixture>
	{
		#region Atributos
		private readonly CausaFixture _fixture;
		private readonly CausaController _controller;
		private readonly ICausaService _domainService;

		private readonly Mock<IMapper> _mapper;
		private readonly Mock<GloballAppConfig> _globalAppConfig;
		private readonly Mock<ILogger<CausaController>> _logger;
		private readonly Mock<IToastNotification> _toastNotification;
		private readonly Mock<IDomainNotificationService> _domainNotificationService;
		private readonly Mock<ICausaRepository> _repository;

		private readonly IEnumerable<Causa> _causaValida;
		private readonly IEnumerable<CausaViewModel> _causaViewModelValida;

		private readonly IEnumerable<Causa> _causaInvalida;
		private readonly IEnumerable<CausaViewModel> _causaViewModelInvalida;

		#endregion

		#region Construtores
		public CausaControllerTests(CausaFixture fixture)
		{
			_fixture = fixture;

			_mapper = new Mock<IMapper>();
			_repository = new Mock<ICausaRepository>();
			_toastNotification = new Mock<IToastNotification>();

			_globalAppConfig = new Mock<GloballAppConfig>();
			_logger = new Mock<ILogger<CausaController>>();

			_causaValida = _fixture.CausaValida();
			_causaViewModelValida = _fixture.CausaViewModelValida();

			_causaInvalida = _fixture.CausaVaziaInvalida();
			_causaViewModelInvalida = _fixture.CausaViewModelInvalida();

			_domainNotificationService = new Mock<IDomainNotificationService>();
			_domainService = new CausaService(_mapper.Object, _repository.Object, _domainNotificationService.Object);
			_controller = new CausaController(_domainService, _domainNotificationService.Object, _toastNotification.Object);

		}
		#endregion

		#region Métodos / Testes
		[Fact]
		[Trait("Causa Controller", "Index_Get_RetornaDadosComSucesso")]
		public async void Index_Get_RetornaDadosComSucesso()
		{
			// Arrange
			// Act

			// Teste de requisicão ao método de acão
			var retornoRequisicao = await _controller.Index();

			// Assert
			retornoRequisicao.Should().BeOfType<ViewResult>(because: "a requisicão foi enviada extamente como o determinado pelo domínio");

		}

		[Fact]
		[Trait("Causa Controller", "Create_Get_RetornoComSucesso")]
		public void Create_Get_RetornoComSucesso()
		{
			// Arrange
			// Act
			var retornoRequisicao = _controller.Create();

			// Assert

			// Teste de requisicão ao método de acão
			var retornoTipado = (ViewResult)retornoRequisicao;

			retornoTipado.Should().BeOfType<ViewResult>(because: "a requisicão foi enviada extamente como o determinado pelo domínio");

		}

		[Fact]
		[Trait("Causa Controller", "Create_Post_RetornoComSucesso")]
		public void Create_Post_RetornoComSucesso()
		{
			// Arrange
			var causaCadastrada = _domainService.RecuperarCausas().Result.FirstOrDefault();

			// Act

			// Teste de requisicão ao método de acão
			var retornoRequisicao = _controller.Create();

			// Assert
			retornoRequisicao.Should().BeOfType<ViewResult>(because: "a requisicão foi enviada extamente como o determinado pelo domínio");

		}

		#endregion
	}
}

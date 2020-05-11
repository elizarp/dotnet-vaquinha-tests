using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Vaquinha.MVC.Controllers;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Service;
using Vaquinha.Tests.Fixtures;
using Vaquinha.Tests.Extensions;
using System.Net;
using FluentAssertions;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Caching.Memory;
using Vaquinha.Domain.ViewModels;
using NToastNotify;
using Microsoft.AspNetCore.Mvc;

namespace Vaquinha.Tests.ControllerTests
{
	[Collection(nameof(DoacaoFixtureCollection))]
	public class DoacaoControllerTests
	{
		private readonly Mock<IDoacaoRepository> _doacaoRepository = new Mock<IDoacaoRepository>();
		private readonly Mock<IMemoryCache> _memoryCache = new Mock<IMemoryCache>();
		private readonly Mock<GloballAppConfig> _globallAppConfig = new Mock<GloballAppConfig>();

		private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		private DoacoesController _doacaoController;
		private readonly IDoacaoService _doacaoService;

		private Mock<IMapper> _mapper;		
		private Mock<IPaymentService> _polenService = new Mock<IPaymentService>();
		private Mock<ILogger<DoacoesController>> _logger = new Mock<ILogger<DoacoesController>>();

		private IDomainNotificationService _domainNotificationService = new DomainNotificationService();

		private Mock<IToastNotification> _toastNotification = new Mock<IToastNotification>();

		private readonly Doacao _doacaoValida;
		private readonly DoacaoViewModel _doacaoModelValida;

		public DoacaoControllerTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
		{
			_doacaoFixture = doacaoFixture;
			_enderecoFixture = enderecoFixture;
			_cartaoCreditoFixture = cartaoCreditoFixture;

			_mapper = new Mock<IMapper>();
			
			_doacaoValida = doacaoFixture.DoacaoValida();
			_doacaoValida.AdicionarEnderecoCobranca(enderecoFixture.EnderecoValido());
			_doacaoValida.AdicionarFormaPagamento(cartaoCreditoFixture.CartaoCreditoValido());

			_doacaoModelValida = doacaoFixture.DoacaoModelValida();
			_doacaoModelValida.EnderecoCobranca = enderecoFixture.EnderecoModelValido();
			_doacaoModelValida.FormaPagamento = cartaoCreditoFixture.CartaoCreditoModelValido();

			//_polenUserDonationValida = _polenUserDonationFixture.PolenUserDonationValida();
			//_doacaoDetalheTransacaoValida = _doacaoDetalheTransacaoFixture.DoacaoDetalheTransacaoValida();

			_mapper.Setup(a => a.Map<DoacaoViewModel, Doacao>(_doacaoModelValida)).Returns(_doacaoValida);
			//_mapper.Setup(a => a.Map<Doacao, PolenUserDonation>(_doacaoValida)).Returns(_polenUserDonationValida);
			//_polenService.Setup(_ => _.AdicionadDoacaoAsync(_polenUserDonationValida)).ReturnsAsync(_doacaoDetalheTransacaoValida);

			_doacaoService = new DoacaoService(_mapper.Object, _doacaoRepository.Object, _domainNotificationService);
		}

		#region HTTPPOST

		[Trait("DoacaoController", "DoacaoController_Adicionar_RetornaDadosComSucesso")]
		[Fact]
		public void DoacaoController_Adicionar_RetornaDadosComSucesso()
		{
			// Arrange            
			_doacaoController = new DoacoesController(_doacaoService, _domainNotificationService, _toastNotification.Object);

			// Act
			var retorno = _doacaoController.Create(_doacaoModelValida);

			_mapper.Verify(a => a.Map<DoacaoViewModel, Doacao>(_doacaoModelValida), Times.Once);
			_toastNotification.Verify(a => a.AddSuccessToastMessage(It.IsAny<string>(),It.IsAny<LibraryOptions>()), Times.Once);

			retorno.Should().BeOfType<RedirectToActionResult>();

			((RedirectToActionResult)retorno).ActionName.Should().Be("Index");
			((RedirectToActionResult)retorno).ControllerName.Should().Be("Home");			
		}

		[Trait("DoacaoController", "DoacaoController_AdicionarDadosInvalidos_BadRequest")]
		[Fact]
		public void DoacaoController_AdicionarDadosInvalidos_BadRequest()
		{
			// Arrange          
			var doacao = _doacaoFixture.DoacaoInvalida();
			var doacaoModelInvalida = new DoacaoViewModel();
			_mapper.Setup(a => a.Map<DoacaoViewModel, Doacao>(doacaoModelInvalida)).Returns(doacao);

			_doacaoController = new DoacoesController(_doacaoService, _domainNotificationService, _toastNotification.Object);

			// Act
			var retorno = _doacaoController.Create(doacaoModelInvalida);

			// Assert                   
			retorno.Should().BeOfType<ViewResult>();

			_mapper.Verify(a => a.Map<DoacaoViewModel, Doacao>(doacaoModelInvalida), Times.Once);

			//response.Data.Should().BeNull();
			//response.Errors.Should().NotBeNull();
			//response.Errors.Should().BeOfType<List<DomainNotification>>();
		}
		
		/*

		[Trait("DoacaoController", "DoacaoController_AdicionarDoacaoException_InternalServerError")]
		[Fact]
		public void DoacaoController_AdicionarDoacaoException_InternalServerError()
		{
			// Arrange
			_doacaoRepository.Setup(a => a.AdicionarAsync(_doacaoValida)).Throws(new Exception("ERRO AO ACESSAO O BANCO DE DADOS"));

			_doacaoController = new DoacoesController(_doacaoService, _domainNotificationService, _toastNotification.Object);

			// Act
			var retorno = _doacaoController.Create(_doacaoModelValida);

			// Assert
			//retorno.StatusCodeShouldBe(HttpStatusCode.InternalServerError);
			Assert.IsType<ObjectResult>(retorno);
			var objectResponse = retorno as ObjectResult;

			_doacaoRepository.Verify(a => a.AdicionarAsync(_doacaoValida), Times.Once);
			_mapper.Verify(a => a.Map<DoacaoViewModel, Doacao>(_doacaoModelValida), Times.Once);

			//var response = retorno.Content();

			response.Data.Should().BeNull();
			response.Errors.Should().NotBeNull();
			(response.Errors as string[]).Should().HaveCount(1, "porque foi lançada uma exeção na camada de acesso a dados");
			(response.Errors as string[]).Should().Contain("ERRO AO ACESSAO O BANCO DE DADOS", because: "é a mensagem de erro configurado na exceção");
		}
*/
		#endregion
	}
}


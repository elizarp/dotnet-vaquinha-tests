using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vaquinha.MVC;
using Vaquinha.Repository.Context;
using Xunit;

namespace Vaquinha.Integration.Tests
{
	public class DoacoesControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Startup>>
	{
		private readonly HttpClient _client;

		private VaquinhaOnlineDBContext _context ;

		public DoacoesControllerIntegrationTests(TestingWebAppFactory<Startup> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task Home_WhenCalled_ReturnsLinkDoacao()
		{
			var response = await _client.GetAsync("/Home");

			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			Assert.Contains($"<a class=\"btn btn-yellow\" href=\"/Doacoes/Create\">", responseString);
		}

		[Fact]
		public async Task Create_WhenCalled_ReturnsCreateForm()
		{
			var response = await _client.GetAsync("/Doacoes/Create");

			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			Assert.Contains("<h2>Doe agora</h2>", responseString);
		}

		[Fact]
		public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
		{
			var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Doacoes/Create");

			var formModel = new Dictionary<string, string>
		{
			{ "DadosPessoais.Nome", "" },
			{ "DadosPessoais.Email", "" }
		};

			postRequest.Content = new FormUrlEncodedContent(formModel);

			var response = await _client.SendAsync(postRequest);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			Assert.Contains("O campo Nome é obrigatório.", responseString);
		}

		[Fact]
		public async Task Create_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedDoacao()
		{
			var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Doacoes/Create");

			var formModel = new Dictionary<string, string>
		{
			{"Valor","500"},
			{"DadosPessoais.Nome","Joao Das Couves"},
			{"DadosPessoais.Email","joao@dascouves.com.br"},
			{"DadosPessoais.MensagemApoio","Teste"},
			{"EnderecoCobranca.TextoEndereco","Rua Das Couves"},
			{"EnderecoCobranca.Numero","1"},
			{"EnderecoCobranca.Cidade","Couveral"},
			{"EnderecoCobranca.Estado","SP"},
			{"EnderecoCobranca.CEP","13133-333"},
			{"EnderecoCobranca.Complemento",""},
			{"EnderecoCobranca.Telefone","(11) 11111-1333"},
			{"FormaPagamento.NomeTitular","Joao das Couves"},
			{"FormaPagamento.NumeroCartaoCredito","5293 2864 6707 9604"},
			{"FormaPagamento.Validade","01/2021"},
			{"FormaPagamento.CVV","111"},
			{"DadosPessoais.Anonima","false"}
		};

			postRequest.Content = new FormUrlEncodedContent(formModel);

			var response = await _client.SendAsync(postRequest);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			var check = from d in _context.Doacoes.ToList()
						where d.DadosPessoais.Nome == formModel.GetValueOrDefault("DadosPessoais.Nome")
						select d;

			Assert.Contains("Doação realizada com sucesso", responseString);
		}
	}
}
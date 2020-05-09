using AutoMapper;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.Models;
using Vaquinha.Domain.Models.Response;
using Vaquinha.Payment.Models;

namespace Vaquinha.Service.AutoMapper
{
    public class PolenToConvideDezenoveMappingProfile : Profile
    {
        public PolenToConvideDezenoveMappingProfile(GloballAppConfig globallAppConfig)
        {
            CreateMap<Doacao, UserDonation>()
                .ForCtorParam("cause", opt => opt.MapFrom(src => new Cause(globallAppConfig.Polen.CauseId, src.Valor)))
                .ForMember(dest => dest.StoreId, m => m.MapFrom(src => globallAppConfig.Polen.StoreId))
                .ForMember(dest => dest.CampaignId, m => m.MapFrom(src => globallAppConfig.Polen.CampaignId))
                .ForMember(dest => dest.IsTest, m => m.MapFrom(src => globallAppConfig.Polen.IsTest))
                .ForMember(dest => dest.Notes, m => m.MapFrom(src => globallAppConfig.Polen.Notes))
                
                .ForPath(dest => dest.Donor.Email, m => m.MapFrom(src => src.DadosPessoais.Email))
                .ForPath(dest => dest.Donor.Identifier, m => m.MapFrom(src => src.DadosPessoais.Email))
                .ForPath(dest => dest.Donor.Name, m => m.MapFrom(src => src.DadosPessoais.Nome))

                //.ForPath(dest => dest.CreditCardData.CardNumber, m => m.MapFrom(src => src.FormaPagamento.NumeroCartaoCredito.OnlyNumbers()))
                .ForPath(dest => dest.CreditCardData.ExpirationDate, m => m.MapFrom(src => src.FormaPagamento.Validade))
                .ForPath(dest => dest.CreditCardData.FullName, m => m.MapFrom(src => src.FormaPagamento.NomeTitular))
                .ForPath(dest => dest.CreditCardData.SecurityCode, m => m.MapFrom(src => src.FormaPagamento.CVV));

            //.ForPath(dest => dest.Address.City, m => m.MapFrom(src => src.EnderecoCobranca.Cidade))                               
            //.ForPath(dest => dest.Address.State, m => m.MapFrom(src => src.EnderecoCobranca.Estado))
            //.ForPath(dest => dest.Address.Street, m => m.MapFrom(src => src.EnderecoCobranca.TextoEndereco))
            //.ForPath(dest => dest.Address.Complement, m => m.MapFrom(src => src.EnderecoCobranca.Complemento))                
            //.ForPath(dest => dest.Address.ZipCode, m => m.MapFrom(src => src.EnderecoCobranca.CEP));

            CreateMap<DoacaoDetalheTransacaoResponseModel, DoacaoDetalheTransacao>()
               .ConstructUsing(src => new DoacaoDetalheTransacao(src.Success, src.ErrorCode, src.Description, src.IsDuplicated,
                                                                 src.Data.InvoiceId, src.Data.InvoiceUrl, src.Data.Metodo, src.Data.OrderId,src.ElapsedMilliseconds));

            CreateMap<InstituicaoModel, PolenCauseGetResponse>()
               .ForMember(dest => dest.NgoName, m => m.MapFrom(src => src.Nome))
               .ForMember(dest => dest.City, m => m.MapFrom(src => src.Cidade))
               .ForMember(dest => dest.State, m => m.MapFrom(src => src.Estado));

            CreateMap<PolenCauseGetResponse, InstituicaoModel>()
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.NgoName))
                .ForMember(dest => dest.Cidade, m => m.MapFrom(src => src.City))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.State));
        }
    }
}

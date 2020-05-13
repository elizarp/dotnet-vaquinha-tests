using AutoMapper;
using System;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Service.AutoMapper
{
    public class VaquinhaOnLineMappingProfile : Profile
    {
        public VaquinhaOnLineMappingProfile()
        {   
            CreateMap<Pessoa, PessoaViewModel>();
            CreateMap<Doacao, DoacaoViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Causa, CausaViewModel>();
            CreateMap<CartaoCredito, CartaoCreditoViewModel>();

            CreateMap<Doacao, DoadorViewModel>()
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.DadosPessoais.Nome))
                .ForMember(dest => dest.Anonima, m => m.MapFrom(src => src.DadosPessoais.Anonima))
                .ForMember(dest => dest.MensagemApoio, m => m.MapFrom(src => src.DadosPessoais.MensagemApoio))
                .ForMember(dest => dest.Valor, m => m.MapFrom(src => src.Valor))             
                .ForMember(dest => dest.DataHora, m => m.MapFrom(src => src.DataHora));

            CreateMap<PessoaViewModel, Pessoa>()
                .ConstructUsing(src => new Pessoa(Guid.NewGuid(), src.Nome, src.Email, src.Anonima, src.MensagemApoio));

            CreateMap<CartaoCreditoViewModel, CartaoCredito>()
                .ConstructUsing(src => new CartaoCredito(src.NomeTitular, src.NumeroCartaoCredito, src.Validade, src.CVV));

            CreateMap<CausaViewModel, Causa>()
                .ConstructUsing(src => new Causa(Guid.NewGuid(), src.Nome, src.Cidade, src.Estado));

            CreateMap<EnderecoViewModel, Endereco>()
                .ConstructUsing(src => new Endereco(Guid.NewGuid(), src.CEP, src.TextoEndereco, src.Complemento, src.Cidade, src.Estado, src.Telefone, src.Numero));

            CreateMap<DoacaoViewModel, Doacao>()
                .ForCtorParam("valor", opt => opt.MapFrom(src => src.Valor))
                .ForCtorParam("dadosPessoais", opt => opt.MapFrom(src => src.DadosPessoais))
                .ForCtorParam("formaPagamento", opt => opt.MapFrom(src => src.FormaPagamento))
                .ForCtorParam("enderecoCobranca", opt => opt.MapFrom(src => src.EnderecoCobranca));
        }
    }
}
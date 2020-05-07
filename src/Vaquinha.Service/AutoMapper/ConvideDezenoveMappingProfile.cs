using AutoMapper;
using System;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.Models;

namespace Vaquinha.Service.AutoMapper
{
    public class ConvideDezenoveMappingProfile : Profile
    {
        public ConvideDezenoveMappingProfile()
        {
            CreateMap<HomeModel, HomeModel>();

            CreateMap<Pessoa, PessoaModel>();
            CreateMap<Doacao, DoadorModel>()
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.DadosPessoais.Nome))
                .ForMember(dest => dest.Anonima, m => m.MapFrom(src => src.DadosPessoais.Anonima))
                .ForMember(dest => dest.MensagemApoio, m => m.MapFrom(src => src.DadosPessoais.MensagemApoio))
                .ForMember(dest => dest.Valor, m => m.MapFrom(src => src.Valor))             
                .ForMember(dest => dest.DataHora, m => m.MapFrom(src => src.DataHora));

            CreateMap<Doacao, DoacaoModel>();
            CreateMap<Endereco, EnderecoModel>();
            CreateMap<CartaoCredito, CartaoCreditoModel>();

            CreateMap<EnderecoResponse, EnderecoModel>();

            CreateMap<PessoaModel, Pessoa>()
                .ConstructUsing(src => new Pessoa(Guid.NewGuid(), src.Nome, src.Email, src.Anonima, src.MensagemApoio));

            CreateMap<CartaoCreditoModel, CartaoCredito>()
                .ConstructUsing(src => new CartaoCredito(src.NomeTitular, src.NumeroCartaoCredito, src.Validade, src.CVV));

            CreateMap<EnderecoModel, Endereco>()
                .ConstructUsing(src => new Endereco(Guid.NewGuid(), src.CEP, src.TextoEndereco, src.Complemento, src.Cidade, src.Estado, src.Telefone, src.Numero));

            CreateMap<DoacaoModel, Doacao>()                
                .ForCtorParam("valor", opt => opt.MapFrom(src => src.Valor))
                .ForCtorParam("dadosPessoais", opt => opt.MapFrom(src => src.DadosPessoais))
                .ForCtorParam("formaPagamento", opt => opt.MapFrom(src => src.FormaPagamento))
                .ForCtorParam("enderecoCobranca", opt => opt.MapFrom(src => src.EnderecoCobranca));

            CreateMap<EnderecoResponse, EnderecoModel>()
                .ForMember(dest => dest.CEP, m => m.MapFrom(src => src.cep))
                .ForMember(dest => dest.TextoEndereco, m => m.MapFrom(src => src.logradouro))
                .ForMember(dest => dest.Complemento, m => m.MapFrom(src => src.complemento))
                .ForMember(dest => dest.Cidade, m => m.MapFrom(src => src.localidade))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.uf));
        }
    }
}
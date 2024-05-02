using AutoMapper;
using RedeCredenciadaApi.ViewModels.Plano;
using RedeCredenciadaApi.ViewModels.Produto;
using RedeCredenciadaApi.ViewModels.Substituicao;
using RedeCredenciadaApi.ViewModels.Substituicao.NovoRecursoViewModel;
using RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel;
using RedeCredenciadaDomain.Entities;
using RedeCredenciadaDomain.Entities.NovoRecurso;
using RedeCredenciadaDomain.Entities.Plano;
using RedeCredenciadaDomain.Entities.Produto;
using RedeCredenciadaDomain.Entities.Recurso;

namespace RedeCredenciadaApi.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Produto
            CreateMap<ProdutoEntity, ProdutoViewModelResponse>()
                .ReverseMap();

            // Plano 
            CreateMap<PlanoEntity, PlanoViewModelResponse>()
               .ReverseMap();

            // Novo Recurso           
            CreateMap<ResultadoEntity, ResultadoViewModel>()
                .ForMember(dest => dest.Recursos, opt => opt.MapFrom(src => src.Recursos))
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
            CreateMap<CidadeNovoRecursoEntity, CidadeNovoRecurso>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.CidadeNovoRecurso))
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
            CreateMap<EstadoNovoRecursoEntity, EstadoNovoRecurso>()
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.EstadoNovoRecurso))
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
            CreateMap<EnderecoNovoRecursoEntity, EnderecoNovoRecurso>()
                .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.BairroNovoRecurso))
                .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.CepNovoRecurso))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.ComplementoNovoRecurso))
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.LogradouroNovoRecurso))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.NumeroNovoRecurso))
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
            CreateMap<EspecialidadesNovoRecursoEntity, EspecialidadesNovoRecurso>()
                .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<PlanoNovoRecursoEntity, PlanoNovoRecurso>()
                .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<PlanosNovoRecursoEntity, PlanosNovoRecurso>()
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
            CreateMap<QualificacoesNovoRecursoEntity, QualificacoesNovoRecurso>()
                .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<QualificacaoNovoRecursoEntity, QualificacaoNovoRecurso>()
                .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<TelefoneNovoRecursoEntity, TelefoneNovoRecurso>()
               .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.NumeroTelefoneNovoRecurso))
               .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoTelefoneNovoRecurso))
               .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<NovoRecursoEntity, NovoRecurso>()
              .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.CnpjNovoRecurso))
              .ForMember(dest => dest.Crm, opt => opt.MapFrom(src => src.CrmNovoRecurso))
              .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.EnderecoNovoRecurso))
              .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src => src.EspecialidadeNovoRecurso))
              .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeNovoRecurso))
              .ForMember(dest => dest.Planos, opt => opt.MapFrom(src => src.PlanoNovoRecurso))
              .ForMember(dest => dest.Qualificacoes, opt => opt.MapFrom(src => src.QualificacaoNovoRecurso))
              .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(src => src.RazaoSocialNovoRecurso))
              .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.TelefoneNovoRecurso))
              .ForMember(dest => dest.TipoAtendimento, opt => opt.MapFrom(src => src.TipoAtendimentoNovoRecurso))
              .ForMember(dest => dest.Unimed, opt => opt.MapFrom(src => src.UnimedNovoRecurso))
              .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.CodigoNovoRecurso))
              .ReverseMap()
              .ForAllMembers(p => p.Ignore());

            // Recurso            
            CreateMap<CidadeRecursoEntity, CidadeRecurso>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.CidadeRecurso))
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
            CreateMap<EstadoRecursoEntity, EstadoRecurso>()
                .ForMember(dest => dest.Sigla, opt => opt.MapFrom(src => src.EstadoRecurso))
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
            CreateMap<EnderecoRecursoEntity, EnderecoRecurso>()
                .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.BairroRecurso))
                .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.CepRecurso))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.ComplementoRecurso))
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.LogradouroRecurso))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.NumeroRecurso))
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
            CreateMap<EspecialidadesRecursoEntity, EspecialidadesRecurso>()
                .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<PlanoRecursoEntity, PlanoRecurso>()
                .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<PlanosRecursoEntity, PlanosRecurso>()
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
            CreateMap<QualificacoesRecursoEntity, QualificacoesRecurso>()
                .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<QualificacaoRecursoEntity, QualificacaoRecurso>()
                .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<TelefoneRecursoEntity, TelefoneRecurso>()
               .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.NumeroTelefoneRecurso))
               .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoTelefoneRecurso))
               .ReverseMap()
               .ForAllMembers(p => p.Ignore());
            CreateMap<RecursoEntity, Recurso>()
              .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.CnpjRecurso))
              .ForMember(dest => dest.Crm, opt => opt.MapFrom(src => src.CrmRecurso))
              .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.EnderecoRecurso))
              .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src => src.EspecialidadeRecurso))
              .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeRecurso))
              .ForMember(dest => dest.Planos, opt => opt.MapFrom(src => src.PlanoRecurso))
              .ForMember(dest => dest.Qualificacoes, opt => opt.MapFrom(src => src.QualificacaoRecurso))
              .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(src => src.RazaoSocialRecurso))
              .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.TelefoneRecurso))
              .ForMember(dest => dest.TipoAtendimento, opt => opt.MapFrom(src => src.TipoAtendimentoRecurso))
              .ForMember(dest => dest.Unimed, opt => opt.MapFrom(src => src.UnimedRecurso))
              .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.CodigoRecurso))
              .ForMember(dest => dest.DataExclusaoSolicitada, opt => opt.MapFrom(src => src.DataExclusaoSolicitada))
              .ReverseMap()
              .ForAllMembers(p => p.Ignore());

            // Recursos - Novo Recurso e Recurso
            CreateMap<RecursosEntity, Recursos>()
                .ReverseMap()
                .ForAllMembers(p => p.Ignore());
        }
    }
}

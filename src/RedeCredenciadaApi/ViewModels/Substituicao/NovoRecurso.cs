using RedeCredenciadaApi.ViewModels.Substituicao.NovoRecursoViewModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao
{
    public class NovoRecurso
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("unimed")]
        public string Unimed { get; set; }

        [JsonPropertyName("endereco")]
        public EnderecoNovoRecurso Endereco { get; set; }

        [JsonPropertyName("crm")]
        public string Crm { get; set; }

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

        [JsonPropertyName("razaoSocial")]
        public string RazaoSocial { get; set; }

        [JsonPropertyName("tipoAtendimento")]
        public string TipoAtendimento { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("telefone")]
        public TelefoneNovoRecurso Telefone { get; set; }

        [JsonPropertyName("qualificacoes")]
        public QualificacoesNovoRecurso Qualificacoes { get; set; }

        [JsonPropertyName("especialidades")]
        public IEnumerable<EspecialidadesNovoRecurso> Especialidades { get; set; }

        [JsonPropertyName("planos")]
        public PlanosNovoRecurso Planos { get; set; }
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel
{
    public class RecursoRecurso
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("unimed")]
        public string Unimed { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("crm")]
        public string Crm { get; set; }

        [JsonPropertyName("endereco")]
        public EnderecoRecurso Endereco { get; set; }

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

        [JsonPropertyName("razaoSocial")]
        public string RazaoSocial { get; set; }

        [JsonPropertyName("tipoAtendimento")]
        public string TipoAtendimento { get; set; }

        [JsonPropertyName("telefone")]
        public TelefoneRecurso Telefone { get; set; }

        [JsonPropertyName("qualificacoes")]
        public QualificacoesRecurso Qualificacoes { get; set; }

        [JsonPropertyName("especialidades")]
        public IEnumerable<EspecialidadesRecurso> Especialidades { get; set; }

        [JsonPropertyName("planos")]
        public PlanosRecurso Planos { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.NovoRecursoViewModel
{
    public class EnderecoNovoRecurso
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("cidade")]
        public CidadeNovoRecurso Cidade { get; set; }

        [JsonPropertyName("estado")]
        public EstadoNovoRecurso Estado { get; set; }
    }
}

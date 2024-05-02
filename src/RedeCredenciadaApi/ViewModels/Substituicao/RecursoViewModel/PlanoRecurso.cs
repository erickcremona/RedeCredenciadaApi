using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel
{
    public class PlanoRecurso
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("numeroRegistroANS")]
        public string NumeroRegistroANS { get; set; }

        [JsonPropertyName("classificacao")]
        public string Classificacao { get; set; }

        [JsonPropertyName("situacao")]
        public string Situacao { get; set; }
    }
}

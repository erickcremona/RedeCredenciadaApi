using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel
{
    public class QualificacaoRecurso
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("habilitado")]
        public string Habilitado { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao
{
    public class Recursos
    {
        [JsonPropertyName("dataExclusao")]
        public string DataExclusao { get; set; }

        [JsonPropertyName("dataInclusaoNovo")]
        public string DataInclusaoNovo { get; set; }

        [JsonPropertyName("novoRecurso")]
        public NovoRecurso NovoRecurso { get; set; }

        [JsonPropertyName("recurso")]
        public Recurso Recurso { get; set; }
    }
}

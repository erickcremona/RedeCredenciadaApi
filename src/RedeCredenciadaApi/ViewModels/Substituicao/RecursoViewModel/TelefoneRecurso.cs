using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel
{
    public class TelefoneRecurso
    {
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }
    }
}
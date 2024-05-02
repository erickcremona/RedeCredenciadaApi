using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel
{
    public class EstadoRecurso
    {
        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }
    }
}

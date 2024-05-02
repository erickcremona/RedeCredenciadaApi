using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.NovoRecursoViewModel
{
    public class EstadoNovoRecurso
    {
        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }
    }
}

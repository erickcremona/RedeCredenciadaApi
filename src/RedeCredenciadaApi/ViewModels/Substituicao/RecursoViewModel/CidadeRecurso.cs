using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel
{
    public class CidadeRecurso
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.NovoRecursoViewModel
{
    public class CidadeNovoRecurso
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }
}

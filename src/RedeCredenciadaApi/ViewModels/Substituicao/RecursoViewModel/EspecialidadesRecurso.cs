using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel
{
    public class EspecialidadesRecurso
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.NovoRecursoViewModel
{
    public class EspecialidadesNovoRecurso
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }
}
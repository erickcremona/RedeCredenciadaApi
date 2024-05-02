using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.NovoRecursoViewModel
{
    public class TelefoneNovoRecurso
    {
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }
    }
}

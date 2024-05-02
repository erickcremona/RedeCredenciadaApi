using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.NovoRecursoViewModel
{
    public class PlanosNovoRecurso
    {
        [JsonPropertyName("plano")]
        public IEnumerable<PlanoNovoRecurso> Plano { get; set; }
    }
}

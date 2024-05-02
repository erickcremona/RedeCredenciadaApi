using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel
{
    public class PlanosRecurso
    {
        [JsonPropertyName("plano")]
        public IEnumerable<PlanoRecurso> Plano { get; set; }
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.RecursoViewModel
{
    public class QualificacoesRecurso
    {
        [JsonPropertyName("qualificacao")]
        public IEnumerable<QualificacaoRecurso> Qualificacao { get; set; }
    }
}

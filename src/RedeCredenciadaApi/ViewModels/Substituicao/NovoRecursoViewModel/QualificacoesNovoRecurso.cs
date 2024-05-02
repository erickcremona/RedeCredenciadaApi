using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedeCredenciadaApi.ViewModels.Substituicao.NovoRecursoViewModel
{
    public class QualificacoesNovoRecurso
    {
        [JsonPropertyName("qualificacao")]
        public IEnumerable<QualificacaoNovoRecurso> Qualificacao { get; set; }
    }
}

using System.Collections.Generic;

namespace RedeCredenciadaDomain.Entities.NovoRecurso
{
    public class QualificacoesNovoRecursoEntity : Entity
    {
        public IEnumerable<QualificacaoNovoRecursoEntity> Qualificacao { get; set; }
    }
}

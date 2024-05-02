using System.Collections.Generic;

namespace RedeCredenciadaDomain.Entities.Recurso
{
    public class QualificacoesRecursoEntity : Entity
    {
        public IEnumerable<QualificacaoRecursoEntity> Qualificacao { get; set; }
    }
}

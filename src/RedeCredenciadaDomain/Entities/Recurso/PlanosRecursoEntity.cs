using System.Collections.Generic;

namespace RedeCredenciadaDomain.Entities.Recurso
{
    public class PlanosRecursoEntity : Entity
    {
        public IEnumerable<PlanoRecursoEntity> Plano { get; set; }
    }
}

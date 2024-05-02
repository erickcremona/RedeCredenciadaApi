using System.Collections.Generic;

namespace RedeCredenciadaDomain.Entities.NovoRecurso
{
    public class PlanosNovoRecursoEntity : Entity
    {
        public IEnumerable<PlanoNovoRecursoEntity> Plano { get; set; }
    }
}

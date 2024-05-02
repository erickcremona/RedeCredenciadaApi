using RedeCredenciadaDomain.Entities.NovoRecurso;
using RedeCredenciadaDomain.Entities.Recurso;

namespace RedeCredenciadaDomain.Entities
{
    public class RecursosEntity : Entity
    {
        public string DataExclusao { get; set; }
        public string DataInclusaoNovo { get; set; }
        public NovoRecursoEntity NovoRecurso { get; set; }
        public RecursoEntity Recurso { get; set; }
    }
}

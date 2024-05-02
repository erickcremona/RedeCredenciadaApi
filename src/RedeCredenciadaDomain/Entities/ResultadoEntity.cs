using System.Collections.Generic;

namespace RedeCredenciadaDomain.Entities
{
    public class ResultadoEntity : Entity
    {
        public IList<RecursosEntity> Recursos { get; set; }
    }
}

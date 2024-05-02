namespace RedeCredenciadaDomain.Entities.Recurso
{
    public class PlanoRecursoEntity : Entity
    {
        public string Nome { get; set; }
        public string NumeroRegistroANS { get; set; }
        public string Classificacao { get; set; }
        public string Situacao { get; set; }
    }
}
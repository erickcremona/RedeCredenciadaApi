namespace RedeCredenciadaDomain.Entities.Recurso
{
    public class EnderecoRecursoEntity : Entity
    {
        public string CepRecurso { get; set; }
        public string LogradouroRecurso { get; set; }

        public string NumeroRecurso { get; set; }

        public string ComplementoRecurso { get; set; }

        public string BairroRecurso { get; set; }

        public CidadeRecursoEntity Cidade { get; set; }

        public EstadoRecursoEntity Estado { get; set; }
    }
}

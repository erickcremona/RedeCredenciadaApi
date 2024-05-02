namespace RedeCredenciadaDomain.Entities.NovoRecurso
{
    public class EnderecoNovoRecursoEntity
    {
        public string CepNovoRecurso { get; set; }
        public string LogradouroNovoRecurso { get; set; }
        public string NumeroNovoRecurso { get; set; }
        public string ComplementoNovoRecurso { get; set; }
        public string BairroNovoRecurso { get; set; }
        public CidadeNovoRecursoEntity Cidade { get; set; }
        public EstadoNovoRecursoEntity Estado { get; set; }
    }
}

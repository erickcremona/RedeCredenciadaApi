using System.Collections.Generic;

namespace RedeCredenciadaDomain.Entities.NovoRecurso
{
    public class NovoRecursoEntity : Entity
    {
        public string CodigoNovoRecurso { get; set; }
        public string UnimedNovoRecurso { get; set; }
        public string CrmNovoRecurso { get; set; }
        public string CnpjNovoRecurso { get; set; }
        public string RazaoSocialNovoRecurso { get; set; }
        public string TipoAtendimentoNovoRecurso { get; set; }
        public string NomeNovoRecurso { get; set; }
        public string PlanosNovoRecurso { get; set; }
        public string QualificacoesNovoRecurso { get; set; }
        public string EspecialidadesNovoRecurso { get; set; }
        public EnderecoNovoRecursoEntity EnderecoNovoRecurso { get; set; }
        public TelefoneNovoRecursoEntity TelefoneNovoRecurso { get; set; }
        public QualificacoesNovoRecursoEntity QualificacaoNovoRecurso { get; set; }
        public IEnumerable<EspecialidadesNovoRecursoEntity> EspecialidadeNovoRecurso { get; set; }
        public PlanosNovoRecursoEntity PlanoNovoRecurso { get; set; }
    }
}

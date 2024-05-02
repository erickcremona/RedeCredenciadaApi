using System.Collections.Generic;

namespace RedeCredenciadaDomain.Entities.Recurso
{
    public class RecursoEntity : Entity
    {
        public string DataExclusao { get; set; }
        public string CodigoRecurso { get; set; }
        public string UnimedRecurso { get; set; }
        public string NomeRecurso { get; set; }
        public string CrmRecurso { get; set; }
        public string CnpjRecurso { get; set; }
        public string RazaoSocialRecurso { get; set; }
        public string TipoAtendimentoRecurso { get; set; }
        public string PlanosRecurso { get; set; }
        public string QualificacoesRecurso { get; set; }
        public string EspecialidadesRecurso { get; set; }
        public string DataExclusaoSolicitada { get; set; }
        public EnderecoRecursoEntity EnderecoRecurso { get; set; }
        public TelefoneRecursoEntity TelefoneRecurso { get; set; }
        public QualificacoesRecursoEntity QualificacaoRecurso { get; set; }
        public IEnumerable<EspecialidadesRecursoEntity> EspecialidadeRecurso { get; set; }
        public PlanosRecursoEntity PlanoRecurso { get; set; }
    }
}

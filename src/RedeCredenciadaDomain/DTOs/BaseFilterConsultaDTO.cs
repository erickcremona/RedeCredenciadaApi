using System;

namespace RedeCredenciadaDomain.DTOs
{
    public class BaseFilterConsultaDTO : ICloneable
    {
        public string CodigoProduto { get; set; }

        public string CodigoPlano { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Filtro { get; set; }

        public string RegistroANS { get; set; }


        /// <summary>
        /// Clona o filtro
        /// </summary>
        /// <returns></returns>
        protected BaseFilterConsultaDTO Clone()
        {
            return this.MemberwiseClone() as BaseFilterConsultaDTO;
        }

        /// <summary>
        /// Habilita clone
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Executa clone do filtro
        /// </summary>
        /// <returns></returns>
        public BaseFilterConsultaDTO GetClone()
        {
            return Clone();
        }
    }
}


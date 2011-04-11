using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela ResistenciaTracao
    /// </summary>
// ReSharper disable InconsistentNaming
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class ResistenciaTracaoVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

        public ResistenciaTracaoVO()
        {
            UsuarioAlt = 1;
            DataAlteracao = DateTime.Now;
            UsuarioInc = 1;
            DataCadastro = DateTime.Now;
            Maximo = null;
            Minimo = null;
            Tolerancia = null;
            Bitola = new BitolaVO();
            CodMateriaPrima = 0;
            CodResistenciaTracao = 0;
        }

        #endregion
        #region Propriedades

        public int CodResistenciaTracao { get; set; }

        public int CodMateriaPrima { get; set; }

        public BitolaVO Bitola { get; set; }

        public decimal? Tolerancia { get; set; }

        public decimal? Minimo { get; set; }

        public decimal? Maximo { get; set; }

        public DateTime DataCadastro { get; set; }

        public int UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }

        #endregion

        #region ToString()
        public override string ToString()
        {
            return "ResistenciaTracaoVO: " + CodResistenciaTracao.ToString();
        }
        #endregion
    }
}


// ------------------------------------------------------------------------- // 


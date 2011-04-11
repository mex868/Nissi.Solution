using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela Bitola
    /// </summary>
// ReSharper disable InconsistentNaming
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class BitolaVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

        public BitolaVO()
        {
            UsuarioAlt = null;
            DataAlteracao = null;
            UsuarioInc = null;
            DataCadastro = null;
            Bitola = 0;
            CodBitola = 0;
        }

        #endregion
        #region Propriedades

        public int CodBitola { get; set; }

        public decimal Bitola { get; set; }

        public DateTime? DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }

        #endregion

        #region ToString()
        public override string ToString()
        {
            return "BitolaVO: " + CodBitola.ToString();
        }
        #endregion
    }
}


// ------------------------------------------------------------------------- // 



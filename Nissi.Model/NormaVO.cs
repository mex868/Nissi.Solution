using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela Norma
    /// </summary>
// ReSharper disable InconsistentNaming
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class NormaVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

        public NormaVO()
        {
            Descricao = String.Empty;
            UsuarioAlt = null;
            DataAlteracao = null;
            UsuarioInc = null;
            DataCadastro = null;
            Revisao = null;
            CodNorma = 0;
        }

        #endregion
        #region Propriedades

        public int CodNorma { get; set; }

        public string Descricao { get; set; }

        public int? Revisao { get; set; }

        public DateTime? DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }

        #endregion

        #region ToString()
        public override string ToString()
        {
            return "NormaVO: " + CodNorma.ToString();
        }
        #endregion
    }
}


// ------------------------------------------------------------------------- // 



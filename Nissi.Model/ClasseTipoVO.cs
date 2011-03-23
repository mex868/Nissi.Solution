using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela ClasseTipo
    /// </summary>
// ReSharper disable InconsistentNaming
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class ClasseTipoVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

        public ClasseTipoVO()
        {
            UsuarioAlt = null;
            DataAlteracao = null;
            UsuarioInc = null;
            DataCadastro = null;
            Descricao = String.Empty;
            CodClasseTipo = null;
        }

        #endregion
        #region Propriedades

        public int? CodClasseTipo { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }

        #endregion

        #region ToString()
        public override string ToString()
        {
            return "ClasseTipoVO: " + CodClasseTipo.ToString();
        }
        #endregion
    }
}


// ------------------------------------------------------------------------- // 


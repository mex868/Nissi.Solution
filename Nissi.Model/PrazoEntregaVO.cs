using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela PrazoEntrega
    /// </summary>
// ReSharper disable InconsistentNaming
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class PrazoEntregaVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

        public PrazoEntregaVO()
        {
            DataCadastro = null;
            UsuarioAlt = null;
            DataAlteracao = null;
            UsuarioInc = null;
            Descricao = String.Empty;
            CodPrazoEntrega = 0;
        }

        #endregion
        #region Propriedades

        public int CodPrazoEntrega { get; set; }

        public string Descricao { get; set; }

        public int Dias { get; set; }

        public DateTime? DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }

        #endregion

        #region ToString()
        public override string ToString()
        {
            return "PrazoEntregaVO: " + CodPrazoEntrega.ToString();
        }
        #endregion
    }
}


// ------------------------------------------------------------------------- // 


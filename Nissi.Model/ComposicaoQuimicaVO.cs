using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    class ComposicaoQuimica
    {
    }
}
namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela ComposicaoQuimica
    /// </summary>
// ReSharper disable InconsistentNaming
    public class ComposicaoQuimicaVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

        public ComposicaoQuimicaVO()
        {
            DataAlteracao = null;
            UsuarioAlt = null;
            UsuarioInc = null;
            DataCadastro = null;
            Descricao = String.Empty;
            CodComposicaoQuimica = null;
        }

        #endregion
        #region Propriedades

        public int? CodComposicaoQuimica { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }

        #endregion

        #region ToString()
        public override string ToString()
        {
            return "ComposicaoQuimicaVO: " + CodComposicaoQuimica.ToString();
        }
        #endregion
    }
}


// ------------------------------------------------------------------------- // 



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela ComparacaoQuimica
    /// </summary>
    /// 
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class ComparacaoQuimicaVO
    {
        #region Campos

        public ComparacaoQuimicaVO()
        {
            UsuarioAlt = null;
            DataAlteracao = null;
            UsuarioInc = null;
            DataCadastro = null;
            Minima = null;
            Maxima = null;
            Descricao = String.Empty;
            CodMateriaPrima = 0;
            CodComparacaoQuimica = null;
        }

        #endregion
        #region Propriedades

        public int? CodComparacaoQuimica { get; set; }

        public int CodMateriaPrima { get; set; }

        public string Descricao { get; set; }

        public decimal? Maxima { get; set; }

        public decimal? Minima { get; set; }

        public DateTime? DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }

        #endregion

        public override string ToString()
        {
            return "ComparacaoQuimicaVO: " + CodComparacaoQuimica.ToString();
        }
    }
}


// ------------------------------------------------------------------------- // 



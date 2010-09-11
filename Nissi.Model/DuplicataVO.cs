using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela Duplicata
    /// </summary>
    [Serializable] //deve ser serializavel para armazenar em viewstate
    public class DuplicataVO 
    {
        #region Campos
        private int? _codDuplicata = null;
        private int? _dias = null;
        private DateTime? _vencimento = null;
        private string _numero = String.Empty;
        private decimal? _valor = null;
        #endregion
        #region Propriedades
        public int? CodDuplicata
        {
            get { return _codDuplicata; }
            set { _codDuplicata = value; }
        }

        public int? Dias
        {
            get { return _dias; }
            set { _dias = value; }
        }

        public DateTime? Vencimento
        {
            get { return _vencimento; }
            set { _vencimento = value; }
        }

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public decimal? Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        #endregion

        /// <summary>
        /// Retorna um texto que identifique essa instância da classe
        /// </summary>
        #region ToString()
        public override string ToString()
        {
            return "DuplicataVO: " + CodDuplicata.ToString();
        }
        #endregion
    }
}

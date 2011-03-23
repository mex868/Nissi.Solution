using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class ProdutoNFVO
    {
        private int? _codNF;
        private int? _NF;
		private DateTime? _dataEmissao;
		private decimal? _qtd;
		private decimal? _valor;
		private decimal? _totalItem;
		private string _descricao = String.Empty;
		private string _codigo = String.Empty;
        private int? _codPedido;
        private string _indStatus;
        private string _codPedidoCliente = String.Empty;
        private string _oP = String.Empty;

        public int? CodNF
        {
            get { return _codNF; }
            set { _codNF = value; }
        }
        
        public int? NF
		{
			get {return _NF;}
			set {_NF = value;}
		}
		
		public DateTime? DataEmissao
		{
			get {return _dataEmissao;}
			set {_dataEmissao = value;}
		}
		
		public decimal? Qtd
		{
			get {return _qtd;}
			set {_qtd = value;}
		}
		
		public decimal? Valor
		{
			get {return _valor;}
			set {_valor = value;}
		}
		
		public decimal? TotalItem
		{
			get {return _totalItem;}
			set {_totalItem = value;}
		}
		
		public string Descricao
		{
			get {return _descricao;}
			set {_descricao = value;}
		}
		
		public string Codigo
		{
			get {return _codigo;}
			set {_codigo = value;}
		}
        public int? CodPedido
        {
            get { return _codPedido; }
            set { _codPedido = value; }
        }

        public string IndStatus
        {
            get { return _indStatus; }
            set { _indStatus = value; }
        }

        public string CodPedidoCliente
        {
            get { return _codPedidoCliente; }
            set { _codPedidoCliente = value; }
        }
        public string OP
        {
            get { return _oP; }
            set { _oP = value; }
        }
		/// <summary>
		/// Retorna um texto que identifique essa instância da classe
		/// </summary>
		public override string ToString()
		{
			return "relatorioVO: " + NF.ToString();
		}	
		
	}	
}

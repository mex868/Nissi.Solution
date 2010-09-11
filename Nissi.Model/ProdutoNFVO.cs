using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class ProdutoNFVO
    {
        private int? _NF;
		private DateTime? _dataEmissao;
		private decimal? _qtd;
		private decimal? _valor;
		private decimal? _totalItem;
		private string _descricao = String.Empty;
		private string _codigo = String.Empty;
		
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
		
				
		/// <summary>
		/// Retorna um texto que identifique essa instância da classe
		/// </summary>
		public override string ToString()
		{
			return "relatorioVO: " + NF.ToString();
		}	
		
	}	
}

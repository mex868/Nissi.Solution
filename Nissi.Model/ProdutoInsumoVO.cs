using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class ProdutoInsumoVO
    {
        public ProdutoInsumoVO()
        {
            Unidade = new UnidadeVO();
            Valor = 0;
            Descricao = string.Empty;
        }

        public int CodProdutoInsumo { get; set; }
        public string Descricao { get; set; }
        public UnidadeVO Unidade { get; set; }
        public decimal? Valor { get; set; }
    }
}

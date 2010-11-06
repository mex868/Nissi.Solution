using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class ProdutoNF
    {
        public List<ProdutoNFVO> Lista(string codigo, string Op, DateTime? dataEmissaoIni, DateTime? dataEmissaoFim)
        {
            return new ProdutoNFData().Lista(codigo, Op, dataEmissaoIni, dataEmissaoFim);
        }
    }
}

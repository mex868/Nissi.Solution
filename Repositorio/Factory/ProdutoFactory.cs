using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio.Factory
{
    public class ProdutoFactory
    {
        public static IProduto Create(TypePedido tipo)
        {
            IProduto produto = null;
            switch (tipo)
            {
                case TypePedido.Compra:
                    produto = new ProdutoMateriaPrima();
                    break;
                case TypePedido.CompraInsumo:
                    produto = new ProdutoProdutoInsumo();
                    break;
            }
            return produto;
        }
    }
}

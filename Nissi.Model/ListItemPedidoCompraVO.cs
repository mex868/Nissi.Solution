﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class ListItemPedidoCompraVO
    {
        public int OrdemCompra { get; set; }
        public int CodPessoa { get; set; }
        public int Tipo { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Fornecedor { get; set; }
        public decimal Bitola { get; set; }
        public MateriaPrimaVO MateriaPrimaVo { get; set; }
        public decimal Preco { get; set; }
        public decimal? Ipi { get; set; }
        public string Unidade { get; set; }
        public decimal Qtde { get; set; }
        public DateTime? DataPrevista { get; set; }
        public DateTime? DataEntrega { get; set; }
        public decimal? QtdeEntregue { get; set; }
        public decimal Saldo { get
        {
            decimal saldo = 0;
            if (QtdeEntregue != null) saldo = (Qtde - QtdeEntregue.Value)*-1;
            return saldo;
        }
        }
        public string Situacao { get { return DataEntrega == null && DataPrevista < DateTime.Now.Date ? "Atrasado" : QtdeEntregue == null ? "Aberto" : QtdeEntregue < Qtde ? "Parcial" : DataPrevista < DataEntrega ? "Entregue em Atraso" : DataPrevista == DataEntrega ? "Entregue" : "Aberto"; } }
        public int DiaEmAtraso { get
            {
                int dia = 0;
                if (DataPrevista != null)
                    dia = DataEntrega == null && DataPrevista < DateTime.Now ? DataPrevista.Value.Subtract(DateTime.Now).Days : DataEntrega != null? DataPrevista.Value.Subtract(DataEntrega.Value).Days:0;
                return dia;
            }
        }
        public string Descricao { get { return MateriaPrimaVo.Descricao; } }
    }
}

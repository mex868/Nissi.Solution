using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class NotaFiscal : NissiBaseBusiness
    {
        /// <summary>
        /// Método para listar os registros da tabela  NotaFiscal 
        /// </summary>
        #region Métodos de Listagem
        public List<NotaFiscalVO> Listar(NotaFiscalVO identNotaFiscal)
        {
          return  new NotaFiscalData().Listar(identNotaFiscal);
        }
        #endregion
        // ------------------------------------------------------------------------- //
        public List<NotaFiscalVO> ListarTudo(NotaFiscalVO identNotaFiscal)
        {
            return new NotaFiscalData().ListarTudo(identNotaFiscal);
        }
        public NotaFiscalVO gerar_numero_nf()
        {
            return new NotaFiscalData().gerar_numero_nf();
        }
        public int ListarNumeroNf(int codNf)
        {
            return new NotaFiscalData().ListarNumeroNf(codNf);
        }

        /// <summary>
        /// Método para incluir um registro na tabela  NotaFiscal 
        /// </summary>
        #region Métodos de Inclusão
        public int Incluir(NotaFiscalVO notaFiscalVO, int codUsuarioOperacao)
        {
           return new NotaFiscalData().Incluir(notaFiscalVO, codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para alterar um registro na tabela  NotaFiscal 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(NotaFiscalVO notaFiscalVO, int codUsuarioOperacao)
        {
            new NotaFiscalData().Alterar(notaFiscalVO, codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para excluir um registro na tabela  NotaFiscal 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(int codNF, int codUsuarioOperacao)
        {
            new NotaFiscalData().Excluir(codNF, codUsuarioOperacao);
        }
        #endregion
 
        public static NotaFiscalVO CalcTotais(NotaFiscalVO identNotaFiscal)
        {
            foreach (ItemNotaFiscalVO identItemNotaFiscal in identNotaFiscal.Itens)
            {
                identNotaFiscal.ValTotalIpi += identItemNotaFiscal.CalcIPI;
                identNotaFiscal.ValTotalImcs += identItemNotaFiscal.CalcIcms;
                identNotaFiscal.ValTotalProduto += identItemNotaFiscal.TotalItem;
                identNotaFiscal.QtdTotal += identItemNotaFiscal.Qtd;
                identNotaFiscal.ValTotalDesc += identItemNotaFiscal.Desconto;
                identNotaFiscal.ICMS = identItemNotaFiscal.ICMS;
            }
            identNotaFiscal.BaseCalcIcms = identNotaFiscal.ValTotalProduto + identNotaFiscal.ValorFrete +
                    identNotaFiscal.ValorSeguro + identNotaFiscal.OutDespAce + identNotaFiscal.ValTotalIpi;
            identNotaFiscal.ValTotalNota = identNotaFiscal.ValTotalProduto + identNotaFiscal.ValorFrete +
                    identNotaFiscal.ValorSeguro+identNotaFiscal.OutDespAce+identNotaFiscal.ValTotalIpi;
            return identNotaFiscal;
        }
    }
}

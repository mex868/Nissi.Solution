﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class Fornecedor : NissiBaseBusiness
    {
        #region Métodos de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_fornecedor
        /// Objeto/Parâmetros: (identFornecedor)
        /// Valores: identFornecedor.CodPessoa,
        /// identFornecedor.CNPJ,
        /// identFornecedor.RazaoSocial,
        /// identFornecedor.NomeFantasia
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<FornecedorVO> Listar(FornecedorVO identFornecedor)
        {
            return new FornecedorData().Listar(identFornecedor);
        }

        // ------------------------------------------------------------------------- // 
        #endregion
        #region Métodos de Inclusão
        /// <summary>
        /// Método para executar a proc pr_incluir_fornecedor 
        /// Objeto/Parâmetros: (identFornecedor, codUsuarioOperacao)
        /// Valores: identFornecedor.Cep.CodCep,
        ///  identFornecedor.NomeFantasia,
        ///  identFornecedor.Tipo,
        ///  identFornecedor.CNPJ,
        ///  identFornecedor.InscricaoEstadual,
        ///  identFornecedor.Numero,
        ///  identFornecedor.Complemento,
        ///  identFornecedor.Telefone,
        ///  dentFornecedor.Fax,
        ///  identFornecedor.Celular,
        ///  identFornecedor.Contato,
        ///  identFornecedor.Email,
        ///  identFornecedor.Site,
        ///  identFornecedor.Observacao,
        ///  identFornecedor.PrazoEntrega,
        ///  identFornecedor.FormaPgto.CodFormaPgto,
        ///  identFornecedor.Departamento.CodDepartamento,
        ///  identFornecedor.TipoFornecimento.CodTipoFornecimento,
        ///  codUsuarioOperacao,
        ///  identFornecedor.Ativo,
        ///  identFornecedor.IndPessoaTipo
        /// </summary>
        public void Incluir(FornecedorVO identFornecedor, int codUsuarioOperacao)
        {
            new FornecedorData().Incluir(identFornecedor, codUsuarioOperacao);
        }


        // ------------------------------------------------------------------------- // 
        #endregion
        #region Métodos de Alteração
        /// <summary>
        /// Método para executar a proc pr_alterar_fornecedor 
        /// Objeto/Parâmetros: (identFornecedor, codUsuarioOperacao)
        /// Valores: identFornecedor.CodPessoa, 
        ///  identFornecedor.CodFornecedor,
        ///  identFornecedor.Cep.CodCep,
        ///  identFornecedor.NomeFantasia,
        ///  identFornecedor.Tipo,
        ///  identFornecedor.CNPJ,
        ///  identFornecedor.InscricaoEstadual,
        ///  identFornecedor.Numero,
        ///  identFornecedor.Complemento,
        ///  identFornecedor.Telefone,
        ///  dentFornecedor.Fax,
        ///  identFornecedor.Celular,
        ///  identFornecedor.Contato,
        ///  identFornecedor.Email,
        ///  identFornecedor.Site,
        ///  identFornecedor.Observacao,
        ///  identFornecedor.PrazoEntrega,
        ///  identFornecedor.FormaPgto.CodFormaPgto,
        ///  identFornecedor.Departamento.CodDepartamento,
        ///  identFornecedor.TipoFornecimento.CodTipoFornecimento,
        ///  codUsuarioOperacao,
        ///  identFornecedor.Ativo,
        ///  identFornecedor.IndPessoaTipo
        /// </summary>
        public void Alterar(FornecedorVO identFornecedor, int codUsuarioOperacao)
        {
            new FornecedorData().Alterar(identFornecedor, codUsuarioOperacao);
        }
        #endregion
        #region Métodos de Exclusão
        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_excluir_fornecedor
        /// Objeto/Parâmetros: (codPessoa)
        /// </summary>
        public void Excluir(int? codPessoa)
        {
            new FornecedorData().Excluir(codPessoa);
        }

        // ------------------------------------------------------------------------- // 
        #endregion
    }
}

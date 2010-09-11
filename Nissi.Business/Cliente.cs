using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class Cliente:NissiBaseBusiness
    {
        #region Métodos de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_cliente 
        /// <summary>
        /// Método para executar a proc pr_selecionar_cliente
        /// Objeto/Parametros: (identCliente)
        /// Valores: identCliente.CodPessoa,
        /// identCliente.CNPJ,
        /// identCliente.RazaoSocial,
        /// identCliente.NomeFantasia.
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<ClienteVO> Listar(ClienteVO identCliente)
        {
           return new ClienteData().Listar(identCliente);
        }
        #endregion
        #region Métodos de Listagem de Transportadoras
        /// <summary>
        /// Método para executar a proc pr_selecionar_tranportadoraporcliente 
        /// Objeto/Parametros: (codPessoa)
        /// </summary>
        public List<TransportadoraVO> ListarPorCliente(int? codPessoa)
        {
            return new ClienteData().ListarPorCliente(codPessoa);
        }

        // ------------------------------------------------------------------------- // 
        #endregion 
        #region Método de Vinculação da Tranportadora com o Cliente
        /// <summary>
        /// Método para executar a proc pr_incluir_clientetransportadora 
        /// Objeto/Parametros: (codPessoa, codTransportadora, usuarioInc)
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public void IncluirVinculacao(int? codPessoa, int? codTransportadora, int? usuarioInc)
        {
            new ClienteData().IncluirVinculacao(codPessoa, codTransportadora, usuarioInc);
        }


        // ------------------------------------------------------------------------- // 
        #endregion
        #region Método que retira a Vinculação da Transportadora com o Cliente
        /// <summary>
        /// Método para executar a proc pr_excluir_clientetransportadora 
        /// Objeto/Parametros: (codPessoa, codTransportadora)
        /// </summary>
        public void ExcluirVinculacao(int? codPessoa, int? codTransportadora)
        {
            new ClienteData().ExcluirVinculacao(codPessoa, codTransportadora);
        }


        // ------------------------------------------------------------------------- // 
        #endregion
        #region Métodos de Inclusão
        /// <summary>
        /// Método para executar a proc pr_incluir_cliente 
        /// Objeto/Parametros: (identCliente, codUsuarioOperacao)
        /// Valore: identCliente.Cep.CodCep,
        /// identCliente.RazaoSocial,
        /// identCliente.NomeFantasia,
        /// identCliente.Tipo,
        /// identCliente.CNPJ,
        /// identCliente.InscricaoEstadual,
        /// identCliente.Numero,
        /// identCliente.Complemento,
        /// identCliente.Telefone,
        /// identCliente.Fax,
        /// identCliente.Celular,
        /// identCliente.Contato,
        /// identCliente.Email,
        /// identCliente.Site,
        /// identCliente.Observacao,
        /// codUsuarioOperacao,
        /// identCliente.Funcionario.CodFuncionario,
        /// identCliente.Ativo,
        /// identCliente.IndPessoaTipo
        /// </summary>
        public int Incluir(ClienteVO identCliente,string xmlLista, int codUsuarioOperacao)
        {
            return new ClienteData().Incluir(identCliente, xmlLista, codUsuarioOperacao);
        }
        #endregion
        #region Métodos de Alteração
        /// <summary>
        /// Método para executar a proc pr_alterar_cliente 
        /// Objeto/Parametros: (identCliente, codUsuarioOperacao)
        /// Valore: identCliente.CodPessoa, 
        /// identCliente.Cep.CodCep,
        /// identCliente.RazaoSocial,
        /// identCliente.NomeFantasia,
        /// identCliente.Tipo,
        /// identCliente.CNPJ,
        /// identCliente.InscricaoEstadual,
        /// identCliente.Numero,
        /// identCliente.Complemento,
        /// identCliente.Telefone,
        /// identCliente.Fax,
        /// identCliente.Celular,
        /// identCliente.Contato,
        /// identCliente.Email,
        /// identCliente.Site,
        /// identCliente.Observacao,
        /// codUsuarioOperacao,
        /// identCliente.Funcionario.CodFuncionario,
        /// identCliente.Ativo,
        /// identCliente.IndPessoaTipo
        /// </summary>
        public void Alterar(ClienteVO identCliente,string xmlLista, int codUsuarioOperacao)
        {
            new ClienteData().Alterar(identCliente,xmlLista, codUsuarioOperacao);
        }
                #endregion
        #region Métodos de Exclusão
        /// <summary>
        /// Método para executar a proc pr_excluir_transportadora
        /// Objeto/Parametros: (codPessoa)
        /// </summary>
        public void Excluir(int? codPessoa)
        {
            new ClienteData().Excluir(codPessoa);
        }
        // ------------------------------------------------------------------------- // 
        #endregion

    }
}

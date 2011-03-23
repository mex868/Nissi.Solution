using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public class EmailEmitente
    {
        #region Métodos de Listagem
        public static List<EmailEmitenteVO> Listar()
        {
            return new EmailEmitenteRepositorio().Listar();
        }

        public static EmailEmitenteVO ListarPorCodigo(int codigoEmailEmitente)
        {
            return new EmailEmitenteRepositorio().ListarPorCodigo(codigoEmailEmitente);

        }
        public static List<EmailEmitenteVO> ListarPorEmailEmitente(string emailEmitente)
        {

            return new EmailEmitenteRepositorio().ListarPorEmailEmitente(emailEmitente);

        }
        public static List<EmailEmitenteVO> ListarPorTipo(int tipo)
        {

            return new EmailEmitenteRepositorio().ListarPorTipo(tipo);

        }
        #endregion
        #region Métodos de Inclusão
        public static int Incluir(string emailEmitente, int tipo, int codUsuarioInc)
        {
            return new EmailEmitenteRepositorio().Incluir(emailEmitente,tipo,codUsuarioInc);
        }
        #endregion
        #region Método de Alteração
        public static void Alterar(int codEmailEmitente, string emailEmitente, int tipo, int codUsuarioAlt)
        {
            new EmailEmitenteRepositorio().Alterar(codEmailEmitente, emailEmitente, tipo, codUsuarioAlt);
        }
        #endregion
        #region Método de Exclusão
        public static void Excluir(int codEmailEmitente, int codUsuarioInc)
        {
            new EmailEmitenteRepositorio().Excluir(codEmailEmitente, codUsuarioInc);
        }
        #endregion        
    }
}

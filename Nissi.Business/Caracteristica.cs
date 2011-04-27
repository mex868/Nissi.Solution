using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public static class Caracteristica
    {
        #region Métodos de Listagem

        public static List<CaracteristicaVO> Listar()
        {
            return new CaracteristicaRepositorio().Listar();
        }
        public static CaracteristicaVO ListarPorCodigo(int codigo)
        {
            return new CaracteristicaRepositorio().ListarPorCodigo(codigo);
        }
        public static List<CaracteristicaVO> ListarPorDescricao(string descricao)
        {
            return new CaracteristicaRepositorio().ListarPorDescricao(descricao);
        }

        #endregion
        #region Métodos de Inclusão

        public static int Incluir(string descricao, short? tipo, int codUsuarioInc)
        {
            return new CaracteristicaRepositorio().Incluir(descricao, tipo, codUsuarioInc);
        }

        #endregion
        #region Método de Alteração

        public static void Alterar(int codigo, string descricao, short? tipo, int codUsuarioAlt)
        {
            new CaracteristicaRepositorio().Alterar(codigo, descricao,tipo, codUsuarioAlt);
        }

        #endregion
        #region Método de Exclusão

        public static void Excluir(int codigo, int codUsuarioInc)
        {
            new CaracteristicaRepositorio().Excluir(codigo, codUsuarioInc);
        }
        #endregion
        #region Método para Listar Tipos de Caracteristica
        public static List<TipoCaracteristica> ListarTipoCaracteristica()
        {
            var lstTipoCaracteristica = new List<TipoCaracteristica>
                                            {
                                                new TipoCaracteristica() {Codigo = 1, Descricao = "Dados Técnicos"},
                                                new TipoCaracteristica() {Codigo = 2, Descricao = "Acabamento"},
                                                new TipoCaracteristica() {Codigo = 3, Descricao = "Carga"}
                                            };
            return lstTipoCaracteristica;
        }

        public static string ListarTipoCaracteristicaDescricao(short codigo)
        {
                        var lstTipoCaracteristica = new List<TipoCaracteristica>
                                            {
                                                new TipoCaracteristica() {Codigo = 1, Descricao = "Dados Técnicos"},
                                                new TipoCaracteristica() {Codigo = 2, Descricao = "Acabamento"},
                                                new TipoCaracteristica() {Codigo = 3, Descricao = "Carga"}
                                            };
            var descricao = lstTipoCaracteristica.Where(t => t.Codigo == codigo).Select(t => t.Descricao).FirstOrDefault();
            return descricao;
        }
        #endregion
    }

    public class TipoCaracteristica
    {
        public short Codigo { get; set; }
        public string Descricao { get; set; }
    }
}

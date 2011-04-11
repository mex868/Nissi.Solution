using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Nissi.Util;


namespace Nissi.DataAccess
{
    public class UsuarioData: NissiBaseData
    {

        public IList<Model.PerfilAcessoVO> ListarPerfilNaoAssociado(int codUsuario)
        {
            OpenCommand("pr_selecionar_perfilusuarionaoassociado");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodUsuario", DbType.Int32, codUsuario);

                List<Model.PerfilAcessoVO> lstPerfilAcessoVO = new List<Model.PerfilAcessoVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        Model.PerfilAcessoVO perfilAcessoVO = new Model.PerfilAcessoVO();

                        perfilAcessoVO.CodPerfilAcesso = GetReaderValue<short>(dr, "CodPerfilAcesso");
                        perfilAcessoVO.NomPerfilAcesso = GetReaderValue<string>(dr, "NomPerfilAcesso");
                        perfilAcessoVO.DescPerfilAcesso = GetReaderValue<string>(dr, "DescPerfilAcesso");
                        lstPerfilAcessoVO.Add(perfilAcessoVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstPerfilAcessoVO;
            }
            finally
            {
                CloseCommand();
            }
        }

        public IList<Model.PerfilAcessoVO> ListarPerfilAssociado(int codUsuario)
        {
            OpenCommand("pr_selecionar_perfilusuarioassociado");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodUsuario", DbType.Int32, codUsuario);

                List<Model.PerfilAcessoVO> lstPerfilAcessoVO = new List<Model.PerfilAcessoVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        Model.PerfilAcessoVO perfilAcessoVO = new Model.PerfilAcessoVO();

                        perfilAcessoVO.CodPerfilAcesso = GetReaderValue<short>(dr, "CodPerfilAcesso");
                        perfilAcessoVO.NomPerfilAcesso = GetReaderValue<string>(dr, "NomPerfilAcesso");
                        perfilAcessoVO.DescPerfilAcesso = GetReaderValue<string>(dr, "DescPerfilAcesso");
                        lstPerfilAcessoVO.Add(perfilAcessoVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstPerfilAcessoVO;
            }
            finally
            {
                CloseCommand();
            }
        }

        public void Incluir(int codUsuario, List<Model.PerfilAcessoVO> listaAssociados, int codUsuarioOperacao)
        {
            foreach (Model.PerfilAcessoVO perfilAcessoVO in listaAssociados)
            {
                OpenCommand("pr_incluir_perfilusuario");
                try
                {
                    // Parâmetros de entrada
                    AddInParameter("@CodUsuario", DbType.Int32, codUsuario);
                    AddInParameter("@CodPerfilAcesso", DbType.Int16, perfilAcessoVO.CodPerfilAcesso);
                    AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
                    ExecuteNonQuery();
                }
                finally
                {
                    CloseCommand();
                }
            }
        }

        public void Excluir(int codUsuario, List<Model.PerfilAcessoVO> listaAssociar)
        {
            foreach (Model.PerfilAcessoVO perfilAcessoVO in listaAssociar)
            {
                OpenCommand("pr_excluir_perfilusuario");
                try
                {
                    // Parâmetros de entrada
                    AddInParameter("@CodUsuario", DbType.Int32, codUsuario);
                    AddInParameter("@CodPerfilAcesso", DbType.Int16, perfilAcessoVO.CodPerfilAcesso);
                    ExecuteNonQuery();
                }
                finally
                {
                    CloseCommand();
                }
            }
        }

        public string ListarMenuUsuario(int codUsuario)
        {
            OpenCommand("pr_selecionar_menuusuario");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodUsuario", DbType.Int32, codUsuario);

                var menuListVO = new Model.XmlMenu { XmlMenuItem = new List<Model.XmlMenuItem>() };
                var menuItem = new Model.XmlMenuItem
                                   {
                                       XmlSubMenu = new Model.XmlSubMenu { XmlSubMenuItem = new List<Model.XmlSubMenuItem>() }
                                   };
                short codigoNovo = 0;
                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        var codigoAntigo = GetReaderValue<short>(dr, "CodMenu");
                        if (codigoAntigo != codigoNovo)
                        {
                            if (!string.IsNullOrEmpty(menuItem.Text))
                                menuListVO.XmlMenuItem.Add(menuItem);
                            menuItem = new Model.XmlMenuItem
                            {
                                XmlSubMenu = new Model.XmlSubMenu { XmlSubMenuItem = new List<Model.XmlSubMenuItem>() }
                            };
                        }                        
                        var xmlSubMenuItem = new Model.XmlSubMenuItem
                                                 {
                                                     Text = GetReaderValue<string>(dr, "submenu"),
                                                     Url = GetReaderValue<string>(dr,"url"),
                                                     ResolveUrl = GetReaderValue<bool>(dr, "resolveurl")
                                                 };
                        menuItem.XmlSubMenu.XmlSubMenuItem.Add(xmlSubMenuItem);
                        menuItem.Text = GetReaderValue<string>(dr, "Menu");
                        codigoNovo = codigoAntigo;
                    }
                    menuListVO.XmlMenuItem.Add(menuItem);
                }
                finally
                {
                    dr.Close();
                }

                return menuListVO.ToXml();
            }
            finally
            {
                CloseCommand();
            }
        }

        public IList<Model.PerfilAcessoVO> ListaPerfilAdministracao(Model.UsuarioVO identUsuario)
        {
            IList<Model.PerfilAcessoVO> listPerfilAcesso = new List<Model.PerfilAcessoVO>();
            if (identUsuario.Funcionario.CodFuncionario != null)
              listPerfilAcesso =  ListarPerfilAssociado(identUsuario.Funcionario.CodFuncionario.Value);
            return listPerfilAcesso;
        }
        /// <summary>
        /// Alteração de senha do usuário (primeiro acesso ao sistema)
        /// </summary>
        /// <param name="identUsuario"></param>
        public void AlterarSenha(Model.UsuarioVO identUsuario)
        {
            OpenCommand("pr_alterar_senhausuario");
            AddInParameter("@CodFuncionario", DbType.Int32, identUsuario.Funcionario.CodFuncionario);
            AddBinaryParameter("@Senha", identUsuario.Funcionario.Senha);
            ExecuteNonQuery();
            CloseCommand();
        }
    }
}

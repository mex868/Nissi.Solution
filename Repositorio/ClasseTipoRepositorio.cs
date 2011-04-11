using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class ClasseTipoRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public ClasseTipoRepositorio()
        {
            _repositorioDataContext= new RepositorioDataContext();
        }
        #region Métodos de Listagem
        public List<ClasseTipoVO> Listar()
        {
            var query = from c in _repositorioDataContext.ClasseTipos
                        select new ClasseTipoVO()
                                   {
                                       CodClasseTipo = c.CodClasseTipo,
                                       Descricao = c.Descricao,
                                       DataCadastro = c.DataCadastro,
                                       DataAlteracao = c.DataAlteracao,
                                       UsuarioInc = c.UsuarioInc,
                                       UsuarioAlt = c.UsuarioAlt
                                   };
            var lstClasseTipo = new List<ClasseTipoVO>();
            if (query.Count() > 0)
                lstClasseTipo = query.ToList();
            return lstClasseTipo;
        }
        public ClasseTipoVO ListarPorCodigo(int codClasseTipo)
        {
            var query = from c in _repositorioDataContext.ClasseTipos
                        where c.CodClasseTipo == codClasseTipo
                        select new ClasseTipoVO()
                        {
                            CodClasseTipo = c.CodClasseTipo,
                            Descricao = c.Descricao,
                            DataCadastro = c.DataCadastro,
                            DataAlteracao = c.DataAlteracao,
                            UsuarioInc = c.UsuarioInc,
                            UsuarioAlt = c.UsuarioAlt
                        };
           var classetipo = query.FirstOrDefault();
            return classetipo;
        }
        public List<ClasseTipoVO> ListarPorClasseTipo(string classeTipo)
        {

            var query = from c in _repositorioDataContext.ClasseTipos
                        where c.Descricao.Contains(classeTipo)
                        select new ClasseTipoVO()
                        {
                            CodClasseTipo = c.CodClasseTipo,
                            Descricao = c.Descricao,
                            DataCadastro = c.DataCadastro,
                            DataAlteracao = c.DataAlteracao,
                            UsuarioInc = c.UsuarioInc,
                            UsuarioAlt = c.UsuarioAlt
                        };

            var lstClasseTipo = new List<ClasseTipoVO>();
            if (query.Count() > 0)
                lstClasseTipo = query.ToList();
            return lstClasseTipo;
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(string descricao, int codUsuarioInc)
        {
            var classetipo = new ClasseTipo()
                                 {
                                     Descricao = descricao,
                                     DataCadastro = DateTime.Now,
                                     UsuarioInc = codUsuarioInc
                                 };
            _repositorioDataContext.ClasseTipos.InsertOnSubmit(classetipo);
            _repositorioDataContext.SubmitChanges();
            return classetipo.CodClasseTipo;
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codClasseTipo, string descricao, int codUsuarioAlt)
        {
            IQueryable<ClasseTipo> query = from c in _repositorioDataContext.ClasseTipos
                                           where c.CodClasseTipo == codClasseTipo
                                           select c;
            var classetipo = query.FirstOrDefault();
            classetipo.Descricao = descricao;
            classetipo.DataAlteracao = DateTime.Now;
            classetipo.UsuarioAlt = codUsuarioAlt;
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codClasseTipo, int codUsuarioExc)
        {
            IQueryable<ClasseTipo> query = from c in _repositorioDataContext.ClasseTipos
                                           where c.CodClasseTipo == codClasseTipo
                                           select c;
            var classetipo = query.FirstOrDefault();
            _repositorioDataContext.ClasseTipos.DeleteOnSubmit(classetipo);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
    }
}

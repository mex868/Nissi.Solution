using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class MateriaPrimaRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public MateriaPrimaRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Métodos de Listagem
        public MateriaPrimaVO ListarPorCodigo(int codigoMateriaPrima)
        {
            var query = from m in _repositorioDataContext.MateriaPrimas
                        join n in _repositorioDataContext.Normas
                            on m.CodNorma equals (n.CodNorma)
                        where m.CodMateriaPrima == codigoMateriaPrima
                        select new MateriaPrimaVO()
                                   {
                                       CodMateriaPrima = m.CodMateriaPrima,
                                       ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                       where ct.CodClasseTipo == m.CodClasseTipo
                                                       select new ClasseTipoVO()
                                                                  {
                                                                      CodClasseTipo = ct.CodClasseTipo,
                                                                      Descricao = ct.Descricao
                                                                  }).FirstOrDefault(),
                                       DataCadastro = m.DataCadastro,
                                       DataAlteracao = m.DataAlteracao,
                                       NormaVo = new NormaVO()
                                                     {
                                                         CodNorma = n.CodNorma,
                                                         Descricao = n.Descricao,
                                                         Revisao = n.Revisao
                                                     },
                                       UsuarioInc = m.UsuarioInc,
                                       UsuarioAlt = m.UsuarioAlt
                                   };
            var materiaPrima = new MateriaPrimaVO();
            if (query.Count() > 0)
                materiaPrima = query.FirstOrDefault();
            return materiaPrima;
        }
        public MateriaPrimaVO ListarTudo(int codMateriaPrima)
        {
            var query = from m in _repositorioDataContext.MateriaPrimas
                        join n in _repositorioDataContext.Normas
                            on m.CodNorma equals (n.CodNorma)
                        where m.CodMateriaPrima == codMateriaPrima
                        select new MateriaPrimaVO()
                        {
                            CodMateriaPrima = m.CodMateriaPrima,
                            ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                            where ct.CodClasseTipo == m.CodClasseTipo
                                            select new ClasseTipoVO()
                                            {
                                                CodClasseTipo = ct.CodClasseTipo,
                                                Descricao = ct.Descricao
                                            }).FirstOrDefault(),
                            DataCadastro = m.DataCadastro,
                            DataAlteracao = m.DataAlteracao,
                            NormaVo = new NormaVO()
                            {
                                CodNorma = n.CodNorma,
                                Descricao = n.Descricao,
                                Revisao = n.Revisao
                            },
                            ComposicaoMateriaPrimaVos = new ComposicaoMateriaPrimaRepositorio().ListarPorCodigo(codMateriaPrima),
                            ResistenciaTracaoVos = new ResistenciaTracaoRepositorio().ListarPorCodigo(codMateriaPrima),
                            UsuarioInc = m.UsuarioInc,
                            UsuarioAlt = m.UsuarioAlt
                        };
            var materiaPrima = query.FirstOrDefault();

            return materiaPrima;
        }

        public List<ComposicaoMateriaPrimaVO> ListaComposicaoMateriaPrima(int codMateriaPrima)
        {
            return new ComposicaoMateriaPrimaRepositorio().ListarPorCodigo(codMateriaPrima);
        }

        public List<MateriaPrimaVO> ListarPorNorma(string descricao)
        {
            var query = from m in _repositorioDataContext.MateriaPrimas
                        join n in _repositorioDataContext.Normas
                        on m.CodNorma equals (n.CodNorma)
                        where n.Descricao.Contains(descricao)
                        select new MateriaPrimaVO()
                        {
                            CodMateriaPrima = m.CodMateriaPrima,
                            ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                            where ct.CodClasseTipo == m.CodClasseTipo
                                            select new ClasseTipoVO()
                                            {
                                                CodClasseTipo = ct.CodClasseTipo,
                                                Descricao = ct.Descricao
                                            }).FirstOrDefault(),
                            DataCadastro = m.DataCadastro,
                            DataAlteracao = m.DataAlteracao,
                            NormaVo = new NormaVO()
                            {
                                CodNorma = n.CodNorma,
                                Descricao = n.Descricao,
                                Revisao = n.Revisao
                            },
                            UsuarioInc = m.UsuarioInc,
                            UsuarioAlt = m.UsuarioAlt,
                        };
            var lstMateriaPrima = new List<MateriaPrimaVO>();
            if (query.Count() > 0)
                lstMateriaPrima = query.ToList();
            return lstMateriaPrima;
        }

        public List<MateriaPrimaVO> ListarPorClasseTipo(int codClasseTipo)
        {
            var query = from m in _repositorioDataContext.MateriaPrimas
                        join c in _repositorioDataContext.ClasseTipos
                        on m.CodClasseTipo equals c.CodClasseTipo
                        join n in _repositorioDataContext.Normas
                        on m.CodNorma equals (n.CodNorma)
                        where c.CodClasseTipo == codClasseTipo
                        select new MateriaPrimaVO()
                        {
                            CodMateriaPrima = m.CodMateriaPrima,
                            ClasseTipoVo = new ClasseTipoVO()
                            {
                                Descricao = c.Descricao
                            },
                            NormaVo = new NormaVO()
                            {
                                CodNorma = n.CodNorma,
                                Descricao = n.Descricao,
                                Revisao = n.Revisao
                            },
                        };
            var lstMateriaPrima = new List<MateriaPrimaVO>();
            if (query.Count() > 0)
                lstMateriaPrima = query.ToList();
            return lstMateriaPrima;
        }
        public List<MateriaPrimaVO> Listar()
        {
            var query = from m in _repositorioDataContext.MateriaPrimas
                        join n in _repositorioDataContext.Normas
                            on m.CodNorma equals (n.CodNorma)
                        select new MateriaPrimaVO()
                        {
                            CodMateriaPrima = m.CodMateriaPrima,
                            ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                            where ct.CodClasseTipo == m.CodClasseTipo
                                            select new ClasseTipoVO()
                                            {
                                                CodClasseTipo = ct.CodClasseTipo,
                                                Descricao = ct.Descricao
                                            }).FirstOrDefault(),
                            DataCadastro = m.DataCadastro,
                            DataAlteracao = m.DataAlteracao,
                            NormaVo = new NormaVO()
                            {
                                CodNorma = n.CodNorma,
                                Descricao = n.Descricao,
                                Revisao = n.Revisao
                            },
                            UsuarioInc = m.UsuarioInc,
                            UsuarioAlt = m.UsuarioAlt
                        };
            var lstmateriaPrima = new List<MateriaPrimaVO>();
            if (query.Count() > 0)
                lstmateriaPrima = query.ToList();
            return lstmateriaPrima;
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(int? codClasseTipo, int codNorma, int usuarioInc, List<ComposicaoMateriaPrimaVO> lstComposicaoMateriaPrima, List<ResistenciaTracaoVO> lstResistenciaTracao)
        {
            var materiaPrima = new MateriaPrima()
                                   {
                                       CodClasseTipo = codClasseTipo,
                                       CodNorma = codNorma,
                                       DataCadastro = DateTime.Now,
                                       UsuarioInc = 1
                                   };
            _repositorioDataContext.MateriaPrimas.InsertOnSubmit(materiaPrima);
            _repositorioDataContext.SubmitChanges();
            new ComposicaoMateriaPrimaRepositorio().Incluir(lstComposicaoMateriaPrima, materiaPrima.CodMateriaPrima);
            new ResistenciaTracaoRepositorio().Incluir(lstResistenciaTracao, materiaPrima.CodMateriaPrima);
            return materiaPrima.CodMateriaPrima;
        }
        #endregion
        #region Métodos de Alteração
        public void Alterar(int codMateriaPrima, int? codClasseTipo, int codNorma, int usuarioAlt, List<ComposicaoMateriaPrimaVO> lstComposicaoMateriaPrima, List<ResistenciaTracaoVO> lstResistenciaTracao)
        {
            IQueryable<MateriaPrima> query = from m in _repositorioDataContext.MateriaPrimas
                                             where m.CodMateriaPrima == codMateriaPrima
                                             select m;
            var materiaPrima = query.First();
            materiaPrima.CodClasseTipo = codClasseTipo;
            materiaPrima.CodNorma = codNorma;
            materiaPrima.DataAlteracao = DateTime.Now;
            materiaPrima.UsuarioAlt = usuarioAlt;
            new ComposicaoMateriaPrimaRepositorio().Alterar(lstComposicaoMateriaPrima, codMateriaPrima);
            new ResistenciaTracaoRepositorio().Alterar(lstResistenciaTracao, codMateriaPrima);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusao
        public void Excluir(int codMateriaPrima, int codUsuarioExc)
        {
            IQueryable<MateriaPrima> query = from m in _repositorioDataContext.MateriaPrimas
                                             where m.CodMateriaPrima == codMateriaPrima
                                             select m;
            var materiaPrima = query.FirstOrDefault();
             new ComposicaoMateriaPrimaRepositorio().ExcluirTodos(codMateriaPrima);
            new ResistenciaTracaoRepositorio().ExcluirTodos(codMateriaPrima);
            _repositorioDataContext.MateriaPrimas.DeleteOnSubmit(materiaPrima);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion


    }
}


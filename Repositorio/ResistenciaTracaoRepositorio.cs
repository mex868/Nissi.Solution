using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class ResistenciaTracaoRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public ResistenciaTracaoRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Métodos de Listagem

        public List<ResistenciaTracaoVO> ListarPorCodigo(int codMateriaPrima)
        {
            var query = from r in _repositorioDataContext.ResistenciaTracaos
                        join b in _repositorioDataContext.Bitolas
                        on r.CodBitola equals b.CodBitola
                        where r.CodMateriaPrima == codMateriaPrima
                        select new ResistenciaTracaoVO()
                        {
                            CodResistenciaTracao = r.CodResistenciaTracao,
                            CodMateriaPrima = r.CodMateriaPrima,
                            Bitola = new BitolaVO(){CodBitola = b.CodBitola, Bitola = b.Bitola1},
                            Tolerancia = r.Tolerancia,
                            Minimo = r.Minimo,
                            Maximo = r.Maximo,
                            DataCadastro = r.DataCadastro,
                            DataAlteracao = r.DataCadastro,
                            UsuarioInc = r.UsuarioInc,
                            UsuarioAlt = r.UsuarioAlt
                        };
            var lstResistenciaTracao = new List<ResistenciaTracaoVO>();
            if (query.Count() > 0)
                lstResistenciaTracao = query.ToList();
            return lstResistenciaTracao;
        }
        public ResistenciaTracaoVO ListarPorCodigoBitola(int codMateriaPrima, int codBitola)
        {
            var query = (from r in _repositorioDataContext.ResistenciaTracaos
                        join b in _repositorioDataContext.Bitolas
                        on r.CodBitola equals b.CodBitola
                        where r.CodMateriaPrima == codMateriaPrima
                        && r.CodBitola == codBitola
                        select new ResistenciaTracaoVO()
                        {
                            CodResistenciaTracao = r.CodResistenciaTracao,
                            CodMateriaPrima = r.CodMateriaPrima,
                            Bitola = new BitolaVO() { CodBitola = b.CodBitola, Bitola = b.Bitola1 },
                            Tolerancia = r.Tolerancia,
                            Minimo = r.Minimo,
                            Maximo = r.Maximo,
                            DataCadastro = r.DataCadastro,
                            DataAlteracao = r.DataCadastro,
                            UsuarioInc = r.UsuarioInc,
                            UsuarioAlt = r.UsuarioAlt
                        }).FirstOrDefault();
            var resistenciaTracao = new ResistenciaTracaoVO();
            if (query != null)
                resistenciaTracao = query;
            return resistenciaTracao;
        }
        #endregion
        #region Método de Inclusão
        public void Incluir(List<ResistenciaTracaoVO> resistenciaTracaoVos, int codMateriaPrima)
        {
            var lstResistenciaTracao = resistenciaTracaoVos.Select(r => new ResistenciaTracao()
            {
                CodMateriaPrima = codMateriaPrima,
                CodResistenciaTracao = r.CodResistenciaTracao,
                CodBitola = r.Bitola.CodBitola,
                Tolerancia = r.Tolerancia,
                Maximo = r.Maximo,
                Minimo = r.Minimo,
                DataCadastro = r.DataCadastro,
                UsuarioInc = r.UsuarioInc
            });
            _repositorioDataContext.ResistenciaTracaos.InsertAllOnSubmit(lstResistenciaTracao);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Alteração
        public void Alterar(List<ResistenciaTracaoVO> resistenciaTracaoVos, int codMateriaPrima)
        {
            var lstResistenciaTracao = resistenciaTracaoVos.Where(r => r.CodResistenciaTracao == 0).Select(r => new ResistenciaTracao()
            {
                CodMateriaPrima = codMateriaPrima,
                CodResistenciaTracao = r.CodResistenciaTracao,
                CodBitola = r.Bitola.CodBitola,
                Tolerancia = r.Tolerancia,
                Maximo = r.Maximo,
                Minimo = r.Minimo,
                DataCadastro = r.DataCadastro,
                UsuarioInc = r.UsuarioInc
            });
            if (lstResistenciaTracao.Count() > 0)
            _repositorioDataContext.ResistenciaTracaos.InsertAllOnSubmit(lstResistenciaTracao);
            foreach (var item in resistenciaTracaoVos)
            {
                if (item.CodResistenciaTracao > 0)
                {
                    IQueryable<ResistenciaTracao> query = from r in _repositorioDataContext.ResistenciaTracaos
                                                          where r.CodResistenciaTracao == item.CodResistenciaTracao
                                                          select r;
                    var resistenciatracao = query.FirstOrDefault();
                    resistenciatracao.CodMateriaPrima = item.CodMateriaPrima;
                    resistenciatracao.CodResistenciaTracao = item.CodResistenciaTracao;
                    resistenciatracao.CodBitola = item.Bitola.CodBitola;
                    resistenciatracao.Tolerancia = item.Tolerancia;
                    resistenciatracao.Maximo = item.Maximo;
                    resistenciatracao.Minimo = item.Minimo;
                    resistenciatracao.DataAlteracao = item.DataAlteracao;
                    resistenciatracao.UsuarioAlt = item.UsuarioAlt;
                }
            }
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codResistenciaTracao)
        {
            IQueryable<ResistenciaTracao> query = from n in _repositorioDataContext.ResistenciaTracaos
                                      where n.CodResistenciaTracao == codResistenciaTracao
                                      select n;
            var resistenciaTracao = query.FirstOrDefault();
            _repositorioDataContext.ResistenciaTracaos.DeleteOnSubmit(resistenciaTracao);
            _repositorioDataContext.SubmitChanges();
        }
        public void ExcluirTodos(int codMateriaPrima)
        {
            IQueryable<ResistenciaTracao> query = from n in _repositorioDataContext.ResistenciaTracaos
                                                  where n.CodMateriaPrima == codMateriaPrima
                                                  select n;
            if (query.Count() > 0)
            {
                var lstResistenciaTracao = query.ToList();
                _repositorioDataContext.ResistenciaTracaos.DeleteAllOnSubmit(lstResistenciaTracao);
                _repositorioDataContext.SubmitChanges();
            }
        }
        #endregion
    }
}

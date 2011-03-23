using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class BitolaRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public BitolaRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Métodos de Listagem
        public List<BitolaVO> Listar()
        {
            var query = from b in _repositorioDataContext.Bitolas
                        select new BitolaVO()
                                   {

                                       CodBitola = b.CodBitola,
                                       Bitola = b.Bitola1,
                                       DataCadastro = b.DataCadastro,
                                       DataAlteracao = b.DataAlteracao,
                                       UsuarioInc = b.UsuarioInc,
                                       UsuarioAlt = b.UsuarioAlt
                                   };
            var lstBitolaVo = new List<BitolaVO>();
            if (query.Count() > 0)
                lstBitolaVo = query.ToList();
            return lstBitolaVo;
        }
        public BitolaVO ListarPorCodigo(int codigoBitola)
        {
            var query = from b in _repositorioDataContext.Bitolas
                        where b.CodBitola == codigoBitola
                        select new BitolaVO()
                                   {
                                       CodBitola = b.CodBitola,
                                       Bitola = b.Bitola1,
                                       DataCadastro = b.DataCadastro,
                                       DataAlteracao = b.DataAlteracao,
                                       UsuarioInc = b.UsuarioInc,
                                       UsuarioAlt = b.UsuarioAlt
                                   };

                var bitolaVo = query.FirstOrDefault();
            return bitolaVo;

        }
        public List<BitolaVO> ListarPorBitola(decimal bitola)
        {
            var query = from b in _repositorioDataContext.Bitolas
                        where b.Bitola1 >= bitola
                        select new BitolaVO()
                        {
                            CodBitola = b.CodBitola,
                            Bitola = b.Bitola1,
                            DataCadastro = b.DataCadastro,
                            DataAlteracao = b.DataAlteracao,
                            UsuarioInc = b.UsuarioInc,
                            UsuarioAlt = b.UsuarioAlt
                        };
            var lstbitolaVo = new List<BitolaVO>();
            if (query.Count() > 0)
                lstbitolaVo = query.ToList();
            return lstbitolaVo;

        }
       #endregion
        #region Métodos de Inclusão
        public int Incluir(decimal bitola, int codUsuarioInc)
        {
            var identBitola = new Bitola()
                                  {
                                      Bitola1 = bitola,
                                      DataCadastro = DateTime.Now,
                                      UsuarioInc = codUsuarioInc
                                  };
            _repositorioDataContext.Bitolas.InsertOnSubmit(identBitola);
            _repositorioDataContext.SubmitChanges();
            return identBitola.CodBitola;
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codBitola, decimal bitola, int codUsuarioAlt)
        {
            IQueryable<Bitola> query = from b in _repositorioDataContext.Bitolas
                                       where b.CodBitola == codBitola
                                       select b;
            var identBitola = query.FirstOrDefault();
            identBitola.Bitola1 = bitola;
            identBitola.DataAlteracao = DateTime.Now;
            identBitola.UsuarioAlt = codUsuarioAlt;
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codBitola, int codUsuarioInc)
        {
            IQueryable<Bitola> query = from b in _repositorioDataContext.Bitolas
                                       where b.CodBitola == codBitola
                                       select b;
            if (query.Count() > 0)
            {
                var identBitola = query.FirstOrDefault();
                _repositorioDataContext.Bitolas.DeleteOnSubmit(identBitola);
                _repositorioDataContext.SubmitChanges();
            }
        }
        #endregion
    }
}

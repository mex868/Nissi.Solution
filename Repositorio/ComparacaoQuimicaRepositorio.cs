using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    
    public class ComparacaoQuimicaRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;      
        public ComparacaoQuimicaRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Métodos de Inclusao
        public void Inclui(List<ComparacaoQuimicaVO> comparacaoQuimicaVos)
        {
            var lstComparacaoQuimica = comparacaoQuimicaVos.Select(comparacaoQuimicaVo => new ComparacaoQuimicaVO()
                                                                                              {
                                                                                                  CodMateriaPrima = comparacaoQuimicaVo.CodMateriaPrima, Descricao = comparacaoQuimicaVo.Descricao, Maxima = comparacaoQuimicaVo.Maxima, Minima = comparacaoQuimicaVo.Minima, DataCadastro = comparacaoQuimicaVo.DataCadastro, UsuarioInc = comparacaoQuimicaVo.UsuarioInc
                                                                                              }).ToList();
           // _repositorioDataContext.ComparacaoQuimicas.InsertAllOnSubmit(lstComparacaoQuimica);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Alteracao
		public void Alterar(List<ComparacaoQuimicaVO> comparacaoQuimicaVos)
		{
		    foreach (var comparacaoQuimicaVo in comparacaoQuimicaVos)
		    {
		        ComparacaoQuimicaVO vo = comparacaoQuimicaVo;
		        //IQueryable<ComparacaoQuimicaVO> query =
		                    //_repositorioDataContext.ComparacaoQuimicas.Where(
		                        //m => m.CodComparacaoQuimica == vo.CodComparacaoQuimica);
            /*var comparacaoQuimica = query.First();
            comparacaoQuimica.Descricao = comparacaoQuimicaVo.Descricao;
            comparacaoQuimica.Maxima = comparacaoQuimicaVo.Maxima;
            comparacaoQuimica.Minima =comparacaoQuimicaVo.Minima;
            comparacaoQuimica.DataAlteracao = comparacaoQuimicaVo.DataAlteracao;
            comparacaoQuimica.UsuarioAlt = comparacaoQuimica.UsuarioAlt;*/
            }
            _repositorioDataContext.SubmitChanges();

		} 
    	#endregion
        #region Método de Exclusao
        public void Exclui(List<ComparacaoQuimicaVO> comparacaoQuimicas )
        {
            //var lstComparacaoQuimica = comparacaoQuimicas.Select(vo => _repositorioDataContext.ComparacaoQuimicas.Where(m => m.CodComparacaoQuimica == vo.CodComparacaoQuimica)).Select(query => query.First()).ToList();
            //_repositorioDataContext.ComparacaoQuimicas.DeleteAllOnSubmit(lstComparacaoQuimica);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
    }
}

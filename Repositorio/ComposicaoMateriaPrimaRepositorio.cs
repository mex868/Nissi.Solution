using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    
    public class ComposicaoMateriaPrimaRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public ComposicaoMateriaPrimaRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }

        public List<ComposicaoMateriaPrimaVO> ListarPorCodigo(int codMateriaPrima)
        {
            var query = from c in _repositorioDataContext.ComposicaoMateriaPrimas
                        where c.CodMateriaPrima == codMateriaPrima
                        select new ComposicaoMateriaPrimaVO()
                                   {
                                       CodComposicaoMateriaPrima = c.CodComposicaoMateriaPrima,
                                       CodMateriaPrima = codMateriaPrima,
                                       BitolaMinima = c.BitolaMinima,
                                       BitolaMaxima = c.BitolaMaxima,
                                       CMinimo = c.CMinimo,
                                       CMaximo = c.CMaximo,
                                       SiMinimo = c.SiMinimo,
                                       SiMaximo = c.SiMaximo,
                                       MnMinimo = c.MnMinimo,
                                       MnMaximo = c.MnMaximo,
                                       PMinimo = c.PMinimo,
                                       PMaximo = c.PMaximo,
                                       SMinimo = c.SMinimo,
                                       SMaximo = c.SMaximo,
                                       CrMinimo = c.CrMinimo,
                                       CrMaximo = c.CrMaximo,
                                       NiMinimo = c.NiMinimo,
                                       NiMaximo = c.NiMinimo,
                                       MoMinimo = c.MoMinimo,
                                       MoMaximo = c.MoMaximo,
                                       CuMinimo = c.CuMinimo,
                                       CuMaximo = c.CuMaximo,
                                       TiMinimo = c.TiMinimo,
                                       TiMaximo = c.TiMaximo,
                                       N2Minimo = c.N2Minimo,
                                       N2Maximo = c.N2Maximo,
                                       CoMinimo = c.CoMinimo,
                                       CoMaximo = c.CoMaximo,
                                       AlMinimo = c.AlMinimo,
                                       AlMaximo = c.AlMinimo,
                                       ZnMinimo = c.ZnMinimo,
                                       ZnMaximo = c.ZnMaximo,
                                       SnMinimo = c.SnMinimo,
                                       SnMaximo = c.SnMaximo,
                                       PbMinimo = c.PbMinimo,
                                       PbMaximo = c.PbMaximo,
                                       FeMinimo = c.FeMinimo,
                                       FeMaximo = c.FeMaximo,
                                       DataCadastro = c.DataCadastro,
                                       UsuarioInc = c.UsuarioInc
                                   };
            var lstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>();
            if (query.Count() > 0)
                lstComposicaoMateriaPrima = query.ToList();

            return lstComposicaoMateriaPrima;
        }

        public ComposicaoMateriaPrimaVO ListarPorCodigoBitola(int codMateriaPrima, decimal bitola)
        {
            var query = from c in _repositorioDataContext.ComposicaoMateriaPrimas
                        where c.CodMateriaPrima == codMateriaPrima
                        select new ComposicaoMateriaPrimaVO()
                        {
                            CodComposicaoMateriaPrima = c.CodComposicaoMateriaPrima,
                            CodMateriaPrima = codMateriaPrima,
                            BitolaMinima = c.BitolaMinima,
                            BitolaMaxima = c.BitolaMaxima,
                            CMinimo = c.CMinimo,
                            CMaximo = c.CMaximo,
                            SiMinimo = c.SiMinimo,
                            SiMaximo = c.SiMaximo,
                            MnMinimo = c.MnMinimo,
                            MnMaximo = c.MnMaximo,
                            PMinimo = c.PMinimo,
                            PMaximo = c.PMaximo,
                            SMinimo = c.SMinimo,
                            SMaximo = c.SMaximo,
                            CrMinimo = c.CrMinimo,
                            CrMaximo = c.CrMaximo,
                            NiMinimo = c.NiMinimo,
                            NiMaximo = c.NiMinimo,
                            MoMinimo = c.MoMinimo,
                            MoMaximo = c.MoMaximo,
                            CuMinimo = c.CuMinimo,
                            CuMaximo = c.CuMaximo,
                            TiMinimo = c.TiMinimo,
                            TiMaximo = c.TiMaximo,
                            N2Minimo = c.N2Minimo,
                            N2Maximo = c.N2Maximo,
                            CoMinimo = c.CoMinimo,
                            CoMaximo = c.CoMaximo,
                            AlMinimo = c.AlMinimo,
                            AlMaximo = c.AlMinimo,
                            ZnMinimo = c.ZnMinimo,
                            ZnMaximo = c.ZnMaximo,
                            SnMinimo = c.SnMinimo,
                            SnMaximo = c.SnMaximo,
                            PbMinimo = c.PbMinimo,
                            PbMaximo = c.PbMaximo,
                            FeMinimo = c.FeMinimo,
                            FeMaximo = c.FeMaximo,
                            DataCadastro = c.DataCadastro,
                            UsuarioInc = c.UsuarioInc
                        };
            var lstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>();
            if (query.Count() > 0)
                lstComposicaoMateriaPrima = query.ToList();

            return lstComposicaoMateriaPrima.FirstOrDefault();
        }
        #region Métodos de Inclusao
        public void Incluir(List<ComposicaoMateriaPrimaVO> composicaoMateriaPrimaVos, int codMateriaPrima)
        {
            var lstComposicaoMateriaPrima = composicaoMateriaPrimaVos.Select(c => new ComposicaoMateriaPrima()
                                                                                              {
                                                                                        CodMateriaPrima = codMateriaPrima,
	                                                                                    BitolaMinima = c.BitolaMinima,
	                                                                                    BitolaMaxima = c.BitolaMaxima,
	                                                                                    CMinimo = c.CMinimo,
	                                                                                    CMaximo = c.CMaximo,
	                                                                                    SiMinimo = c.SiMinimo,
	                                                                                    SiMaximo = c.SiMaximo, 
	                                                                                    MnMinimo = c.MnMinimo,
	                                                                                    MnMaximo = c.MnMaximo,  
	                                                                                    PMinimo = c.PMinimo,
	                                                                                    PMaximo = c.PMaximo,
	                                                                                    SMinimo = c.SMinimo,
	                                                                                    SMaximo = c.SMaximo,
	                                                                                    CrMinimo = c.CrMinimo,
	                                                                                    CrMaximo = c.CrMaximo,
	                                                                                    NiMinimo = c.NiMinimo,
	                                                                                    NiMaximo = c.NiMinimo,
	                                                                                    MoMinimo  = c.MoMinimo,
	                                                                                    MoMaximo  = c.MoMaximo,
	                                                                                    CuMinimo  = c.CuMinimo,
	                                                                                    CuMaximo  = c.CuMaximo,
	                                                                                    TiMinimo  = c.TiMinimo,
	                                                                                    TiMaximo  = c.TiMaximo,
	                                                                                    N2Minimo  = c.N2Minimo,
	                                                                                    N2Maximo  = c.N2Maximo,
	                                                                                    CoMinimo  = c.CoMinimo,
	                                                                                    CoMaximo  = c.CoMaximo,
	                                                                                    AlMinimo  = c.AlMinimo,
	                                                                                    AlMaximo  = c.AlMinimo,
	                                                                                    ZnMinimo  = c.ZnMinimo,
	                                                                                    ZnMaximo  = c.ZnMaximo,
	                                                                                    SnMinimo  = c.SnMinimo,
	                                                                                    SnMaximo  = c.SnMaximo,
	                                                                                    PbMinimo = c.PbMinimo, 
	                                                                                    PbMaximo = c.PbMaximo, 
	                                                                                    FeMinimo = c.FeMinimo,
	                                                                                    FeMaximo  = c.FeMaximo,
                                                                                        DataCadastro = c.DataCadastro, 
                                                                                        UsuarioInc = c.UsuarioInc
                                                                                         });
             _repositorioDataContext.ComposicaoMateriaPrimas.InsertAllOnSubmit(lstComposicaoMateriaPrima);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Alteracao
		public void Alterar(List<ComposicaoMateriaPrimaVO> composicaoMateriaPrimaVos, int codMateriaPrima)
		{
            var lstComposicaoMateriaPrima = composicaoMateriaPrimaVos.Where( c =>
                                                                             c.CodComposicaoMateriaPrima == 0).Select(c => new ComposicaoMateriaPrima()
		                                                                              {
                                                                                          CodMateriaPrima = codMateriaPrima,
		                                                                                  BitolaMinima = c.BitolaMinima,
		                                                                                  BitolaMaxima = c.BitolaMaxima,
		                                                                                  CMinimo = c.CMinimo,
		                                                                                  CMaximo = c.CMaximo,
		                                                                                  SiMinimo = c.SiMinimo,
		                                                                                  SiMaximo = c.SiMaximo,
		                                                                                  MnMinimo = c.MnMinimo,
		                                                                                  MnMaximo = c.MnMaximo,
		                                                                                  PMinimo = c.PMinimo,
		                                                                                  PMaximo = c.PMaximo,
		                                                                                  SMinimo = c.SMinimo,
		                                                                                  SMaximo = c.SMaximo,
		                                                                                  CrMinimo = c.CrMinimo,
		                                                                                  CrMaximo = c.CrMaximo,
		                                                                                  NiMinimo = c.NiMinimo,
		                                                                                  NiMaximo = c.NiMinimo,
		                                                                                  MoMinimo = c.MoMinimo,
		                                                                                  MoMaximo = c.MoMaximo,
		                                                                                  CuMinimo = c.CuMinimo,
		                                                                                  CuMaximo = c.CuMaximo,
		                                                                                  TiMinimo = c.TiMinimo,
		                                                                                  TiMaximo = c.TiMaximo,
		                                                                                  N2Minimo = c.N2Minimo,
		                                                                                  N2Maximo = c.N2Maximo,
		                                                                                  CoMinimo = c.CoMinimo,
		                                                                                  CoMaximo = c.CoMaximo,
		                                                                                  AlMinimo = c.AlMinimo,
		                                                                                  AlMaximo = c.AlMinimo,
		                                                                                  ZnMinimo = c.ZnMinimo,
		                                                                                  ZnMaximo = c.ZnMaximo,
		                                                                                  SnMinimo = c.SnMinimo,
		                                                                                  SnMaximo = c.SnMaximo,
		                                                                                  PbMinimo = c.PbMinimo,
		                                                                                  PbMaximo = c.PbMaximo,
		                                                                                  FeMinimo = c.FeMinimo,
		                                                                                  FeMaximo = c.FeMaximo,
		                                                                                  DataCadastro = c.DataCadastro,
		                                                                                  UsuarioInc = c.UsuarioInc
		                                                                              });
            if (lstComposicaoMateriaPrima.Count() > 0)
		    _repositorioDataContext.ComposicaoMateriaPrimas.InsertAllOnSubmit(lstComposicaoMateriaPrima);
            foreach (ComposicaoMateriaPrimaVO item in composicaoMateriaPrimaVos)
            {
                if (item.CodComposicaoMateriaPrima > 0)
                {
                    
                IQueryable<ComposicaoMateriaPrima> query = from c in _repositorioDataContext.ComposicaoMateriaPrimas
                                                 where c.CodComposicaoMateriaPrima == item.CodComposicaoMateriaPrima
                                                 select c;
                var composicaoMateriaPrima = query.FirstOrDefault();
                composicaoMateriaPrima.CodMateriaPrima = item.CodMateriaPrima;
                composicaoMateriaPrima.BitolaMinima = item.BitolaMinima;
                composicaoMateriaPrima.BitolaMaxima = item.BitolaMaxima;
                composicaoMateriaPrima.CMinimo = item.CMinimo;
                composicaoMateriaPrima.CMaximo = item.CMaximo;
                composicaoMateriaPrima.SiMinimo = item.SiMinimo;
                composicaoMateriaPrima.SiMaximo = item.SiMaximo;
                composicaoMateriaPrima.MnMinimo = item.MnMinimo;
                composicaoMateriaPrima.MnMaximo = item.MnMaximo;
                composicaoMateriaPrima.PMinimo = item.PMinimo;
                composicaoMateriaPrima.PMaximo = item.PMaximo;
                composicaoMateriaPrima.SMinimo = item.SMinimo;
                composicaoMateriaPrima.SMaximo = item.SMaximo;
                composicaoMateriaPrima. CrMinimo = item.CrMinimo;
                composicaoMateriaPrima.CrMaximo = item.CrMaximo;
                composicaoMateriaPrima.NiMinimo = item.NiMinimo;
                composicaoMateriaPrima.NiMaximo = item.NiMinimo;
                composicaoMateriaPrima.MoMinimo = item.MoMinimo;
                composicaoMateriaPrima.MoMaximo = item.MoMaximo;
                composicaoMateriaPrima.CuMinimo = item.CuMinimo;
                composicaoMateriaPrima.CuMaximo = item.CuMaximo;
                composicaoMateriaPrima.TiMinimo = item.TiMinimo;
                composicaoMateriaPrima.TiMaximo = item.TiMaximo;
                composicaoMateriaPrima.N2Minimo = item.N2Minimo;
                composicaoMateriaPrima.N2Maximo = item.N2Maximo;
                composicaoMateriaPrima.CoMinimo = item.CoMinimo;
                composicaoMateriaPrima.CoMaximo = item.CoMaximo;
                composicaoMateriaPrima.AlMinimo = item.AlMinimo;
                composicaoMateriaPrima.AlMaximo = item.AlMinimo;
                composicaoMateriaPrima.ZnMinimo = item.ZnMinimo;
                composicaoMateriaPrima.ZnMaximo = item.ZnMaximo;
                composicaoMateriaPrima.SnMinimo = item.SnMinimo;
                composicaoMateriaPrima.SnMaximo = item.SnMaximo;
                composicaoMateriaPrima.PbMinimo = item.PbMinimo;
                composicaoMateriaPrima.PbMaximo = item.PbMaximo;
                composicaoMateriaPrima.FeMinimo = item.FeMinimo;
                composicaoMateriaPrima.FeMaximo = item.FeMaximo;
                composicaoMateriaPrima.DataAlteracao = item.DataCadastro;
                composicaoMateriaPrima.UsuarioAlt = item.UsuarioInc;
                }

            }
		    _repositorioDataContext.SubmitChanges();

		} 
    	#endregion
        #region Método de Exclusao
        public void Excluir(int codComposicaoMateriaPrima)
        {
            IQueryable<ComposicaoMateriaPrima> query = from c in _repositorioDataContext.ComposicaoMateriaPrimas
                                                  where c.CodComposicaoMateriaPrima == codComposicaoMateriaPrima
                                                  select c;
            var composicaoMateriaPrima = query.FirstOrDefault();
            _repositorioDataContext.ComposicaoMateriaPrimas.DeleteOnSubmit(composicaoMateriaPrima);
            _repositorioDataContext.SubmitChanges();
        }
        public void ExcluirTodos(int codMateriaPrima)
        {
            IQueryable<ComposicaoMateriaPrima> query = from c in _repositorioDataContext.ComposicaoMateriaPrimas
                                                       where c.CodMateriaPrima == codMateriaPrima
                                                       select c;
            if (query.Count() > 0)
            {
                var lstcomposicaoMateriaPrima = query.ToList();
                _repositorioDataContext.ComposicaoMateriaPrimas.DeleteAllOnSubmit(lstcomposicaoMateriaPrima);
                _repositorioDataContext.SubmitChanges();
            }
        }
        #endregion
    }
}

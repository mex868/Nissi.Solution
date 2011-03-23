using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio.Factory
{
    public class ProdutoMateriaPrima: IProduto
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public ProdutoMateriaPrima()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }   
   
        public string GetNameProduct(int codigo)
        {
            string descricao = string.Empty;
                var query = from m in _repositorioDataContext.MateriaPrimas
                            join n in _repositorioDataContext.Normas
                                on m.CodNorma equals (n.CodNorma)
                            where m.CodMateriaPrima == codigo
                            select new MateriaPrimaVO()
                                       {
                                           ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                           where ct.CodClasseTipo == m.CodClasseTipo
                                                           select new ClasseTipoVO()
                                                                      {
                                                                          Descricao = ct.Descricao
                                                                      }).FirstOrDefault(),
                                           NormaVo = new NormaVO()
                                                         {
                                                             Descricao = n.Descricao,
                                                             Revisao = n.Revisao
                                                         }
                                       };
                var materiaPrima = new MateriaPrimaVO();
                if (query.Count() > 0)
                    materiaPrima = query.FirstOrDefault();
                        descricao = materiaPrima.NormaVo.Descricao + "/" + materiaPrima.NormaVo.Revisao +
                                    (materiaPrima.ClasseTipoVo != null ? materiaPrima.ClasseTipoVo.Descricao : string.Empty);
            return descricao;
        }
    }
}

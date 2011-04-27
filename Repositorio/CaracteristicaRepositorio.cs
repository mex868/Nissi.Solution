using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Repositorio
{
    public class CaracteristicaRepositorio: ICaracteristica
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public CaracteristicaRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }


        public List<Model.CaracteristicaVO> Listar()
        {
            var queryitem = from c in _repositorioDataContext.Caracteristicas
                            select new Model.CaracteristicaVO()
                                       {
                                           CodCaracteristica = c.CodCaracteristica,
                                           Descricao = c.Descricao,
                                           Tipo = c.Tipo,
                                       };
            var lstCaracteristica = new List<Model.CaracteristicaVO>();
            if (queryitem.Count() > 0)
                lstCaracteristica = queryitem.ToList();
            return lstCaracteristica;
        }

        public Model.CaracteristicaVO ListarPorCodigo(int codigo)
        {
            var queryitem = from c in _repositorioDataContext.Caracteristicas
                            where c.CodCaracteristica == codigo
                            select new Model.CaracteristicaVO()
                            {
                                CodCaracteristica = c.CodCaracteristica,
                                Descricao = c.Descricao,
                                Tipo = c.Tipo,
                            };
            var caracteristicaVos = new Model.CaracteristicaVO();
            if (queryitem.Count() > 0)
                caracteristicaVos = queryitem.FirstOrDefault();
            return caracteristicaVos;
        }

        public List<Model.CaracteristicaVO> ListarPorDescricao(string descricao)
        {
            var queryitem = from c in _repositorioDataContext.Caracteristicas
                            where c.Descricao.Contains(descricao)
                            select new Model.CaracteristicaVO()
                            {
                                CodCaracteristica = c.CodCaracteristica,
                                Descricao = c.Descricao,
                                Tipo = c.Tipo,
                            };
            var lstCaracteristica = new List<Model.CaracteristicaVO>();
            if (queryitem.Count() > 0)
                lstCaracteristica = queryitem.ToList();
            return lstCaracteristica;
        }

        public int Incluir(string descricao, short? tipo, int codUsuarioInc)
        {
            var caracteristica = new Caracteristica()
                                     {
                                         Descricao = descricao,
                                         Tipo = tipo,
                                         DataCadastro = DateTime.Now,
                                         UsuarioInc = codUsuarioInc
                                     };
            _repositorioDataContext.Caracteristicas.InsertOnSubmit(caracteristica);
            _repositorioDataContext.SubmitChanges();
            return caracteristica.CodCaracteristica;
        }

        public void Alterar(int codigo, string descricao, short? tipo, int codUsuarioAlt)
        {
            IQueryable<Caracteristica> query = from c in _repositorioDataContext.Caracteristicas
                                                       where c.CodCaracteristica == codigo
                                                       select c;
            var identCaracteristica = query.FirstOrDefault();
            identCaracteristica.CodCaracteristica = codigo;
            identCaracteristica.Descricao = descricao;
            identCaracteristica.Tipo = tipo;
            identCaracteristica.UsuarioAlt = codUsuarioAlt;
            identCaracteristica.DataAlteracao = DateTime.Now;
            _repositorioDataContext.SubmitChanges();
        }

        public void Excluir(int codigo, int codUsuarioInc)
        {
            IQueryable<Caracteristica> query = from c in _repositorioDataContext.Caracteristicas
                                               where c.CodCaracteristica == codigo
                                               select c;
            var identCaracteristica = query.FirstOrDefault();
            _repositorioDataContext.Caracteristicas.DeleteOnSubmit(identCaracteristica);
            _repositorioDataContext.SubmitChanges();

        }
    }
}

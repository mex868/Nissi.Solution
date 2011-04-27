using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Repositorio
{
    public class ProcessoRepositorio: IProcesso
    {
        
        private readonly RepositorioDataContext _repositorioDataContext;
        public ProcessoRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        
        public List<Model.ProcessoVO> Listar()
        {
            var queryitem = from c in _repositorioDataContext.Processos
                            select new Model.ProcessoVO()
                            {
                                CodProcesso = c.CodProcesso,
                                Descricao = c.Descricao,
                            };
            var lstProcesso = new List<Model.ProcessoVO>();
            if (queryitem.Count() > 0)
                lstProcesso = queryitem.ToList();
            return lstProcesso;           
        }

        public Model.ProcessoVO ListarPorCodigo(int codigo)
        {
            var queryitem = from c in _repositorioDataContext.Processos
                            where c.CodProcesso == codigo
                            select new Model.ProcessoVO()
                            {
                                CodProcesso = c.CodProcesso,
                                Descricao = c.Descricao,
                            };
            var ProcessoVos = new Model.ProcessoVO();
            if (queryitem.Count() > 0)
                ProcessoVos = queryitem.FirstOrDefault();
            return ProcessoVos;
        }

        public List<Model.ProcessoVO> ListarPorDescricao(string descricao)
        {
            var queryitem = from c in _repositorioDataContext.Processos
                            where c.Descricao.Contains(descricao)
                            select new Model.ProcessoVO()
                            {
                                CodProcesso = c.CodProcesso,
                                Descricao = c.Descricao,
                            };
            var lstProcesso = new List<Model.ProcessoVO>();
            if (queryitem.Count() > 0)
                lstProcesso = queryitem.ToList();
            return lstProcesso;
        }

        public int Incluir(string descricao,  int codUsuarioInc)
        {
            var Processo = new Processo()
            {
                Descricao = descricao,
                DataCadastro = DateTime.Now,
                UsuarioInc = codUsuarioInc
            };
            _repositorioDataContext.Processos.InsertOnSubmit(Processo);
            _repositorioDataContext.SubmitChanges();
            return Processo.CodProcesso;
        }

        public void Alterar(int codigo, string descricao,  int codUsuarioAlt)
        {
            IQueryable<Processo> query = from c in _repositorioDataContext.Processos
                                               where c.CodProcesso == codigo
                                               select c;
            var identProcesso = query.FirstOrDefault();
            identProcesso.CodProcesso = codigo;
            identProcesso.Descricao = descricao;
            identProcesso.UsuarioAlt = codUsuarioAlt;
            identProcesso.DataAlteracao = DateTime.Now;
            _repositorioDataContext.SubmitChanges();
        }

        public void Excluir(int codigo, int codUsuarioInc)
        {
            IQueryable<Processo> query = from c in _repositorioDataContext.Processos
                                               where c.CodProcesso == codigo
                                               select c;
            var identProcesso = query.FirstOrDefault();
            _repositorioDataContext.Processos.DeleteOnSubmit(identProcesso);
            _repositorioDataContext.SubmitChanges();
        }
    }
}

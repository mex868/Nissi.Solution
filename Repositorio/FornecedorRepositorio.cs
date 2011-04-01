using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class FornecedorRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public FornecedorRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }

        public List<FornecedorVO> ListarPorBitola(int codBitola)
        {
            var queryitem = (from i in _repositorioDataContext.ItemPedidoCompras
                             join p in _repositorioDataContext.PedidoCompras
                                 on i.CodPedidoCompra equals p.CodPedidoCompra
                             join f in _repositorioDataContext.Pessoas
                                 on p.CodFornecedor equals f.CodPessoa
                             join fo in _repositorioDataContext.Fornecedors
                                on f.CodPessoa equals fo.CodPessoa
                             where i.CodBitola == codBitola
                             select new FornecedorVO()
                             {
                                 CodPessoa = f.CodPessoa,
                                 CodFornecedor = fo.CodFornecedor,
                                 RazaoSocial = f.RazaoSocial,
                                 NomeFantasia = f.NomeFantasia,
                                 CNPJ = f.CNPJ,
                                 InscricaoEstadual = f.InscricaoEstadual,
                                 Telefone = f.Telefone
                             }).Distinct().ToList();

            var lstFornecedorVO = new List<FornecedorVO>();
            if (queryitem.Count() > 0)
                lstFornecedorVO = queryitem.ToList();
            return lstFornecedorVO;
        }

        public List<FornecedorVO> ListarPorClasseTipo(int codMateriaPrima)
        {
            var queryitem = (from i in _repositorioDataContext.ItemPedidoCompras
                             join p in _repositorioDataContext.PedidoCompras
                                 on i.CodPedidoCompra equals p.CodPedidoCompra
                             join f in _repositorioDataContext.Pessoas
                                 on p.CodFornecedor equals f.CodPessoa
                             join fo in _repositorioDataContext.Fornecedors
                                on f.CodPessoa equals fo.CodPessoa
                             join ma in _repositorioDataContext.MateriaPrimas
                                on i.CodMateriaPrima equals ma.CodMateriaPrima
                             where ma.CodMateriaPrima == codMateriaPrima
                             select new FornecedorVO()
                             {
                                 CodPessoa = f.CodPessoa,
                                 CodFornecedor = fo.CodFornecedor,
                                 RazaoSocial = f.RazaoSocial,
                                 NomeFantasia = f.NomeFantasia,
                                 CNPJ = f.CNPJ,
                                 InscricaoEstadual = f.InscricaoEstadual,
                                 Telefone = f.Telefone
                             }).Distinct().ToList();

            var lstFornecedorVO = new List<FornecedorVO>();
            if (queryitem.Count() > 0)
                lstFornecedorVO = queryitem.ToList();
            return lstFornecedorVO;
        }
    }
}

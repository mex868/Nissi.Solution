using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class ClassificacaoFiscal
    {
        public List<ClassificacaoFiscalVO> Listar(ClassificacaoFiscalVO identClassificacaoFiscal)
        {
            return new ClassificacaoFiscalData().Listar(identClassificacaoFiscal);
        }
        public int Incluir(ClassificacaoFiscalVO identClassificacaoFiscal)
        {
           return new ClassificacaoFiscalData().Incluir(identClassificacaoFiscal);
        }
        public void Alterar(ClassificacaoFiscalVO identClassificacaoFiscal)
        {
            new ClassificacaoFiscalData().Alterar(identClassificacaoFiscal);
        }
        public void Excluir(ClassificacaoFiscalVO identClassificacaoFiscal)
        {
            new ClassificacaoFiscalData().Excluir(identClassificacaoFiscal);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class UnidadeFederacao: NissiBaseBusiness
    {
        #region Métodos de Listagem
        public List<UnidadeFederacaoVO> Listar()
        {
            return new UnidadeFederacaoData().Listar();
        }
        #endregion
    }
}

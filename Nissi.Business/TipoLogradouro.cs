using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class TipoLogradouro : NissiBaseBusiness
    {
        #region Método de Listagen
        public List<TipoLogradouroVO> Listar()
        {
           return new TipoLogradouroData().Listar();
        }
        #endregion
    }
}

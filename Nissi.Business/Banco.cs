using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
   public class Banco: NissiBaseBusiness
   {
       #region Métodos de Listagem
       /// <summary>
       /// Lista os bancos cadastrados
       /// </summary>
       /// <returns></returns>
       public List<BancoVO> Listar()
       {
           return new BancoData().Lista();
       }

       #endregion
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable]
    public class NissiBaseVO
    {
        /// <summary>
        /// Utilizado na tela de Acesso Direto ao Banco
        /// </summary>
        public enum enTipoExecucaoProc
        {
            Select = 1,
            ExecProc = 2,
            InsUpdDel = 3
        }
    }
}

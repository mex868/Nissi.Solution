using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class Cidade: NissiBaseBusiness
    {
        /// <summary>
        /// Listagem de Cidades por UF.
        /// Objeto/Parâmetros: (identUF)
        /// Valores: identUF.CodUF
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        /// <param name="identUF">identUF.CodUF(opcional)</param>
        /// <returns></returns>
        public List<CidadeVO> Listar(UnidadeFederacaoVO identUF)
        {
            return new CidadeData().Lista(identUF);
        }
    }
}

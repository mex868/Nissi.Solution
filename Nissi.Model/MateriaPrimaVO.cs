using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela MateriaPrima
    /// </summary>
// ReSharper disable InconsistentNaming
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class MateriaPrimaVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

        public MateriaPrimaVO()
        {
            ClasseTipoVo = new ClasseTipoVO();
            NormaVo = new NormaVO();
            DataCadastro = DateTime.Now;
            DataAlteracao = null;
            UsuarioAlt = null;
            UsuarioInc = 1;
            CodMateriaPrima = 0;
            ComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>();
            ResistenciaTracaoVos = new List<ResistenciaTracaoVO>();
        }

        #endregion
        #region Propriedades

        public string DescricaoInsumo { get; set; }
        public int CodMateriaPrima { get; set; }

        public NormaVO NormaVo { get; set; }

        public ClasseTipoVO ClasseTipoVo { get; set; }

        public string Descricao
        {
            get
            {
                string descricao = NormaVo.Descricao + "/" + NormaVo.Revisao;
                if (!string.IsNullOrEmpty(DescricaoInsumo))
                    descricao = DescricaoInsumo;
                if (ClasseTipoVo != null)
                    descricao += ClasseTipoVo.Descricao;
                return descricao;
            }
        }

        public DateTime DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }
        public List<ComposicaoMateriaPrimaVO> ComposicaoMateriaPrimaVos { get; set; }
        public List<ResistenciaTracaoVO> ResistenciaTracaoVos { get; set; }
        #endregion

        public override string ToString()
        {
            return "MateriaPrimaVO: " + CodMateriaPrima.ToString();
        }

    }
}


// ------------------------------------------------------------------------- // 



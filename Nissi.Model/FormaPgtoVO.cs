using System;

namespace Nissi.Model
{
/// <summary>
/// Summary description for Class1
/// </summary>
// ReSharper disable InconsistentNaming
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class FormaPgtoVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

    #endregion

    #region Propriendades

    public short CodFormaPgto { get; set; }

    public string Descricao { get; set; }

    public short? Parcelas { get; set; }

    public DateTime DataCadastro { get; set; }

    public short? Intervalo { get; set; }

    public int? UsuarioInc { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public int? UsuarioAlt { get; set; }

    #endregion
    }
}

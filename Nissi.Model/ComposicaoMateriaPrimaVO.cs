using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela ComposicaoMateriaPrima
    /// </summary>
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class ComposicaoMateriaPrimaVO
    {
        #region Campos
        private int _codComposicaoMateriaPrima = 0;
        private int _codMateriaPrima = 0;
        private decimal? _bitolaMinima = null;
        private decimal? _bitolaMaxima = null;
        private decimal? _cMinimo = null;
        private decimal? _cMaximo = null;
        private decimal? _siMinimo = null;
        private decimal? _siMaximo = null;
        private decimal? _mnMinimo = null;
        private decimal? _mnMaximo = null;
        private decimal? _pMinimo = null;
        private decimal? _pMaximo = null;
        private decimal? _sMinimo = null;
        private decimal? _sMaximo = null;
        private decimal? _crMinimo = null;
        private decimal? _crMaximo = null;
        private decimal? _niMinimo = null;
        private decimal? _niMaximo = null;
        private decimal? _moMinimo = null;
        private decimal? _moMaximo = null;
        private decimal? _cuMinimo = null;
        private decimal? _cuMaximo = null;
        private decimal? _tiMinimo = null;
        private decimal? _tiMaximo = null;
        private decimal? _n2Minimo = null;
        private decimal? _n2Maximo = null;
        private decimal? _coMinimo = null;
        private decimal? _coMaximo = null;
        private decimal? _alMinimo = null;
        private decimal? _alMaximo = null;
        private decimal? _znMinimo = null;
        private decimal? _znMaximo = null;
        private decimal? _snMinimo = null;
        private decimal? _snMaximo = null;
        private decimal? _pbMinimo = null;
        private decimal? _pbMaximo = null;
        private decimal? _feMinimo = null;
        private decimal? _feMaximo = null;
        private DateTime _dataCadastro = DateTime.Now;
        private int _usuarioInc = 1;
        private DateTime? _dataAlteracao = DateTime.Now;
        private int? _usuarioAlt = 1;
        #endregion
        #region Propriedades
        public int CodComposicaoMateriaPrima
        {
            get { return _codComposicaoMateriaPrima; }
            set { _codComposicaoMateriaPrima = value; }
        }

        public int CodMateriaPrima
        {
            get { return _codMateriaPrima; }
            set { _codMateriaPrima = value; }
        }

        public decimal? BitolaMinima
        {
            get { return _bitolaMinima; }
            set { _bitolaMinima = value; }
        }

        public decimal? BitolaMaxima
        {
            get { return _bitolaMaxima; }
            set { _bitolaMaxima = value; }
        }

        public decimal? CMinimo
        {
            get { return _cMinimo; }
            set { _cMinimo = value; }
        }

        public decimal? CMaximo
        {
            get { return _cMaximo; }
            set { _cMaximo = value; }
        }

        public decimal? SiMinimo
        {
            get { return _siMinimo; }
            set { _siMinimo = value; }
        }

        public decimal? SiMaximo
        {
            get { return _siMaximo; }
            set { _siMaximo = value; }
        }

        public decimal? MnMinimo
        {
            get { return _mnMinimo; }
            set { _mnMinimo = value; }
        }

        public decimal? MnMaximo
        {
            get { return _mnMaximo; }
            set { _mnMaximo = value; }
        }

        public decimal? PMinimo
        {
            get { return _pMinimo; }
            set { _pMinimo = value; }
        }

        public decimal? PMaximo
        {
            get { return _pMaximo; }
            set { _pMaximo = value; }
        }

        public decimal? SMinimo
        {
            get { return _sMinimo; }
            set { _sMinimo = value; }
        }

        public decimal? SMaximo
        {
            get { return _sMaximo; }
            set { _sMaximo = value; }
        }

        public decimal? CrMinimo
        {
            get { return _crMinimo; }
            set { _crMinimo = value; }
        }

        public decimal? CrMaximo
        {
            get { return _crMaximo; }
            set { _crMaximo = value; }
        }

        public decimal? NiMinimo
        {
            get { return _niMinimo; }
            set { _niMinimo = value; }
        }

        public decimal? NiMaximo
        {
            get { return _niMaximo; }
            set { _niMaximo = value; }
        }

        public decimal? MoMinimo
        {
            get { return _moMinimo; }
            set { _moMinimo = value; }
        }

        public decimal? MoMaximo
        {
            get { return _moMaximo; }
            set { _moMaximo = value; }
        }

        public decimal? CuMinimo
        {
            get { return _cuMinimo; }
            set { _cuMinimo = value; }
        }

        public decimal? CuMaximo
        {
            get { return _cuMaximo; }
            set { _cuMaximo = value; }
        }

        public decimal? TiMinimo
        {
            get { return _tiMinimo; }
            set { _tiMinimo = value; }
        }

        public decimal? TiMaximo
        {
            get { return _tiMaximo; }
            set { _tiMaximo = value; }
        }

        public decimal? N2Minimo
        {
            get { return _n2Minimo; }
            set { _n2Minimo = value; }
        }

        public decimal? N2Maximo
        {
            get { return _n2Maximo; }
            set { _n2Maximo = value; }
        }

        public decimal? CoMinimo
        {
            get { return _coMinimo; }
            set { _coMinimo = value; }
        }

        public decimal? CoMaximo
        {
            get { return _coMaximo; }
            set { _coMaximo = value; }
        }

        public decimal? AlMinimo
        {
            get { return _alMinimo; }
            set { _alMinimo = value; }
        }

        public decimal? AlMaximo
        {
            get { return _alMaximo; }
            set { _alMaximo = value; }
        }

        public decimal? ZnMinimo
        {
            get { return _znMinimo; }
            set { _znMinimo = value; }
        }

        public decimal? ZnMaximo
        {
            get { return _znMaximo; }
            set { _znMaximo = value; }
        }

        public decimal? SnMinimo
        {
            get { return _snMinimo; }
            set { _snMinimo = value; }
        }

        public decimal? SnMaximo
        {
            get { return _snMaximo; }
            set { _snMaximo = value; }
        }

        public decimal? PbMinimo
        {
            get { return _pbMinimo; }
            set { _pbMinimo = value; }
        }

        public decimal? PbMaximo
        {
            get { return _pbMaximo; }
            set { _pbMaximo = value; }
        }

        public decimal? FeMinimo
        {
            get { return _feMinimo; }
            set { _feMinimo = value; }
        }

        public decimal? FeMaximo
        {
            get { return _feMaximo; }
            set { _feMaximo = value; }
        }

        public DateTime DataCadastro
        {
            get { return _dataCadastro; }
            set { _dataCadastro = value; }
        }

        public int UsuarioInc
        {
            get { return _usuarioInc; }
            set { _usuarioInc = value; }
        }

        public DateTime? DataAlteracao
        {
            get { return _dataAlteracao; }
            set { _dataAlteracao = value; }
        }

        public int? UsuarioAlt
        {
            get { return _usuarioAlt; }
            set { _usuarioAlt = value; }
        }
        #endregion

        #region ToString()
        public override string ToString()
        {
            return "ComposicaoMateriaPrimaVO: " +CodComposicaoMateriaPrima.ToString();
        }
        #endregion
    }
}





 // ------------------------------------------------------------------------- // 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable] //deve ser serializavel para armazenar em viewstate
    public class EmitenteVO
    {
        #region Variáveis
        private int? _codEmitente = null;
        private string _razaoSocial = string.Empty;
        private string _nomeFantasia = string.Empty;
        private string _cnpj = string.Empty;
        private string _inscricaoEstadual = string.Empty;
        private string _cnae = string.Empty;
        private string _inscricaoMunicipal = string.Empty;
        private string _inscricaoEstadualSub = string.Empty;
        private CEPVO _cep;
        private string _telefone = string.Empty;
        private string _logradouro = string.Empty;
        private int? _numero = null;
        private string _complemento = string.Empty;
        private string _pais = string.Empty;
        private byte[] _image = null;
        private string _fax = string.Empty;
        private string _email = string.Empty;
        #endregion

        #region Propriedades
        public byte[] Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public int? CodEmitente
        {
            get { return _codEmitente; }
            set { _codEmitente = value; }
        }

        public string RazaoSocial
        {
            get { return _razaoSocial; }
            set { _razaoSocial = value; }
        }

        public string NomeFantasia
        {
            get { return _nomeFantasia; }
            set { _nomeFantasia = value; }
        }
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }
        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set { _inscricaoEstadual = value; }
        }
        public string CNAE
        {
            get { return _cnae; }
            set { _cnae = value; }
        }
        public string InscricaoMunicipal
        {
            get { return _inscricaoMunicipal; }
            set { _inscricaoMunicipal = value; }
        }
        public string InscricaoEstadualSub
        {
            get { return _inscricaoEstadualSub; }
            set { _inscricaoEstadualSub = value; }
        }
        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }
        public CEPVO Cep
        {
            get
            {
                if (_cep == null)
                    _cep = new CEPVO();
                return _cep;
            }
            set { _cep = value; }
        }
        public string Logradouro
        {
            get { return _logradouro; }
            set { _logradouro = value; }
        }
        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }
        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
        public int? Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        #endregion
    }
}


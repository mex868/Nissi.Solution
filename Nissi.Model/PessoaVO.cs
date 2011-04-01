using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
	/// Essa classe representa os campos da tabela Pessoa
	/// </summary>
    [Serializable()] //deve ser serializavel para armazenar em viewstate
	public class PessoaVO
	{	
		#region Campos
		private int _codPessoa = 0;
        private string _codRef = string.Empty;
		private string _razaoSocial = String.Empty;
		private string _nomeFantasia = String.Empty;
		private string _tipo = String.Empty;
		private string _cNPJ = String.Empty;
		private string _inscricaoEstadual = String.Empty;
		private string _numero = String.Empty;
		private string _complemento = String.Empty;
		private string _telefone = String.Empty;
		private string _fax = String.Empty;
		private string _celular = String.Empty;
		private string _contato = String.Empty;
		private string _email = String.Empty;
		private string _site = String.Empty;
		private string _observacao = String.Empty;
		private DateTime? _dataCadastro = null;
		private int? _usuarioInc = null;
		private DateTime? _dataAlteracao = null;
		private int? _usuarioAlt = null;
		private bool? _ativo = null;
        private bool? _indPessoaTipo = null;
        private CEPVO _cep;
        private string _emailNFE = string.Empty;
        private string _cepCobranca = string.Empty;
        private string _enderecoCobranca = string.Empty;
		#endregion	
		#region Propriedades
		public int CodPessoa
		{
			get {return _codPessoa;}
			set {_codPessoa = value;}
		}

        public string CodRef
        {
            get { return _codRef; }
            set { _codRef = value; }
        }

		public string RazaoSocial
		{
			get {return _razaoSocial;}
			set {_razaoSocial = value;}
		}

		public string NomeFantasia
		{
			get {return _nomeFantasia;}
			set {_nomeFantasia = value;}
		}

		public string Tipo
		{
			get {return _tipo;}
			set {_tipo = value;}
		}

		public string CNPJ
		{
			get {return _cNPJ;}
			set {_cNPJ = value;}
		}

		public string InscricaoEstadual
		{
			get {return _inscricaoEstadual;}
			set {_inscricaoEstadual = value;}
		}

		public string Numero
		{
			get {return _numero;}
			set {_numero = value;}
		}

		public string Complemento
		{
			get {return _complemento;}
			set {_complemento = value;}
		}

		public string Telefone
		{
			get {return _telefone;}
			set {_telefone = value;}
		}

		public string Fax
		{
			get {return _fax;}
			set {_fax = value;}
		}

		public string Celular
		{
			get {return _celular;}
			set {_celular = value;}
		}

		public string Contato
		{
			get {return _contato;}
			set {_contato = value;}
		}

		public string Email
		{
			get {return _email;}
			set {_email = value;}
		}

		public string Site
		{
			get {return _site;}
			set {_site = value;}
		}

		public string Observacao
		{
			get {return _observacao;}
			set {_observacao = value;}
		}

		public DateTime? DataCadastro
		{
			get {return _dataCadastro;}
			set {_dataCadastro = value;}
		}

		public int? UsuarioInc
		{
			get {return _usuarioInc;}
			set {_usuarioInc = value;}
		}

		public DateTime? DataAlteracao
		{
			get {return _dataAlteracao;}
			set {_dataAlteracao = value;}
		}

		public int? UsuarioAlt
		{
			get {return _usuarioAlt;}
			set {_usuarioAlt = value;}
		}

		public bool? Ativo
		{
			get {return _ativo;}
			set {_ativo = value;}
		}
        
        public bool? IndPessoaTipo
        {
            get { return _indPessoaTipo; }
            set { _indPessoaTipo = value; }
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
        public string EmailNFE
        {
            get { return _emailNFE; }
            set { _emailNFE = value; }
        }
        public string CepCobranca
        {
            get { return _cepCobranca; }
            set { _cepCobranca = value; }
        }
        public string EnderecoCobranca
        {
            get { return _enderecoCobranca; }
            set { _enderecoCobranca = value; }
        }
		#endregion
		
		/// <summary>
		/// Retorna um texto que identifique essa instância da classe
		/// </summary>
		#region ToString()
		public override string ToString()
		{
			return "PessoaVO: " + CodPessoa.ToString();
		}			
		#endregion
	}
}


 // ------------------------------------------------------------------------- // 


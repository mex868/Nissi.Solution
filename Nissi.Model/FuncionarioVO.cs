using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable] //deve ser serializavel para armazenar em viewstate
    public class FuncionarioVO : PessoaVO
    {
        #region Campos
        private int? _codFuncionario;
        private DateTime? _dataAdmissao;
        private DateTime? _dataDemissao;
        private bool _acessaSistema;
        private string _login;
        private byte[] _senha;
        private CargoVO _cargo;
        private DepartamentoVO _departamento;
        private List<PerfilAcessoVO> _perfils;
        private bool _modoDesenvolvedor;
        private BancoVO _banco;

        #endregion

        #region Propriedades
        public int? CodFuncionario
        {
            get { return _codFuncionario; }
            set { _codFuncionario = value; }
        }
        public string CPF
        {
            get { return CNPJ; }
            set { CNPJ = value; }
        }

        public string RG
        {
            get { return InscricaoEstadual; }
            set { InscricaoEstadual = value; }
        }
        public string Nome
        {
            get { return RazaoSocial; }
            set { RazaoSocial = value; }
        }
        public string Apelido
        {
            get { return NomeFantasia; }
            set { NomeFantasia = value; }
        }
        public DateTime? DataAdmissao
        {
            get { return _dataAdmissao; }
            set { _dataAdmissao = value; }
        }
        public DateTime? DataDemissao
        {
            get { return _dataDemissao; }
            set { _dataDemissao = value; }
        }
        public bool AcessaSistema
        {
            get { return _acessaSistema; }
            set { _acessaSistema = value; }
        }
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        public byte[] Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }
        public CargoVO Cargo
        {
            get
            {
                if (_cargo == null)
                    _cargo = new CargoVO();
                    return _cargo; }
            set { _cargo = value; }
        }
        public DepartamentoVO Departamento
        {
            get
            {
                if (_departamento == null)
                    _departamento = new DepartamentoVO();
                return _departamento; }
            set { _departamento = value; }
        }
        public List<PerfilAcessoVO> Perfils
        {
            get
            {
                if (_perfils == null)
                    _perfils = new List<PerfilAcessoVO>();
                    return _perfils; 
            }
            set { _perfils = value; }
        }
        public BancoVO Banco
        {
            get{
                if (_banco == null)
                    _banco = new BancoVO();
                return _banco;
            }
            set {_banco = value;}
        }
        public bool ModoDesenvolvedor
        {
            get { return _modoDesenvolvedor; }
            set { _modoDesenvolvedor = value; }
        }
        #endregion

        /// <summary>
        /// Retorna um texto que identifique essa instância da classe
        /// </summary>
        #region ToString()
        public override string ToString()
        {
            return "FuncionarioVO: " + CodFuncionario.ToString();
        }
        #endregion
    }
}

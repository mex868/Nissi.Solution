using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class ParametroVO
    {

        private string _modelo;

        public string Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }
        private string _serie;

        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }
        private string _dataPacket;

        public string DataPacket
        {
            get { return _dataPacket; }
            set { _dataPacket = value; }
        }
        private string _schemas;

        public string Schemas
        {
            get { return _schemas; }
            set { _schemas = value; }
        }
        private string _unidadeFederada;
        public string UnidadeFederada
        {
            get { return _unidadeFederada; }
            set { _unidadeFederada = value; }
        }
        private string _cnpj;
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }

        private string _danfeLogo;

        public string DanfeLogo
        {
            get { return _danfeLogo; }
            set { _danfeLogo = value; }
        }
        private string _ambiente;

        public string Ambiente
        {
            get { return _ambiente; }
            set { _ambiente = value; }
        }
        private string _noSerieCertificado;

        public string NoSerieCertificado
        {
            get { return _noSerieCertificado; }
            set { _noSerieCertificado = value; }
        }
        private string _ativaTrace;

        public string AtivaTrace
        {
            get { return _ativaTrace; }
            set { _ativaTrace = value; }
        }
        private string _verProc;

        public string VerProc
        {
            get { return _verProc; }
            set { _verProc = value; }
        }
        private string _nFeRecepcao;

        public string NFeRecepcao
        {
            get { return _nFeRecepcao; }
            set { _nFeRecepcao = value; }
        }
        private string _nFeRetRecepcao;

        public string NFeRetRecepcao
        {
            get { return _nFeRetRecepcao; }
            set { _nFeRetRecepcao = value; }
        }
        private string _nFeCancelamento;

        public string NFeCancelamento
        {
            get { return _nFeCancelamento; }
            set { _nFeCancelamento = value; }
        }
        private string _nFeInutilizacao;

        public string NFeInutilizacao
        {
            get { return _nFeInutilizacao; }
            set { _nFeInutilizacao = value; }
        }
        private string _nFeConsultaProtocolo;

        public string NFeConsultaProtocolo
        {
            get { return _nFeConsultaProtocolo; }
            set { _nFeConsultaProtocolo = value; }
        }
        private string _nFeStatusServico;

        public string NFeStatusServico
        {
            get { return _nFeStatusServico; }
            set { _nFeStatusServico = value; }
        }
        private string _danfeInfo;

        public string DanfeInfo
        {
            get { return _danfeInfo; }
            set { _danfeInfo = value; }
        }
        private string _pathPrincipal;

        public string PathPrincipal
        {
            get { return _pathPrincipal; }
            set { _pathPrincipal = value; }
        }
        private string _tipoDanfe;

        public string TipoDanfe
        {
            get { return _tipoDanfe; }
            set { _tipoDanfe = value; }
        }
        private string _totalizarCfop;

        public string TotalizarCfop
        {
            get { return _totalizarCfop; }
            set { _totalizarCfop = value; }
        }
        private string _dataPacketFormSeg;

        public string DataPacketFormSeg
        {
            get { return _dataPacketFormSeg; }
            set { _dataPacketFormSeg = value; }
        }
    }

}

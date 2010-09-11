﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nissi.WinFormsApplication.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ListarNotaFiscal", ReplyAction="http://tempuri.org/IService1/ListarNotaFiscalResponse")]
        Nissi.Model.NotaFiscalVO[] ListarNotaFiscal(Nissi.Model.NotaFiscalVO identNotaFiscal);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ListarParametro", ReplyAction="http://tempuri.org/IService1/ListarParametroResponse")]
        Nissi.Model.ParametroVO[] ListarParametro();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/IncluirParametro", ReplyAction="http://tempuri.org/IService1/IncluirParametroResponse")]
        void IncluirParametro(Nissi.Model.ParametroVO identParametro);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AlterarParametro", ReplyAction="http://tempuri.org/IService1/AlterarParametroResponse")]
        void AlterarParametro(Nissi.Model.ParametroVO identParametro);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ExcluirParametro", ReplyAction="http://tempuri.org/IService1/ExcluirParametroResponse")]
        void ExcluirParametro();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ListarNfe", ReplyAction="http://tempuri.org/IService1/ListarNfeResponse")]
        Nissi.Model.NfeVO[] ListarNfe(System.Nullable<int> codNF);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/IncluirNfe", ReplyAction="http://tempuri.org/IService1/IncluirNfeResponse")]
        void IncluirNfe(Nissi.Model.NfeVO identNFe, System.Nullable<int> codUsuarioInc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AlterarNfe", ReplyAction="http://tempuri.org/IService1/AlterarNfeResponse")]
        void AlterarNfe(Nissi.Model.NfeVO identNfe, System.Nullable<int> codUsuarioAlt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ExcluirNfe", ReplyAction="http://tempuri.org/IService1/ExcluirNfeResponse")]
        void ExcluirNfe(System.Nullable<int> codNF);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GerarLote", ReplyAction="http://tempuri.org/IService1/GerarLoteResponse")]
        int GerarLote(Nissi.Model.LoteVO identLote);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GravarReciboNFe", ReplyAction="http://tempuri.org/IService1/GravarReciboNFeResponse")]
        void GravarReciboNFe(Nissi.Model.NfeVO identNFe);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GravarStatusNFe", ReplyAction="http://tempuri.org/IService1/GravarStatusNFeResponse")]
        void GravarStatusNFe(Nissi.Model.NfeVO identNFe);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Nissi.WinFormsApplication.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Nissi.WinFormsApplication.ServiceReference1.IService1>, Nissi.WinFormsApplication.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Nissi.Model.NotaFiscalVO[] ListarNotaFiscal(Nissi.Model.NotaFiscalVO identNotaFiscal) {
            return base.Channel.ListarNotaFiscal(identNotaFiscal);
        }
        
        public Nissi.Model.ParametroVO[] ListarParametro() {
            return base.Channel.ListarParametro();
        }
        
        public void IncluirParametro(Nissi.Model.ParametroVO identParametro) {
            base.Channel.IncluirParametro(identParametro);
        }
        
        public void AlterarParametro(Nissi.Model.ParametroVO identParametro) {
            base.Channel.AlterarParametro(identParametro);
        }
        
        public void ExcluirParametro() {
            base.Channel.ExcluirParametro();
        }
        
        public Nissi.Model.NfeVO[] ListarNfe(System.Nullable<int> codNF) {
            return base.Channel.ListarNfe(codNF);
        }
        
        public void IncluirNfe(Nissi.Model.NfeVO identNFe, System.Nullable<int> codUsuarioInc) {
            base.Channel.IncluirNfe(identNFe, codUsuarioInc);
        }
        
        public void AlterarNfe(Nissi.Model.NfeVO identNfe, System.Nullable<int> codUsuarioAlt) {
            base.Channel.AlterarNfe(identNfe, codUsuarioAlt);
        }
        
        public void ExcluirNfe(System.Nullable<int> codNF) {
            base.Channel.ExcluirNfe(codNF);
        }
        
        public int GerarLote(Nissi.Model.LoteVO identLote) {
            return base.Channel.GerarLote(identLote);
        }
        
        public void GravarReciboNFe(Nissi.Model.NfeVO identNFe) {
            base.Channel.GravarReciboNFe(identNFe);
        }
        
        public void GravarStatusNFe(Nissi.Model.NfeVO identNFe) {
            base.Channel.GravarStatusNFe(identNFe);
        }
    }
}

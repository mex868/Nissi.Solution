<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="relCliente.aspx.cs" Inherits="Nissi.WebPresentation.Relatorios.RelClientes" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div>
          <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
    </div>
    
        <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="100%" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Relatorios\relClientes.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ODSemitente" Name="DataSetEmitente" />
                    <rsweb:ReportDataSource DataSourceId="ODSCliente" Name="DataSetCliente" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ODSCliente" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            
            TypeName="Nissi.WebPresentation.DataSetTableAdapters.pr_relatorio_clienteTableAdapter">
            <SelectParameters>
                <asp:Parameter Name="CodPessoaIni" Type="Int32" />
                <asp:Parameter Name="CodPessoaFim" Type="Int32" />
                <asp:Parameter Name="UF" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODSemitente" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            TypeName="Nissi.WebPresentation.DataSetTableAdapters.pr_selecionar_emitenteTableAdapter">
            <SelectParameters>
                <asp:Parameter Name="CodEmitente" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>

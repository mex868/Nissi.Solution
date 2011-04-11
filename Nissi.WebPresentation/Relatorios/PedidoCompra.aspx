<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PedidoCompra.aspx.cs" Inherits="Nissi.WebPresentation.Relatorios.PedidoCompra" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:ScriptManager runat="server" ID="sm" AsyncPostBackTimeout="6000"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="100%" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
            <LocalReport ReportPath="Relatorios\PedidoCompra.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ODSEmitente" Name="DataSetEmitente" />
                    <rsweb:ReportDataSource DataSourceId="ODSPedidoCompra" 
                        Name="DataSetPedidoCompra" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ODSPedidoCompra" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            TypeName="Nissi.WebPresentation.DataSetTableAdapters.pr_relatorio_pedidocompraTableAdapter">
            <SelectParameters>
                <asp:Parameter Name="CodPedidoCompra" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODSEmitente" runat="server" 
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

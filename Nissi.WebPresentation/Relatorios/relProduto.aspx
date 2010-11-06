<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="relProduto.aspx.cs" Inherits="Nissi.WebPresentation.Relatorios.RelProduto" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="100%" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Relatorios\relProduto.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ODSemitente" Name="DataSetEmitente" />
                <rsweb:ReportDataSource DataSourceId="ODSproduto" 
                    Name="DataSetProduto" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ODSemitente" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="Nissi.WebPresentation.DataSetTableAdapters.pr_selecionar_emitenteTableAdapter">
        <SelectParameters>
            <asp:Parameter Name="CodEmitente" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSproduto" runat="server"
       OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="Nissi.WebPresentation.DataSetTableAdapters.pr_relatorio_produtonfTableAdapter">
        <SelectParameters>
            <asp:Parameter Name="Codigo" Type="String" />
            <asp:Parameter Name="Descricao" Type="String" />
            <asp:Parameter Name="DataEmissaoIni" Type="DateTime" />
            <asp:Parameter Name="DataEmissaoFim" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="relNotaFiscal.aspx.cs" Inherits="Nissi.WebPresentation.Relatorios.relNotaFiscal" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
        <rsweb:ReportViewer 
            Width="100%" 
            ID="rwNota" 
            runat="server" 
            Font-Names="Verdana" 
            Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt" 
            Height="100%" ZoomMode="PageWidth">
        <LocalReport ReportPath="Relatorios\relNotaFiscal.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjEmitente" Name="DataSetEmitente" />
                <rsweb:ReportDataSource DataSourceId="SouceNotaFiscal" 
                    Name="DataSetNotaFiacal" />
            </DataSources>
        </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="SouceNotaFiscal" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
            
            TypeName="Nissi.WebPresentation.DataSetTableAdapters.pr_relatorio_nfTableAdapter" 
            onselecting="SouceNotaFiscal_Selecting">
            <SelectParameters>
                <asp:Parameter Name="DataEmissaoIni" Type="DateTime" />
                <asp:Parameter Name="DataEmissaoFim" Type="DateTime" />
                <asp:Parameter Name="CFOP" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjEmitente" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="Nissi.WebPresentation.DataSetTableAdapters.pr_selecionar_emitenteTableAdapter">
        <SelectParameters>
            <asp:Parameter Name="CodEmitente" Type="Int32" />
        </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>

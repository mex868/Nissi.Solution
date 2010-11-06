<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelNotaFiscalPrincipal.aspx.cs" Inherits="Nissi.WebPresentation.Relatorios.NotaFiscal.RelNotaFiscalPrincipal" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="100%" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
            <LocalReport ReportPath="Relatorios\NotaFiscal\relViewNotaFiscal.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceNFE" Name="DataSetNFe" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourcenfeProc" 
                        Name="DataSetnfeProc" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceinfNFe" 
                        Name="DataSetinfNFe" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceIde" Name="DataSetide" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceEmit" 
                        Name="DataSetemit" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceEnderEmit" 
                        Name="DataSetenderEmit" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceDest" 
                        Name="DataSetdest" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceEnderDest" 
                        Name="DataSetenderDest" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceDet" Name="DataSetdet" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceProd" 
                        Name="DataSetprod" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceImposto" 
                        Name="DataSetimposto" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceICMS" 
                        Name="DataSetICMS" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceICMS40" 
                        Name="DataSetICMS40" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceTotal" 
                        Name="DataSetTotal" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceICMSTot" 
                        Name="DataSetICMSTot" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceTransp" 
                        Name="DataSettransp" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceTransporta" 
                        Name="DataSetTransporta" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceVol" Name="DataSetvol" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceCobr" 
                        Name="DataSetcobr" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceFat" Name="DataSetfat" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceDup" Name="DataSetdup" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceInfAdic" 
                        Name="DataSetinfAdic" />
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSourceProtNFe" 
                        Name="DataSetProtNfe" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSourceProtNFe" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceInfAdic" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceDup" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceFat" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceCobr" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceVol" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceTransporta" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceTransp" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceICMSTot" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceTotal" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceICMS40" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceICMS" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceImposto" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceProd" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceDet" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceEnderDest" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceDest" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceEnderEmit" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceIde" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceEmit" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceinfNFe" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourcenfeProc" runat="server">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceNFE" runat="server">
        </asp:ObjectDataSource>
        <br />
    
    </div>
    </form>
</body>
</html>

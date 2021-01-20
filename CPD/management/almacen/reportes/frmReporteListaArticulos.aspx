<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmReporteListaArticulos.aspx.vb" Inherits="CPD.frmReporteListaArticulos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><asp:LinkButton id="btnPrincipal" Text="text" class="text-uppercase" runat="server">PRINCIPAL</asp:LinkButton></li>
            <li class="active text-uppercase">Reportes</li>
            <li class="active text-uppercase">Lista de artículos</li>
        </ol>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-md-12 ">
                <div class="card">
                    <div class="card-header ch-alt bgm-bluegray">
                        <h2>Reporte de lista de artículos</h2>
                    </div>
                    <div class="card-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <center>
                                <rsweb:ReportViewer ID="rptListaArticulos" runat="server" Width="100%" SizeToReportContent="true"></rsweb:ReportViewer>
                                </center>
                            </ContentTemplate>
                        </asp:UpdatePanel> 

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

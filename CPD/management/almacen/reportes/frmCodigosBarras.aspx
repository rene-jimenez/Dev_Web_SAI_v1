<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmCodigosBarras.aspx.vb" Inherits="CPD.frmCodigosBarras" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
       <div class="container">
        <ol class="breadcrumb">
            <li><asp:LinkButton ID="btnPrincipal" Text="text" class="text-uppercase" runat="server">PRINCIPAL</asp:LinkButton></li>
            <li class="active text-uppercase">Reportes</li>
        </ol>
        <div class="clearfix"></div>             
            <div class="row">
                <div class="card">
                    <div class="card-header">
                        <h2>Códigos de barras</h2>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <center>
                                      <rsweb:ReportViewer ID="rpt" runat="server" Width="100%" SizeToReportContent="true"></rsweb:ReportViewer>
                               </center>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="text-right">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-sm-offset-4 col-sm-8 p-b-20">
                                                <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="11"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>             
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

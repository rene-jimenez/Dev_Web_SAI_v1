<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmReporteComprasArea.aspx.vb" Inherits="CPD.frmReporteComprasArea" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
        <div class="container">
        <ol class="breadcrumb">
            <li><a href="../../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li class="active text-uppercase">Reporte</li>
            <li class="active text-uppercase">Compras por área</li>
        </ol>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-md-6 col-md-offset-3">
                <div class="card">
                    <div class="card-header ch-alt bgm-bluegray">
                        <h2>Reporte compras por área</h2>
                    </div>
                    <div class="card-body">
                        <div class="tab-content p-20">
                            <div role="tabpanel" class="tab-pane animated fadeIn in active" id="tab-1">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Fecha inicio <span class="text-danger">(*)</span></label>
                                        <div class="input-group col-sm-5">
                                            <asp:TextBox ID="txbFechaInicio" runat="server" class="form-control conMascaraFecha conVentanaFecha" />
                                            <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Fecha final </label>
                                        <div class="input-group col-sm-5">
                                            <asp:TextBox ID="txbFechaFinal" runat="server" class="form-control conMascaraFecha conVentanaFecha" />
                                            <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div class="col-sm-offset-3 col-sm-8 p-b-20">
                                                    <asp:LinkButton class="btn btn-primary" runat="server" ID="btnGenerarReporte" TabIndex="10"><i class="fa fa-print"></i> Generar reporte</asp:LinkButton>
                                                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="11"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="alert alert-danger" runat="server" id="resultadoReporte" visible="false">
                                            <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                                Lo sentimos, No hay resultados con esos parámetros!</h5>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>


                </div>
            </div>

        </div>

    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row">
                <center> <rsweb:ReportViewer ID="rptReporteComprasArea" runat="server" Width="100%" SizeToReportContent="true"></rsweb:ReportViewer></center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

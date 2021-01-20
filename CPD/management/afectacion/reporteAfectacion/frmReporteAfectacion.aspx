<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmReporteAfectacion.aspx.vb" Inherits="CPD.frmReporteAfectacion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
  <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Afectación</a></li>
            <li class="active text-uppercase">Reporte</li>
        </ol>
        
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Reporte Afectación</h2>
            </div>
            <div class="card-body card-padding">
                <div class="row" >
                    <center><rsweb:ReportViewer ID="rptAfectacion" runat="server" Width="100%" SizeToReportContent="true"></rsweb:ReportViewer></center>
                </div>
                <div class="text text-right p-b-25 p-r-25">                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
                
                 
    </div>    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

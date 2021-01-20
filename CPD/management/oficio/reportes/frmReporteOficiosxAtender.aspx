<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmReporteOficiosxAtender.aspx.vb" Inherits="CPD.frmReporteOficiosxAtender" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
  <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Oficio</a></li>
            <li class="active text-uppercase">Reporte</li>
        </ol>
        
        <div class="clearfix"></div>

       <div class="row">
           <div class="col-md-6 col-md-offset-3">
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray"">
                <h2>Busqueda por fechas</h2>
            </div>
            <div class="card-body card-padding">
                <div class="row" >
                        <div class="form-horizontal">                        
                                   
                             <div class="form-group">
                                            <label class="col-sm-4 control-label">Fecha inicial <span class="text-danger">(*)</span></label>
                                            <div class="col-sm-6">
                                                <div class="fg-line">
                                                    <asp:TextBox runat="server" ID="txbFechaInicial" class="form-control input-sm conVentanaFecha"  TabIndex="3" />
                                                </div>
                                            </div>
                                        </div>
                             <div class="form-group">
                                            <label class="col-sm-4 control-label">Fecha final<span class="text-danger">(*)</span></label>
                                            <div class="col-sm-6">
                                                <div class="fg-line">
                                                    <asp:TextBox runat="server" ID="txbFechaFinal" class="form-control input-sm conVentanaFecha"  TabIndex="4" />
                                                </div>
                                            </div>
                                        </div>
                             <div class="form-group">
                                            <div class="col-sm-offset-4 col-sm-8 p-b-20">
                                             <asp:UpdatePanel runat="server" >
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="btnBuscarFecha" runat="server" CssClass="btn btn-primary" TabIndex="3"><i class="fa fa-print"></i> Generar reporte</asp:LinkButton>
                                                        <asp:LinkButton ID="btnRegresar" runat="server" CssClass="btn btn-default" TabIndex="4"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                               </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                       </div>
                </div>
          
            </div>
        </div>
           </div>
       </div>     

      <div class="row">
      
       <asp:UpdatePanel id="updrpt" UpdateMode="Conditional" runat="server">
            <ContentTemplate>

                <div class="card-body card-padding"">
                <div class="row" >
                   <center> <rsweb:ReportViewer ID="rptOficioxAtender" visible="true"  runat="server" Width="100%" SizeToReportContent="true"></rsweb:ReportViewer></center>
                </div>   
    
            </div>
           </ContentTemplate>
       </asp:UpdatePanel>
          
        </div>         
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="conUpdatePanel" runat="server">
      
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
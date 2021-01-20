<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmReporteSolicitud.aspx.vb" Inherits="CPD.frmReporteSolicitud" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    
          <div class="container" >
                <ol class="breadcrumb">
                <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
                <li><a href="#" class="text-uppercase">Reporte Gastos</a></li>
            </ol>

            <div class="block-header">
                <h2 class="active text-uppercase">Reporte de Solicitud de gastos
                </h2>
            </div>
               <div class="clearfix"></div>
            <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Reporte Solicitud </h2>
            </div>
            <div class="card-body card-padding">
               
                <div class="row" id="divCmb" runat="server" visible="false">
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Solicita</label>
                                <asp:DropDownList ID="cmbSolicita" class="input-sm form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Autoriza</label>
                                <asp:DropDownList ID="cmbAutoriza" runat="server" class="input-sm form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-sm-12 text-right">
                        <div class="m-t-10">
                            <asp:Button ID="btnGuardar" runat="server" class="btn btn-mdb-color btn-sm waves-effect" Text="Generar" ValidationGroup="val" />
                            <asp:Button ID="btnCancelar" runat="server" class="btn btn-blue-grey btn-sm waves-effect" data-dismiss="modal" Text="Cancelar" />
                        </div>
                    </div>                    
                </div>
                 
                <div  class="form-group m-b-30">
                    <center>
                 <rsweb:ReportViewer ID="rptReporteSolicitudGastos" runat="server"  Height="660px" LinkDisabledColor="Black" PromptAreaCollapsed="True" Width="100%" SizeToReportContent="true" ></rsweb:ReportViewer> 
                        </center>                                          
                     </div> 
                <div class="text text-right p-b-25 p-r-25">                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
              
            </div>
                <div class="card-footer">

                </div>
               
           
        </div>

          </div>
      
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

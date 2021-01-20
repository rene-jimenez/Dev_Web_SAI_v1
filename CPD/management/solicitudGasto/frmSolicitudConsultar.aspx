<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmSolicitudConsultar.aspx.vb" Inherits="CPD.frmSolicitudConsultar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Gastos</a></li>
            <li class="active text-uppercase">Solicitud de gastos</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Solicitud de gastos
            </h2>
        </div>
        
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Consultar solicitud</h2>
            </div>
            <div class="card-body card-padding">
                <div class="row">

                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno SAF</label>
                                <asp:TextBox ID="txbTurnoSAF" runat="server" class="form-control input-sm" disabled="" />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno DRM </label>
                                <asp:TextBox ID="txbTurnoDRM" runat="server" class="form-control input-sm" disabled="" />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha de elaboración</label>
                                <asp:TextBox ID="txbFechaElaboracion" runat="server" class="form-control input-sm" disabled="" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Folio caja</label>
                                <asp:TextBox ID="txbFolioCaja" runat="server" class="form-control input-sm" disabled="" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Folio tesoreria</label>
                                <asp:TextBox ID="txbFolioTesoreria" runat="server" class="form-control input-sm" disabled="" />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha recepción</label>
                                <asp:TextBox ID="txbFechaRecepcion" runat="server" class="form-control input-sm" disabled="" />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Área</label>
                                <asp:TextBox ID="txbArea" runat="server" class="form-control input-sm" disabled="" />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Cargo presupuestal</label>
                                <asp:TextBox ID="txbCargoPres" runat="server" class="form-control input-sm" disabled="" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Partida presupuestal</label>
                                <asp:TextBox ID="txbPartidaPresupuestal" runat="server" class="form-control input-sm" disabled="" />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Importe</label>                            

                                <asp:TextBox ID="txbImporte" runat="server" class="form-control input-sm" disabled="" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Concepto</label>
                                <asp:TextBox ID="txbConcepto" runat="server" MaxLength="300" class="form-control input-sm" TextMode="MultiLine" Rows="3" disabled="" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Marca de agua</label>
                                <asp:TextBox ID="txbMarcaAgua" runat="server" MaxLength="300" class="form-control input-sm" TextMode="MultiLine" Rows="3" disabled="" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">

        <div class="col-sm-12" id="esCancelado" runat="server">
            <div class="card bgm-red">

                <div class="card-body card-padding">
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <div class="checkbox">
                                        
                                        <i class="fa fa-trash-o fa-4x" aria-hidden="true"></i>
                                        <label class="fg-label">Cancelado</label>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha cancelación</label>
                                    <asp:TextBox ID="txbFechaCancelacion" runat="server" class="form-control input-sm" style="color: white;" disabled="" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <label class="fg-label">Responsable cancelación</label>
                                    <asp:TextBox ID="txbResponsableCancelacion" runat="server" class="form-control input-sm" style="color: white;" disabled="" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <label class="fg-label">Observaciones</label>
                                    <asp:TextBox ID="txbObservacionCancelacion" runat="server" class="form-control input-sm" style="color: white;" disabled="" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <div class="text text-right p-b-25 p-t-25 p-r-25">
                <asp:LinkButton class="btn btn-primary" runat="server" ID="btnImprimir"><i class="fa fa-print"></i> Imprimir</asp:LinkButton>
                <asp:LinkButton class="btn btn-default" runat="server" ID="btnCancelar"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
            </div>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
     
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
       <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".conFiltro").select2();
                   }
               });
           };
</script>
</asp:Content>


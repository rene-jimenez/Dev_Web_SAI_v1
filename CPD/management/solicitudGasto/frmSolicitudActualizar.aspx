<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmSolicitudActualizar.aspx.vb" Inherits="CPD.frmSolicitudActualizar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
<div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Gastos</a></li>
            <li class="active text-uppercase">Actualizar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Solicitud de gastos
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Modificar solicitud</h2>
            </div>
           <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno SAF </label>
                                <asp:TextBox ID="txbTurnoSAF" runat="server" class="form-control input-sm" disabled/>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno DRM </label>
                                <asp:TextBox ID="txbTurnoDRM" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha elaboración </label>
                                <asp:TextBox ID="txbFechaElaboracion" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>
                    

                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Área </label>
                                <asp:TextBox ID="txbArea" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Cargo Presupuestal </label>
                                <asp:TextBox ID="txbCargoPres" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Partida presupuestal </label>
                                <asp:TextBox ID="txPartidaPres" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>
                    
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Importe</label>
                                <asp:TextBox ID="txbImporte" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Marca de agua</label>
                                <asp:TextBox runat="server" ID="txbMarcaAgua" MaxLength="300" CssClass="form-control" TextMode="MultiLine" Rows="3" disabled />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Concepto</label>
                                <asp:TextBox runat="server" ID="txbConcepto" MaxLength="300" CssClass="form-control" TextMode="MultiLine" Rows="3" disabled />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-4" id="divFechaRecepcion" runat="server" >
                      
                                  <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha de recepcion <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbFechaRec" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" placeholder="Seleccione una fecha" />

                            </div>
                        </div>
                      
                    </div>

                    <div class="col-sm-4" id="divFolioTe" runat="server">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Folio de tesoreria <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbFolioTesoreria" runat="server" class="form-control input-sm" AutoPostBack="true" maxlength="6"  placeholder="Folio Tesoreria" />

                            </div>
                        </div>
                    </div>

                    <div class="col-sm-4" id="divFolioCa" runat="server">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Folio de caja <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbFolioCaja" runat="server" class="form-control input-sm" AutoPostBack="true" maxlength="6"  placeholder="Folio Caja" />
                            </div>
                        </div>
                    </div>
                     
                </div>
                <div class="text text-right p-b-25 p-r-25">
                     <asp:LinkButton class="btn btn-success" runat="server" ID="btnActualizar"><i class="fa fa-refresh"></i> Actualizar</asp:LinkButton>
                     <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>                                       
                </div>
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
                       $(".conVentanaFecha").datepicker({ format: 'dd/mm/yyyy', autoclose: true });
                   }
               });
           };
</script>
    <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".conMascaraFecha").inputmask("99/99/9999");
                   }
               });
           };
</script>
    
</asp:Content>


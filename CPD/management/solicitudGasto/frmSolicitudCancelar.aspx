<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmSolicitudCancelar.aspx.vb" Inherits="CPD.frmSolicitudCancelar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
    <asp:HiddenField  id="idlbl" runat="server" />
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
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Cancelar solicitud</h2>
            </div>
           <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno SAF </label>
                                <asp:TextBox ID="txbTurnoSAF" runat="server" class="form-control input-sm" disabled/>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno DRM </label>
                                <asp:TextBox ID="txbTurnoDRM" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha elaboración </label>
                                <asp:TextBox ID="txbFechaElaboracion" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>                    
                    
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha Recepción </label>
                                <asp:TextBox ID="txbFechaRecepcion" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>
                    
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Folio de tesorería </label>
                                <asp:TextBox ID="txbFolioTeso" runat="server" class="form-control input-sm" disabled />

                            </div>
                        </div>
                    </div>
                     <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Folio de Caja </label>
                                <asp:TextBox ID="txbFolioCaja" runat="server" class="form-control input-sm" disabled />

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
                     <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Observaciones <span class="text-danger">(*)</span></label>
                                <asp:TextBox runat="server" ID="txbObservaciones" MaxLength="300" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Escribe observaciones" />
                            </div>
                        </div>
                    </div>

                   
                     
                </div>
                <div class="text text-right p-b-25 p-r-25">
                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnCancelar"><i class="fa fa-refresh"></i> Cancelar</asp:LinkButton> 
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
                       $(".conFiltro").select2();
                   }
               });
           };
</script>
</asp:Content>



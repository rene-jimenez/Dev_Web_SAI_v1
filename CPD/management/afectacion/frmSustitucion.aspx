<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmSustitucion.aspx.vb" Inherits="CPD.frmSustitucion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
       <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Afectación presupuestal</a></li>
            <li class="active text-uppercase">Alta</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Afectación presupuestal
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Sustitucion de afectación presupuestal</h2>
            </div>
           <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-3">

                        <small><strong>Turno SAF</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="txbTurnoSAF" runat="server" /></h5>
                        <br />
                        <small><strong>Turno DRM</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="txbTurnoDRM_" runat="server" /></h5>
                        <br />
                         <small><strong>Núm. Pedido</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="txbNumPedido" Text="Aquí va el num pedido" runat="server" /></h5>
                        <br />                       
                        <small><strong>Área solicitante</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="txbAreaSolicitante" Text="Aquí va el área" runat="server" /></h5>
                        <br />
                         <small><strong>Cargo presupuestal</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="txbCargoPresupuestal" Text="Aquí va el cargo" runat="server" /></h5>
                        <br />
                         <small><strong>Partida presupuestal</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="txbPartida" Text="Aquí va la partida" runat="server" /></h5>
                        <br />
                    </div>
                           <div class="col-sm-9">
                               <div class="row">
                                <div class="col-sm-6">
                                        <small><strong>Solicitud de expedición de pago a favor de:</strong> </small>
                                        <h5 class="m-5 f-600"> <asp:Label id="txbProveedor" runat="server" Text="Razón social proveedor" /></h5>
                                </div>
                                <div class="col-sm-6 block-header" style="background: #f4f7f7">
                                    <small><strong>Por la cantidad de:</strong> </small>
                                        <h5 class="m-t-5 f-600"> <asp:Label id="txbImportePago" Text="$0.00" runat="server" /></h5>
                                            <ul class="actions p-t-15">

                                                            <li class="dropdown">
                                                                <a href="#" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkSubtotal" runat="server">Subtotal: $</asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkDescuento" runat="server">-Descuento: $</asp:LinkButton>
                                                                    </li>
                                                                     <li>
                                                                        <asp:LinkButton ID="lnkIva" runat="server" >+ Iva: $</asp:LinkButton>
                                                                    </li>
                                                                     <li class="f-14" style="border-top:solid 1px #c4c7c7">
                                                                        <asp:LinkButton ID="lnkTotal" runat="server">Total: $</asp:LinkButton>
                                                                    </li>

                                                                </ul>
                                                            </li>
                                                        </ul>
                                        
                                </div>
                                <div class="col-sm-6">
                                     <small><strong>Solicita:</strong> </small>
                                        <h5 class="m-t-5 f-600"> <asp:Label id="lblSolicita" Text="$0.00" runat="server" /></h5>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group m-b-30">
                                           <small><strong>Autoriza:</strong> </small>
                                        <h5 class="m-t-5 f-600"> <asp:Label id="lblAutoriza" Text="$0.00" runat="server" /></h5>
                        </div>
                                </div>
                                                             <div class="col-sm-12">

                                                                    <small><strong>Concepto</strong> </small>
                                        <h5 class="m-t-5 f-600"> <asp:Label id="txbConcepto" Text="$0.00" runat="server" /></h5>
                                </div>
                                   <div class="col-sm-12">
                                        <small><strong>Marca de agua</strong> </small>
                                        <h5 class="m-t-5 f-600"> <asp:Label id="txbMarcaAgua" Text="$0.00" runat="server" /></h5>
                                </div>
  
                               </div>
                                
                       
                        <br />
                    </div>

                     
                </div>


               

                <div class="text text-right p-b-25 p-r-25">
                   <asp:LinkButton class="btn btn-primary" runat="server" ID="btnImprimir"><i class="fa fa-print"></i> Imprimir</asp:LinkButton>
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

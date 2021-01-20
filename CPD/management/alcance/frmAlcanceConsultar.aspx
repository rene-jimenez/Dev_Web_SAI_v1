<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAlcanceConsultar.aspx.vb" Inherits="CPD.frmAlcanceConsultar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">

    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Alcance</a></li>
            <li class="active text-uppercase">Consultar Alcance</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Alcance
            </h2>
        </div>
        <div class="clearfix"></div>
        


        <div class="card" id="profile-main">
                        <div class="pm-overview c-overflow mCustomScrollbar " style="overflow: visible;">
                            <div class="mCustomScrollBox mCSB_vertical_horizontal mCSB_outside" style="max-height: none;" tabindex="0">
                                <div id="mCSB_3_container" class="mCSB_container mCS_no_scrollbar_x" style="position: relative; top: 0px; left: 0px; width: 100%;" dir="ltr">
                                    <div class="pmo-block pmo-contact">
                                <h2>
                                <asp:Label ID="LBLTITULO" runat="server" Text="Datos Solicitud de Gasto" CssClass="f-15"></asp:Label></h2>
                               <ul>
                                <li class="ng-binding">
                                     <strong>DRM: </strong><asp:Label ID="lblDRM" runat="server" ></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>SAF: </strong> <asp:Label ID="lblSAF" runat="server" ></asp:Label>
                                </li>
                                <li class="ng-binding">
                                     <strong>ÁREA: </strong><asp:Label ID="lblNombreArea" runat="server"></asp:Label>
                                </li>
                                <li class="ng-binding">
                                     <strong>CARGO: </strong><asp:Label ID="lblCargo" runat="server" ></asp:Label>
                                </li>
                                 <li class="ng-binding">
                                    <strong>CONCEPTO: </strong> <asp:Label ID="lblConcepto" runat="server" ></asp:Label>
                                </li>
                                 <li class="ng-binding">
                                    <strong>FOLIO TESORERÍA: </strong> <asp:Label ID="lblFolioTesoreria" runat="server" ></asp:Label>
                                </li>
                                 <li class="ng-binding">
                                     <strong>FOLIO CAJA: </strong><asp:Label ID="lblFolioCaja" runat="server" ></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>FECHA RECEPCIÓN: </strong> <asp:Label ID="lblFechaLiberacion" runat="server" ></asp:Label>
                                </li>
                                <li class="ng-binding">
                                     <strong>FECHA CAPTURA: </strong><asp:Label ID="lblFechaCaptura" runat="server"></asp:Label>
                                </li>                                 
                            </ul>                               
                            </div>                            
                        </div>
                            </div>                           
                            <div class="mCSB_scrollTools mCSB_3_scrollbar mCS-minimal-dark mCSB_scrollTools_horizontal" style="display: none;">
                                <div class="mCSB_draggerContainer">
                                    <div class="mCSB_dragger" style="position: absolute; min-width: 50px; width: 0px; left: 0px;" oncontextmenu="return false;">                                       
                                    </div>                                   
                                </div>
                            </div>
                        </div>

                        <div class="pm-body clearfix">           
                            <div class="pmb-block">
                                 <div class="pmbb-header">
                                    <h2><i class="zmdi zmdi-assignment m-r-10"></i> Detalles de la solicitud</h2>
                                 </div>
                                <div class="pmbb-body p-l-30">
                                    <div class="pmbb-view">
                                        <table id="data-table" class="table table-striped table-vmiddle">
                                <thead>
                                    <tr>
                                        <th data-column-id="area" width="15%">Área</th>
                                        <th data-column-id="partida" width="15%">Partida</th>
                                        <th data-column-id="descripcion" width="60%">Descripción</th>
                                        <th data-column-id="importe" width="15%">Importe</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <tr style="border-top: double 2px #525353; background-color: #f7f7f7">

                                        <td>
                                            <asp:Label ID="lblArea" runat="server" Width="15%"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblPartida" runat="server" Width="15%"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblDescripcion" runat="server" Width="60%"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblCantidad" runat="server" Width="15%"></asp:Label></td>

                                    </tr>

                                </tbody>
                                <tfoot>
                                    <tr style="border-top: solid 1.5px #808080; vertical-align:middle; background-color: #ffffff">
                                        <td></td> 
                                        <td></td> 
                                        <td></td> 
                                        <td></td> 
                                    </tr>
                                </tfoot>
                            </table>  
                                   </div>
                                </div>
                                <div style="background: #f4f7f7">
                                    <div class="pmbb-header">
                                        <h2><i class="zmdi zmdi-star m-r-10 c-deeporange"></i>Datos del Alcance </h2>
                                    </div>

                                    <div class="pmbb-body p-l-30">                                                                      
                                          <div class="pmbb-view">
                                                    <dl class="dl-horizontal">
                                                        <dt class="p-t-10">Folio Tesoreria:</dt>
                                                        <dd>
                                                            <asp:Label ID="lblFolioAlcanceTesoreria" runat="server"></asp:Label>
                                                        </dd>
                                                    </dl>
                                                    <dl class="dl-horizontal">
                                                        <dt class="p-t-10">Folio Caja:</dt>
                                                        <dd>
                                                            <asp:Label ID="lblFolioAlcanceCaja" runat="server" ></asp:Label>

                                                        </dd>
                                                    </dl>

                                                    <div class="pmbb-view">
                                                        <dl class="dl-horizontal">
                                                            <dt class="p-t-10">Fecha recepción:</dt>
                                                            <dd>
                                                                <asp:Label ID="lblFechaRecepcionAlcance" runat="server" ></asp:Label>

                                                            </dd>
                                                        </dl>
                                                        <dl class="dl-horizontal">
                                                            <dt class="p-t-10">Fecha captura:</dt>
                                                            <dd>
                                                                <asp:Label ID="lblFechaCapturaAlcance" runat="server"></asp:Label>

                                                            </dd>
                                                        </dl>
                                                        <dl class="dl-horizontal">
                                                            <dt class="p-t-10">Importe:</dt>
                                                            <dd>
                                                                <asp:Label ID="lblImporteAlcance" runat="server" ></asp:Label>
                                                            </dd>
                                                        </dl>
                                                        <dl class="dl-horizontal">
                                                            <dt class="p-t-10">Partida Presupuestal:</dt>
                                                            <dd>
                                                                <asp:Label ID="lblPartidaPresupuestalAlcance" runat="server" ></asp:Label>
                                                            </dd>
                                                        </dl>



                                                             <div class="col-sm-12" id="esCancelado" runat="server">
            <div class="card bgm-red">

                <div class="card-body card-padding">
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <div class="checkbox">
                                        
                                        <i class="fa fa-trash-o fa-2x" aria-hidden="true"></i>
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
                                                        <div class="text text-right m-t-25 p-b-25 p-r-25">
                                                            <asp:LinkButton class="btn btn-primary" runat="server" ID="btnImprimir" TabIndex="4"><i class="fa fa-print"></i> Imprimir</asp:LinkButton>
                                                            <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="5"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                                        </div>
                                                    </div>
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

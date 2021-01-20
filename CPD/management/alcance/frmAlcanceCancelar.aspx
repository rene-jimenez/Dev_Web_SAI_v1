<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAlcanceCancelar.aspx.vb" Inherits="CPD.frmAlcanceCancelar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="conUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Alcance</a></li>
            <li class="active text-uppercase">Cancelar Alcance</li>
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
                                <h2><asp:Label ID="LBLTITULO" runat="server" Text="Datos de la solicitud de gasto" CssClass="f-15"></asp:Label></h2>
                             <ul>
                                <li class="ng-binding">
                                     <b> DRM :</b>
                              <asp:Label ID="lblDRM" runat="server" Text="aqui va el Turno DRM" CssClass="f-15"></asp:Label> 
                                </li>

                                <li class="ng-binding">
                                    <b> SAF : </b>
                                    <asp:Label ID="lblSAF" runat="server" Text="aqui va el SAF"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Área :</b>
                                    <asp:Label ID="lblNombreArea" runat="server" Text="aqui va el área"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Cargo :</b>
                                   <asp:Label ID="lblCargo" runat="server" Text="aqui va el cargo pres"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Concepto :</b>
                                 <asp:Label ID="lblConcepto" runat="server" Text="aqui va el concepto" CssClass="text text-uppercase"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Folio Tesoreria :</b>
                                <asp:Label ID="lblFolioTesoreria" runat="server" Text="f-tesorería ant."></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Folio Caja :</b>
                                    <asp:Label ID="lblFolioCaja" runat="server" Text="f-caja ant."></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Fecha recepción :</b>
                                <asp:Label ID="lblFechaLiberacion" runat="server" Text="fecha recepcion" CssClass="text text-uppercase"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Fecha captura :</b>
                                <asp:Label ID="lblFechaCaptura" runat="server" Text="fecha captura" CssClass="text text-uppercase"></asp:Label> 
                                </li>
                                
                            </ul>

                        </div>

                    </div>

                </div>

            </div>

            <div class="pm-body clearfix">

                <div class="pmb-block">
                    <div class="pmbb-header">
                        <h2><i class="zmdi zmdi-assignment m-r-10"></i>Detalles de la solicitud</h2>
                    </div>

                    <div class="pmbb-body p-l-50">
                        <div class="pmbb-view">
                            <table id="data-table" class="table table-striped table-vmiddle">
                                <thead>
                                     <tr class="success">
                                        <th data-column-id="area" width="15%">Área</th>
                                        <th data-column-id="partida" width="35%">Partida</th>
                                        <th data-column-id="descripcion" width="35%">Descripción</th>
                                        <th data-column-id="importe" width="15%">Importe</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <tr style="border-top: double 2px #525353; background-color: #f7f7f7">

                                        <td>
                                            <asp:Label ID="lblArea" runat="server" ></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblPartida" runat="server" ></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblDescripcion" runat="server" ></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblimporte" runat="server" ></asp:Label></td>

                                    </tr>

                                </tbody>
                                <tfoot>
                                    <tr style="border-top: solid 1.5px #808080; vertical-align: middle; background-color: #ffffff">
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
                        <div class="pmbb-header f-15"">
                            <h2><i class="zmdi zmdi-attachment m-r-10 c-deeporange"></i>Datos de la solicitud de alcance</h2>
                            <hr />
                  
                        <div class="pmbb-body p-l-30">
                            <div class="">
                                <div class="col-sm-12" id="divCancelacion" runat="server">
                                    <div class="col-sm-10">

                                        <dl class="dl-horizontal">
                                            <dt class="p-t-11"> <i>Importe del alcance:</i>: </dt>
                                            <dd>
                                                <asp:Label runat="server" ID="lblImporteAlcance"></asp:Label>

                                            </dd>
                                        </dl>
                                        <dl class="dl-horizontal">
                                            <dt class="p-t-11">Responsable: </dt>
                                            <dd>
                                                <asp:Label runat="server" ID="lblResponsable" Text="Aqui va el responsable"></asp:Label>

                                            </dd>
                                        </dl>
                                        <dl class="dl-horizontal">
                                            <dt class="p-t-10">Observaciones: <span class="text-danger">(*)</span></dt>
                                            <dd>
                                                <div class="fg-line">
                                                    <asp:TextBox ID="txbObservaciones" runat="server" class="form-control" placeholder="Motivo de la cancelación" TabIndex="1" Rows="3"></asp:TextBox>
                                                </div>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                            </div>

                              <div class="text text-right m-t-25 p-b-25 p-r-25">
                                        <asp:LinkButton class="btn btn-success" runat="server" ID="btnCancelarAlcance" TabIndex="2"><i class="fa fa-check-circle"></i> Cancelar Alcance</asp:LinkButton>
                                        <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="3"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                </div>
                              
                            
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

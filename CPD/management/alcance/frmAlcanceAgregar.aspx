<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAlcanceAgregar.aspx.vb" Inherits="CPD.frmAlcanceAgregar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Alcance</a></li>
            <li class="active text-uppercase">Agregar Alcance</li>
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
                                     <strong>DRM: </strong>
                                    <asp:Label ID="lblDRM" runat="server" Text="aqui va el DRM" ></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>SAF: </strong>
                                    <asp:Label ID="lblSAF" runat="server" Text="aqui va el SAF"></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>ÁREA: </strong>
                                    <asp:Label ID="lblNombreArea" runat="server" Text="aqui va el área"></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>CARGO: </strong>
                                    <asp:Label ID="lblCargo" runat="server" Text="aqui va el cargo pres"></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>CONCEPTO: </strong>
                                    <asp:Label ID="lblConcepto" runat="server" Text="aqui va el concepto" CssClass="text text-uppercase"></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>FOLIO TESORERÍA: </strong>
                                    <asp:Label ID="lblFolioTesoreria" runat="server" Text="f-tesorería" ></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>FOLIO CAJA: </strong>
                                    <asp:Label ID="lblFolioCaja" runat="server" Text="f-caja"></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>FECHA LIBERACIÓN: </strong>
                                    <asp:Label ID="lblFechaLiberacion" runat="server" Text="fecha liberación" CssClass="text text-uppercase"></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <strong>FECHA CAPTURA: </strong>
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
                                    <h2><i class="zmdi zmdi-assignment m-r-10"></i> Detalles de la solicitud</h2>
                    </div>
                    <div class="pmbb-body p-l-30">
                        <div class="pmbb-view">
                            <table id="data-table" class="table table-striped table-vmiddle">
                                <thead>
                                    <tr>
                                        <th data-column-id="area" width="15%">Área</th>
                                        <th data-column-id="partida" width="15%">Partida</th>
                                        <th data-column-id="descripcion" width="50%">Descripción</th>
                                        <th data-column-id="importe" width="25%">Importe</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <tr style="border-top: double 2px #525353; background-color: #f7f7f7">

                                        <td>
                                            <asp:Label ID="lblArea" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblPartida" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblDescripcion" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblCantidad" runat="server"></asp:Label></td>

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
                        <div class="pmbb-header f-15">
                            <h2><i class="zmdi zmdi-plus-circle m-r-10 c-deeporange"></i>Datos de la solicitud de alcance</h2>


                        </div>

                        <div class="pmbb-body p-l-30">


                            <div class="pmbb-view">
                           
                                    <dl class="dl-horizontal"  id="divImporte" runat="server">
                                        <dt class="p-t-10">Importe: <span class="text-danger">(*)</span></dt>
                                        <dd>
                                            <div class="fg-line">
                                                <asp:TextBox ID="txbImporte" runat="server" class="form-control" placeholder="$ 00.00" onkeydown="return thisIsDouble(event);" TabIndex="1"></asp:TextBox>
                                            </div>

                                        </dd>
                                    </dl>
                                   <dl class="dl-horizontal">
                                    <dt class="p-t-10">Partida presupuestal: <span class="text-danger">(*)</span></dt>
                                    <dd>
                                        <div class="fg-line">
                                            <div class="select">
                                                <asp:DropDownList ID="cmbPartida" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True" TabIndex="2">
                                                    <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </dd>
                                </dl>


                                <div class="text text-right m-t-25 p-b-25 p-r-25">
                                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar" TabIndex="3"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="4"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
 </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

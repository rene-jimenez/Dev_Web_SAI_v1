<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmConsultarEntrada.aspx.vb" Inherits="CPD.frmConsultarEntrada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">

    <div class="container">
        <ol class="breadcrumb">
             <li><a href="../../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Almacén</a></li>
            <li><a href="#" class="text-uppercase">Entrada</a></li>
            <li class="active text-uppercase">Consultar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Almacén
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Consultar entrada
                </h2>
            </div>
            <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Núm. Pedido <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbNumeroPedido" runat="server" MaxLength="6" class="form-control input-sm" disabled="" placeholder="000000" TabIndex="1" />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-8">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Proveedor <span class="text-danger">(*)</span></label>
                                <asp:Label runat="server" ID="txbProveedor" CssClass="form-control input-sm" TabIndex="5"/>
                                <%--<asp:TextBox ID="txbProveedor" runat="server" class="form-control input-sm " disabled="" TabIndex="3" />--%>

                            </div>
                        </div>

                    </div>

                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="input-group">
                            <div class="fg-line">
                                <label class="fg-label">Fecha final <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbFechaFinal" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" disabled="" TabIndex="3" />
                            </div>
                            <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="input-group">
                            <div class="fg-line">
                                <label class="fg-label">Fecha pedido <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbFechaPedido" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" disabled="" TabIndex="4" />
                            </div>
                            <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <div class="fg-line">
                                <label class="fg-label"></label>
                                <div class="form-group">
                                    <div class="checkbox">
                                        &nbsp;&nbsp;&nbsp;
                              <label >
                                  <asp:CheckBox ID="chkNota" runat="server" TabIndex="7" />
                                  <i class="input-helper"></i>
                                  Nota
                              </label>
                                        &nbsp;&nbsp;&nbsp;
                             <label>
                                 <asp:CheckBox ID="chkFactura" runat="server" TabIndex="8" />
                                 <i class="input-helper"></i>
                                 Factura
                             </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Número del documento <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbFactura" runat="server" MaxLength="6" class="form-control input-sm" disabled="" placeholder="123456" TabIndex="5" />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Observaciones:</label>
                                <asp:TextBox ID="txbObservaciones" runat="server" class="form-control input-sm" Rows="2" disabled="" TextMode="MultiLine" TabIndex="9" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="card-body card-padding">
                <div class="row">
                     <div class="col-sm-12">

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lsvArticulosEntrada" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>
                                            <th style="width: 5%" class="text-center">Código barras</th>
                                            <th style="width: 5%;" class="text-center">Cant. Pedida</th>
                                            <th style="width: 5%;" class="text-center">Cant. Recibida</th>                                           
                                            <th style="width: 5%;" class="text-center">Cant. Faltante</th>
                                            <th style="width: 30%;" class="text-Center">Artículo</th>
                                            <th style="width: 5%;" class="text-center">Únidad Medida</th>
                                            <th style="width: 8%;" class="text-center">Precio</th>
                                            <th style="width: 8%;" class="text-center">Subtotal</th>
                                            <th style="width: 8%;" class="text-Center">IVA</th>
                                            <th style="width: 8%;" class="text-center">Total</th>
                                            <th style="width: 8%;" class="text-center">Tipo Entrada</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCodeBar" Text='<%#Eval("_codigoBarras") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCantidadPedida" Text='<%#Eval("_cantidadPedido") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCantidadRecibida" Text='<%#Eval("cantidad") %>' runat="server" />
                                    </td>                                  
                                    <td>
                                        <asp:Label ID="lblCantidadFaltante" Text='<%#Eval("_cantidadFaltante") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblArticulo" Text='<%#Eval("_nombreArticulo") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUnidadMedida" Text='<%#Eval("_unidadMedidaArticulo") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCostoXUnidad" Text='<%#Eval("_precioUnitario", "{0:c4}") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubtotal" Text='<%#Eval("_subTotal", "{0:c}") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIva" Text='<%#If(Eval("_iva"), Eval("_valorIva"), "0") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCostoTotal" Text='<%#Eval("_total", "{0:c}") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTipoEntrada" class='<%# If(Eval("esParcial"), "text-warning", "text-success") %>' Text='<%#If(Eval("esParcial"), "Parcial", "Completo") %>' runat="server" />
                                    </td>

                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <tr class="bgm-deeporange">
                                    <div>
                                        <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                        No existen registros
                                    </div>
                                </tr>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                     </div>
                </div>
            </div>



            <div class="text text-right p-b-25 p-r-25">
                <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="11"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
            </div>


        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmPedidoConsultar.aspx.vb" Inherits="CPD.frmPedidoConsultar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">PEDIDOS</a></li>
            <li class="active text-uppercase">CONSULTA DE PEDIDOS</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Pedidos
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header ch-alt">
                        <h2>Pedido:
                            <asp:Label ID="lblPedido" runat="server" Text="xxxxxx"></asp:Label>
                            <small>Turno SAF: 
                                <asp:Label ID="lblSaf" runat="server" Text="xxxxxx">
                                </asp:Label></small>
                        </h2>
                    </div>
                    <div class="card-body card-padding">
                        <div class="pmo-contact">
                            <ul>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i><strong>
                                    <asp:Label ID="lblTurnoDRM" runat="server" Text="aqui va el Turno DRM" CssClass="f-15"></asp:Label></strong> </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblArea" runat="server" Text="aqui va el área"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblFechaElaboracion" runat="server" Text="aqui va fecha elaboración"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblCargoPresupuestal" runat="server" Text="aqui va el cargo pres"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblPartidaPresupuestal" runat="server" Text="aqui va la partida pres"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblElaboro" runat="server" Text="elaboró:"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblReviso" runat="server" Text="revisó:"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblAutorizo" runat="server" Text="autorizó"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblFechaSolicitud" runat="server" Text="fecha Solicitud"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblFechaAcordada" runat="server" Text="fecha Acordada"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblFechaRecibida" runat="server" Text="fecha Recibida"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblProveedor" runat="server" Text="aqui va el proveedor"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblTipoPago" runat="server" Text="aqui va el tipopago"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblObservaciones" runat="server" Text="aqui van las observaciones"></asp:Label>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header ch-alt">
                        <h2>Artículos en este pedido con
                            <asp:Label ID="lblFElab" runat="server" Text="aqui va fecha elaboración"></asp:Label></h2>
                    </div>
                    <div class="card-body card-padding">
                        <asp:ListView ID="lvsListado" runat="server" ItemPlaceholderID="elemento">
                            <LayoutTemplate>
                                <table id="data-table" class="table table-striped table-vmiddle">
                                    <thead>
                                        <tr>
                                            <th data-column-id="id" data-type="numeric" width="5%">Núm.</th>
                                            <th data-column-id="articulo" width="40%">Articulo</th>
                                            <th data-column-id="unidad" width="10%">Unidad Medida</th>
                                            <th data-column-id="cantidad" width="10%">Cantidad</th>
                                            <th data-column-id="precio" width="15%">Precio unitario</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="elemento" runat="server" />
                                    </tbody>
                                    <tr class="rating-list">
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>

                                <tr class="rating-list">
                                    <td>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblArticulo" runat="server" Text='<%#Eval("_articulo")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblUnidad" runat="server" Text='<%#Eval("_unidadMedidaArticulo")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("cantidad")%>'></asp:Label></td>
                                    <td class="text-right">
                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# "$ " + Eval("precioUnitario", "{0}")%>'></asp:Label></td>


                                </tr>

                            </ItemTemplate>
                            <InsertItemTemplate>
                            </InsertItemTemplate>
                            <EmptyItemTemplate>
                            </EmptyItemTemplate>
                            <EmptyDataTemplate>
                                <tr class="text-center">
                                    <div class="bgm-deeporange p-20">
                                        <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                            No existen registros, revisa la información o llama a la Coordinación de Informática, al área de sistemas</h5>
                                    </div>
                                </tr>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <table id="data-table" class="table table-striped table-vmiddle">
                            <div id="lineaSubTotal" runat="server">
                                <tr style="border-top: solid 1.5px black; vertical-align: middle; background-color: #ffffff;">
                                    <td></td>
                                    <td class="text-right">SubTotal: </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblSubTotal" runat="server" Text=''></asp:Label></td>
                                </tr>
                            </div>
                            <div id="lineaDescuento" runat="server">
                                <tr style="border-top: solid 1px #808080; background-color: #f9f9f9">
                                    <td></td>
                                    <td class="text-right">-- descuento: </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblDescuento" Text='<% #Eval("CDesc") %>' runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </div>
                            <div id="lineaIva" runat="server">
                                <tr style="border-top: solid 1px #808080; background-color: #eef0f0">
                                    <td></td>
                                    <td class="text-right">+ iva: </td>
                                    <td class="text-right">
                                        <asp:Label ID="lblIva" Text='<% #Eval("CIva") %>' runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </div>
                            <div id="lineaGranTotal" runat="server">
                                <tr style="border-top: double 2px #525353; background-color: #e4e6e6">
                                    <td></td>
                                    <td class="text-right f-14">Total: </td>
                                    <td class="text-right">
                                        <strong>
                                            <asp:Label ID="lblGranTotal" Text='<% #Eval("CGTotal") %>' runat="server" CssClass="c-black f-14"></asp:Label></strong>
                                    </td>
                                </tr>
                            </div>
                        </table>

                           <div class="">
                            <div class="form-group">
                                <div class="fg-line">
                                    <label class="fg-label"></label>
                                    <div class="form-group">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="chkPedido"  runat="server" />
                                                <i class="input-helper"></i>
                                                Pedido
                                            </label>
                                            &nbsp;&nbsp;&nbsp;
                                            <label>
                                                 <asp:CheckBox ID="chkVerAlmacen" runat="server" />
                                                 <i class="input-helper"></i>
                                                 Ver almacén
                                             </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="text text-right">
                                    <asp:LinkButton class="btn btn-primary" runat="server" ID="btnImprimir"><i class="fa fa-print"></i> Imprimir</asp:LinkButton>
                                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" OnClick="btnCerrar_Click"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                </div>
                                
                            </div>
                        </div>

                        
                    </div>
                </div>
             
            </div>
        </div>
        <div class="clearfix"></div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

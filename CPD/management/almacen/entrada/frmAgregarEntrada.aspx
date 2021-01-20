<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAgregarEntrada.aspx.vb" Inherits="CPD.frmAgregarEntrada" %>
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
            <li class="active text-uppercase">Agregar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Almacén
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>
                    Agregar entrada
                </h2>
            </div>
            <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Núm. Pedido <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbNumeroPedido" runat="server"  ReadOnly ="true"  class="form-control input-sm"  TabIndex="1" />
                            </div>
                        </div>
                    </div>
                   
                   <div class="col-sm-8">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Proveedor <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:TextBox ID="txbProveedor" runat="server"  ReadOnly ="true"  class="form-control input-sm"  TabIndex="2" />

                                </div>
                            </div>
                    </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="input-group">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Fecha entregada <span class="text-danger">(*)</span></label>
                                                        <asp:TextBox ID="txbFechaFinal" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="3" />
                                                    </div>
                                                    <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
                                                </div>
                    </div>
                   
                    <div class="col-sm-3">
                        <div class="input-group">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Fecha pedido <span class="text-danger">(*)</span></label>
                                                        <asp:TextBox ID="txbFechaPedido" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" ReadOnly ="true"  TabIndex="4" />
                                                    </div>
                                                    <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
                                                </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <div class="fg-line">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                <label class="fg-label">Marque la casilla cuando desee:</label>                               
                                        
                                        <label class="radio radio-inline m-r-20">
                                            <asp:RadioButton runat="server" ID="chkNota" AutoPostBack="true" />
                                            <i class="input-helper"></i>
                                            Nota
                                        </label>
                                        <label class="radio radio-inline m-r-20">
                                            <asp:RadioButton runat="server" ID="chkFactura" AutoPostBack="true" />

                                            <i class="input-helper"></i>
                                            Factura
                                        </label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Número de remisión  <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbRemision" runat="server"  MaxLength="50" class="form-control input-sm" placeholder="123456" TabIndex="5" />
                            </div>
                        </div>
                    </div>
                    
                    </div>
                
                <div class="row">
                    <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Observaciones:</label>
                                                        <asp:TextBox ID="txbObservaciones" runat="server" class="form-control input-sm" Rows="2" TextMode="MultiLine" TabIndex="9" />
                                                    </div>
                                                </div>
                </div>
</div>
            <div class="card-body">
                <h4>
                   Lista de artículos
                </h4>
                <br />
                <br />
                <asp:UpdatePanel runat="server" UpdateMode="Conditional"  ID="updateListView">
                    <ContentTemplate>
                        <asp:ListView ID="lsvListado" runat="server" ItemPlaceholderID="elementoPlaceHolder" >
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>

                                            <th style="width: 5%" class="text-center">Código barras</th>
                                            <th style="width: 5%;" class="text-center">Cant. Pedida</th>
                                            <th style="width: 5%;" class="text-center">Existencia</th>
                                            <th style="width: 5%;" class="text-center">Entrada</th>
                                            <th style="width: 5%;" class="text-center">Cant. Faltante</th>
                                            <th style="width: 30%;" class="text-Center">Artículo</th>
                                            <th style="width: 5%;" class="text-center">Unidad Medida</th>
                                            <th style="width: 8%;" class="text-center">Precio Unitario</th>
                                            <th style="width: 8%;" class="text-center">Subtotal</th>
                                            <th style="width: 8%;" class="text-Center">IVA</th>
                                            <th style="width: 8%;" class="text-center">Costo total</th>
                                            <th style="width: 8%;" class="text-center">Tipo entrada</th>
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
                                        <asp:Label ID="lblCodeBar" Text='<%#Eval("_codigoBarras")%>' ToolTip='<%#Eval("idPedido") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCantidadPedida" Text='<%#Eval("cantidadPedida", "{0:0}")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCantidadRecibida" Text='<%#Eval("_existencia", "{0:0}")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbEntrada" onkeypress="return numero(event)" Tooltip ='<%#Eval("idArticulo") %>' runat="server" TabIndex='<%#Container.DataItemIndex%>' Text="0" class="form-control input-sm soloNumeros"   Width="50" OnTextChanged="txbCantidadRecibida_OnTextChanged" AutoPostBack="true" MaxLength="10" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCantidadFaltante" runat="server"  Text="0"/>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblArticulo" Text='<%#Eval("_articulo")%>' ToolTip='<%#Eval("idArticulo") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUnidadMedida" Text='<%#Eval("_unidadMedidaArticulo")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCostoXUnidad" Text='<%#Eval("precioUnitario", "{0:c}")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubtotal" Text='<%#Eval("_subTotal", "{0:c}")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIva" Text='<%#Eval("_ivaSubtotal", "{0:c}")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCostoTotal" Text='<%#Eval("_total", "{0:c}")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTipoEntrada"  Text= "Parcial" runat="server" />
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
                        
            
            <asp:UpdatePanel runat="server" ID="updateBotonesAddCan">
                                <ContentTemplate>
                                    <div class="row"></div>
                                      <div class="text text-right p-b-25 p-r-25">
                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnAgregar" TabIndex="10"><i class="fa fa-check-circle"></i> Agregar</asp:LinkButton>
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="11"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
                                </ContentTemplate>
            </asp:UpdatePanel>
               
            
        
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
                       $(".listaConFiltro").DataTable();
                   }
               });
           };
</script>

   <script>
       function restar() {
           var resta = 0;
           $('.subtotal').each(function () {
               resta -= Number($(this).val());
           });
           $('#lblCantidadFaltante').html(sum)
       }
   </script>
    
</asp:Content>

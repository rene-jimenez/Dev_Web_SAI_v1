<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmEditarEntrada.aspx.vb" Inherits="CPD.frmEditarEntrada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><asp:LinkButton id="btnPrincipal" Text="text" class="text-uppercase" runat="server">PRINCIPAL</asp:LinkButton></li>
            <li><a href="#" class="text-uppercase">Almacén</a></li>
            <li><a href="#" class="text-uppercase">Entrada</a></li>
            <li class="active text-uppercase">Editar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Almacén
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Editar entrada
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
                                <div class="select">
                                    <asp:Label runat="server" ID="txbProveedor" CssClass="form-control input-sm" TabIndex="5" />
                                    <%--<asp:DropDownList ID="cmbProveedor" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True"  TabIndex="2">
                                        <asp:ListItem Text="Seleccione un elemento de la lista" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>--%>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="input-group">
                            <div class="fg-line">
                                <label class="fg-label">Fecha final <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbFechaFinal" runat="server" class="form-control input-sm" disabled="" TabIndex="3" />
                            </div>
                            <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="input-group">
                            <div class="fg-line">
                                <label class="fg-label">Fecha pedido <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbFechaPedido" runat="server" class="form-control input-sm " disabled="" TabIndex="4" />
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
                                <label class="fg-label">Número de documento <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbNumDocumento" runat="server" MaxLength="6" class="form-control input-sm" placeholder="123456" TabIndex="5" />
                            </div>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-sm-12">
                    <div class="form-group m-b-30">
                        <div class="fg-line">
                            <label class="fg-label">Observaciones:</label>
                            <asp:TextBox ID="txbObservaciones" runat="server" class="form-control input-sm" Rows="2" TextMode="MultiLine" TabIndex="9" />
                        </div>
                    </div>
                        </div>
                </div>
            </div>
            <div class="card-body card-padding">
                <div class="row">
                     <div class="col-sm-12">

                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="updateListView" >
                    <ContentTemplate>
                        <asp:ListView ID="lsvArticulosEntrada" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>

                                            <th style="width: 5%" class="text-center">Código Barras</th>
                                            <th style="width: 5%;" class="text-center">Cant. Pedida</th>
                                            <th style="width: 5%;" class="text-center">Cant. Recibida</th>                                            
                                            <th style="width: 5%;" class="text-center">Cant. Faltante</th>
                                            <th style="width: 30%;" class="text-Center">Artículo</th>
                                            <th style="width: 5%;" class="text-center">Unidad Medida</th>
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
                                     <td class="text-center">
                                        <asp:Label ID="lblCodeBar" Text='<%#Eval("_codigoBarras") %>' runat="server" />
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="lblCantidadPedida" Text='<%#Eval("_cantidadPedido") %>' runat="server" />
                                    </td>
                                    <td>

                                
       <asp:TextBox runat="server" id="txbCantidadRecibida" TabIndex='<%#Container.DataItemIndex%>' Tooltip ='<%#Eval("Id") %>' class="form-control input-sm text-center" width="50" OnTextChanged="txbCantidadRecibida_OnTextChanged" AutoPostBack="true" Text='<%#Eval("cantidad") %>' ClientIDMode="Static" />
                                      
                                        
                                                                </td>                                  
                                    <td class="text-center">
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


            <asp:UpdatePanel runat="server">
                <ContentTemplate>


            <div class="text text-right p-b-25 p-r-25">
                <asp:LinkButton class="btn btn-success" runat="server" ID="btnEditar" TabIndex="10"><i class="fa fa-pencil-square-o"></i> Editar</asp:LinkButton>
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
</asp:Content>

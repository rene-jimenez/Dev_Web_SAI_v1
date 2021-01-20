<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmPedidoAgregar.aspx.vb" Inherits="CPD.frmPedidoAgregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
    <script>
        var nav4 = window.Event ? true : false;
        function acceptNum(evt) {
            var key = nav4 ? evt.which : evt.keyCode;
            return (key <= 13 || (key >= 48 && key <= 57) || key == 40);
        }

    </script>

    <script>
        function validateDecimal(valor) {
            var RE = /^d*(.d{1})?d{0,1}$/;
            if (RE.test(valor)) {
                return true;
            } else {
                return false;
            }
        }

    </script>
    <script>
        function filterFloat(evt, input) {
            // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
            var key = window.Event ? evt.which : evt.keyCode;
            var chark = String.fromCharCode(key);
            var tempValue = input.value + chark;
            if (key >= 48 && key <= 57) {
                if (filter(tempValue) === false) {
                    return false;
                } else {
                    return true;
                }
            } else {
                if (key == 8 || key == 13 || key == 0) {
                    return true;
                } else if (key == 46) {
                    if (filter(tempValue) === false) {
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    return false;
                }
            }
        }
        function filter(__val__) {
            var preg = /^([0-9]+\.?[0-9]{0,6})$/;
            if (preg.test(__val__) === true) {
                return true;
            } else {
                return false;
            }

        }
    </script>
    <script>
        function SoloNumerosDecimales3(e, valInicial, nEntero, nDecimal) {
            var obj = e.srcElement || e.target;
            var tecla_codigo = (document.all) ? e.keyCode : e.which;
            var tecla_valor = String.fromCharCode(tecla_codigo);
            var patron2 = /[\d.]/;
            var control = (tecla_codigo === 46 && (/[.]/).test(obj.value)) ? false : true;
            var existePto = (/[.]/).test(obj.value);

            //el tab
            if (tecla_codigo === 8)
                return true;

            if (valInicial !== obj.value) {
                var TControl = obj.value.length;
                if (existePto === false && tecla_codigo !== 46) {
                    if (TControl === nEntero) {
                        obj.value = obj.value + ".";
                    }
                }

                if (existePto === true) {
                    var subVal = obj.value.substring(obj.value.indexOf(".") + 1, obj.value.length);

                    if (subVal.length > 1) {
                        return false;
                    }
                }

                return patron2.test(tecla_valor) && control;
            }
            else {
                if (valInicial === obj.value) {
                    obj.value = '';
                }
                return patron2.test(tecla_valor) && control;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server" />
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Pedido</a></li>
            <li class="active text-uppercase">Agregar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Pedido
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>
                    <asp:Label runat="server" ID="lblTitulo" Text="Agrega un pedido"></asp:Label>
                </h2>
            </div>
            <div class="card-body card-padding">
                <div class="form-wizard form-wizard-basic form-wizard-horizontal fw-container">
                    <div class="form-wizard-nav">
                        <div class="progress">
                            <div class="progress-bar progress-bar-primary"></div>
                        </div>
                        <ul class="nav nav-justified">
                            <li class="active"><a href="#paso1" data-toggle="tab"><span class="step">1</span> <span class="title">Datos generales</span></a></li>
                            <li><a href="#paso2" data-toggle="tab"><span class="step">2</span> <span class="title">Cuadro de pedido</span></a></li>
                        </ul>
                    </div>
                    <div class="tab-content clearfix">
                      <div class="tab-pane active" id="paso1">
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Turno DRM</label><asp:HiddenField ID="hdfTurnoDRM" runat="server" Value="" />
                                                        <asp:Label ID="lblTurnoDRM" runat="server" class="form-control input-sm" Value="" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Área</label><asp:HiddenField ID="hdfArea" runat="server" Value="" />
                                                        <asp:Label ID="lblArea" runat="server" class="form-control input-sm" value="" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                <div class="input-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Fecha de elaboración: </label>
                                                        <asp:HiddenField ID="hdfFechaElab" runat="server" Value="" />
                                                        <asp:Label ID="lblFechaElaboracion" runat="server" class="form-control input-sm" />
                                                    </div>
                                                    <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Cargo presupuestal:  <span class="text-danger">(*)</span></label>
                                                        <div class="select">
                                                            <asp:DropDownList ID="cmbCargoPresupuestal" runat="server" CssClass="conFiltro" Width="100%" AppendDataBoundItems="True" TabIndex="1">
                                                                <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Partida presupuestal:  <span class="text-danger">(*)</span></label>
                                                        <div class="select">
                                                            <asp:DropDownList ID="cmbPartidaPresupuestal" runat="server" CssClass="conFiltro" Width="100%" AppendDataBoundItems="True" TabIndex="2">
                                                                <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Elaboró:</label>
                                                       
                                                          <asp:Label ID="lblElaboro" runat="server" class="form-control input-sm" value=""/>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Revisó: <span class="text-danger">(*)</span></label>
                                                        <div class="select">
                                                            <asp:DropDownList ID="cmbReviso" runat="server" CssClass="conFiltro" Width="100%" AppendDataBoundItems="True" TabIndex="4">
                                                                <asp:ListItem Text="Seleccione un elemento de la lista" Enabled="true"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Autoriza: <span class="text-danger">(*)</span></label>
                                                        <div class="select">
                                                            <asp:DropDownList ID="cmbAutoriza" runat="server" CssClass="conFiltro" Width="100%" AppendDataBoundItems="True" TabIndex="5">
                                                                <asp:ListItem Text="Seleccione un elemento de la lista" Enabled="true"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row  m-b-30">
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Fecha solicitud a proveedor: <span class="text-danger">(*)</span></label>
                                                        <asp:TextBox ID="txbFechaSolicitud" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                                    </div>
                                                    <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Fecha acordada de entrega: <span class="text-danger">(*)</span></label>
                                                        <asp:TextBox ID="txbFechaAcordadaEntrega" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="7" />
                                                    </div>
                                                    <span class="input-group-addon"><i class="fa fa-calendar" aria-hidden="true"></i></span>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Fecha de recibido: <span class="text-danger">(*)</span></label>
                                                        <asp:TextBox ID="txbFechaRecibido" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="8" />
                                                    </div>
                                                    <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Proveedor: <span class="text-danger">(*)</span></label>
                                                        <div class="select">
                                                            <asp:DropDownList ID="cmbProveedor" runat="server" CssClass="conFiltro" Width="100%" AppendDataBoundItems="True" TabIndex="9">
                                                                <asp:ListItem Text="Seleccione un elemento de la lista" Enabled="true"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Tipo pago: <span class="text-danger">(*)</span></label>
                                                        <div class="select">
                                                            <asp:DropDownList ID="cmbTipoPago" runat="server" CssClass="conFiltro" Width="100%" AppendDataBoundItems="True" TabIndex="10">
                                                                <asp:ListItem Text="Seleccione un elemento de la lista" Enabled="true"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                <div class="form-group">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Marque la casilla cuando desee:</label>
                                                        <div class="form-group">
                                                            <div class="checkbox">
                                                                &nbsp;&nbsp;&nbsp;
                                                     <label>
                                                         <asp:CheckBox ID="chkPedido" runat="server" TabIndex="12" />
                                                         <i class="input-helper"></i>
                                                         Pedido
                                                     </label>
                                                                &nbsp;&nbsp;&nbsp;
                                                    <label>
                                                        <asp:CheckBox ID="chkVerAlmacen" runat="server" TabIndex="13" />
                                                        <i class="input-helper"></i>
                                                        Ver almacén
                                                    </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Observaciones:</label>
                                                        <asp:TextBox ID="txbObservaciones" runat="server" class="form-control input-sm" Rows="2" TextMode="MultiLine" TabIndex="14" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div><!-- termina paso 1 -->
                                                  
                      <div class="tab-pane" id="paso2">
                            <asp:UpdatePanel UpdateMode="Conditional" ID="updateAgregarArticulo" runat="server">
                                <ContentTemplate>
                                    <div class="card col-md-4 col-lg-4" id="divPanelAgregar" runat="server">
                                        <div class="card-header ch-alt bgm-green m-b-20">
                                            <h2>Agregar artículo</h2>
                                        </div>
                                        <div class="card-body p-5">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">@</span>
                                                        <asp:DropDownList ID="cmbArticulo" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True" TabIndex="16">
                                                            <asp:ListItem Text="Seleccione un elemento de la lista" selected="true"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">#</span>
                                                        <asp:TextBox ID="txbCantidad" runat="server" class="form-control input-sm numeric" placeholder="Escribe la cantidad" TabIndex="17" onkeydown = "return thisIsNumber(event);" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">$</span>
                                                        <asp:TextBox ID="txbPrecio" runat="server" class="form-control input-sm" name="entero" placeholder="Escribe el precio" TabIndex="18" onkeypress="return filterFloat(event,this);" maxlength="20" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group m-t-10  text-right">
                                                        <div class="fg-line">

                                                            <asp:LinkButton class="btn btn-primary" runat="server" ID="btnArticulos" OnClick="btnArticulos_Click" TabIndex="23"><i class="fa fa-plus"></i> Agregar articulo</asp:LinkButton>
                                                            <asp:LinkButton class="btn bgm-gray" runat="server" ID="btnCancelarArticulos" OnClick="btnCancelarArticulos_Click" TabIndex="24"><i class="fa fa-cog fa-spin"></i> Ocultar</asp:LinkButton>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel UpdateMode="Conditional" ID="updateCuadroPedido" runat="server">
                                <ContentTemplate>
                                    <div class="card md-12" id="divCuadroPedido" runat="server">
                                        <div class="card-header ch-alt m-b-20">
                                            <h2><asp:Label ID="lblTituloCuadroPedido" runat="server" Text="Cuadro de pedido"></asp:Label></h2>
                                            <button class="btn bgm-green btn-float waves-effect" runat="server" onserverclick="btnMostrarAgregarArticulo_Click" id="btnMostrarAgregarArticulo"><i class="fa fa-plus fa-2x" aria-hidden="true"></i></button>
                                        </div>
                                        <asp:ListView runat="server" ID="lsvCuadroPedido" ItemPlaceholderID="elementoPlaceHolder">
                                            <LayoutTemplate>
                                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                                    <thead class="cf">
                                                        <tr>
                                                            <th data-column-id="id" data-type="numeric" width="10%">Núm.</th>
                                                            <th style="width: 50%;" class="text-left">Articulo</th>
                                                            <th style="width: 10%;" class="text-center">Cantidad</th>
                                                            <th style="width: 20%;" class="text-center">Precio</th>
                                                            <th style="width: 10%;" class="text-center">Acción</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                                    </tbody>


                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="rating-list">
                                                    <td class="text-left">
                                                        <asp:Label ID="lblID" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label>

                                                    </td>
                                                    <td class="text-left">
                                                        <asp:Label ID="lblArticulos" Text='<%#Eval("_articulo")%>' runat="server"></asp:Label>

                                                    </td>
                                                    <td class="text-center">
                                                        <asp:Label ID="lblCantidad" Text='<%#Eval("cantidad")%>' runat="server"></asp:Label>

                                                    </td>
                                                    <td class="text-center">
                                                        <asp:Label ID="lblPrecio" Text='<%#"$ " + Eval("precioUnitario", "{0}")%>' runat="server"></asp:Label>

                                                    </td>
                                                   
                                                    <td class="text-center">
                                                        <ul class="actions">

                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkQuitarArticulo" runat="server" OnClick="lnkQuitarArticulo_Click" TabIndex='<%#Container.DataItemIndex%>' >Quitar</asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkVerStock" runat="server" CommandName='<%#Eval("idArticulo") %>' OnClick="lnkVerStock_Click">Ver stock</asp:LinkButton>
                                                                    </li>
                                                                     <li style="background-color:#f5f5f4">
                                                                        <asp:LinkButton ID="lnkQuitarTodos" runat="server" CommandName='<%#Eval("idArticulo") %>' OnClick="lnkQuitarTodos_Click"><span class="c-red">*</span>Quitar todos</asp:LinkButton>
                                                                    </li>

                                                                </ul>
                                                            </li>
                                                        </ul>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                 <tr class="text-center" >
                                                    <div class="bgm-deeporange p-20">
                                                        <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                                        No existen registros, revisa la información o llama a la Coordinación de Informática, al área de sistemas</h5>
                                                    </div>
                                                </tr>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <table class="table table-striped">
                                            <div id="lineaSubTotal" runat="server">
                                                <tr style="border-top: solid 1.5px #808080; vertical-align: middle; background-color: #ffffff">
                                                    <td colspan="3">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <asp:Panel ID="pnlChkDoM" runat="server">
                                                                    <div class="checkbox">
                                                                        <label>
                                                                            <asp:CheckBox ID="chkDoMaths" runat="server" OnCheckedChanged="chkDoMaths_CheckedChanged" AutoPostBack="true" />
                                                                            <i class="input-helper"></i>
                                                                            Aplicar IVA 
                                                                        </label>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Panel ID="pnlTxbDesc" runat="server">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon">Descuento</span>
                                                                    <asp:TextBox ID="txbDescuento" runat="server" class="form-control input-sm" placeholder="$ 0.00" TabIndex="17" AutoPostBack="true" OnTextChanged="txbDescuento_TextChanged" onkeydown="return thisIsDouble(event);"></asp:TextBox>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                    <td></td>

                                                    <td class="text-right">Subtotal:</td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblSubTotal" Text='' runat="server"></asp:Label>

                                                    </td>

                                                </tr>

                                </div>
                                  <div id="lineaDescuento" runat="server">
                                    <tr style="border-top: solid 1px #808080; background-color:#f9f9f9">

                                        <td colspan="3"></td>
                                        <td></td>

                                        <td class="text-right">- descuento:</td>
                                        <td class="text-right">
                                            <asp:Label ID="lblDescuento" Text='<% #Eval("CDesc")%>' runat="server" ></asp:Label>

                                        </td>


                                    </tr>

                                </div>
                               <div id="lineaIva" runat="server">
                                    <tr style="border-top: solid 1px #808080; background-color: #eef0f0">

                                        <td colspan="3"></td>
                                        <td></td>

                                        <td class="text-right">+ iva:</td>
                                        <td class="text-right">
                                            <asp:Label ID="lblIva" Text='<% #Eval("CIva")%>' runat="server"></asp:Label>

                                        </td>

                                    </tr>

                                </div>
                                <div id="lineaGranTotal" runat="server">
                                    <tr style="border-top: double 2px #525353; background-color:#f7f7f7">

                                        <td colspan="3"></td>
                                        <td></td>

                                        <td class="text-right f-14">Total:</td>
                                        <td class="text-right">
                                            <strong><asp:Label ID="lblGranTotal" Text='<% #Eval("CGTotal")%>' runat="server" CssClass="c-black"></asp:Label></strong>

                                        </td>


                                    </tr>

                                </div>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel runat="server" ID="updateBotonesAddCan">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="text text-right p-b-25 p-r-25">
                                            <asp:LinkButton class="btn btn-success" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click" TabIndex="25"><i class="fa fa-check-circle"></i> Agregar pedido</asp:LinkButton>
                                            <asp:LinkButton class="btn btn-default" runat="server" ID="btnCancelar" OnClick="btnCancelar_Click" TabIndex="26"><i class="fa fa-cog fa-spin"></i> Cancelar</asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            
                        </div><!-- termina paso2 -->

                    </div><!-- termina tab content -->
                    <div class="text text-center">
                        <ul class="fw-footer pagination wizard text-center">
                                <li class="previous first"><a class="a-prevent" href=""><i><span class="zmdi zmdi-more-horiz"></span></i></a></li>
                        <li class="previous"><a class="a-prevent" href=""><i><span class="zmdi zmdi-long-arrow-left"></span></i></a></li>
                        <li class="next"><a class="a-prevent" href=""><i><span class="zmdi zmdi-long-arrow-right"></span></i></a></li>
                        <li class="next last"><a class="a-prevent" href=""><i><span class="zmdi zmdi-more-horiz"></span></i></a></li>
                                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
      
    <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".conMascaraFecha").inputmask("99/99/9999");
                       $(".conFiltro").select2();
                   }
               });
           };
</script>
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>


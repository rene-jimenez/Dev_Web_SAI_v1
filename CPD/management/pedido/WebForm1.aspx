<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="WebForm1.aspx.vb" Inherits="CPD.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
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
                    <asp:Label runat="server" ID="lblTitulo"></asp:Label>Agrega un pedido</h2>
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
                                            <label class="fg-label">Turno DRM</label>
                                            <asp:TextBox ID="txbTurnoDRM" runat="server" class="form-control input-sm" ReadOnly="true" disabled />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Área</label>
                                            <asp:TextBox ID="txbArea" runat="server" class="form-control input-sm" ReadOnly="true" disabled />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Fecha de elaboración</label>
                                            <asp:TextBox ID="txbFechaElaboracion" runat="server" class="form-control input-sm" ReadOnly="true" disabled />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Cargo presupuestal</label>
                                            <div class="select">
                                                <asp:DropDownList ID="cmbCargoPresupuestal" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                                    <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Partida presupuestal</label>
                                            <div class="select">
                                                <asp:DropDownList ID="cmbPartidaPresupuestal" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                                    <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Mes afectar</label>
                                            <asp:TextBox ID="txbMesAfectar" runat="server" class="form-control input-sm" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Elaboró</label>
                                            <div class="select">
                                                <asp:DropDownList ID="cmbElaboro" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                                    <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Revisó</label>
                                            <div class="select">
                                                <asp:DropDownList ID="cmbReviso" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                                    <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Autoriza</label>
                                            <div class="select">
                                                <asp:DropDownList ID="cmbAutoriza" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
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
                                            <label class="fg-label">Fecha solicitud a proveedor</label>

                                            <div class="input-group">

                                                <asp:TextBox ID="txbFechaSolicitud" runat="server" class="form-control input-sm" />
                                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                                            </div>



                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Fecha acordada de entrega</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txbFechaAcordadaEntrega" runat="server" class="form-control input-sm" />
                                                <span class="input-group-addon"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Fecha de recibido</label>
                                            <asp:TextBox ID="txbFechaRecibido" runat="server" class="form-control input-sm" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Proveedor</label>
                                            <div class="select">
                                                <asp:DropDownList ID="cmbProveedor" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                                    <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Tipo pago</label>
                                            <div class="select">
                                                <asp:DropDownList ID="cmbTipoPago" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                                    <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
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
                                                    <label>
                                                        <asp:CheckBox ID="chkIva" runat="server" />
                                                        <i class="input-helper"></i>
                                                        Aplicar IVA 
                                                    </label>
                                                    &nbsp;&nbsp;&nbsp;
                                                     <label>
                                                         <asp:CheckBox ID="chkPedido" runat="server" />
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
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Observaciones</label>
                                            <asp:TextBox ID="TextBox2" runat="server" class="form-control input-sm" Rows="2" TextMode="MultiLine" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="paso2">
                            <asp:UpdatePanel UpdateMode="Conditional" ID="updateAgregarArticulo" runat="server">
                                <ContentTemplate>
                                    <div class="card col-md-4 col-lg-4" id="divPanelAgregar" visible="false" runat="server">
                                        <div class="card-header ch-alt bgm-green m-b-20">
                                            <h2>Agregar artículo</h2>
                                        </div>
                                        <div class="card-body p-5">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">@</span>
                                                        <asp:DropDownList ID="cmbArticulo" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                                            <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<input type="text" class="form-control" placeholder="Seleccione el articulo">--%>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">#</span>
                                                        <asp:TextBox ID="txbCantidad" runat="server" class="form-control input-sm" placeholder="Escribe la cantidad"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">$</span>
                                                        <asp:TextBox ID="txbPrecio" runat="server" class="form-control input-sm" placeholder="$1,000,000.00"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 ">
                                                    <div class="form-group m-t-10  text-right">
                                                        <div class="fg-line">

                                                            <asp:LinkButton class="btn btn-success" runat="server" ID="btnAgregarArticulos"><i class="fa fa-check-circle"></i> Agregar articulo</asp:LinkButton>
                                                            <asp:LinkButton class="btn bgm-gray" runat="server" ID="btnCancelarAddArt"><i class="fa fa-cog fa-spin"></i> Cancelar</asp:LinkButton>
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
                                    <div class="card col-md-12 col-lg-12" id="divCuadroPedido" runat="server">
                                        <div class="card-header ch-alt m-b-20">
                                            <h2>Cuadro de pedido</h2>
                                            <button class="btn bgm-green btn-float waves-effect" runat="server" id="btnMostrarAgregarArticulo"><i class="fa fa-plus fa-2x" aria-hidden="true"></i></button>
                                        </div>
                                        <asp:ListView runat="server" ID="lsvCuadroPedido" ItemPlaceholderID="elementoPlaceHolder">
                                            <LayoutTemplate>
                                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                                    <thead class="cf">
                                                        <tr>
                                                            <th data-column-id="id" data-type="numeric" width="10%">ID</th>
                                                            <th style="width: 50%;" class="text-left">Articulo</th>
                                                            <th style="width: 10%;" class="text-center">Cantidad</th>
                                                            <th style="width: 10%;" class="text-center">Precio</th>
                                                            <th style="width: 20%;" class="text-center">Total</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="rating-list">
                                                    <td>
                                                        <asp:Label ID="lblID" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblArticulos" Text="text" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblCantidad" Text='' runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblPrecio" Text='' runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblTotal" Text='' runat="server"></asp:Label></td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <div class="alert alert-danger">
                                                        <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                                        Lo sentimos, No hay registros
                                                    </div>
                                                </tr>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                            <div class="text text-right p-b-25 p-r-25">
                                <asp:LinkButton class="btn btn-success" runat="server" ID="btnAgregar"><i class="fa fa-check-circle"></i> Agregar pedido</asp:LinkButton>
                                <asp:LinkButton class="btn btn-default" runat="server" ID="btnCancelar"><i class="fa fa-cog fa-spin"></i> Cancelar</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="text text-center">
                        <ul class="fw-footer pagination wizard text-center">
                            <li class="previous first"><a class="a-prevent" href=""><i><span class="fa fa-step-backward"></span></i></a></li>
                            <li class="previous"><a class="a-prevent" href=""><i><span class="fa fa-backward"></span></i></a></li>
                            <li class="next"><a class="a-prevent" href=""><i><span class="fa fa-forward"></span></i></a></li>
                            <li class="next last"><a class="a-prevent" href=""><i><span class="fa fa-step-forward"></span></i></a></li>
                        </ul>
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

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmListaPedidosAfectacion.aspx.vb" Inherits="CPD.frmListaPedidosAfectacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="#" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">PEDIDOS</a></li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">PEDIDOS PARA AFECTACIÓN</h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Lista de pedidos</h2>
            </div>
            <div class="card-body card-padding">
     
                        <asp:ListView ID="lsvPedidos" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>

                                            <th data-column-id="id" data-type="numeric" width="5%">ID</th>
                                            <th style="width: 10%;" class="text-center">N° Pedido</th>
                                            <th style="width: 35%;" class="text-left">Proveedor</th>
                                            <th style="width: 20%;" class="text-center">Pedido</th>
                                            <th style="width: 10%;" class="text-center">T. pago</th>
                                            <th style="width: 10%;" class="text-center">Fecha</th>                                            
                                            <th style="width: 10%;" class="text-center">Axn</th>
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
                                        <asp:Label ID="lblIDLista" Text='<%#Container.DataItemIndex + 1 %> ' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label id="lblNumPedido" Text='<%#Eval("numeroPedido")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProveedor" Text='<%#Eval("_nombreProveedor")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNombrePedido" Text='<%#Eval("_nombrePartida")%>' runat="server" />
                                    </td>
                                    
                                    <td>
                                        <asp:Label ID="lblTipoPago" Text='<%#Eval("_nombreTipoPago")%>' runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblFechaPAgo" Text='<%#Eval("fechaElaboracion")%>' runat="server" />
                                    </td>
                                    <td style="text-align: center">

                                          <asp:LinkButton runat="server" ID="btnSeleccionar" OnClick="btnSeleccionar_OnClick" CommandArgument='<%#Eval("id")%>' TabIndex='<%#Container.DataItemIndex %>' CssClass="btn btn-icon command-edit waves-effect waves-circle  waves-float bgm-bluegray">
												<span class="zmdi zmdi-eye"></span>
                                        </asp:LinkButton>

                                      <%--  <ul class="actions">

                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkAgregar" runat="server" OnClick="lnkAgregar_Click" CommandName='<%#Eval("id") %>' >Agregar Afectación</asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkModificar" runat="server" OnClick="lnkModificar_Click" CommandName='<%#Eval("id") %>'>Modificar Afectación</asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkConsultar" runat="server" OnClick="lnkConsultar_Click" CommandName='<%#Eval("id") %>'>Consultar Afectación</asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkSustituir" runat="server"    OnClick="lnkSustituir_Click" CommandName='<%#Eval("id") %>'>Sustituir Afectación</asp:LinkButton>
                                                                    </li>

                                                                </ul>
                                                            </li>
                                                        </ul>--%>
                                        
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                 <tr class="bgm-deeporange">
                                                  
                                     <div class="alert alert-danger alert-dismissible" role="alert">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                Atención! <a href="" class="alert-link">La consulta no arrojo pedidos.</a> 
                                                    </div>
                                     
                         
                                                </tr>
                            </EmptyDataTemplate>
                        </asp:ListView>

            <div class="col-sm-12 text-right">
            <div class="form-group m-b-10">
                <div class="fg-line">                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnSalir" OnClick="btnSalir_Click"><i class="fa fa-cog fa-spin"></i> Salir</asp:LinkButton>
                </div>
            </div>
        </div>


            </div>
  <br /><br />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

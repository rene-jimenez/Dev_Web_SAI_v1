<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmResultadoBusquedaArt.aspx.vb" Inherits="CPD.frmResultadoBusquedaArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a  class="text-uppercase">ARTÍCULO</a></li>
            <li class="active text-uppercase">RESULTADO</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">RESULTADO BUSQUEDA ARTÍCULO
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Artículos encontrados</h2>
            </div>
            <div class="card-body card-padding">
                <div class="card-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:ListView ID="lvsResultadoBusquedaArticulo" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                                <LayoutTemplate>
                                    <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                        <thead class="cf">
                                            <tr>
                                                <th data-column-id="id" data-type="numeric">NÚM.</th>
                                                <th style="width: 40%;" class="text-left">Nombre</th>
                                                <th style="width: 10%;" class="text-center">Existencia</th>
                                                <th style="width: 15%;" class="text-left">Categoria</th>
                                                <th style="width: 10%;" class="text-center">Estatus</th>
                                                <th style="width: 20%;" class="text-center">Acción</th>
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
                                            <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label>
                                        </td>
                                        <td>
                                             <asp:LinkButton runat="server" ID="lblNombre" ClientIDMode="AutoID" Text='<%#Eval("nombre")%>' Enabled="False"></asp:LinkButton>                                       
                                        </td>
                                        <td>
                                            <asp:Label ID="lblExistencia" runat="server" Text='<%#Eval("existencia")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCategoria" runat="server" Text='<%#Eval("nombreCategoria")%>'></asp:Label>
                                        </td>

                                        <td class="rl-star text-center"><%# If(Eval("esActivo"), "<span class='btn-icon-text'><i class='zmdi zmdi-power active'></i> Activo</span>", "<span class='btn-icon-text'><i class='zmdi zmdi-power zmdi-hc-rotate-180 c-gray'></i> Inactivo</span>") %></td>
                                        <td runat="server" visible='<%# IIf(Eval("esActivo"), True, False) %>' class="text-center">
                                            <ul class="actions">

                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                         <asp:LinkButton ID="btnEditar" runat="server" OnClick="btnEditar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-border-color"></span> Editar</asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                         <asp:LinkButton ID="btnActivoDesactivar" runat="server" OnClick="btnActivoDesactivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-power zmdi-hc-rotate-180""></span> Desactivar</asp:LinkButton>
                                                                    </li>
                                                             </li>
                                            </ul>                                                                                 
                                        </td>
                                        <td runat="server" visible='<%# IIf(Convert.ToBoolean(Eval("esActivo")), False, True) %>' class="text-center">
                                           <ul class="actions">
                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>
                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                         <asp:LinkButton ID="btnDesactivoActivar" runat="server" OnClick="btnDesactivoActivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-power"></span> Activar</asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </li>
                                           </ul>                                                                                       
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div class="alert alert-danger">No hay registros</div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                    <div class="col-sm-12 text-right">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <asp:LinkButton class="btn btn-primary" runat="server" ID="btnAgregar"><i class="fa fa fa-plus-circle"></i> Agregar</asp:LinkButton>
                                <asp:LinkButton class="btn bgm-gray" runat="server" ID="btnBuscar"><i class="fa fa fa-reply"></i> Regresar</asp:LinkButton>
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

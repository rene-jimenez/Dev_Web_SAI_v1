<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmCatalogoEstatico.aspx.vb" Inherits="CPD.frmCatalogoEstatico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a class="active text-uppercase">CATÁLOGOS ESTATICOS</a></li>

        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">
                <asp:Label runat="server" ID="tituloA"></asp:Label></h2>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-md-7 col-sm-12">
                <div class="card" runat="server" id="cardlista">
                    <div class="card-header ch-alt bgm-bluegray">
                        <h2>Listado de
                            <asp:Label runat="server" ID="tituloB"></asp:Label></h2>
                        <button class="btn bgm-green btn-float waves-effect" runat="server" onserverclick="btnANuevo_Click" id="btnANuevo"><i class="fa fa-plus fa-2x" aria-hidden="true"></i></button>
                    </div>
                    <div class="card-body">
                        <asp:UpdatePanel runat="server" ID="nuevo">
                            <ContentTemplate>
                                <asp:ListView ID="lsvCatalogos" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                                    <LayoutTemplate>
                                        <table id="data-table" class="table table-striped table-vmiddle">
                                            <thead>
                                                <tr>
                                                    <th style="width: 15%;" class="text-left">Num</th>
                                                    <th class="text-left" width="45%">Nombre</th>
                                                    <th data-column-id="status" width="20%">Estatus</th>
                                                    <th class="text-right" width="20%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="rating-list">
                                            <td style="text-align: center">
                                                <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex + 1%>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("nombre") %>'></asp:Label>
                                            </td>
                                            <td class="rl-star text-center">
                                                <%# IIf(Eval("esActivo"), "<span class='btn-icon-text'><i class='zmdi zmdi-power active'></i> Activo </span>", "<span class='btn-icon-text'><i class='zmdi zmdi-power zmdi-hc-rotate-180 c-gray'></i> Inactivo</span>") %>
                                                <asp:Label ID="lblEstatus" runat="server" Visible="false" Text='<%#Eval("esActivo")%>'></asp:Label>
                                            </td>
                                            <td id="tdAccion" runat="server" style="text-align: center">
                                                <ul class="actions">
                                                    <li class="dropdown">
                                                        <a href="" data-toggle="dropdown" aria-expanded="false">
                                                            <i class="zmdi zmdi-more-vert"></i>
                                                        </a>
                                                        <ul class="dropdown-menu dropdown-menu-right">
                                                            <li>
                                                                <asp:LinkButton ID="btnEditar" runat="server" OnClick="btnEditar_OnClick" ClientIDMode="AutoID" CommandArgument='<%#Eval("id")%>'><span class="zmdi zmdi-border-color"></span> Editar</asp:LinkButton>
                                                            </li>
                                                            <li>
                                                                <asp:LinkButton ID="btnDesactivar" ClientIDMode="AutoID" runat="server" CommandArgument='<%#Eval("id")%>' OnClick="btnDesactivar_OnClick" TabIndex='<%#Container.DataItemIndex%>'><span class='<%#IIf(Eval("esActivo"), "zmdi zmdi-power zmdi-hc-rotate-180", "zmdi zmdi-power active")%>'></span> <%# If(Eval("esActivo"), "Desactivar", "Activar") %></asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                                <%--<asp:LinkButton ID="btnEditar" ClientIDMode="AutoID" runat="server" CommandArgument='<%#Eval("id")%>' ToolTip="Editar" OnClick="btnEditar_OnClick" TabIndex='<%#Container.DataItemIndex%>' visible='<%# IIf(Eval("esActivo"), True, False) %>' CssClass="btn command-edit bgm-orange">
                                                <span class="zmdi zmdi-edit"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDesactivar" ClientIDMode="AutoID" runat="server" CommandArgument='<%#Eval("id")%>' ToolTip="Cambiar Estado" OnClick="btnDesactivar_OnClick" TabIndex='<%#Container.DataItemIndex%>' CssClass='<%#IIf(Eval("esActivo"), "btn bgm-bluegray", "btn bgm-gray")%>'>
                                                <span class='<%#IIf(Eval("esActivo"), "zmdi zmdi-power active", "zmdi zmdi-power zmdi-hc-rotate-180")%>'></span>
                                                </asp:LinkButton>--%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <div class="alert alert-danger">No hay registros</div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                </div>
                </div>
            </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel runat="server" ClientIDMode="AutoID" ID="updateAregar" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-5 col-sm-12" runat="server" id="divLateral">
                                    <div class="card">
                                        <div class="card-header ch-alt bgm-bluegray">
                                            <h2>
                                                <asp:Label ID="lblTitulo" runat="server"></asp:Label></h2>
                                        </div>

                                        <div class="card-body card-padding">
                                            <div class="row">

                                                <div class="form-group m-b-30">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Nombre</label>
                                                        <asp:TextBox ID="txbNombre" runat="server" class="form-control input-sm" placeholder="Escríbe el nombre" />
                                                        <asp:HiddenField ID="lblHiddenId" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="m-t-10">
                                                <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                                                <asp:LinkButton class="btn btn-success" runat="server" ID="btnActualizar"><span class="glyphicon glyphicon-pencil"></span> Actualizar</asp:LinkButton>
                                                <asp:LinkButton class="btn btn-default" runat="server" ID="btnCancelar"><i class="fa fa-times"></i> Cancelar</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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

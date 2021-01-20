<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmPrincipalFirma.aspx.vb" Inherits="CPD.frmPrincipalFirma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="#" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">FIRMA</a></li>
            <li class="active text-uppercase">CONSULTA</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">CONSULTA FIRMAS
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Lista de firmas</h2>
            </div>
            <div class="card-body card-padding">
                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                    <thead>
                        <tr>
                            <th data-column-id="id" data-type="numeric">ID</th>
                            <th data-column-id="tipo">Nombre</th>                            
                            <th data-column-id="fecha" data-order="desc">Usuario</th>
                            <th data-column-id="estado" data-order="desc">Estado</th>
                            <th data-column-id="commands" data-formatter="commands" data-sortable="false">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>1</td>
                            <td>Firma 1</td>                            
                            <td>Usuario</td>
                            <td><span class="label label-primary">Activo -<span class="glyphicon glyphicon-trash"></span></span></td>
                            <td>
                                <button type="button" class="btn btn-icon command-edit waves-effect waves-circle waves-float"><span class="zmdi zmdi-eye"></span></button>
                                <button type="button" class="btn btn-icon command-edit waves-effect waves-circle  waves-float bgm-blue"><span class="zmdi zmdi-edit"></span></button>
                                <button type="button" class="btn btn-icon command-delete waves-effect waves-circle  waves-float bgm-deeporange"><span class="zmdi zmdi-delete"></span></button>
                            </td>
                        </tr>

                        <tr>
                            <td>2</td>
                            <td>Firma 2</td>                          
                            <td>Usuario</td>
                            <td><span class="label label-default">Inactivo -<span class="glyphicon glyphicon-check"></span></span></td>
                            <td>
                                <button type="button" class="btn btn-icon command-edit waves-effect waves-circle  waves-float"><span class="zmdi zmdi-eye"></span></button>
                                <button type="button" class="btn btn-icon command-edit waves-effect waves-circle waves-float"><span class="zmdi zmdi-edit"></span></button>
                                <button type="button" class="btn btn-icon command-delete waves-effect waves-circle  waves-float bgm-gray"><span class="zmdi zmdi-redo"></span></button>
                            </td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td>Firma 3</td>                           
                            <td>Usuario</td>
                            <td><span class="label label-primary">Activo -<span class="glyphicon glyphicon-trash"></span></span></td>
                            <td>
                                <button type="button" class="btn btn-icon command-edit waves-effect waves-circle waves-float"><span class="zmdi zmdi-eye"></span></button>
                                <button type="button" class="btn btn-icon command-edit waves-effect waves-circle  waves-float bgm-blue"><span class="zmdi zmdi-edit"></span></button>
                                <button type="button" class="btn btn-icon command-delete waves-effect waves-circle  waves-float bgm-deeporange"><span class="zmdi zmdi-delete"></span></button>
                            </td>
                        </tr>
                        <tr>
                            <td>4</td>
                            <td>Firma 4</td>                            
                            <td>Usuario</td>
                            <td><span class="label label-primary">Activo -<span class="glyphicon glyphicon-trash"></span></span></td>
                            <td>
                                <button type="button" class="btn btn-icon command-edit waves-effect waves-circle waves-float"><span class="zmdi zmdi-eye"></span></button>
                                <button type="button" class="btn btn-icon command-edit waves-effect waves-circle  waves-float bgm-blue"><span class="zmdi zmdi-edit"></span></button>
                                <button type="button" class="btn btn-icon command-delete waves-effect waves-circle  waves-float bgm-deeporange"><span class="zmdi zmdi-delete"></span></button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="col-sm-12 text-right">
            <div class="form-group m-b-30">
                <div class="fg-line">
                    <%--<asp:LinkButton class="btn btn-default" runat="server" ID="btnAgregar"><i class="fa fa fa-plus-square-o"></i> Agregar nuevo artículo</asp:LinkButton>--%>
                    <%--<asp:LinkButton class="btn btn-warning" runat="server" ID="btnBuscar"><i class="fa fa fa-search"></i> Nueva busqueda</asp:LinkButton>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

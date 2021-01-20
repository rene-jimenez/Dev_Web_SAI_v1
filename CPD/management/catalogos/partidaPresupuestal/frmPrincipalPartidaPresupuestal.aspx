﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmPrincipalPartidaPresupuestal.aspx.vb" Inherits="CPD.frmPrincipalPartidaPresupuestal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
     <div class="container">
        <ol class="breadcrumb">
            <li><a href="../../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a  class="active text-uppercase">PARTIDA PRESUPUESTAL</a></li>

        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">AGREGAR/EDITAR
            </h2>
        </div>
        <div class="clearfix"></div>

        <div class="row">
            <div class="col-md-7 col-sm-12">
                <div class="card" id="cardlista" runat="server">
                    <div class="card-header ch-alt bgm-bluegray">
                        <h2>Listado</h2>
                    </div>


                    <div class="card-body" >
                        <asp:ListView ID="lvListaPartida" runat="server" ItemPlaceholderID="elemento">
                            <LayoutTemplate>
                                <table id="data-table" class="table table-striped table-vmiddle">
                            <thead>
                                <tr>
                                    <th data-column-id="id" data-type="numeric" width="5%">Núm.</th>
                                    <th data-column-id="nombre" width="55%">Nombre</th>
                                    <th data-column-id="numero" width="10%">Número</th>
                                    <th data-column-id="status" width="10%" class="text-center">Estado</th>
                                    <th data-column-id="commands" width="20%" class="text-center">Acción</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:PlaceHolder ID="elemento" runat="server" />
                            </tbody>
                        </table>
                            </LayoutTemplate>
                            <ItemTemplate>

                                <tr class="rating-list">
                                    <td><asp:Label ID="lbl_lv_ID" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label></td>
                                    <td><asp:Label ID="lbl_lv_Nombre" runat="server" Text='<%#Eval("nombre")%>'></asp:Label></td>
                                    <td><asp:Label ID="lbl_lv_Numero" runat="server" Text='<%#Eval("numero")%>'></asp:Label></td>
                                    <td class="rl-star text-center"><%# If(Eval("esActivo"), "<span class='btn-icon-text'><i class='zmdi zmdi-power active'></i> Activo</span>", "<span class='btn-icon-text'><i class='zmdi zmdi-power zmdi-hc-rotate-180 c-gray'></i> Inactivo</span>") %></td>
                                    <td runat="server" visible='<%# IIf(Eval("esActivo"), True, False) %>' class="text-center">
                                        <ul class="actions">
                                            <li class="dropdown">
                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                    <i class="zmdi zmdi-more-vert"></i>
                                                </a>
                                                <ul class="dropdown-menu dropdown-menu-right">
                                                    <li>
                                                        <asp:LinkButton ID="btnActivoDesactivar" runat="server" OnClick="btnActivoDesactivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-power zmdi-hc-rotate-180""></span> Desativar </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="btnActivoEditar" runat="server" OnClick="btnActivoEditar_Click" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-border-color"></span> Editar </asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                        <%--<asp:LinkButton ID="btnActivoEditar" runat="server" OnClick="btnActivoEditar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>' CssClass="btn command-edit bgm-orange"><span class="zmdi zmdi-border-color"></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnActivoDesactivar" runat="server" OnClick="btnActivoDesactivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>' CssClass="btn bgm-bluegray"><span class="zmdi zmdi-power zmdi-hc-rotate-180"></span></asp:LinkButton>--%>
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
                                        <%--<asp:LinkButton ID="btnDesactivoActivar" runat="server" OnClick="btnDesactivoActivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>' CssClass="btn bgm-gray"><span class="zmdi zmdi-power"></span></asp:LinkButton>--%>

                                    </td>
                                </tr>

                            </ItemTemplate>
                            <EmptyItemTemplate>

                            </EmptyItemTemplate>
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

                </div>
            </div>
            <div class="col-md-5 col-sm-12">
                <div class="card" id="cardForm" runat="server">
                    <div class="card-header ch-alt bgm-bluegray">
                        <h2><asp:label id="lblTitleCardNuevo" runat="server"></asp:label></h2>
                    </div>

                    <div class="card-body card-padding">
                        <div class="row">

                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <label class="fg-label">Nombre partida</label>
                                    <asp:TextBox ID="txbNombre" runat="server" class="form-control input-sm" placeholder="Nombre partida" />
                                    <asp:HiddenField id="lblHiddenId" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <label class="fg-label">Número partida</label>
                                    <asp:TextBox ID="txbNumero" runat="server" class="form-control input-sm" MaxLength="4" placeholder="Número partida" onkeydown = "return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);" />
                                </div>
                            </div>
                        </div>
                        <div class="  m-t-10">
                            <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar"><span class="fa fa-check-circle"></span> Guardar</asp:LinkButton>
                            <asp:LinkButton class="btn btn-success" runat="server" ID="btnActualizar"><span class="glyphicon glyphicon-pencil"></span> Actualizar</asp:LinkButton>
                            <asp:LinkButton class="btn btn-default" runat="server" ID="btnCancelar" OnClientClick="k"><span class="fa fa-cog fa-spin"></span> Cerrar</asp:LinkButton>
                        </div>
                    </div>
            </div>
        </div>
    </div>


    </div>

    <div class="modal fade" id="preventClick" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">

                    <div class="block-header">
                        <h2>HEY!</h2>
                        
                    </div>
                </div>
                <div class="modal-body">                    
                   
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="lnkCerrar" CssClass="btn btn-link" >Cerrar modal</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

     <script type="text/javascript">
        function HideModal()
        {
            $('#preventClick').modal("hide");
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function showModal()
        {
            $('#preventClick').modal({ backdrop: 'static', 'keyboard': true, 'show': true });
        }
       
        with (Sys.WebForms.PageRequestManager.getInstance()) {
            add_endRequest(carga);
        }
       

        
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">

   

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
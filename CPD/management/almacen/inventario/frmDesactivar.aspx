<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmDesactivar.aspx.vb" Inherits="CPD.frmDesactivar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
    <div class="modal fade" id="myModalConfirm" tabindex="-2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type:"button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <span class="text text-info"><h4 class="text-info">Confirmación</h4></span>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="grid simple horizontal gray" id="divRespuesta" runat="server">
                            <div class="grid-title">
                                <asp:Label ID="lblConfirmacionCuerpo" runat="server"/> 
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="text-right" style="padding:25px;">
                                <asp:linkButton ID="btnEventoConfirmar" runat="server" class="btn btn-success btn-sm" OnClick="btnEventoConfirmar_Click" OnClientClick="HideModal()"><i class="fa fa-thumbs-up"></i> Aceptar</asp:linkButton>
								<asp:linkButton ID="btnCerrarConfirmacíon" runat="server" class="btn btn-default btn-sm"  data-dismiss="modal" aria-hidden="true" OnClientClick="HideModal()"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:linkButton>
                                <asp:HiddenField ID="idEliminar" runat="server" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>               
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">

    <div class="container">
    <ol class="breadcrumb">
            <li><a href="~/default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Almacén</a></li>
            <li><a href="#" class="text-uppercase">Inventario</a></li>
            <li class="active text-uppercase">Desactivar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Almacén
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>
                    Desactivar inventario
                </h2>
            </div>
            <div class="card-body">

           
                <asp:UpdatePanel UpdateMode="Conditional" ID="updateArticulo" runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lsvListado" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>
                                             <th style="width: 10%;" class="text-center">Código Barras</th>
                                            <th style="width: 35%;" class="text-center">Artículo</th>
                                            <th style="width: 10%;" class="text-center">Stock mínimo</th>
                                            <th style="width: 10%;" class="text-center">Stock máximo</th>
                                            <th style="width: 10%;" class="text-center">Cantidad inicial</th>
                                            <th style="width: 9%;" class="text-center">Unidad medida</th>
                                            <th style="width: 10%;" class="text-center">Existencia</th>
                                         <%--   <th style="width: 27%;" class="text-center">Proveedor</th> --%>                                          
                                            <th style="width: 6%;" class="text-center">Acción</th>
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
                                        <asp:Label ID="lblCodebar" Text='<%#Eval("codigoBarras")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblArticulo" Text='<%#Eval("nombre")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblStockMinimo" Text='<%#Eval("stockMinimo")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblStockMaximo" Text='<%#Eval("stockMaximo")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblCantidadInicial" Text='<%#Eval("cantidadInicial")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblUnidadMedida" Text='<%#Eval("nombreUnidadMedida")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblExistencia" Text='<%#Eval("existencia")%>'  runat="server" />
                                    </td>
                                    <td id="td1" runat ="server" style="text-align: right">
                                        <ul class="actions">

                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkDesactivar" runat="server" ClientIDMode="AutoID" OnClick="lnkDesactivar_Click" CommandArgument='<%#Eval("id")%>' TabIndex='<%#Container.DataItemIndex %>'><span class="zmdi zmdi-power zmdi-hc-rotate-180"></span>Desactivar</asp:LinkButton>
                                                                    </li>

                                                                </ul>
                                                            </li>
                                                        </ul>
                                        
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                 <tr class="bgm-deeporange">
                                                    <div>
                                                        <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                                        No existen registros, revisa la información o llama a la Coordinación de Informática, al área de Sistemas
                                                    </div>
                                                </tr>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class ="card-footer">
                <div class="col-sm-12 text-right">
            <div class="form-group m-b-10">
                <div class="fg-line">                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnSalir"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
            </div>
          <br />
            <br />     
              <br />   
        </div>       
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
    <script type="text/javascript" >
        function HideModal() {
            $('#preventClick').modal("hide");
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function showModal() {
            $('#preventClick').modal({ backdrop: 'static', 'keyboard': true, 'show': true });
        }

        with (Sys.WebForms.PageRequestManager.getInstance()) {
            add_endRequest(carga);
        }
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

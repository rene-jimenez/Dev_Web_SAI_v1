<%@ Page Language="vb"  MasterPageFile="~/management/Site.Master" AutoEventWireup="false" CodeBehind="frmConsultaArea.aspx.vb" Inherits="CPD.frmConsultaArea" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
        <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a class="text-uppercase">ÁREAS</a></li>
            <li class="active text-uppercase">CONSULTA</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">CONSULTA ÁREAS
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-md-12 col-sm-12">
                <div class="card" id="cardlista" runat="server">
                    <div class="card-header ch-alt bgm-bluegray">
                        <h2>Listado de áreas</h2>
                    </div>
                    <div class="card-body card-padding">
                        <div class="card-body">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:ListView ID="lsvLista" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                                <LayoutTemplate>
                                    <table class="table table-striped table-vmiddle listaConFiltro" id="filtro">
                                        <thead>
                                            <tr>
                                                <th style="width: 5%;" class="text-left">Núm.</th>
                                                <th style="width: 30%;" class="text-left">Nombre</th>
                                                <th style="width: 15%;" class="text-left">Código</th>
                                                <th style="width: 10%;" class="text-left">Tipo</th>
                                                <th style="width: 10%;" class="text-left">Jerarquía</th>
                                                <th style="width: 20%;" class="text-center">Estado</th>
                                                <th style="width: 10%;" class="text-right">Acción</th>
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
                                            <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("nombre")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCiudad" Text='<%#Eval("codigo") %>' runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDomicilio" Text='<%#Eval("tipo") %>' runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblJerarquia" Text='<%#Eval("jerarquia") %>' runat="server"></asp:Label>
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
                                            
                                            <asp:LinkButton ID="btnActivoDesactivar" runat="server" OnClick="btnActivoDesactivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>' ><span class="zmdi zmdi-power zmdi-hc-rotate-180""></span> Desactivar</asp:LinkButton>
                                            </li>
                                                                    <li>  <asp:LinkButton runat="server" ID="btnSeleccionar" CommandArgument='<%#Eval("id")%>' TabIndex='<%#Container.DataItemIndex %>' OnClick="btnSeleccionar_Click" >
												                    <span class="zmdi zmdi-border-color"></span> Editar
                                            </asp:LinkButton> </li>
                                                                    <li>
                                             <asp:LinkButton runat="server" ClientIDMode="AutoID" ID="btnBorrar" OnClick="btnBorrar_Click" CommandName='<%#Eval("id")%>' TabIndex='<%#Container.DataItemIndex%>'>
												                    <span class="zmdi zmdi-delete"></span> Eliminar
                                            </asp:LinkButton>

</li>
                                                                    

                                                                </ul>
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
                                            <asp:LinkButton ID="btnDesactivoActivar" runat="server" OnClick="btnDesactivoActivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>' ><span class="zmdi zmdi-power"></span> Activar</asp:LinkButton>
</li>
                                                                    

                                                                </ul>
                                                            </li>
                                                        </ul>
                                        </td>
                                                                                                           
                                      <%-- <td>
                                           <ul class="actions">

                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                       <asp:LinkButton ID="lnkQuitar" runat="server"><span class="zmdi zmdi-block"></span> Quitar pedido</asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkVer" runat="server" OnClick="lnkVer_Click" CommandName='<%#Eval("id") %>'>Consultar pedido</asp:LinkButton>
                                                                    </li>
                                                                     <li>
                                                                        <asp:LinkButton ID="lnkEditar" runat="server" OnClick="lnkEditar_Click" CommandName='<%#Eval("id") %>'>Editar Pedido</asp:LinkButton>
                                                                    </li>
                                                                    

                                                                </ul>
                                                            </li>
                                                        </ul>
                                          </td>--%>

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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                        </div>
                        <div class="row">
                            <div class="col-sm-12 text-right">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <asp:LinkButton class="btn btn-default" runat="server" ID="lnkCerrar"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>




        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="conUpdatePanel" runat="server">
    <div class="modal fade" id="myModalConfirm" tabindex="-2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                           <span class="text text-info"><h4 class="text-info">Confirmación </h4> </span> 
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="grid simple horizontal gray" id="divRespuesta" runat="server">
                                    <div class="grid-title ">
                                        <asp:Label ID='lblConfirmacionCuerpo' runat='server' />
                                    </div>
                                </div>
                                
                                        <div class="form-actions">
                                    <div class="text-right" style="padding:25px;">
                                        <asp:Button ID="btnEventoConfirmar" Text="Confirmar" runat="server" class="btn bgm-cyan btn-sm"   />
                                        <asp:Button ID="btnCerrarConfirmacíon" Text="Cancelar" runat="server" class="btn btn-default btn-sm"  data-dismiss="modal" aria-hidden="true" />
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
    
    <%--<script src="<%=Page.ResolveUrl("~/js/jquery.dataTables.min.js")%>"></script>
	<script src="<%=Page.ResolveUrl("~/js/InicializaDataTable.js")%>"></script>
  		<script src="<%=Page.ResolveUrl("~/js/dataTables.bootstrap.min.js")%>"></script>--%>
</asp:Content>


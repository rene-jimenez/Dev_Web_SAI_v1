<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmConsultaProveedor.aspx.vb" Inherits="CPD.frmConsultaProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="conUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">PROVEEDOR</a></li>
            <li class="active text-uppercase">CONSULTA</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">BUSQUEDA/CONSULTA PROVEEDOR
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-md-12 col-sm-12">

            <div class="card" id="cardlista" runat="server">
                <div class="card-header ch-alt bgm-bluegray">
                            <h2>Listado de proveedores</h2>
                        </div>
                <div class="card-body card-padding">
                    <div class="card-body">

                        <asp:ListView ID="lsvLista" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                            <LayoutTemplate>
                                <table class="table table-striped table-vmiddle listaConFiltro" id="filtro">
                                    <thead >
                                        <tr>
                                             <th style="width: 3%;" class="text-left">NÚM.</th>
                                            <th style="width: 22%;" class="text-left">Nombre</th>
                                            <th style="width: 14%;" class="text-left">Ciudad</th>
                                            <th style="width: 16%;" class="text-left">Domicilio</th>
                                            <th style="width: 8%;" class="text-left">Giro</th>
                                            <th style="width: 8%;" class="text-left">R.F.C.</th>
                                            <th style="width: 5%;" class="text-left">Estado</th>
                                            <th style="width: 24%;" class="text-center">Acción</th>
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
                                        <asp:Label ID="lblCiudad" Text='<%#Eval("ciudad") %>' runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDomicilio" Text='<%#Eval("domicilio") %>' runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGiro" Text='<%#Eval("giro") %>' runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRfc" Text='<%#Eval("rfc") %>' runat="server"></asp:Label>
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
                                                                        <asp:LinkButton ID="btnActivoDesactivar" runat="server" OnClick="btnActivoDesactivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-power zmdi-hc-rotate-180""></span> Desactivar</asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                          <asp:LinkButton runat="server" ID="btnSeleccionar" CommandArgument='<%#Eval("id")%>' TabIndex='<%#Container.DataItemIndex %>' OnClick="btnSeleccionar_Click1"><span class="zmdi zmdi-border-color"></span> Editar  </asp:LinkButton>
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
                                                                        <asp:LinkButton ID="btnDesactivoActivar" runat="server" OnClick="btnDesactivoActivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-power"></span> Activar</asp:LinkButton>
                                                                    </li>
                                                           </li>
                                            </ul>                                           
                                        </td>                                   
                                </tr>
                            </ItemTemplate>
                             <EmptyDataTemplate>
                                    <tr>
                                        <div class="alert alert-danger">
                                            <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble  zmdi-hc-fw mdc-text-blue"></span>
                                            Lo sentimos, No hay registros
                                        </div>
                                    </tr>
                                </EmptyDataTemplate>
                        </asp:ListView>
                    
                    <div class="col-sm-12 text-right">
                        <div class="form-group m-b-30">
                            <div class="fg-line">                                                    
                                  <asp:LinkButton class="btn bgm-gray" runat="server" ID="btnCancelar"><i class="fa fa fa-times"></i> Cancelar</asp:LinkButton>                                 
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
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
    
    <script src="<%=Page.ResolveUrl("~/js/jquery.dataTables.min.js")%>"></script>
	<script src="<%=Page.ResolveUrl("~/js/InicializaDataTable.js")%>"></script>
  	<%-- <script type="text/javascript" charset="utf-8">
	     $(document).ready(function () {
	         InicializarDatable('#filtro');
	     });
	</script>--%>
	<script src="<%=Page.ResolveUrl("~/js/dataTables.bootstrap.min.js")%>"></script>
</asp:Content>


<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmListadoEntrada.aspx.vb" Inherits="CPD.frmListadoEntrada" %>
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
                                <asp:Button ID="btnEventoConfirmar" Text="Confirmar" runat="server" class="btn btn-success"   />
                                <asp:Button ID="btnCerrarConfirmacíon" Text="Cancelar" runat="server" class="btn btn-default"  data-dismiss="modal" aria-hidden="true" />
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
            <li><a href="../../default.aspx" class="text-uppercase">PRINCIPAL</a></li>            
            <li class="active text-uppercase"><asp:Label ID="lblBreadcrumb" runat="server" Text=""></asp:Label></li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Almacén
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card" runat="server" id="listado">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>
                    <asp:Label ID="lblTituloListado" runat="server" />
                </h2>
            </div>
            <div class="card-body">
  <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lsvListado" runat="server" ItemPlaceholderID="elementoPlaceHolder" >
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>

                                            <th data-column-id="id" data-type="numeric"  style="width: 5%;">Núm.</th>
                                            <th style="width: 10%;" class="text-center">Fecha elaboración</th>
                                            <th style="width: 10%;" class="text-center">Núm. Pedido</th>
                                            <th style="width: 40%;" class="text-center">Proveedor</th>
                                            <th style="width: 15%;" class="text-center">Tipo pago</th>
                                         <th style="width: 10%;" class="text-center">Turno DRM</th>                                        
                                            <th style="width: 10%;" class="text-center">Acción</th>
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
                                       <asp:Label ID="lblIdPedido" runat="server" Text='<%#Eval("id") %>' Visible="false"/> <asp:Label ID="lblIDListaA" Text='<%#Container.DataItemIndex + 1 %> ' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label id="lblFechaElaboraciónA" Text='<%# String.Format(FormatDateTime(Eval("fechaElaboracion")))%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumeroPedidoA" Text='<%#Eval("numeroPedido")%>' runat="server" />
                                    </td>
                                    <td class="text-left">
                                        <asp:Label ID="lblProveedorA" Text='<%#Eval("_nombreProveedor")%>' runat="server" />
                                    </td>
                                    
                                    <td>
                                       <asp:Label ID="lblTipoPagoA" Text='<%#Eval("_nombreTipoPago")%>' runat="server" />
                                    </td>
                                  <td>
                                        <asp:Label ID="lblNumeroAreaA" Text='<%#Eval("observaciones")%>' runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <ul class="actions">

                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkConsultarA" runat="server" CommandName='<%#Eval("id") %>' OnClick="lnkConsultarA_Click"><i class="fa fa-play animated infinite wobble c-bluegray"></i> Dar entrada</asp:LinkButton>
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
                         <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="LsvListado2" runat="server" ItemPlaceholderID="elementoPlaceHolderA" OnItemDataBound="lvsListado2_OnItemDataBound">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>

                                            <th data-column-id="id" data-type="numeric"  style="width: 5%;">ID</th>
                                            <th style="width: 10%;" class="text-center">Fecha elaboración</th>
                                            <th style="width: 10%;" class="text-center">Núm. Pedido</th>
                                            <th style="width: 40%;" class="text-center">Proveedor</th>
                                            <th style="width: 15%;" class="text-center">Tipo pago</th>
                                         <th style="width: 10%;" class="text-center">Turno DRM</th>                                        
                                            <th style="width: 10%;" class="text-center">Acción</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="elementoPlaceHolderA" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIDListaB" Text='<%#Container.DataItemIndex + 1 %> ' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label id="lblFechaElaboraciónB" Text='<%#Eval("_fechaPedidoRecibido")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumeroPedidoB" Text='<%#Eval("_numeroPedido")%>' runat="server" />
                                    </td>
                                    <td class="text-left">
                                        <asp:Label ID="lblProveedorB" Text='<%#Eval("_nombreProveedor")%>' runat="server" />
                                    </td>
                                    
                                    <td>
                                       <asp:Label ID="lblTipoPagoB" Text='<%#Eval("_tipoPago")%>' runat="server" />
                                    </td>
                                   <td>
                                        <asp:Label ID="lblNumeroAreaB" Text='<%#Eval("_turnoDRM")%>' runat="server" />
                                    </td>
                                    <td style="text-align: right">
                                        <ul class="actions">

                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkConsultar2" runat="server" CommandName='<%#Eval("id") %>' OnClick="lnkConsultar2_Click" ><i class="fa fa-play animated infinite wobble c-bluegray"></i> Ir a la entrada</asp:LinkButton>
                                                                    </li>
                                                                      <li>
                                                                        <asp:LinkButton ID="lnkEliminar" ClientIDMode="AutoID" runat="server" CommandName='<%#Eval("id") %>' TabIndex='<%#Container.DataItemIndex%>' OnClick="lnkEliminar_Click" ><i class="fa fa-trash"></i> Eliminar entrada</asp:LinkButton>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        



                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
       <div class="col-sm-12 text-right">
            <div class="form-group m-b-10">
                <div class="fg-line">                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnSalir" OnClick="btnSalir_Click"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
             <br />
            <br />
             </div>

        

    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
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
		function carga() {
		    $(".listaConFiltro").DataTable();          
		}
		  
	</script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

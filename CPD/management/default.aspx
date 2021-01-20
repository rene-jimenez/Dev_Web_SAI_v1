<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="default.aspx.vb" Inherits="CPD._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">

   <div class="container">
			<div class="block-header">
				<h2 class="active text-uppercase">BIENVENIDO <asp:Label ID="lblUsuario" runat="server" class="c-deeporange" text="USER" />. HOY ES <asp:Label ID="lblFecha" runat="server"  />.
					</h2>                
			</div>
		<div class="clearfix"></div>
       <div id="divDashboard" runat="server">

       
       <div class="row">
           
           <div class="col-md-3">
                <div class="card">
                    <div class="panel-body text-center h-200">
                        <i class="fa fa-file fa-4x c-deeporange"></i>

                        <h1 class="m-xs"><asp:Label ID="lblOEP" runat="server" Text="" /></h1>

                        <h3 class="font-extra-bold no-margins">
                           Oficios
                        </h3>
                        <small>En proceso</small>
                    </div>
                   
                </div>
            </div>
           <div class="col-md-3">
                <div class="card">
                    <div class="panel-body text-center h-200">
                        <i class="fa fa-credit-card fa-4x"></i>

                        <h1 class="m-xs"><asp:Label ID="lblSGNL" runat="server" Text="" /></h1>

                        <h3 class="font-extra-bold no-margins">
                           Solicitud de gastos
                        </h3>
                        <small>No liberados.</small>
                    </div>
                    
                </div>
            </div>
           <div class="col-md-3">
                <div class="card">
                    <div class="panel-body text-center h-200">
                        <i class="fa fa-usd fa-4x"></i>

                        <h1 class="m-xs"><asp:Label ID="lblSGLP" runat="server" Text="" /></h1>

                        <h3 class="font-extra-bold no-margins">
                           Solicitud de gastos
                        </h3>
                        <small>Liberados pendientes.</small>
                    </div>
                    
                </div>
            </div>
           <div class="col-md-3">
                <div class="card">
                    <div class="panel-body text-center h-200">
                        <i class="fa fa-usd fa-4x c-green"></i>

                        <h1 class="m-xs"><asp:Label ID="lblSGC" runat="server" Text="" /></h1>

                        <h3 class="font-extra-bold no-margins">
                           Solicitud de gastos
                        </h3>
                        <small>Comprobados.</small>
                    </div>
                   
                </div>
            </div>
            <div class="col-md-3">
                <div class="card">
                    <div class="panel-body text-center h-200">
                        <i class="fa fa-file-archive-o fa-4x c-green"></i>

                        <h1 class="m-xs"><asp:Label ID="lblOA" runat="server" Text="" /></h1>

                        <h3 class="font-extra-bold no-margins">
                           Oficios
                        </h3>
                        <small>Atendidos.</small>
                    </div>
                   
                </div>
            </div>
           <div class="col-md-3">
                <div class="card">
                    <div class="panel-body text-center h-200">
                        <i class="fa fa-cubes fa-4x"></i>

                        <h1 class="m-xs"><asp:Label ID="lblPPE" runat="server" Text="" /></h1>

                        <h3 class="font-extra-bold no-margins">
                           Pedidos
                        </h3>
                        <small>Sin entrada.</small>
                    </div>
                    
                </div>
            </div>
           <div class="col-md-3">
                <div class="card">
                    <div class="panel-body text-center h-200">
                        <i class="fa fa-dropbox fa-4x c-green"></i>

                        <h1 class="m-xs"><asp:Label ID="lblPE" runat="server" Text="" /></h1>

                        <h3 class="font-extra-bold no-margins">
                           Entradas
                        </h3>
                        <small>Completas a almacén.</small>
                    </div>
                   
                </div>
            </div>
           <div class="col-md-3">
                <div class="card">
                    <div class="panel-body text-center h-200">
                        <i class="fa fa-plane fa-4x c-deeporange"></i>

                        <h1 class="m-xs"><asp:Label ID="lblPSA" runat="server" Text="" /></h1>

                        <h3 class="font-extra-bold no-margins">
                           Pedidos
                        </h3>
                        <small>Sin afectación.</small>
                    </div>
                   
                </div>
            </div>

           </div>

       <div class="row">


<%--<div class="col-md-6">
                <div class="card">
			        <div class="card-header">
			            <h2>Artículos con stock mínimo</h2>
                        
			        </div>
			
			        <div class="card-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lsvArticulos" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>
                                            <th data-column-id="id" data-type="numeric" width="5%">Núm.</th>
                                            <th style="width: 35%;" class="text-left">Artículo</th>
                                            <th style="width: 20%;" class="text-center">Existencia</th>                                       
                                            <th style="width: 20%;" class="text-center">Stock mínimo</th>
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
                                    <td class="text-left">
                                        <asp:Label ID="lblProveedor" Text='<%#Eval("nombre")%>' runat="server" />
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="lblNombrePedido" Text='<%# Eval("existencia") %>' runat="server" class='<%# if (eval("existencia") = "0", "c-deeporange f-200", "") %>' />
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="lblStockMinimo" Text='<%#Eval("stockMinimo")%>' runat="server" />
                                    </td>
                                    
                                    
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                  <tr class="text-center" >
                                                    <div class="bgm-deeporange p-20">
                                                        <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                                        No se puede mostrar registros, revisa la información o llama a la Coordinación de Informática, al área de sistemas</h5>
                                                    </div>
                                                </tr>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                </div>
            </div>--%>
 <div class="col-md-6">
           <div class="card forum-box">
               <div class="card-header">
			            <h2>Últimos oficios recibidos</h2>
			        </div>
                 <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lsvOfix" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                            <LayoutTemplate>

   <div class="card-body card-padding">
        <div class="row">

                <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                
        </div>
    </div>

     </LayoutTemplate>
                            <ItemTemplate>
                                  <div class="col-md-8 forum-heading">
                                <a href="#"><h4> <i class="fa fa-file-o fa-1x c-bluegray"></i>  <asp:Label ID="lblProveedor" Text='<%#Eval("fechaCaptura", "{0:D}")%>' runat="server" /></h4></a>
                <div class="desc">Oficio de la  <asp:Label ID="lbl3" Text='<%#Eval("_area")%>' runat="server" />.   <asp:Label ID="lbl4" Text='<%#Eval("asunto")%>' runat="server" />
                </div>
            </div>
            <div class="col-md-2 forum-info">
                <span class=""> DRM </span>
                <small class="number"> <asp:Label ID="lbl1" Text='<%#Eval("turnoDRM")%>' runat="server"/></small>
            </div>
            <div class="col-md-2 forum-info">
                <span class=""> SAF </span>
                <small class="number"> <asp:Label ID="lbl2" Text='<%#Eval("turnoSAF")%>' runat="server" /></small>
            </div>
</ItemTemplate>
                                 <EmptyDataTemplate>
                                  <tr class="text-center" >
                                                    <div class="bgm-deeporange p-20">
                                                        <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                                        No se puede mostrar los últimos oficios, revisa la información o llama a la Coordinación de Informática, al área de sistemas</h5>
                                                    </div>
                                                </tr>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
</div>
</div>

           
       </div>

       </div>
	</div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
      <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".listaConFiltro").DataTable();
                   }
               });
           };
</script>

</asp:Content>
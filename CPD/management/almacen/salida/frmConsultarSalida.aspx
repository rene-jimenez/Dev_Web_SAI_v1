<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmConsultarSalida.aspx.vb" Inherits="CPD.frmConsultarSalida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
    <div class="container">
	<ol class="breadcrumb">
		<li><asp:linkButton class="text-uppercase" runat="server" id="go2Default" OnClick="go2Default_Click">PRINCIPAL</asp:linkButton></li>
		<li><a href="#" class="text-uppercase">Almacén</a></li>
		<li><a href="#" class="text-uppercase">Salida</a></li>
		<li class="active text-uppercase">Consultar</li>
	</ol>
	<div class="block-header">
		<h2 class="active text-uppercase">Almacén
		</h2>
	</div>
	<div class="clearfix"></div>
	
		<div class="row">
			<div class="col-md-8 col-md-offset-2">
				<div class="card">
					<div class="card-header ch-alt bgm-bluegray">
						<h2>
							Consultar salida
						</h2>
					</div>
					<div class="card-body card-padding">
                        
						<div class="form-horizontal m-10">
							<div class="form-group p-t-20">
								<label class="col-sm-4 control-label">Fecha inicial <span class="text-danger">(*)</span></label>
								<div class="col-sm-8">
									<div class="input-group">
										<div class="fg-line">
											<asp:TextBox ID="txbFechaInicial" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" AutoPostBack="true" TabIndex="1" />
										</div>
										<span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
									</div>
								</div>
							</div>
							<div class="form-group p-t-20">
								<label class="col-sm-4 control-label">Fecha final <span class="text-danger">(*)</span></label>
								<div class="col-sm-8">
									<div class="input-group">
										<div class="fg-line">
											<asp:TextBox ID="txbFechaFinal" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha"  AutoPostBack="true" TabIndex="2" />
										</div>
										<span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
									</div>
								</div>
							</div>
							<div class="form-group">
								<label class="col-sm-4 control-label">Área  <span class="text-danger">(*)</span></label>
								<div class="col-sm-8">
									<div class="fg-line">
										<div class="select">
											<asp:DropDownList ID="cmbArea" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True" TabIndex="3" AutoPostBack="true"  OnSelectedIndexChanged="cmbArea_SelectedIndexChanged1">
												<asp:ListItem Text="Seleccione un elemento de la lista" Value="0" Selected="True"></asp:ListItem>
                                                
											</asp:DropDownList>
										</div>
									</div>
								</div>
							</div>
							<div class="form-group">
								<label class="col-sm-4 control-label">Folio vale u oficio  <span class="text-danger">(*)</span></label>
								<div class="col-sm-8">
									<div class="fg-line">
										<div class="select">
											<asp:DropDownList ID="cmbFolio" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="cmbFolio_SelectedIndexChanged">
												<asp:ListItem Text="Seleccione un elemento de la lista" Value="0" Selected="True"></asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
								</div>
							</div>
						</div>
                            
					</div>
				</div>
			</div>
		</div>
		<div class="row">
			<div class="card" id="lista" runat="server">
				<div class="card-header">
					<h2>
						Lista de artículos
					</h2>
				</div>
				<div class="card-body">
                    <asp:UpdatePanel runat="server" ID="updpnl">
					<ContentTemplate>
						<asp:ListView ID="lsvListado" runat="server" ItemPlaceholderID="elementoPlaceHolder">
							<LayoutTemplate>
								<table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
									<thead class="cf">
										<tr>
											<th style="width: 20%;" class="text-left">Código de barras</th>
											<th style="width: 35%;" class="text-left">Artículo</th>
											<th style="width: 15%;" class="text-left">Cant. Entregada</th>
											<th style="width: 15%;" class="text-left">Precio unitario</th>
											<th style="width: 15%;" class="text-left">Importe</th>
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
										<asp:Label ID="lblCodigoBarras" Text='<%#Eval("_codigoBarras")%>' runat="server" />
									</td>
									<td>
										<asp:Label ID="lblArticulo" Text='<%#Eval("_nombreArticulo")%>' runat="server" />
									</td>
									<td>
										<asp:Label ID="lblCantidadEntregada" Text='<%#Eval("cantidad")%>' runat="server" />
									</td>
									<td>
                                        <asp:Label ID="lblPrecioUnitario" Text='<%#String.Format("{0:C4}", Eval("_ultimoprecio"))%>' runat="server" />
										
									</td>
									<td>
										<asp:Label ID="lblImporte" Text='<%#String.Format("{0:C2}", Eval("_importe"))%>' runat="server" />
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
				</div>
			</div>

            <div class="col-sm-12 text-right">
            <div class="form-group m-b-10">
                <div class="fg-line">                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnSalir" OnClick="btnSalir_Click"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
		</div>
	
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">







</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
 <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".conVentanaFecha").datepicker({ format: 'dd/mm/yyyy', autoclose: true });
                   }
               });
           };
</script>
    <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".conMascaraFecha").inputmask("99/99/9999");
                   }
               });
           };
</script>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

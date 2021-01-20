<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmEditarSalida.aspx.vb" Inherits="CPD.frmEditarSalida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
	<div class="container">
		<ol class="breadcrumb">
			<li>
				<asp:linkButton class="text-uppercase" runat="server" id="go2Default" OnClick="go2Default_Click" >PRINCIPAL</asp:linkButton>
			</li>
			<li><a href="#" class="text-uppercase">Almacén</a></li>
			<li><a href="#" class="text-uppercase">Salida</a></li>
			<li class="active text-uppercase">Editar</li>
		</ol>
		<div class="block-header">
			<h2 class="active text-uppercase">Almacén
			</h2>
		</div>
		<div class="clearfix"></div>
		<div class="card">
			<div class="card-header ch-alt bgm-bluegray">
				<h2>
					Editar salida
				</h2>
			</div>
			<div class="card-body card-padding">
				<div class="row">
					<div class="col-sm-6">
						<div class="fg-line">
							<label class="fg-label">Area </label>
							<asp:Label ID="lblArea" runat="server" class="form-control input-sm"  TabIndex="1" Text="Area" />
							<asp:HiddenField id="hdfArea" runat="server" Value=""/>
						</div>
					</div>
					<div class="col-sm-6">
						<div class="input-group">
							<div class="fg-line">
								<label class="fg-label">Fecha de salida <span class="text-danger">(*)</span></label>
								<asp:TextBox ID="txbFechaSalida" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="2" OnTextChanged="txbFechaSalida_TextChanged" />
							</div>
							<span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-sm-6">
						<div class="fg-line">
							<label class="fg-label">Folio vale </label>
							<asp:label ID="lblFolioVale" runat="server" class="form-control input-sm"  TabIndex="3" />
						</div>
					</div>
					<div class="col-sm-6">
						<div class="fg-line">
							<label class="fg-label">Folio oficio </label>
							<asp:TextBox ID="txbFoliooficio" runat="server" MaxLength="6" class="form-control input-sm" AutoPostBack="true" placeholder="000000" TabIndex="4" />
						</div>
					</div>
				</div>
				<div class="row">
					<div class="form-group m-b-30">
						<div class="fg-line">
							<label class="fg-label">Observaciones:</label>
							<asp:TextBox ID="txbObservaciones" runat="server" class="form-control input-sm" Rows="2" TextMode="MultiLine" TabIndex="3" />
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="clearfix"></div>
		<div class="row">
			<div class="col-md-4">
				<div class="card">
					<div class="card-header c-bluegray" runat="server" id="headerAgregarArticulos">
						<h2>
							<asp:Label runat="server" id="lblTituloAgregarCodigos" Text="Agregar artículos" />
						</h2>
					</div>
					<div class="card-body card-padding">
						<div class="form-horizontal m-10">
                             <div class="col-sm-12">
                                         <div class="form-group">
                                            
                                            <div class="fg-line">
                                                <label class="fg-label">Elige tipo de busqueda:</label>  <br />
                                                <br />
                                             
                                                    <label class="radio radio-inline m-r-20" style="padding: 0px; padding-left: 8%;">
                                                            <asp:RadioButton checked="true" runat="server" ID="chkCodigo" AutoPostBack="true" />
                                                            <i class="input-helper"></i>Código de Barras
                                                        </label>
                                                        <label class="radio radio-inline m-r-20" style="padding: 0px; padding-left: 8%;">
                                                            <asp:RadioButton runat="server" ID="chkNombre" AutoPostBack="true" />

                                                            <i class="input-helper"></i>Nombre
                                                        </label>
                                            
                                                        
                                            
                                            </div>
                                        </div>
                                    </div>
							<div class="form-group p-t-20" id="divCodigoBarras" runat="server">
								<label class="col-sm-4 control-label">Código de barras  <span class="text-danger">(*)</span></label>
								<div class="col-sm-8">
									<div class="fg-line">
										<asp:TextBox runat="server" ID="txbCodigo" ClientIDMode="AutoID" CssClass="form-control input-sm" TabIndex="5" OnTextChanged="txbCodigo_TextChanged" AutoPostBack="true"/>
									</div>
								</div>
							</div>
							<div class="form-group" id="divNombreArticulo" runat="server">
								<label class="col-sm-4 control-label">Artículo</label>
								<div class="col-sm-8">
									<div class="fg-lines">
										<asp:Label runat="server" ID="lblArticuloCodigo" CssClass="form-controls input-sm" TabIndex="5" text=""/>
									</div>
								</div>
							</div>
                            <div class="form-group" id="divCmbArticulo" runat="server">
                                        <div class="col-sm-12">
                                            <div class="fg-line">
                                                <label class="col-sm-4 control-label">Artículo <span class="text-danger">(*)</span></label>
                                                <div class="select">
                                                    <asp:DropDownList ID="cmbArticulo" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True" OnTextChanged="cmbArticulo_TextChanged" AutoPostBack="true" TabIndex="1">
                                                        <asp:ListItem Text="Seleccione un elemento de la lista" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
							<div class="form-group">
								<label class="col-sm-4 control-label">Cantidad  <span class="text-danger">(*)</span></label>
								<div class="col-sm-6">
									<div class="fg-line">
										<asp:TextBox runat="server" ID="txbCantidad" CssClass="form-control input-sm" TabIndex="7" onkeydown = "return thisIsNumber(event);" />
										<asp:HiddenField id="hdfNuevo" runat="server" Value="Nuevo"/>
									</div>
								</div>
								<div class="col-sm-2 text-right">
									<ul class="actions">
										<li class="dropdown">
											<a href="" data-toggle="dropdown" aria-expanded="false">
											<i class="zmdi zmdi-more-vert"></i>
											</a>
											<ul class="dropdown-menu dropdown-menu-right">
												<li>
													<i class="fa fa-thumbs-up animated rubberBand c-bluegray"></i> Existencia:
												</li>
												<li>
													<span class="f-400">
														<asp:label runat="server" ID="lblstockActual" Text=""/>
													</span>
												</li>
											</ul>
										</li>
									</ul>
								</div>
							</div>
							<div class="row">
								<div class="text text-right p-b-25 p-r-25">
									<asp:UpdatePanel  ID="upnasjhjasdgh" runat="server">
										<ContentTemplate>
											<asp:LinkButton class="btn btn-primary" runat="server" ID="btnAgregarCodigo" TabIndex="8" OnClick="btnAgregarCodigo_Click" ><i class="fa fa-check-circle"></i> Agregar</asp:LinkButton>
											<asp:LinkButton class="btn btn-default" runat="server" ID="btnBorrar" TabIndex="9" OnClick="btnBorrar_Click" ><i class="fa fa-cog fa-spin"></i> Borrar</asp:LinkButton>
										</ContentTemplate>
									</asp:UpdatePanel>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-8">
				<div class="card">
					<div class="card-body">
						<div class="card-header">
							<h2>
								<asp:Label ID="lblTituloArticulos" runat="server" Text="Lista de artículos" />
							</h2>
						</div>
						<div class="card-body">
							<asp:UpdatePanel UpdateMode="Conditional" ID="updateAgregarArticulo" runat="server">
								<ContentTemplate>
									<div>
										<asp:ListView ID="lsvListado" runat="server" ItemPlaceholderID="elementoPlaceHolder">
											<LayoutTemplate>
												<table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
													<thead class="cf">
														<tr>
															<th data-column-id="ID" data-type="numeric"  style="width: 5%;">Núm.</th>
															<th style="width: 15%;" class="text-center">Código de Barras</th>
															<th style="width: 30%;" class="text-center">Artículo</th>
															<th style="width: 10%;" class="text-center">Cant. Entregada</th>
															<th style="width: 15%;" class="text-center">Precio unitario</th>
															<th style="width: 15%;" class="text-center">Importe</th>
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
														<asp:Label ID="lblIDLista" Text='<%#Container.DataItemIndex + 1 %> ' runat="server" />
													</td>
													<td>
														<asp:Label ID="Label1" Text='<%#Eval("_codigoBarras")%>' runat="server" />
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
													<td style="text-align: right">
														<ul class="actions">
															<li class="dropdown">
																<a href="" data-toggle="dropdown" aria-expanded="false">
																<i class="zmdi zmdi-more-vert"></i>
																</a>
																<ul class="dropdown-menu dropdown-menu-right">
																	<li>
																		<asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="lnkEliminar_Click" CommandName='<%#Eval("ipUsuario") %>'><i class="fa fa-times animated infinite wobble c-red"></i> Eliminar</asp:LinkButton>
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
															No existen registros, revisa la información o llama a la Coordinación de Informática, al área de sistemas
														</h5>
													</div>
												</tr>
											</EmptyDataTemplate>
										</asp:ListView>
									</div>
								</ContentTemplate>
							</asp:UpdatePanel>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="row">
			<div class="text text-right p-b-25 p-r-25">
				<asp:LinkButton class="btn btn-success" runat="server" ID="btnEditar" TabIndex="10" OnClick="btnEditar_Click" ><i class="fa fa-pencil-square-o"></i> Editar</asp:LinkButton>
				<asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="11" OnClick="btnCerrar_Click"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
			</div>
		</div>
	</div>
	<div class="modal fade" id="myModalConfirm" tabindex="-2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<span class="text text-info">
						<h4 class="text-info">Confirmación </h4>
					</span>
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
								<asp:linkButton ID="btnEventoConfirmar" runat="server" class="btn btn-success btn-sm" OnClick="btnEventoConfirmar_Click" OnClientClick="HideModal()"><i class="fa fa-thumbs-up"></i> Aceptar</asp:linkButton>
								<asp:linkButton ID="btnCerrarConfirmacíon" runat="server" class="btn btn-default btn-sm"  data-dismiss="modal" aria-hidden="true" OnClientClick="HideModal()"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:linkButton>
								<asp:HiddenField ID="idEliminar" runat="server" Visible="false" />
								<asp:HiddenField ID="hdfTipoEliminar" runat="server" Visible="false" />
							</div>
						</div>
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
	<script type="text/javascript">
		var prm = Sys.WebForms.PageRequestManager.getInstance();
		if (prm != null) {
		    prm.add_endRequest(function (sender, e) {
		        if (sender._postBackSettings.panelsToUpdate != null) {
		            $(".conVentanaFecha").datepicker({ format: 'dd/mm/yyyy', autoclose: true });
		        }
		    });
		};
		
		var prm = Sys.WebForms.PageRequestManager.getInstance();
		if (prm != null) {
		    prm.add_endRequest(function (sender, e) {
		        if (sender._postBackSettings.panelsToUpdate != null) {
		            $(".conMascaraFecha").inputmask("99/99/9999");
		        }
		    });
		};
		
	</script>
        <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".conFiltro").select2();
                   }
               });
           };
</script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server"></asp:Content>


<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmRol.aspx.vb" Inherits="CPD.frmRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #btnAgregar
        {
            width: 65px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
	<div class="container">
		<ol class="breadcrumb">
			<li><a href="#" class="text-uppercase">PRINCIPAL</a></li>
			<li class="active text-uppercase">Rol</li>
		</ol>

		<div class="block-header">
				<h2 class="active text-uppercase">rol</h2>                
		</div>
		<div class ="row">
            <div class="col-md-7 col-sm-12"  id="divLista" runat ="server">
                <div class="card">
					<div class="card-header ch-alt bgm-bluegray">
					    <h2>Lista de roles</h2>
					</div>
                      
					<div class="card-body card-padding">
                       <asp:UpdatePanel runat="server" ID="updateLista"   ClientIDMode="AutoID"  UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:ListView ID="lsvRol" runat="server" ItemPlaceholderID="bodyListView">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-vmiddle listaConFiltro" aria-busy="false">
                                            <thead class="cf">
                                                <tr>
                                                    <th data-column-id="id" data-type="numeric" width="5%">Núm.</th>
                                                    <th class="text-left" style="width: 40%">Rol</th>
                                                    <th class="text-center" style="width: 20%">Estatus</th>
                                                    <th class="text-center" style="width: 35%">
                                                        <asp:LinkButton Class="btn btn-success" runat="server" ID="btnANuevo" OnClick="btnANuevo_Click"><span class="glyphicon glyphicon-plus-sign"></span> Agregar</asp:LinkButton></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="bodyListView" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="rating-list">
                                            <td><asp:Label ID="lbl_lv_ID" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label></td>
                                            <td>
                                                <%--<asp:TextBox runat="server" ID="txbLsvNombre" Text='<%#Eval("nombre")%>' aria-describedby="inputSuccess3Status" CssClass="form-control" Visible="false" />--%>
                                                <asp:Label ID="labelLsvNombre" Text='<%#Eval("nombre")%>' runat="server" />
                                            </td>
                                            <td class="rl-star text-center"><%# If(Eval("esActivo"), "<span class='btn-icon-text'><i class='zmdi zmdi-power active'></i> Activo</span>", "<span class='btn-icon-text'><i class='zmdi zmdi-power zmdi-hc-rotate-180 c-gray'></i> Inactivo</span>") %></td>
                                            <%--  <td class="text-center">   
                                                <span>
                                                 <%#IIf(Eval("esActivo"), "<span class='label label-primary'>Activo-<i class='glyphicon glyphicon-trash'></i></span>", "<span class='label label-default'> Inactivo-<i class='glyphicon glyphicon-check'></i></span>")%>
                                              <asp:CheckBox Checked='<%#Eval("esActivo")%>' Enabled="false" Visible="false" ID="chkEsActivo" runat="server" />
                                                                     </span>   
                                            </td>--%>                                   
                                            <td runat="server" visible='<%# IIf(Eval("esActivo"), True, False) %>' class="text-center">
                                                <ul class="actions">
                                                    <li class="dropdown">
                                                        <a href="" data-toggle="dropdown" aria-expanded="false">
                                                            <i class="zmdi zmdi-more-vert"></i>
                                                        </a>
                                                        <ul class="dropdown-menu dropdown-menu-right">
                                                            <li>
                                                                <asp:LinkButton ID="btnActivoEditar" runat="server" CommandArgument='<%#Eval("id") %>' OnClick="btnActivoEditar_Click" ClientIDMode="AutoID"><span class="zmdi zmdi-border-color"></span> Edita</asp:LinkButton>
                                                            </li>
                                                            <li>
                                                                <asp:LinkButton ID="btnActivoDesactivar" runat="server" OnClick="btnActivoDesactivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("id") %>'><span class="zmdi zmdi-power zmdi-hc-rotate-180"></span> Desactivar</asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                                <%--<asp:LinkButton ID="btnActivoEditar" runat="server" OnClick="btnActivoEditar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("id") %>' CssClass="btn command-edit bgm-orange"><span class="zmdi zmdi-border-color"></span></asp:LinkButton>
                                                <asp:LinkButton ID="btnActivoDesactivar" runat="server" OnClick="btnActivoDesactivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("id") %>' CssClass="btn bgm-bluegray"><span class="zmdi zmdi-power zmdi-hc-rotate-180"></span></asp:LinkButton>--%>
                                            </td>
                                            <td runat="server" visible='<%# IIf(Convert.ToBoolean(Eval("esActivo")), False, True) %>' class="text-center">
                                                <ul class="actions">
                                                    <li class="dropdown">
                                                        <a href="" data-toggle="dropdown" aria-expanded="false">
                                                            <i class="zmdi zmdi-more-vert"></i>
                                                        </a>
                                                        <ul class="dropdown-menu dropdown-menu-right">
                                                            <li>
                                                                <asp:LinkButton ID="btnDesactivoActivar" runat="server" OnClick="btnDesactivoActivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("id") %>'><span class="zmdi zmdi-power"></span> Activar</asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </td>




                                            <%--<td id="tdAccion" runat="server" style="text-align: center">
                                                <div class="btn-demo">
                                                    <button type ="button" ClientIDMode="AutoID" TabIndex='<%#Container.DataItemIndex%>' name='<%#Container.DataItemIndex%>'  runat="server" id="btnEditar" onserverclick="btnEditar_OnClick" class="btn btn-icon command-edit waves-effect waves-circle  waves-float bgm-blue"><span class="zmdi zmdi-edit"></span></button> 
                                                    <button type ="button" ClientIDMode="AutoID" TabIndex='<%#Container.DataItemIndex%>' CommandArgument='<%#Eval("id")%>' name='<%#Container.DataItemIndex%>' class="btn btn-icon command-delete waves-effect waves-circle waves-float bgm-deeporange" runat="server" id="btnEliminar" onserverclick="btnEliminar_OnClick"><span class="zmdi zmdi-delete"></span></button> 
                                                    <button type ="button" ClientIDMode="AutoID" TabIndex='<%#Container.DataItemIndex%>' CommandArgument='<%#Eval("id")%>' name='<%#Container.DataItemIndex%>' class="btn btn-icon waves-effect waves-circle bgm-gray" runat="server" id="btnActualizar" ToolTip="Guardar cambios" onserverclick="btnActualizar_OnClick" visible="false"  ><span class="zmdi zmdi-redo"></span></button> 
                                                    <button type ="button" ClientIDMode="AutoID"  TabIndex='<%#Container.DataItemIndex%>' name='<%#Container.DataItemIndex%>' class="btn btn-icon command-delete  waves-effect waves-circle waves-float bgm-deeporange" runat="server" id="btnCancelar" onserverclick="btnCancelar_OnClick" ToolTip="Cancelar" visible="false" ><span class="zmdi zmdi-close-circle" ></span></button>                                                                                                    
                                                </div>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <div class="alert alert-danger">No hay registros</div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                          </ContentTemplate>
                         </asp:UpdatePanel>                                                  
                    </div>					
				</div>
            </div>
             <asp:UpdatePanel runat="server" ClientIDMode="AutoID" id="updateAgregar" UpdateMode="Conditional">
                <ContentTemplate>
                        <div class="col-md-5 col-sm-12" id="divPanelLateral" runat ="server">
                  <div class="card">
					<div class="card-header ch-alt bgm-bluegray">
						<h2>Agregue un nuevo rol</h2>
					</div>                
					<div class="card-body card-padding">
						<div class="form-group  m-b-30">
							<div class="fg-line">								
								<label class="fg-label">Nuevo rol</label>
                                <asp:TextBox class="form-control input-sm" runat="server" id="txbNuevoRol"  placeholder="Agrega un nuevo rol" />
                                 <asp:HiddenField id="lblHiddenId" runat="server" />
							</div>
						</div>         
                                                     
					</div>
                      <div  class="text text-right card-padding">
                          <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btnAgregarRol" runat="server" ValidationGroup="validarCampos" class="btn btn-success"><span class="glyphicon glyphicon-floppy-disk"></span> Guardar</asp:LinkButton>                   
                                        <asp:LinkButton  ID="btnActualizar" runat="server" ValidationGroup="validarCampos" class="btn btn-success" Visible ="false" ><span class="glyphicon glyphicon-pencil"></span> Actualizar</asp:LinkButton>
                                        <asp:LinkButton ID="btnSalir" runat ="server" class="btn btn-default"><span class="fa fa-cog fa-spin"></span> Cerrar</asp:LinkButton>                              
                                    </ContentTemplate>
                         </asp:UpdatePanel>
                      </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="El campo rol es obligatorio" CssClass="label label-danger" ControlToValidate="txbNuevoRol" ValidationGroup="validarCampos" runat="server" />
				</div>
              </div>
                </ContentTemplate></asp:UpdatePanel>
              
		</div>
		<div class="col-sm-12 col-md-8 m-b-20">
				
			</div>

		<div class="clearfix"></div>
				
					
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


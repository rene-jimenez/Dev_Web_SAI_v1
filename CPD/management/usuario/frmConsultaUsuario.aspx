<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmConsultaUsuario.aspx.vb" Inherits="CPD.frmConsultaUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">

	<div class="container">
		<ol class="breadcrumb">
			<li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
			<li><a href="frmAltaUsuario.aspx"class="text-uppercase">Usuario</a></li>
			<li class="active text-uppercase">Consulta</li>
		</ol>
	    <div class="block-header">
				<h2 class="active text-uppercase">Consulta de usuarios</h2>                
			</div>
		<div class="clearfix"></div>	
		<div class="card">
          <div class="card-header ch-alt bgm-bluegray">
                <h2>Lista de usuarios para editar</h2>
            </div>
          <div class="card-body">
                <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                    <asp:ListView ID="lvsUsuarios" runat="server" ItemPlaceholderID="elementoPlaceHolder" >
                                    <LayoutTemplate>
                                        <table id="data-table-selection" class="table table-striped table-vmiddle listaConFiltro" aria-busy="false">
                                            <thead class="cf">
                                                <tr>
                                                    <th style="width: 5%;" class="text-left">NÚM.</th>
                                                    <th style="width: 25%;" class="text-left">NOMBRE</th>
                                                    <th style="width: 10%;" class="text-left">USUARIO</th>
                                                    <th style="width: 10%;" class="text-center">EMAIL</th>
                                                    <th style="width: 10%;" class="text-center">TELÉFONO</th>
                                                    <th style="width: 10%;" class="text-center">ESTATUS</th>
                                                    <th style="width: 10%;" class="text-center">RESET CONTRASEÑA</th>
                                                    <th style="width: 20%;" class="text-center">
                                                        <asp:LinkButton Class="btn btn-success" runat="server" ID="btnANuevo" OnClick="btnANuevo_Click"><span class="glyphicon glyphicon-plus-sign"></span> Agregar</asp:LinkButton>                                                
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="rating-list">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("nombre")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUsuario" runat="server" Text='<%#Eval("usuario")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("email")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblTel" runat="server" Text='<%#Eval("telefono")%>'></asp:Label>
                                            </td>
                                            <td class="rl-star text-center"><%# If(Eval("esActivo"), "<span class='btn-icon-text'><i class='zmdi zmdi-power active'></i> Activo</span>", "<span class='btn-icon-text'><i class='zmdi zmdi-power zmdi-hc-rotate-180 c-gray'></i> Inactivo</span>") %></td>                                    


                                         <td id="tdAccion1" runat="server" style="text-align: center">
                                                <asp:CheckBox ID="chkResetPasword" runat="server" Checked='<%#Eval("esResetcontrasena")%>' TabIndex='<%#Container.DataItemIndex %>' class="square-green"  AutoPostBack="True" Enabled="false"  />
                                                 </td>

                                              <td runat="server" visible='<%# IIf(Eval("esActivo"), True, False) %>' class="text-center">
                                             <ul class="actions">

                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                        <asp:LinkButton ID="btnSeleccionar" runat="server" OnClick="btnSeleccionar_OnClick" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-border-color"></span> Editar </asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="btnActivoDesactivar" runat="server" OnClick="btnActivoDesactivar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>' ><span class="zmdi zmdi-power zmdi-hc-rotate-180""></span> Desactivar</asp:LinkButton>
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
                                                                </ul>
                                                            </li>
                                                </ul>                                                                                                                                              
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <div class="alert alert-danger">No hay registros</div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
            </div>   
            
            <div class="card-footer">
                            <asp:UpdatePanel ID="updatePanelBtns5" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-12 text-right">
                                    <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <asp:LinkButton runat="server" CssClass="btn bgm-gray" id="btnCerrar"><span class="fa fa fa-times"></span> Cancelar</asp:LinkButton>
                                    </div>
                                    </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>                         
                        </div>
            <br />
            <br />
                        
					</div>
	</div>

</asp:Content>


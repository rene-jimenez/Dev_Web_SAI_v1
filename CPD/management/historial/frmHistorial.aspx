<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmHistorial.aspx.vb" Inherits="CPD.frmHistorial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
		<ol class="breadcrumb">
			<li><a href="#" class="text-uppercase">PRINCIPAL</a></li>
			<li><a href="#" class="text-uppercase">Administración</a></li>
			<li class="active text-uppercase">Historial</li>
		</ol>

		<div class="card" id="profile-main">
			<div class="pm-overview c-overflow" style="overflow: hidden; outline: none;" tabindex="0">
				<div class="pmo-pic">
					<div class="">
						 <i class="text-center fa fa-history fa-fw fa-pulse fa-4x"></i>
					</div>
                    <asp:HiddenField ID="hdfSelector" runat="server" />
                    <h4>Historial</h4>
                   
					<div class="form ">
              
<div class="form-group">
                                    <div class="fg-line">
                                        <label class="fg-label">Consulta por:</label>
                                        <div class="select">
                                            <asp:DropDownList ID="cmbTipoHistorial" runat="server" CssClass="form-control conFiltrol"  autoPostBack="true"  AppendDataBoundItems="True">
                                                <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                                <asp:ListItem Text="Todo"></asp:ListItem>
                                                <asp:ListItem Text="Módulo"></asp:ListItem>
                                                <asp:ListItem Text="Usuario"></asp:ListItem>
                                                <asp:ListItem Text="Fecha especifica"></asp:ListItem>
                                                <asp:ListItem Text="Rango de fechas"></asp:ListItem>
                                                <asp:ListItem Text="Tipo de edición"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
    
						

                        <%--inicia consulta por todo--%>
                         <div id="divTodo" runat="server" visible ="true">
                      <%--          <div class="form-group">
                                    <div class="fg-line">
                                        <label class="fg-label">Tipo de edición:</label>
                                        <div class="select">
                                            <asp:DropDownList ID="cmbEdicionxTodo" runat="server" CssClass="form-control conFiltrol" AppendDataBoundItems="True">
                                                <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                                <asp:ListItem Text="Todos"></asp:ListItem>
                                                <asp:ListItem Text="Agregar"></asp:ListItem>
                                                <asp:ListItem Text="Editar"></asp:ListItem>
                                                <asp:ListItem Text="Desactivar"></asp:ListItem>
                                                <asp:ListItem Text="*Eliminar"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>            
                                <div class="form-group">
                                <div class="fg-line">
                                    <label class="fg-label">Módulo:</label>
                                    <div class="select">
                                        <asp:DropDownList ID="cmbModuloxTodo" runat="server" CssClass="form-control conFiltrol" AppendDataBoundItems="True">
                                            <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                            <asp:ListItem Text="Todos"></asp:ListItem>
                                            <asp:ListItem Text="Usuario"></asp:ListItem>
                                            <asp:ListItem Text="Área"></asp:ListItem>
                                            <asp:ListItem Text="Pedido"></asp:ListItem>
                                            <asp:ListItem Text="Oficio"></asp:ListItem>
                                            <asp:ListItem Text="Entrada Almacén"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                <div class="form-group" >
                                <div class="fg-line">
                                    <label class="fg-label">Fecha:</label>
                                    <div class="select">
                                        <asp:DropDownList ID="cmbFechaxTodo" runat="server" CssClass="form-control conFiltrol"  autoPostBack="true"  AppendDataBoundItems="True">
                                            <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                            <asp:ListItem Text="Todo"></asp:ListItem>
                                            <asp:ListItem Text="Fecha"></asp:ListItem>
                                            <asp:ListItem Text="Rango de fecha"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                                   
                         </div>
                               
                        <%--termina consulta por todo--%>

                        <%--inicia consulta por modulo--%>
                        <div id="divModulo" runat="server" visible ="true">
                            <div class="form-group">
                                <div class="fg-line">
                                    <label class="fg-label">Módulo:</label>
                                    <div class="select">
                                        <asp:DropDownList ID="cmbModuloTipoModulo" runat="server" CssClass="form-control conFiltrol" AppendDataBoundItems="True">
                                            <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                            <%--<asp:ListItem Text="Todo"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" >
                                <div class="fg-line">
                                    <label class="fg-label">Fecha:</label>
                                    <div class="select">
                                        <asp:DropDownList ID="cmbporModuloporFecha" runat="server" CssClass="form-control conFiltrol"  autoPostBack="true"  AppendDataBoundItems="True">
                                            <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                            <asp:ListItem Text="Todo"></asp:ListItem>
                                            <asp:ListItem Text="Fecha"></asp:ListItem>
                                            <asp:ListItem Text="Rango de fecha"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div id="divmoduloxFecha" runat="server" visible ="true">
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbporModuloFecha" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            </div>
                                 <div id="divmoduloxFechas" runat="server" visible ="true">
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha inicial: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbporModuloFechaInicial" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha final: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbModuloFechaFinal" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            </div>
                        </div>
                        <%--termina consulta por módulo--%>

                        <%--Consulta por usuario y modulo--%>
                        <div id="divUsuario" runat="server" visible ="true">
                            <div class="form-group">
                                <div class="fg-line">
                                    <label class="fg-label">Usuario:</label>
                                    <div class="select">
                                        <asp:DropDownList ID="cmbporUsuarioPorUsuario" runat="server" CssClass="form-control conFiltrol" AppendDataBoundItems="True">
                                            <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                     </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="fg-line">
                                    <label class="fg-label">Módulo:</label>
                                    <div class="select">
                                        <asp:DropDownList ID="cmbporUsuarioModulo" runat="server" CssClass="form-control conFiltrol" AppendDataBoundItems="True">
                                            <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                            <%--<asp:ListItem Text="Todo"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" >
                                <div class="fg-line">
                                    <label class="fg-label">Fecha:</label>
                                    <div class="select">
                                        <asp:DropDownList ID="cmbusuarioModuloxFecha" runat="server" CssClass="form-control conFiltrol"  autoPostBack="true"  AppendDataBoundItems="True">
                                            <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                            <asp:ListItem Text="Todo"></asp:ListItem>
                                            <asp:ListItem Text="Fecha"></asp:ListItem>
                                            <asp:ListItem Text="Rango de fecha"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div id="divUsuarioModuloxFecha" runat="server" visible ="true">
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbusuariomoduloxfecha" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            </div>
                                 <div id="divUsuarioModuloxFechas" runat="server" visible ="true">
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha inicial: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbusuariomodulofInicial" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha final: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbusuariomodulofFinal" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            </div>
                        </div>
                        <%--termina consulta por usuario--%>


                         <%-- inicia consulta por fecha--%>
                        <div id="divFecha" runat="server" visible ="true">
                        <div class="input-group">
                                                    <div class="fg-line">
                                                        <label class="fg-label">Fecha: <span class="text-danger">(*)</span></label>
                                                        <asp:TextBox ID="txbPorFecha" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                                    </div>
                                                    <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                                                </div>
                        </div>
                       <%--termina consulta por fecha--%>
                        

                       <%-- inicia consulta por rango de fecha--%>
                        <div id="divRangoFechas" runat="server" visible ="true">
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha inicial: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbPorRangoFechaInicial" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha final: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbPorRangoFechaFinal" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                        </div>
                       <%-- termina consulta por rango de fecha--%>


                         <%-- inicia consulta por tipo de edicion--%>
                        <div id="divEdicion" runat="server" visible ="true">
                           <div class="form-group">
                                    <div class="fg-line">
                                        <label class="fg-label">Tipo de edición:</label>
                                        <div class="select">
                                            <asp:DropDownList ID="cmbPorTipoEdicion" runat="server" CssClass="form-control conFiltrol" AppendDataBoundItems="True">
                                                <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                              <%--  <asp:ListItem Text="Todo"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                           <div class="form-group">
                                <div class="fg-line">
                                    <label class="fg-label">Módulo:</label>
                                    <div class="select">
                                        <asp:DropDownList ID="cmbEdicionModulo" runat="server" CssClass="form-control conFiltrol" AppendDataBoundItems="True">
                                            <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                            <%--<asp:ListItem Text="Todo"></asp:ListItem>--%>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="form-group" >
                                <div class="fg-line">
                                    <label class="fg-label">Fecha:</label>
                                    <div class="select">
                                        <asp:DropDownList ID="cmbEdicionModuloxFecha" runat="server" CssClass="form-control conFiltrol"  autoPostBack="true"  AppendDataBoundItems="True">
                                            <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                            <asp:ListItem Text="Todo"></asp:ListItem>
                                            <asp:ListItem Text="Fecha"></asp:ListItem>
                                            <asp:ListItem Text="Rango de fecha"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                                 <div id="divEdicionModuloxFecha" runat="server" visible ="true">
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbedicionmodulofecha" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            </div>
                                 <div id="divEdicionModuloxFechas" runat="server" visible ="true">
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha inicial: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbedicionmodulofechaInicial" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            <div class="input-group">
                                <div class="fg-line">
                                    <label class="fg-label">Fecha final: <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbedicionmodulofechaFinal" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="6" />
                                </div>
                                <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                            </div>
                            </div>
                        </div>
                       <%-- termina consulta por tipo de edicion--%>

					</div>
                     <div class="form-group">
                                            <div class="col-md-12 col-sm-6 p-b-10">
                                             <asp:UpdatePanel runat="server" >
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary" TabIndex="3"><i class="fa fa-search"></i> Buscar</asp:LinkButton>
                                                        <asp:LinkButton ID="btnRegresar" runat="server" CssClass="btn btn-default" TabIndex="4"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                               </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
				</div>

			</div>

			<div class="pm-body clearfix">

				<div class="pmb-block">
					<div class="pmbb-header">
						<h2><i class="zmdi zmdi-user m-r-5"></i>Listado de historial, <asp:Label runat ="server" ID="lblhistorial"></asp:Label></h2>
					</div>
					
				</div>

				<div class="pmb-block">
					

					<div class="pmbb-body p-l-30">
                        <asp:UpdatePanel UpdateMode="Conditional" ID="updateListado" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="lsvHistoria" runat="server" ItemPlaceholderID="elementoPlaceHolder" ViewStateMode="Enabled">
                                    <LayoutTemplate>
                                        <table id="data-table" class="table table-striped table-vmiddle">
                                            <thead>
                                                <tr>
                                                    <th data-column-id="id" data-type="numeric" class="text-center" width="3%">Núm.</th>
                                                    <th data-column-id="txt" width="15%">Módulo</th>
                                                    <th data-column-id="opr" width="15%">Operación</th>
                                                    <th data-column-id="opr" width="40%" class="text-center">Usuario</th>
                                                    <th data-column-id="axn" width="18%" class="text-center">Fecha</th>
                                                    <th data-column-id="axn" width="5%">Acción</th>

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
                                                <asp:Label ID="lblnum" Text='<%#Container.DataItemIndex + 1 %> ' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblModulo" Text='<%#Eval("modulo")%>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOperacion" Text='<%#Eval("operacion")%>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUsuario" Text='<%#Eval("_Usuario")%>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaOperacion" Text='<%#Format(Eval("fechaOperacion"), "dd/MM/yyyy")%>' runat="server" />
                                            </td>
                                            <td style="text-align: center">
                                                <ul class="actions">

                                                    <li class="dropdown">
                                                        <a href="" data-toggle="dropdown" aria-expanded="false">
                                                            <i class="zmdi zmdi-more-vert"></i>
                                                        </a>

                                                        <ul class="dropdown-menu dropdown-menu-right">

                                                            <li>
                                                                <asp:LinkButton ID="lnkVer" runat="server" OnClick="lnkVer_Click" CommandName='<%#Eval("id") %>'> Consultar  </asp:LinkButton>
                                                            </li>


                                                        </ul>
                                                    </li>
                                                </ul>

                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                    <EmptyDataTemplate>
             
                                         
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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAltaUsuario.aspx.vb" Inherits="CPD.frmAltaUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">

        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="frmConsultaUsuario.aspx" class="text-uppercase">Usuario</a></li>
            <li class="active text-uppercase">Alta</li>
        </ol>
        <div class ="block-header">
            <h2 class="active text-uppercase">Alta de usuario
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
			    <h2><asp:Label runat="server" ID="lblTitulo"></asp:Label>Usuario</h2>
			</div>
        
            <div class="card-body card-padding">
                <div class="form-wizard form-wizard-basic form-wizard-horizontal fw-container">                   
                    <div class="form-wizard-nav">
                        <div class="progress"><div class="progress-bar progress-bar-primary"></div></div>
                       <ul class="nav nav-justified">
							<li class="active"><a href="#tab1" data-toggle="tab"><span class="step">1</span> <span class="title">Datos de usuario</span></a></li>
							<li><a href="#tab2" data-toggle="tab"><span class="step">2</span> <span class="title">Asignación de roles a</span></a></li>
						</ul>                                               
                    </div>                   
                    <div class="tab-content clearfix">
                        <div class="tab-pane active" id="tab1">
                            <div class="col-lg-12 col-md-12 col-xs-12">                                                                    
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-xs-12">
                                                <label class="col-sm-12 fg-label" for="form-group-input">
                                                    Nombre completo <span class="text-danger">(*)</span>
                                                </label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txbNombre" runat="server" class="form-control input-sm conTextoEnPascal" MaxLength="50" placeholder="Nombre(s) ApellidoPaterno ApellidoMaterno" ValidationGroup="val"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-xs-12">
                                                <label class="col-sm-12 fg-label" for="form-group-input">
                                                    Usuario <span class="text-danger">(*)</span>
                                                </label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txbUsuario" runat="server" class="form-control conTextoEnMinuscula" placeholder="Escribe el usuario" ValidationGroup="val"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                     <div class="row">


                                        <div  id="divPsw1" runat="server">
                                            <div class="col-lg-6 col-md-6 col-xs-12">
                                                <label class="col-sm-12 fg-label" for="form-group-input">
                                                    Contraseña <span class="text-danger">(*)</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txbContrasena" runat="server" type="password" class="form-control input-sm" placeholder="••••••••••" ValidationGroup="val"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divPsw2" runat="server">
                                            <div class="col-lg-6 col-md-6 col-xs-12">
                                                <label class="col-sm-12 fg-label" for="form-group-input">
                                                    Confirmar contraseña
                                    <asp:CompareValidator ID="cmpContraseña" runat="server" ErrorMessage=" Las contraseñas no son iguales." ValidationGroup="val"
                                        Operator="Equal" CssClass="label label-warning" ControlToCompare="txbContrasena" ControlToValidate="txbContrasena2" Type="String"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="rfvConfirme" runat="server" ErrorMessage=" campo requerido." CssClass="label label-warning" ValidationGroup="val" ControlToValidate="txbContrasena2"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txbContrasena2" runat="server" type="password" class="form-control input-sm"  ValidationGroup="val" placeholder="Confirma la contraseña"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                         </div>
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-xs-12">
                                                <label class="col-sm-12 fg-label" for="form-group-input">
                                                    Correo electrónico 
                                <asp:RegularExpressionValidator ID="revMail" runat="server" ErrorMessage="El formato del correo electrónico no es válido" CssClass="label label-warning"
                                    ControlToValidate="txbCorreoElectronico" ValidationGroup="val" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                                                </label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txbCorreoElectronico" runat="server" class="form-control" placeholder="ejemplo@dominio.com.mx"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-xs-12">
                                                <label class="col-sm-12 fg-label" for="form-group-input">
                                                    Teléfono</label>
                                                <div class="col-sm-12">                                                    
                                                    <asp:TextBox ID="txbTelefono" runat="server" class="form-control conMascaraTelefono" placeholder="eg: (999) 999 99-99" maxlength="15" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-xs-12">
                                                <label class="col-sm-12 fg-label" for="form-group-input">
                                                    Área <span class="text-danger">(*)</span> </label>
                                                <div class="select col-sm-12">
                                                    <asp:DropDownList ID="cmbArea" runat="server" class="form-control" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" runat="server" id="divResetPsw">
                                            <div class="col-lg-12 col-md-12 col-xs-12">
                                                <label class="col-sm-12 control-label" for="form-group-input">
                                                    Contraseña</label>
                                                <div class="col-sm-12">
                                                    <asp:CheckBox ID="chkResetPsw" runat="server" />
                                                    <span class="text">Deseas Resetear la contraseña?</span>
                                                </div>
                                            </div>
                                        </div>                                
                            </div>
                        </div>
                        <div class="tab-pane" id="tab2">
                            
                              

                                <div class="card-body card-padding">

                                    <div class="row">


                                        <div class="box box-default">
                                            <div class="box-header with-border">
                                                <div style="padding-left:50px; padding-bottom:30px;">
<h4 class="box-title">
                                                    
                                                    Control de acceso a sistema
                                                  
                                                </h4>
                                                </div>
                                                
                                            </div>
                                            <div class="box-body">
                                                <div class="col-lg-4 col-md-4 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-xs-12">
                                                <label class="col-sm-12 fg-label" for="form-group-input">
                                                    Rol <span class="text-danger">(*)</span></label>
                                                <div class="select col-sm-12">
                                                    <asp:DropDownList ID="cmbRol" runat="server" class="form-control" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-xs-12">
                                                <label class="col-sm-12 fg-label" for="form-group-input">
                                                    Elige un Sistema para el rol <span class="text-danger">(*)</span></label>
                                                <div class="select col-sm-12">
                                                    <asp:DropDownList ID="cmbSistema" runat="server" class="form-control" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-lg-12 col-md-12 col-xs-12">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="btnAgregarRol" runat="server" ValidationGroup="validarCampos" CssClass="btn btn-info btn-block "> <span class="fa fa-plus-circle"></span> Agregar Sistemas</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-xs-12" style="min-height: 200px;">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="row" runat="server" id="divListaroles" visible="false">
                                                        <div class="row" id="div1" runat="server">
                                                            <div class="col-lg-12 col-md-12 col-xs-12">
                                                                <asp:UpdatePanel ID="updatePanelBtns5" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="row">
                                                                            <h5> Lista de permisos <asp:Label ID="lblRolesDeUsuario" runat="server"></asp:Label></h5>    
                                                                            
                                                                        </div>
                                                                        
                                                                        <asp:ListView runat="server" ID="lvsRoles" ItemPlaceholderID="elementoPlaceHolder">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-bordered table-striped listaSinFiltro">
                                                                                    <thead class="cf">
                                                                                        <tr>
                                                                                            <th class="text-left" style="width: 45%">Rol</th>
                                                                                            <th class="text-center" style="width: 25%">Sistema</th>
                                                                                            <th class="text-center" style="width: 30%">Acción</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td class="text-left">
                                                                                        <asp:Label ID="lblRol" runat="server" Text='<%#Eval("nombre")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">
                                                                                        <asp:Label ID="lblSistema" runat="server" Text='<%#Eval("_nombreSistema") + " " + Eval("_añoSistema")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-center">
                                                                                        <asp:UpdatePanel ID="updatePanelBtns5" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:LinkButton ID="btnEliminarRol" runat="server" OnClick="btnEliminarRol_OnClick" CommandArgument='<%#Eval("idRol")%>' ToolTip='<%#Eval("id")%>' ClientIDMode="AutoID">
																		<span class="zmdi zmdi-delete"></span>
                                                                                                </asp:LinkButton>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <EmptyDataTemplate>
                                                                                <div class="alert alert-danger">
                                                                                    No hay registros
                                                                                </div>
                                                                            </EmptyDataTemplate>
                                                                        </asp:ListView>

                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                            </div>
                                        </div>                                       
                                    </div>
                                    <div class="row text-right">
                                    <asp:UpdatePanel ID="updatePanelBtns2" runat="server">
                                        <ContentTemplate>
                                            <asp:linkButton class="btn btn-success" runat="server" ID="btnGuardar"><span class="fa fa-check-circle"></span> Guardar</asp:linkButton>                                            
                                            <asp:LinkButton runat="server" CssClass="btn btn-default" id="btnCerrar"><span class="fa fa fa-times"></span> Cancelar</asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                </div>
                                   
                                <div class="text text-center">
                                <ul class="fw-footer pagination wizard text-center" >
                                <li class="previous first"><a class="a-prevent" href=""><i><span class="zmdi zmdi-more-horiz"></span></i></a></li>
                                <li class="previous"><a class="a-prevent" href=""><i><span class="zmdi zmdi-long-arrow-left"></span></i></a></li>
                                <li class="next"><a class="a-prevent" href=""><i><span class="zmdi zmdi-long-arrow-right"></span></i></a></li>
                                <li class="next last"><a class="a-prevent" href=""><i><span class="zmdi zmdi-more-horiz"></span></i></a></li>
                            </ul>
                            </div>
                            
                        </div>
                    </div>
                </div>
        </div>
        </div>
   </div>
      
    
</asp:Content>

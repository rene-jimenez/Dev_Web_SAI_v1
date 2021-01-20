<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmPerfil.aspx.vb" Inherits="CPD.frmPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/fileinput.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">


	<div class="container">
		<ol class="breadcrumb">
			<li><asp:LinkButton runat="server" ID="btnHome"><i class="fa fa-dashboard"></i> PRINCIPAL</asp:LinkButton></li>
			<li><a href="#" class="text-uppercase">Usuario</a></li>
			<li class="active text-uppercase">Perfil</li>
		</ol>

        <div class="card" id="profile-main">
            <div class="pm-overview c-overflow" style="overflow: hidden; outline: none;" tabindex="0">
                <div class="pmo-pic">
                    <div class="p-relative" id="imagenAvatar" runat ="server" >
                       <asp:Image  ID="imgAvatar" runat="server" class="image-responsible" />
                    </div>
                    <div class="kv-avatar center-block">
                        <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />
                        <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }
    </script>
                           
                        <asp:FileUpload ID="imgAvatarX" runat="server" class="file-loading" ClientIDMode="Static" MaximumNumberOfFiles="1" />
                        <br />
                        <div class="alert alert-warning alert-dismissible" runat="server" id="divMensajeImagen">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                            <asp:Label ID="lblMensajeImagen" runat="server" />
                        </div>
                    </div>
                    <div class="pmo-stat bgm-bluegray">
                        <h5 class="m-0 c-white"><span>
                            <asp:Label ID="lblnombre" runat="server"></asp:Label></span></h5>
                        <h4 class="c-white"><span>
                            <asp:Label ID="lblUsuario" runat="server" TabIndex="-1" BorderWidth="0px"></asp:Label></span></h4>
                    </div>
                </div>
            </div>
            <div class="pm-body clearfix">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="col-lg-8 col-md-8 col-xs-12">
                            <div class="pmb-block">
                                <div class="pmb-header">
                                    <h2><i class="zmdi zmdi-account m-r-5"></i>Información General</h2>
                                    <asp:TextBox ID="lblFotos" runat="server" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                                    <ul class="actions">
                                        <li class="dropdown">
                                            <a href="" data-toggle="dropdown">
                                                <i class="zmdi zmdi-more-vert"></i>
                                            </a>

                                            <ul class="dropdown-menu dropdown-menu-right">
                                                <li>
                                                    <asp:LinkButton runat="server" ID="btnEditar" class="btn btn-default">Editar
                                                          <span class="glyphicon glyphicon-pencil"></span>
                                                    </asp:LinkButton>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="pmbb-body p-l-30">
                                    <dl class="dl-horizontal">
                                        <dt class="p-t-10">Nombre completo</dt>
                                        <dd>
                                            <div class="fg-line">
                                                <asp:TextBox ID="txbNombre" class="form-control conTextoEnPascal" Text='<%#Eval("nombre")%>' placeholder="Nombre(s) ApellidoPaterno ApellidoMaterno" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Campo Nombre es obligatorio" CssClass="label label-warning" ControlToValidate="txbNombre" ValidationGroup="val" runat="server" />
                                            </div>

                                        </dd>
                                        <dt class="p-t-10">Usuario</dt>
                                        <dd>
                                            <div class="fg-line">
                                                <input type="text" class="form-control" placeholder="No puedes cambiar el usuario" disabled />
                                            </div>
                                        </dd>
                                        <dt class="p-t-10">Contraseña</dt>
                                        <dd>
                                            <div class="fg-line">
                                                <asp:TextBox MaxLength="40" ID="txbContrasena" Text='<%#Eval("contrasena")%>' runat="server" class="form-control" type="password" placeholder="**********"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Campo Obligatorio" CssClass="label label-warning" ControlToValidate="txbContrasena" ValidationGroup="val" runat="server" />
                                            </div>
                                        </dd>
                                        <dt class="p-t-10">Confirmar Contraseña</dt>
                                        <dd>
                                            <div class="fg-line">
                                                <asp:TextBox MaxLength="40" ID="txbConfirmarContrasena" Text='<%#Eval("confirmarContrasena")%>' runat="server" class="form-control" type="password" placeholder="*********"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Las cajas no son iguales" CssClass="label label-warning" ControlToValidate="txbConfirmarContrasena" ControlToCompare="txbContrasena" ValidationGroup="val"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Campo Obligatorio" CssClass="label label-warning" ControlToValidate="txbConfirmarContrasena" ValidationGroup="val" runat="server" />
                                            </div>
                                        </dd>
                                        <dt class="p-t-10">Teléfono</dt>
                                        <dd>
                                            <div class="fg-line">
                                                <asp:TextBox ID="txbTelefono" Text='<%#Eval("telefono")%>' runat="server" class="form-control conMascaraTelefono" placeholder="eg: (999) 999 99-99" maxlength="15" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </dd>
                                        <dt class="p-t-10">Correo electrónico</dt>
                                        <dd>
                                            <div class="fg-line">
                                                <asp:TextBox ID="txbCorreo" Text='<%#Eval("correoElectronico")%>' runat="server" class="form-control" placeholder="ejemplo@dominio.com.mx"></asp:TextBox>
                                                <asp:RegularExpressionValidator CssClass="label label-warning" ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txbCorreo" ErrorMessage="Formato de e-mail invalido"></asp:RegularExpressionValidator>
                                            </div>
                                        </dd>

                                    </dl>
                                    <label class="col-lg-12 col-md-12 col-xs-12 control-label"></label>
                                    <div class="m-t-30">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div class="form-group text-center" visible="false" id="divMostrar" runat="server">
                                                    <asp:LinkButton  runat ="server" ID="btnGuardar" cssClass="btn btn-success" >
                                                        <span class="glyphicon glyphicon-floppy-disk"></span> Guardar
                                                    </asp:LinkButton>  
                                                    <asp:LinkButton runat="server" ID="btnCerrar" CssClass="btn btn-default"><span class="fa fa-cog fa-spin"></span> Cancelar y Salir</asp:LinkButton>
                                                </div>
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


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
    <script src="../../js/fileinput.min.js"></script>
   <script>
        var foto = document.getElementById('<%=lblFotos.ClientID%>').value;
        var btnCust = '';
        $("#imgAvatarX").fileinput({
            overwriteInitial: false,
            showClose: true,
            showRemove:false,
            showCaption: false,
            showBrowse: false,
            browseOnZoneClick: true,
            showUpload: false,
            elErrorContainer: '#kv-avatar-errors-2',
            msgErrorClass: 'alert alert-block alert-danger',
            defaultPreviewContent: '<img src="' + foto + '" alt="Tu avatar" style="width:170px"><h6 class="text-muted">Click para seleccionar</h6>',
            layoutTemplates: { main2: '{preview} ' + btnCust + ' {remove} {browse}' }
        });
    </script>


</asp:Content>
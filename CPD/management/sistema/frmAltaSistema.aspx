<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAltaSistema.aspx.vb" Inherits="CPD.frmAltaSistema" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../css/fileinput.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
       <div class="container">
		<ol class="breadcrumb">
			<li><a class="text-uppercase" href="../default.aspx">PRINCIPAL</a></li>
			<li><a href="frmListaSistemas.aspx" class="text-uppercase">Sistema</a></li>
			<li class="active text-uppercase">ALTA</li>
		</ol>
			<div class="block-header">
				<h2 class="active text-uppercase">Alta de sistema</h2>                
			</div>
		<div class="clearfix"></div>
           <div class="card" id="profile-main">
               <div class="pm-overview c-overflow" style="overflow: hidden; outline: none;" tabindex="0">
                     <div class="pmo-pic">
                                <label class="fg-label">Logo</label>                           
                         <div class="p-relative" id="imagenAvatar" runat="server">
                             <asp:Image ID="imgAvatar" runat="server" class="image-responsible" />
                         </div>
                    <div class="kv-avatar center-block" id ="divUpdload" runat ="server" >
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
                     </div>
               </div>
               <div class="pm-body clearfix">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="col-lg-8 col-md-8 col-xs-12">                           
                             <div class="pmb-block">
                                 <div class="pmb-header">
                                     <h2><i class="zmdi zmdi-account m-r-5"></i>Información del Sistema</h2>
                                    <asp:TextBox ID="lblFotos" runat="server" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                                 </div>
                                 <div class="pmbb-body p-l-30">
                                     <div class="fg-line">
                                         <label class="fg-label">Nombre</label>
                                         <asp:TextBox ID="txbNombre" runat="server" class="form-control input-sm" placeholder="Escribe el nombre" />
                                     </div>
                                     <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Tipo de sistema</label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbtipoSistema" runat="server" CssClass="form-control" AppendDataBoundItems="True">  
                                        <asp:ListItem Text="Seleccione un elemento de la lista"  Enabled ></asp:ListItem>
                                        <asp:ListItem Text="Ordinario"></asp:ListItem>
                                        <asp:ListItem Text="Campaña"></asp:ListItem>                                                                
                                    </asp:DropDownList>
                                </div>                          
                        </div>
                         </div>
                                     <div class="form-group m-b-30">
                                         <div class="fg-line">
                                             <label class="fg-label">Año</label>
                                             <div class="select">
                                                 <asp:DropDownList ID="cmpAnio" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                     <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                     <asp:ListItem Text="2015"></asp:ListItem>
                                                     <asp:ListItem Text="2016"></asp:ListItem>
                                                     <asp:ListItem Text="2017"></asp:ListItem>
                                                     <asp:ListItem Text="2018"></asp:ListItem>
                                                     <asp:ListItem Text="2019"></asp:ListItem>
                                                     <asp:ListItem Text="2020"></asp:ListItem>
                                                 </asp:DropDownList>
                                             </div>


                                         </div>
                                     </div>
                                 </div>                                                                                                   
                             </div>                                                                                                                                           
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
           </div>
               <br />
               <br />
               <div class="text text-right">
                   <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-sm-12 text-right">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                                        <asp:LinkButton class="btn bgm-gray" runat="server" ID="btnCancelar"><i class="fa fa-times"></i> Cancelar</asp:LinkButton>
                                        <asp:LinkButton class="btn bgm-gray" runat="server" ID="btnCerrar"><i class="fa fa-cog"></i> Cerrar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
              </div>                                                       
          </div>
           
</div> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
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

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAjustarInventario.aspx.vb" Inherits="CPD.frmAjustarInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="conUpdatePanel" runat="server">

 <div class="container">
    <ol class="breadcrumb">
            <li><a href="~/default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Almacén</a></li>
            <li><a href="#" class="text-uppercase">Inventario</a></li>
            <li class="active text-uppercase">Ajustar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Almacén
            </h2>
        </div>
        <div class="clearfix"></div>
     <div class="row">
         <div class="col-md-8 col-md-offset-2">
         <div class="card ">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>
                    Ajustar inventario
                </h2>
            </div>
                                <div class="form-horizontal m-10">
                                <div class="form-group p-t-20">
								<label class="col-sm-4 control-label">Código de barras</label>
								<div class="col-sm-8">
									<div class="fg-line">
										<asp:label runat="server" ID="lblCodigoBarras" CssClass="form-control input-sm" TabIndex="1" />
									</div>
								</div>
							</div>
							<div class="form-group">
								<label class="col-sm-4 control-label">Artículo</label>
								<div class="col-sm-8">
									<div class="fg-line">
										<asp:label runat="server" ID="lblArticulo" CssClass="form-control input-sm" TabIndex="2"  />
									</div>
								</div>
							</div>
							<div class="form-group">
								<label class="col-sm-4 control-label">Fecha </label>
								<div class="col-sm-8">
                                     <div class="input-group">
                                                    <div class="fg-line">
                                                        
                                                        <asp:label ID="lblfecha" runat="server" TabIndex="3" />
                                                    </div>
                                                   <%-- <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>--%>
                                                </div>
								</div>
							</div>
                                 
                                
							<div class="form-group">
								<label class="col-sm-4 control-label">Cantidad a modificar<span class="text-danger">(*)</span></label>
								<div class="col-sm-8">
									<div class="fg-line">
										<asp:TextBox runat="server" ID="txbCantidad" onKeyPress="return acceptNum(event);" MaxLength="9"  class="form-control input-sm " TabIndex="4" />
									</div>
								</div>
							</div>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate >
                                            <div class="form-group">
								<label class="col-sm-4 control-label">Tipo de operación</label>
								<div class="col-sm-8">
									  <label class="radio radio-inline m-r-10">
                                                    <asp:RadioButton runat="server" id="rdbSumar" AutoPostBack="true" TabIndex="5"/>
                                <i class="input-helper"></i>  
                                Sumar
                            </label>
                                                    <label class="radio radio-inline m-r-10">
                                  <asp:RadioButton runat="server" id="rdbRestar" AutoPostBack="true" TabIndex="6" />

                                <i class="input-helper"></i>  
                                Restar
                            </label>
                                    
                                 </div>
								
							</div>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                            

                            <div class="form-group">
								<label class="col-sm-4 control-label">Comentario <span class="text-danger">(*)</span></label>
								<div class="col-sm-8">
									<div class="fg-line">
										<asp:TextBox ID="txbComentario" runat="server" class="form-control input-sm" Rows="2" MaxLength="500"  TextMode="MultiLine" TabIndex="9" />
									</div>
								</div>
							</div>


                            <div class="text text-right p-b-25 p-r-25">
                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar" TabIndex="7"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="8"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
</div>
             </div>
     </div>
</div>
 </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
      <script>
        var nav4 = window.Event ? true : false;
        function acceptNum(evt) {
            var key = nav4 ? evt.which : evt.keyCode;
            return (key <= 13 || (key >= 48 && key <= 57) || key == 40);
        }

    </script>
</asp:Content>

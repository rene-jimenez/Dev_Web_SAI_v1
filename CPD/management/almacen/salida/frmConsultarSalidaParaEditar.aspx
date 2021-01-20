<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmConsultarSalidaParaEditar.aspx.vb" Inherits="CPD.frmConsultarSalidaParaEditar" %>
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
                                                        <asp:TextBox ID="txbFechaInicial" runat="server" AutoPostBack="true" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="1"  />
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
                                                        
                                                        <asp:TextBox ID="txbFechaFinal" runat="server" AutoPostBack="true" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="2" OnTextChanged="txbFechaFinal_TextChanged" />
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
                                    <asp:DropDownList ID="cmbArea" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True"  TabIndex="3" OnSelectedIndexChanged="cmbArea_SelectedIndexChanged" AutoPostBack="true" >
                                        <asp:ListItem Text="Seleccione un elemento de la lista" Selected="True" Value="0"></asp:ListItem>
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
                                    <asp:DropDownList ID="cmbFolio" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True"  TabIndex="4" OnSelectedIndexChanged="cmbFolio_SelectedIndexChanged" AutoPostBack="true" >
                                        <asp:ListItem Text="Seleccione un elemento de la lista" Selected="True" Value="0"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
									</div>
								</div>
							</div>
							
							 <div class="row">
              <div class="text text-right p-b-25 p-r-25">
                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnEditar" TabIndex="5" OnClick="btnEditar_Click" ><i class="fa fa-pencil-square-o"></i> Editar</asp:LinkButton>
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCancelar" TabIndex="6" OnClick="btnCancelar_Click"><i class="fa fa-cog fa-spin"></i> Cancelar</asp:LinkButton>
                </div>
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

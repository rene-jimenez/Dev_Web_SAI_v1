<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmArea.aspx.vb" Inherits="CPD.frmArea1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
function valida(e){
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla==8){
        return true;
    }
        
    // Patron de entrada, en este caso solo acepta numeros
    patron =/[0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}
</script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
			<li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
			<li><a class="text-uppercase">ADMINISTRACIÓN</a></li>
			<li class="active text-uppercase">Área</li>
		</ol>
			<div class="block-header">
				<h2 class="active text-uppercase">área
					</h2>                
			</div>
    <div class="clearfix"></div>


    <div class="card">

        <div class="card-header ch-alt bgm-bluegray">
			    <h2><asp:Label runat="server" ID="lblTitulo"></asp:Label>área</h2>
			</div>
        
        <div class="card-body card-padding">
			<div role="tabpanel" class="tab">
                <ul class="tab-nav" role="tablist">
                      <li class="active"><a id="tiA" runat="server" href="#tabArea" data-toggle="tab">Área</a></li>
                       <li><a id="tiB" runat="server" href="#tabSubArea"   data-toggle="tab" >Subárea</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" runat="server" ClientIDMode="Static" id="tabArea">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <label class="fg-label">Nombre <span class="text-danger">(*)</span></label>
                                        <asp:TextBox for="name" ID="txbNombreArea" runat="server" class="form-control input-sm" MaxLength="100"  placeholder="Nombre del área" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <label class="fg-label">Código <span class="text-danger">(*)</span></label>
                                        <asp:TextBox ID="txbCodigoArea" runat="server" class="form-control input-sm" MaxLength="5" placeholder="Código del área" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <label class="fg-label">Tipo de área <span class="text-danger">(*)</span></label>
                                        <div class="select">
                                            <asp:DropDownList ID="cmbTipoArea" runat="server" CssClass="form-control conFiltrol" AppendDataBoundItems="True">
                                                <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                                <asp:ListItem Text="Interna"></asp:ListItem>
                                                <asp:ListItem Text="Externa"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <label class="fg-label">Jerarquía <span class="text-danger">(*)</span></label>
                                        <asp:TextBox ID="txbJerarquiaArea" runat="server" class="form-control input-sm" MaxLength="2" onkeypress="return valida(event)" placeholder="Jerarquía del área" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div style="padding-top: 20px; padding-bottom: 20px; text-align: right;">
                                <div runat="server" id="Tab1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton class="btn btn-success" runat="server" ID="lnkGuardarArea"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                                            <asp:linkbutton class="btn btn-default" runat="server" id="lnkCerrarArea" ><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- tab area--%>
                    <div class="tab-pane" runat="server" ClientIDMode="Static" id="tabSubArea">
                        <div class="row">
                            <div class="col-sm-6">
                                <%--<div class="form-group m-b-30">
                                    <div class="fg-line">--%>
                                        <label class="fg-label">Área principal <span class="text-danger">(*)</span></label>
                                        <div class="select">
                                            <asp:DropDownList ID="cmbAreaPadre" runat="server" cssClass="form-control " AppendDataBoundItems="True">
                                                <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    <%--</div>--%>
                                <%--</div>--%>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <label class="fg-label">Nombre <span class="text-danger">(*)</span></label>
                                        <asp:TextBox ID="txbNombreSubArea" runat="server" class="form-control input-sm" MaxLength="100" placeholder="Nombre del subárea" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <label class="fg-label">Código <span class="text-danger">(*)</span></label>
                                        <asp:TextBox ID="txbCodigoSubArea" runat="server" class="form-control input-sm" MaxLength="5" placeholder="Código del subárea" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <label class="fg-label">Tipo de área <span class="text-danger">(*)</span></label>
                                        <div class="select">
                                            <asp:DropDownList ID="cmbTipoAreaSub" runat="server" CssClass="form-control " AppendDataBoundItems="True">
                                                <asp:ListItem Text="Seleccione un elemento de la lista"></asp:ListItem>
                                                <asp:ListItem Text="Interna"></asp:ListItem>
                                                <asp:ListItem Text="Externa"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <label class="fg-label">Jerarquía <span class="text-danger">(*)</span></label>
                                        <asp:TextBox ID="txbJerarquiaSub" runat="server" class="form-control input-sm" MaxLength="2" onkeypress="return valida(event)" placeholder="Jerarquía del subárea" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 text-right">
                                <div runat="server" id="Tab2">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>

                                           <asp:LinkButton class="btn btn-success" runat="server" ID="lnkGuardarSub"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                                            <asp:linkbutton class="btn btn-default" runat="server" id="lnkCerrarSub" ><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>


                   </div><%-- tab subarea--%>
              

                  </div> <%--tab content--%>
              </div> <%--tabpanel--%>
					
             
         </div>  <%--card body--%>
     </div> <%--card--%>
</div> <%--*/container*/--%>



</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="conUpdatePanel" runat="server">

</asp:Content>
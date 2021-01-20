<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmBusquedaArticulo.aspx.vb" Inherits="CPD.frmBusquedaArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
		<ol class="breadcrumb">
			<li><a><asp:LinkButton ID="lnkPrincipal" runat="server" PostBackUrl="~/management/default.aspx">PRINCIPAL</asp:LinkButton></a></li>
			<li><a class="text-uppercase" href="frmPrincipalArticulo.aspx">ARTÍCULO</a></li>
			<li class="active text-uppercase">ALTA</li>
		</ol>
			<div class="block-header">
				<h2 class="active text-uppercase">BUSQUEDA ARTÍCULO	</h2>                
			</div>
		<div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Buscar artículo</h2>
            </div>
            <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-8">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Artículo</label>
                                <asp:TextBox ID="txbNombre" runat="server" class="form-control input-sm" placeholder="Escribe nombre de artículo" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Categoría</label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbCategoria" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">                                                                               
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-sm-12 text-right">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <asp:LinkButton class="btn btn-success" runat="server" ID="btnBuscar"><i class="fa fa fa-search"></i> Buscar</asp:LinkButton>
                                        <asp:LinkButton class="btn bgm-gray" runat="server" ID="btnCancelar"><i class="fa fa fa-times"></i> Cancelar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>                   
                </div>
            </div>
        </div> 
	</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

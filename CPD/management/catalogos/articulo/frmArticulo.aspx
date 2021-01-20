<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmArticulo.aspx.vb" Inherits="CPD.frmArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
		<ol class="breadcrumb">
			<li><a class="text-uppercase" href="../../default.aspx">PRINCIPAL</a></li>
			<li><a href="frmPrincipalArticulo.aspx" class="text-uppercase">ARTÍCULO</a></li>
			<li class="active text-uppercase">ALTA</li>
		</ol>
			<div class="block-header">
				<h2 class="active text-uppercase">
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
					</h2>                
			</div>
		<div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Ingrese datos de artículo</h2>
            </div>
            <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Nombre <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbNombre" runat="server" class="form-control input-sm" placeholder="Escribe el nombre" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Unidad medida <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbUnidadMedida" runat="server" CssClass="form-control" AppendDataBoundItems="True">                                        
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Código de barras <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbCodigoBarras" runat="server"  class="form-control input-sm" MaxLength="10" placeholder="Escribe el código de barras" AutoPostBack="True" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Cantidad inicial <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbCantidadInicial" runat="server" class="form-control input-sm soloNumeros " MaxLength="10" placeholder="Escribe la cantidad inicial" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Existencia <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbExistencia" runat="server"  class="form-control input-sm soloNumeros" MaxLength="10" placeholder="Escribe la existencia" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Stock mínimo <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbStockMinimo" runat="server" class="form-control input-sm soloNumeros"  MaxLength="10" placeholder="Escribe el stock mínimo" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Stock máximo <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbStockMaximo" runat="server" class="form-control input-sm soloNumeros " MaxLength="10" placeholder="Escribe el stock máximo" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">url</label>
                                <asp:TextBox ID="txbUrl" runat="server" class="form-control input-sm" placeholder="Escribe la url" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Categoría</label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbCategoria" runat="server" CssClass="form-control" AppendDataBoundItems="True">                                     
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Último precio <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbUltimoPrecio" runat="server" class="form-control input-sm" placeholder="Escribe el último precio" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Entra a almacén</label>
                                <label class="radio radio-inline m-r-20">
                                    <asp:RadioButton ID="rdbSi" runat="server" GroupName="opcion" />
                                    <i class="input-helper"></i>
                                    Si                                 
                                </label>
                                <label class="radio radio-inline m-r-20">
                                    <asp:RadioButton ID="rdbNo" runat="server" GroupName="opcion" />
                                    <i class="input-helper"></i>
                                    No
                                </label>                                
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-sm-12 text-right">
                                <div class="form-group m-b-30">
                                    <div class="fg-line">
                                        <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                                        <asp:LinkButton class="btn bgm-gray" runat="server" ID="btnCancelar"><i class="fa fa-times"></i> Cancelar</asp:LinkButton>
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

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmFirma.aspx.vb" Inherits="CPD.frmFirma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
         <div class="modal fade" id="myModalConfirm" tabindex="-2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                           <span class="text text-info"><h4 class="text-info">Confirmación </h4> </span> 
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="grid simple horizontal gray" id="divRespuesta" runat="server">
                                    <div class="grid-title ">
                                        <asp:Label ID='lblConfirmacionCuerpo' runat='server' />
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <div class="text-right" style="padding:25px;">
                                        <asp:Button ID="btnEventoConfirmar" Text="Confirmar" runat="server" class="btn bgm-cyan btn-sm"   />
                                        <asp:Button ID="btnCerrarConfirmacíon" Text="Cancelar" runat="server" class="btn btn-default btn-sm"  data-dismiss="modal" aria-hidden="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
		<ol class="breadcrumb">
			<li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
			<li><a href="#" class="text-uppercase">FIRMA</a></li>
		</ol>
			<div class="block-header">
				<h2 class="active text-uppercase">ALTA DE FIRMAS</h2>                
			</div>
		<div class="clearfix"></div>

        <div class ="row">
            <div class="col-md-7 col-sm-12">
                <div class="card" id="cardLista" runat="server">
                    <div class="card-header ch-alt bgm-bluegray">
                         <h2>Lista de firmas</h2>
                    </div>
                     <div class="card-body">
                         <asp:UpdatePanel runat="server">
                          <ContentTemplate>
                                    <asp:ListView runat="server" ID="lvsFirmas" ItemPlaceholderID="elementoPlaceHolder">
                                                                    <LayoutTemplate>
                                                                        <table id="data-table" class="table table-striped table-vmiddle">
                                                                            <thead class="cf">
                                                                                <tr>
                                                                                    <th data-column-id="id" style="width: 5%">ID</th>
                                                                                    <th class="text-left" style="width: 40%">Nombre</th>
                                                                                    <th class="text-center" style="width: 25%">Firma</th>
                                                                                    <th class="text-center" style="width: 30%">Acción</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr class="rating-list">
                                                                            <td><asp:Label ID="lblNum" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label></td>
                                                                            <td class="text-left">
                                                                                <asp:Label ID="lblUsuario" runat="server" Text='<%#Eval("_nombreUsuario")%>'></asp:Label>
                                                                            </td>
                                                                            <td class="text-center">
                                                                                <asp:Label ID="lblFirma" runat="server" Text='<%#Eval("nombre")%>'></asp:Label>
                                                                            </td>
                                                                            <td class="text-center">
                                                                                <asp:UpdatePanel ID="updatePanelBtns5" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:LinkButton ID="btnEliminarFirma" runat="server" CommandArgument='<%#Eval("id")%>' ToolTip='<%#Eval("id")%>' OnClick="btnEliminar_OnClick" CssClass="btn bgm-gray" ClientIDMode="AutoID">
																		                    <span class="fa fa-trash"></span>
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
            <div class="col-md-5 col-sm-12">
                <div class ="card" id="cardForm" runat="server">                     
                     <div class="card-header ch-alt bgm-bluegray">
                            <h2>Ingrese datos de firma</h2>
                    </div>
                     <div class="card-body card-padding">
                    <div class="row">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Usuario</label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbUsuarios" runat="server" CssClass="form-control" AppendDataBoundItems="True">                                   
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Tipo de Firma</label>
                                <div class="select">
                                        <asp:DropDownList ID="cmbFirma" runat="server" class="form-control">
                                        <asp:ListItem Text="Elige un tipo de firma" ></asp:ListItem>
                                        <asp:ListItem Text="Autoriza"></asp:ListItem>
                                        <asp:ListItem Text="Solicita"></asp:ListItem>
                                        <asp:ListItem Text="Elaboró"></asp:ListItem>
                                        <asp:ListItem Text="Revisó"></asp:ListItem>
                                        <asp:ListItem Text="Responsable"></asp:ListItem>
                                        <asp:ListItem Text="Cotizó"></asp:ListItem>
                                        <asp:ListItem Text="Revisó cotización"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>  
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                  <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar"><span class="fa fa-check-circle"></span> Guardar</asp:LinkButton>
                                        <%--<asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>--%>
                                        <asp:LinkButton class="btn btn-default" runat="server" ID="btnCancelar"><span class="fa fa-times"></span> Cancelar</asp:LinkButton>
                                        <%--<asp:LinkButton class="btn bgm-gray" runat="server" ID="btnCancelar"><i class="fa fa-times"></i> Cancelar</asp:LinkButton>--%>
                                    </ContentTemplate></asp:UpdatePanel>
                                
                            </div>
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
</asp:Content>

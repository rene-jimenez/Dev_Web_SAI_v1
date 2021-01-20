<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmPermiso.aspx.vb" Inherits="CPD.frmPermiso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
		<ol class="breadcrumb">
			<li><a href="../default.aspx"class="text-uppercase">PRINCIPAL</a></li>
			<li class="active text-uppercase">Permiso</li>
		</ol>
		<div class="block-header">
				<h2 class="active text-uppercase">Permiso
					</h2>                
		</div>


		<div class="col-sm-12 col-md-8 m-b-20">
				<div class="card">
					<div class="card-header ch-alt bgm-bluegray">
						<h2>Selecciona un rol</h2>
					</div>                
					<div class="card-body card-padding">
						<div class="form-group  m-b-30">
							<div class="fg-line">								
								<div class="select">
                                    <asp:DropDownList ID="cmbRol" runat="server" class="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                    					
								</div>
                                		
							</div>
						   <div><span class="input-group-addon">
                                <asp:LinkButton ID="btnAgregarRol" ClientIDMode="AutoID" runat="server" class="btn btn-icon waves-effect waves-circle waves-float bgm-blue" ValidationGroup="validarCampos">
                                    <i class="fa fa-arrow-right"></i>
                                </asp:LinkButton>
                               <asp:LinkButton runat="server" CssClass="btn bgm-bluegray btn-icon waves-effect waves-circle waves-float" id="btnCerrar" ><i class="fa fa-ban"></i></asp:LinkButton>
                            </span>	</div>
                        </div> 
                                           						
					</div>
				</div>
		</div>

		<div class="clearfix"></div>	
         <div id="divPaginas" class="col-lg-8 col-md-7 col-xs-12"  runat="server" visible="false">
                <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Lista de permisos rol <asp:Label ID="lblNombreRol" runat="server"></asp:Label></h2>
            </div>
             
             <div class="card-body card-padding" >                               
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:ListView ID="lsvPermiso" runat="server" ItemPlaceholderID="bodyListView">
                                    <LayoutTemplate>
                                        <table  class="table table-striped table-vmiddle" aria-busy="false">
                                            <thead>
                                                <th class="text-left" style="">Páginas</th>
                                                <th style="width: 25%; text-align: center"  class="select-cell text-right">
                                                    <asp:LinkButton ID="btnMarcar" runat="server" OnClick="btnMarcar_OnClick" Text="Marcar |">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDesmarcar" runat="server" OnClick="btnDesmarcar_OnClick" Text="Desmarcar">
                                                    </asp:LinkButton>
                                                </th>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="bodyListView" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:TextBox Style="font-size: 12px;" runat="server" ID="txbLsvNombre" Text='<%#Eval("nombrePagina")%>' aria-describedby="inputSuccess3Status" CssClass="form-control" BackColor="#ccff99" Visible="false" />
                                                <asp:Label ID="labelLsvPermiso" Text='<%#Eval("nombrePagina")%>' runat="server" ToolTip='<%#Eval("idPagina")%>' />
                                            </td>
                                            <td style="width: 20%; text-align: center">
                                                <asp:CheckBox ID="chkEsActivo" runat="server" class="select-box" Checked='<%#Eval("esActivo")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>                    
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:ListView ID="lvsPaginas" runat="server" ItemPlaceholderID="bodyListView">
                                    <LayoutTemplate>
                                        <table class="table table-bordered table-striped">
                                            <thead>
                                                <th>Paginas</th>
                                                <th style="width: 25%; text-align: center" class="text-center">
                                                    <asp:LinkButton ID="btnMarcar" runat="server" OnClick="btnMarcar_OnClick" Text="Marcar |">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDesmarcar" runat="server" OnClick="btnDesmarcar_OnClick" Text="Desmarcar">
                                                    </asp:LinkButton>
                                                </th>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="bodyListView" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:TextBox Style="font-size: 12px;" runat="server" ID="txbLsvNombre" Text='<%#Eval("nombre")%>' aria-describedby="inputSuccess3Status" CssClass="form-control" BackColor="#ccff99" Visible="false" />
                                                <asp:Label ID="labelLsvPermiso" Text='<%#Eval("nombre")%>' runat="server" ToolTip='<%#Eval("id")%>' />
                                            </td>
                                            <td style="width: 20%; text-align: center">
                                                <asp:CheckBox ID="chkEsActivo" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                 <div class="card-footer">
                <div class="text text-right">
                            <asp:LinkButton ID="btnActualizar" Text="Actualizar" runat="server" CausesValidation="false" ToolTip="Actualizar" CssClass="btn btn-success waves-effect"><i class="fa fa-check-circle"></i>
                                    Asignar
                            </asp:LinkButton>
                        </div>
            </div>
			 </div>  
            
            	
        </div>
         </div>
		


	</div>
</asp:Content>
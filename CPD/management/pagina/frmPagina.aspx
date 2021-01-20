<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmPagina.aspx.vb" Inherits="CPD.frmPagina" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">


	<div class="container">
		<ol class="breadcrumb">
			<li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
			<li class="active text-uppercase">Página</li>
		</ol>

		<div class="block-header">
			<h2 class="active text-uppercase">Alta de Páginas
			</h2>
		</div>
 <div class="clearfix"></div>

        <div class="card">
        <div class="card-header ch-alt bgm-bluegray">
			    <h2><asp:Label runat="server" ID="lblTitulo"></asp:Label>Página</h2>
			</div>
        
        <div class="card-body card-padding">            
                <div class="form-wizard form-wizard-basic form-wizard-horizontal fw-containerl">                   
                    <div class="form-wizard-nav">
                        <div class="progress"><div class="progress-bar progress-bar-primary"></div></div>
                       <ul class="nav nav-justified">
							<li class="active"><a href="#tabPagina" data-toggle="tab"><span class="step">1</span> <span class="title">Página</span></a></li>
							<li><a href="#tabSubPagina" data-toggle="tab"><span class="step">2</span> <span class="title">Sub página</span></a></li>
						</ul>                                               
                    </div>   
                    <div class="tab-content clearfix">
                         <div class="tab-pane active" id="tabPagina">
                            <div class="row">
					             <div class="col-sm-6">
					                  <div class="form-group m-b-30">
					                    	<div class="fg-line">
						                    	<label class="fg-label">Nombre</label>
                                                <asp:TextBox ID="txbNombre" runat="server" class="form-control input-sm" placeholder="Nombre de la página" />
							                </div>
						              </div>
					             </div>
                                 <div class="col-sm-6">
					                    	    <div class="form-group m-b-30">
					                    		    <div class="fg-line">
						                    		    <label class="fg-label">URL</label>
                                                        <asp:TextBox ID="txbUrl" runat="server" class="form-control input-sm" placeholder="url" />
							                        </div>
						                        </div>
                                            </div>                                 
                            </div>
                            <div class="row">
                                <div runat="server" id="Tab1" class="text text-right">
                                              <asp:UpdatePanel runat="server"> 
                                                  <ContentTemplate>
                                                     <asp:linkButton runat="server" CssClass="btn btn-success" ID="btnGuardarPagina" ValidationGroup="val"><i class="fa fa-check-circle" ></i> Guardar página</asp:linkButton>                                            
                                                     <asp:LinkButton runat="server" CssClass="btn btn-default" id="btnCerrarPagina"><span class="fa fa-cog fa-spin"></span>Cancelar y Salir</asp:LinkButton>
                                              </ContentTemplate>
                                              </asp:UpdatePanel>
                                            </div>                                
                            </div>
                         </div>
                         <div class="tab-pane"  id="tabSubPagina">                                                          
                                <div class="card-body card-padding">
                                    <div class ="row">
                                        <div class="col-sm-6">
					                    	<div class="form-group m-b-30">
					                    		<div class="fg-line">
						                    		<label class="fg-label">Página principal</label>
								                        <div class="select">
                                                        <asp:DropDownList ID="cmbPaginaPrincipal" runat="server" cssClass="form-control" AppendDataBoundItems="True">                                                     
                                                        </asp:DropDownList>
									                  	</div>
							                    </div>
						                    </div>
					                    </div>
                                    </div>
                                   <div class="row">                                        
					                    <div class="col-sm-6">
					                    	<div class="form-group m-b-30">
					                    		<div class="fg-line">
						                    		<label class="fg-label">Nombre</label>
                                                    <asp:TextBox ID="txbNombreSubPagina" runat="server" class="form-control input-sm" placeholder="Nombre de la subPágina" />
							                    </div>
						                    </div>
					                    </div>
                                        <div class="col-sm-6">
					                    	<div class="form-group m-b-30">
					                    		<div class="fg-line">
						                    		<label class="fg-label">Código</label>
                                                    <asp:TextBox ID="txbUrlSubPagina" runat="server" class="form-control input-sm" placeholder="URL de la subPágina" />
							                    </div>
						                    </div>
                                        </div>                                        
                                  </div>
                                        <div class="row">
                                                <div runat="server" id="Tab2" class="text text-right">
                                              <asp:UpdatePanel runat="server"> <ContentTemplate> 
                                                  <asp:linkButton runat="server" CssClass="btn btn-success" ID="btnGuardarSub" ValidationGroup="val"><i class="fa fa-check-circle" ></i> Guardar Sub página</asp:linkButton>                                            
                                                  <asp:LinkButton runat="server" CssClass="btn btn-default" id="btnCerrarSub"><span class="fa fa-cog fa-spin"></span>Cerrar</asp:LinkButton>
                                              </ContentTemplate>
                                              </asp:UpdatePanel>
                                            </div>
                                            </div>
                                  </div>                                   
                                    <div class="clearfix"></div>
                                    <br />
                                    <br />
                                                           
                               </div>                                                      
                        
                        <div class="text text-center">
                                      <ul class="fw-footer pagination wizard text-center" >
                                          <li class="previous first disabled"><a class="a-prevent" href=""><i><span class="zmdi zmdi-more-horiz"></span></i></a></li>
                                          <li class="previous disabled"><a class="a-prevent" href=""><i><span class="zmdi zmdi-long-arrow-left"></span></i></a></li>
                                          <li class="next"><a class="a-prevent" href=""><i><span class="zmdi zmdi-long-arrow-right"></span></i></a></li>
                                          <li class="next last"><a class="a-prevent" href=""><i><span class="zmdi zmdi-more-horiz"></span></i></a></li>
                                     </ul>
                         </div>
                    </div>
                </div>
			 <%--tabpanel--%>					             
         </div> 
         <%--card body--%>
     </div> 
        </div>
	<%--	<div class="row">
			<div class="col-lg-4 col-md-4 col-sm-4">
				<div class="card">
					<div class="card-header ch-alt bgm-bluegray">
						<h2>Nombre del Menú</h2>
					</div>
                
					<div class="card-body card-padding">
						<div class="form-group  m-b-30">
							<div class="fg-line">
								<input type="text" class="form-control" placeholder="Nombre página" />
							</div>
						</div>
						<div class="form-group  m-b-30">
							<div class="fg-line">
								<input type="text" class="form-control" placeholder="URL" />
							</div>
						</div>
							<button class="btn btn-info btn-sm">Agregar</button>
					</div>
					<div class="p-10">
						<button class="btn btn-success"><label class="text">Administración </label> <i class="zmdi zmdi-chevron-right"> </i></button>
					</div>

				</div>
			</div>


			<div class="col-lg-4 col-md-4 col-sm-4">
				<div class="card">
					<div class="card-header ch-alt bgm-bluegray">
						<h2>Nombre del Sub-menú</h2>
					</div>
                
					<div class="card-body card-padding">
						<div class="form-group  m-b-30">
							<div class="fg-line">
								<input type="text" class="form-control" placeholder="Nombre página" />
							</div>
						</div>
						<div class="form-group  m-b-30">
							<div class="fg-line">
								<input type="text" class="form-control" placeholder="URL" />
							</div>
						</div>
							<button class="btn btn-info btn-sm">Agregar</button>
					</div>
					<div class="p-10">
						<button class="btn btn-success"><label class="text">Administración 2 </label> <i class="zmdi zmdi-chevron-right"> </i></button>
					</div>
				</div>
			</div>

			<div class="col-lg-4 col-md-4 col-sm-4">
				<div class="card">
					<div class="card-header ch-alt bgm-bluegray">
						<h2>Nombre del Sub-sub-menú</h2>
					</div>
                
					<div class="card-body card-padding">
						<div class="form-group  m-b-30">
							<div class="fg-line">
								<input type="text" class="form-control" placeholder="Nombre página" />
							</div>
						</div>
						<div class="form-group  m-b-30">
							<div class="fg-line">
								<input type="text" class="form-control" placeholder="URL" />
							</div>
						</div>
							<button class="btn btn-info btn-sm">Agregar</button>
					</div>
					<div class="p-10">
						<button class="btn btn-success"><label class="text">Administración 3 </label> <i class="zmdi zmdi-chevron-right"> </i></button>
					</div>
				</div>
			</div>
			

		</div>--%>





			<%--termina el div container--%>
</asp:Content>

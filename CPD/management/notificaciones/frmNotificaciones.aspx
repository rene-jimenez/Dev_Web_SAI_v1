<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmNotificaciones.aspx.vb" Inherits="CPD.frmNotificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">

	<div class="container">

		<ol class="breadcrumb">
			<li><a href="#" class="text-uppercase">PRINCIPAL</a></li>
			<li class="active text-uppercase">Notificaciones</li>
		</ol>

		<div class="block-header">
			<h2 class="active text-uppercase">Notificaciones de usuario
			</h2>
		</div>

		<%-- este bloque es la bandea de entrada LISTA DE MENSAJES--%>
		<div class="card m-b-0" id="messages-main1">
                        
            <div class="ms-menu">
                            <div class="ms-block">
                                <div class="ms-user">
                                    <img src='<%=Page.ResolveUrl("~/img/perfiles/jv3.jpg")%>' alt="">
                                    <div><h4>Admin</h4></div>
                                </div>
                            </div>
                            
                            
                            
                            <div class="listview lv-user m-t-20">
                                <div class="lv-item media active">
                                    <div class="lv-avatar bgm-green pull-left">
                                        <i class="zmdi wobble zmdi-email-open"></i>
                                    </div>
                                    <div class="media-body">
                                        <div class="lv-title">INBOX</div>
                                        <div class="lv-small">Bandeja de Entrada</div>
                                    </div>
                                </div>
                                
                                <div class="lv-item media">
                                    <div class="lv-avatar bgm-orange pull-left">
										<i class="zmdi zmdi-email"></i>
                                    </div>
                                    <div class="media-body">
                                        <div class="lv-title">OUTBOX</div>
                                        <div class="lv-small">Bandeja de Salida</div>
                                    </div>
                                </div>
                                
                                <div class="lv-item media">
                                    <div class="lv-avatar bgm-red pull-left">
										<i class="zmdi zmdi-delete"></i>
                                    </div>
                                    <div class="media-body">
                                        <div class="lv-title">TRASH</div>
                                        <div class="lv-small">Mensajes Borrados</div>
                                    </div>
                                </div>
                                
                                
                                
                                
                                
                                
                            </div>

                            
                        </div>
            
            <div class="ms-body">
                            <div class="listview lv-message">
                                <div class="lv-header-alt clearfix">
                                    
                                    <div class="lvh-label hidden-xs">
                                        
                                        <span class="c-black">Bandeja de Entrada</span>
                                    </div>
                                    
                                    
                                </div>
                                
                                <div class="lv-body">
                                    
                                    <a class="lv-item" href="">
                                        <div class="media">
                                            <div class="pull-left">
                                                <img class="lv-img-sm" src='<%=Page.ResolveUrl("~/img/browsers/chrome.png")%>' alt="" />
                                            </div>
                                            <div class="media-body">
                                                <div class="lv-title">Agus</div>
                                                <small class="lv-small">Amigo, hazme un reporte de proveedores que contenga la siguiente información: ...</small>
                                            </div>
                                        </div>
                                    </a>
                                    <a class="lv-item" href="">
                                        <div class="media">
                                            <div class="pull-left">
                                                <img class="lv-img-sm" src='<%=Page.ResolveUrl("~/img/browsers/firefox.png")%>' alt="" />
                                            </div>
                                            <div class="media-body">
                                                <div class="lv-title">Bere</div>
                                                <small class="lv-small">Ya remplazé los archivos que me solicitaste ....</small>
                                            </div>
                                        </div>
                                    </a>
									<a class="lv-item" href="">
                                        <div class="media">
                                            <div class="pull-left">
                                                <img class="lv-img-sm" src='<%=Page.ResolveUrl("~/img/browsers/safari.png")%>' alt="" />
                                            </div>
                                            <div class="media-body">
                                                <div class="lv-title">Thve</div>
                                                <small class="lv-small">Hola, que traes puesto hoy???</small>
                                            </div>
                                        </div>
                                    </a>
                                </div>




							</div>
			</div>

		</div>
		
		<div class="m-b-20"></div>
		<div class="clearfix"></div>
			
		<%-- este es el bloque cuando entran a la bandeja de entrada a ver un MENSAJE INDIVIDUAL --%>
		<div class="block-header">
			<h2 class="active text-uppercase">Notificaciones de usuario
			</h2>
		</div>
		
		<div class="card m-b-0" id="messages-main">
                        
            <div class="ms-menu">
                            <div class="ms-block">
                                <div class="ms-user">
                                    <img src='<%=Page.ResolveUrl("~/img/perfiles/jv3.jpg")%>' alt="">
                                    <div><h4>Admin</h4></div>
                                </div>
                            </div>
                            
                            
                            
                            <div class="listview lv-user m-t-20">
                                <div class="lv-item media active">
                                    <div class="lv-avatar bgm-green pull-left">
                                        <i class="zmdi wobble zmdi-email-open"></i>
                                    </div>
                                    <div class="media-body">
                                        <div class="lv-title">INBOX</div>
                                        <div class="lv-small">Bandeja de Entrada</div>
                                    </div>
                                </div>
                                
                                <div class="lv-item media">
                                    <div class="lv-avatar bgm-orange pull-left">
										<i class="zmdi zmdi-email"></i>
                                    </div>
                                    <div class="media-body">
                                        <div class="lv-title">OUTBOX</div>
                                        <div class="lv-small">Bandeja de Salida</div>
                                    </div>
                                </div>
                                
                                <div class="lv-item media">
                                    <div class="lv-avatar bgm-red pull-left">
										<i class="zmdi zmdi-delete"></i>
                                    </div>
                                    <div class="media-body">
                                        <div class="lv-title">TRASH</div>
                                        <div class="lv-small">Mensajes Borrados</div>
                                    </div>
                                </div>
                                
                                
                                
                                
                                
                                
                            </div>

                            
                        </div>
            
            <div class="ms-body">
                            <div class="listview lv-message">
                                <div class="lv-header-alt clearfix">
                                    
                                    <div class="lvh-label hidden-xs">
                                        
                                        <span class="c-black">Bandeja de Entrada</span>
                                    </div>
                                    
                                    <ul class="lv-actions actions">
                                        
                                        <li>
                                            <a href="">
                                                <i class="zmdi zmdi-check"></i>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="">
                                                <i class="zmdi zmdi-delete"></i>
                                            </a>
                                        </li>                         
                                        
                                    </ul>
                                </div>
                                
                                <div class="lv-body">                                    
                                    
									<div class="p-15">
                                    <div class="tv-header media">
                                        
                                        <div class="media-body p-t-5">
                                            <h3 class="d-block">Agus</h3>
                                            <small class="c-gray">10 de mayo de 2016, a las 12:20</small>
                                        </div>
                                        
                                        
                                    </div>
                                    <div class="tv-body">
                                        
                                       
                                        <p>Amigo, hazme un reporte de proveedores que contenga la siguiente información: Quien de ellos es mayor de 30 años, cuales tienen bigote y de esos a quién le salió primero el bigote.</p>
                                                                        
                                        <div class="clearfix"></div>
                                    
                                        
                                </div>






                                </div>
                                <%-- este bloque funciona para mandar un mensaje al usuario que estoy viendo el mensaje --%>
                                <div class="lv-footer ms-reply">
                                    <textarea placeholder="Contesta a Agus un mensaje rápido..."></textarea>
                                    
                                    <button><i class="zmdi zmdi-mail-send"></i></button>
                                </div>
                            </div>
                        </div>
						</div>

		</div>



	
		<%-- la bandeja de salida y mensajes borrados funcionan similar a la de entrada (osea llevan la misma estructura que la de entrada) --%>
		
		
	</div><%--termina el div container--%>




	

</asp:Content>

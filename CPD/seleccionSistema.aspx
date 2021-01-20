<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="seleccionSistema.aspx.vb" Inherits="CPD.seleccionSistema" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link href='<%#Page.ResolveUrl("~/css/animate.min.css")%>' rel="stylesheet" />
		<link href='<%#Page.ResolveUrl("~/css/sweet-alert.css")%>' rel="stylesheet" />
		<link href='<%#Page.ResolveUrl("~/css/material-design-iconic-font.min.css")%>' rel="stylesheet" />
		<link href='<%#Page.ResolveUrl("~/css/jquery.mCustomScrollbar.min.css")%>' rel="stylesheet" />  
		<link href='<%#Page.ResolveUrl("~/css/font-awesome.min.css")%>' rel="stylesheet" /> 
	    <!-- estos CSS van al final y los demas van dentro del content head -->
		<link href='<%#Page.ResolveUrl("~/css/main1.css")%>' rel="stylesheet" /><!-- 1 -->
		<link href='<%#Page.ResolveUrl("~/css/main2.css")%>' rel="stylesheet" /><!-- 2 -->

</head>
<body>
    <form id="form1" runat="server">
    <div class="container">

     <div class="block-header" style="padding-top:100px;">
 <h2 class="active text-uppercase">Administrador de aplicaciones</h2>

</div>
        
    	<div class="card" id="profile-main">
			<div class="pm-overview c-overflow" style="overflow: hidden; outline: none;" tabindex="0">
				<div class="pmo-pic">
					<div class="p-relative">
                        <asp:Image ImageUrl="~/img/perfiles/Avatar_PRI.png" ID="imgAvatar" runat="server" class="image-responsible" />
					</div>
					<div class="pmo-stat bgm-bluegray">
						<h5 class="m-0 c-white"><span><asp:Label ID="lblNombreUsuario" runat="server" />
                            </span></h5>
						<h4 class="c-white"><span><asp:Label ID="lblUsuario" runat="server" /></span></h4>
										</div>
				</div>

			</div>
			<div class="pm-body clearfix">
				<div class="pmb-block">
					<div class="pmbb-header">
						<h2><i class="zmdi zmdi-user m-r-5"></i>Seleccione un tipo de aplicación</h2>
					</div>
					<div class="pmbb-body p-l-30">
						<div class="pmbb-view">
							De clic en la imagen que especifica el tipo de sistema al que desee ingresar. Si no le aparece ninguna imagen es posible que el administrador haya desabilitado su cuenta, si es así contacte a su administrador de sistema, éste le proporcionara las credenciales necesarias para acceder al sistema deseado.
						</div>
					</div>
				</div>
                          <div class="col-sm-12">
                                    <div role="tabpanel" class="tab">
                                        <ul class="tab-nav" role="tablist" tabindex="3" style="overflow: hidden; outline: none;">
                                            <li class="active"><a href="#tabOrdinario" aria-controls="tabOrdinario" role="tab" data-toggle="tab" aria-expanded="true">Ordinario</a></li>
                                            <li role="presentation" class=""><a href="#tabCampaña" aria-controls="tabCampaña" role="tab" data-toggle="tab" aria-expanded="false">Campaña</a></li>
                                        
                                        </ul>
                                        <div class="tab-content">
                                            <div role="tabpanel" class="tab-pane animated fadeInRight active" id="tabOrdinario">                       
                                               <asp:ListView ID="lvData" runat="server" ItemPlaceholderID="phData">
                                                    <LayoutTemplate>
                                                        <asp:PlaceHolder ID="phData" runat="server"></asp:PlaceHolder>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <a href='#' id='Tab0' onserverclick="btnRedi_OnClick" runat='server' dataid='<%#Eval("idSistemaRol")%>'>
                                                            <div class='col-sm-12 col-md-6'>
                                                                <div class='mini-charts-item bgm-green'>
                                                                    <div class='clearfix'>
                                                                        <div class='chart stats-bar'>
                                                                            <div class='pull-left'>
                                                                                <img class='lv-img-md' src='<%# "img/" + Eval("_fotoSistema").ToString()%>' alt=''>
                                                                            </div>
                                                                        </div>
                                                                        <div class='count'>
                                                                            <small><%#Eval("_nombreSistema")%></small><h2><strong><%#Eval("_añoSistema")%></strong></h2>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </a>       
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                                     
                                             </div>
                                            <div role="tabpanel" class="tab-pane animated fadeInRight" id="tabCampaña">
                                                <asp:ListView ID="lvsCampaña" runat="server" ItemPlaceholderID="phData">
                                                    <LayoutTemplate>
                                                        <asp:PlaceHolder ID="phData" runat="server"></asp:PlaceHolder>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <a href='#' id='Tab0' onserverclick="btnRedi_OnClick" runat='server' dataid='<%#Eval("idSistemaRol")%>'>
                                                            <div class='col-sm-12 col-md-6'>
                                                                <div class='mini-charts-item bgm-red'>
                                                                    <div class='clearfix'>
                                                                        <div class='chart stats-bar'>
                                                                            <div class='pull-left'>
                                                                                <img class='lv-img-md' src='<%# "img/" + Eval("_fotoSistema").ToString()%>' alt=''>
                                                                            </div>
                                                                        </div>
                                                                        <div class='count'>
                                                                            <small><%#Eval("_nombreSistema")%></small><h2><strong><%#Eval("_añoSistema")%></strong></h2>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </a>       
                                                    </ItemTemplate>
                                                </asp:ListView>
                                           </div>
                                        </div>
                                    </div>
                                </div>
                        <div class="pmb-block">
							<div class="pmbb-body p-l-30">

                                <div class="row m-t-25 p-0 m-b-25">

		</div>
					</div>
                </div>

			</div>
		</div>

    </div>
    </form>

       <script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script>
        <script src="<%=Page.ResolveUrl("~/js/bootstrap.min.js")%>" type="text/javascript"></script>
        <script src="<%=Page.ResolveUrl("~/js/sweet-alert.min.js")%>" type="text/javascript"></script>
        <script src="<%=Page.ResolveUrl("~/js/jquery.mCustomScrollbar.concat.min.js")%>" type="text/javascript"></script>
        <script src="<%=Page.ResolveUrl("~/js/cads_V1.js")%>" type="text/javascript"></script>
      
        <!-- Placeholder for IE9 -->
        <!--[if IE 9 ]>
            <script src="<%=Page.ResolveUrl("~/js/jquery.placeholder.min.js")%>" type="text/javascript"></script>
        <![endif]-->

        <script src="<%=Page.ResolveUrl("~/js/functions.js")%>" type="text/javascript"></script>

		<script >
		    if ($('[data-pmb-action]')[0]) {
		        $('body').on('click', '[data-pmb-action]', function (e) {
		            e.preventDefault();
		            var d = $(this).data('pmb-action');

		            if (d === "edit") {
		                $(this).closest('.pmb-block').toggleClass('toggled');
		            }

		            if (d === "reset") {
		                $(this).closest('.pmb-block').removeClass('toggled');
		            }


		        });
		    }
		</script>
</body>

</html>

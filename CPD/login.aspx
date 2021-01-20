<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="CPD.login" %>

<!DOCTYPE html>
<html>
	<head>
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<title>:::::LOGIN INTO SAI:::::</title>
		
		<link href="css/animate.min.css" rel="stylesheet" />
		<link href="css/sweet-alert.css" rel="stylesheet" />
		<link href="css/material-design-iconic-font.min.css" rel="stylesheet" />
		<link href="css/jquery.mCustomScrollbar.min.css" rel="stylesheet" />   

		<!-- estos CSS van al final y los demas van dentro del content head -->
		<link href="css/main1.css" rel="stylesheet" /><!-- 1 -->
		<link href="css/main2.css" rel="stylesheet" /><!-- 2 -->

	</head>

	<body class="login-content">
	
			    <form id="form1" runat="server">

                    <div style="padding-top:150px;">

                    </div>
        <asp:Panel ID="hittingMe" runat="server" DefaultButton="BtnValidar" class="lc-block lcb-alt toggled"> 
            
            <img class="lcb-pic" src="img/sispro-logo-square.jpg" alt="">
            

            <div style="padding-top:45px;">

       <div class="fg-line">
               <asp:TextBox runat="server" ID="txbUsuario" CssClass="form-control" placeholder="Escribe tu usuario" />
            </div>
			<div class="m-b-20">
                
            </div>

			<div class="fg-line">
            <asp:TextBox runat="server" ID="txbPassword" CssClass="form-control" placeholder="Escribe tu contraseña" Textmode="Password" /> 
 
            </div>
			<div class="m-b-20">
                
            </div>
                <asp:ScriptManager runat="server" />
                <asp:UpdatePanel runat="server">
                        <ContentTemplate>
        <asp:LinkButton  runat="server" class="btn btn-login btn-danger" ID="btnValidar" data-type="danger" data-align="center" data-from="top" data-animation-in="animated flipInY" data-animation-out="animated bounceOut">
                  <i class="zmdi zmdi-arrow-forward"></i> ENTRAR
            </asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
     
        

           <br /><br />


            </div>

     
            
        </asp:Panel>
			    </form>
		  


	   
		 <footer id="footer">
			
		</footer>


	
		
		<!-- Older IE warning message -->
		<!--[if lt IE 9]>
			<div class="ie-warning">
				<h1 class="c-white">Cuidado!!</h1>
				<p>Estás usando una versión muy antigua de Internet Explorer <br/>por favor usa cualquiera de los siguientes navegadores web.</p>
				<div class="iew-container">
					<ul class="iew-download">
						<li>
							<a href="http://www.google.com/chrome/">
								<img src="../img/browsers/chrome.png" alt="">
								<div>Chrome</div>
							</a>
						</li>
						<li>
							<a href="https://www.mozilla.org/en-US/firefox/new/">
								<img src="../img/browsers/firefox.png" alt="">
								<div>Firefox</div>
							</a>
						</li>
						<li>
							<a href="http://www.opera.com">
								<img src="../img/browsers/opera.png" alt="">
								<div>Opera</div>
							</a>
						</li>
						<li>
							<a href="https://www.apple.com/safari/">
								<img src="../img/browsers/safari.png" alt="">
								<div>Safari</div>
							</a>
						</li>
						<li>
							<a href="http://windows.microsoft.com/en-us/internet-explorer/download-ie">
								<img src="../img/browsers/ie.png" alt="">
								<div>IE (New)</div>
							</a>
						</li>
					</ul>
				</div>
				<p>Disculpa las molestias!</p>
			</div>   
		<![endif]-->


		<!-- Javascript Libraries -->
<%--		<script src="js/jquery.min.js" type="text/javascript"></script>--%>
        <script src="js/jquery-2.1.1.min.js"></script>
		<script src="js/bootstrap.min.js" type="text/javascript"></script>
        <script src="js/sweetalert.min.js"></script>
        <script src="js/waves.min.js"></script>
		<script src="js/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>
        <script src="js/bootstrap-growl.min.js"></script>
        <script src="js/functions.js"></script>
     
		<!-- Placeholder for IE9 -->
		<!--[if IE 9 ]>
			<script src="../js/jquery.placeholder.min.js" type="text/javascript"></script>
		<![endif]-->

	
	<script type="text/javascript">
	    /*
         * Notifications
         */
	    function notify(from, align, icon, type, animIn, animOut) {
	        $.growl({
	            icon: icon,
	            title: ' ¿Atención! ',
	            message: 'El usuario o la contraseña son incorrectos.',
	            url: ''
	        }, {
	            element: 'body',
	            type: type,
	            allow_dismiss: true,
	            placement: {
	                from: from,
	                align: align
	            },
	            offset: {
	                x: 20,
	                y: 85
	            },
	            spacing: 10,
	            z_index: 1031,
	            delay: 4500,
	            timer: 2000,
	            url_target: '_blank',
	            mouse_over: false,
	            animate: {
	                enter: animIn,
	                exit: animOut
	            },
	            icon_type: 'class',
	            template: '<div data-growl="container" class="alert" role="alert">' +
                                '<button type="button" class="close" data-growl="dismiss">' +
                                    '<span aria-hidden="true">&times;</span>' +
                                    '<span class="sr-only">Close</span>' +
                                '</button>' +
                                '<span data-growl="icon"></span>' +
                                '<span data-growl="title"></span>' +
                                '<span data-growl="message"></span>' +
                                '<a href="#" data-growl="url"></a>' +
                            '</div>'
	        });
	    };
        </script>
	</body>
</html>

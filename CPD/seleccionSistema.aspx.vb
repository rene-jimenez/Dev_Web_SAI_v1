Imports cadenero.entidades.nspUsuarioAutenticado
Imports cadenero.CRN.nspUsuario
Public Class seleccionSistema : Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Header.DataBind()
        If Not IsPostBack Then
            Try
                Dim usuario As usuarioAutenticado = Session("autentication")
                Dim usuarioR = New cadenero.CRN.nspUsuario.Proceso_ObtenerUsuario() With {.id = usuario.idUsuario}.Ejecutar
                Dim cad1 As New System.Text.StringBuilder
                Dim cad2 As New System.Text.StringBuilder
                lblNombreUsuario.Text = usuarioR.nombre
                lblUsuario.Text = usuarioR.usuario
                imgAvatar.ImageUrl = "~/img/perfiles/" + usuario.foto
                Dim listaSistemasUsuario = New cadenero.CRN.nspUsuarioRol.Proceso_ObtenerUsuarioRoles() With {.tipoConsulta = cadenero.entidades.nspUsuarioRol.tipoConsultaUsuarioRol.idUsuario, .idUsuario = usuario.idUsuario}.Ejecutar
                Dim listaOrdinario = listaSistemasUsuario.Where(Function(a) a._tipo = "Ordinario").ToList
                Dim listaCampaña = listaSistemasUsuario.Where(Function(a) a._tipo = "Campaña").ToList
                If listaOrdinario.Count > 0 Then
                    lvData.DataSource = listaOrdinario.ToList()
                    lvData.DataBind()
                End If
                If listaCampaña.Count > 0 Then
                    lvsCampaña.DataSource = listaCampaña.ToList()
                    lvsCampaña.DataBind()
                End If
            Catch ex As Exception
                Response.Redirect("login.aspx")
            End Try
        End If
    End Sub
    Protected Sub btnRedi_OnClick(sender As Object, e As EventArgs)
        Dim btnredi As HtmlAnchor = sender
        Dim posicion As String = btnredi.Attributes("dataId")
        Dim usuario As usuarioAutenticado = Session("autentication")
        Dim sistema = New CRN.nspSistema.Proceso_ObtenerSistema() With {.id = Guid.Parse(posicion)}.Ejecutar
        Session("user") = usuario
        Session("system_Sispro") = sistema
        FormsAuthentication.RedirectFromLoginPage(UCase(usuario.nombreUsuario), True)
        Response.Redirect("~/management/default.aspx")
    End Sub
    Private Function mensajeVacio() As String
        Return "<div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> Atención! No hay cuentas activas para éste usuario, consulta con el Administrador de sistemas.</div>"
    End Function

End Class
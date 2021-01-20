Imports cadenero.entidades.nspUsuarioAutenticado
Imports cadenero.CRN.nspUsuario
Imports cadenero.entidades.nspUsuario
Imports cadenero.CRN.nspUsuarioAutenticado
Public Class login
    Inherits System.Web.UI.Page

    Private Sub btnValidar_Click(sender As Object, e As EventArgs) Handles btnValidar.Click
        Dim usuarioAutenticado = New Proceso_UsuarioAutenticado() With {.Usuario = txbUsuario.Text, .Contrasena = txbPassword.Text}.Ejecutar
        If usuarioAutenticado.Count = 0 Then
            Dim cadena As String = "notify('top', 'center', '', 'danger', 'animated flipInY', 'animated bounceOut');"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", cadena, True)
        Else
            Session("autentication") = usuarioAutenticado.FirstOrDefault
            If usuarioAutenticado(0).resetear Then
                Response.Redirect("~/nuevoPassword.aspx")
            Else
                Response.Redirect("~/seleccionSistema.aspx")
            End If

        End If
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session.RemoveAll()
            Session.Clear()
            txbUsuario.Focus()

        End If
    End Sub
End Class
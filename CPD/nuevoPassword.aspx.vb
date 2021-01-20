Imports cadenero.entidades.nspUsuarioAutenticado
Imports cadenero.CRN.nspUsuario
Public Class nuevoPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim usuario As usuarioAutenticado = Session("autentication")
            lblNombre.Text = usuario.nombreUsuario
            lblId.Value = usuario.idUsuario.ToString
            imgAvatar.ImageUrl = "~/img/perfiles/" + usuario.foto
        End If
    End Sub

    Private Sub btnGuardarContrasena_Click(sender As Object, e As EventArgs) Handles btnGuardarContrasena.Click
        Try
            If txbContrasenaNueva.Text = "" Then
                Throw New Exception("El campo contraseña es obligatorio.")
            End If
            If txbContrasenaNueva2.Text = "" Then
                Throw New Exception("Confirme la contraseña antes de coontinuar.")
            End If
            If txbContrasenaNueva.Text.Trim <> txbContrasenaNueva2.Text.Trim Then
                txbContrasenaNueva.Text = ""
                txbContrasenaNueva2.Text = ""
                Throw New Exception("Las contraseñas no coinciden.")
            Else
                Dim editarUsuario = New Proceso_ObtenerUsuario() With {.id = Guid.Parse(lblId.Value.ToString)}.Ejecutar()
                Dim biblioteca As New Contexto.Biblioteca.controladorDeFunciones.Conversion
                Dim pass = biblioteca.strToEncryptSHAUTF8(txbContrasenaNueva.Text)
                editarUsuario.contrasena = pass
                If editarUsuario.email = Nothing Then
                    editarUsuario.email = "ejemplo@tic.com"
                End If
                If editarUsuario.telefono = Nothing Then
                    editarUsuario.telefono = "No tiene"
                End If
                editarUsuario.esResetContrasena = False
                editarUsuario.idUsuarioMovimiento = Guid.Parse(lblId.Value.ToString)
                editarUsuario.ipUsuario = "Desconocido"
                Dim respuesta = New Proceso_ActualizarUsuario() With {.entidad = editarUsuario}.Ejecutar()
                Select Case respuesta.respuesta
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                        Response.Redirect("~/seleccionSistema.aspx")
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                        Throw New Exception(respuesta.comentario.ToString)
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                        Throw New Exception(respuesta.comentario.ToString)
                End Select
            End If
        Catch ex As Exception
            Dim cadena As String = "notify('top', 'center', '', 'danger', 'animated flipInY', 'animated bounceOut','" + ex.Message.ToString + "');"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", cadena, True)
        End Try
    End Sub


End Class
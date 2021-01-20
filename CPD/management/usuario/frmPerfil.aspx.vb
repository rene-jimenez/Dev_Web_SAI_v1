Imports cadenero.CRN.nspUsuario
Imports Contexto.Notificaciones.controladorMensajes
Imports cadenero.entidades.nspUsuarioAutenticado
Imports CES.nspPopup
Public Class frmPerfil : Inherits nspPaginaBase.PaginaBase
    Dim controlador As New notificacionesDeUsuario
    Dim avatarx As HtmlInputFile
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        imgAvatarX.Attributes("onchange") = "UploadFile(this)"
        If Not IsPostBack Then
            Try
                divMensajeImagen.Visible = False
                Dim cargarDatos = New Proceso_ObtenerUsuario() With {.id = IdUsuario}.Ejecutar
                txbNombre.Text = cargarDatos.nombre
                txbTelefono.Text = cargarDatos.telefono
                txbCorreo.Text = cargarDatos.email
                txbContrasena.Text = cargarDatos.contrasena
                txbConfirmarContrasena.Text = cargarDatos.contrasena
                lblnombre.Text = cargarDatos.nombre
                lblUsuario.Text = cargarDatos.usuario
                activarDesactivarControles(True)
                imgAvatar.ImageUrl = "~/img/perfiles/" + cargarDatos.foto
                lblFotos.Text = "~/img/perfiles/" + cargarDatos.foto
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub

    Protected Sub Upload(sender As Object, e As EventArgs)
        Try
            If Request.Files.Count > 0 Then
                Dim filename As String = imgAvatarX.FileName
                Dim nombreArchivo As String = ""
                Select Case True
                    Case filename.EndsWith(".jpg")
                        nombreArchivo = obtieneNombreImagen(".jpg")
                    Case filename.EndsWith(".jpeg")
                        nombreArchivo = obtieneNombreImagen(".jpeg")
                    Case filename.EndsWith(".png")
                        nombreArchivo = obtieneNombreImagen(".png")
                    Case filename.EndsWith(".tif")
                        nombreArchivo = obtieneNombreImagen(".tif")
                    Case Else
                        Throw New Exception
                End Select
                Dim strUploadPath As String = "~/img/perfiles/"
                imgAvatarX.SaveAs(Server.MapPath(strUploadPath) + nombreArchivo)
                Session("nombreFoto") = nombreArchivo
                Dim datosUsuario = New Proceso_ObtenerUsuario() With {.id = IdUsuario}.Ejecutar()
                datosUsuario.foto = nombreArchivo.ToString
                Dim respuestaProcesoActualizar = New Proceso_ActualizarUsuario() With {.entidad = datosUsuario}.Ejecutar()
                Select Case respuestaProcesoActualizar.respuesta
                    Case tipoRespuestaDelProceso.Completado
                        divMensajeImagen.Visible = True
                        lblFotos.ToolTip = nombreArchivo
                        divMensajeImagen.Attributes("class") = "alert alert-success alert-dismissible"
                        lblMensajeImagen.Text = " <i class='fa fa-check-circle' aria-hidden='true'></i>  Listo"
                        lblFotos.Text = "~/img/perfiles/" + nombreArchivo
                        imgAvatar.ImageUrl = "~/img/perfiles/" + nombreArchivo
                    Case Else
                        divMensajeImagen.Visible = True
                        divMensajeImagen.Attributes("class") = "alert alert-warning alert-dismissible"
                        lblMensajeImagen.Text = " <i class='fa fa-exclamation-triangle' aria-hidden='true'></i> " + nombreArchivo
                        lblFotos.Text = "../img/perfiles/userIncorrect.jpg"
                        imgAvatar.ImageUrl = "../img/perfiles/userIncorrect.jpg"
                End Select

            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try

    End Sub



#Region "btns"
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        mandaDefault()
    End Sub
    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        activarDesactivarControles(False)
        divMostrar.Visible = True
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim respuestaValidacion = validarUsuario()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                '   Throw New Exception(resultado.comentario)
                OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            Dim fotoActualizada As String
            If Not Session("nombreFoto") Is Nothing Then
                fotoActualizada = Session("nombreFoto")
            Else
                fotoActualizada = "avatar.jpg"
            End If

            Dim datosUsuarioEditar = New cadenero.CRN.nspUsuario.Proceso_ObtenerUsuario() With {.id = IdUsuario}.Ejecutar
            datosUsuarioEditar.nombre = txbNombre.Text
            datosUsuarioEditar.telefono = txbTelefono.Text
            datosUsuarioEditar.contrasena = txbContrasena.Text
            datosUsuarioEditar.esResetContrasena = False
            datosUsuarioEditar.email = txbCorreo.Text
            datosUsuarioEditar.foto = fotoActualizada.ToString
            Dim resultadoEditar = New cadenero.CRN.nspUsuario.Proceso_ActualizarUsuario() With {.entidad = datosUsuarioEditar, .reset = False}.Ejecutar

            Select Case resultadoEditar.respuesta
                Case tipoRespuestaDelProceso.Completado
                    activarDesactivarControles(True)
                    divMostrar.Visible = False
                    OnMostrarMensajeAccion("Confirmación", controlador.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "perfil"), tipoPopup.Verde, True, "management/usuario/frmPerfil.aspx")
                    'OnMostrarMensajeAccion("", "Datos actualizados correctamente", tipoPopup.Verde, True, "management/usuario/frmPerfil.aspx")
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", resultadoEditar.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", resultadoEditar.comentario, tipoPopup.Rojo, False, "")

            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub
#End Region

#Region "métodos"
    Private Sub activarDesactivarControles(bandera As Boolean)
        txbNombre.ReadOnly = bandera
        txbTelefono.ReadOnly = bandera
        txbCorreo.ReadOnly = bandera
        txbContrasena.ReadOnly = bandera
        txbConfirmarContrasena.ReadOnly = bandera
    End Sub

    Private Function validarUsuario() As respuestaDelProceso
        Dim resultadoValidacion As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbNombre.Text = "" Then
            resultadoValidacion.comentario = "El nombre es un campo obligatorio"
            resultadoValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado
            Return resultadoValidacion
        End If
        If txbContrasena.Text = "" Then
            resultadoValidacion.comentario = "El campo contraseña es obligatoria"
            resultadoValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado
            Return resultadoValidacion
        End If
        If txbConfirmarContrasena.Text = "" Then
            resultadoValidacion.comentario = "El campo confirmar contraseña es obligatorio"
            resultadoValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado
            Return resultadoValidacion
        End If
        Return resultadoValidacion
    End Function


    Private Function obtieneNombreImagen(ext As String)
        Dim nombre As String = Date.Now.Day.ToString + Date.Now.Hour.ToString + Date.Now.Minute.ToString + Date.Now.Second.ToString + ext
        Return nombre
    End Function
    Private Sub eliminarSessionArchivo()
        Session("nombreFoto") = Nothing
        Session.Remove("nombreFoto")
    End Sub

#End Region


End Class
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup, CES.nspSistema
Imports CRN.nspSistema
Imports System.Transactions
Public Class frmAltaSistema : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim avatarx As HtmlInputFile
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        imgAvatarX.Attributes("onchange") = "UploadFile(this)"
        If Not IsPostBack Then
            divMensajeImagen.Visible = False
            Select Case Request.QueryString("band")
                Case "edt"
                    poblarDatosSistema()
                    btnCancelar.Visible = True
                    btnCerrar.Visible = False
                Case "cns"
                    poblarDatosSistema()
                    deshabilitarControles()
                    btnGuardar.Visible = False
                    btnCancelar.Visible = False
                    btnCerrar.Visible = True
                    divUpdload.Visible = False
                Case Else
                    lblFotos.ToolTip = "Vacío"
                    btnCancelar.Visible = True
                    btnCerrar.Visible = False
            End Select
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            If Request.QueryString("band") = "edt" Then
                Dim mensaje = validarCampos()
                If mensaje.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                    Throw New Exception(mensaje.comentario)
                End If
                Dim edtSistema = New Proceso_ObtenerSistema() With {.id = Guid.Parse(Request.QueryString("id"))}.Ejecutar()
                edtSistema.nombre = txbNombre.Text
                edtSistema.tipo = cmbtipoSistema.SelectedValue
                edtSistema.año = cmpAnio.SelectedValue
                edtSistema.ipUsuario = direccionIP
                edtSistema.idUsuarioMovimiento = IdUsuario
                edtSistema.idSistema = sistema.sistemaActivo.idSistema
                Dim respuesta = New Proceso_ActualizarSistema() With {.entidad = edtSistema}.Ejecutar()
                Select Case respuesta.respuesta
                    Case tipoRespuestaDelProceso.Completado
                        OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "sistema"), tipoPopup.Verde, True, "management/default.aspx")
                    Case tipoRespuestaDelProceso.Advertencia
                        OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Rojo, False, "")
                    Case tipoRespuestaDelProceso.NoCompletado
                        OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Rojo, False, "")
                End Select
            Else 'alta
                Dim nombreArchivo = lblFotos.ToolTip.ToString
                Dim mensaje = validarCampos()
                If mensaje.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                    Throw New Exception(mensaje.comentario)
                End If
                Dim nuevoSistema As New sistema
                nuevoSistema.id = Guid.NewGuid
                nuevoSistema.nombre = txbNombre.Text
                nuevoSistema.tipo = cmbtipoSistema.SelectedValue
                nuevoSistema.año = cmpAnio.SelectedValue
                nuevoSistema.ipUsuario = direccionIP
                nuevoSistema.idUsuarioMovimiento = IdUsuario
                nuevoSistema.idSistema = sistema.sistemaActivo.idSistema
                nuevoSistema.logo = nombreArchivo
                Using scope As New TransactionScope()
                    Dim respuesta = New Proceso_AgregarSistema() With {.entidad = nuevoSistema}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            Dim msj = addUsuariosAdmin(nuevoSistema.id)
                            If msj.respuesta <> tipoRespuestaDelProceso.Completado Then
                                Throw New Exception(mensaje.comentario)
                            Else
                                OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_guardaron, "sistema"), tipoPopup.Verde, True, "management/default.aspx")
                            End If
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                    scope.Complete()
                End Using
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
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
                        divMensajeImagen.Visible = True
                        divMensajeImagen.Attributes("class") = "alert alert-warning alert-dismissible"
                        lblMensajeImagen.Text = " <i class='fa fa-exclamation-triangle' aria-hidden='true'></i> Formato de imagen incorrecto"
                        Exit Sub
                End Select
                lblFotos.ToolTip = nombreArchivo
                divMensajeImagen.Visible = True
                divMensajeImagen.Attributes("class") = "alert alert-success alert-dismissible"
                lblMensajeImagen.Text = " <i class='fa fa-check-circle' aria-hidden='true'></i>  Formato correcto!"

                If Request.QueryString("band") = "edt" Then
                    subirImagen(nombreArchivo)
                Else
                    Dim strUploadPath As String = "~/img/"
                    imgAvatarX.SaveAs(Server.MapPath(strUploadPath) + nombreArchivo)
                    lblFotos.Text = "~/img/" + nombreArchivo
                    imgAvatar.ImageUrl = "~/img/" + nombreArchivo
                End If
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub


    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        mandaDefault()
    End Sub
    Private Sub poblarDatosSistema()
        Dim consulta = New Proceso_ObtenerSistema() With {.id = Guid.Parse(Request.QueryString("id"))}.Ejecutar()
        txbNombre.Text = consulta.nombre.ToString
        cmbtipoSistema.SelectedValue = consulta.tipo
        cmpAnio.SelectedValue = consulta.año
        lblFotos.Text = "~/img/" + consulta.logo.ToString
        imgAvatar.ImageUrl = "~/img/" + consulta.logo.ToString
    End Sub
    Private Sub deshabilitarControles()
        txbNombre.ReadOnly = True
        cmbtipoSistema.Enabled = False
        cmpAnio.Enabled = False
    End Sub
    Public Sub subirImagen(nombreArchivo)
        Dim strUploadPath As String = "~/img/"
        imgAvatarX.SaveAs(Server.MapPath(strUploadPath) + nombreArchivo)
        Dim consulta = New Proceso_ObtenerSistema() With {.id = Guid.Parse(Request.QueryString("id"))}.Ejecutar()
        consulta.logo = nombreArchivo.ToString
        Dim respuestaProcesoActualizar = New Proceso_ActualizarSistema() With {.entidad = consulta}.Ejecutar()
        Select Case respuestaProcesoActualizar.respuesta
            Case tipoRespuestaDelProceso.Completado
                divMensajeImagen.Visible = True
                divMensajeImagen.Attributes("class") = "alert alert-success alert-dismissible"
                lblMensajeImagen.Text = " <i class='fa fa-check-circle' aria-hidden='true'></i>  Listo"
                lblFotos.Text = "~/img/" + nombreArchivo
                imgAvatar.ImageUrl = "~/img/" + nombreArchivo
            Case Else
                divMensajeImagen.Visible = True
                divMensajeImagen.Attributes("class") = "alert alert-warning alert-dismissible"
                lblMensajeImagen.Text = " <i class='fa fa-exclamation-triangle' aria-hidden='true'></i> " + nombreArchivo
                lblFotos.Text = "../img/loading.jpg"
                imgAvatar.ImageUrl = "../img/loading.jpg"
        End Select
    End Sub
    Private Sub eliminarSessionArchivo()
        Session("nombreFoto") = Nothing
        Session.Remove("nombreFoto")
    End Sub

    Private Function addUsuariosAdmin(idSistema As Guid)
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        ' Dim consultaRol = New cadenero.CRN.nspRol.Proceso_ObtenerRol() With {.id = Guid.Parse("efd014a0-4091-4daa-80d6-0fa5f9deaa98")}.Ejecutar()
        Dim consultaAdms = New CRN.nspUsuariosAdmin.Proceso_ObtenerIdUsuariosAdmin().Ejecutar()
        Using scope As New TransactionScope()
            For i = 0 To consultaAdms.Count - 1
                Dim newUsuario As New cadenero.entidades.nspUsuarioRol.usuarioRol
                newUsuario.id = Guid.NewGuid
                newUsuario.idUsuario = consultaAdms(0).idUsuarioAdministrador
                newUsuario.idSistemaRol = idSistema
                newUsuario.idRol = Guid.Parse("efd014a0-4091-4daa-80d6-0fa5f9deaa98")
                newUsuario.esActivo = True
                newUsuario.ipUsuario = direccionIP
                newUsuario.idUsuarioMovimiento = IdUsuario
                newUsuario.idSistema = sistemaActivo.id
                Dim respuestaAdd = New cadenero.CRN.nspUsuarioRol.Proceso_AgregarUsuarioRol() With {.entidad = newUsuario}.Ejecutar()
                Select Case respuestaAdd.respuesta
                    Case tipoRespuestaDelProceso.Completado
                        respuesta.respuesta = tipoRespuestaDelProceso.Completado
                       ' OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "entrada a almacén"), tipoPopup.Verde, False)
                    Case tipoRespuestaDelProceso.Advertencia
                        OnMostrarMensajeAccion("Atención", respuestaAdd.comentario, tipoPopup.Naranja, False, "")
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                    Case tipoRespuestaDelProceso.NoCompletado
                        OnMostrarMensajeAccion("Atención", respuestaAdd.comentario, tipoPopup.Rojo, False, "")
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End Select
            Next
            scope.Complete()
        End Using
        Return respuesta
    End Function
    Private Function validarCampos()
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbNombre.Text.ToString = "" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "turnoSAF")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf cmbtipoSistema.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "área")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf cmpAnio.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "responsable")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf lblFotos.ToolTip = "Vacío" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "imagen")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        End If

        Return respuesta
    End Function
    Private Function obtieneNombreImagen(ext As String)
        Dim nombre As String = Date.Now.Day.ToString + Date.Now.Hour.ToString + Date.Now.Minute.ToString + Date.Now.Second.ToString + ext
        Return nombre
    End Function

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Response.Redirect("management/sistema/frmListaSistemas.aspx")
    End Sub
End Class
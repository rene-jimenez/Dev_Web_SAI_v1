Imports CES, CRN
Imports CRN.nspArea
Imports CES.nspArea

Imports Contexto.Notificaciones.controladorMensajes
Public Class frmArea1 : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarAreasPrincipales()
                Dim accion = Request.QueryString("band").ToString
                Select Case accion
                    Case "edt"
                        lblTitulo.Text = "Editar el "
                        Dim id As String = Request.QueryString("id").ToString  'recibe id 

                        Dim areaEditar = New Proceso_ObtenerArea() With {.id = Guid.Parse(Request.QueryString("id"))}.Ejecutar()
                        If areaEditar.idAreaPadre Is Nothing Then 'Area padre
                            tabSubArea.Visible = False
                            tiB.Visible = False
                            tabArea.Attributes("Class") = ("tab-pane active")
                            tabSubArea.Attributes("Class") = ("tab-pane")
                            CargarAreaEditar(areaEditar)

                        Else                                'Area hija
                            tabArea.Visible = False
                            tiA.Visible = False
                            CargarAreaHijoEditar(areaEditar)
                            tabArea.Attributes("Class") = ("tab-pane")
                            tabSubArea.Attributes("Class") = ("tab-pane active")
                        End If

                        'llenarArea(id)
                    Case "add"
                        lblTitulo.Text = "Crear nueva "
                    Case Else
                        mandaDefault()
                End Select


            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub


    Protected Sub llenarAreasPrincipales()
        Dim consultaAreasPrincipales = New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.idAreaPadreEsActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbAreaPadre.DataValueField = "id"
        cmbAreaPadre.DataTextField = "nombre"
        cmbAreaPadre.DataSource = consultaAreasPrincipales.OrderBy(Function(a) a.nombre).ToList
        cmbAreaPadre.DataBind()
        cmbAreaPadre.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub CargarAreaEditar(consultaObtenerUnArea As area) 'area padre

        Try
            txbNombreArea.Text = consultaObtenerUnArea.nombre.ToString
            txbCodigoArea.Text = consultaObtenerUnArea.codigo.ToString
            cmbTipoArea.SelectedValue = consultaObtenerUnArea.tipo.ToString
            txbJerarquiaArea.Text = consultaObtenerUnArea.jerarquia.ToString

        Catch ex As Exception
            OnMostrarMensajeAccion("Área", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
        End Try

    End Sub
    Protected Sub CargarAreaHijoEditar(consultaObtenerUnArea As area) 'area hija
        Try
            Dim IdAreaPadre As Guid = consultaObtenerUnArea.idAreaPadre
            txbNombreSubArea.Text = consultaObtenerUnArea.nombre.ToString
            txbCodigoSubArea.Text = consultaObtenerUnArea.codigo.ToString
            cmbAreaPadre.SelectedValue = consultaObtenerUnArea.idAreaPadre.ToString
            cmbTipoAreaSub.SelectedValue = consultaObtenerUnArea.tipo.ToString
            txbJerarquiaSub.Text = consultaObtenerUnArea.jerarquia.ToString

        Catch ex As Exception
            OnMostrarMensajeAccion("Área", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
        End Try
    End Sub
    Public Sub llenarArea(id As String)
        Dim consultaArea = New CRN.nspArea.Proceso_ObtenerArea() With {.id = Guid.Parse(id)}.Ejecutar()
        txbNombreArea.Text = consultaArea.nombre.ToString
        txbCodigoArea.Text = consultaArea.codigo.ToString
        cmbTipoArea.SelectedValue = consultaArea.tipo.ToString
        txbJerarquiaArea.Text = consultaArea.jerarquia
    End Sub
    Private Function validarArea() As Contexto.Notificaciones.controladorMensajes.respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbNombreArea.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El nombre del área es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        If txbCodigoArea.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo código área es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If

        If cmbTipoArea.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo tipo del área es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        If txbJerarquiaArea.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo jerarquía del área es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function
    Private Function validarSubArea() As Contexto.Notificaciones.controladorMensajes.respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If cmbAreaPadre.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo área principal es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        If txbNombreSubArea.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo nombre área es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        If txbCodigoSubArea.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo código área es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        If cmbTipoAreaSub.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo tipo del área es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        If txbJerarquiaSub.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo jerarquía del área es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        'If txbJerarquiaSub.Text > "1" Then
        '    respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
        '    respuesta.comentario = "El campo jerarquía debe ser mayor a uno"
        'End If

        Return respuesta
    End Function
    Private Sub lnkGuardarArea_Click(sender As Object, e As EventArgs) Handles lnkGuardarArea.Click
        Try
            Dim resultadoValidacion = validarArea()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim accion = Request.QueryString("band").ToString
            Select Case accion
                Case "edt"
                    Dim id As String = Request.QueryString("id").ToString  'recibe id 
                    Dim editArea = New CRN.nspArea.Proceso_ObtenerArea() With {.id = Guid.Parse(Request.QueryString("id"))}.Ejecutar
                    editArea.nombre = txbNombreArea.Text
                    editArea.codigo = txbCodigoArea.Text
                    editArea.tipo = cmbTipoArea.SelectedValue
                    editArea.jerarquia = txbJerarquiaArea.Text
                    editArea.idUsuarioMovimiento = IdUsuario
                    editArea.ipUsuario = direccionIP
                    editArea.idSistema = sistemaActivo.idSistema
                    'editArea.idAreaPadre = Guid.Parse("00000000-0000-0000-0000-000000000000")
                    Dim respuesta = New CRN.nspArea.Proceso_ActualizarArea() With {.entidad = editArea}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "área"), nspPopup.tipoPopup.Verde, True, "management/area/frmConsultaArea.aspx")
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
                    End Select
                Case "add"

                    Dim newArea As New area
                    newArea.id = Guid.NewGuid
                    newArea.nombre = txbNombreArea.Text
                    newArea.codigo = txbCodigoArea.Text
                    newArea.tipo = cmbTipoArea.SelectedValue
                    newArea.jerarquia = txbJerarquiaArea.Text
                    newArea.idUsuarioMovimiento = IdUsuario
                    newArea.ipUsuario = direccionIP
                    newArea.idSistema = sistemaActivo.idSistema
                    'newArea.idAreaPadre = Guid.Parse("00000000-0000-0000-0000-000000000000")
                    Dim respuesta = New CRN.nspArea.Proceso_AgregarArea() With {.entidad = newArea}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "área"), nspPopup.tipoPopup.Verde, True, "management/area/frmArea.aspx?band=add")
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
                    End Select
            End Select






        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Private Sub lnkGuardarSub_Click(sender As Object, e As EventArgs) Handles lnkGuardarSub.Click
        Try
            Dim resultadoValidacion = validarSubArea()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim accion = Request.QueryString("band").ToString
            Select Case accion
                Case "edt"
                    Dim id = Request.QueryString("id").ToString
                    Dim editArea = New CRN.nspArea.Proceso_ObtenerArea() With {.id = Guid.Parse(Request.QueryString("id"))}.Ejecutar
                    editArea.nombre = txbNombreSubArea.Text
                    editArea.codigo = txbCodigoSubArea.Text
                    editArea.tipo = cmbTipoAreaSub.SelectedValue

                    editArea.jerarquia = txbJerarquiaSub.Text
                    editArea.idUsuarioMovimiento = IdUsuario
                    editArea.ipUsuario = direccionIP
                    editArea.idSistema = sistemaActivo.idSistema
                    editArea.idAreaPadre = Guid.Parse(cmbAreaPadre.SelectedValue)
                    Dim respuesta = New CRN.nspArea.Proceso_ActualizarArea() With {.entidad = editArea}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "área"), nspPopup.tipoPopup.Verde, True, "management/area/frmConsultaArea.aspx")
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
                    End Select

                Case "add"
                    Dim newArea As New area
                    newArea.id = Guid.NewGuid
                    newArea.nombre = txbNombreSubArea.Text
                    newArea.codigo = txbCodigoSubArea.Text
                    newArea.tipo = cmbTipoAreaSub.SelectedValue
                    newArea.jerarquia = txbJerarquiaSub.Text
                    newArea.idUsuarioMovimiento = IdUsuario
                    newArea.ipUsuario = direccionIP
                    newArea.idSistema = sistemaActivo.idSistema
                    newArea.idAreaPadre = Guid.Parse(cmbAreaPadre.SelectedValue)
                    Dim respuesta = New CRN.nspArea.Proceso_AgregarArea() With {.entidad = newArea}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "área"), nspPopup.tipoPopup.Verde, True, "management/area/frmArea.aspx?band=add")
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                        Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
                    End Select
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Private Sub lnkCerrarSub_Click(sender As Object, e As EventArgs) Handles lnkCerrarSub.Click
        'If Not Request.QueryString("id") Is Nothing Then
        '    Response.Redirect("frmArea.aspx")
        'Else
        Response.Redirect("../default.aspx")
        'End If
    End Sub
    Private Sub lnkCerrarArea_Click(sender As Object, e As EventArgs) Handles lnkCerrarArea.Click
        'If Not Request.QueryString("id") Is Nothing Then
        '    Response.Redirect("frmArea.aspx")
        'Else
        Response.Redirect("../default.aspx")
        'End If
    End Sub


End Class
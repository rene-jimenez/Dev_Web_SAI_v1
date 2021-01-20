Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup, CRN.nspOficio, CES.nspOficio
Imports CRN.nspEstatusOficio, CES.nspEstatusOficio
Imports CRN.nspArea, CES.nspArea
Imports CRN.nspResponsable, CES.nspResponsable
Imports CRN.nspRubroRequerimiento, CES.nspRubroRequerimiento
Public Class frmEditarOficio : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim validacionCHK As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarArea()
            poblarResponsables()
            poblarRubro()
            poblarFormularioOficio()
        End If
    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim mensaje = validarCampos()
            If mensaje.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(mensaje.comentario)
            End If
            Dim msjchk = validarChka()
            If msjchk.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(msjchk.comentario)
            End If

            Dim idOficio As Guid = Guid.Parse(Request.QueryString("id").ToString)
            Dim OficioEdit = New Proceso_ObtenerUnOficio() With {.id = idOficio}.Ejecutar()
            OficioEdit.idArea = Guid.Parse(cmbArea.SelectedValue.ToString)
            OficioEdit.idCargoPresupuestal = Guid.Parse(cmbCargoPres.SelectedValue.ToString)
            OficioEdit.idResponsable = Guid.Parse(cmbResponsable.SelectedValue.ToString)
            If OficioEdit.esAtendido = True Then
                If chkConsolidar.Checked = False Then
                    OficioEdit.idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111113")
                    txbFechaAtendido.Text = ""
                End If
            End If
            If chkConsolidar.Checked = True Then
                OficioEdit.idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111117")
            End If

            OficioEdit.idRubro = Guid.Parse(cmbRubro.SelectedValue.ToString)
            OficioEdit.idUsuarioMovimiento = IdUsuario

            OficioEdit.asunto = txbAsunto.Text
            If txbObservaciones.Text.ToString <> "" Then
                OficioEdit.comentarios = txbObservaciones.Text
            End If

            If txbFechaAtendido.Text <> "" Then
                OficioEdit.fechaAtendido = CDate(txbFechaAtendido.Text)
            Else
                OficioEdit.fechaAtendido = Nothing
            End If
            OficioEdit.indicaciones = txbIndicaciones.Text
            OficioEdit.turnoDRM = txbTurnoDRM.Text + cmbTurnoDRM.SelectedValue
            OficioEdit.esAtendido = chkConsolidar.Checked
            OficioEdit.turnoSAF = txbTurnoSAF.Text
            'OficioEdit.esAtendido = CBool(chkConsolidar.Checked)
            OficioEdit.ipUsuario = direccionIP
            Dim respuestaProceso = New Proceso_EditarOficio() With {.entidad = OficioEdit}.Ejecutar()
            Select Case respuestaProceso.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "Oficio"), tipoPopup.Verde, True, "management/default.aspx")
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuestaProceso.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuestaProceso.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try

    End Sub
    Private Sub poblarFormularioOficio()
        Dim oficio = New Proceso_ObtenerUnOficio With {.id = Guid.Parse(Request.QueryString("id").ToString)}.Ejecutar()
        txbTurnoSAF.Text = oficio.turnoSAF
        txbTurnoDRM.Text = separarDRM(oficio.turnoDRM)
        cmbTurnoDRM.SelectedValue = Mid(oficio.turnoDRM, 7, 1)
        cmbArea.SelectedValue = oficio.idArea.ToString
        cmbCargoPres.SelectedValue = oficio.idCargoPresupuestal.ToString
        If oficio.esDocInterno = True Then
            lblDocInternoSi.Visible = True
            lblDocInternoNo.Visible = False
        Else
            lblDocInternoNo.Visible = True
            lblDocInternoSi.Visible = False
        End If
        lblFechaCap.Text = CDate(oficio.fechaCaptura).ToShortDateString.ToString
        cmbResponsable.SelectedValue = oficio.idResponsable.ToString
        cmbRubro.SelectedValue = oficio.idRubro.ToString
        txbAsunto.Text = oficio.asunto
        txbIndicaciones.Text = oficio.indicaciones
        txbObservaciones.Text = oficio.comentarios
        txbEstatusOficio.Text = obtenerEstatusOficio(oficio.idEstatusOficio.ToString)
        txbFechaAtendido.Text = oficio.fechaAtendido.ToString
        chkConsolidar.Checked = oficio.esAtendido
        If obtenerEstatusOficio(oficio.idEstatusOficio.ToString) = "Consolidado" Then
            chkConsolidar.Checked = True
        Else
            chkConsolidar.Checked = False
        End If

    End Sub

    Private Sub poblarArea()
        Dim listaAreas = New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar
        cmbArea.Items.Add("Seleccione un elemento de la lista")
        cmbArea.DataTextField = "nombre"
        cmbArea.DataValueField = "id"
        cmbArea.DataSource = listaAreas
        cmbArea.DataBind()
        cmbCargoPres.Items.Add("Seleccione un elemento de la lista")
        cmbCargoPres.DataTextField = "nombre"
        cmbCargoPres.DataValueField = "id"
        cmbCargoPres.DataSource = listaAreas
        cmbCargoPres.DataBind()
    End Sub
    Private Sub poblarResponsables()
        Dim listaResponsable = New Proceso_ObtenerResponsables() With {.tipoConsulta = tipoConsultaResponsable.esActivo, .esActivo = True}.Ejecutar
        cmbResponsable.Items.Add("Seleccione un elemento de la lista")
        cmbResponsable.DataTextField = "nombre"
        cmbResponsable.DataValueField = "id"
        cmbResponsable.DataSource = listaResponsable
        cmbResponsable.DataBind()
    End Sub
    Private Sub poblarRubro()
        Dim listaRubro = New Proceso_ObtenerRubroRequerimientos() With {.tipoConsulta = tipoConsultaRubroRequerimiento.esActivo, .esActivo = True}.Ejecutar
        cmbRubro.Items.Add("Seleccione un elemento de la lista")
        cmbRubro.DataTextField = "nombre"
        cmbRubro.DataValueField = "id"
        cmbRubro.DataSource = listaRubro
        cmbRubro.DataBind()
    End Sub
    Private Function separarDRM(DRM As String) As String
        Dim cadenaDRM As String = ""
        For i = 1 To 6
            cadenaDRM = cadenaDRM + Mid(DRM, i, 1)
        Next
        Return cadenaDRM
    End Function
    Private Function obtenerEstatusOficio(id As String) As String
        Dim estatusOficio = New Proceso_ObtenerEstatusOficio() With {.id = Guid.Parse(id)}.Ejecutar()
        Return estatusOficio.nombre
    End Function
    Private Function validarCampos()
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbTurnoDRM.Text.ToString = "" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "turnoDRM")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf cmbArea.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "área")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf cmbResponsable.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "responsable")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf cmbRubro.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "rubro")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf cmbCargoPres.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "cargo presupuestal")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf txbAsunto.Text.ToString = "" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "asunto")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf txbIndicaciones.Text.ToString = "" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "indicaciones")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf chkConsolidar.Checked Then
            If txbFechaAtendido.ToString.Length = 0 Then
                respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha atendido")
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End If
        End If
        Return respuesta
    End Function

    Private Function validarChka()
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If chkConsolidar.Checked = True Then
            Dim respuestaValisolicitudes = New CRN.nspValidarEstatusOficio.Proceso_ObtenerValidacionEstatusOficio() With {.accion = 1, .idOficio = Guid.Parse(Request.QueryString("id").ToString)}.Ejecutar()
            If respuestaValisolicitudes.bandera <> "No atender" Then
                Dim respuestaValiPedido = New CRN.nspValidarEstatusOficio.Proceso_ObtenerValidacionEstatusOficio() With {.accion = 2, .idOficio = Guid.Parse(Request.QueryString("id").ToString)}.Ejecutar()
                If respuestaValiPedido.bandera = "No atender" Then
                    respuesta.comentario = "No se puede completar la operación hay pedidos sin afectación presupuestalo"
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Else
                respuesta.comentario = "No se puede completar la operación hay solicitudes de gasto sin comprobar"
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End If
        End If
        Return respuesta
    End Function
    Private Sub btn_Cancelar_Click(sender As Object, e As EventArgs) Handles btn_Cancelar.Click
        mandaDefault()
    End Sub
End Class
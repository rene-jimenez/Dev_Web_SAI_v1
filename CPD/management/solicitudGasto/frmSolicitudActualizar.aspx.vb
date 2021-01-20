Imports CRN.nspSolicitudGasto
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Net.NetworkInformation
Public Class frmSolicitudActualizar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idOficio = Request.QueryString("idOficio")
                'se programa así pues se considera ques solo habra una solicitud activa para un idOficio
                Dim solicitud = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = CES.nspSolicitudGasto.tipoConsultaSolicitudGasto.idOficio, .idOficio = Guid.Parse(idOficio)}.Ejecutar().Where(Function(a) a.esCancelado = False)
                poblarSolicitud(solicitud(0).id)
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
        resetClases()
    End Sub


#Region "Métodos"
    Public Sub poblarSolicitud(id As Guid)
        Dim solicitud = New Proceso_ObtenerSolicitudGasto() With {.id = id}.Ejecutar()
        txbTurnoSAF.Text = solicitud._turnoSAF.ToString
        txbTurnoDRM.Text = solicitud._turnoDRM.ToString
        txbFechaElaboracion.Text = solicitud.fechaElaboracion.ToString
        txbArea.Text = solicitud._nombreArea.ToString
        txbCargoPres.Text = solicitud._nombreCargoPresupuestal.ToString
        txPartidaPres.Text = solicitud._nombrePartidaPresupuestal.ToString
        txbImporte.Text = solicitud.importe.ToString
        txbMarcaAgua.Text = solicitud.marcaAgua.ToString
        txbConcepto.Text = solicitud.concepto.ToString
        If Not solicitud.fechaRecepcion Is Nothing Then
            txbFechaRec.Text = FormatDateTime(solicitud.fechaRecepcion, DateFormat.ShortDate)
        End If

        If Not solicitud.folioCaja Is Nothing Then
            txbFolioCaja.Text = solicitud.folioCaja
        End If
        If Not solicitud.folioTesoreria Is Nothing Then
            txbFolioTesoreria.Text = solicitud.folioTesoreria
        End If

    End Sub

    Public Sub deshabilitarControles()
        txbFolioTesoreria.ReadOnly = True
        txbFolioCaja.ReadOnly = True
        txbFechaRec.ReadOnly = False
    End Sub

#End Region
#Region "Funciones"
    Private Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function
    Private Function validarActualizarSolicitud() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Dim mensaje As New notificacionesDeUsuario
        If txbFolioCaja.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            divFolioCa.Attributes.Add("class", "col-sm-4 animated bounce")
            Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "folio de caja"))
        End If

        If txbFolioTesoreria.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            divFolioTe.Attributes.Add("class", "col-sm-4 animated bounce")
            Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "folio de tesorería"))
        End If

        If txbFechaRec.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            divFechaRecepcion.Attributes.Add("class", "col-sm-4 animated bounce")
            Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha de recepción"))
        Else
            If CDate(txbFechaRec.Text) > Date.Now Then
                divFechaRecepcion.Attributes.Add("class", "col-sm-4 animated bounce")
                respuesta.comentario = "La fecha no debe ser menor o igual a hoy."
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End If
        End If


        Return respuesta
    End Function
    Private Function validarFolios() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Dim mensaje As New notificacionesDeUsuario
        Dim solicitudes = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = CES.nspSolicitudGasto.tipoConsultaSolicitudGasto.todos, .idSistema = sistemaActivo.id}.Ejecutar()
        'se programa así pues se considera ques solo habra una solicitud activa para un idOficio
        Dim idOficio = Request.QueryString("idOficio")
        Dim solicitud = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = CES.nspSolicitudGasto.tipoConsultaSolicitudGasto.idOficio, .idOficio = Guid.Parse(idOficio)}.Ejecutar().Where(Function(a) a.esCancelado = False)
        For i = 0 To solicitudes.Count - 1
            If solicitudes(i).id <> solicitud(0).id Then
                If solicitudes(i).folioCaja = txbFolioCaja.Text And solicitudes(i).folioTesoreria = txbFolioTesoreria.Text Then
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                    respuesta.comentario = "Existe otra solicitud con el mismo folio de caja y de tesorería"
                    Throw New Exception(respuesta.comentario)
                Else
                    If solicitudes(i).folioTesoreria = txbFolioTesoreria.Text Then
                        respuesta.comentario = "Existe otra solicitud con el mismo folio de tesorería"
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                        Throw New Exception(respuesta.comentario)
                    End If
                    If solicitudes(i).folioCaja = txbFolioCaja.Text Then
                        respuesta.comentario = "Existe otra solicitud con el mismo folio de caja"
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                        Throw New Exception(respuesta.comentario)
                    End If

                End If
            End If
        Next
        Return respuesta
    End Function
    Private Sub resetClases()
        divFolioCa.Attributes.Add("class", "col-sm-4")
        divFolioTe.Attributes.Add("class", "col-sm-4")
        divFechaRecepcion.Attributes.Add("class", "col-sm-4")
    End Sub
#End Region
#Region "Botones"
    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            Dim respuestaValidacion = validarActualizarSolicitud()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            Dim resultadoValidacionDos = validarFolios()
            If resultadoValidacionDos.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            Dim mc = getMacAddress()
            Dim idOficio = Request.QueryString("idOficio")
            Dim solicitud = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = CES.nspSolicitudGasto.tipoConsultaSolicitudGasto.idOficio, .idOficio = Guid.Parse(idOficio)}.Ejecutar().Where(Function(a) a.esCancelado = False)
            Dim idSolicitud = solicitud(0).id
            Dim respuesta = New Proceso_ActualizarSolicitudGasto() With {.id = idSolicitud,
                .folioCaja = txbFolioCaja.Text,
                .folioTesoreria = txbFolioTesoreria.Text,
                .fechaRecepcion = txbFechaRec.Text,
                .idUsuarioMovimiento = IdUsuario,
                .ipUsuario = mc}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    deshabilitarControles()
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "solicitud"), tipoPopup.Verde, True, "/management/solicitudGasto/reporteSolicitud/frmReporteSolicitud.aspx?idS=" + idSolicitud.ToString)
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub


    Private Sub txbFolioCaja_TextChanged(sender As Object, e As EventArgs) Handles txbFolioCaja.TextChanged
        If IsNumeric(txbFolioCaja.Text) = False Then
            divFolioCa.Attributes.Add("class", "col-sm-4 animated bounce")
            OnMostrarMensajeAccion("Crítico", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Formato_incorrecto_para_el_campo_X, "folio de Caja"), tipoPopup.Naranja, False, "")
            Exit Sub
        Else
            txbFolioCaja.Text = String.Format("{0:D6}", CInt(txbFolioCaja.Text))
        End If
    End Sub

    Private Sub txbFolioTesoreria_TextChanged(sender As Object, e As EventArgs) Handles txbFolioTesoreria.TextChanged
        If IsNumeric(txbFolioTesoreria.Text) = False Then
            divFolioTe.Attributes.Add("class", "col-sm-4 animated bounce")
            OnMostrarMensajeAccion("Crítico", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Formato_incorrecto_para_el_campo_X, "folio de Tesorería"), tipoPopup.Naranja, False, "")
            Exit Sub
        Else
            txbFolioTesoreria.Text = String.Format("{0:D6}", CInt(txbFolioTesoreria.Text))
        End If
    End Sub

#End Region
End Class
Imports CRN.nspAfectacionPresupuestal, CRN.nspOficio, CRN.nspPedido, CRN.nspAlcance
Imports CES, CES.nspAlcance
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmAlcanceActualizar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenardatos()
            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub


#Region "datos"
    Private Sub llenardatos()
        Dim idAlcance = Request.QueryString("idAlcance")
        Dim datos = New Proceso_ObtenerAlcance() With {.id = Guid.Parse(idAlcance.ToString)}.Ejecutar()
        lblDRM.Text = datos._turnoDrm
        lblSAF.Text = datos._turnoSaf
        'txtImporteAlcance.Text = datos.importe
        Dim area = New CRN.nspArea.Proceso_ObtenerArea() With {.id = Guid.Parse(datos._idArea.ToString)}.Ejecutar()
        lblNombreArea.Text = area.nombre.ToString
        lblArea.Text = area.codigo.ToString
        If Not datos._CargoPresupuestal Is Nothing Then
            lblCargo.Text = datos._CargoPresupuestal.ToString
        Else
            lblCargo.Text = "Sin cargo presupuestal"
        End If
        If Not datos._conceptoSolicitud Is Nothing Then
            lblConcepto.Text = datos._conceptoSolicitud.ToString
        Else
            lblConcepto.Text = "Sin concepto"
        End If

        If Not datos.folioCaja Is Nothing Then
            txbNuevoFolioCaja.Text = datos.folioCaja
        End If

        If Not datos.folioTesoreria Is Nothing Then
            txbNuevoFolioTesoreri.Text = datos.folioTesoreria
        End If

        If Not datos.fechaRecepcion Is Nothing Then
            txbFechaRecepcion.Text = datos.fechaRecepcion
        End If

        If Not datos._folioCajaSolicitud Is Nothing Then
            lblFolioCaja.Text = datos._folioCajaSolicitud.ToString
        Else
            lblFolioCaja.Text = "Sin folio de caja"
        End If
        If Not datos._folioTesoreriaSolicitud Is Nothing Then
            lblFolioTesoreria.Text = datos._folioTesoreriaSolicitud.ToString
        Else
            lblFolioTesoreria.Text = "Sin folio de tesorería"
        End If
        If Not datos._fechaCapturaSolicitud.ToString Is Nothing Then
            lblFechaCaptura.Text = CDate(datos._fechaCapturaSolicitud).ToString("D", CultureInfo.CreateSpecificCulture("es-MX"))
        Else
            lblFechaCaptura.Text = "Sin fecha captura"
        End If
        If Not datos._fechaRecepcionSolicitud.ToString Is Nothing Then
            lblFechaLiberacion.Text = CDate(datos._fechaRecepcionSolicitud).ToString("D", CultureInfo.CreateSpecificCulture("es-MX"))
        Else
            lblFechaLiberacion.Text = "Sin fecha recepción"
        End If

        Dim solicitud = New CRN.nspSolicitudGasto.Proceso_ObtenerSolicitudGasto() With {.id = Guid.Parse(datos.idSolicitud.ToString)}.Ejecutar()
        lblPartida.Text = solicitud._nombrePartidaPresupuestal.ToString
        Dim oficio = New CRN.nspOficio.Proceso_ObtenerUnOficio() With {.id = Guid.Parse(solicitud.idOficio.ToString)}.Ejecutar()
        If Not solicitud.concepto Is Nothing Then
            lblDescripcion.Text = solicitud.concepto.ToString
        Else
            lblDescripcion.Text = "Sin descripción"
        End If
        If Not solicitud.importe.ToString Is Nothing Then
            lblimporte.Text = solicitud.importe.ToString("C")
        Else
            lblimporte.Text = "Sin importe"
        End If


    End Sub
#End Region

#Region "validar"
    Private Function validar() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)

        If (txbNuevoFolioCaja.Text.Length() < 6) Then
            txbNuevoFolioCaja.Focus()
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "folio caja")
            Throw New Exception(respuesta.comentario)
        End If

        If (txbNuevoFolioTesoreri.Text.Length() < 6) Then
            txbNuevoFolioCaja.Focus()
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "folio tesorería")
            Throw New Exception(respuesta.comentario)
        End If
        If txbFechaRecepcion.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha recepción")
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function
    Private Function validarFolios() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Dim mensaje As New notificacionesDeUsuario
        Dim idOficio = Request.QueryString("idOficio")
        Dim alcance = New Proceso_ObtenerAlcances() With {.tipoConsulta = tipoConsultaAlcance.todos}.Ejecutar().Where(Function(a) a.idSistema = sistemaActivo.id)
        For i = 0 To alcance.Count - 1
            If alcance(i).id <> Guid.Parse(Request.QueryString("idAlcance")) Then
                If alcance(i).folioCaja = txbNuevoFolioCaja.Text And alcance(i).folioTesoreria = txbNuevoFolioTesoreri.Text Then
                    respuesta.comentario = "Existe otra solicitud con el mismo folio de caja y de tesorería"
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                    Throw New Exception(respuesta.comentario)
                Else
                    If alcance(i).folioTesoreria = txbNuevoFolioTesoreri.Text Then
                        respuesta.comentario = "Existe otra solicitud con el mismo folio de tesorería"
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                        Throw New Exception(respuesta.comentario)
                    End If
                    If alcance(i).folioCaja = txbNuevoFolioCaja.Text Then
                        respuesta.comentario = "Existe otra solicitud con el mismo folio de caja"
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                        Throw New Exception(respuesta.comentario)
                    End If
                End If
            End If
        Next
        Return respuesta
    End Function

#End Region

#Region "botones"
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            Dim idAlcance = Request.QueryString("idAlcance").ToString
            Dim resultadoValidacion = validar()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim resultadoValidacionDos = validarFolios()
            If resultadoValidacionDos.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(resultadoValidacionDos.comentario)
            End If
            Dim actualizalcance As New alcance
            actualizalcance.id = Guid.Parse(idAlcance)
            actualizalcance.folioCaja = txbNuevoFolioCaja.Text
            actualizalcance.folioTesoreria = txbNuevoFolioTesoreri.Text
            actualizalcance.fechaRecepcion = txbFechaRecepcion.Text
            actualizalcance.idSistema = sistemaActivo.idSistema
            actualizalcance.ipUsuario = direccionIP
            actualizalcance.idUsuarioMovimiento = IdUsuario
            actualizalcance._tipoEdicion = tipoEdicionAlcance.Actualizar
            Dim respuesta = New Proceso_ActualizarAlcance() With {.entidad = actualizalcance}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "Alcance"), nspPopup.tipoPopup.Verde, True, "management/alcance/reporteAlcance/frmReporteAlcance.aspx?idAlcance=" + idAlcance.ToString)
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try
    End Sub


#End Region
#Region "completar_textos"
    Private Sub txbNuevoFolioCaja_TextChanged(sender As Object, e As EventArgs) Handles txbNuevoFolioCaja.TextChanged
        If txbNuevoFolioCaja.Text = "" Then
            Exit Sub
        Else
            txbNuevoFolioCaja.Text = String.Format("{0:D6}", CInt(txbNuevoFolioCaja.Text))
            txbNuevoFolioCaja.Focus()
        End If
    End Sub

    Private Sub txbNuevoFolioTesoreri_TextChanged(sender As Object, e As EventArgs) Handles txbNuevoFolioTesoreri.TextChanged
        If txbNuevoFolioTesoreri.Text = "" Then
            Exit Sub
        Else
            txbNuevoFolioTesoreri.Text = String.Format("{0:D6}", CInt(txbNuevoFolioTesoreri.Text))
            txbNuevoFolioTesoreri.Focus()
        End If
    End Sub


#End Region

End Class
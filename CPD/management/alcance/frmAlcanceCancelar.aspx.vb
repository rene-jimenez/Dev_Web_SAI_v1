Imports CRN.nspAlcance
Imports CES, CES.nspAlcance
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmAlcanceCancelar : Inherits nspPaginaBase.PaginaBase
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
        Dim idAfectacion = Request.QueryString("idAlcance")
        Dim datos = New Proceso_ObtenerAlcance() With {.id = Guid.Parse(idAfectacion.ToString)}.Ejecutar()
        lblImporteAlcance.Text = datos.importe
        lblDRM.Text = datos._turnoDrm
        lblSAF.Text = datos._turnoSaf
        Dim area = New CRN.nspArea.Proceso_ObtenerArea() With {.id = Guid.Parse(datos._idArea.ToString)}.Ejecutar()
        lblNombreArea.Text = area.nombre.ToString
        lblArea.Text = area.codigo.ToString
        If Not datos._CargoPresupuestal Is Nothing Then
            lblCargo.Text = datos._CargoPresupuestal.ToString
        Else
            lblCargo.Text = "Sin cargo Presupuestal"
        End If
        If Not datos._conceptoSolicitud Is Nothing Then
            lblConcepto.Text = datos._conceptoSolicitud.ToString
        Else
            lblConcepto.Text = "Sin concepto"
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

        'If Not NombreUsuario.ToString Is Nothing Then
        lblResponsable.Text = NombreUsuario.ToString
        'Else
        '    lblResponsable.Text = "Sin responsable"
        'End If

    End Sub

#End Region
#Region "validar"
    Private Function validar() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)

        If txbObservaciones.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "observaciones")
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function

#End Region
#Region "botones"
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnCancelarAlcance_Click(sender As Object, e As EventArgs) Handles btnCancelarAlcance.Click
        Try
            Dim idAlcance = Request.QueryString("idAlcance").ToString
            Dim resultadoValidacion = validar()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim cancelalcance As New CES.nspAlcance.alcance
            cancelalcance.id = Guid.Parse(idAlcance)
            cancelalcance.responsableCancelacion = NombreUsuario
            cancelalcance.observacionCancelacion = txbObservaciones.Text
            cancelalcance.esCancelado = True
            cancelalcance.fechaCancelacion = Date.Now
            cancelalcance.idSistema = sistemaActivo.idSistema
            cancelalcance.ipUsuario = direccionIP
            cancelalcance.idUsuarioMovimiento = IdUsuario
            cancelalcance._tipoEdicion = tipoEdicionAlcance.Cancelar
            Dim respuesta = New CRN.nspAlcance.Proceso_CancelarAlcance() With {.entidad = cancelalcance}.Ejecutar()
            Select Case respuesta.respuesta
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "Alcance"), nspPopup.tipoPopup.Verde, True, "management/default.aspx")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try
    End Sub

#End Region
End Class
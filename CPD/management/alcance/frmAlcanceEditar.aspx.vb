Imports CRN.nspAfectacionPresupuestal, CRN.nspOficio, CRN.nspPedido, CRN.nspAlcance

Imports CES, CES.nspAlcance
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmAlcanceEditar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarcombos()
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
        txbImporte.Text = datos.importe
        cmbPartida.SelectedValue = datos.idPartida.ToString
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
    Private Sub llenarcombos()
        Dim partida = New CRN.nspPartidaPresupuestal.Proceso_ObtenerPartidasPresupuestales() With {.tipoConsulta = nspPartidaPresupuestal.tipoConsultaPartidaPresupuestal.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbPartida.DataTextField = "nombre"
        cmbPartida.DataValueField = "id"
        cmbPartida.DataSource = partida
        cmbPartida.DataBind()
    End Sub
#End Region

#Region "botones"
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

        Try
            Dim idAlcance = Request.QueryString("idAlcance").ToString
            Dim resultadoValidacion = validar()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim editalcance As New CES.nspAlcance.alcance
            editalcance.id = Guid.Parse(idAlcance)
            editalcance.idPartida = Guid.Parse(cmbPartida.SelectedValue)
            editalcance.importe = txbImporte.Text
            editalcance.idSistema = sistemaActivo.idSistema
            editalcance.ipUsuario = direccionIP
            editalcance.idUsuarioMovimiento = IdUsuario
            editalcance._tipoEdicion = tipoEdicionAlcance.editar
            Dim respuesta = New CRN.nspAlcance.Proceso_EditarAlcance() With {.entidad = editalcance}.Ejecutar()
            Select Case respuesta.respuesta
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "Alcance"), nspPopup.tipoPopup.Verde, True, "management/alcance/reporteAlcance/frmReporteAlcance.aspx?idAlcance=" + idAlcance.ToString)
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try

    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
#End Region

#Region "validar"
    Private Function validar() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbImporte.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "importe")
            Throw New Exception(respuesta.comentario)
        End If
        If cmbPartida.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "partida presupuestal")
            Throw New Exception(respuesta.comentario)
        End If

        Return respuesta
    End Function

    'Private Sub txbImporte_TextChanged(sender As Object, e As EventArgs) Handles txbImporte.TextChanged
    '    txbImporte.Text = Format(txbImporte.Text, "S/ #,##0.00")
    'End Sub

#End Region


End Class
Imports CRN.nspPartidaPresupuestal, CRN.nspAlcance, CRN.nspSolicitudGasto, CRN.nspArea
Imports CES.nspArea, CES.nspAlcance
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Net.NetworkInformation
Imports System.Globalization
Public Class frmAlcanceAgregar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idSolicitud = Request.QueryString("idSolicitud")
                llenarComboPartida()
                poblarCampos()

            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "Métodos"
    Public Sub poblarCampos()
        Dim idSolicitud = Request.QueryString("idSolicitud")
        Dim solicitud = New Proceso_ObtenerSolicitudGasto() With {.id = Guid.Parse(idSolicitud)}.Ejecutar()
        lblDRM.Text = solicitud._turnoDRM.ToString
        lblSAF.Text = solicitud._turnoSAF.ToString
        lblCantidad.Text = "$ " + FormatNumber(solicitud.importe, 2)
        Dim area = New Proceso_ObtenerArea() With {.id = solicitud._idArea}.Ejecutar()
        lblNombreArea.Text = area.nombre.ToString
        lblArea.Text = area.codigo.ToString
        Dim cargoPres = New Proceso_ObtenerArea() With {.id = solicitud._idCargoPresupuestal}.Ejecutar()
        lblCargo.Text = cargoPres.nombre.ToString
        lblConcepto.Text = solicitud.concepto.ToString
        If solicitud.folioCaja <> "" Then
            lblFolioCaja.Text = solicitud.folioCaja.ToString
        Else
            lblFolioCaja.Text = "Sin folio de caja"
        End If
        If solicitud.fechaRecepcion.ToString <> "" Then
            lblFechaLiberacion.Text = CDate(solicitud.fechaRecepcion).ToString("D",
                        CultureInfo.CreateSpecificCulture("es-MX"))
        Else
            lblFechaLiberacion.Text = "Sin Fecha de recepción"
        End If

        lblFechaCaptura.Text = CDate(solicitud.fechaElaboracion).ToString("D", CultureInfo.CreateSpecificCulture("es-MX"))
        If solicitud.folioTesoreria <> "" Then
            lblFolioTesoreria.Text = solicitud.folioTesoreria.ToString
        Else
            lblFolioTesoreria.Text = "Sin folio de Tesorería"
        End If
        cmbPartida.SelectedValue = solicitud.idPartidaPresupuestal.ToString

        'Datos partida
        Dim partidaDetalles = New Proceso_ObtenerPartidaPresupuestal() With {.id = solicitud.idPartidaPresupuestal}.Ejecutar()
        lblPartida.Text = partidaDetalles.numero.ToString
        lblDescripcion.Text = partidaDetalles.nombre.ToString
    End Sub
    Public Sub llenarComboPartida()
        Dim ListaPartidaPresup = New Proceso_ObtenerPartidasPresupuestales() With {.tipoConsulta = CES.nspPartidaPresupuestal.tipoConsultaPartidaPresupuestal.todos}.Ejecutar()
        cmbPartida.DataValueField = "id"
        cmbPartida.DataTextField = "nombre"
        cmbPartida.Items.Add("Seleccione un elemento de la lista")
        cmbPartida.DataSource = ListaPartidaPresup.OrderBy(Function(a) a.nombre).ToList
        cmbPartida.DataBind()
        cmbPartida.SelectedValue = "Seleccione un elemento de la lista"
    End Sub

#End Region
#Region "Funciones"
    Private Function validarAgregarUsuario() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If txbImporte.Text.Length = 0 Then
                divImporte.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "importe"))
            End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function

    Private Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function
    Public Sub deshabilitarControles()
        cmbPartida.Enabled = False
        txbImporte.ReadOnly = True
    End Sub
#End Region
#Region "Botones"
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim respuestaValidacion = validarAgregarUsuario()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            Dim mc = getMacAddress()
            Dim idSolicitud = Request.QueryString("idSolicitud")
            Dim entidad = New CES.nspAlcance.alcance
            entidad.id = Guid.NewGuid()
            entidad.idPartida = Guid.Parse(cmbPartida.SelectedValue)
            entidad.idSolicitud = Guid.Parse(idSolicitud)
            entidad.importe = txbImporte.Text.ToString
            entidad.no = "1"   'este se agregará siempre como 1 mientras no se necesite ser recursivo
            entidad.idUsuarioMovimiento = IdUsuario
            entidad.ipUsuario = mc
            entidad.idSistema = sistemaActivo.id
            Dim respuesta = New Proceso_AgregarAlcance() With {.entidad = entidad}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    deshabilitarControles()
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "alcance"), tipoPopup.Verde, True, "/management/alcance/reporteAlcance/frmReporteAlcance.aspx?idAlcance=" + entidad.id.ToString)
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Private Sub txbImporte_TextChanged(sender As Object, e As EventArgs) Handles txbImporte.TextChanged
        If IsNumeric(txbImporte.Text) = False Then
            divImporte.Attributes.Add("class", "col-sm-6 animated bounce")
            OnMostrarMensajeAccion("Atención", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Formato_incorrecto_para_el_campo_X, "Importe"), tipoPopup.Naranja, False, "")
            Exit Sub
        End If
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub



#End Region



End Class
Imports CRN.nspOficio, CRN.nspArea, CRN.nspSolicitudGasto, CRN.nspPartidaPresupuestal
Imports CES.nspOficio, CES.nspArea, CES.nspSolicitudGasto, CES.nspPartidaPresupuestal
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Net.NetworkInformation
Imports System.Globalization
Imports System.Threading
Imports System.Text.RegularExpressions
Public Class frmSolicitudAgregar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idOficio = Request.QueryString("idOficio")
                cargarDatosOficio(Guid.Parse(idOficio))
                poblarPartidaPresupuestal()
                txbFechaElaboracion.Text = Date.Now
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
        resetClases()
    End Sub


#Region "Métodos"
    Public Sub cargarDatosOficio(id As Guid)
        Dim oficio = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.id, .id = id}.Ejecutar()

        If Not oficio(0).turnoSAF Is Nothing Then
            txbTurnoSAF.Text = oficio(0).turnoSAF.ToString
        End If
        txbTurnoDRM.Text = oficio(0).turnoDRM.ToString
        poblarArea()
        cmbArea.SelectedValue = oficio(0).idArea.ToString
        poblarCargoPresupuestal()
        cmbCargoPres.SelectedValue = oficio(0).idCargoPresupuestal.ToString
    End Sub

    Public Sub poblarArea()
        Dim consultarArea = New CRN.nspArea.Proceso_ObtenerAreas() With {.tipoConsulta = CES.nspArea.tipoConsultaArea.todos}.Ejecutar()
        cmbArea.DataValueField = "id"
        cmbArea.DataTextField = "nombre"
        cmbArea.Items.Add("Seleccione un elemento de la lista")
        cmbArea.DataSource = consultarArea.OrderBy(Function(a) a.nombre).ToList
        cmbArea.DataBind()
        cmbArea.SelectedValue = "Seleccione un elemento de la lista"
    End Sub

    Public Sub poblarCargoPresupuestal()
        Dim consultarArea = New CRN.nspArea.Proceso_ObtenerAreas() With {.tipoConsulta = CES.nspArea.tipoConsultaArea.todos}.Ejecutar()
        cmbCargoPres.DataValueField = "id"
        cmbCargoPres.DataTextField = "nombre"
        cmbCargoPres.Items.Add("Seleccione un elemento de la lista")
        cmbCargoPres.DataSource = consultarArea.OrderBy(Function(a) a.nombre).ToList
        cmbCargoPres.DataBind()
        cmbCargoPres.SelectedValue = "Seleccione un elemento de la lista"
    End Sub

    Public Sub poblarPartidaPresupuestal()
        Dim cargaPresup = New Proceso_ObtenerPartidasPresupuestales() With {.tipoConsulta = CES.nspPartidaPresupuestal.tipoConsultaPartidaPresupuestal.esActivo, .esActivo = True}.Ejecutar()
        cmbPartidaPres.DataValueField = "id"
        cmbPartidaPres.DataTextField = "nombre"
        cmbPartidaPres.Items.Add("Seleccione un elemento de la lista")
        cmbPartidaPres.DataSource = cargaPresup.OrderBy(Function(a) a.nombre).ToList
        cmbPartidaPres.DataBind()
        cmbPartidaPres.SelectedValue = "Seleccione un elemento de la lista"
    End Sub

    Public Sub deshabilitarControles()
        cmbArea.Enabled = False
        cmbCargoPres.Enabled = False
        cmbPartidaPres.Enabled = False
        txbConcepto.ReadOnly = True
        txbImporte.ReadOnly = True
        txbMarcaAgua.ReadOnly = True
    End Sub
    Private Sub resetClases()

        divArea.Attributes.Add("class", "col-sm-6")
        divCargoPres.Attributes.Add("class", "col-sm-6")
        divPartidaPre.Attributes.Add("class", "col-sm-6")
        divImporte.Attributes.Add("class", "col-sm-6")
        divMarcaAgua.Attributes.Add("class", "col-sm-6")
        divConcepto.Attributes.Add("class", "col-sm-6")
    End Sub
#End Region
#Region "Funciones"
    Private Function validarAgregarSolicitud() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If Not cmbArea.SelectedValue <> "Seleccione un elemento de la lista" Then
                divArea.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "área"))
            End If
            If Not cmbCargoPres.SelectedValue <> "Seleccione un elemento de la lista" Then
                divCargoPres.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "cargo presupuestal"))
            End If
            If Not cmbPartidaPres.SelectedValue <> "Seleccione un elemento de la lista" Then
                divPartidaPre.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "partida presupuestal"))
            End If
            If txbImporte.Text.Length = 0 Then
                divImporte.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "importe"))
            End If
            If txbMarcaAgua.Text.Length = 0 Then
                divMarcaAgua.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "marca de agua"))
            End If
            If txbConcepto.Text.Length = 0 Then
                divConcepto.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "concepto"))
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
#End Region
#Region "Botones"
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim respuestaValidacion = validarAgregarSolicitud()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            Dim mc = getMacAddress()
            Dim idOficio = Request.QueryString("idOficio")
            Dim respuesta = New CRN.nspSolicitudGasto.Proceso_AgregarSolicitudGasto() With
            {.idOficio = Guid.Parse(idOficio),
            .idPartidaPresupuestal = Guid.Parse(cmbPartidaPres.SelectedValue.ToString),
            .importe = txbImporte.Text.ToString,
            .concepto = txbConcepto.Text.ToString,
            .marcaAgua = txbMarcaAgua.Text.ToString,
            .idSistema = sistemaActivo.id,
            .idUsuarioMovimiento = IdUsuario,
            .ipUsuario = mc,
            .idArea = Guid.Parse(cmbArea.SelectedValue.ToString),
            .idCargoPresupuestal = Guid.Parse(cmbCargoPres.SelectedValue.ToString),
            .idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111115")}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    deshabilitarControles()
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "solicitud"), tipoPopup.Verde, True, "/management/solicitudGasto/reporteSolicitud/frmReporteSolicitud.aspx?idOficio=" + idOficio.ToString)
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub txbImporte_TextChanged(sender As Object, e As EventArgs) Handles txbImporte.TextChanged

        If IsNumeric(txbImporte.Text) = False Then
            divImporte.Attributes.Add("class", "col-sm-6 animated bounce")
            OnMostrarMensajeAccion("Atención", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Formato_incorrecto_para_el_campo_X, "Importe"), tipoPopup.Naranja, False, "")
            Exit Sub
        End If
    End Sub


#End Region
End Class
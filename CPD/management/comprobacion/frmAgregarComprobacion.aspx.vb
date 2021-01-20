Imports CES, CES.nspImporteComprobacion, CES.nspPedido, CES.nspComprobacion
Imports CRN, CRN.nspImporteComprobacion, CRN.nspFirma, CRN.nspPedido, CRN.nspComprobacion
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup

Public Class frmAgregarComprobacion : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idOficio = Request.QueryString("idOficio")
                poblarCampos(Guid.Parse(idOficio))
                poblarAutorizados()
                poblarResponsable()
            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "Metodos"
    Public Sub poblarCampos(id As Guid)
        Dim consulta = New Proceso_ObtenerImporteComprobacion() With {.tipoConsulta = tipoConsultaImporteComprobacion.idOficio, .idOficio = id}.Ejecutar()
        lblNumeroOficio.Text = consulta(0).turnoDrm.ToString
        If Not consulta(0).turnoSaf Is Nothing Then
            lblSAF.Text = consulta(0).turnoSaf.ToString
        Else
            lblSAF.Text = "Sin SAF"

        End If

        lblFolioTesoreriaSolicitud.Text = consulta(0).folioTesoreriaSolicitud.ToString
        lblFolioCajaSolicitud.Text = consulta(0).folioCajaSolicitud.ToString
        If Not consulta(0).folioTesoreriaAlcance Is Nothing Then
            lblFolioTesoreriaAlcance.Text = consulta(0).folioTesoreriaAlcance.ToString
        Else
            lblFolioTesoreriaAlcance.Text = "Sin alcance"
        End If

        If Not consulta(0).folioCajaAlcance Is Nothing Then
            lblFolioCajaAlcance.Text = consulta(0).folioCajaAlcance.ToString
        Else
            lblFolioCajaAlcance.Text = "Sin alcance"
        End If

        lblImporte.Text = consulta(0).importeTotalPedido.ToString
        lblEjercido.Text = consulta(0).importeTotalSolicitado.ToString
        lblDevolucion.Text = consulta(0).importeDevolucion.ToString
        lblCargoPresupuestal.Text = consulta(0).CargoPresupuestal.ToString
        Dim numeroDevolucion As Double = Convert.ToDouble(lblDevolucion.Text)

        If numeroDevolucion < 0 Then
            deshabilitar()
        End If
        Dim pedidos = New Proceso_ObtenerPedidos() With {.tipoConsulta = tipoConsultaPedido.idOficio, .idOficio = id}.Ejecutar().Where(Function(a) a.estatusPedido = True And a.idTipoPago = Guid.Parse("71747111-2222-3333-4444-111111111112")).ToList.OrderBy(Function(a) a.numeroPedido)
        If pedidos.Count > 0 Then
            lblPedido1.Text = pedidos(0).numeroPedido.ToString
            Dim pedidosExtras As String = lblPedido1.Text
            If pedidos.Count > 1 Then
                For i = 1 To pedidos.Count - 1
                    pedidosExtras = pedidosExtras + " , " + pedidos(i).numeroPedido.ToString
                Next
                lblPedido1.Text = pedidosExtras.TrimEnd(",")
            End If
        Else
            lblPedido1.Text = "Sin pedido"
        End If

    End Sub
    Public Sub poblarAutorizados()
        Dim listaAutoriza = New Proceso_ObtenerFirmas() With {.tipoConsulta = nspFirma.tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Autoriza", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(ew) ew._nombreUsuario).ToList
        cmbAutoriza.DataSource = listaAutoriza
        cmbAutoriza.DataValueField = "id"
        cmbAutoriza.DataTextField = "_nombreUsuario"
        cmbAutoriza.Items.Add("Seleccione un elemento de la lista")
        cmbAutoriza.DataSource = listaAutoriza.OrderBy(Function(a) a.nombre).ToList
        cmbAutoriza.DataBind()
        cmbAutoriza.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Public Sub poblarResponsable()
        Dim listaResponsables = New Proceso_ObtenerFirmas() With {.tipoConsulta = nspFirma.tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Responsable", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(ew) ew._nombreUsuario).ToList
        cmbResponsable.DataSource = listaResponsables
        cmbResponsable.DataValueField = "id"
        cmbResponsable.DataTextField = "_nombreUsuario"
        cmbResponsable.Items.Add("Seleccione un elemento de la lista")
        cmbResponsable.DataSource = listaResponsables.OrderBy(Function(a) a.nombre).ToList
        cmbResponsable.DataBind()
        cmbResponsable.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Private Sub deshabilitar()
        txbConcepto.ReadOnly = True
        txbMarcaAgua.ReadOnly = True
        cmbAutoriza.Enabled = False
        cmbResponsable.Enabled = False
    End Sub
#End Region
#Region "Funciones"
    Private Function validarAgregarSolicitud() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If Not cmbAutoriza.SelectedValue <> "Seleccione un elemento de la lista" Then
                divAutoriza.Attributes.Add("class", "animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "autoriza"))
            End If
            If txbConcepto.Text.Length = 0 Then
                divConcepto.Attributes.Add("class", "animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "concepto"))
            End If
            If txbMarcaAgua.Text.Length = 0 Then
                divMarcaAgua.Attributes.Add("class", "animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "marca de Agua"))
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

    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            If txbMarcaAgua.ReadOnly = True Then
                OnMostrarMensajeAccion("Atención", "No se puede agregar una comprobación ya que el importe ejercido es mayor al solicitado", tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            Dim respuestaValidacion = validarAgregarSolicitud()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            Dim mc = getMacAddress()
            Dim idOficio = Request.QueryString("idOficio")
            Dim comprobacion = New comprobacion
            comprobacion.id = Guid.NewGuid()
            comprobacion.idOficio = Guid.Parse(idOficio.ToString)
            comprobacion.idResponsable = Guid.Parse(cmbResponsable.SelectedValue)
            comprobacion.fechaElaboracion = Date.Now
            comprobacion.concepto = txbConcepto.Text.ToString
            comprobacion.idAutoriza = Guid.Parse(cmbAutoriza.SelectedValue)
            comprobacion.devolucion = False
            comprobacion.marcaAgua = txbMarcaAgua.Text.ToString
            comprobacion.idSistema = sisActivo.sistemaActivo.idSistema
            comprobacion.ipUsuario = getMacAddress()
            comprobacion.idUsuarioMovimiento = IdUsuario
            Dim respuesta = New Proceso_AgregarComprobacion() With {.entidad = comprobacion}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "comprobación"), tipoPopup.Verde, True, "management/comprobacion/reporteComprobacion/frmReporteComprobacion.aspx?idComp=" + comprobacion.id.ToString)
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub




#End Region


End Class
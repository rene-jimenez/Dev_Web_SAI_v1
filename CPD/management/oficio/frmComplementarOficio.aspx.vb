Imports CRN.nspOficio, CRN.nspResponsable, CRN.nspRubroRequerimiento
Imports CES
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Public Class frmComplementarOficio : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                limpiarControles()
                llenarListados()
            Catch ex As Exception
            End Try
        End If
    End Sub
#Region "Funciones"
    Private Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function
    Private Function ValidarControles() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If (cmbResponsable.SelectedValue = "Seleccione un elemento de la lista") Then
                SetFocus(cmbResponsable)
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Responsable"))
            End If
            If (cmbRubro.SelectedValue = "Seleccione un elemento de la lista") Then
                SetFocus(cmbRubro)
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Rubro"))
            End If
            If (txbIndicaciones.Text.Trim() = String.Empty) Then
                SetFocus(txbIndicaciones)
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Indicaciones"))
            End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
#End Region
#Region "Metodos"
    Private Sub complementarOficio()
        Try
            Dim respuestaValidacion = ValidarControles()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If
            Dim idx As String = Request.QueryString("id")
            Dim mc = getMacAddress()
            Dim compi As New Proceso_ComplementarOficio With {
            .idOficio = Guid.Parse(idx),
            .idResponsable = Guid.Parse(cmbResponsable.SelectedValue.ToString),
            .idRubro = Guid.Parse(cmbRubro.SelectedValue.ToString),
            .indicaciones = txbIndicaciones.Text.Trim(),
            .ipUsuario = mc,
            .idUsuarioMovimiento = IdUsuario
            }
            Dim respuesta As respuestaDelProceso = compi.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Oficio"), nspPopup.tipoPopup.Verde, True, "management/default.aspx")
                    limpiarControles()
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Protected Sub limpiarControles()
        txbIndicaciones.Text = String.Empty
        cmbResponsable.ClearSelection()
        cmbRubro.ClearSelection()

    End Sub
    Protected Sub llenarListados()

        Dim listaResponsable = New Proceso_ObtenerResponsables() With {.tipoConsulta = nspResponsable.tipoConsultaResponsable.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(n) n.nombre).ToList
        cmbResponsable.DataSource = listaResponsable
        cmbResponsable.DataTextField = "nombre"
        cmbResponsable.DataValueField = "id"
        cmbResponsable.DataBind()

        Dim listaRubro = New Proceso_ObtenerRubroRequerimientos() With {.tipoConsulta = nspRubroRequerimiento.tipoConsultaRubroRequerimiento.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(n) n.nombre).ToList
        cmbRubro.DataSource = listaRubro
        cmbRubro.DataTextField = "nombre"
        cmbRubro.DataValueField = "id"
        cmbRubro.DataBind()

        Dim idx As String = Request.QueryString("id")
        Dim llenarCajas = New Proceso_ObtenerUnOficio() With {.id = Guid.Parse(idx)}.Ejecutar()
        txbAsunto.Text = llenarCajas.asunto
        txbTurnoDRM.Text = llenarCajas.turnoDRM
        txbTurnoSAF.Text = llenarCajas.turnoSAF
        txbArea.Text = llenarCajas._area
    End Sub
#End Region
#Region "Botones"
    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs)
        limpiarControles()
        mandaDefault()
    End Sub
    Protected Sub btnComplementar_Click(sender As Object, e As EventArgs)
        complementarOficio()
    End Sub
#End Region
End Class
Imports CRN.nspArea, CRN.nspOficio
Imports CES
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes

Public Class frmAltaOficio : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                limpiarControles()
                llenarListado()
            Catch ex As Exception

            End Try
        End If

    End Sub
#Region "Metodos"


    Private Sub txbTurnoSAF_TextChanged(sender As Object, e As EventArgs) Handles txbTurnoSAF.TextChanged
        If txbTurnoSAF.Text = "" Then
            Exit Sub
        Else
            txbTurnoSAF.Text = String.Format("{0:D6}", CInt(txbTurnoSAF.Text))
            txbTurnoDRM.Focus()
        End If
    End Sub

    Private Sub txbTurnoDRM_TextChanged(sender As Object, e As EventArgs) Handles txbTurnoDRM.TextChanged
        If txbTurnoDRM.Text = "" Then
            Exit Sub
        Else
            txbTurnoDRM.Text = String.Format("{0:D6}", CInt(txbTurnoDRM.Text))
            cmbTurnoDRM.Focus()
        End If
    End Sub
    Protected Sub llenarListado()
        Dim listaArea = New Proceso_ObtenerAreas() With {.tipoConsulta = nspArea.tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(n) n.nombre).ToList
        cmbArea.DataSource = listaArea
        cmbArea.DataTextField = "nombre"
        cmbArea.DataValueField = "id"
        cmbArea.DataBind()
    End Sub
    Protected Sub limpiarControles()
        txbAsunto.Text = String.Empty
        txbTurnoDRM.Text = String.Empty
        txbTurnoSAF.Text = String.Empty
        cmbArea.ClearSelection()
        cmbTurnoDRM.ClearSelection()
    End Sub
    Protected Sub guardarNuevoOficio()
        Try
            Dim respuestaValidacion = ValidarControles()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If

            Dim mc = getMacAddress()

            Dim ente As New Proceso_AgregarOficio With {
            .asunto = txbAsunto.Text.Trim(),
            .turnoSAF = txbTurnoSAF.Text.Trim(),
            .turnoDRM = txbTurnoDRM.Text.Trim() + cmbTurnoDRM.Text.Trim(),
            .idArea = Guid.Parse(cmbArea.SelectedValue.ToString),
            .ipUsuario = mc,
            .idUsuarioMovimiento = IdUsuario,
            .idSistema = sisActivo.sistemaActivo.id
            }
            Dim respuesta As respuestaDelProceso = ente.Ejecutar()
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

#End Region
#Region "Funciones"

    Private Function ValidarControles() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If (txbTurnoDRM.Text.Trim() = "") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "turno DRM"))
            End If
            If (cmbArea.SelectedValue = "Seleccione un elemento de la lista") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "área"))
            End If
            If (txbAsunto.Text.Trim() = "") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "asunto"))
            End If

        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function

    '    Private Function tiene_numeros(texto)

    '        For (i = 0; i<texto.length; i++)

    '      If (numeros.indexOf(texto.charAt(i), 0)!= -1) Then
    '                Return 1;
    '    End If
    '            Return 0;
    'End Function
    Private Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function
#End Region
#Region "Botones"
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        guardarNuevoOficio()

    End Sub

    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs)
        limpiarControles()
        mandaDefault()
    End Sub
#End Region

End Class
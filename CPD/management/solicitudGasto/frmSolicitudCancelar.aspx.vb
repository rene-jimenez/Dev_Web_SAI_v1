Imports CRN.nspSolicitudGasto
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Net.NetworkInformation
Public Class frmSolicitudCancelar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idOficio = Request.QueryString("idOficio")
                Dim solicitud = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = CES.nspSolicitudGasto.tipoConsultaSolicitudGasto.idOficio, .idOficio = Guid.Parse(idOficio)}.Ejecutar().Where(Function(a) a.esCancelado = False)
                idlbl.Value = solicitud(0).id.ToString
                poblarSolicitud(solicitud(0).id)
            Catch ex As Exception
                mandaDefault()
            End Try
        End If
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
    End Sub



#End Region
#Region "Funciones"
    Private Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function
    Private Function validarAgregarUsuario() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Dim mensaje As New notificacionesDeUsuario
        If txbObservaciones.Text.Length = 0 Then
            respuesta.comentario = "Las observaciones de la cancelación es un campo obligatorio."
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        End If
        Return respuesta
    End Function
#End Region

#Region "Botones"
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            Dim respuestaValidacion = validarAgregarUsuario()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                Exit Sub
                'Throw New Exception(respuestaValidacion.comentario)
            End If
            Dim mc = getMacAddress()
            Dim idOficio = Request.QueryString("idOficio")
            Dim idSolicitud = Guid.Parse(idlbl.Value)
            Dim solicitud = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = CES.nspSolicitudGasto.tipoConsultaSolicitudGasto.idOficio, .idOficio = Guid.Parse(idOficio)}.Ejecutar()
            Dim responsableCancelacion = New cadenero.CRN.nspUsuario.Proceso_ObtenerUsuario() With {.id = IdUsuario}.Ejecutar()
            Dim respuesta = New CRN.nspSolicitudGasto.Proceso_CancelarSolicitudGasto() With {.id = idSolicitud,
                .esCancelado = True,
                .FechaCancelacion = Date.Now,
                .responsableCancelacion = responsableCancelacion.nombre.ToString,
                .observacionCancelacion = txbObservaciones.Text.ToString,
                .idUsuarioMovimiento = IdUsuario,
                .ipUsuario = mc}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", "Tu solicitud se canceló correctamente", tipoPopup.Verde, True, "/management/solicitudGasto/reporteSolicitud/frmReporteSolicitud.aspx?idS=" + idSolicitud.ToString + "&ca=1")
                    txbObservaciones.ReadOnly = True
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





#End Region
End Class
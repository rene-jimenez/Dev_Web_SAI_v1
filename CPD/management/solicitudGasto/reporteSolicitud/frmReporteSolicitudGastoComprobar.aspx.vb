Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Imports CES.nspSolicitudGastoComprobacion
Public Class frmReporteSolicitudGastoComprobar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            resultadoVacioCategoria.Visible = False
            If Request.QueryString("band") = "comp" Then
                lblTitulo.Text = "Solicitudes de gasto - Liberados y comprobados"
            Else
                lblTitulo.Text = "Solicitudes de gasto - Liberados pendientes de comprobar"
            End If
        End If
    End Sub

#Region "funciones"
    Private Function validarCampos()

        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If txbFechaInicial.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Fecha inicial"))
            End If
            If txbFechaFinal.Text.Length = 0 Then
                txbFechaFinal.Text = txbFechaInicial.Text
            End If
            If CDate(txbFechaInicial.Text) > Date.Now Then
                Throw New Exception("La fecha inicial no debe ser mayor a la fecha actual")
                txbFechaInicial.Text = String.Empty
                txbFechaInicial.Focus()
            End If
            If CDate(txbFechaInicial.Text) > CDate(txbFechaFinal.Text) Then
                Throw New Exception("La fecha final no debe ser menor a la fecha inicial")
                txbFechaFinal.Text = String.Empty
                txbFechaFinal.Focus()
            End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
#End Region
#Region "btn"
    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        Try
            resultadoVacioCategoria.Visible = False
            Dim respuestaValidacion = validarCampos()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If
            Dim op As Integer
            If Request.QueryString("band") = "comp" Then
                op = 1
            Else
                op = 0
            End If

            Dim consultaSol = New CRN.nspSolicitudGastoComprobacion.Proceso_ObtenerSolicitudGastoComprobacion() With {.accion = op, .fechaInicial = txbFechaInicial.Text, .fechaFinal = DateAdd(DateInterval.Day, 1, CDate(txbFechaFinal.Text)), .idSistema = sistemaActivo.id}.Ejecutar()
            rptSolicGastoComp.LocalReport.DataSources.Clear()
            rptSolicGastoComp.LocalReport.Refresh()
            Dim fechas As New List(Of ReportParameter)
            fechas.Add(New ReportParameter("fechaInicial", UCase(CDate(txbFechaInicial.Text).ToString("D"))))
            fechas.Add(New ReportParameter("fechaFinal", UCase(CDate(txbFechaFinal.Text).ToString("D"))))
            fechas.Add(New ReportParameter("nomUsuario", UCase(NombreUsuario)))
            fechas.Add(New ReportParameter("sistema", UCase(sistemaActivo.nombre)))
            fechas.Add(New ReportParameter("anio", sistemaActivo.año))
            Dim dataSource As ReportDataSource = New ReportDataSource()
            Dim dtsSolicitudGastoLiberado As New dtsSolicitudGastoLiberado
            If consultaSol.Count > 0 Then
                For i = 0 To consultaSol.Count - 1
                    Dim turnoSAF As String = consultaSol(i).turnoSAF
                    Dim turnoDRM As String = consultaSol(i).turnoDRM
                    Dim folioTes As String = consultaSol(i).folioTes
                    Dim folioCaja As String = consultaSol(i).folioCaja
                    Dim importe As String = consultaSol(i).importe.ToString("C")
                    Dim concepto As String = UCase(consultaSol(i).concepto)
                    Dim fechaLib As String = consultaSol(i).fechadeLiberacion
                    Dim fechaCap As String = CDate(consultaSol(i).fechadeCaptura).ToString("dd/MM/yyyy")
                    Dim dias As String = consultaSol(i).diasTranscurridos
                    Dim no As String = i + 1
                    dtsSolicitudGastoLiberado.tblSolGastoLiberado.AddtblSolGastoLiberadoRow(turnoSAF, turnoDRM, folioTes, folioCaja, importe, concepto, fechaLib, fechaCap, dias, no)
                Next

                If Request.QueryString("band") = "comp" Then
                    dataSource = New ReportDataSource("dtsSolicitudGastoLiberado", dtsSolicitudGastoLiberado.tblSolGastoLiberado.DefaultView)
                    Me.rptSolicGastoComp.ProcessingMode = ProcessingMode.Local
                    rptSolicGastoComp.LocalReport.ReportPath = "management/solicitudGasto/reporteSolicitud/rpt/rptSolicidudGastoLiberadosYcomprobados.rdlc"
                    rptSolicGastoComp.LocalReport.SetParameters(fechas)
                    rptSolicGastoComp.LocalReport.DataSources.Add(dataSource)
                    rptSolicGastoComp.LocalReport.Refresh()
                Else
                    dataSource = New ReportDataSource("dtsSolicitudGastoLiberado", dtsSolicitudGastoLiberado.tblSolGastoLiberado.DefaultView)
                    Me.rptSolicGastoComp.ProcessingMode = ProcessingMode.Local
                    rptSolicGastoComp.LocalReport.ReportPath = "management/solicitudGasto/reporteSolicitud/rpt/rptSolicitudGastoLiberadoNoComprobados.rdlc"
                    rptSolicGastoComp.LocalReport.SetParameters(fechas)
                    rptSolicGastoComp.LocalReport.DataSources.Add(dataSource)
                    rptSolicGastoComp.LocalReport.Refresh()
                End If
            Else
                resultadoVacioCategoria.Visible = True
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
#End Region
#Region "métodos"

#End Region
End Class
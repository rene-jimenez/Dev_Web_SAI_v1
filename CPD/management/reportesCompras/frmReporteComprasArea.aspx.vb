Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup, CES.nspReporteCompras
Imports CRN.nspReporteCompras
Imports Microsoft.Reporting.WebForms
Public Class frmReporteComprasArea : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        Try
            Dim fechaActual As New List(Of ReportParameter)
            fechaActual.Add(New ReportParameter("fechaActual", (CDate(Date.Now).ToString("M/d/yyyy H:mm"))))
            fechaActual.Add(New ReportParameter("usuario", NombreUsuario))
            fechaActual.Add(New ReportParameter("sistema", sistemaActivo.nombre))
            fechaActual.Add(New ReportParameter("year", sistemaActivo.año))

            Dim dataSource As ReportDataSource = New ReportDataSource()
            rptReporteComprasArea.LocalReport.DataSources.Clear()

            Dim dtsReporteComprasArea As New dtsReporteComprasArea
            If txbFechaInicio.Text = "" Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Fecha inicial"))
            End If
            If txbFechaFinal.Text = "" Then
                txbFechaFinal.Text = txbFechaInicio.Text
            End If
            If CDate(txbFechaInicio.Text) > Date.Now Then
                Throw New Exception("La fecha inicial no puede ser mayor a la fecha de hoy")
            End If
            If CDate(txbFechaFinal.Text) > Date.Now Then
                Throw New Exception("La fecha final no puede ser mayor a la fecha de hoy")
            End If
            If CDate(txbFechaFinal.Text) < CDate(txbFechaInicio.Text) Then
                Throw New Exception("La fecha final no puede ser menor a la fecha inicial")
            End If

            Dim listaArea = New Proceso_ObtenerReportesCompras() With {.tipoConsulta = tipoConsultaReporteCompras.comprasPorArea, .fechaInicial = txbFechaInicio.Text, .fechaFinal = txbFechaFinal.Text, .idSistema = sisActivo.sistemaActivo.idSistema}.Ejecutar()
            If listaArea.Count > 0 Then
                resultadoReporte.Visible = False
                rptReporteComprasArea.Visible = True
                For i = 0 To listaArea.Count - 1
                    Dim nombreArea As String = listaArea(i).nombreArea
                    Dim fechaCaptura As String = listaArea(i).fechaElaboracion
                    Dim turnoDRM As String = listaArea(i).turnoDRM
                    Dim numPedido As String = listaArea(i).numeroPedido
                    Dim tipoPago As String = listaArea(i).tipoPago
                    Dim partida As String = listaArea(i).partida
                    Dim importe As Double = listaArea(i).importe
                    Dim fechaA As String = CDate(txbFechaInicio.Text).ToString("D")
                    Dim fechaB As String = CDate(txbFechaFinal.Text).ToString("D")
                    dtsReporteComprasArea.tblReporteComprasArea.AddtblReporteComprasAreaRow(nombreArea, fechaCaptura, turnoDRM, numPedido, tipoPago, partida, importe, fechaA, fechaB)
                Next
                dataSource = New ReportDataSource("dtsReporteComprasArea", dtsReporteComprasArea.tblReporteComprasArea.DefaultView)
                rptReporteComprasArea.ProcessingMode = ProcessingMode.Local
                rptReporteComprasArea.LocalReport.ReportPath = "management/reportesCompras/rpt/comprasAreas.rdlc"
                rptReporteComprasArea.LocalReport.SetParameters(fechaActual)
                rptReporteComprasArea.LocalReport.DataSources.Add(dataSource)
                rptReporteComprasArea.LocalReport.Refresh()
            Else
                resultadoReporte.Visible = True
                rptReporteComprasArea.Visible = False
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub
End Class
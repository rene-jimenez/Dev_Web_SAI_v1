Imports System.Globalization
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup, CES.nspReporteCompras
Imports CRN.nspReporteCompras
Imports Microsoft.Reporting.WebForms
Public Class frmReporteComprasProveedor : Inherits nspPaginaBase.PaginaBase
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
            Dim num As Integer = 0
            Dim fechaActual As New List(Of ReportParameter)
            fechaActual.Add(New ReportParameter("fechaActual", (CDate(Date.Now).ToString("M/d/yyyy H:mm"))))
            fechaActual.Add(New ReportParameter("usuario", NombreUsuario))
            fechaActual.Add(New ReportParameter("sistema", sistemaActivo.nombre))
            fechaActual.Add(New ReportParameter("year", sistemaActivo.año))

            Dim dataSource As ReportDataSource = New ReportDataSource()
            rptReporteComprasProveedor.LocalReport.DataSources.Clear()

            Dim dtsReporteComprasProveedor As New dtsReporteComprasProveedor
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

            Dim listaCompraProveedor = New Proceso_ObtenerReportesCompras() With {.tipoConsulta = tipoConsultaReporteCompras.comprasPorProveedor, .fechaInicial = txbFechaInicio.Text, .fechaFinal = txbFechaFinal.Text, .idSistema = sisActivo.sistemaActivo.idSistema}.Ejecutar()
            If listaCompraProveedor.Count > 0 Then
                resultadoReporte.Visible = False
                rptReporteComprasProveedor.Visible = True
                For i = 0 To listaCompraProveedor.Count - 1
                    num = num + 1
                    Dim nombreProveedor As String = listaCompraProveedor(i).nombre
                    Dim fechaCaptura As String = listaCompraProveedor(i).fechaElaboracion
                    Dim turnoDRM As String = listaCompraProveedor(i).turnoDRM
                    Dim numPedido As String = listaCompraProveedor(i).numeroPedido
                    Dim tipoPago As String = listaCompraProveedor(i).tipoPago
                    Dim importe As Double = listaCompraProveedor(i).importe
                    Dim fechaA As String = CDate(txbFechaInicio.Text).ToString("D")
                    Dim fechaB As String = CDate(txbFechaFinal.Text).ToString("D")
                    Dim numero As Integer = num
                    dtsReporteComprasProveedor.tblReporteComprasProveedor.AddtblReporteComprasProveedorRow(nombreProveedor, fechaCaptura, turnoDRM, numPedido, tipoPago, importe, fechaA, fechaB, num)
                Next
                dataSource = New ReportDataSource("dtsReporteComprasProveedor", dtsReporteComprasProveedor.tblReporteComprasProveedor.DefaultView)
                rptReporteComprasProveedor.ProcessingMode = ProcessingMode.Local
                rptReporteComprasProveedor.LocalReport.ReportPath = "management/reportesCompras/rpt/comprasProveedor.rdlc"
                rptReporteComprasProveedor.LocalReport.SetParameters(fechaActual)
                rptReporteComprasProveedor.LocalReport.DataSources.Add(dataSource)
                rptReporteComprasProveedor.LocalReport.Refresh()
            Else
                resultadoReporte.Visible = True
                rptReporteComprasProveedor.Visible = False
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub
End Class
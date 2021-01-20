Imports System.Globalization
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports CES, CES.nspPopup, CES.nspDetalleEntrada
Imports CRN.nspDetalleEntrada
Imports Microsoft.Reporting.WebForms
Public Class frmReporteEntradaArticulo : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

#Region "Botones"
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click

        Try
            Dim fechaActual As New List(Of ReportParameter)
            fechaActual.Add(New ReportParameter("fecha", CDate(Date.Now).ToString("D")))

            Dim dataSource As ReportDataSource = New ReportDataSource()
            rptReporteEntradaArticulo.LocalReport.DataSources.Clear()
            Dim dtsReporteEntradaArticulo As New dtsReporteEntradaArticulo
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

            Dim listaReporteArticulo = New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = tipoConsultaDetalleEntrada.rangoFechasProvedor, .fechaInicial = txbFechaInicio.Text, .fechaFinal = txbFechaFinal.Text, .idSistema = sisActivo.sistemaActivo.idSistema}.Ejecutar()
            If listaReporteArticulo.Count > 0 Then
                resultadoReporte.Visible = False
                rptReporteEntradaArticulo.Visible = True
                For i = 0 To listaReporteArticulo.Count - 1
                    Dim nombreArticulo As String = listaReporteArticulo(i)._nombreArticulo
                    Dim fechaEntrada As String = listaReporteArticulo(i).fechaEntrada
                    Dim proveedor As String = listaReporteArticulo(i)._nombreProveedor
                    Dim cantidad As String = listaReporteArticulo(i).cantidad
                    Dim precioUnitario As Double = listaReporteArticulo(i)._precioUnitario
                    Dim total As Double = listaReporteArticulo(i)._total
                    Dim fechaA As String = CDate(txbFechaInicio.Text).ToString("D")
                    Dim fechaB As String = CDate(txbFechaFinal.Text).ToString("D")
                    dtsReporteEntradaArticulo.tblReporteEntradaArticulo.AddtblReporteEntradaArticuloRow(nombreArticulo, fechaEntrada, proveedor, cantidad, precioUnitario, total, fechaA, fechaB)
                Next
                dataSource = New ReportDataSource("dtsReporteEntradaArticulo", dtsReporteEntradaArticulo.tblReporteEntradaArticulo.DefaultView)
                rptReporteEntradaArticulo.ProcessingMode = ProcessingMode.Local
                rptReporteEntradaArticulo.LocalReport.ReportPath = "management/almacen/reportes/rpt/reporteEntradaArticulo.rdlc"
                rptReporteEntradaArticulo.LocalReport.SetParameters(fechaActual)
                rptReporteEntradaArticulo.LocalReport.DataSources.Add(dataSource)
                rptReporteEntradaArticulo.LocalReport.Refresh()
            Else
                resultadoReporte.Visible = True
                rptReporteEntradaArticulo.Visible = False
            End If

        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

#End Region
End Class
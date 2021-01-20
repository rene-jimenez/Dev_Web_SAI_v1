Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Imports CES.nspReporteAlmacen, CRN.nspReporteAlmacen
Public Class frmReporteListaArticulos
    Inherits nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try

                Dim consultaListaArticulos = New Proceso_ObtenerReportesAlamacen() With {.tipoConsulta = tipoConsultaReporteAlmacen.listaArticulos, .tipoSistema = sistemaActivo.tipo}.Ejecutar
                Dim fechas As New List(Of ReportParameter)
                fechas.Add(New ReportParameter("fecha", CDate(Date.Now).ToString("D")))

                Dim dataSource As ReportDataSource = New ReportDataSource()
                rptListaArticulos.LocalReport.DataSources.Clear()
                Dim dtsListaArticulos As New dtsListaArticulos

                If consultaListaArticulos.Count > 0 Then

                    For i = 0 To consultaListaArticulos.Count - 1
                        Dim categoria As String = consultaListaArticulos(i).nombreCategoria
                        Dim articulo As String = consultaListaArticulos(i).nombreArticulo
                        Dim precio As String = "$ " + consultaListaArticulos(i).ultimoPrecio.ToString("N4")
                        Dim cantidad As String = consultaListaArticulos(i).existencia
                        Dim total As String = "$ " + consultaListaArticulos(i).total.ToString("N2")

                        dtsListaArticulos.tblListaArticulos.AddtblListaArticulosRow(categoria, articulo, precio, cantidad, total)
                    Next
                    dataSource = New ReportDataSource("dtsListaArticulos", dtsListaArticulos.tblListaArticulos.DefaultView)
                    rptListaArticulos.ProcessingMode = ProcessingMode.Local
                    rptListaArticulos.LocalReport.ReportPath = "management/almacen/reportes/rpt/rptListaArticulos.rdlc"
                    rptListaArticulos.LocalReport.SetParameters(fechas)
                    rptListaArticulos.LocalReport.DataSources.Add(dataSource)
                    rptListaArticulos.LocalReport.Refresh()
                Else
                    Throw New Exception("Lo sentimos, No hay resultados con esos parámetros!")
                End If

            Catch ex As Exception
                OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
            End Try
        End If
    End Sub
    Private Sub btnPrincipal_Click(sender As Object, e As EventArgs) Handles btnPrincipal.Click
        mandaDefault()
    End Sub
End Class
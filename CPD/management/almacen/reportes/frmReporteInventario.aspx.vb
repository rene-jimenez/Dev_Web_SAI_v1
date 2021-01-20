Imports System.Globalization
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports CES, CES.nspPopup, CES.nspCategoria, CES.nspArticulo
Imports CRN.nspCategoria, CRN.nspArticulo
Imports Microsoft.Reporting.WebForms
Public Class frmReporteInventario : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarCategoria()
        End If
    End Sub

#Region "Metodo"
    Public Sub poblarCategoria()
        Dim listaCategoria = New Proceso_ObtenerCategorias() With {.tipoConsulta = nspCategoria.tipoConsultaCategoria.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbCategoria.Items.Add("Todos")
        cmbCategoria.DataValueField = "id"
        cmbCategoria.DataTextField = "nombre"
        cmbCategoria.DataSource = listaCategoria.OrderBy(Function(a) a.esActivo).ToList
        cmbCategoria.DataBind()
    End Sub

#End Region

#Region "Botones"
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        Try

            Select Case cmbCategoria.SelectedValue

                Case "Todos"
                    Dim todosArticulos = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.todos, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar().OrderBy(Function(a) a.nombreCategoria).ToList

                    Dim fecha As Date = Date.Now
                    Dim fechaActual As New List(Of ReportParameter)
                    fechaActual.Add(New ReportParameter("fecha", CDate(fecha).ToString("D")))

                    Dim dataSource As ReportDataSource = New ReportDataSource()
                    rptReporteInventario.LocalReport.DataSources.Clear()
                    Dim dtsReporteInventario As New dtsReporteInventario

                    If todosArticulos.Count > 0 Then
                        resultadoVacioCategoria.Visible = False
                        rptReporteInventario.Visible = True
                        For i = 0 To todosArticulos.Count - 1
                            Dim descripcion As String = todosArticulos(i).nombre
                            Dim existencia As String = todosArticulos(i).existencia
                            Dim categoria As String = todosArticulos(i).nombreCategoria
                            dtsReporteInventario.tblInventario.AddtblInventarioRow(descripcion, existencia, categoria)
                        Next
                        dataSource = New ReportDataSource("dtsReporteInventario", dtsReporteInventario.tblInventario.DefaultView)
                        rptReporteInventario.ProcessingMode = ProcessingMode.Local
                        rptReporteInventario.LocalReport.ReportPath = "management/almacen/reportes/rpt/rptReporteInventario.rdlc"
                        rptReporteInventario.LocalReport.SetParameters(fechaActual)
                        rptReporteInventario.LocalReport.DataSources.Add(dataSource)
                        rptReporteInventario.LocalReport.Refresh()
                    Else
                        resultadoVacioCategoria.Visible = True
                        rptReporteInventario.Visible = False
                    End If
                Case Else

                    Dim idCat As Guid = Guid.Parse(cmbCategoria.SelectedValue.ToString)
                    Dim listaArticulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.reporteXCategoria, .idCategoria = idCat, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar()

                    Dim fechaActual As New List(Of ReportParameter)
                    fechaActual.Add(New ReportParameter("fecha", CDate(Date.Now).ToString("D")))

                    Dim dataSource As ReportDataSource = New ReportDataSource()
                    rptReporteInventario.LocalReport.DataSources.Clear()
                    Dim dtsReporteInventario As New dtsReporteInventario

                    If listaArticulo.Count > 0 Then
                        resultadoVacioCategoria.Visible = False
                        rptReporteInventario.Visible = True
                        For i = 0 To listaArticulo.Count - 1
                            Dim descripcion As String = listaArticulo(i).nombre
                            Dim existencia As String = listaArticulo(i).existencia
                            Dim categoria As String = listaArticulo(i).nombreCategoria

                            dtsReporteInventario.tblInventario.AddtblInventarioRow(descripcion, existencia, categoria)
                        Next
                        dataSource = New ReportDataSource("dtsReporteInventario", dtsReporteInventario.tblInventario.DefaultView)
                        rptReporteInventario.ProcessingMode = ProcessingMode.Local
                        rptReporteInventario.LocalReport.ReportPath = "management/almacen/reportes/rpt/rptReporteInventario.rdlc"
                        rptReporteInventario.LocalReport.SetParameters(fechaActual)
                        rptReporteInventario.LocalReport.DataSources.Add(dataSource)
                        rptReporteInventario.LocalReport.Refresh()
                    Else
                        resultadoVacioCategoria.Visible = True
                        rptReporteInventario.Visible = False
                    End If


            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try

    End Sub
#End Region
End Class
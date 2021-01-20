Imports CES.nspPopup
Imports Microsoft.Reporting.WebForms
Imports CES.nspArticulo, CRN.nspArticulo
Imports System.Drawing

Public Class frmCodigosBarras
    Inherits nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim dataSource As ReportDataSource = New ReportDataSource()
                rpt.LocalReport.DataSources.Clear()
                Dim dtsCodigo As New dtsCodigo
                Dim consultarArticulos = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.activosParaCodigoBarras, .tipoSistema = sistemaActivo.tipo, .esActivo = True}.Ejecutar()
                For i = 0 To consultarArticulos.Count - 1
                    Dim categoria As String = consultarArticulos(i).nombreCategoria
                    Dim codigoBarras As String = consultarArticulos(i).codigoBarras
                    Dim conversiones As New Contexto.Biblioteca.controladorDeFunciones.Conversion
                    Dim bm As Bitmap = conversiones.strToBarCode128(codigoBarras)
                    Dim converter As New ImageConverter
                    Dim aa = converter.ConvertTo(bm, GetType(Byte()))
                    dtsCodigo.tblCodigoBarras.AddtblCodigoBarrasRow(codigoBarras, categoria, aa)
                Next
                dataSource = New ReportDataSource("dtsCodigo", dtsCodigo.tblCodigoBarras.DefaultView)
                rpt.ProcessingMode = ProcessingMode.Local
                rpt.LocalReport.ReportPath = "management/almacen/reportes/rpt/rptCodigosBarras.rdlc"
                rpt.LocalReport.DataSources.Add(dataSource)
                rpt.LocalReport.Refresh()
            Catch ex As Exception
                OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
            End Try
        End If
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnPrincipal_Click(sender As Object, e As EventArgs) Handles btnPrincipal.Click
        mandaDefault()
    End Sub
End Class
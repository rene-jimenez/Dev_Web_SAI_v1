Imports CES.nspDetalleSalidaAlmacen
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Imports CES.nspReporteAlmacen, CRN.nspReporteAlmacen
Imports CES.nspCategoria, CRN.nspCategoria
Public Class frmReporteSalidaPorCategoria : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarCategorias()
        End If
    End Sub
#Region "botones"
    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        Try
            Dim respuestaValidacion = validarCampos()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If
            Dim consultaSalidasPorCategoria As List(Of reporteAlmacen) = consultar(cmbCategoria.SelectedValue)
            Dim fechas As New List(Of ReportParameter)
            fechas.Add(New ReportParameter("fecha", CDate(Date.Now).ToString("D")))
            fechas.Add(New ReportParameter("fechaInicial", CDate(txbFechaInicial.Text).ToString("D")))
            fechas.Add(New ReportParameter("fechaFinal", CDate(txbFechaFinal.Text).ToString("D")))

            Dim dataSource As ReportDataSource = New ReportDataSource()
            rptSalidaPorCategoria.LocalReport.DataSources.Clear()
            Dim dtsSalidaPorCategoria As New dtsSalidaPorCategoria

            If consultaSalidasPorCategoria.Count > 0 Then
                rptSalidaPorCategoria.Visible = True

                For i = 0 To consultaSalidasPorCategoria.Count - 1

                    Dim categoria As String = consultaSalidasPorCategoria(i).nombreCategoria
                    Dim area As String = consultaSalidasPorCategoria(i).nombreArea
                    Dim cantidad As Integer = consultaSalidasPorCategoria(i).cantidad
                    Dim precio As String = "$ " + consultaSalidasPorCategoria(i).gastoTotal.ToString("N2")

                    dtsSalidaPorCategoria.tblSalidaPorCategoria.AddtblSalidaPorCategoriaRow(categoria, area, cantidad, precio)
                Next
                dataSource = New ReportDataSource("dtsSalidaPorCategoria", dtsSalidaPorCategoria.tblSalidaPorCategoria.DefaultView)
                rptSalidaPorCategoria.ProcessingMode = ProcessingMode.Local
                rptSalidaPorCategoria.LocalReport.ReportPath = "management/almacen/reportes/rpt/rptSalidaPorCategoria.rdlc"
                rptSalidaPorCategoria.LocalReport.SetParameters(fechas)
                rptSalidaPorCategoria.LocalReport.DataSources.Add(dataSource)
                rptSalidaPorCategoria.LocalReport.Refresh()
            Else
                rptSalidaPorCategoria.Visible = False
                Throw New Exception("Lo sentimos, No hay resultados con esos parámetros!")
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
    Private Sub btnPrincipal_Click(sender As Object, e As EventArgs) Handles btnPrincipal.Click
        mandaDefault()
    End Sub
#End Region

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
            End If
            If CDate(txbFechaInicial.Text) > CDate(txbFechaFinal.Text) Then
                Throw New Exception("La fecha final no debe ser menor la fecha inicial")
            End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
#End Region
#Region "metodos"
    Protected Sub poblarCategorias()
        Dim listaCategorias = New Proceso_ObtenerCategorias() With {.tipoConsulta = tipoConsultaCategoria.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbCategoria.Items.Add("Todos")
        cmbCategoria.DataValueField = "id"
        cmbCategoria.DataTextField = "nombre"
        cmbCategoria.DataSource = listaCategorias.OrderBy(Function(a) a.nombre).ToList
        cmbCategoria.DataBind()
    End Sub
    Function consultar(accion As String) As List(Of reporteAlmacen)
        Dim resultado As List(Of reporteAlmacen) = Nothing
        Select Case accion
            Case "Todos"
                resultado = New Proceso_ObtenerReportesAlamacen() With {.tipoConsulta = tipoConsultaReporteAlmacen.salidaPorCategoria, .fechaInicial = CDate(txbFechaInicial.Text), .fechaFinal = CDate(txbFechaFinal.Text), .idSistema = Guid.Parse(sistemaActivo.id.ToString)}.Ejecutar
            Case Else
                resultado = New Proceso_ObtenerReportesAlamacen() With {.tipoConsulta = tipoConsultaReporteAlmacen.salidaPorCategoria, .fechaInicial = CDate(txbFechaInicial.Text), .fechaFinal = CDate(txbFechaFinal.Text), .idSistema = Guid.Parse(sistemaActivo.id.ToString)}.Ejecutar.Where(Function(x) x.idCategoria = Guid.Parse(cmbCategoria.SelectedValue)).ToList
        End Select
        Return resultado
    End Function
#End Region
End Class
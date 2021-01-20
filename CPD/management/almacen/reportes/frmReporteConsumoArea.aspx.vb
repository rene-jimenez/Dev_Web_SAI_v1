Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Imports CES.nspReporteAlmacen, CES.nspArea
Imports CRN.nspReporteAlmacen, CRN.nspArea
Public Class frmReporteConsumoArea : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarAreas()
        End If
    End Sub
#Region "Metodos"
    Protected Sub poblarAreas()
        Dim listaAreas = New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbArea.DataValueField = "id"
        cmbArea.DataTextField = "nombre"
        cmbArea.DataSource = listaAreas.OrderBy(Function(a) a.nombre).ToList
        cmbArea.DataBind()
        cmbArea.SelectedValue = "Seleccione un elemento de la lista"
    End Sub



#End Region
#Region "Funciones"
    Private Function validarCampos()
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If cmbArea.SelectedValue = "Seleccione un elemento de la lista" Then
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = "Seleccione un elemento de la lista"
                Throw New Exception(respuesta.comentario)
            End If
            If txbFechaInicial.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Fecha inicial"))
            End If
            'If txbFechaInicial.Text > Date.Now Then
            '    Throw New Exception("La fecha inicial no debe ser mayor a la fecha actual")
            'End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
#End Region
#Region "Botones"
    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        Try
            Dim respuestaValidacion = validarCampos()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If

            Dim dataSource As ReportDataSource = New ReportDataSource
            rptReporteConsumoArea.LocalReport.DataSources.Clear()
            Dim dtsConsumoArea As New dtsConsumoArea

            If txbFechaFinal.Text = "" Then
                txbFechaFinal.Text = txbFechaInicial.Text
            End If
            If CDate(txbFechaInicial.Text) > Date.Now Then
                Throw New Exception("La fecha inicial no puede ser mayor a la fecha de hoy")
            End If
            If CDate(txbFechaFinal.Text) > Date.Now Then
                Throw New Exception("La fecha final no puede ser mayor a la fecha de hoy")
            End If
            If CDate(txbFechaFinal.Text) < CDate(txbFechaInicial.Text) Then
                Throw New Exception("La fecha final no puede ser menor a la fecha inicial")
            End If
            Dim consumoPorArea = New Proceso_ObtenerReportesAlamacen() With {.tipoConsulta = tipoConsultaReporteAlmacen.consumoPorArea, .fechaInicial = CDate(txbFechaInicial.Text), .fechaFinal = CDate(txbFechaFinal.Text), .idSistema = Guid.Parse(sistemaActivo.id.ToString)}.Ejecutar().Where(Function(x) x.idArea = Guid.Parse(cmbArea.SelectedValue)).ToList
            If consumoPorArea.Count > 0 Then
                resultadoVacioConsumo.Visible = False
                rptReporteConsumoArea.Visible = True
                For i = 0 To consumoPorArea.Count - 1
                    Dim area As String = consumoPorArea(i).nombreArea
                    Dim categoria As String = consumoPorArea(i).nombreCategoria
                    Dim articulo As String = consumoPorArea(i).nombreArticulo
                    Dim ultmPrecio As Double = consumoPorArea(i).ultimoPrecio
                    Dim cantidad As Integer = consumoPorArea(i).cantidad
                    Dim consumo As Double = consumoPorArea(i).gastoTotal
                    Dim fechaA As String = txbFechaInicial.Text
                    Dim fechaB As String = txbFechaFinal.Text
                    dtsConsumoArea.tblConsumoArea.AddtblConsumoAreaRow(area, categoria, articulo, ultmPrecio, cantidad, consumo, fechaA, fechaB)
                Next
                dataSource = New ReportDataSource("dtsConsumoArea", dtsConsumoArea.tblConsumoArea.DefaultView)
                rptReporteConsumoArea.ProcessingMode = ProcessingMode.Local
                rptReporteConsumoArea.LocalReport.ReportPath = "management/almacen/reportes/rpt/reporteConsumoPorArea.rdlc"
                rptReporteConsumoArea.LocalReport.DataSources.Add(dataSource)
                rptReporteConsumoArea.LocalReport.Refresh()
            Else
                resultadoVacioConsumo.Visible = True
                rptReporteConsumoArea.Visible = False

            End If

        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
#End Region
End Class
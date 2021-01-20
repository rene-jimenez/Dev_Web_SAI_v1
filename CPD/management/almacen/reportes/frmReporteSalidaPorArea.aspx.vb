Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Imports CES.nspReporteAlmacen, CRN.nspReporteAlmacen
Imports CES.nspArea, CRN.nspArea
Public Class frmReporteSalidaPorArea
    Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarAreas()
        End If
    End Sub
#Region "botones"
    Private Sub btnGenerarReporte_Click(sender As Object, e As EventArgs) Handles btnGenerarReporte.Click
        Try
            Dim respuestaValidacion = validarCampos()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If
            Dim consultaSalidaPorArea = consultar(cmbArea.SelectedValue)
            Dim fechas As New List(Of ReportParameter)
            fechas.Add(New ReportParameter("fecha", CDate(Date.Now).ToString("D")))
            fechas.Add(New ReportParameter("fechaInicial", CDate(txbFechaInicial.Text).ToString("D")))
            fechas.Add(New ReportParameter("fechaFinal", CDate(txbFechaFinal.Text).ToString("D")))

            Dim dataSource As ReportDataSource = New ReportDataSource()
            rptSalidaPorArea.LocalReport.DataSources.Clear()
            Dim dtsSalidaPorArea As New dtsSalidaPorArea

            If consultaSalidaPorArea.Count > 0 Then
                rptSalidaPorArea.Visible = True
                For i = 0 To consultaSalidaPorArea.Count - 1
                    Dim fechaSalida As String = consultaSalidaPorArea(i).fechaSalida
                    Dim area As String = consultaSalidaPorArea(i).nombreArea
                    Dim articulo As String = consultaSalidaPorArea(i).nombreArticulo
                    Dim cantidad As String = consultaSalidaPorArea(i).cantidad
                    Dim numVale As String = consultaSalidaPorArea(i).numVale

                    dtsSalidaPorArea.tblSalidaPorArea.AddtblSalidaPorAreaRow(fechaSalida, area, articulo, cantidad, numVale)
                Next
                dataSource = New ReportDataSource("dtsSalidaPorArea", dtsSalidaPorArea.tblSalidaPorArea.DefaultView)
                rptSalidaPorArea.ProcessingMode = ProcessingMode.Local
                rptSalidaPorArea.LocalReport.ReportPath = "management/almacen/reportes/rpt/rptSalidaPorArea.rdlc"
                rptSalidaPorArea.LocalReport.SetParameters(fechas)
                rptSalidaPorArea.LocalReport.DataSources.Add(dataSource)
                rptSalidaPorArea.LocalReport.Refresh()
            Else
                rptSalidaPorArea.Visible = False
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
                    Throw New Exception("La fecha final no debe ser menor a la fecha inicial")
                End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
#End Region
#Region "metodos"
    Protected Sub poblarAreas()
        Dim listaAreas = New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.esActivo.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbArea.Items.Add("Todos")
        cmbArea.DataValueField = "id"
        cmbArea.DataTextField = "nombre"
        cmbArea.DataSource = listaAreas.OrderBy(Function(a) a.nombre).ToList
        cmbArea.DataBind()
    End Sub
    Function consultar(accion As String) As List(Of reporteAlmacen)
        Dim resultado As List(Of reporteAlmacen) = Nothing
        Select Case accion
            Case "Todos"
                resultado = New Proceso_ObtenerReportesAlamacen() With {.tipoConsulta = tipoConsultaReporteAlmacen.salidaPorArea, .fechaInicial = CDate(txbFechaInicial.Text), .fechaFinal = CDate(txbFechaFinal.Text), .idSistema = Guid.Parse(sistemaActivo.id.ToString)}.Ejecutar
            Case Else
                resultado = New Proceso_ObtenerReportesAlamacen() With {.tipoConsulta = tipoConsultaReporteAlmacen.salidaPorArea, .fechaInicial = CDate(txbFechaInicial.Text), .fechaFinal = CDate(txbFechaFinal.Text), .idSistema = Guid.Parse(sistemaActivo.id.ToString)}.Ejecutar.Where(Function(x) x.idArea = Guid.Parse(cmbArea.SelectedValue)).ToList
        End Select
        Return resultado
    End Function
#End Region
End Class
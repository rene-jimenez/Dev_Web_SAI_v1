Imports CES.nspDetalleSalidaAlmacen
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Public Class frmreporteSalidaxArticulo : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarcombo()
                'panel2.Visible = False
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub

#Region "botones"
    Private Sub btnBuscarFecha_Click(sender As Object, e As EventArgs) Handles btnBuscarFecha.Click
        Try
            Dim resultadoValidacion = validarfechas()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            If cmbArtículo.SelectedValue = "No definido" Then
                Dim consultaarticulo = New CRN.nspDetalleSalidaAlmacen.Proceso_ObtenerDetallesSalidaAlmacen() With {.tipoConsulta = tipoConsultaDetalleSalidaAlmacen.soloRangoFechas, .fechaInicial = txbFechaInicial.Text, .fechaFinal = txbFechaFinal.Text, .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a._nombreArticulo).ToList
                If consultaarticulo.Count > 0 Then
                    Dim fechas As New List(Of ReportParameter)
                    fechas.Add(New ReportParameter("fechaActual", UCase(CDate(Date.Now).ToString("D"))))
                    fechas.Add(New ReportParameter("fechaInicial", UCase(CDate(txbFechaInicial.Text).ToString("MMMM dd, yyyy"))))
                    fechas.Add(New ReportParameter("fechaFinal", UCase(CDate(txbFechaFinal.Text).ToString("MMMM dd, yyyy"))))
                    Dim dataSource As ReportDataSource = New ReportDataSource()
                    rptSalidaxArticulo.LocalReport.DataSources.Clear()
                    Dim dts As New dtsSalidaxArticulo

                    For i = 0 To consultaarticulo.Count - 1
                        Dim nombre As String = UCase(consultaarticulo(i)._nombreArticulo.ToString)
                        Dim area As String = UCase(consultaarticulo(i)._nombreArea.ToString)
                        Dim cantidad As Integer = consultaarticulo(i).cantidad
                        dts.tblSalidaxArticulo.AddtblSalidaxArticuloRow(nombre, area, cantidad)
                    Next
                    dataSource = New ReportDataSource("DataSet1", dts.tblSalidaxArticulo.DefaultView)
                    rptSalidaxArticulo.ProcessingMode = ProcessingMode.Local
                    rptSalidaxArticulo.LocalReport.ReportPath = "management/almacen/reportes/rpt/rptSalidaxArticulo.rdlc"
                    rptSalidaxArticulo.LocalReport.SetParameters(fechas)
                    rptSalidaxArticulo.LocalReport.DataSources.Add(dataSource)
                    rptSalidaxArticulo.LocalReport.Refresh()

                Else
                    Throw New Exception("El rango de fechas no generó ningun resultado")
                    txbFechaFinal.Text = ""
                    txbFechaInicial.Text = ""
                    cmbArtículo.SelectedValue = "No definido"
                End If
            Else
                Dim consultaarticulo = New CRN.nspDetalleSalidaAlmacen.Proceso_ObtenerDetallesSalidaAlmacen() With {.tipoConsulta = tipoConsultaDetalleSalidaAlmacen.rangofechas, .fechaInicial = txbFechaInicial.Text, .fechaFinal = txbFechaFinal.Text, .idArticulo = Guid.Parse(cmbArtículo.SelectedValue), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a._nombreArticulo).ToList
                If consultaarticulo.Count > 0 Then
                    Dim fechas As New List(Of ReportParameter)
                    fechas.Add(New ReportParameter("fechaActual", UCase(CDate(Date.Now))))
                    fechas.Add(New ReportParameter("fechaInicial", UCase((CDate(txbFechaInicial.Text)).ToString("MMMM dd, yyyy"))))
                    fechas.Add(New ReportParameter("fechaFinal", UCase((CDate(txbFechaFinal.Text)).ToString("MMMM dd, yyyy"))))
                    Dim dataSource As ReportDataSource = New ReportDataSource()
                    rptSalidaxArticulo.LocalReport.DataSources.Clear()
                    Dim dts As New dtsSalidaxArticulo

                    For i = 0 To consultaarticulo.Count - 1
                        Dim nombre As String = UCase(consultaarticulo(i)._nombreArticulo.ToString)
                        Dim area As String = UCase(consultaarticulo(i)._nombreArea.ToString)
                        Dim cantidad As Integer = consultaarticulo(i).cantidad
                        dts.tblSalidaxArticulo.AddtblSalidaxArticuloRow(nombre, area, cantidad)
                    Next
                    dataSource = New ReportDataSource("DataSet1", dts.tblSalidaxArticulo.DefaultView)
                    rptSalidaxArticulo.ProcessingMode = ProcessingMode.Local
                    rptSalidaxArticulo.LocalReport.ReportPath = "management/almacen/reportes/rpt/rptSalidaxArticulo.rdlc"
                    rptSalidaxArticulo.LocalReport.SetParameters(fechas)
                    rptSalidaxArticulo.LocalReport.DataSources.Add(dataSource)
                    rptSalidaxArticulo.LocalReport.Refresh()

                Else
                    Throw New Exception("El rango de fechas no generó ningun resultado para el artículo seleccionado")
                    txbFechaFinal.Text = ""
                    txbFechaInicial.Text = ""
                    cmbArtículo.SelectedValue = "No definido"
                End If

            End If

            txbFechaFinal.Text = ""
            txbFechaInicial.Text = ""
            cmbArtículo.SelectedValue = "No definido"
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try


    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        mandaDefault()
    End Sub
    'Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
    '    mandaDefault()

    'End Sub



#End Region

#Region "validarDatos"
    Private Function validarfechas()
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)

        If txbFechaInicial.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha inicial")
            Throw New Exception(respuesta.comentario)
        End If
        If txbFechaFinal.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha final")
            Throw New Exception(respuesta.comentario)
        End If
        If CDate(txbFechaInicial.Text) > Date.Now Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha inicial no debe ser mayor a la fecha actual"
            Throw New Exception(respuesta.comentario)
        End If
        If CDate(txbFechaInicial.Text) > CDate(txbFechaFinal.Text) Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha final no debe ser mayor a la fecha inicial"
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function
#End Region
#Region "llenarCombo"
    Protected Sub llenarcombo()
        Dim consultaArticulos = New CRN.nspArticulo.Proceso_ObtenerArticulos() With {.tipoConsulta = CES.nspArticulo.tipoConsultaArticulo.todos, .tipoSistema = sistemaActivo.tipo}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbArtículo.DataValueField = "id"
        cmbArtículo.DataTextField = "nombre"
        cmbArtículo.DataSource = consultaArticulos.OrderBy(Function(a) a.nombre).ToList
        cmbArtículo.DataBind()
        cmbArtículo.SelectedValue = "No definido"
    End Sub



#End Region


End Class
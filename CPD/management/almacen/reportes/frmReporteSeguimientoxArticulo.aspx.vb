Imports CES.nspReporteSeguimientoxArticulo, CRN.nspReporteSeguimientoxArticulo
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Public Class frmReporteSeguimientoxArticulo : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                rptSeguimientoxArticulos.Visible = True
                llenarcombo()
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub

#Region "botones"


    'Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
    '    mandaDefault()
    'End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        mandaDefault()
    End Sub

    Private Sub btnBuscarFecha_Click(sender As Object, e As EventArgs) Handles btnBuscarFecha.Click
        Try

            Dim resultadoValidacion = validarfechas()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim consultaarticulos As New List(Of CES.nspReporteSeguimientoxArticulo.ReporteSeguimientoxArticulo)
            If cmbArtículo.SelectedValue = "No definido" Then
                consultaarticulos = New CRN.nspReporteSeguimientoxArticulo.Proceso_ObtenerReporteSeguimientoxArticulo() With {.fechaInicial = txbFechaInicial.Text, .fechaFinal = txbFechaFinal.Text, .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.articulo).ToList
            Else
                consultaarticulos = New CRN.nspReporteSeguimientoxArticulo.Proceso_ObtenerReporteSeguimientoxArticulo() With {.fechaInicial = txbFechaInicial.Text, .fechaFinal = txbFechaFinal.Text, .idArticulo = Guid.Parse(cmbArtículo.SelectedValue), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fecha).ToList
            End If
            rptSeguimientoxArticulos.Visible = True

            If consultaarticulos.Count > 0 Then
                Dim fechas As New List(Of ReportParameter)
                fechas.Add(New ReportParameter("fechaActual", UCase(CDate(Date.Now).ToString("D"))))
                fechas.Add(New ReportParameter("fechaInicial", UCase((CDate(txbFechaInicial.Text)).ToString("MMMM dd, yyyy"))))
                fechas.Add(New ReportParameter("fechaFinal", UCase((CDate(txbFechaFinal.Text)).ToString("MMMM dd, yyyy"))))
                Dim dataSource As ReportDataSource = New ReportDataSource()
                rptSeguimientoxArticulos.LocalReport.DataSources.Clear()
                Dim dts As New dtsSeguimientoxArticulo
                Dim entradas As String = 0
                Dim salidas As String = 0
                Dim bandE As Integer = 0
                Dim bandS As Integer = 0
                Dim totalArt As Integer = consultaarticulos.Count - 1
                Dim j As Integer = 0
                Dim auxArticulo As String = consultaarticulos(0).articulo
                For i = 0 To consultaarticulos.Count - 1
                    If auxArticulo = consultaarticulos(i).articulo Then
                        entradas = 0
                        salidas = 0
                        j = i
                        While auxArticulo = consultaarticulos(j).articulo.ToString
                            If consultaarticulos(j).tipoOperacion = "Entrada" And bandE = 0 Then
                                bandE = 1
                                entradas = consultaarticulos(j).suma.ToString
                            End If
                            If consultaarticulos(j).tipoOperacion = "Salida" And bandS = 0 Then
                                bandS = 1
                                salidas = consultaarticulos(j).suma.ToString
                            End If
                            If j < consultaarticulos.Count - 1 Then
                                j = j + 1
                                If consultaarticulos(j).articulo Is Nothing Then
                                    Exit While
                                End If
                            Else
                                Exit While
                            End If
                        End While
                        bandE = 0
                        bandS = 0
                        If Not consultaarticulos(j).articulo Is Nothing Then
                            auxArticulo = consultaarticulos(j).articulo
                        Else
                            auxArticulo = ""
                        End If
                    End If
                    Dim articulo As String = UCase(consultaarticulos(i).articulo.ToString)
                    Dim stockMinimo As String = consultaarticulos(i).stockMinimo
                    Dim stockMaximo As String = consultaarticulos(i).stockMaximo
                    Dim fecha As String = UCase(CDate(consultaarticulos(i).fecha.ToString("MMMM dd, yyyy")))
                    Dim folio As String = consultaarticulos(i).folio
                    Dim tipoOperacion As String = UCase(consultaarticulos(i).tipoOperacion.ToString)
                    Dim existencia As String = consultaarticulos(i).existencia
                    Dim cantidad As String = consultaarticulos(i).cantidad
                    Dim area As String = UCase(consultaarticulos(i).area.ToString)
                    dts.tblSeguimientoxArticulo.AddtblSeguimientoxArticuloRow(articulo, fecha, folio, tipoOperacion, existencia, cantidad, area, stockMinimo, stockMaximo, entradas, salidas)
                Next
                dataSource = New ReportDataSource("DataSet1", dts.tblSeguimientoxArticulo.DefaultView)
                rptSeguimientoxArticulos.ProcessingMode = ProcessingMode.Local
                rptSeguimientoxArticulos.LocalReport.ReportPath = "management/almacen/reportes/rpt/rptSeguimientoxArticulos.rdlc"
                rptSeguimientoxArticulos.LocalReport.SetParameters(fechas)
                rptSeguimientoxArticulos.LocalReport.DataSources.Add(dataSource)
                rptSeguimientoxArticulos.LocalReport.Refresh()


            Else
                Throw New Exception("el rango de fechas no generó ningun resultado")
                txbFechaFinal.Text = ""
                txbFechaInicial.Text = ""
                cmbArtículo.SelectedValue = "No definido"
            End If
            txbFechaFinal.Text = ""
            txbFechaInicial.Text = ""
            cmbArtículo.SelectedValue = "No definido"
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub


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
Imports CES.nspOficio
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Public Class frmReporteOficiosxAtender : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

#Region "botones"
    Private Sub btnBuscarFecha_Click(sender As Object, e As EventArgs) Handles btnBuscarFecha.Click
        Try

            Dim bandera As String = Request.QueryString("band")
            Dim etiqueta As String
            Dim resultadoValidacion = validar()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            If bandera = "atendidos" Then
                bandera = "1"
                etiqueta = "ATENDIDOS"
            Else
                If bandera = "porAtender" Then
                    bandera = "porAtender"
                    bandera = "0"
                    etiqueta = "POR ATENDER"
                Else

                    Throw New Exception("Se produjó un error, la bandera es erronea")
                End If


            End If
            Dim FFinal As Date = CDate(txbFechaFinal.Text)
            Dim consulta = New CRN.nspOficio.Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.esAtendido, .fechaInicial = CDate(txbFechaInicial.Text), .fechaFinal = CDate(txbFechaFinal.Text), .esAtendido = bandera, .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.turnoDRM).ToList
            Dim numero As Integer = CInt(consulta.Count)
            If numero > 0 Then
                Dim fechas As New List(Of ReportParameter)
                'fechas.Add(New ReportParameter("fechaActual", UCase(CDate(Date.Now))))
                fechas.Add(New ReportParameter("fechaInicial", UCase((CDate(txbFechaInicial.Text)).ToString("MMMM dd, yyyy"))))
                fechas.Add(New ReportParameter("fechaFinal", UCase((CDate(txbFechaFinal.Text)).ToString("MMMM dd, yyyy"))))
                fechas.Add(New ReportParameter("etiqueta", etiqueta.ToString))
                fechas.Add(New ReportParameter("numero", numero))
                fechas.Add(New ReportParameter("elaboro", UCase(NombreUsuario.ToString)))
                fechas.Add(New ReportParameter("tipoSistema", UCase(sistemaActivo.nombre.ToString)))
                fechas.Add(New ReportParameter("anio", UCase(sistemaActivo.año.ToString)))
                Dim dataSource As ReportDataSource = New ReportDataSource()
                rptOficioxAtender.LocalReport.DataSources.Clear()
                Dim dts As New dtsReporteOficioxAtender
                For i = 0 To consulta.Count - 1
                    Dim fechaAtendido As String
                    Dim turnoDRM As String
                    Dim turnoSAF As String
                    Dim asunto As String
                    Dim fechaCaptura As String = (CDate(consulta(i).fechaCaptura).ToString("dd/MM/yyyy"))
                    If Not consulta(i).fechaAtendido Is Nothing Then
                        fechaAtendido = (CDate(consulta(i).fechaAtendido)).ToString("dd/MM/yyyy")
                    Else
                        fechaAtendido = "N/A"

                    End If
                    If Not consulta(i).turnoDRM Is Nothing Then
                        turnoDRM = UCase(consulta(i).turnoDRM.ToString)
                    Else
                        turnoDRM = "N/A"
                    End If
                    If Not consulta(i).turnoSAF Is Nothing Then
                        turnoSAF = UCase(consulta(i).turnoSAF.ToString)
                    Else
                        turnoSAF = "N/A"
                    End If

                    Dim area As String = UCase(consulta(i)._area.ToString)
                    If Not consulta(i).asunto Is Nothing Then
                        asunto = UCase(consulta(i).asunto.ToString)
                    Else
                        asunto = "N/A"
                    End If
                    Dim cont As Integer = cont + 1
                    Dim diast As Integer = DateDiff(DateInterval.Day, CDate(consulta(i).fechaCaptura), Date.Now)
                    dts.tblOficio.AddtblOficioRow(fechaCaptura, fechaAtendido, turnoDRM, turnoSAF, area, asunto, cont, diast)
                Next
                dataSource = New ReportDataSource("DataSet1", dts.tblOficio.DefaultView)
                rptOficioxAtender.ProcessingMode = ProcessingMode.Local
                rptOficioxAtender.LocalReport.ReportPath = "management/oficio/reportes/rpt/rptReporteOficioxAtender.rdlc"
                rptOficioxAtender.LocalReport.SetParameters(fechas)
                rptOficioxAtender.LocalReport.DataSources.Add(dataSource)
                rptOficioxAtender.LocalReport.Refresh()
                rptOficioxAtender.LocalReport.Refresh()
                updrpt.Update()

            Else
                Throw New Exception("El rango de fechas no generó ningun resultado")
                txbFechaFinal.Text = ""
                txbFechaInicial.Text = ""
            End If


        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")

        End Try
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        mandaDefault()
    End Sub
#End Region

#Region "validar"
    Private Function validar()
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
        If txbFechaInicial.Text > Date.Now Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha inicial no debe ser mayor a la fecha actual"
            Throw New Exception(respuesta.comentario)
        End If

        If txbFechaInicial.Text > txbFechaInicial.Text Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha final no debe ser mayor a la fecha inicial"
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function
#End Region

End Class
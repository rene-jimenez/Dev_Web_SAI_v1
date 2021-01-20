Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Imports CES.nspSolicitudGastoComprobacionNoLiberado, CRN.nspSolicitudGastoComprobacionNoLiberado
Public Class frmReporteSolicitudGastoNoLiberado : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub
    Public Function validaFecha() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)

        If txbFechaInicial.Text.Length = 0 Then
            Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Fecha inicial"))
            txbFechaInicial.Focus()
        End If
        If txbFechaFinal.Text.Length = 0 Then
            Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Fecha final"))
            txbFechaFinal.Focus()
        End If

        If CDate(txbFechaInicial.Text) > Date.Now Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha inicial no puede ser mayor a la fecha actual"
            Throw New Exception(respuesta.comentario)
            txbFechaInicial.Text = String.Empty
            txbFechaInicial.Focus()
        End If

        If CDate(txbFechaFinal.Text) > Date.Now Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha Final no puede ser mayor a la fecha actual"
            Throw New Exception(respuesta.comentario)
            txbFechaFinal.Text = String.Empty
            txbFechaFinal.Focus()
        End If


        If CDate(txbFechaInicial.Text) > CDate(txbFechaFinal.Text) Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha final no puede ser menor a la fecha inicial"
            Throw New Exception(respuesta.comentario)
            txbFechaFinal.Text = String.Empty
            txbFechaFinal.Focus()
        End If

        Return respuesta
    End Function
    Public Sub limpia()
        txbFechaFinal.Text = String.Empty
        txbFechaInicial.Text = String.Empty
    End Sub

    Protected Sub btnGenerarReporte_Click(sender As Object, e As EventArgs)
        Try
            Dim respuestaValidacion = validaFecha()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If

            Dim laFechaFinal, laFechaInicial As Date

            If txbFechaInicial.Text = txbFechaFinal.Text Then
                laFechaInicial = CDate(txbFechaInicial.Text).AddHours(0).AddMinutes(0).AddSeconds(0)
                laFechaFinal = CDate(txbFechaFinal.Text).AddHours(23).AddMinutes(59).AddSeconds(59)
            Else
                laFechaInicial = CDate(txbFechaInicial.Text).AddHours(0).AddMinutes(0).AddSeconds(0)
                laFechaFinal = CDate(txbFechaFinal.Text).AddHours(23).AddMinutes(59).AddSeconds(59)
            End If


            Dim listaNoLiberados = New CRN.nspSolicitudGastoComprobacionNoLiberado.Proceso_ObtenerSolicitudGastoComprobacionNoLiberado() With {.fechaInicial = laFechaInicial, .fechaFinal = laFechaFinal, .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(v) v.turnoDRM)


            If listaNoLiberados.Count = 0 Then
                Throw New Exception("El rango de fechas no trae resultados")
            End If

            rptSolGasComNoLib.LocalReport.DataSources.Clear()
            rptSolGasComNoLib.LocalReport.Refresh()


            Dim num As Integer = 0
            Dim paraMetros As New List(Of ReportParameter)

            paraMetros.Add(New ReportParameter("fechaInicial", UCase((CDate(txbFechaInicial.Text)).ToString("MMMM dd, yyyy"))))
            paraMetros.Add(New ReportParameter("fechaFinal", UCase((CDate(txbFechaFinal.Text)).ToString("MMMM dd, yyyy"))))
            paraMetros.Add(New ReportParameter("elnombrequetuquieras", listaNoLiberados.Count()))
            paraMetros.Add(New ReportParameter("creador", UCase(NombreUsuario)))
            paraMetros.Add(New ReportParameter("sistema", UCase(sistemaActivo.nombre)))
            paraMetros.Add(New ReportParameter("anio", sistemaActivo.año))
            Dim datasource As ReportDataSource = New ReportDataSource()

            Dim dtsReporte As New dtsSolGasComNoLib

            If listaNoLiberados.Count > 0 Then

                resultadoVacioReporte.Visible = False
                rptSolGasComNoLib.Visible = True
                For h = 0 To listaNoLiberados.Count - 1
                    num = num + 1
                    Dim a, b, c, d, ee, f As String
                    Dim g As Integer
                    a = listaNoLiberados(h).fechadeCaptura
                    b = listaNoLiberados(h).turnoSAF
                    c = listaNoLiberados(h).turnoDRM
                    d = listaNoLiberados(h).importeSolicitud
                    ee = UCase(listaNoLiberados(h).concepto)
                    f = listaNoLiberados(h).diasTranscurridos
                    g = num

                    If b Is Nothing Then
                        b = "No trae SAF"
                    End If
                    If c Is Nothing Then
                        c = "No trae DRM"
                    End If

                    If b Is "" Then
                        b = "No trae SAF"
                    End If

                    If c Is "" Then
                        c = "No trae DRM"
                    End If

                    dtsReporte.tblReporteJuve.AddtblReporteJuveRow(a, b, c, d, ee, f, g)
                Next

                datasource = New ReportDataSource("dataSet1", dtsReporte.tblReporteJuve.DefaultView)
                rptSolGasComNoLib.ProcessingMode = ProcessingMode.Local
                rptSolGasComNoLib.LocalReport.ReportPath = "management/solicitudGasto/reporteSolicitud/rpt/rptSolicitudGastoComprobacionNoLiberado.rdlc"
                rptSolGasComNoLib.LocalReport.SetParameters(paraMetros)
                rptSolGasComNoLib.LocalReport.DataSources.Add(datasource)
                rptSolGasComNoLib.LocalReport.Refresh()
                'updRep.Update()

            End If



        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub
End Class
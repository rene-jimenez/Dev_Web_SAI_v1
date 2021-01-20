Imports CES.nspImporteComprobacion
Imports CRN.nspImporteComprobacion, CRN.nspComprobacion, CRN.nspOficio, CRN.nspArea
Imports CES.nspSolicitudGasto, CRN.nspSolicitudGasto
Imports CRN.nspPartidaPresupuestal
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Imports CES.nspPopup
Public Class frmReporteDevolucion : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idComprobacion = Request.QueryString("idComp")
                Dim consultaComprobacion = New Proceso_ObtenerComprobacion() With {.id = Guid.Parse(idComprobacion)}.Ejecutar()
                Dim idOficio As Guid = consultaComprobacion.idOficio
                Dim datosOficio = New Proceso_ObtenerUnOficio With {.id = idOficio}.Ejecutar()
                Dim datosImporte = New Proceso_ObtenerImporteComprobacion() With {.tipoConsulta = tipoConsultaImporteComprobacion.idOficio, .idOficio = idOficio}.Ejecutar().FirstOrDefault
                Dim areas = New Proceso_ObtenerArea() With {.id = Guid.Parse(datosOficio.idArea.ToString)}.Ejecutar()
                Dim solicitud = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = tipoConsultaSolicitudGasto.idOficio, .idOficio = idOficio}.Ejecutar().FirstOrDefault
                Dim partida = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(solicitud.idPartidaPresupuestal.ToString)}.Ejecutar()
                Dim Conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
                Dim cadena = Conversor.dblToStrPesos(datosImporte.importeTotalPedido).ToString()
                Dim lista As New List(Of ReportParameter)
                lista.Add(New ReportParameter("drm", datosImporte.turnoDrm))
                lista.Add(New ReportParameter("saf", datosImporte.turnoSaf))
                lista.Add(New ReportParameter("claveArea", areas.codigo))
                lista.Add(New ReportParameter("area", datosOficio._area))
                lista.Add(New ReportParameter("cargoPresupuestal", datosImporte.CargoPresupuestal))
                lista.Add(New ReportParameter("partidaPresupuestal", solicitud._nombrePartidaPresupuestal))
                lista.Add(New ReportParameter("numeroPartida", partida.numero))
                lista.Add(New ReportParameter("importe", datosImporte.importeTotalPedido))
                lista.Add(New ReportParameter("tarjeta", datosImporte.folioTesoreriaAlcance + " " + datosImporte.folioTesoreriaSolicitud))
                lista.Add(New ReportParameter("caja", datosImporte.folioCajaAlcance + " " + datosImporte.folioCajaSolicitud))
                lista.Add(New ReportParameter("cadenaImporte", cadena))
                Dim dataSource As ReportDataSource = New ReportDataSource()
                rptDevolucion.LocalReport.DataSources.Clear()
                Dim dtsDevolucion As New dtsDevolucion
                Dim listaComprobacion = New Proceso_ObtenerComprobacion() With {.id = Guid.Parse(idComprobacion)}.Ejecutar()
                Dim concepto As String = listaComprobacion.concepto
                Dim autorizo As String = listaComprobacion._nombreAutoriza
                Dim fechaElabora As String = listaComprobacion.fechaElaboracion
                Dim responsable As String = listaComprobacion._nombreResponsable
                dtsDevolucion.tblDevolucion.AddtblDevolucionRow(concepto, autorizo, fechaElabora, responsable)
                dataSource = New ReportDataSource("dtsDevolucion", dtsDevolucion.tblDevolucion.DefaultView)
                rptDevolucion.ProcessingMode = ProcessingMode.Local
                rptDevolucion.LocalReport.ReportPath = "management/comprobacion/reporteDevolucion/rpt/rptDevolucion.rdlc"
                rptDevolucion.LocalReport.SetParameters(lista)
                rptDevolucion.LocalReport.DataSources.Add(dataSource)
                rptDevolucion.LocalReport.Refresh()
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
End Class
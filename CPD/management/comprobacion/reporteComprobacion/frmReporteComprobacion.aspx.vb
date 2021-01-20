Imports CES, CES.nspImporteComprobacion, CES.nspPedido, CES.nspComprobacion
Imports CRN, CRN.nspImporteComprobacion, CRN.nspPedido, CRN.nspComprobacion, CRN.nspOficio, CRN.nspArea
Imports CES.nspSolicitudGasto, CRN.nspSolicitudGasto
Imports CRN.nspPartidaPresupuestal
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms
Public Class frmReporteComprobacion : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim idComprobacion As Guid = Guid.Parse(Request.QueryString("idComp"))
            Dim consultaComprobacion = New Proceso_ObtenerComprobacion() With {.id = idComprobacion}.Ejecutar()
            Dim idOficio As Guid = consultaComprobacion.idOficio
            Dim datosOficio = New Proceso_ObtenerUnOficio() With {.id = idOficio}.Ejecutar()
            Dim datosImporte = New Proceso_ObtenerImporteComprobacion() With {.tipoConsulta = tipoConsultaImporteComprobacion.idOficio, .idOficio = idOficio}.Ejecutar().FirstOrDefault
            Dim pedidos = New Proceso_ObtenerPedidos() With {.tipoConsulta = tipoConsultaPedido.idOficio, .idOficio = idOficio}.Ejecutar().Where(Function(a) a.estatusPedido = True And a.idTipoPago = Guid.Parse("71747111-2222-3333-4444-111111111112")).ToList.OrderBy(Function(a) a.numeroPedido)
            Dim areas = New Proceso_ObtenerArea() With {.id = Guid.Parse(datosOficio.idArea.ToString)}.Ejecutar()
            Dim solicitud = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = tipoConsultaSolicitudGasto.idOficio, .idOficio = idOficio}.Ejecutar().FirstOrDefault
            Dim partida = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(solicitud.idPartidaPresupuestal.ToString)}.Ejecutar()
            Dim Conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
            Dim cadena = Conversor.dblToStrPesos(datosImporte.importeTotalPedido).ToString()
            Dim lista As New List(Of ReportParameter)
            lista.Add(New ReportParameter("drm", datosImporte.turnoDrm))
            lista.Add(New ReportParameter("saf", datosImporte.turnoSaf))
            lista.Add(New ReportParameter("tarjeta", datosImporte.folioTesoreriaAlcance))
            lista.Add(New ReportParameter("folioTesoreria", datosImporte.folioTesoreriaSolicitud))
            lista.Add(New ReportParameter("caja", datosImporte.folioCajaAlcance))
            lista.Add(New ReportParameter("cajaSolicitud", datosImporte.folioCajaSolicitud))
            lista.Add(New ReportParameter("cargoPresupuestal", datosImporte.CargoPresupuestal))
            lista.Add(New ReportParameter("importe", FormatNumber(datosImporte.importeTotalSolicitado, 2)))
            lista.Add(New ReportParameter("ejercido", FormatNumber(datosImporte.importeTotalPedido, 2)))
            lista.Add(New ReportParameter("devolucion", FormatNumber(datosImporte.importeDevolucion, 2)))
            lista.Add(New ReportParameter("cadenaImporte", cadena))
            lista.Add(New ReportParameter("partidaPresupuestal", solicitud._nombrePartidaPresupuestal))
            lista.Add(New ReportParameter("area", datosOficio._area))
            lista.Add(New ReportParameter("claveArea", areas.codigo))
            lista.Add(New ReportParameter("numeroPartida", partida.numero))
            lista.Add(New ReportParameter("nomUsuario", NombreUsuario))
            lista.Add(New ReportParameter("sistema", UCase(sistema.sistemaActivo.nombre) + " " + UCase(sistema.sistemaActivo.año.ToString)))
            Dim listaPedidos As String
            If pedidos.Count > 0 Then
                listaPedidos = pedidos(0).numeroPedido.ToString
                If pedidos.Count > 1 Then
                    For i = 1 To pedidos.Count - 1
                        listaPedidos = listaPedidos + " , " + pedidos(i).numeroPedido.ToString
                    Next
                    'Else
                    '    listaPedidos = datosPedido(0).numeroPedido.ToString
                End If
            Else
                listaPedidos = "No hay pedidos"
            End If

            Dim pedidosParametro As New List(Of ReportParameter)
            pedidosParametro.Add(New ReportParameter("numPedido", listaPedidos))
            Dim dataSource As ReportDataSource = New ReportDataSource()
            rptComprobacion.LocalReport.DataSources.Clear()
            Dim dtsComprobacion As New dtsComprobacion
            Dim listaComprobacion = New Proceso_ObtenerComprobaciones() With {.tipoConsulta = tipoConsultaComprobacion.id, .id = idComprobacion}.Ejecutar().ToList
            If listaComprobacion.Count > 0 Then
                For i = 0 To listaComprobacion.Count - 1
                    Dim concepto As String = listaComprobacion(i).concepto
                    Dim autorizo As String = listaComprobacion(i)._nombreAutoriza
                    Dim responsable As String = listaComprobacion(i)._nombreResponsable
                    Dim marcaAgua As String = listaComprobacion(i).marcaAgua
                    Dim fechaElabora As String = listaComprobacion(i).fechaElaboracion
                    dtsComprobacion.tblComprobacion.AddtblComprobacionRow(concepto, autorizo, marcaAgua, fechaElabora, responsable)
                Next
                dataSource = New ReportDataSource("dtsComprobacion", dtsComprobacion.tblComprobacion.DefaultView)
                rptComprobacion.ProcessingMode = ProcessingMode.Local
                rptComprobacion.LocalReport.ReportPath = "management/comprobacion/reporteComprobacion/rpt/rptComprobacion.rdlc"
                rptComprobacion.LocalReport.SetParameters(lista)
                rptComprobacion.LocalReport.SetParameters(pedidosParametro)
                rptComprobacion.LocalReport.DataSources.Add(dataSource)
                rptComprobacion.LocalReport.Refresh()
            End If
        End If
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
End Class
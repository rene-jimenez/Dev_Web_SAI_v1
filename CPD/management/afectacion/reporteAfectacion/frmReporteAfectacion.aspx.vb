Imports CES.nspAfectacionPresupuestal, CRN.nspAfectacionPresupuestal
Imports CES.nspArea, CRN.nspArea
Imports CES.nspPartidaPresupuestal, CRN.nspPartidaPresupuestal
Imports CES.nspSistema, CRN.nspSistema
Imports CES.nspPedido, CRN.nspPedido
Imports CES.nspDetallePedido, CRN.nspDetallePedido
Imports CES.nspFirma, CRN.nspFirma
Imports CES.nspOficio, CRN.nspOficio
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports Microsoft.Reporting.WebForms

Public Class frmReporteAfectacion : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensaje As New notificacionesDeUsuario
    Dim sistema As New nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sustitucion As String = ""
            If Request.QueryString("tipo") = "sust" Then
                Dim idPedido = Request.QueryString("idPedido")
                Dim consultaPedido = New Proceso_ObtenerPedido() With {.id = Guid.Parse(idPedido)}.Ejecutar()
                Dim datosOficio = New Proceso_ObtenerUnOficio() With {.id = consultaPedido.idOficio}.Ejecutar()
                Dim consultaAfectacion = New Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = tipoConsultaAfectacionPresupuestal.idPedido, .idPedido = Guid.Parse(idPedido)}.Ejecutar().FirstOrDefault()
                Dim autoriza = New Proceso_ObtenerFirma() With {.id = Guid.Parse(consultaAfectacion.idAutoriza.ToString)}.Ejecutar()
                Dim solicita = New Proceso_ObtenerFirma() With {.id = Guid.Parse(consultaAfectacion.idSolicita.ToString)}.Ejecutar()
                Dim area = New Proceso_ObtenerArea() With {.id = Guid.Parse(datosOficio.idArea.ToString)}.Ejecutar()
                Dim partidaPresupuestal = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(consultaPedido.idPartida.ToString)}.Ejecutar()
                Dim codigo As New List(Of ReportParameter)
                Dim responsable As New List(Of ReportParameter)
                Dim numero As New List(Of ReportParameter)
                Dim letra As New List(Of ReportParameter)
                Dim afectacion As New List(Of ReportParameter)
                Dim autor As New List(Of ReportParameter)
                Dim solict As New List(Of ReportParameter)
                Dim unPedido As New List(Of ReportParameter)
                Dim unOficio As New List(Of ReportParameter)
                Dim nombreSistema As New List(Of ReportParameter)
                Dim marca As New List(Of ReportParameter)
                Dim nombreUsuariox As New List(Of ReportParameter)
                Dim dataSource As ReportDataSource = New ReportDataSource()
                rptAfectacion.LocalReport.DataSources.Clear()
                Dim dtsAfectacion As New dtsAfectacion
                codigo.Add(New ReportParameter("claveArea", area.codigo))
                responsable.Add(New ReportParameter("responsable", consultaPedido._nombreProveedor))
                numero.Add(New ReportParameter("clavePartida", partidaPresupuestal.numero))
                afectacion.Add(New ReportParameter("conSustitucion", sustitucion))
                autor.Add(New ReportParameter("autoriza", UCase(autoriza._nombreUsuario)))
                solict.Add(New ReportParameter("elaboro", UCase(solicita._nombreUsuario)))
                unPedido.Add(New ReportParameter("pedido", consultaPedido.numeroPedido))
                unPedido.Add(New ReportParameter("partidaPresupuestal", UCase(consultaPedido._nombrePartida)))
                unOficio.Add(New ReportParameter("drm", datosOficio.turnoDRM))
                unOficio.Add(New ReportParameter("saf", datosOficio.turnoSAF))
                unOficio.Add(New ReportParameter("cargoPresupuestal", datosOficio._cargoPresupuestal))
                unOficio.Add(New ReportParameter("area", datosOficio._area))
                nombreSistema.Add(New ReportParameter("sistema", UCase(sistema.sistemaActivo.nombre) + " " + sistema.sistemaActivo.año.ToString))
                marca.Add(New ReportParameter("marcaAgua", UCase(consultaAfectacion.marcaAgua)))
                nombreUsuariox.Add(New ReportParameter("nombreUsuario", UCase(NombreUsuario.ToString)))
                Dim valorIVA = New CRN.nspIva.Proceso_ObtenerIva() With {.fecha = consultaPedido.fechaElaboracion}.Ejecutar

                sustitucion = "ESTE PEDIDO SUSTITUYE AL ANTERIOR POR UN IMPORTE DE " + consultaAfectacion.total.ToString("N2")
                Dim concepto As String = consultaAfectacion.concepto
                Dim fechaElabora As String = Date.Now
                Dim listaDetallePedido = New Proceso_ObtenerDetallePedidos() With {.tipoConsulta = tipoConsultaDetallePedido.idPedido, .idPedido = Guid.Parse(idPedido)}.Ejecutar().OrderBy(Function(d) d._numeroPedido).ToList
                Dim subtotal As String = FormatNumber(listaDetallePedido.Sum(Function(s) s._total), 2).ToString
                Dim Total As Double = 0
                Dim iva As Double = 0
                Dim descuento As Double = 0
                If consultaPedido.descuento <> "0" Then
                    descuento = FormatNumber(Double.Parse(consultaPedido.descuento), 2)
                End If
                If (descuento > 0) Then
                    If consultaPedido.iva Then
                        iva = FormatNumber((subtotal - descuento) * valorIVA, 2)
                    Else
                        iva = 0
                    End If
                Else
                    If consultaPedido.iva Then
                        iva = FormatNumber(subtotal * valorIVA, 2)
                    Else
                        iva = 0
                    End If
                End If
                Total = FormatNumber(subtotal - descuento + iva, 2)
                Dim Conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
                Dim cadena = Conversor.dblToStrPesos(subtotal - descuento + iva).ToString()
                letra.Add(New ReportParameter("importeCadena", cadena))
                dtsAfectacion.tblAfectacion.AddtblAfectacionRow(subtotal, descuento.ToString("N2"), iva.ToString("N2"), Total.ToString("N2"), FormatDateTime(fechaElabora, DateFormat.ShortDate), UCase(concepto))
                dataSource = New ReportDataSource("dtsAfectacion", dtsAfectacion.tblAfectacion.DefaultView)
                rptAfectacion.ProcessingMode = ProcessingMode.Local
                rptAfectacion.LocalReport.ReportPath = "management/afectacion/reporteAfectacion/rpt/rptAfectacion.rdlc"
                rptAfectacion.LocalReport.SetParameters(unPedido)
                rptAfectacion.LocalReport.SetParameters(responsable)
                rptAfectacion.LocalReport.SetParameters(autor)
                rptAfectacion.LocalReport.SetParameters(solict)
                rptAfectacion.LocalReport.SetParameters(unOficio)
                rptAfectacion.LocalReport.SetParameters(afectacion)
                rptAfectacion.LocalReport.SetParameters(nombreUsuariox)
                rptAfectacion.LocalReport.SetParameters(letra)
                rptAfectacion.LocalReport.SetParameters(marca)
                rptAfectacion.LocalReport.SetParameters(codigo)
                rptAfectacion.LocalReport.SetParameters(numero)
                rptAfectacion.LocalReport.SetParameters(nombreSistema)
                rptAfectacion.LocalReport.DataSources.Add(dataSource)
                rptAfectacion.LocalReport.Refresh()
            Else
                Dim idPedido = Request.QueryString("idPedido")
                Dim consultaPedido = New Proceso_ObtenerPedido() With {.id = Guid.Parse(idPedido)}.Ejecutar()
                Dim datosOficio = New Proceso_ObtenerUnOficio() With {.id = consultaPedido.idOficio}.Ejecutar()
                Dim consultaAfectacion = New Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = tipoConsultaAfectacionPresupuestal.idPedido, .idPedido = Guid.Parse(idPedido)}.Ejecutar().FirstOrDefault()
                Dim autoriza = New Proceso_ObtenerFirma() With {.id = Guid.Parse(consultaAfectacion.idAutoriza.ToString)}.Ejecutar()
                Dim solicita = New Proceso_ObtenerFirma() With {.id = Guid.Parse(consultaAfectacion.idSolicita.ToString)}.Ejecutar()

                Dim area = New Proceso_ObtenerArea() With {.id = Guid.Parse(datosOficio.idArea.ToString)}.Ejecutar()
                Dim partidaPresupuestal = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(consultaPedido.idPartida.ToString)}.Ejecutar()

                Dim Conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
                Dim cadena = Conversor.dblToStrPesos(consultaAfectacion.total).ToString()
                Dim codigo As New List(Of ReportParameter)
                Dim responsable As New List(Of ReportParameter)
                Dim numero As New List(Of ReportParameter)
                Dim letra As New List(Of ReportParameter)
                Dim afectacion As New List(Of ReportParameter)
                Dim autor As New List(Of ReportParameter)
                Dim solict As New List(Of ReportParameter)
                Dim unPedido As New List(Of ReportParameter)
                Dim unOficio As New List(Of ReportParameter)
                Dim nombreSistema As New List(Of ReportParameter)
                Dim marca As New List(Of ReportParameter)
                Dim nombreUsuariox As New List(Of ReportParameter)

                codigo.Add(New ReportParameter("claveArea", area.codigo))
                responsable.Add(New ReportParameter("responsable", consultaPedido._nombreProveedor))
                numero.Add(New ReportParameter("clavePartida", partidaPresupuestal.numero))

                afectacion.Add(New ReportParameter("conSustitucion", sustitucion))
                autor.Add(New ReportParameter("autoriza", UCase(autoriza._nombreUsuario)))
                solict.Add(New ReportParameter("elaboro", UCase(solicita._nombreUsuario)))
                unPedido.Add(New ReportParameter("pedido", consultaPedido.numeroPedido))
                unPedido.Add(New ReportParameter("partidaPresupuestal", UCase(consultaPedido._nombrePartida)))
                unOficio.Add(New ReportParameter("drm", datosOficio.turnoDRM))
                unOficio.Add(New ReportParameter("saf", datosOficio.turnoSAF))
                unOficio.Add(New ReportParameter("cargoPresupuestal", datosOficio._cargoPresupuestal))
                unOficio.Add(New ReportParameter("area", datosOficio._area))
                nombreSistema.Add(New ReportParameter("sistema", UCase(sistema.sistemaActivo.nombre) + " " + sistema.sistemaActivo.año.ToString))
                marca.Add(New ReportParameter("marcaAgua", UCase(consultaAfectacion.marcaAgua)))
                nombreUsuariox.Add(New ReportParameter("nombreUsuario", UCase(NombreUsuario.ToString)))
                letra.Add(New ReportParameter("importeCadena", cadena))
                Dim dataSource As ReportDataSource = New ReportDataSource()
                rptAfectacion.LocalReport.DataSources.Clear()
                Dim dtsAfectacion As New dtsAfectacion
                Dim listaAfectacion = New Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = tipoConsultaAfectacionPresupuestal.idPedido, .idPedido = Guid.Parse(idPedido)}.Ejecutar().ToList
                If listaAfectacion.Count > 0 Then
                    For i = 0 To listaAfectacion.Count - 1
                        Dim subtotal As String = FormatNumber(listaAfectacion(i).subtotal, 2)
                        Dim descuento As String = FormatNumber(listaAfectacion(i).descuento, 2)
                        Dim iva As String = FormatNumber(listaAfectacion(i).iva, 2)
                        Dim total As String = FormatNumber(listaAfectacion(i).total, 2)
                        Dim fechaElabora As String = listaAfectacion(i).fechaElaboracion
                        Dim concepto As String = listaAfectacion(i).concepto
                        dtsAfectacion.tblAfectacion.AddtblAfectacionRow(subtotal, descuento, iva, total, fechaElabora, UCase(concepto))
                    Next
                    dataSource = New ReportDataSource("dtsAfectacion", dtsAfectacion.tblAfectacion.DefaultView)
                    rptAfectacion.ProcessingMode = ProcessingMode.Local
                    rptAfectacion.LocalReport.ReportPath = "management/afectacion/reporteAfectacion/rpt/rptAfectacion.rdlc"
                    rptAfectacion.LocalReport.SetParameters(unPedido)
                    rptAfectacion.LocalReport.SetParameters(responsable)
                    rptAfectacion.LocalReport.SetParameters(autor)
                    rptAfectacion.LocalReport.SetParameters(solict)
                    rptAfectacion.LocalReport.SetParameters(unOficio)
                    rptAfectacion.LocalReport.SetParameters(afectacion)
                    rptAfectacion.LocalReport.SetParameters(nombreUsuariox)
                    rptAfectacion.LocalReport.SetParameters(letra)
                    rptAfectacion.LocalReport.SetParameters(marca)
                    rptAfectacion.LocalReport.SetParameters(codigo)
                    rptAfectacion.LocalReport.SetParameters(numero)
                    rptAfectacion.LocalReport.SetParameters(nombreSistema)
                    rptAfectacion.LocalReport.DataSources.Add(dataSource)
                    rptAfectacion.LocalReport.Refresh()
                End If
            End If
        End If
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
End Class
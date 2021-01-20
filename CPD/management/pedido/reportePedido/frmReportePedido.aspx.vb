Imports CES.nspProveedor, CRN.nspProveedor, CES.nspTelefonoProveedor, CRN.nspTelefonoProveedor
Imports CES.nspOficio, CRN.nspOficio
Imports CES.nspPedido, CRN.nspPedido
Imports CES.nspFirma, CRN.nspFirma
Imports CES.nspIva, CRN.nspIva
Imports CES.nspDetallePedido, CRN.nspDetallePedido
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup
Imports Microsoft.Reporting.WebForms
Public Class frmReportePedido : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensaje As New notificacionesDeUsuario
    Dim sistema As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idPed = Request.QueryString("idPedido")
                Dim unpedido = New Proceso_ObtenerPedido() With {.id = Guid.Parse(idPed)}.Ejecutar()
                Dim unOficio = New Proceso_ObtenerUnOficio() With {.id = unpedido.idOficio}.Ejecutar()
                Dim unProveedor = New Proceso_ObtenerProveedor() With {.id = unpedido.idProveedor}.Ejecutar()
                Dim elaboro = New Proceso_ObtenerFirma() With {.id = Guid.Parse(unpedido.idElaboro.ToString)}.Ejecutar()
                Dim reviso = New Proceso_ObtenerFirma() With {.id = Guid.Parse(unpedido.idReviso.ToString)}.Ejecutar()
                Dim autoriza = New Proceso_ObtenerFirma() With {.id = Guid.Parse(unpedido.idAutoriza.ToString)}.Ejecutar()
                Dim telProveedor = New Proceso_ObtenerTelefonosProveedor() With {.tipoConsulta = tipoConsultaTelefonoProveedor.idProveedor, .idProveedor = Guid.Parse(unProveedor.id.ToString)}.Ejecutar()
                Dim listaReportePedido = New Proceso_ObtenerDetallePedidos() With {.tipoConsulta = tipoConsultaDetallePedido.idPedido, .idPedido = Guid.Parse(idPed)}.Ejecutar().OrderBy(Function(d) d._numeroPedido).ToList
                Dim ivva = New Proceso_ObtenerIva() With {.fecha = DateTime.Today}.Ejecutar()

                Dim paraTurno As New List(Of ReportParameter)
                paraTurno.Add(New ReportParameter("cargoPresupuestal", UCase(unOficio._cargoPresupuestal)))
                paraTurno.Add(New ReportParameter("turno", unOficio.turnoDRM))

                Dim listaParametros As New List(Of ReportParameter)
                listaParametros.Add(New ReportParameter("proveedor", UCase(unpedido._nombreProveedor)))
                listaParametros.Add(New ReportParameter("numPedido", unpedido.numeroPedido))
                listaParametros.Add(New ReportParameter("fechaElaboro", UCase(unpedido.fechaElaboracion)))
                listaParametros.Add(New ReportParameter("fechaSolicitud", UCase(unpedido.fechaRequerida)))
                listaParametros.Add(New ReportParameter("fechaAcordada", UCase(unpedido.fechaAcordada)))
                listaParametros.Add(New ReportParameter("fechaRecibido", unpedido.fechaRecibido))
                listaParametros.Add(New ReportParameter("observaciones", UCase(unpedido.observaciones)))
                listaParametros.Add(New ReportParameter("nomUsuario", UCase(NombreUsuario)))

                Dim paraProveedor As New List(Of ReportParameter)
                paraProveedor.Add(New ReportParameter("CP", UCase(unProveedor.codigoPostal)))
                paraProveedor.Add(New ReportParameter("colonia", UCase(unProveedor.colonia)))
                paraProveedor.Add(New ReportParameter("ciudad", UCase(unProveedor.ciudad)))

                Dim listaTelefono As String = ""
                For i = 0 To telProveedor.Count - 1
                    listaTelefono = listaTelefono + "  " + telProveedor(i).numero.ToString
                Next
                Dim telefonos As New List(Of ReportParameter)
                telefonos.Add(New ReportParameter("telefono", listaTelefono))

                Dim nomElaboro As New List(Of ReportParameter)
                nomElaboro.Add(New ReportParameter("elaboro", UCase(elaboro._nombreUsuario)))
                Dim nomReviso As New List(Of ReportParameter)
                nomReviso.Add(New ReportParameter("reviso", UCase(reviso._nombreUsuario)))
                Dim nomAutoriza As New List(Of ReportParameter)
                nomAutoriza.Add(New ReportParameter("autoriza", UCase(autoriza._nombreUsuario)))

                Dim nombreSistema As New List(Of ReportParameter)
                nombreSistema.Add(New ReportParameter("sistema", UCase(sistema.sistemaActivo.nombre) + " " + UCase(sistema.sistemaActivo.año.ToString)))

                Dim pSubtotal As Double = 0
                Dim pDescuento As Double = 0
                Dim pTIva As Double = 0
                Dim pTG As Double = 0

                If unpedido.iva = True Then
                    If (listaReportePedido.Count() > 0) Then
                        pSubtotal = listaReportePedido.Sum(Function(s) s._total)
                        If unpedido.descuento <> "0" Then
                            pDescuento = (Double.Parse(unpedido.descuento))
                        End If
                        If (pDescuento > 0) Then
                            pTIva = (pSubtotal - pDescuento) * ivva
                        Else
                            pTIva = pSubtotal * ivva
                        End If
                    End If
                ElseIf unpedido.iva = False Then

                    If (listaReportePedido.Count() > 0) Then
                        pSubtotal = listaReportePedido.Sum(Function(s) s._total)
                        If unpedido.descuento <> "0" Then
                            pDescuento = (Double.Parse(unpedido.descuento))
                        End If
                    End If
                End If
                pTG = pSubtotal - pDescuento + pTIva



                Dim cantidades As New List(Of ReportParameter)
                cantidades.Add(New ReportParameter("subTotal", pSubtotal.ToString("N2")))
                cantidades.Add(New ReportParameter("descuento", pDescuento.ToString("N2")))
                If pTIva < 0 Then
                    pTIva = (-1) * pTIva
                    cantidades.Add(New ReportParameter("iva", "-" + pTIva.ToString("N2")))
                Else
                    cantidades.Add(New ReportParameter("iva", pTIva.ToString("N2")))
                End If

                If pTG < 0 Then
                    pTG = (-1) * pTG
                    cantidades.Add(New ReportParameter("total2", "-" + pTG.ToString("N2")))
                Else
                    cantidades.Add(New ReportParameter("total2", pTG.ToString("N2")))
                End If

                Dim Conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
            Dim cadena = Conversor.dblToStrPesos(pTG).ToString()

            Dim letra As New List(Of ReportParameter)
            letra.Add(New ReportParameter("cantidadLetra", cadena))

            Dim dataSource As ReportDataSource = New ReportDataSource()
            rptPedido.LocalReport.DataSources.Clear()
            Dim dtsPedido As New dtsPedido
            'Dim listaReportePedido = New Proceso_ObtenerDetallePedidos() With {.tipoConsulta = tipoConsultaDetallePedido.idPedido, .idPedido = Guid.Parse(idPed)}.Ejecutar().OrderBy(Function(d) d._numeroPedido).ToList
            If listaReportePedido.Count > 0 Then
                For i = 0 To listaReportePedido.Count - 1
                    Dim cantidad As String = listaReportePedido(i).cantidad
                        Dim descripcion As String = UCase(listaReportePedido(i)._articulo)
                        Dim precioUni As String = "$ " + listaReportePedido(i).precioUnitario.ToString("N6")
                        Dim total As String = "$ " + listaReportePedido(i)._total.ToString("N2")
                        dtsPedido.tblReporteSolicitud.AddtblReporteSolicitudRow(cantidad, descripcion, precioUni, total)

                Next
                dataSource = New ReportDataSource("dtsPedido", dtsPedido.tblReporteSolicitud.DefaultView)
                rptPedido.ProcessingMode = ProcessingMode.Local
                rptPedido.LocalReport.ReportPath = "management/pedido/reportePedido/rpt/rptPedido.rdlc"
                rptPedido.LocalReport.SetParameters(paraTurno)
                rptPedido.LocalReport.SetParameters(listaParametros)
                rptPedido.LocalReport.SetParameters(paraProveedor)
                rptPedido.LocalReport.SetParameters(telefonos)
                rptPedido.LocalReport.SetParameters(nomElaboro)
                rptPedido.LocalReport.SetParameters(nomReviso)
                rptPedido.LocalReport.SetParameters(nomAutoriza)
                rptPedido.LocalReport.SetParameters(cantidades)
                rptPedido.LocalReport.SetParameters(letra)
                rptPedido.LocalReport.SetParameters(nombreSistema)
                rptPedido.LocalReport.DataSources.Add(dataSource)
                rptPedido.LocalReport.Refresh()
            End If


            Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If

    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
End Class
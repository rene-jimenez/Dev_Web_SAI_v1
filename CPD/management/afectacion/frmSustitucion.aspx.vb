Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup
Imports CRN.nspAfectacionPresupuestal, CRN.nspOficio, CRN.nspRubroRequerimiento, CRN.nspPartidaPresupuestal, CRN.nspFirma, CRN.nspProveedor, CRN.nspTipoPago, CES.nspDetallePedido, CRN.nspDetallePedido, CRN.nspUnidadMedida, CRN.nspArticulo, CES.nspArticulo
Public Class frmSustitucion
    Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarDatos()
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub

    Public Sub llenarDatos()
        Dim idPedido As String = Request.QueryString("id")
        Dim consultaPedido = New CRN.nspPedido.Proceso_ObtenerPedido() With {.id = Guid.Parse(idPedido)}.Ejecutar()
        txbNumPedido.Text = consultaPedido.numeroPedido.ToString
        txbPartida.Text = consultaPedido._nombrePartida.ToString
        txbProveedor.Text = consultaPedido._nombreProveedor.ToString

        Dim datosOficio = New CRN.nspOficio.Proceso_ObtenerUnOficio() With {.id = consultaPedido.idOficio}.Ejecutar()
        If Not datosOficio.turnoSAF Is Nothing Then
            txbTurnoSAF.Text = datosOficio.turnoSAF.ToString
        End If
        txbTurnoDRM_.Text = datosOficio.turnoDRM.ToString
        txbAreaSolicitante.Text = datosOficio._area.ToString
        txbCargoPresupuestal.Text = datosOficio._cargoPresupuestal.ToString


        Dim valorIVA = New CRN.nspIva.Proceso_ObtenerIva() With {.fecha = consultaPedido.fechaElaboracion}.Ejecutar
        Dim listaDetallePedido = New Proceso_ObtenerDetallePedidos() With {.tipoConsulta = tipoConsultaDetallePedido.idPedido, .idPedido = Guid.Parse(idPedido)}.Ejecutar().OrderBy(Function(d) d._numeroPedido).ToList
        Dim consultaAfectacion = New Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = CES.nspAfectacionPresupuestal.tipoConsultaAfectacionPresupuestal.idPedido, .idPedido = Guid.Parse(idPedido)}.Ejecutar()
        txbConcepto.Text = consultaAfectacion(0).concepto.ToString
        txbMarcaAgua.Text = consultaAfectacion(0).marcaAgua.ToString

        Dim consultaSolicitante = New CRN.nspFirma.Proceso_ObtenerFirma() With {.id = consultaAfectacion(0).idSolicita}.Ejecutar()
        lblSolicita.Text = consultaSolicitante._nombreUsuario

        Dim consultaAutoriza = New CRN.nspFirma.Proceso_ObtenerFirma() With {.id = consultaAfectacion(0).idAutoriza}.Ejecutar()
        lblAutoriza.Text = consultaAutoriza._nombreUsuario
        If consultaPedido.iva Then
            If (listaDetallePedido.Count() > 0) Then
                Dim Total As Double = 0
                Dim subTotal As Double = 0
                Dim iva As Double = 0
                Dim descuento As Double = 0
                subTotal = listaDetallePedido.Sum(Function(s) s._total)
                If consultaPedido.descuento <> "0" Then
                    descuento = (Double.Parse(consultaPedido.descuento))
                End If
                If (descuento > 0) Then
                    iva = (subTotal - descuento) * valorIVA
                Else
                    iva = subTotal * valorIVA
                End If
                Total = subTotal - descuento + iva
                lnkSubtotal.Text = "Subtotal: $" + subTotal.ToString("N2")
                lnkDescuento.Text = "-Descuento: $" + descuento.ToString("N2")
                lnkIva.Text = "+Iva: " + iva.ToString + "%"
                lnkTotal.Text = "Total: $" + Total.ToString("N2")
                txbImportePago.Text = "$" + Total.ToString("N2")
            End If
        Else
            If (listaDetallePedido.Count() > 0) Then
                Dim Total As Double = 0
                Dim subTotal As Double = 0
                Dim descuento As Double = 0
                subTotal = listaDetallePedido.Sum(Function(s) s._total)
                If consultaPedido.descuento <> "0" Then
                    descuento = (Double.Parse(consultaPedido.descuento))
                End If
                Total = subTotal - descuento
                lnkSubtotal.Text = "Subtotal: $" + subTotal.ToString("N2")
                lnkDescuento.Text = "-Descuento: $" + descuento.ToString("N2")
                lnkIva.Text = "+Iva: 0 " + "%"
                lnkTotal.Text = "Total: $" + Total.ToString("N2")
                txbImportePago.Text = "$" + Total.ToString("N2")
            End If

        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Response.Redirect("~/management/afectacion/reporteAfectacion/frmReporteAfectacion.aspx?idPedido=" + Request.QueryString("id") + "&tipo=sust")
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
End Class
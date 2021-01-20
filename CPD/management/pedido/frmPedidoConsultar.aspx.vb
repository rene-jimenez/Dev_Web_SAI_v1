Imports CRN.nspArea, CRN.nspPedido, CRN.nspOficio, CRN.nspRubroRequerimiento, CRN.nspPartidaPresupuestal, CRN.nspFirma, CRN.nspProveedor, CRN.nspTipoPago, CES.nspDetallePedido, CRN.nspDetallePedido, CRN.nspUnidadMedida, CRN.nspArticulo, CES.nspArticulo
Imports CES
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmPedidoConsultar : Inherits paginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarListaPedido()

            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "Funciones"
    Protected Function controladorOperacioneslineales(axn As String)
        lineaSubTotal.Visible = True
        lineaIva.Visible = False
        lineaDescuento.Visible = False
        lineaGranTotal.Visible = False
        Select Case axn

            Case "C1"
                lineaGranTotal.Visible = True
            Case "C2"
                lineaIva.Visible = True
                lineaGranTotal.Visible = True
            Case "C3"
                lineaDescuento.Visible = True
                lineaGranTotal.Visible = True
            Case "C4"
                lineaIva.Visible = True
                lineaDescuento.Visible = True
                lineaGranTotal.Visible = True
            Case "CT"
                lineaSubTotal.Visible = False
                lineaGranTotal.Visible = True
            Case Else
                lineaSubTotal.Visible = False
                lineaIva.Visible = False
                lineaDescuento.Visible = False
                lineaGranTotal.Visible = False
        End Select
        Return axn
    End Function
#End Region
#Region "Metodos"
    Protected Sub llenarListaPedido()
        Dim idx As String = Request.QueryString("idPedido")
        Dim unpedido = New Proceso_ObtenerPedido() With {.id = Guid.Parse(idx)}.Ejecutar()
        Dim unOficio = New Proceso_ObtenerUnOficio() With {.id = unpedido.idOficio}.Ejecutar()
        lblTurnoDRM.Text = "Turno DRM: " + unOficio.turnoDRM

        lblArea.Text = "Área: " + unOficio._area
        lblAutorizo.Text = "Autorizó: " + unpedido._nombreAutoriza
        lblCargoPresupuestal.Text = "Cargo presupuestal: " + unOficio._cargoPresupuestal
        lblElaboro.Text = "Elaboró: " + unpedido._nombreElaboro
        lblFechaAcordada.Text = "Fec. acordada: " + CDate(unpedido.fechaAcordada).ToString("dd/MM/yyyy")
        lblFechaElaboracion.Text = "Fec. de elaboración: " + CDate(unpedido.fechaElaboracion).ToString("dd/MM/yyyy")
        lblFElab.Text = "Fec. de elaboración: " + CDate(unpedido.fechaElaboracion).ToString("dd/MM/yyyy")
        lblFechaRecibida.Text = "Fec. recibida: " + CDate(unpedido.fechaRecibido).ToString("dd/MM/yyyy")
        lblFechaSolicitud.Text = "Fec. de solicitud: " + CDate(unpedido.fechaRequerida).ToString("dd/MM/yyyy")
        lblObservaciones.Text = "Observaciones: " + unpedido.observaciones
        lblPartidaPresupuestal.Text = "Partida presupuestal: " + unpedido._nombrePartida
        lblProveedor.Text = "Proveedor: " + unpedido._nombreProveedor
        lblReviso.Text = "Revisó: " + unpedido._nombreReviso
        lblTipoPago.Text = "Tipo pago: " + unpedido._nombreTipoPago
        chkPedido.Checked = unpedido.estatusPedido
        chkVerAlmacen.Checked = unpedido.verAlmacen
        Dim listaDetallePedido = New Proceso_ObtenerDetallePedidos() With {.tipoConsulta = tipoConsultaDetallePedido.idPedido, .idPedido = Guid.Parse(idx)}.Ejecutar().OrderBy(Function(d) d._numeroPedido).ToList
        lvsListado.DataSource = listaDetallePedido
        lvsListado.DataBind()
        'Dim sumatotal = listaDetallePedido.Sum(Function(s) s._total)
        'lblSubTotal.Text = sumatotal.ToString
        lblPedido.Text = unpedido.numeroPedido
        lblSaf.Text = unOficio.turnoSAF

        Dim iiva As Boolean
        Dim JgTtl As Double = 0
        Dim JsTtl As Double = 0
        Dim JtTlIv As Double = 0
        Dim JtTlDsct As Double = 0
        Dim valorIVA = New CRN.nspIva.Proceso_ObtenerIva() With {.fecha = unpedido.fechaElaboracion}.Ejecutar

        If unpedido.iva Then
                iiva = True



        Else
                iiva = False

        End If
            Dim ddesc As Boolean
            If unpedido.descuento = "0" Then
                ddesc = False

        Else
                ddesc = True

        End If
        If iiva = True Then
            If (listaDetallePedido.Count() > 0) Then
                JsTtl = listaDetallePedido.Sum(Function(s) s._total)
                If unpedido.descuento <> "0" Then
                    JtTlDsct = (Double.Parse(unpedido.descuento))
                End If
                If (JtTlDsct > 0) Then
                    controladorOperacioneslineales("C4")
                    JtTlIv = (JsTtl - JtTlDsct) * valorIVA
                Else
                    JtTlIv = JsTtl * valorIVA
                    controladorOperacioneslineales("C2")
                End If
                JgTtl = JsTtl - JtTlDsct + JtTlIv
                lblDescuento.Text = "$" + (JtTlDsct.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblIva.Text = "$" + (JtTlIv.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblGranTotal.Text = "$" + (JgTtl.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblSubTotal.Text = "$" + (JsTtl.ToString("0,0.00", CultureInfo.InvariantCulture))
            End If

        Else
            If (listaDetallePedido.Count() > 0) Then
                JsTtl = listaDetallePedido.Sum(Function(s) s._total)
                If unpedido.descuento <> "0" Then
                    JtTlDsct = (Double.Parse(unpedido.descuento))
                    controladorOperacioneslineales("C3")
                Else
                    controladorOperacioneslineales("CT")
                End If
                JgTtl = JsTtl - JtTlDsct
                lblDescuento.Text = "$" + (JtTlDsct.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblIva.Text = "$" + (JtTlIv.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblGranTotal.Text = "$" + (JgTtl.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblSubTotal.Text = "$" + (JsTtl.ToString("0,0.00", CultureInfo.InvariantCulture))
            End If

        End If

    End Sub



#End Region
#Region "Botones"
    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Response.Redirect("~/management/pedido/reportePedido/frmReportePedido.aspx?idPedido=" + Request.QueryString("idPedido"))
    End Sub
#End Region
End Class
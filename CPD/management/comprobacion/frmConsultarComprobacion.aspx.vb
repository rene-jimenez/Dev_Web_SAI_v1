Imports CES.nspImporteComprobacion, CES.nspPedido
Imports CRN.nspImporteComprobacion, CRN.nspPedido, CRN.nspComprobacion
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup
Public Class frmConsultarComprobacion : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idComprobacion = Request.QueryString("idComp")
                poblarCampos(Guid.Parse(idComprobacion))
            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "Metodos"
    Public Sub poblarCampos(id As Guid)
        Dim consultaComprobacion = New Proceso_ObtenerComprobacion() With {.id = id}.Ejecutar()
        lblConcepto.Text = consultaComprobacion.concepto.ToString
        lblMarcaAgua.Text = consultaComprobacion.marcaAgua.ToString
        lblAutoriza.Text = consultaComprobacion._nombreAutoriza.ToString
        lblResponsable.Text = consultaComprobacion._nombreResponsable.ToString
        Dim consulta = New Proceso_ObtenerImporteComprobacion() With {.tipoConsulta = tipoConsultaImporteComprobacion.idOficio, .idOficio = consultaComprobacion.idOficio}.Ejecutar()
        lblDRM.Text = consulta(0).turnoDrm.ToString
        lblSAF.Text = consulta(0).turnoSaf.ToString
        lblFolioTesoreriaSolicitud.Text = consulta(0).folioTesoreriaSolicitud.ToString
        lblFolioCajaSolicitud.Text = consulta(0).folioCajaSolicitud.ToString
        If Not consulta(0).folioTesoreriaAlcance Is Nothing Then
            lblFolioTesoreriaAlcance.Text = consulta(0).folioTesoreriaAlcance.ToString
        Else
            lblFolioTesoreriaAlcance.Text = "Sin alcance"
        End If
        If Not consulta(0).folioCajaAlcance Is Nothing Then
            lblFolioCajaAlcance.Text = consulta(0).folioCajaAlcance.ToString
        Else
            lblFolioCajaAlcance.Text = "Sin alcance"
        End If
        lblImporte.Text = "$ " + consulta(0).importeTotalPedido.ToString
        lblEjercido.Text = "$ " + consulta(0).importeTotalSolicitado.ToString
        lblDevolucion.Text = "$ " + consulta(0).importeDevolucion.ToString
        lblCargoPresupuestal.Text = consulta(0).CargoPresupuestal.ToString
        Dim pedidos = New Proceso_ObtenerPedidos() With {.tipoConsulta = tipoConsultaPedido.idOficio, .idOficio = consultaComprobacion.idOficio}.Ejecutar().Where(Function(a) a.estatusPedido = True And a.idTipoPago = Guid.Parse("71747111-2222-3333-4444-111111111112")).ToList.OrderBy(Function(a) a.numeroPedido)
        If pedidos.Count > 0 Then
            lblPedido1.Text = pedidos(0).numeroPedido.ToString
            Dim pedidosExtras As String = lblPedido1.Text
            If pedidos.Count > 1 Then
                For i = 1 To pedidos.Count - 1
                    pedidosExtras = pedidosExtras + " , " + pedidos(i).numeroPedido.ToString
                Next
                lblPedido1.Text = pedidosExtras.TrimEnd(",")
            End If
        Else
            lblPedido1.Text = "Sin pedido"
        End If
    End Sub


    Private Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function
#End Region

#Region "Botones"
    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim idComprobacion = Request.QueryString("idComp")
        Response.Redirect("reporteComprobacion/frmReporteComprobacion.aspx?idComp=" + idComprobacion.ToString)
    End Sub
#End Region
End Class
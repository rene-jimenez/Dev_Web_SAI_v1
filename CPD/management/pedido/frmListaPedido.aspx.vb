Imports CRN.nspArea, CRN.nspPedido, CRN.nspOficio, CRN.nspRubroRequerimiento, CRN.nspPartidaPresupuestal, CRN.nspFirma, CRN.nspProveedor, CRN.nspTipoPago, CES.nspDetallePedido, CRN.nspDetallePedido, CRN.nspUnidadMedida, CRN.nspArticulo, CES.nspArticulo
Imports CES
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI, System.Data
Imports System.Globalization
Public Class frmListaPedido : Inherits paginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Request.QueryString("Band") = "0" Or Request.QueryString("Band") = "1" Then

            Else
                mandaDefault()

            End If
            llenaLista()
        End If

    End Sub
#Region "Funciones"

#End Region
#Region "Metodos"
    Protected Sub consultas()
        Dim idx As String = Request.QueryString("idOficio")
        Dim qwe = New Proceso_ObtenerUnOficio With {.id = Guid.Parse(idx)}.Ejecutar()
        Dim cargo = New Proceso_ObtenerAreas() With {.tipoConsulta = nspArea.tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList

    End Sub
    Protected Sub llenaLista()
        Dim idx As String = Request.QueryString("idOficio")
        Dim llenado = New Proceso_ObtenerPedidos() With {.tipoConsulta = nspPedido.tipoConsultaPedido.idOficio, .idOficio = Guid.Parse(idx)}.Ejecutar().OrderBy(Function(f) f.numeroPedido).ToList
        lsvPedidos.DataSource = llenado
        lsvPedidos.DataBind()



    End Sub

#End Region
#Region "Botones"
    Protected Sub lnkVer_Click(sender As Object, e As EventArgs)
        Dim clic As LinkButton = sender
        Dim idPedido As Guid = Guid.Parse(clic.CommandName)
        Response.Redirect("~/management/pedido/frmPedidoConsultar.aspx?idPedido=" + idPedido.ToString)
    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub

    Protected Sub lnkEditar_Click(sender As Object, e As EventArgs)
        Dim clic As LinkButton = sender
        Dim idEditar As Guid = Guid.Parse(clic.CommandName)
        Response.Redirect("~/management/pedido/frmPedidoEditar.aspx?id=" + idEditar.ToString)
    End Sub
#End Region
End Class
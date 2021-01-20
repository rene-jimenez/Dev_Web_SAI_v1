Imports CRN.nspArea, CRN.nspPedido, CRN.nspOficio, CRN.nspRubroRequerimiento, CRN.nspPartidaPresupuestal, CRN.nspFirma, CRN.nspProveedor, CRN.nspTipoPago, CES.nspDetallePedido, CRN.nspDetallePedido, CRN.nspUnidadMedida, CRN.nspArticulo, CES.nspArticulo
Imports CES
Imports CES.nspPopup
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmListaPedidosAfectacion : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try

                Dim tipo As String = Request.QueryString("band")
                llenaLista(tipo)
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
    Protected Sub btnSeleccionar_OnClick(sender As Object, e As EventArgs)
        Try
            Dim btnSeleccionar As LinkButton = sender
            Dim idPedido As Guid = Guid.Parse(btnSeleccionar.CommandArgument)
            Dim accion = Request.QueryString("band")
            Select Case accion
                Case "add"
                    Response.Redirect("~/management/afectacion/frmAgregarAfectacion.aspx?idPedido=" + idPedido.ToString)
                Case "edt"
                    Dim idAfectacion = New CRN.nspAfectacionPresupuestal.Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = nspAfectacionPresupuestal.tipoConsultaAfectacionPresupuestal.idPedido, .idPedido = idPedido}.Ejecutar.FirstOrDefault.id
                    Response.Redirect("~/management/afectacion/frmModificarAfectacion.aspx?idAfectacion=" + idAfectacion.ToString)
                Case "cst"
                    Response.Redirect("~/management/afectacion/frmConsultarAfectacion.aspx?idPedido=" + idPedido.ToString)
                Case "stc"
                    Response.Redirect("~/management/afectacion/frmSustitucion.aspx?id=" + idPedido.ToString)

            End Select
        Catch ex As Exception

        End Try

    End Sub
#Region "Metodos"
    Protected Sub consultas()
        Dim idx As String = Request.QueryString("idOficio")
        Dim qwe = New Proceso_ObtenerUnOficio() With {.id = Guid.Parse(idx)}.Ejecutar()
        Dim cargo = New Proceso_ObtenerAreas() With {.tipoConsulta = nspArea.tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
    End Sub
    Protected Sub llenaLista(tipo As String)
        Dim idx As String = Request.QueryString("idOficio")
        Select Case tipo
            Case "add"
                Dim listaPedidos = New Proceso_ObtenerPedidos() With {.tipoConsulta = nspPedido.tipoConsultaPedido.Sin_afectacion, .idOficio = Guid.Parse(idx)}.Ejecutar().ToList
                lsvPedidos.DataSource = listaPedidos.Where(Function(a) a._nombreTipoPago = "Afectacion Presupuestaria")
                lsvPedidos.DataBind()
            Case "edt", "cst"
                Dim listaPedidos = New Proceso_ObtenerPedidos() With {.tipoConsulta = nspPedido.tipoConsultaPedido.con_Afectacion, .idOficio = Guid.Parse(idx)}.Ejecutar().ToList.Where(Function(a) a._nombreTipoPago = "Afectacion Presupuestaria")
                lsvPedidos.DataSource = listaPedidos
                lsvPedidos.DataBind()
            Case "stc"
                Dim listaSustitucion As New List(Of nspPedido.pedido)
                Dim listaPedidos = New Proceso_ObtenerPedidos() With {.tipoConsulta = nspPedido.tipoConsultaPedido.con_Afectacion, .idOficio = Guid.Parse(idx)}.Ejecutar().ToList.Where(Function(a) a._nombreTipoPago = "Afectacion Presupuestaria")

                For i = 0 To listaPedidos.Count - 1
                    Dim totalPedido As Decimal = 0
                    '                    Dim iva As Decimal = 0
                    Dim descuento As Decimal = 0
                    Dim subtotal As Decimal = 0
                    Dim listaDetallePedido = New CRN.nspDetallePedido.Proceso_ObtenerDetallePedidos() With {.tipoConsulta = tipoConsultaDetallePedido.idPedido, .idPedido = listaPedidos(i).id}.Ejecutar
                    For j = 0 To listaDetallePedido.Count - 1
                        subtotal += listaDetallePedido(j).precioUnitario * listaDetallePedido(j).cantidad
                    Next
                    descuento = listaPedidos(i).descuento
                    ' iva = New CRN.nspIva.Proceso_ObtenerIvas() With {.tipoConsulta = nspIva.tipoConsultaIva.fechaEntradaVigor}.Ejecutar.FirstOrDefault.nombre
                    If listaPedidos(i).iva Then
                        totalPedido = (subtotal - descuento) * 1.16
                    Else
                        totalPedido = (subtotal - descuento)
                    End If

                    Dim totalAfectacion = New CRN.nspAfectacionPresupuestal.Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = nspAfectacionPresupuestal.tipoConsultaAfectacionPresupuestal.idPedido, .idPedido = listaPedidos(i).id}.Ejecutar.FirstOrDefault.total

                    If totalAfectacion <> totalPedido Then
                        listaSustitucion.Add(listaPedidos(i))
                    End If
                Next
                lsvPedidos.DataSource = listaSustitucion
                lsvPedidos.DataBind()
        End Select
    End Sub
#End Region
    Protected Sub btnSalir_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub
    '#Region "Botones"

    '    Protected Sub lnkConsultar_Click(sender As Object, e As EventArgs)
    '        Dim clic As LinkButton = sender
    '        Dim idPedido As Guid = Guid.Parse(clic.CommandName)
    '        Response.Redirect("~/management/afectacion/frmConsultarAfectacion.aspx?idPedido=" + idPedido.ToString)
    '    End Sub
    '    Protected Sub lnkAgregar_Click(sender As Object, e As EventArgs)

    '    End Sub
    '    Protected Sub lnkModificar_Click(sender As Object, e As EventArgs)
    '        Dim clic As LinkButton = sender
    '        Dim idPedido As Guid = Guid.Parse(clic.CommandName)
    '        Response.Redirect("~/management/afectacion/frmModificarAfectacion.aspx?id=" + idPedido.ToString)
    '    End Sub
    '    Protected Sub lnkSustituir_Click(sender As Object, e As EventArgs)
    '        Dim clic As LinkButton = sender
    '        Dim idPedido As Guid = Guid.Parse(clic.CommandName)
    '        Response.Redirect("~/management/afectacion/frmSustitucion.aspx?id=" + idPedido.ToString)
    '    End Sub
    '#End Region
End Class
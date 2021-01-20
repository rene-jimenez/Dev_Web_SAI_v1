Imports CRN.nspArticulo
Imports CES.nspArticulo
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmListaArticulosAjustar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim band As String = Request.QueryString("band")
                poblarLista(band)
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "datos"
    Public Sub poblarLista(band As String)

        Try
            Dim sistema = sistemaActivo.tipo.ToString
            Dim articulos = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.entraAlmacen, .tipoSistema = sistema, .entraAlmacen = True}.Ejecutar().OrderBy(Function(k) k.codigoBarras).ToList

            If Request.QueryString("band") = "cons" Then
                lblTitulo.Text = "Lista de artículos para consulta"
                demas.Visible = False
                consultar.Visible = True
                lsvConsultar.DataSource = articulos
                lsvConsultar.DataBind()
            Else
                lblTitulo.Text = "Lista de artículos para ajustar"
                demas.Visible = True
                consultar.Visible = False
                lsvListado.DataSource = articulos
                lsvListado.DataBind()
            End If

        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", "" & ex.Message.ToString, tipoPopup.Naranja, False, "")

        End Try
    End Sub

#End Region


    Protected Sub btnSeleccionar_OnClick(sender As Object, e As EventArgs)
        Try
            Dim btn As LinkButton = sender
            Dim indice As Integer = btn.TabIndex
            Dim id As Guid = Guid.Parse(btn.CommandArgument)
            Dim idString = id.ToString
            Dim miPagina = "frmAltaUsuario.aspx?band=edt&id=" & idString
            Response.Redirect(miPagina)
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub
#Region "botones"
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        mandaDefault()
    End Sub

    Protected Sub lnkSeleccionar_Click(sender As Object, e As EventArgs)
        Try
            Dim btnSeleccionar As LinkButton = sender
            Dim idArticulo As Guid = Guid.Parse(btnSeleccionar.CommandArgument)

            Response.Redirect("~/management/almacen/inventario/frmAjustarInventario.aspx?idArticulo=" + idArticulo.ToString)
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", "" & ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try

    End Sub
#End Region
    'Public Sub lsvListado_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lsvListado.ItemDataBound
    '    If e.Item.ItemType = ListViewItemType.DataItem Then
    '        Dim tdx As TableHeaderRow
    '        e.Item.FindControl("tdx")
    '        tdx.Visible = True

    '    End If

    'End Sub

End Class
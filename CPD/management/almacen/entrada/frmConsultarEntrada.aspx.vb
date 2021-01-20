Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspEntrada, CRN.nspEntrada
Imports CES.nspDetalleEntrada, CRN.nspDetalleEntrada
Imports CES.nspPopup
Public Class frmConsultarEntrada : Inherits nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim idEntrada As Guid = Guid.Parse(Request.QueryString("idEntrada"))
            poblarFormulario(idEntrada)
        End If

    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub poblarFormulario(idEntrada As Guid)
        Dim entrada = New Proceso_ObtenerEntrada() With {.id = idEntrada}.Ejecutar()
        txbNumeroPedido.Text = entrada._numeroPedido
        txbFechaFinal.Text = entrada.fechaEntrada
        txbProveedor.Text = entrada._nombreProveedor
        txbFechaPedido.Text = entrada._fechaPedidoRecibido
        txbFactura.Text = entrada.numRemision
        If entrada.esNota = True Then
            chkNota.Checked = True
            chkFactura.Checked = False
            chkFactura.Visible = False
        Else
            chkNota.Checked = False
            chkFactura.Checked = True
            chkNota.Visible = False
        End If
        txbObservaciones.Text = entrada.comentario

        Dim listaDetalleEntrda = New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = tipoConsultaDetalleEntrada.idEntrada, .idEntrada = Guid.Parse(entrada.id.ToString)}.Ejecutar
        lsvArticulosEntrada.DataSource = listaDetalleEntrda
        lsvArticulosEntrada.DataBind()

    End Sub

End Class
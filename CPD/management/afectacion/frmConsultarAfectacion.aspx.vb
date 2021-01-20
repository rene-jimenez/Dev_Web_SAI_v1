Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup
Imports CRN.nspAfectacionPresupuestal
Public Class frmConsultarAfectacion : Inherits nspPaginaBase.PaginaBase
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
#Region "Métodos"
    Public Sub llenarDatos()
        Dim idPedido As String = Request.QueryString("idPedido")
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
        Dim consultaAfectacion = New Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = CES.nspAfectacionPresupuestal.tipoConsultaAfectacionPresupuestal.idPedido, .idPedido = Guid.Parse(idPedido)}.Ejecutar()
        txbConcepto.Text = consultaAfectacion(0).concepto.ToString
        txbMarcaAgua.Text = consultaAfectacion(0).marcaAgua.ToString
        lnkIva.Text = "+ Iva: $" + consultaAfectacion(0).iva.ToString("N2")
        lnkDescuento.Text = "-Descuento: $" + consultaAfectacion(0).descuento.ToString("N2")
        lnkSubtotal.Text = "Subtotal: $" + consultaAfectacion(0).subtotal.tostring("N2")
        lnkTotal.Text = "Total: $" + consultaAfectacion(0).total.ToString("N2")
        txbImportePago.Text = consultaAfectacion(0).total.ToString("N2")
        lblAutoriza.Text = consultaAfectacion(0)._nombreAutoriza.ToString
        lblSolicita.Text = consultaAfectacion(0)._nombreSolicita.ToString

        'Dim consultaSolicitante = New CRN.nspFirma.Proceso_ObtenerFirma() With {.id = consultaAfectacion(0).idSolicita}.Ejecutar()
        'Dim usuario = New cadenero.CRN.nspUsuario.Proceso_ObtenerUsuario() With {.id = consultaSolicitante.idUsuario}.Ejecutar()
        'lblSolicita.Text = usuario.nombre.ToString
        'Dim consultaAutoriza = New CRN.nspFirma.Proceso_ObtenerFirma() With {.id = consultaAfectacion(0).idAutoriza}.Ejecutar()
        'Dim usuarioAutoriza = New cadenero.CRN.nspUsuario.Proceso_ObtenerUsuario() With {.id = consultaAutoriza.idUsuario}.Ejecutar()
        'lblAutoriza.Text = usuarioAutoriza.nombre.ToString


    End Sub


#End Region
#Region "Botones"
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Response.Redirect("~/management/afectacion/reporteAfectacion/frmReporteAfectacion.aspx?idPedido=" + Request.QueryString("idPedido"))
    End Sub

#End Region

End Class
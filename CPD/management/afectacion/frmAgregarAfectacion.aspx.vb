Imports CRN.nspAfectacionPresupuestal, CRN.nspArea, CRN.nspPedido, CRN.nspOficio, CRN.nspRubroRequerimiento, CRN.nspPartidaPresupuestal, CRN.nspFirma, CRN.nspProveedor, CRN.nspTipoPago, CES.nspDetallePedido, CRN.nspDetallePedido, CRN.nspUnidadMedida, CRN.nspArticulo, CES.nspArticulo
Imports CES, CES.nspAfectacionPresupuestal
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmAgregarAfectacion : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                listaOculta.Visible = False
                llenarLabelsDefault()
                llenarDrops()
            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub



#Region "Metodos"
    Public Sub llenarLabelsDefault()
        Dim idx = Request.QueryString("idPedido")
        Dim qwe = New Proceso_ObtenerPedido() With {.id = Guid.Parse(idx)}.Ejecutar()
        Dim rty = New Proceso_ObtenerUnOficio() With {.id = qwe.idOficio}.Ejecutar()
        Dim asd = New Proceso_ObtenerDetallePedido() With {.id = qwe.id}.Ejecutar()
        lblTurnoSAF.Text = rty.turnoSAF
        lblTurnoDRM.Text = rty.turnoDRM
        lblNumPedido.Text = qwe.numeroPedido
        lblAreaSolicitante.Text = rty._area
        lblCargoPresupuestal.Text = rty._cargoPresupuestal
        lblPartida.Text = qwe._nombrePartida
        lblProveedor.Text = qwe._nombreProveedor
        ObtenerDatos()
    End Sub
    Public Sub llenarDrops()
        Dim solicita = New Proceso_ObtenerFirmas() With {.tipoConsulta = nspFirma.tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Solicita", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(sl) sl._nombreUsuario).ToList
        cmbSolicita.DataTextField = "_nombreUsuario"
        cmbSolicita.DataValueField = "id"
        cmbSolicita.DataSource = solicita
        cmbSolicita.DataBind()

        Dim autoriza = New Proceso_ObtenerFirmas() With {.tipoConsulta = nspFirma.tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Autoriza", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(au) au._nombreUsuario).ToList
        cmbAutoriza.DataTextField = "_nombreUsuario"
        cmbAutoriza.DataValueField = "id"
        cmbAutoriza.DataSource = autoriza
        cmbAutoriza.DataBind()
    End Sub
    Public Sub recolectorDeBasura()
        lnkDescuento.Text = String.Empty
        lnkIva.Text = String.Empty
        lnkTotal.Text = String.Empty
        lnkSubtotal.Text = String.Empty
        lblImportePago.Text = String.Empty
        lvsListado.Items.Clear()
    End Sub

    Public Sub ObtenerDatos()
        Dim idx = Request.QueryString("idPedido")
        Dim pedido = New Proceso_ObtenerPedido() With {.id = Guid.Parse(idx)}.Ejecutar()
        Dim valorIVA = New CRN.nspIva.Proceso_ObtenerIva() With {.fecha = pedido.fechaElaboracion}.Ejecutar
        Dim listaDetallePedido = New Proceso_ObtenerDetallePedidos() With {.tipoConsulta = tipoConsultaDetallePedido.idPedido, .idPedido = Guid.Parse(idx)}.Ejecutar().OrderBy(Function(d) d._numeroPedido).ToList
        lvsListado.DataSource = listaDetallePedido
        lvsListado.DataBind()
        Dim banderaIva As Boolean
        Dim Total As Double = 0
        Dim subTotal As Double = 0
        Dim iva As Double = 0
        Dim descuento As Double = 0

        If pedido.iva Then
            banderaIva = True
        Else
            banderaIva = False
        End If
        Dim ddesc As Boolean
        If pedido.descuento = "0" Then
            ddesc = False
        Else
            ddesc = True
        End If
        If banderaIva = True Then
            If (listaDetallePedido.Count() > 0) Then

                subTotal = listaDetallePedido.Sum(Function(s) s._total)
                If pedido.descuento <> "0" Then
                    descuento = (Double.Parse(pedido.descuento))
                End If
                If (descuento > 0) Then
                    iva = (subTotal - descuento) * valorIVA
                Else
                    iva = subTotal * valorIVA
                End If
                Total = subTotal - descuento + iva
                lnkDescuento.Text = "Descuento: $" + (descuento.ToString("0,0.00", CultureInfo.InvariantCulture))
                lnkIva.Text = "Iva: $" + (iva.ToString("0,0.00", CultureInfo.InvariantCulture))
                lnkTotal.Text = "Total: $" + (Total.ToString("0,0.00", CultureInfo.InvariantCulture))
                lnkSubtotal.Text = "Subtotal: $" + (subTotal.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblImportePago.Text = "$" + (Total.ToString("0,0.00", CultureInfo.InvariantCulture))
            End If

        Else
            If (listaDetallePedido.Count() > 0) Then
                subTotal = listaDetallePedido.Sum(Function(s) s._total)
                If pedido.descuento <> "0" Then
                    descuento = (Double.Parse(pedido.descuento))
                Else
                End If
                Total = subTotal - descuento
                lnkDescuento.Text = "Descuento: $" + (descuento.ToString("0,0.00", CultureInfo.InvariantCulture))
                lnkIva.Text = "Iva: $" + (iva.ToString("0,0.00", CultureInfo.InvariantCulture))
                lnkTotal.Text = "Total: $" + (Total.ToString("0,0.00", CultureInfo.InvariantCulture))
                lnkSubtotal.Text = "Subtotal: $" + (subTotal.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblImportePago.Text = "$" + (Total.ToString("0,0.00", CultureInfo.InvariantCulture))
            End If

        End If

    End Sub


#End Region
#Region "Funciones"
    Private Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function
    Private Function validars() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If cmbSolicita.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "solicita")
            Throw New Exception(respuesta.comentario)
        End If
        If cmbAutoriza.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "autoriza")
            Throw New Exception(respuesta.comentario)
        End If
        If txbConcepto.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "concepto")
            Throw New Exception(respuesta.comentario)

        End If
        If txbMarcaAgua.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "marca de agua")
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function
#End Region
#Region "Botones"
    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs)
        recolectorDeBasura()
        mandaDefault()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Try
            Dim resultadoValidacion = validars()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim idx = Request.QueryString("idPedido")

            Dim nvAfc = New nspAfectacionPresupuestal.afectacionPresupuestal
            nvAfc.idPedido = Guid.Parse(idx)
            nvAfc.idSolicita = Guid.Parse(cmbSolicita.SelectedValue)
            nvAfc.idAutoriza = Guid.Parse(cmbAutoriza.SelectedValue)
            nvAfc.concepto = txbConcepto.Text.Trim()
            nvAfc.marcaAgua = txbMarcaAgua.Text.Trim()
            nvAfc.iva = (Double.Parse(Mid(lnkIva.Text, 7)))
            nvAfc.descuento = (Double.Parse(Mid(lnkDescuento.Text, 13)))
            nvAfc.subtotal = (Double.Parse(Mid(lnkSubtotal.Text, 12)))
            nvAfc.total = (Double.Parse(Mid(lnkTotal.Text, 9)))
            nvAfc.idSistema = sisActivo.sistemaActivo.idSistema
            nvAfc.ipUsuario = getMacAddress()
            nvAfc.idUsuarioMovimiento = IdUsuario
            Dim respuesta = New Proceso_agregarAfectacionPresupuestal() With {
            .entidad = nvAfc
            }.Ejecutar()

            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_guardaron, "El pedido se ha guardado correctamente"), nspPopup.tipoPopup.Verde, True, "management/afectacion/reporteAfectacion/frmReporteAfectacion.aspx?idPedido=" + idx.ToString)
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
#End Region
End Class
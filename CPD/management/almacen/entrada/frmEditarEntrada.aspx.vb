Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspEntrada, CRN.nspEntrada
Imports CES.nspArticulo, CRN.nspArticulo
Imports CES.nspDetalleEntrada, CRN.nspDetalleEntrada

Imports CES.nspPopup
Public Class frmEditarEntrada : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim idEntrada = Guid.Parse(Request.QueryString("idEntrada"))
            poblarFormulario(idEntrada)

        End If
    End Sub

#Region "botones"
    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        Try
            Dim idEntrada = Guid.Parse(Request.QueryString("idEntrada"))
            Dim entrada = New Proceso_ObtenerEntrada() With {.id = idEntrada}.Ejecutar()
            If chkNota.Checked = True Then
                entrada.esNota = True
            Else
                entrada.esNota = False
            End If
            entrada.numRemision = txbNumDocumento.Text
            entrada.comentario = txbObservaciones.Text
            entrada.tipo = validarEntradaCompleta(entrada.id)
            entrada.ipUsuario = direccionIP
            entrada.idUsuarioMovimiento = IdUsuario
            entrada.idSistema = sistemaActivo.id
            Dim respuesta = New Proceso_ActualizarEntrada() With {.entidad = entrada}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    Session("listaDetalleEntrada") = Nothing
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "entrada a almacén"), tipoPopup.Verde, True, "management/default.aspx")
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Critico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try

    End Sub
    Private Sub chkNota_CheckedChanged(sender As Object, e As EventArgs) Handles chkNota.CheckedChanged
        If chkNota.Checked Then
            chkFactura.Checked = False
        End If
    End Sub
    Private Sub chkFactura_CheckedChanged(sender As Object, e As EventArgs) Handles chkFactura.CheckedChanged
        If chkFactura.Checked Then
            chkNota.Checked = False
        End If
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
    Private Sub btnPrincipal_Click(sender As Object, e As EventArgs) Handles btnPrincipal.Click
        mandaDefault()
    End Sub
#End Region


#Region "metodos"
    Protected Sub txbCantidadRecibida_OnTextChanged(sender As Object, e As EventArgs)
        Try
            Dim txb As TextBox = sender
            Dim txbCantidad As TextBox = CType(lsvArticulosEntrada.Items(txb.TabIndex).FindControl("txbCantidadRecibida"), TextBox)
            Dim idDetalleEntrada As Guid = Guid.Parse(txbCantidad.ToolTip.ToString)
            Dim editarDetalleEntrada = New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = tipoConsultaDetalleEntrada.id, .id = idDetalleEntrada}.Ejecutar().FirstOrDefault
            Dim cantidadAnterior = editarDetalleEntrada.cantidad
            If txbCantidad.Text < 0 Then
                txbCantidad.Text = ""
                Throw New Exception("La cantidad de entrada no puede ser menor a 0")
            End If
            If editarDetalleEntrada._cantidadPedido < txbCantidad.Text Then
                txbCantidad.Text = ""
                Throw New Exception("La cantidad de entrada no puede ser mayor a la del pedido")
            End If
            If editarDetalleEntrada._cantidadPedido = txbCantidad.Text Then
                editarDetalleEntrada.esParcial = False
            Else
                editarDetalleEntrada.esParcial = True
            End If
            editarDetalleEntrada.cantidad = txbCantidad.Text
            editarDetalleEntrada.ipUsuario = direccionIP
            editarDetalleEntrada.idUsuarioMovimiento = IdUsuario
            editarDetalleEntrada.idSistema = sistemaActivo.id
            Dim respuesta = New Proceso_ActualizarDetalleEntrada() With {.entidad = editarDetalleEntrada}.Ejecutar()

            Dim editarArticulo = New Proceso_ObtenerArticulo() With {.id = Guid.Parse(editarDetalleEntrada.idArticulo.ToString)}.Ejecutar()
            editarArticulo.existencia = (editarArticulo.existencia - cantidadAnterior) + txbCantidad.Text
            editarArticulo.ipUsuario = direccionIP
            editarArticulo.idUsuarioMovimiento = IdUsuario
            editarArticulo.tipoSistema = sistemaActivo.tipo
            Dim respuesta2 = New Proceso_ActualizarArticulo() With {.entidad = editarArticulo}.Ejecutar()

            Dim entrada = New Proceso_ObtenerEntradas() With {.tipoConsulta = tipoConsultaEntrada.id, .id = editarDetalleEntrada.idEntrada}.Ejecutar().FirstOrDefault()
            entrada.tipo = validarEntradaCompleta(entrada.id)
            Dim respuesta3 = New Proceso_ActualizarEntrada() With {.entidad = entrada}.Ejecutar()

            poblarDetalleEntrada(editarDetalleEntrada.idEntrada)
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "/management/almacen/entrada/frmEditarEntrada.aspx")
        End Try

    End Sub
    Private Sub poblarFormulario(idEntrada As Guid)
        Dim entrada = New Proceso_ObtenerEntrada() With {.id = idEntrada}.Ejecutar()
        txbNumeroPedido.Text = entrada._numeroPedido
        txbFechaFinal.Text = entrada.fechaEntrada
        txbProveedor.Text = entrada._nombreProveedor
        txbFechaPedido.Text = entrada._fechaPedidoRecibido
        txbNumDocumento.Text = entrada.numRemision
        If entrada.esNota = True Then
            chkNota.Checked = True
            chkFactura.Checked = False
        Else
            chkNota.Checked = False
            chkFactura.Checked = True
        End If
        txbObservaciones.Text = entrada.comentario
        Dim listaDetalleEntrda = New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = tipoConsultaDetalleEntrada.idEntrada, .idEntrada = Guid.Parse(entrada.id.ToString)}.Ejecutar
        lsvArticulosEntrada.DataSource = listaDetalleEntrda
        lsvArticulosEntrada.DataBind()
    End Sub
    Private Sub poblarDetalleEntrada(idEntrada As Guid)
        Dim listaDetalleEntrda = New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = tipoConsultaDetalleEntrada.idEntrada, .idEntrada = Guid.Parse(idEntrada.ToString)}.Ejecutar
        lsvArticulosEntrada.DataSource = listaDetalleEntrda
        lsvArticulosEntrada.DataBind()

    End Sub
#End Region
#Region "funciones"
    Private Function validarEntradaCompleta(idEntrada As Guid) As Boolean
        Dim bandera As Boolean = True
        Dim detallesEntrada = New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = tipoConsultaDetalleEntrada.idEntrada, .idEntrada = Guid.Parse(idEntrada.ToString)}.Ejecutar()
        For i = 0 To detallesEntrada.Count - 1
            If detallesEntrada(i).esParcial = True Then
                bandera = False
                Exit For
            End If
        Next
        Return bandera
    End Function


#End Region
End Class
Imports CRN.nspEntrada, CRN.nspDetalleEntrada, CRN.nspPedido, CRN.nspDetallePedidoParaEntrada
Imports CES.nspEntrada, CES.nspDetalleEntrada, CES.nspDetallePedidoParaEntrada, CES
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Transactions

Public Class frmActualizarEntrada : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                'idEntrada
                llenarListaArticulos(Guid.Parse(Request.QueryString("idEntrada")))
            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "Metodos"

    Private Sub llenarListaArticulos(idEntrada)
        Dim consultaEntrada = New Proceso_ObtenerEntrada() With {.id = idEntrada}.Ejecutar()
        Dim consulta = New Proceso_ObtenerPedido() With {.id = consultaEntrada.idPedido}.Ejecutar()
        txbNumeroPedido.Text = consulta.numeroPedido.ToString
        txbProveedor.Text = consulta._nombreProveedor.ToString
        txbFechaFinal.Text = Date.Now
        txbFechaPedido.Text = consulta.fechaElaboracion.ToString
        txbFactura.Text = consultaEntrada.numRemision.ToString
        If consultaEntrada.esNota = True Then
            chkNota.Checked = True
            chkFactura.Checked = False
            chkFactura.Visible = False
        Else
            chkNota.Checked = False
            chkFactura.Checked = True
            chkNota.Visible = False
        End If
        txbObservaciones.Text = consultaEntrada.comentario.ToString
        txbFechaFinal.Text = Date.Now
        Dim consultaLista = New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = nspDetalleEntrada.tipoConsultaDetalleEntrada.idEntrada, .idEntrada = idEntrada}.Ejecutar().Where(Function(a) a.esParcial = True)
        Session("listaArticulos") = consultaLista
        lsvListado.DataSource = consultaLista.ToList
        lsvListado.DataBind()
    End Sub

#End Region

#Region "Funciones"
    Private Function validarNoSeaMayor() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        For i = 0 To lsvListado.Items.Count - 1
            Dim cantPedida As Integer = CInt(CType(lsvListado.Items(i).FindControl("lblCantidadPedida"), Label).Text)
            Dim cantidadFal As Integer = CType(lsvListado.Items(i).FindControl("txbEntrada"), TextBox).Text
            Dim cantRecibida As Integer = CType(lsvListado.Items(i).FindControl("lblCantidadRecibida"), Label).Text
            If cantidadFal < cantPedida Then
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = "La cantidad  debe ser menor a la cantidad faltante"
                Throw New Exception(respuesta.comentario)
            End If
        Next
        Return respuesta
    End Function
    Protected Sub txbCantidadRecibida_OnTextChanged(sender As Object, e As EventArgs)
        Try
            Dim txb As TextBox = sender
            Dim cantPedida As Integer = CInt(CType(lsvListado.Items(txb.TabIndex).FindControl("lblCantidadPedida"), Label).Text)
            Dim lblCantidadFaltante As Label = CType(lsvListado.Items(txb.TabIndex).FindControl("lblCantidadFaltante"), Label)
            Dim cantidadFaltante As Integer = CInt(lblCantidadFaltante.Text)
            Dim lblTipoEntrada As Label = CType(lsvListado.Items(txb.TabIndex).FindControl("lblTipoEntrada"), Label)
            Dim lblCantidadRecibida As Label = CType(lsvListado.Items(txb.TabIndex).FindControl("lblCantidadRecibida"), Label)
            Dim cantidadRecibida As Integer = CInt(CType(lsvListado.Items(txb.TabIndex).FindControl("lblCantidadRecibida"), Label).Text)
            Dim cantEntrada As Integer = CInt(CType(lsvListado.Items(txb.TabIndex).FindControl("txbEntrada"), TextBox).Text)
            If txb.Text < 0 Then
                txb.Text = ""
                ' OnMostrarMensajeAccion("Advertencia", "La cantidad no puede ser negativa", nspPopup.tipoPopup.Naranja, False, "")
                Throw New Exception("La cantidad no puede ser negativa2")
                Exit Sub
            End If
            If CInt(lblCantidadRecibida.Text) > cantPedida Then
                txb.Text = ""
                'OnMostrarMensajeAccion("Advertencia", "La cantidad de entrada no puede ser mayor a la del pedido", nspPopup.tipoPopup.Naranja, False, "")
                Throw New Exception("La cantidad de entrada no puede ser mayor a la del pedido")
                Exit Sub
            End If
            If cantEntrada > CInt(lblCantidadFaltante.Text) Then
                txb.Text = ""
                ' OnMostrarMensajeAccion("Advertencia", "La cantidad de entrada no puede ser mayor a la cantidad del faltante del pedido", nspPopup.tipoPopup.Naranja, False, "")
                Throw New Exception("La cantidad de entrada no puede ser mayor a la cantidad del faltante del pedido")
                Exit Sub
            End If

            If CInt(lblCantidadFaltante.Text) < 0 Then
                txb.Text = ""
                'OnMostrarMensajeAccion("Advertencia", "Excediste la cantidad de entrada que se solicitó en el pedido", nspPopup.tipoPopup.Naranja, False, "")
                Throw New Exception("Excediste la cantidad de entrada que se solicitó en el pedido")
                Exit Sub
            End If
            lblCantidadRecibida.Text = cantEntrada + cantidadRecibida
            lblCantidadFaltante.Text = cantPedida - (cantidadRecibida + cantEntrada)

            If cantPedida = CInt(lblCantidadRecibida.Text) Then
                lblTipoEntrada.Text = "Completada"
                lblTipoEntrada.CssClass = "text-success"
            Else
                lblTipoEntrada.Text = "Parcial"
                lblTipoEntrada.CssClass = "text-warning"
            End If

            If CType(lsvListado.Items(txb.TabIndex).FindControl("txbEntrada"), TextBox).Text.ToString = "" Then
                'OnMostrarMensajeAccion("Advertencia", "La cantidad de entrada no puede ir vacía, la cantidad es requerida  ejm. 0", nspPopup.tipoPopup.Naranja, False, "")
                Throw New Exception("La cantidad de entrada no puede ir vacía, la cantidad es requerida  ejm. 0")
                Exit Sub
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", "" & ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try


    End Sub
    Private Sub lsvListado_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lsvListado.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim id As Guid = Guid.Parse(CType(e.Item.FindControl("lblArticulo"), Label).ToolTip)
            Dim cns = New Proceso_ObtenerDetalleEntrada() With {.id = id}.Ejecutar()
            Dim cantPedida As Integer = CInt(CType(e.Item.FindControl("lblCantidadPedida"), Label).Text)
            Dim cantEntrada As Integer = CInt(CType(e.Item.FindControl("txbEntrada"), TextBox).Text)
            Dim cantRecibida As Integer = CInt(CType(e.Item.FindControl("lblCantidadRecibida"), Label).Text)
            Dim lblTipoEntrada As Label = CType(e.Item.FindControl("lblTipoEntrada"), Label)
            Dim lblCantidadFaltante As Label = CType(e.Item.FindControl("lblCantidadFaltante"), Label)
            Dim cantidadFaltante1 As Integer = CInt(lblCantidadFaltante.Text)
            Dim txbEntrada As TextBox = CType(e.Item.FindControl("txbEntrada"), TextBox)
            Dim cantidadFaltanteTotal As Integer = cantPedida - (cantEntrada + cantidadFaltante1)
            If txbEntrada.Text = "0" Then
                lblCantidadFaltante.Text = cantPedida - cantRecibida
            Else
                lblCantidadFaltante.Text = cantidadFaltanteTotal
            End If
            If cantPedida = cantEntrada Then
                    lblTipoEntrada.Text = "Completada"
                    lblTipoEntrada.CssClass = "text-success"
                Else
                    lblTipoEntrada.Text = "Parcial"
                    lblTipoEntrada.CssClass = "text-warning"
                End If

            End If
    End Sub
#End Region
#Region "botones"
    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            'Dim resultadoValidacion = validarNoSeaMayor()
            'If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
            '    Throw New Exception(resultadoValidacion.comentario)
            'End If
            ' Dim consultaEntrada = New Proceso_ObtenerEntradas() With {.tipoConsulta = tipoConsultaEntrada.idPedido, .idPedido = (Guid.Parse(Request.QueryString("idPedido")))}.Ejecutar()
            Dim entradEditar = New Proceso_ObtenerEntrada() With {.id = Guid.Parse(Request.QueryString("idEntrada"))}.Ejecutar()
            entradEditar.comentario = txbObservaciones.Text
            Dim DetallesEntradaEditar As New detalleEntrada
            For i = 0 To lsvListado.Items.Count - 1
                Dim cant As Integer = CInt(CType(lsvListado.Items(i).FindControl("txbEntrada"), TextBox).Text)
                Dim idDetalleEntrada As Guid = Guid.Parse(CType(lsvListado.Items(i).FindControl("lblArticulo"), Label).ToolTip)
                Dim cantRecibida As Integer = CInt(CType(lsvListado.Items(i).FindControl("lblCantidadRecibida"), Label).Text)
                Dim cantPedida As Integer = CInt(CType(lsvListado.Items(i).FindControl("lblCantidadPedida"), Label).Text)
                If cant <> 0 Then
                    Dim consultaDetalleEntradaEditar = New Proceso_ObtenerDetalleEntrada() With {.id = idDetalleEntrada}.Ejecutar()
                    consultaDetalleEntradaEditar.id = idDetalleEntrada
                    consultaDetalleEntradaEditar.cantidad = cantRecibida
                    If cantRecibida = cantPedida Then
                        consultaDetalleEntradaEditar.esParcial = False
                    Else
                        consultaDetalleEntradaEditar.esParcial = True
                    End If
                    consultaDetalleEntradaEditar.idSistema = sistemaActivo.idSistema
                    consultaDetalleEntradaEditar.ipUsuario = direccionIP
                    consultaDetalleEntradaEditar.idUsuarioMovimiento = IdUsuario
                    Using scope As New TransactionScope()
                        Dim respuestaActualizarDetalleEntrada = New Proceso_ActualizarDetalleEntrada() With {.entidad = consultaDetalleEntradaEditar}.Ejecutar()
                        If respuestaActualizarDetalleEntrada.respuesta <> tipoRespuestaDelProceso.Completado Then
                            OnMostrarMensajeAccion("Atención", respuestaActualizarDetalleEntrada.comentario, nspPopup.tipoPopup.Rojo, False, "")
                            Exit Sub
                        Else
                            Dim articuloEditar = New CRN.nspArticulo.Proceso_ObtenerArticulo() With {.id = Guid.Parse(CType(lsvListado.Items(i).FindControl("lblCodeBar"), Label).ToolTip)}.Ejecutar()
                            articuloEditar.id = Guid.Parse(CType(lsvListado.Items(i).FindControl("lblCodeBar"), Label).ToolTip)
                            articuloEditar.existencia = articuloEditar.existencia + CType(lsvListado.Items(i).FindControl("txbEntrada"), TextBox).Text
                            articuloEditar.idSistema = sistemaActivo.idSistema
                            articuloEditar.ipUsuario = direccionIP
                            articuloEditar.idUsuarioMovimiento = IdUsuario
                            Dim respuestaArticuloActu = New CRN.nspArticulo.Proceso_ActualizarArticulo() With {.entidad = articuloEditar}.Ejecutar()
                            If respuestaArticuloActu.respuesta <> tipoRespuestaDelProceso.Completado Then
                                OnMostrarMensajeAccion("Atención", respuestaArticuloActu.comentario, nspPopup.tipoPopup.Rojo, False, "")
                                Exit Sub
                            End If
                        End If
                        scope.Complete()
                    End Using
                End If
            Next
            'consulta para editar el estatus de completado de entrada
            Dim listaDetalles = New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = tipoConsultaDetalleEntrada.idEntrada, .idEntrada = Guid.Parse(Request.QueryString("idEntrada"))}.Ejecutar().Where(Function(a) a.esParcial = True).ToList
            If listaDetalles.Count > 0 Then 'si hay parciales
                entradEditar.tipo = False
            Else
                entradEditar.tipo = True
            End If
            Dim respuestaActualizarEntrada = New Proceso_ActualizarEntrada() With {.entidad = entradEditar}.Ejecutar()
            Select Case respuestaActualizarEntrada.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "entrada a almacén"), nspPopup.tipoPopup.Verde, True, "management/default.aspx")
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuestaActualizarEntrada.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Atención", respuestaActualizarEntrada.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

#End Region
End Class
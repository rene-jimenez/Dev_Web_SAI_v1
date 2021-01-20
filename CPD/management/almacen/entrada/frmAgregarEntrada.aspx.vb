Imports CRN.nspEntrada, CRN.nspArticulo, CRN.nspPedido, CRN.nspDetallePedidoParaEntrada, CRN.nspUltimoPrecioArticulo
Imports CES.nspEntrada, CES.nspDetalleEntrada, CES.nspDetallePedidoParaEntrada, CES, CES.nspUltimoPrecioArticulo
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Imports System.Transactions


Public Class frmAgregarEntrada : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                poblarCamposPedido(Guid.Parse(Request.QueryString("idPedido")))
                llenarListaArticulos(Guid.Parse(Request.QueryString("idPedido")))
            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub

#Region "Metodos"
    Private Sub poblarCamposPedido(idPedido)
        Dim consulta = New Proceso_ObtenerPedido() With {.id = idPedido}.Ejecutar()
        txbNumeroPedido.Text = consulta.numeroPedido.ToString
        txbProveedor.Text = consulta._nombreProveedor.ToString
        txbFechaFinal.Text = Date.Now
        txbFechaPedido.Text = consulta.fechaElaboracion.ToString
    End Sub
    Private Sub llenarListaArticulos(idPedido)
        Dim consultaLista = New Proceso_ObtenerDetallesPedidoParaEntrada() With {.tipoConsulta = nspDetallePedidoParaEntrada.tipoConsultaDetallePedidoParaEntrada.idPedido, .idPedido = idPedido}.Ejecutar()
        lsvListado.DataSource = consultaLista.ToList
        lsvListado.DataBind()
    End Sub

    Protected Sub txbCantidadRecibida_OnTextChanged(sender As Object, e As EventArgs)
        Try
            Dim txb As TextBox = sender
            Dim cantPedida As Integer = CInt(CType(lsvListado.Items(txb.TabIndex).FindControl("lblCantidadPedida"), Label).Text)
            Dim existencia As Label = CType(lsvListado.Items(txb.TabIndex).FindControl("lblCantidadRecibida"), Label)
            Dim cantidadFaltante As Integer = CInt(CType(lsvListado.Items(txb.TabIndex).FindControl("lblCantidadFaltante"), Label).Text)
            Dim lblTipoEntrada As Label = CType(lsvListado.Items(txb.TabIndex).FindControl("lblTipoEntrada"), Label)
            Dim lblCantidadFaltante As Label = CType(lsvListado.Items(txb.TabIndex).FindControl("lblCantidadFaltante"), Label)
            Dim cantEntrada As Integer = CInt(CType(lsvListado.Items(txb.TabIndex).FindControl("txbEntrada"), TextBox).Text)
            Dim idArticulo As Guid = Guid.Parse(txb.ToolTip.ToString)

            Dim cantidadFaltanteTotal As Integer = cantPedida - cantEntrada
            If cantEntrada < 0 Then
                txb.Text = ""
                Throw New Exception("La cantidad de entrada no puede ser menor a 0")
            End If
            If cantPedida < cantEntrada Then
                txb.Text = ""
                Throw New Exception("La cantidad de entrada no puede ser mayor a la del pedido")
            End If
            If (cantEntrada + cantidadFaltanteTotal) > cantPedida Then
                Throw New Exception("La cantidad de entrada no puede ser mayor a la del pedido")
            End If
            If cantEntrada.ToString = "" Then
                Throw New Exception("La cantidad de entrada no puede ir vacía, la cantidad es requerida  ejm. 0")
            End If
            Dim articulo = New Proceso_ObtenerArticulo() With {.id = idArticulo}.Ejecutar()
            existencia.Text = articulo.existencia + cantEntrada
            lblCantidadFaltante.Text = cantidadFaltanteTotal

            If cantPedida = cantEntrada Then
                lblTipoEntrada.Text = "Completada"
                lblTipoEntrada.CssClass = "text-success"
            Else
                lblTipoEntrada.Text = "Parcial"
                lblTipoEntrada.CssClass = "text-warning"
            End If

        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
            updateListView.Update()
        End Try


    End Sub

    Private Sub lsvListado_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lsvListado.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim cantPedida As Integer = CInt(CType(e.Item.FindControl("lblCantidadPedida"), Label).Text)
            Dim cantEntrada As Integer = CInt(CType(e.Item.FindControl("txbEntrada"), TextBox).Text)
            Dim lblTipoEntrada As Label = CType(e.Item.FindControl("lblTipoEntrada"), Label)
            Dim lblCantidadFaltante As Label = CType(e.Item.FindControl("lblCantidadFaltante"), Label)
            Dim cantidadFaltanteTotal As Integer = cantPedida - cantEntrada
            lblCantidadFaltante.Text = cantidadFaltanteTotal
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

#Region "Funciones"
    Private Function validar() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbRemision.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El número de remisión es obligatoria"
            Throw New Exception(respuesta.comentario)
        End If
        If chkFactura.Checked = False And chkNota.Checked = False Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "Debes seleccionar un tipo de remisión"
            Throw New Exception(respuesta.comentario)
        End If

        Return respuesta
    End Function
    Private Function validarLista() As respuestaDelProceso
        Dim banCero As Integer = 0
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        For i = 0 To lsvListado.Items.Count - 1
            Dim cantEntrada As Integer = CInt(CType(lsvListado.Items(i).FindControl("txbEntrada"), TextBox).Text)
            Dim cantPedida As Integer = CInt(CType(lsvListado.Items(i).FindControl("lblCantidadPedida"), Label).Text)
            Dim cantidad As Boolean = False
            If cantEntrada > cantPedida Then
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = "La cantidad debe ser menor a la cantidad requerida"
                Throw New Exception(respuesta.comentario)
            End If
            If CType(lsvListado.Items(i).FindControl("txbEntrada"), TextBox).Text.Length = 0 Then
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = "Cantidad requerida de uno de los artículos, ejm. 0"
                Throw New Exception(respuesta.comentario)
            End If
            If cantEntrada = 0 Then
                banCero = +1
            End If
        Next
        If banCero = lsvListado.Items.Count Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "Es requerido que al menos entre un artículo de la lista"
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function


#End Region

#Region "botones"
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim resultadoValidacion = validar()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim resultadoValidacionLista = validarLista()
            If resultadoValidacionLista.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacionLista.comentario)
            End If
            Dim entidadEntrada As New nspEntrada.entrada
            Dim esEntradaParcial As Integer
            entidadEntrada.fechaEntrada = txbFechaFinal.Text
            If chkFactura.Checked = True Then
                entidadEntrada.esNota = False
            Else
                entidadEntrada.esNota = True
            End If
            entidadEntrada.id = Guid.NewGuid()
            entidadEntrada.idPedido = Guid.Parse(Request.QueryString("idPedido"))
            Dim consultaNoEntradas = New Proceso_ObtenerEntradas() With {.tipoConsulta = tipoConsultaEntrada.todos, .idSistema = sistemaActivo.idSistema}.Ejecutar()
            Dim ultimo As Integer = consultaNoEntradas.Count
            entidadEntrada.numEntrada = ultimo + 1
            Dim listaARemplazar As New List(Of detalleEntrada)
            For i = 0 To lsvListado.Items.Count - 1
                Dim newDetalleEntrada As New detalleEntrada
                Dim cantidad As Integer = CInt(CType(lsvListado.Items(i).FindControl("txbEntrada"), TextBox).Text)
                Dim cantPedida As Integer = CType(lsvListado.Items(i).FindControl("lblCantidadPedida"), Label).Text
                newDetalleEntrada.id = Guid.NewGuid
                newDetalleEntrada.idEntrada = entidadEntrada.id
                newDetalleEntrada.idArticulo = Guid.Parse(CType(lsvListado.Items(i).FindControl("lblArticulo"), Label).ToolTip)
                newDetalleEntrada.cantidad = CInt(CType(lsvListado.Items(i).FindControl("txbEntrada"), TextBox).Text)
                If cantidad <> cantPedida Then
                    newDetalleEntrada.esParcial = True
                    esEntradaParcial = +1
                Else
                    newDetalleEntrada.esParcial = False
                End If
                newDetalleEntrada.fechaEntrada = Date.Now
                newDetalleEntrada.idSistema = sistemaActivo.idSistema
                newDetalleEntrada.ipUsuario = direccionIP
                newDetalleEntrada.idUsuarioMovimiento = IdUsuario
                listaARemplazar.Add(newDetalleEntrada)
            Next

            If listaARemplazar.Count < 1 Then
                OnMostrarMensajeAccion("Atención", "Es obligatorio que al menos entre un artículo", nspPopup.tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            If esEntradaParcial >= 1 Then
                entidadEntrada.tipo = False  'parcial  true=completa
            Else
                entidadEntrada.tipo = True
            End If
            entidadEntrada.numRemision = txbRemision.Text.ToString
            entidadEntrada.comentario = txbObservaciones.Text
            entidadEntrada.idSistema = sistemaActivo.idSistema
            entidadEntrada.ipUsuario = direccionIP
            entidadEntrada.idUsuarioMovimiento = IdUsuario
            Using scope As New TransactionScope()
                Dim respuestaAgregarEntrada = New Proceso_AgregarEntrada() With {.entidad = entidadEntrada, .listaDetalleEntrada = listaARemplazar}.Ejecutar()
                If respuestaAgregarEntrada.respuesta <> tipoRespuestaDelProceso.Completado Then
                    Throw New Exception(respuestaAgregarEntrada.comentario)
                Else
                    For i = 0 To lsvListado.Items.Count - 1
                        Dim idArticulo As Guid = Guid.Parse(CType(lsvListado.Items(i).FindControl("lblArticulo"), Label).ToolTip)
                        Dim idPedido As Guid = Guid.Parse(CType(lsvListado.Items(i).FindControl("lblCodeBar"), Label).ToolTip)
                        Dim cantidad As Integer = CInt(CType(lsvListado.Items(i).FindControl("txbEntrada"), TextBox).Text)
                        Dim detallePedido = New CRN.nspDetallePedido.Proceso_ObtenerDetallePedidos() With {.tipoConsulta = nspDetallePedido.tipoConsultaDetallePedido.xPedidoArticulo, .idPedido = idPedido, .idArticulo = idArticulo}.Ejecutar()
                        Dim articuloEditar = New Proceso_ObtenerArticulo() With {.ID = idArticulo}.Ejecutar()
                        articuloEditar.id = idArticulo
                        articuloEditar.existencia = articuloEditar.existencia + cantidad
                        articuloEditar.ultimoPrecio = detallePedido(0).precioUnitario
                        articuloEditar.idSistema = sistemaActivo.idSistema
                        articuloEditar.ipUsuario = direccionIP
                        articuloEditar.idUsuarioMovimiento = IdUsuario
                        Dim respuestaExistencia = New Proceso_ActualizarArticulo() With {.entidad = articuloEditar}.Ejecutar()
                        If respuestaExistencia.respuesta <> tipoRespuestaDelProceso.Completado Then
                            OnMostrarMensajeAccion("Atención", respuestaExistencia.comentario, nspPopup.tipoPopup.Naranja, False, "")
                            Throw New Exception(respuestaExistencia.comentario)
                        Else
                            Dim ultimoPrecio As New ultimoPrecioArticulo
                            ultimoPrecio.id = Guid.NewGuid
                            ultimoPrecio.idArticulo = idArticulo
                            ultimoPrecio.ultimoPrecio = detallePedido(0).precioUnitario
                            ultimoPrecio.fecha = Date.Now
                            ultimoPrecio.idSistema = sistemaActivo.idSistema
                            ultimoPrecio.ipUsuario = direccionIP
                            ultimoPrecio.idUsuarioMovimiento = IdUsuario
                            Dim respuestaGuardarUltPrecio = New Proceso_AgregarUltimoPrecioArticulo() With {.entidad = ultimoPrecio}.Ejecutar()
                            If respuestaGuardarUltPrecio.respuesta <> tipoRespuestaDelProceso.Completado Then
                                OnMostrarMensajeAccion("Atención", respuestaGuardarUltPrecio.comentario, nspPopup.tipoPopup.Naranja, False, "")
                                Throw New Exception(respuestaGuardarUltPrecio.comentario)
                            End If
                        End If
                    Next
                    Select Case respuestaAgregarEntrada.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_guardaron, "entrada a almacén"), nspPopup.tipoPopup.Verde, True, "management/default.aspx")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuestaAgregarEntrada.comentario, nspPopup.tipoPopup.Naranja, False, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuestaAgregarEntrada.comentario, nspPopup.tipoPopup.Rojo, False, "")
                    End Select
                    scope.Complete()
                End If
            End Using
        Catch ex As Exception
            OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
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

#End Region
End Class
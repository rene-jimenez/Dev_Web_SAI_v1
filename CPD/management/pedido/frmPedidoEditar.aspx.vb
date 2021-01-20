Imports CES, CRN
Imports CRN.nspArea, CRN.nspPartidaPresupuestal, CRN.nspFirma, CRN.nspProveedor, CRN.nspTipoPago, CRN.nspArticulo, CRN.nspDetallePedido
Imports CES.nspPartidaPresupuestal, CES.nspArea, CES.nspFirma, CES.nspProveedor, CES.nspTipoPago, CES.nspArticulo, CES.nspDetallePedido

Imports Contexto.Notificaciones.controladorMensajes
Public Class frmPedidoEditar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim hdsubtotal1 As Double = 0
    Dim hdtotal1 As Double = 0
    Dim hdsubtotaldesc1 As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarCargoPres()
                llenarPartidaPresupuestal()
                llenarFirmaReviso()
                llenarFirmaAutoriza()
                llenarProveedor()
                llenarTipoPago()
                llenarArticulo()
                poblarDatosPedido()

                Dim idPedido = Request.QueryString("id").ToString
                Dim listaPedido = New CRN.nspAfectacionPresupuestal.Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = CES.nspAfectacionPresupuestal.tipoConsultaAfectacionPresupuestal.idPedido, .idPedido = Guid.Parse(idPedido)}.Ejecutar()
                If listaPedido.Count >= 1 Then
                    divPleca2.Visible = True
                Else
                    divPleca2.Visible = False
                End If
            Catch ex As Exception
                MsgBox(ex.Message.ToString)
            End Try
        End If
    End Sub
    Protected Sub btnMostrarAgregarArticulo_Click(sender As Object, e As EventArgs)
        divCuadroPedido.Attributes.Remove("class")
        divCuadroPedido.Attributes("class") = "card col-md-8 col-lg-8 card animated bounceInRight"
        divPanelAgregar.Attributes.Remove("class")
        divPanelAgregar.Attributes("class") = "card col-md-4 col-lg-4 card animated bounceInLeft"
        divPanelAgregar.Visible = True
        updateAgregarArticulo.Update()
        updateCuadroPedido.Update()
    End Sub

#Region "Metodos llenarCombos"
    Protected Sub llenarCargoPres()
        Dim consultaAreas = New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbCargoPresupuestal.DataValueField = "id"
        cmbCargoPresupuestal.DataTextField = "nombre"
        cmbCargoPresupuestal.DataSource = consultaAreas.OrderBy(Function(a) a.nombre).ToList
        cmbCargoPresupuestal.DataBind()
        cmbCargoPresupuestal.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub llenarPartidaPresupuestal()
        Dim consultaCargos = New Proceso_ObtenerPartidasPresupuestales() With {.tipoConsulta = tipoConsultaPartidaPresupuestal.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbPartidaPresupuestal.DataValueField = "id"
        cmbPartidaPresupuestal.DataTextField = "nombre"
        cmbPartidaPresupuestal.DataSource = consultaCargos.OrderBy(Function(a) a.nombre).ToList
        cmbPartidaPresupuestal.DataBind()
        cmbPartidaPresupuestal.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub llenarFirmaReviso()
        Dim consultaFirmasRev = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombre, .Nombre = "Revisó"}.Ejecutar().OrderBy(Function(r) r._nombreUsuario).ToList
        cmbReviso.DataValueField = "id"
        cmbReviso.DataTextField = "_nombreUsuario"
        cmbReviso.DataSource = consultaFirmasRev.ToList
        cmbReviso.DataBind()
        cmbReviso.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub llenarFirmaAutoriza()
        Dim consultaFirmasAut = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombre, .Nombre = "Autoriza"}.Ejecutar().OrderBy(Function(a) a._nombreUsuario).ToList
        cmbAutoriza.DataValueField = "id"
        cmbAutoriza.DataTextField = "_nombreUsuario"
        cmbAutoriza.DataSource = consultaFirmasAut.ToList
        cmbAutoriza.DataBind()
        cmbAutoriza.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub llenarProveedor()
        Dim consulta = New Proceso_ObtenerProveedores() With {.tipoConsulta = tipoConsultaProveedor.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbProveedor.DataValueField = "id"
        cmbProveedor.DataTextField = "nombre"
        cmbProveedor.DataSource = consulta.OrderBy(Function(a) a.nombre).ToList
        cmbProveedor.DataBind()
        cmbProveedor.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub llenarTipoPago()
        Dim consulta = New Proceso_ObtenerTiposPagos() With {.tipoConsulta = tipoConsultaTipoPago.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbTipoPago.DataValueField = "id"
        cmbTipoPago.DataTextField = "nombre"
        cmbTipoPago.DataSource = consulta.OrderBy(Function(a) a.nombre).ToList
        cmbTipoPago.DataBind()
        cmbTipoPago.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub llenarArticulo()
        Dim consultaArticulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.esActivo, .esActivo = True, .tipoSistema = sistemaActivo.tipo}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbArticulo.DataValueField = "id"
        cmbArticulo.DataTextField = "nombre"
        cmbArticulo.DataSource = consultaArticulo.OrderBy(Function(a) a.nombre).ToList
        cmbArticulo.DataBind()
        cmbArticulo.SelectedValue = "Seleccione un elemento de la lista"
    End Sub

#End Region

#Region "Validar"
    Private Function validararticulo() As Contexto.Notificaciones.controladorMensajes.respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbCantidad.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "cantidad")
            Throw New Exception(respuesta.comentario)
        End If
        If txbPrecio.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "precio")
            Throw New Exception(respuesta.comentario)
        End If
        If cmbArticulo.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "Seleccione un articulo de la lista"
            Throw New Exception(respuesta.comentario)
        End If

        Return respuesta
    End Function

    Private Function validarPedido() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If cmbCargoPresupuestal.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "cargo presupuestal")
            Throw New Exception(respuesta.comentario)
        End If
        If cmbPartidaPresupuestal.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "partida presupuestal")
            Throw New Exception(respuesta.comentario)
        End If
        If cmbReviso.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "revisó")
            Throw New Exception(respuesta.comentario)
        End If
        If cmbAutoriza.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "autoriza")
            Throw New Exception(respuesta.comentario)
        End If
        If txbFechaSolicitud.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha solicitud")
            Throw New Exception(respuesta.comentario)
        End If
        If txbFechaAcordadaEntrega.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha acordada")
            Throw New Exception(respuesta.comentario)
        End If
        If txbFechaRecibido.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha de recibido")
            Throw New Exception(respuesta.comentario)
        End If

        If (CDate(txbFechaSolicitud.Text)) > (CDate(txbFechaAcordadaEntrega.Text)) Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha de solicitud debe ser menor o igual a la fecha acordada de entrega"
            Throw New Exception(respuesta.comentario)
        End If

        If (CDate(txbFechaAcordadaEntrega.Text)) > (CDate(txbFechaRecibido.Text)) Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha de recibido debe ser mayor o igual a la fecha acordada de entrega"
            Throw New Exception(respuesta.comentario)
        End If

        If cmbProveedor.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "proveedor")
            Throw New Exception(respuesta.comentario)
        End If

        If cmbTipoPago.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "tipo pago")
            Throw New Exception(respuesta.comentario)
        End If

        If chkVerAlmacen.Checked = True And chkPedido.Checked = False Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La casilla 'Ver almacen' no se puede seleccionar si la casilla 'Pedido' no se ha seleccionado"
            Throw New Exception(respuesta.comentario)
        End If


        If txbDescuento.Text.Length = 0 Then
            txbDescuento.Text = "0"
        End If

        Return respuesta
    End Function
#End Region


#Region "operaciones"
    Private Sub subtotal()
        Dim subtotal As Double = 0
        For i = 0 To lsvCuadroPedido.Items.Count - 1
            subtotal += CType(lsvCuadroPedido.Items(i).FindControl("lblSubTotal"), Label).Text
        Next
        hdsubtotal1 = subtotal.ToString
    End Sub
    Private Sub iva()
        Dim iva As Double
        If chkIva.Checked = True Then
            lineaIva.Visible = True
            subtotal()
            descuento()
            Dim ivaSist = New CRN.nspIva.Proceso_ObtenerIva() With {.fecha = Date.Now}.Ejecutar()
            lblIvaAgregado.Text = ivaSist.ToString
            iva = ivaSist.ToString
            iva = (hdtotal1 * iva)

            lblIva.Text = FormatCurrency(iva, 2)

            If lineaDescuento.Visible = True Then
                hdtotal1 = hdsubtotaldesc1 + iva
                total()
            Else
                hdtotal1 = hdsubtotal1 + iva
                total()
            End If

        Else

            lineaIva.Visible = False
            'lblIvaAgregado.Text = "N/A"
            subtotal()
            descuento()
            total()
        End If
    End Sub
    Private Sub descuento()
        Try
            Dim des As Double

            If txbDescuento.Text = String.Empty Then
                lineaDescuento.Visible = False
                subtotal()

                hdtotal1 = hdsubtotal1
                total()
            Else
                lineaDescuento.Visible = True
                des = txbDescuento.Text
                lblDescuento.Text = FormatCurrency(des, 2)
                subtotal()
                If txbDescuento.Text > hdsubtotal1 Then
                    txbDescuento.Text = "0"
                    Throw New Exception("El descuento es mayor al subtotal")
                End If
                hdsubtotaldesc1 = hdsubtotal1 - des
                hdtotal1 = hdsubtotaldesc1
                total()
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try

    End Sub
    Private Sub total()
        lblSubTotal.Text = FormatCurrency(hdsubtotal1, 2)
        lblGranTotal.Text = FormatCurrency(hdtotal1, 2)
    End Sub
#End Region

    Private Sub btnAgregarArticulos_Click(sender As Object, e As EventArgs) Handles btnAgregarArticulos.Click
        Try
            Dim idPedido = Request.QueryString("id")
            Dim resultadoValidacion = validararticulo()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim articulo = New CRN.nspDetallePedido.Proceso_ObtenerDetallePedidos() With {.tipoConsulta = tipoConsultaDetallePedido.xPedidoArticulo, .idPedido = Guid.Parse(idPedido), .idArticulo = (Guid.Parse(cmbArticulo.SelectedValue))}.Ejecutar()
            If articulo.Count <> 0 Then
                Throw New Exception("El artículo está duplicado, favor de verificarlo")
            End If

            Dim nuevoDetallePedido As New detallePedido
            nuevoDetallePedido.id = Guid.NewGuid
            nuevoDetallePedido.idPedido = Guid.Parse(Request.QueryString("id"))
            nuevoDetallePedido.idArticulo = Guid.Parse(cmbArticulo.SelectedValue)
            nuevoDetallePedido.cantidad = txbCantidad.Text
            nuevoDetallePedido.precioUnitario = txbPrecio.Text
            nuevoDetallePedido.idUsuarioMovimiento = IdUsuario
            nuevoDetallePedido.ipUsuario = direccionIP
            nuevoDetallePedido.idSistema = sistemaActivo.idSistema

            Dim respuesta = New CRN.nspDetallePedido.Proceso_AgregarDetallePedido() With {.entidad = nuevoDetallePedido}.Ejecutar

            Select Case respuesta.respuesta
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                    limpiarArticulo()
                    poblarDatosArticulos()
                    updateAgregarArticulo.Update()
                    updateCuadroPedido.Update()
                    iva()
                    'total()
                    Dim cont = lsvCuadroPedido.Items.Count()
                    lblTituloCuadroPedido.Text = "Total artículos:" + cont.ToString
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Critico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
    Private Sub poblarDatosArticulos()
        Try

            Dim idPedido = Request.QueryString("id")
            Dim consultaPedido = New CRN.nspPedido.Proceso_ObtenerPedido() With {.id = Guid.Parse(idPedido.ToString)}.Ejecutar()
            Dim listaArticulo = New CRN.nspDetallePedido.Proceso_ObtenerDetallePedidos() With {.tipoConsulta = CES.nspDetallePedido.tipoConsultaDetallePedido.idPedido, .idPedido = Guid.Parse(consultaPedido.id.ToString)}.Ejecutar
            lsvCuadroPedido.DataSource = listaArticulo.ToList
            lsvCuadroPedido.DataBind()
            iva()
            Dim cont = lsvCuadroPedido.Items.Count()
            lblTituloCuadroPedido.Text = "Total artículos:" + cont.ToString
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")

        End Try


    End Sub
    Private Sub poblarDatosPedido()

        Try
            Dim idPedido = Request.QueryString("id").ToString
            Dim consultaPedido = New CRN.nspPedido.Proceso_ObtenerPedido() With {.id = Guid.Parse(idPedido.ToString)}.Ejecutar()
            Dim consultaOficio = New CRN.nspOficio.Proceso_ObtenerUnOficio() With {.id = Guid.Parse(consultaPedido.idOficio.ToString)}.Ejecutar()

            Dim listaPedido = New CRN.nspAfectacionPresupuestal.Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = CES.nspAfectacionPresupuestal.tipoConsultaAfectacionPresupuestal.idPedido, .idPedido = Guid.Parse(idPedido)}.Ejecutar()
            txbTurnoDRM.Text = consultaOficio.turnoDRM.ToString
            txbArea.Text = consultaOficio._area.ToString
            txbFechaElaboracion.Text = FormatDateTime(consultaPedido.fechaElaboracion, DateFormat.ShortDate)
            cmbCargoPresupuestal.SelectedValue = consultaOficio.idCargoPresupuestal.ToString
            cmbPartidaPresupuestal.SelectedValue = consultaPedido.idPartida.ToString
            lblElaboro.Text = NombreUsuario.ToString
            cmbReviso.SelectedValue = consultaPedido.idReviso.ToString
            cmbAutoriza.SelectedValue = consultaPedido.idAutoriza.ToString
            txbObservaciones.Text = consultaPedido.observaciones.ToString
            txbFechaSolicitud.Text = FormatDateTime(consultaPedido.fechaRequerida, DateFormat.ShortDate)
            txbFechaAcordadaEntrega.Text = FormatDateTime(consultaPedido.fechaAcordada, DateFormat.ShortDate)
            txbFechaRecibido.Text = FormatDateTime(consultaPedido.fechaRecibido, DateFormat.ShortDate)
            cmbProveedor.SelectedValue = consultaPedido.idProveedor.ToString
            cmbTipoPago.SelectedValue = consultaPedido.idTipoPago.ToString
            Dim des As Double = consultaPedido.descuento
                txbDescuento.Text = FormatCurrency(des, 2)
            If consultaPedido.iva = True Then
                chkIva.Checked = True
            Else
                chkIva.Checked = False
            End If

            If consultaPedido.estatusPedido = True Then
                chkPedido.Checked = True
            Else
                chkPedido.Checked = False
            End If

            If consultaPedido.verAlmacen = True Then
                chkVerAlmacen.Checked = True
                controles(False)
            Else
                chkVerAlmacen.Checked = False
            End If

            poblarDatosArticulos()
            If listaPedido.count >= 1 Then
                controles(False)
            End If

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try
    End Sub
    Protected Sub controles(bandera As Boolean)


        If bandera = True Then
            txbArea.ReadOnly = False
            txbFechaElaboracion.ReadOnly = False
            txbFechaSolicitud.ReadOnly = False
            txbFechaAcordadaEntrega.ReadOnly = False
            txbFechaRecibido.ReadOnly = False
            txbDescuento.ReadOnly = False
        Else
            txbArea.ReadOnly = True
            txbFechaElaboracion.ReadOnly = True
            txbFechaSolicitud.ReadOnly = True
            txbFechaAcordadaEntrega.ReadOnly = True
            txbFechaRecibido.ReadOnly = True
            txbDescuento.ReadOnly = True
        End If
        cmbCargoPresupuestal.Enabled = bandera
        cmbPartidaPresupuestal.Enabled = bandera
        cmbReviso.Enabled = bandera
        cmbAutoriza.Enabled = bandera
        cmbProveedor.Enabled = bandera
        cmbTipoPago.Enabled = bandera
        btnMostrarAgregarArticulo.Visible = bandera
        lsvCuadroPedido.Enabled = bandera
        btnModificar.Visible = bandera
        chkIva.Enabled = bandera
    End Sub
    Private Sub limpiarArticulo()
        txbCantidad.Text = String.Empty
        txbPrecio.Text = String.Empty
        cmbArticulo.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim idPedido = Request.QueryString("id").ToString


            If lsvCuadroPedido.Items.Count = 0 Then
                    Throw New Exception("Debes ingresar al menos un artículo")
                End If
            Dim resultadoValidacion = validarPedido()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If

            Dim consultaPedido = New CRN.nspPedido.Proceso_ObtenerPedido() With {.id = Guid.Parse(idPedido.ToString)}.Ejecutar()
            Dim consultaOficio = New CRN.nspOficio.Proceso_ObtenerUnOficio() With {.id = Guid.Parse(consultaPedido.idOficio.ToString)}.Ejecutar()

            Dim nuevoOficio As New CES.nspOficio.oficio
            nuevoOficio.id = Guid.Parse(consultaOficio.id.ToString)
            nuevoOficio.idArea = Guid.Parse(consultaOficio.idArea.ToString)
            nuevoOficio.turnoDRM = consultaOficio.turnoDRM.ToString
            nuevoOficio.idCargoPresupuestal = Guid.Parse(cmbCargoPresupuestal.SelectedValue)

            Dim nuevoPedido As New CES.nspPedido.pedido
            nuevoPedido.id = Guid.Parse(consultaPedido.id.ToString)
            nuevoPedido.idOficio = Guid.Parse(consultaPedido.idOficio.ToString)
            nuevoPedido.idAutoriza = Guid.Parse(cmbAutoriza.SelectedValue)
            nuevoPedido.idElaboro = idElaboro
            nuevoPedido.idReviso = Guid.Parse(cmbReviso.SelectedValue)
            nuevoPedido.idProveedor = Guid.Parse(cmbProveedor.SelectedValue)
            nuevoPedido.idAutoriza = Guid.Parse(cmbAutoriza.SelectedValue)
            nuevoPedido.idTipoPago = Guid.Parse(cmbTipoPago.SelectedValue)

            If chkVerAlmacen.Checked = True Then
                If chkPedido.Checked = True Then
                    nuevoPedido.verAlmacen = True
                    nuevoOficio.idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111114") 'pedido
                    nuevoPedido.estatusPedido = True
                Else
                    Throw New Exception("El pedido debe estar al 100%")
                End If
            Else
                nuevoPedido.verAlmacen = False
            End If

            If chkPedido.Checked Then
                nuevoOficio.idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111114") 'pedido
                nuevoPedido.estatusPedido = True
            Else
                nuevoOficio.idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111113") 'prepedido
                nuevoPedido.estatusPedido = False
            End If

            If chkIva.Checked = True Then
                nuevoPedido.iva = True
            Else
                nuevoPedido.iva = False
            End If
            nuevoPedido.idPartida = Guid.Parse(cmbPartidaPresupuestal.SelectedValue)
            nuevoPedido.fechaRequerida = CDate(txbFechaSolicitud.Text)
            nuevoPedido.fechaAcordada = CDate(txbFechaAcordadaEntrega.Text)
            nuevoPedido.fechaRecibido = CDate(txbFechaRecibido.Text)
            nuevoPedido.observaciones = txbObservaciones.Text
            nuevoPedido.numeroPedido = consultaPedido.numeroPedido.ToString
            nuevoPedido.descuento = txbDescuento.Text
            nuevoPedido.idSistema = sistemaActivo.idSistema
            nuevoPedido.ipUsuario = direccionIP
            nuevoPedido.idUsuarioMovimiento = IdUsuario
            Dim respuesta = New CRN.nspPedido.Proceso_ActualizarPedido() With {.entidad = nuevoPedido, .idOficio = nuevoOficio.id, .idEstatusOficio = nuevoOficio.idEstatusOficio, .idCargoPresupuestal = Guid.Parse(cmbCargoPresupuestal.SelectedValue), .idSistema = sistemaActivo.idSistema, .idUsuarioMovimiento = IdUsuario, .ipUsuario = direccionIP}.Ejecutar()
            Select Case respuesta.respuesta
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                        OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "Pedido"), nspPopup.tipoPopup.Verde, True, "management/pedido/reportepedido/frmReportePedido.aspx?idPedido=" + idPedido.ToString)

                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select



        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try
    End Sub
    Protected Sub chkIva_CheckedChanged1(sender As Object, e As EventArgs)
        iva()

    End Sub
    Private Sub txbDescuento_TextChanged(sender As Object, e As EventArgs) Handles txbDescuento.TextChanged
        iva()
    End Sub
    Protected Sub lnkVerStock_Click(sender As Object, e As EventArgs)
        Dim clic As LinkButton = sender
        Dim indice As Integer = clic.TabIndex
        Dim idStckArt = Guid.Parse(clic.CommandName)
        Dim r35 = New Proceso_ObtenerArticulo() With {.id = idStckArt}.Ejecutar()

        Dim lvsPop As New StringBuilder
        lvsPop.Append("<div class='pmo-contact'>")
        lvsPop.Append("<ul>")
        lvsPop.Append("<li class='ng-binding f-16'> <i Class='zmdi zmdi-collection-item-4 zmdi-hc-fw'></i> Existencia:")
        lvsPop.Append(" ")
        lvsPop.Append("<span class='c-teal'>")
        lvsPop.Append(r35.existencia)
        lvsPop.Append(" </span>")
        lvsPop.Append("</li>")
        lvsPop.Append("<li class='ng-binding f-16'> <i Class='zmdi zmdi-dropbox zmdi-hc-fw'></i> Categoría:")
        lvsPop.Append(" ")
        lvsPop.Append(r35.nombreCategoria)
        lvsPop.Append("</li>")
        lvsPop.Append("<li class='ng-binding f-16'> <i class='zmdi zmdi-local-offer zmdi-hc-fw c-green'></i></i> Stock Máximo:")
        lvsPop.Append(" ")
        lvsPop.Append(r35.stockMaximo)
        lvsPop.Append("</li>")
        lvsPop.Append("<li Class='ng-binding f-16'> <i class='zmdi zmdi-local-offer zmdi-hc-fw c-red'></i></i> Stock Mínimo:")
        lvsPop.Append(" ")
        lvsPop.Append(r35.stockMinimo)
        lvsPop.Append("</li>")
        lvsPop.Append("</ul>")
        lvsPop.Append("</div>")

        OnMostrarMensajeAccion(r35.nombre, lvsPop.ToString, nspPopup.tipoPopup.Gris, False, "")
    End Sub
    Protected Sub lnkQuitarTodos_Click(sender As Object, e As EventArgs)
        Try
            Dim btn As LinkButton = sender
            Dim indice As Integer = btn.TabIndex
            Dim idEliminar As Guid = Guid.Parse(btn.CommandArgument)
            'Dim cont = ListView1.Items.Count()
            'For i = cont To ListView1.Items.Count - 1
            Dim respuesta = New CRN.nspDetallePedido.Proceso_EliminarDetallePedido() With {.id = idEliminar, .ipUsuario = direccionIP, .idUsuarioMovimiento = IdUsuario}.Ejecutar
            Select Case respuesta.respuesta
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado

                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Advertencia", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select
            'Next
            poblarDatosArticulos()
            updateAgregarArticulo.Update()
            updateCuadroPedido.Update()


        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Protected Sub lnkQuitar_Click(sender As Object, e As EventArgs)
        Try
            Dim linkButton As LinkButton = sender
            Dim indice As Integer = linkButton.TabIndex
            Dim idEliminar As Guid = Guid.Parse(linkButton.CommandArgument)

            Dim respuesta = New CRN.nspDetallePedido.Proceso_EliminarDetallePedido() With {.id = idEliminar, .ipUsuario = direccionIP, .idUsuarioMovimiento = IdUsuario}.Ejecutar
            Select Case respuesta.respuesta
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                    poblarDatosArticulos()
                    updateAgregarArticulo.Update()
                    updateCuadroPedido.Update()
                    descuento()
                    iva()
                    total()
                    Dim cont = lsvCuadroPedido.Items.Count()
                    lblTituloCuadroPedido.Text = "Total artículos:" + cont.ToString
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Advertencia", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Private Sub chkVerAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles chkVerAlmacen.CheckedChanged
        If chkVerAlmacen.Checked = True Then
            chkPedido.Checked = True
        End If
    End Sub

    Private Sub btnCancelarAddArt_Click(sender As Object, e As EventArgs) Handles btnCancelarAddArt.Click
        divCuadroPedido.Attributes.Remove("class")
        divCuadroPedido.Attributes("class") = "card col-md-12 col-lg-12 card animated bounceInRight"
        divPanelAgregar.Visible = False
        updateCuadroPedido.Update()
    End Sub

    Private Sub txbPrecio_TextChanged(sender As Object, e As EventArgs) Handles txbPrecio.TextChanged
        Try
            Dim val As Double = 0.0001
            If txbPrecio.Text <= val Then
                txbPrecio.Text = ""
                Throw New Exception("Favor de verificar el precio")
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try

    End Sub

    Private Sub chkPedido_CheckedChanged(sender As Object, e As EventArgs) Handles chkPedido.CheckedChanged
        If chkPedido.Checked = False Then
            controles(True)
        End If
    End Sub
End Class
Imports CES, CRN
Imports CRN.nspArea, CRN.nspPartidaPresupuestal, CRN.nspFirma, CRN.nspProveedor, CRN.nspTipoPago, CRN.nspArticulo, CRN.nspDetallePedido, CRN.nspPedido, CRN.nspGenerico
Imports CES.nspPartidaPresupuestal, CES.nspArea, CES.nspFirma, CES.nspProveedor, CES.nspTipoPago, CES.nspArticulo, CES.nspDetallePedido, CES.nspPedido, CES.nspOficio
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmPedidoSinDRM : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim hdsubtotal1 As Double = 0
    Dim hdtotal1 As Double = 0
    Dim hdsubtotaldesc1 As Double = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                txbFechaElaboracion.Text = DateTime.Now.ToString("dd/MM/yyyy")
                lblElaboro.Text = NombreUsuario.ToString
                Dim listadetalle As New List(Of detallePedido)
                Session("listadetalle") = listadetalle
                llenarAreasPrincipales()
                llenarCargoPres()
                llenarPartidaPresupuestal()
                llenarFirmaReviso()
                llenarFirmaAutoriza()
                llenarProveedor()
                llenarTipoPago()
                llenarArticulo()
                conDRM.Visible = False
                sinDRM.Visible = True
                tablatotales.Visible = False
                lineaIva.Visible = False
                lineaDescuento.Visible = False
                lineaGranTotal.Visible = False
                Dim drm = New Proceso_ObtenerDatoGenerico() With {.tipoConsulta = CES.nspGenerico.tipoConsultaGenerico.ultimoTurnoDRM_Especial_SP, .idSistema = sistemaActivo.idSistema}.Ejecutar()
                drmNum.Value = drm.valor.ToString
                txbFolioDocumentoInterno.Text = drm.valor
            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If


    End Sub
#Region "Metodos llenarCombos"

    Protected Sub llenarAreasPrincipales()
        Dim consultaAreasPri = New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbAreaPadre.DataValueField = "id"
        cmbAreaPadre.DataTextField = "nombre"
        cmbAreaPadre.DataSource = consultaAreasPri.OrderBy(Function(a) a.nombre).ToList
        cmbAreaPadre.DataBind()
        cmbAreaPadre.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
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
    'Protected Sub llenarFirmaElaboro()
    '    Dim consultaFirmasElab = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombre, .Nombre = "Elaboró"}.Ejecutar().OrderBy(Function(a) a._nombreUsuario).ToList
    '    cmbElaboro.DataValueField = "id"
    '    cmbElaboro.DataTextField = "_nombreUsuario"
    '    cmbElaboro.DataSource = consultaFirmasElab.ToList
    '    cmbElaboro.DataBind()
    '    cmbElaboro.SelectedValue = "Seleccione un elemento de la lista"
    'End Sub
    Protected Sub llenarFirmaReviso()
        Dim consultaFirmasRev = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Revisó", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(r) r._nombreUsuario).ToList
        cmbReviso.DataValueField = "id"
        cmbReviso.DataTextField = "_nombreUsuario"
        cmbReviso.DataSource = consultaFirmasRev.ToList
        cmbReviso.DataBind()
        cmbReviso.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub llenarFirmaAutoriza()
        Dim consultaFirmasAut = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Autoriza", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(a) a._nombreUsuario).ToList
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

#Region "validar"
    Private Function validararticulo() As Contexto.Notificaciones.controladorMensajes.respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
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

        Catch ex As Exception

        End Try
        Return respuesta
    End Function
    Private Function validarPedido() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)

        Try

            If cmbAreaPadre.SelectedValue = "Seleccione un elemento de la lista" Then
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "área")
                Throw New Exception(respuesta.comentario)
            End If
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


            'If cmbElaboro.SelectedValue = "Seleccione un elemento de la lista" Then
            '    respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            '    respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "elaboró")
            '    Throw New Exception(respuesta.comentario)
            'End If

            If cmbReviso.SelectedValue = "Seleccione un elemento de la lista" Then
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Revisó")
                Throw New Exception(respuesta.comentario)
            End If
            If cmbAutoriza.SelectedValue = "Seleccione un elemento de la lista" Then
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Autoriza")
                Throw New Exception(respuesta.comentario)
            End If

            If txbFechaSolicitud.Text.Length = 0 Then
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Fecha solicitud a proveedor")
                Throw New Exception(respuesta.comentario)
            End If

            If txbFechaAcordadaEntrega.Text.Length = 0 Then
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha acordada de entrega")
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


            'If (Session("listaArticulos").Count = 0) Then
            '    respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            '    respuesta.comentario = " Debes ingresar al menos un artículo"
            '    Throw New Exception(respuesta.comentario)
            'End If

        Catch ex As Exception

        End Try
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

    Protected Sub btnMostrarAgregarArticulo_Click(sender As Object, e As EventArgs)
        divCuadroPedido.Attributes.Remove("class")
        divCuadroPedido.Attributes("class") = "card col-md-8 col-lg-8 card animated bounceInRight"
        divPanelAgregar.Attributes.Remove("class")
        divPanelAgregar.Attributes("class") = "card col-md-4 col-lg-4 card animated bounceInLeft"
        divPanelAgregar.Visible = True
        updateAgregarArticulo.Update()
        updateCuadroPedido.Update()
    End Sub
    Private Sub btnAgregarArticulos_Click(sender As Object, e As EventArgs) Handles btnAgregarArticulos.Click
        Try
            'divCuadroPedido.Attributes.Remove("class")
            'divCuadroPedido.Attributes("class") = "card col-md-12 col-lg-12 card animated bounceInLeft"
            'divPanelAgregar.Visible = False
            tablatotales.Visible = True

            Dim resultadoValidacion = validararticulo()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If

            Try
                Dim listaRemplazar As List(Of detallePedido) = CType(Session("listadetalle"), List(Of detallePedido))
                For i = 0 To listaRemplazar.Count - 1
                    If listaRemplazar(i).idArticulo = Guid.Parse(cmbArticulo.SelectedValue) Then
                        Throw New Exception(" El artículo estaría duplicado.")
                    End If
                Next

                Dim nuevoDetallePedido As New detallePedido
                nuevoDetallePedido.id = Guid.NewGuid
                nuevoDetallePedido.idArticulo = Guid.Parse(cmbArticulo.SelectedValue)
                nuevoDetallePedido.cantidad = txbCantidad.Text
                nuevoDetallePedido.precioUnitario = txbPrecio.Text
                nuevoDetallePedido._articulo = cmbArticulo.SelectedItem.ToString
                nuevoDetallePedido.idUsuarioMovimiento = IdUsuario
                nuevoDetallePedido.ipUsuario = direccionIP
                nuevoDetallePedido.idSistema = sistemaActivo.idSistema

                listaRemplazar.Add(nuevoDetallePedido)
                Session("listadetalle") = listaRemplazar
                lsvCuadroPedido.DataSource = listaRemplazar
                lsvCuadroPedido.DataBind()
                limpiarArticulo()
                updateAgregarArticulo.Update()
                updateCuadroPedido.Update()
                'descuento()
                iva()
                Dim cont = lsvCuadroPedido.Items.Count()
                lblTituloCuadroPedido.Text = "Total artículos:" + cont.ToString

            Catch ex As Exception
                OnMostrarMensajeAccion("Advertencia", "" & ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
            End Try


        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", "" & ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
        End Try
    End Sub
    Private Sub limpiarArticulo()
        txbCantidad.Text = String.Empty
        txbPrecio.Text = String.Empty
        cmbArticulo.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Private Sub validardrm()
        Try
            Dim drm = New Proceso_ObtenerDatoGenerico() With {.tipoConsulta = CES.nspGenerico.tipoConsultaGenerico.ultimoTurnoDRM_Especial_SP, .idSistema = sistemaActivo.idSistema}.Ejecutar().valor.ToString

            If conDRM.Visible = True Then
                If txbTurnoDRM.Text = "" Then
                    Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Turno DRM"))
                End If
                Dim turno = New CRN.nspOficio.Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.porDRM, .turnoDRM = (txbTurnoDRM.Text & (cmbTurnoDRM.SelectedValue)), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.nombre).ToList

                If turno.Count = 0 Then
                    drmNum.Value = (txbTurnoDRM.Text & (cmbTurnoDRM.SelectedValue))
                Else
                    Throw New Exception(" El turno DRM estaría duplicado.")
                End If

            Else
                    drmNum.Value = drm
            End If

        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", "" & ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try

    End Sub
    Private Sub btnCancelarAddArt_Click(sender As Object, e As EventArgs) Handles btnCancelarAddArt.Click
        Try
            Dim listadetalle As List(Of detallePedido) = CType(Session("listadetalle"), List(Of detallePedido))
            If listadetalle.Count = 0 Then
                Throw New Exception(" Debes ingresar al menos un artículo")
            Else
                divCuadroPedido.Attributes.Remove("class")
                divCuadroPedido.Attributes("class") = "card col-md-12 col-lg-12 card animated bounceInLeft"
                divPanelAgregar.Visible = False
                updateAgregarArticulo.Update()
                updateCuadroPedido.Update()
                updateGuardaSub.Update()
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try
    End Sub
    Protected Sub btnDRMNo_Click(sender As Object, e As EventArgs)
        sinDRM.Visible = True
        conDRM.Visible = False
        drmNum.Value = txbFolioDocumentoInterno.Text
    End Sub
    Private Sub lnkCerrar2_Click(sender As Object, e As EventArgs) Handles lnkCerrar2.Click
        mandaDefault()
    End Sub

    Private Sub lnkGuardarSub_Click(sender As Object, e As EventArgs) Handles lnkGuardarSub.Click

        Try
            validardrm()
            Dim resultadoValidacion = validarPedido()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If

            Dim listadetalle As List(Of detallePedido) = CType(Session("listadetalle"), List(Of detallePedido))

            If listadetalle.Count = 0 Then
                Throw New Exception(" Debes ingresar al menos un artículo")
            End If
            Dim nuevoOficio As New oficio
            nuevoOficio.id = Guid.NewGuid
            nuevoOficio.idArea = Guid.Parse(cmbAreaPadre.SelectedValue)
            nuevoOficio.turnoDRM = drmNum.Value
            nuevoOficio.idCargoPresupuestal = Guid.Parse(cmbCargoPresupuestal.SelectedValue)
            nuevoOficio.esDocInterno = True

            Dim nuevoPedido As New pedido
            nuevoPedido.id = Guid.NewGuid
            nuevoPedido.idOficio = nuevoOficio.id
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
            If chkPedido.Checked = True Then
                nuevoPedido.estatusPedido = True
                nuevoOficio.idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111114") 'pedido
            Else
                nuevoPedido.estatusPedido = False
                nuevoOficio.idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111113") 'prepedido
            End If

            If chkIva.Checked = True Then
                nuevoPedido.iva = True
            Else
                nuevoPedido.iva = False
            End If
            nuevoPedido.idPartida = Guid.Parse(cmbPartidaPresupuestal.SelectedValue)
            nuevoPedido.fechaRequerida = txbFechaSolicitud.Text
            nuevoPedido.fechaAcordada = txbFechaAcordadaEntrega.Text
            nuevoPedido.fechaRecibido = txbFechaRecibido.Text
            nuevoPedido.observaciones = txbObservaciones.Text
            Dim numPedido = New Proceso_ObtenerDatoGenerico() With {.tipoConsulta = CES.nspGenerico.tipoConsultaGenerico.ultimoNumeroPedido, .idSistema = sistemaActivo.idSistema}.Ejecutar()
            nuevoPedido.numeroPedido = numPedido.valor.ToString
            nuevoPedido.descuento = txbDescuento.Text
            Dim respuesta = New CRN.nspPedido.Proceso_AgregarPedidoSinDRM() With {.entidad = nuevoPedido, .listaDetallesPedido = listadetalle, .oficio = nuevoOficio, .idSistema = sistemaActivo.idSistema, .idUsuarioMovimiento = IdUsuario, .ipUsuario = direccionIP}.Ejecutar()
            Select Case respuesta.respuesta
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Pedido sin DRM"), nspPopup.tipoPopup.Verde, True, "management/pedido/reportepedido/frmReportePedido.aspx?idPedido=" + nuevoPedido.id.ToString)
                    Session.Remove("listadetalle")

                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try


    End Sub
    Protected Sub chkIva_CheckedChanged1(sender As Object, e As EventArgs)
        iva()

    End Sub
    Private Sub txbDescuento_TextChanged(sender As Object, e As EventArgs) Handles txbDescuento.TextChanged

        iva()
    End Sub
    Protected Sub btnDRMEditar_Click(sender As Object, e As EventArgs)
        sinDRM.Visible = False
        conDRM.Visible = True
    End Sub

    Protected Sub lnkQuitarTodos_Click(sender As Object, e As EventArgs)
        Dim lista As New List(Of detallePedido)
        Session("listaArticulos") = lista
        lsvCuadroPedido.Items.Clear()
        lsvCuadroPedido.DataSource = lista
        lsvCuadroPedido.DataBind()
        updateCuadroPedido.Update()
        updateAgregarArticulo.Update()
        lblDescuento.Text = 0
        lblIva.Text = 0
        lblGranTotal.Text = 0
        lblSubTotal.Text = 0
        tablatotales.Visible = False
        lineaGranTotal.Visible = False
        lineaIva.Visible = False
        lineaDescuento.Visible = False
        lblTituloCuadroPedido.Text = "Total artículos:0"

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

    Protected Sub lnkQuitar_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        Dim indice As Integer = btn.TabIndex

        Dim listadetalle As List(Of detallePedido) = CType(Session("listadetalle"), List(Of detallePedido))
        listadetalle.RemoveAt(indice)
        Session("listadetalle") = listadetalle
        lsvCuadroPedido.DataSource = listadetalle
        lsvCuadroPedido.DataBind()
        descuento()
        iva()
        total()

        Dim cont = lsvCuadroPedido.Items.Count()
        lblTituloCuadroPedido.Text = "Total artículos:" + cont.ToString

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

    Private Sub txbTurnoDRM_TextChanged(sender As Object, e As EventArgs) Handles txbTurnoDRM.TextChanged
        If txbTurnoDRM.Text = "" Then
            Exit Sub
        Else
            If IsNumeric(txbTurnoDRM.Text) Then
                txbTurnoDRM.Text = String.Format("{0:D6}", CInt(txbTurnoDRM.Text))
            Else
                txbTurnoDRM.Text = drmNum.Value
                txbFolioDocumentoInterno.Text = drmNum.Value
            End If
        End If
    End Sub
End Class
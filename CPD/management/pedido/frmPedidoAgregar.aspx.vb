Imports CRN.nspArea, CRN.nspPedido, CRN.nspOficio, CRN.nspPartidaPresupuestal, CRN.nspFirma, CRN.nspProveedor, CRN.nspTipoPago, CES.nspDetallePedido, CRN.nspArticulo, CRN.nspComprobacion, CES.nspComprobacion, CES.nspTipoPago
Imports CES
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Globalization

Public Class frmPedidoAgregar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                txbDescuento.Text = String.Empty
                lblElaboro.Text = NombreUsuario
                Dim listaArticulos As New List(Of detallePedido)
                Session("listaArticulos") = listaArticulos
                divPanelAgregar.Visible = False
                controladorOperacioneslineales("")
                llenasDrops()
                llenarBoxesDefault()
            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "Metodos"
    Public Sub llenarBoxesDefault()
        Dim idx As String = Request.QueryString("idOficio")
        Dim oficio = New Proceso_ObtenerUnOficio() With {.id = Guid.Parse(idx)}.Ejecutar()
        lblTitulo.Text = String.Empty
        If Not oficio.turnoSAF Is Nothing Then
            lblTitulo.Text = "Agrega un pedido al turno SAF: " + oficio.turnoSAF + " y turno DRM: " + oficio.turnoDRM
        Else
            lblTitulo.Text = "Agrega un pedido"
        End If
        lblTurnoDRM.Text = oficio.turnoDRM
        hdfArea.Value = oficio.idArea.ToString
        lblFechaElaboracion.Text = oficio.fechaCaptura
        lblArea.Text = oficio._area
        If Not oficio.idCargoPresupuestal Is Nothing Then
            cmbCargoPresupuestal.SelectedValue = oficio.idCargoPresupuestal.ToString
        End If
    End Sub
    Protected Sub limpiarControles()
        cmbCargoPresupuestal.ClearSelection()
        cmbPartidaPresupuestal.ClearSelection()
        'cmbElaboro.ClearSelection()
        cmbReviso.ClearSelection()
        cmbAutoriza.ClearSelection()
        txbFechaSolicitud.Text = String.Empty
        txbFechaAcordadaEntrega.Text = String.Empty
        txbFechaRecibido.Text = String.Empty
        cmbProveedor.ClearSelection()
        cmbTipoPago.ClearSelection()
        chkDoMaths.Checked = False
        chkPedido.Checked = False
        chkVerAlmacen.Checked = False
        txbDescuento.Text = String.Empty
        txbObservaciones.Text = String.Empty
        cmbArticulo.ClearSelection()
        txbPrecio.Text = String.Empty
        txbCantidad.Text = String.Empty
    End Sub
    Protected Sub limpiarArticulos()
        cmbArticulo.ClearSelection()
        txbPrecio.Text = String.Empty
        txbCantidad.Text = String.Empty
    End Sub
    Public Sub llenasDrops()
        Dim cargo = New Proceso_ObtenerAreas() With {.tipoConsulta = nspArea.tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbCargoPresupuestal.DataSource = cargo
        cmbCargoPresupuestal.DataTextField = "nombre"
        cmbCargoPresupuestal.DataValueField = "id"
        cmbCargoPresupuestal.DataBind()
        Dim partida = New Proceso_ObtenerPartidasPresupuestales() With {.tipoConsulta = nspPartidaPresupuestal.tipoConsultaPartidaPresupuestal.todos}.Ejecutar().OrderBy(Function(o) o.nombre).ToList
        cmbPartidaPresupuestal.DataSource = partida
        cmbPartidaPresupuestal.DataTextField = "nombre"
        cmbPartidaPresupuestal.DataValueField = "id"
        cmbPartidaPresupuestal.DataBind()
        'Dim elabora = New Proceso_ObtenerFirmas() With {.tipoConsulta = nspFirma.tipoConsultaFirma.nombre, .Nombre = "Elaboró"}.Ejecutar().OrderBy(Function(r) r._nombreUsuario).ToList
        'cmbElaboro.DataSource = elabora
        'cmbElaboro.DataTextField = "_nombreUsuario"
        'cmbElaboro.DataValueField = "id"
        'cmbElaboro.DataBind()
        Dim revisa = New Proceso_ObtenerFirmas() With {.tipoConsulta = nspFirma.tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Revisó", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(w) w._nombreUsuario).ToList
        cmbReviso.DataSource = revisa
        cmbReviso.DataTextField = "_nombreUsuario"
        cmbReviso.DataValueField = "id"
        cmbReviso.DataBind()
        Dim autoriza = New Proceso_ObtenerFirmas() With {.tipoConsulta = nspFirma.tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Autoriza", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(ew) ew._nombreUsuario).ToList
        cmbAutoriza.DataSource = autoriza
        cmbAutoriza.DataTextField = "_nombreUsuario"
        cmbAutoriza.DataValueField = "id"
        cmbAutoriza.DataBind()
        Dim proveedor = New Proceso_ObtenerProveedores() With {.tipoConsulta = nspProveedor.tipoConsultaProveedor.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(l) l.nombre).ToList
        cmbProveedor.DataSource = proveedor
        cmbProveedor.DataTextField = "nombre"
        cmbProveedor.DataValueField = "id"
        cmbProveedor.DataBind()

        Dim comprobacion = New Proceso_ObtenerComprobaciones() With {.tipoConsulta = tipoConsultaComprobacion.idOficio, .idOficio = Guid.Parse(Request.QueryString("idOficio"))}.Ejecutar()
        Dim listaTipoPago = New Proceso_ObtenerTiposPagos() With {.tipoConsulta = nspTipoPago.tipoConsultaTipoPago.todos}.Ejecutar()
        Dim lista = New List(Of tipoPago)
        If comprobacion.Count > 0 Then
            For i = 0 To listaTipoPago.Count - 1
                If listaTipoPago(i).id.ToString <> "71747111-2222-3333-4444-111111111112" Then
                    lista.Add(listaTipoPago(i))
                End If
            Next
        Else
            lista = listaTipoPago
        End If
        cmbTipoPago.DataSource = lista
        cmbTipoPago.DataTextField = "nombre"
        cmbTipoPago.DataValueField = "id"
        cmbTipoPago.DataBind()
        Dim articulin = New Proceso_ObtenerArticulos() With {.tipoConsulta = .tipoConsulta.todos, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar().OrderBy(Function(x) x.nombre).ToList
        cmbArticulo.DataSource = articulin
        cmbArticulo.DataTextField = "nombre"
        cmbArticulo.DataValueField = "id"
        cmbArticulo.DataBind()

    End Sub

#End Region
#Region "Funciones"
    Protected Function controladorVistas(vst As String)
        divCuadroPedido.Visible = False
        divPanelAgregar.Visible = False
        btnMostrarAgregarArticulo.Visible = False
        Select Case vst
            Case "vst_all"
                divCuadroPedido.Visible = True
                divPanelAgregar.Visible = True
                btnMostrarAgregarArticulo.Visible = True
            Case "vst_Ps"
                divCuadroPedido.Visible = True
                divPanelAgregar.Visible = True
            Case "vst_CP"
                divCuadroPedido.Visible = True
            Case "vst_PA"
                divPanelAgregar.Visible = True
            Case "vst_btn+"
                btnMostrarAgregarArticulo.Visible = True
            Case "vst_CP+"
                divCuadroPedido.Visible = True
                btnMostrarAgregarArticulo.Visible = True
            Case "vst_PA+"
                divPanelAgregar.Visible = True
                btnMostrarAgregarArticulo.Visible = True

        End Select
        Return vst
    End Function
    Protected Function controladorAnimacionesTerrenales(anmt As String)

        Select Case anmt
            Case "CP8"
                divCuadroPedido.Attributes.Remove("class")
                divCuadroPedido.Attributes("class") = "card col-md-8 animated wooble"
                divPanelAgregar.Attributes.Remove("class")
                divPanelAgregar.Attributes("class") = "card col-md-4 animated bounceInLeft"
            Case "CP12"
                divCuadroPedido.Attributes.Remove("class")
                divCuadroPedido.Attributes("class") = "card col-md-12 animated wooble"
        End Select
        Return anmt
    End Function
    Protected Function controladorOperacioneslineales(axn As String)
        lineaSubTotal.Visible = True
        lineaIva.Visible = False
        lineaDescuento.Visible = False
        lineaGranTotal.Visible = False
        Select Case axn

            Case "C1"
                lineaGranTotal.Visible = True
                lineaDescuento.Visible = False
                lineaIva.Visible = False
            Case "C2"
                lineaIva.Visible = True
                lineaGranTotal.Visible = True
                lineaDescuento.Visible = False
            Case "C3"
                lineaDescuento.Visible = True
                lineaGranTotal.Visible = True
                lineaIva.Visible = False
            Case "C4"
                lineaIva.Visible = True
                lineaDescuento.Visible = True
                lineaGranTotal.Visible = True
            Case "CT"
                lineaSubTotal.Visible = False
                lineaGranTotal.Visible = True
            Case "CI"
                lineaSubTotal.Visible = True
                lineaIva.Visible = False
                lineaDescuento.Visible = False
                lineaGranTotal.Visible = False
            Case Else
                lineaSubTotal.Visible = False
                lineaIva.Visible = False
                lineaDescuento.Visible = False
                lineaGranTotal.Visible = False
        End Select
        Return axn
    End Function
    Private Function validararticulo() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If cmbArticulo.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "Debes seleccionar un articulo de la lista"
            Throw New Exception(respuesta.comentario)
        End If
        If txbCantidad.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La cantidad es obligatoria"
            Throw New Exception(respuesta.comentario)

        End If
        If txbPrecio.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo precio del artículo es obligatorio"
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
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Fecha solicitud")
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



        If CDate(txbFechaSolicitud.Text) > CDate(txbFechaAcordadaEntrega.Text) Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha de solicitud debe ser menor o igual a la fecha acordada."
            Throw New Exception(respuesta.comentario)
        End If

        If CDate(txbFechaAcordadaEntrega.Text) > CDate(txbFechaRecibido.Text) Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha acordada debe ser menor o igual a la fecha de recibido"
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

        If (Session("listaArticulos").Count = 0) Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = " Debes ingresar al menos un artículo"
            Throw New Exception(respuesta.comentario)
        End If

        Return respuesta
    End Function
    Private Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function
#End Region
#Region "botones"
    Protected Sub btnMostrarAgregarArticulo_Click(sender As Object, e As EventArgs)
        controladorVistas("vst_Ps")
        controladorAnimacionesTerrenales("CP8")
        updateAgregarArticulo.Update()
        updateCuadroPedido.Update()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Try
            Dim resultadoValidacion = validarPedido()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If

            Dim listaArticulos As List(Of detallePedido) = CType(Session("listaArticulos"), List(Of detallePedido))

            Dim x7lofc As New nspOficio.oficio
            x7lofc.id = Guid.Parse(Request.QueryString("idOficio"))
            x7lofc.idCargoPresupuestal = Guid.Parse(cmbCargoPresupuestal.SelectedValue)

            If chkPedido.Checked Then
                x7lofc.idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111114")
            Else
                x7lofc.idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111113")
            End If
            x7lofc.idArea = Guid.Parse(hdfArea.Value)
            x7lofc.turnoDRM = lblTurnoDRM.Text
            Dim x8lpd = New nspPedido.pedido
            x8lpd.id = Guid.NewGuid()
            x8lpd.idOficio = Guid.Parse(Request.QueryString("idOficio"))
            x8lpd.idAutoriza = Guid.Parse(cmbAutoriza.SelectedValue)
            x8lpd.idElaboro = idElaboro
            x8lpd.idReviso = Guid.Parse(cmbReviso.SelectedValue)
            x8lpd.idProveedor = Guid.Parse(cmbProveedor.SelectedValue)
            x8lpd.idPartida = Guid.Parse(cmbPartidaPresupuestal.SelectedValue)
            x8lpd.iva = chkDoMaths.Checked
            x8lpd.idTipoPago = Guid.Parse(cmbTipoPago.SelectedValue)
            x8lpd.estatusPedido = chkPedido.Checked
            x8lpd.verAlmacen = chkVerAlmacen.Checked
            x8lpd.fechaRequerida = txbFechaSolicitud.Text
            x8lpd.fechaAcordada = txbFechaAcordadaEntrega.Text
            x8lpd.fechaRecibido = txbFechaRecibido.Text
            x8lpd.estatusPedido = chkPedido.Checked

            x8lpd.observaciones = txbObservaciones.Text.Trim()

            If txbDescuento.Text.Length > 0 Then
                x8lpd.descuento = txbDescuento.Text
            Else
                x8lpd.descuento = 0
            End If

            Dim respuesta = New Proceso_AgregarPedido() With {
            .entidad = x8lpd,
            .oficio = x7lofc,
            .idSistema = sisActivo.sistemaActivo.idSistema,
            .idUsuarioMovimiento = IdUsuario,
            .ipUsuario = getMacAddress(),
            .listaDetallesPedido = listaArticulos
            }.Ejecutar()

            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    Session.Remove("listaArticulos")
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_guardaron, "El pedido se ha guardado correctamente"), nspPopup.tipoPopup.Verde, True, "management/pedido/reportePedido/frmReportePedido.aspx?idPedido=" + x8lpd.id.ToString)
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        limpiarControles()
        Session.Remove("listaArticulos")
        mandaDefault()
    End Sub

    Public Sub btnArticulos_Click(sender As Object, e As EventArgs)
        controladorAnimacionesTerrenales("CP8")

        Try
            Dim resultadoValidacion = validararticulo()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
                'OnMostrarMensajeAccion("Advertencia", resultadoValidacion.comentario, nspPopup.tipoPopup.Naranja, False, "")
            Else
                Dim listaRemplazar As List(Of detallePedido) = CType(Session("listaArticulos"), List(Of detallePedido))

                For v = 0 To listaRemplazar.Count - 1
                    If listaRemplazar(v).idArticulo = Guid.Parse(cmbArticulo.SelectedValue) Then
                        Throw New Exception(" El artículo está duplicado.")
                    End If
                Next

                Dim mc = getMacAddress()
                Dim nuevoArticulo As New detallePedido

                nuevoArticulo.id = Guid.NewGuid
                nuevoArticulo.idArticulo = Guid.Parse(cmbArticulo.SelectedValue)
                nuevoArticulo._articulo = cmbArticulo.SelectedItem.Text
                nuevoArticulo.cantidad = txbCantidad.Text
                nuevoArticulo.precioUnitario = txbPrecio.Text
                nuevoArticulo.idUsuarioMovimiento = IdUsuario
                nuevoArticulo.ipUsuario = mc
                nuevoArticulo.idSistema = sisActivo.sistemaActivo.idSistema
                nuevoArticulo._total = txbCantidad.Text * txbPrecio.Text

                ' Dim CSubTotalxLinea = nuevoArticulo._total

                If txbDescuento.Text = String.Empty Then
                    txbDescuento.Text = 0
                End If

                ' Dim TxtDescuento = txbDescuento.Text

                listaRemplazar.Add(nuevoArticulo)
                Session("listaArticulos") = listaRemplazar
                lsvCuadroPedido.DataSource = listaRemplazar
                lsvCuadroPedido.DataBind()
                updateCuadroPedido.Update()

                If listaRemplazar.Count > 0 Then
                    lblTituloCuadroPedido.Text = "Total de " + listaRemplazar.Count.ToString + " artículos"
                End If


                operacionTerrenal()
                limpiarArticulos()
                updateAgregarArticulo.Update()
                updateCuadroPedido.Update()

                If listaRemplazar.Count >= 1 Then
                    controladorOperacioneslineales("C1")
                Else
                    controladorOperacioneslineales("")
                End If
                updateCuadroPedido.Update()

            End If


        Catch ex As Exception
            OnMostrarMensajeAccion("Critico", "" & ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
        End Try


    End Sub

    Protected Sub btnCancelarArticulos_Click(sender As Object, e As EventArgs)
        limpiarArticulos()
        controladorVistas("vst_CP+")
        controladorAnimacionesTerrenales("CP12")
        operacionTerrenal()
        updateAgregarArticulo.Update()
        updateCuadroPedido.Update()
    End Sub
    Protected Sub lnkQuitarTodos_Click(sender As Object, e As EventArgs)
        Dim lista As New List(Of detallePedido)
        Session("listaArticulos") = lista
        lsvCuadroPedido.Items.Clear()
        lsvCuadroPedido.DataSource = lista
        lsvCuadroPedido.DataBind()
        controladorOperacioneslineales("")
        limpiarArticulos()

        controladorVistas("vst_CP+")
        controladorAnimacionesTerrenales("CP12")
        If lista.Count > 0 Then
            lblTituloCuadroPedido.Text = "Total de artículos:" + lista.Count.ToString
        Else
            lblTituloCuadroPedido.Text = "La lista de pedidos se ha vaciado correctamente"
        End If
        updateCuadroPedido.Update()
        updateAgregarArticulo.Update()

    End Sub

    Protected Sub chkDoMaths_CheckedChanged(sender As Object, e As EventArgs)
        controladorOperacioneslineales("CI")
        operacionTerrenal()
        updateCuadroPedido.Update()
    End Sub

    Private Sub operacionTerrenal()

        Dim listaRemplazar As List(Of detallePedido) = CType(Session("listaArticulos"), List(Of detallePedido))
        Dim JgTtl As Double = 0
        Dim JsTtl As Double = 0
        Dim JtTlIv As Double = 0
        Dim JtTlDsct As Double = 0
        Dim valorIVA = New CRN.nspIva.Proceso_ObtenerIva() With {.fecha = Date.Now()}.Ejecutar
        If chkDoMaths.Checked = True Then
            If (listaRemplazar.Count() > 0) Then
                JsTtl = listaRemplazar.Sum(Function(s) s._total)
                If (txbDescuento.Text <> "" And txbDescuento.Text <> "0") Then
                    JtTlDsct = (Double.Parse(txbDescuento.Text))
                End If
                If (JtTlDsct > 0) Then
                    controladorOperacioneslineales("C4")
                    JtTlIv = (JsTtl - JtTlDsct) * valorIVA
                Else
                    JtTlIv = JsTtl * valorIVA
                    controladorOperacioneslineales("C2")
                End If
                JgTtl = JsTtl - JtTlDsct + JtTlIv
                lblDescuento.Text = "$" + (JtTlDsct.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblIva.Text = "$" + (JtTlIv.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblGranTotal.Text = "$" + (JgTtl.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblSubTotal.Text = "$" + (JsTtl.ToString("0,0.00", CultureInfo.InvariantCulture))
            End If
            updateCuadroPedido.Update()
        Else
            If (listaRemplazar.Count() > 0) Then
                JsTtl = listaRemplazar.Sum(Function(s) s._total)
                If (txbDescuento.Text <> "" And txbDescuento.Text <> "0") Then
                    JtTlDsct = (Double.Parse(txbDescuento.Text))
                    controladorOperacioneslineales("C3")
                Else
                    controladorOperacioneslineales("C1")
                End If
                JgTtl = JsTtl - JtTlDsct
                lblDescuento.Text = "$" + (JtTlDsct.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblIva.Text = "$" + (JtTlIv.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblGranTotal.Text = "$" + (JgTtl.ToString("0,0.00", CultureInfo.InvariantCulture))
                lblSubTotal.Text = "$" + (JsTtl.ToString("0,0.00", CultureInfo.InvariantCulture))
            End If
            updateCuadroPedido.Update()
        End If
    End Sub

    Protected Sub txbDescuento_TextChanged(sender As Object, e As EventArgs) Handles txbDescuento.TextChanged
        If txbDescuento.Text IsNot Nothing Then
            If txbDescuento.Text = String.Empty Then
                txbDescuento.Text = 0
            End If
            operacionTerrenal()

        End If
        updateCuadroPedido.Update()
    End Sub

    Protected Sub lnkQuitarArticulo_Click(sender As Object, e As EventArgs)
        Dim clic As LinkButton = sender
        Dim indice As Integer = clic.TabIndex
        Dim listaArticulos As List(Of detallePedido) = CType(Session("listaArticulos"), List(Of detallePedido))
        listaArticulos.RemoveAt(indice)
        Session("listaArticulos") = listaArticulos

        lsvCuadroPedido.DataSource = listaArticulos
        lsvCuadroPedido.DataBind()
        Dim r34 = lsvCuadroPedido.Items.Count()
        lblTituloCuadroPedido.Text = "Total de artículos:" + r34.ToString

        If listaArticulos.Count() = 0 Then
            controladorAnimacionesTerrenales("CP12")
            divPanelAgregar.Visible = False
            chkDoMaths.Checked = False
            operacionTerrenal()
            controladorOperacioneslineales("")
            lblTituloCuadroPedido.Text = "Agrega artículos al pedido"
            updateAgregarArticulo.Update()
            btnMostrarAgregarArticulo.Visible = True
            updateCuadroPedido.Update()
            '  btnAgregar.Visible = False
        Else
            controladorAnimacionesTerrenales("CP8")
            operacionTerrenal()
            updateCuadroPedido.Update()
            updateAgregarArticulo.Update()
        End If
    End Sub

    'Protected Sub lnkVerProveedor_Click(sender As Object, e As EventArgs)
    '    Dim x = Guid.Parse(cmbProveedor.SelectedValue)
    '    Dim y = New Proceso_ObtenerProveedor With {.id = x}.Ejecutar()
    '    lblProveedor = y.nombre
    '    lblRFC = y.rfc
    '    lblGiro = y.giro
    'End Sub

    Protected Sub lnkverstock_click(sender As Object, e As EventArgs)
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



#End Region
    'btnMostrarAgregarArticulo_Click

End Class
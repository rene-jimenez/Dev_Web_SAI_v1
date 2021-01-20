Public Class Site : Inherits System.Web.UI.MasterPage
    Dim paginaBase As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Header.DataBind()
        If Not IsPostBack Then
            lblAñoSistema.Text = paginaBase.sistemaActivo.año
            lblNombreSistema.Text = paginaBase.sistemaActivo.nombre
            lblNombreUsuario.Text = paginaBase.NombreUsuario
            imgAvatar.ImageUrl = "~/img/perfiles/" + paginaBase.ImagenUsuario.ToString
            Dim z = paginaBase.idElaboro

            construirMenu()
        End If
    End Sub
    Private Sub btnMenuArribaCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnMenuArribaCerrarSesion.Click
        paginaBase.CerrarSesion()
    End Sub
#Region "leftMenu"
    Private Sub btnAltaUsuario_Click(sender As Object, e As EventArgs) Handles btnAltaUsuario.Click
        Response.Redirect("~/management/usuario/frmAltaUsuario.aspx?band=add")
    End Sub

    Private Sub btnConsultaUsuario_Click(sender As Object, e As EventArgs) Handles btnConsultaUsuario.Click
        Response.Redirect("~/management/usuario/frmConsultaUsuario.aspx")
    End Sub

    Private Sub btnRol_Click(sender As Object, e As EventArgs) Handles btnRol.Click
        Response.Redirect("~/management/rol/frmRol.aspx")
    End Sub
    Private Sub btnPermiso_Click(sender As Object, e As EventArgs) Handles btnPermiso.Click
        Response.Redirect("~/management/permiso/frmPermiso.aspx")
    End Sub
    Private Sub btnPerfilUsuarioMenuIzquierdo_Click(sender As Object, e As EventArgs) Handles btnPerfilUsuarioMenuIzquierdo.Click
        Response.Redirect("~/management/usuario/frmPerfil.aspx")
    End Sub

    Private Sub btnPagina_Click(sender As Object, e As EventArgs) Handles btnPagina.Click
        Response.Redirect("~/management/pagina/frmPagina.aspx")
    End Sub

#End Region
    Private Sub btnCerrarSesionLeft_Click(sender As Object, e As EventArgs) Handles btnCerrarSesionLeft.Click
        paginaBase.CerrarSesion()
    End Sub

    Private Sub btnAltaArea_Click(sender As Object, e As EventArgs) Handles btnAltaArea.Click
        Response.Redirect("~/management/area/frmArea.aspx?band=add")
    End Sub
    Private Sub btnConsultarArea_Click(sender As Object, e As EventArgs) Handles btnConsultarArea.Click
        Response.Redirect("~/management/area/frmConsultaArea.aspx")
    End Sub

    Private Sub btnAltaProveedor_Click(sender As Object, e As EventArgs) Handles btnAltaProveedor.Click
        Response.Redirect("~/management/proveedor/frmProveedor.aspx?band=add")
    End Sub
    Private Sub btnConsultarProveedor_Click(sender As Object, e As EventArgs) Handles btnConsultarProveedor.Click
        Response.Redirect("~/management/proveedor/frmConsultaProveedor.aspx")
    End Sub

    Private Sub btnAltaArticulo_Click(sender As Object, e As EventArgs) Handles btnAltaArticulo.Click
        Response.Redirect("~/management/catalogos/articulo/frmBusquedaArticulo.aspx")
    End Sub
    Private Sub btnConsultarArticulo_Click(sender As Object, e As EventArgs) Handles btnConsultarArticulo.Click
        Response.Redirect("~/management/catalogos/articulo/frmPrincipalArticulo.aspx")
    End Sub

    'Private Sub btnResponsable_Click(sender As Object, e As EventArgs) Handles btnResponsable.Click
    '    Response.Redirect("~/management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=Responsable")
    'End Sub
    'Private Sub btnFirmas_Click(sender As Object, e As EventArgs) Handles btnFirmas.Click
    '    Response.Redirect("~/management/catalogos/Firmas/frmFirma.aspx")
    'End Sub
    'Private Sub btnCategoria_Click(sender As Object, e As EventArgs) Handles btnCategoria.Click
    '    Response.Redirect("~/management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=Categoria")
    'End Sub
    'Private Sub btnDocContable_Click(sender As Object, e As EventArgs) Handles btnDocContable.Click
    '    Response.Redirect("~/management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=DocumentoContable")
    'End Sub
    'Private Sub btnUnidadMedida_Click(sender As Object, e As EventArgs) Handles btnUnidadMedida.Click
    '    Response.Redirect("~/management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=UnidadMedida")
    'End Sub
    'Private Sub btnTipoPago_Click(sender As Object, e As EventArgs) Handles btnTipoPago.Click
    '    Response.Redirect("~/management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=TipoPago")
    'End Sub
    'Private Sub btnEstatusOficio_Click(sender As Object, e As EventArgs) Handles btnEstatusOficio.Click
    '    Response.Redirect("~/management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=EstatusOficio")
    'End Sub

    'Private Sub btnPartidaPresupuestal_Click(sender As Object, e As EventArgs) Handles btnPartidaPresupuestal.Click
    '    Response.Redirect("~/management/catalogos/partidaPresupuestal/frmPrincipalpartidaPresupuestal.aspx")
    'End Sub
#Region "Oficio"
    Private Sub btnAltaOficio_Click(sender As Object, e As EventArgs) Handles btnAltaOficio.Click
        Response.Redirect("~/management/oficio/frmAltaOficio.aspx")
    End Sub
    Private Sub btnConsultarOficio_Click(sender As Object, e As EventArgs) Handles btnConsultarOficio.Click
        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=Consultar")
    End Sub
    Private Sub btnComplementarOficio_Click(sender As Object, e As EventArgs) Handles btnComplementarOficio.Click
        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=Complementar")
    End Sub
    Private Sub btnEditarOficio_Click(sender As Object, e As EventArgs) Handles btnEditarOficio.Click
        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=Editar")
    End Sub
#End Region
    Private Sub btnRubros_Click(sender As Object, e As EventArgs) Handles btnRubros.Click
        Response.Redirect("~/management/catalogos/rubros/frmPrincipalRubros.aspx")
    End Sub
#Region "Historial"

#End Region

#Region "Pedido"
    Private Sub btnAgregarPedido_Click(sender As Object, e As EventArgs) Handles btnAgregarPedido.Click
        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=PedidoAgregar")
    End Sub

    Private Sub btnAgregarSinDRMPedido_Click(sender As Object, e As EventArgs) Handles btnAgregarSinDRMPedido.Click
        Response.Redirect("~/management/pedido/frmPedidoSinDRM.aspx")
    End Sub

    Private Sub btnEditarPedido_Click(sender As Object, e As EventArgs) Handles btnEditarPedido.Click
        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=PedidoEditar")
    End Sub

    Private Sub btnConsultarPedido_Click(sender As Object, e As EventArgs) Handles btnConsultarPedido.Click
        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=PedidoConsultar")
    End Sub


#End Region
#Region "Solicitud gasto"
    'Private Sub btnAgregarSolicitudGasto_Click(sender As Object, e As EventArgs) Handles btnAgregarSolicitudGasto.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=SolicitudGastoAgregar")
    'End Sub

    'Private Sub btnAgregarSolicitudConDocumentoInterno_Click(sender As Object, e As EventArgs) Handles btnAgregarSolicitudConDocumentoInterno.Click
    '    Response.Redirect("~/management/solicitudGasto/frmSolicitudDocumentoInterno.aspx")
    'End Sub

    'Private Sub btnEditarSolicitudGasto_Click(sender As Object, e As EventArgs) Handles btnEditarSolicitudGasto.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=SolicitudGastoEditar")
    'End Sub

    'Private Sub btnActualizarSolicitudGasto_Click(sender As Object, e As EventArgs) Handles btnActualizarSolicitudGasto.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=SolicitudGastoActualizar")
    'End Sub

    'Private Sub btnCancelarSolicitudGasto_Click(sender As Object, e As EventArgs) Handles btnCancelarSolicitudGasto.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=SolicitudGastoCancelar")
    'End Sub

    'Private Sub btnConsultarSolicitudGasto_Click(sender As Object, e As EventArgs) Handles btnConsultarSolicitudGasto.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=SolicitudGastoConsultar")
    'End Sub



#End Region
#Region "Afectación presupuestal"
    'Private Sub btnAgregarAfectacion_Click(sender As Object, e As EventArgs) Handles btnAgregarAfectacion.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=afectacionAgregar")
    'End Sub

    'Private Sub btnEditarAfectacion_Click(sender As Object, e As EventArgs) Handles btnEditarAfectacion.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=AfectacionPresupuestalEditar")
    'End Sub

    'Private Sub btnSustituirAfectacion_Click(sender As Object, e As EventArgs) Handles btnSustituirAfectacion.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=AfectacionPresupuestalSustituir")
    'End Sub
    'Private Sub btnConsultarAfectacion_Click(sender As Object, e As EventArgs) Handles btnConsultarAfectacion.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=AfectacionPresupuestalConsultar")
    'End Sub

#End Region
#Region "alcance"

    'Private Sub btnAgregarAlcance_Click(sender As Object, e As EventArgs) Handles btnAgregarAlcance.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=alcanceAgregar")
    'End Sub
    'Private Sub btnEditarAlcance_Click(sender As Object, e As EventArgs) Handles btnEditarAlcance.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=alcanceEditar")
    'End Sub


    'Private Sub btnActualizarAlcance_Click(sender As Object, e As EventArgs) Handles btnActualizarAlcance.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=alcanceActualizar")
    'End Sub

    'Private Sub btnCancelarAlcance_Click(sender As Object, e As EventArgs) Handles btnCancelarAlcance.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=alcanceCancelar")
    'End Sub

    'Private Sub btnConsultarAlcance_Click(sender As Object, e As EventArgs) Handles btnConsultarAlcance.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=alcanceConsultar")
    'End Sub
#Region "Solicitud Gastos a Comprobar"
#End Region



#End Region
#Region "comprobación"
    'Private Sub btnAgregarComprobacion_Click(sender As Object, e As EventArgs) Handles btnAgregarComprobacion.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=comprobacionAgregar")
    'End Sub
    'Private Sub btnConsultarComprobacion_Click(sender As Object, e As EventArgs) Handles btnConsultarComprobacion.Click
    '    Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=comprobacionConsultar")
    'End Sub
#End Region
    '#Region "devolución"
    '    Private Sub btnAgregarDevolucion_Click(sender As Object, e As EventArgs) Handles btnAgregarDevolucion.Click
    '        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=devolucionAgregar")
    '    End Sub
    '    Private Sub btnConsultarDevolucion_Click(sender As Object, e As EventArgs) Handles btnConsultarDevolucion.Click
    '        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=devolucionConsultar")
    '    End Sub

    '#End Region
#Region "Almacén"

    Private Sub btnAgregarEntrada_Click(sender As Object, e As EventArgs) Handles btnAgregarEntrada.Click
        Response.Redirect("~/management/almacen/entrada/frmListadoEntrada.aspx?band=alta")
    End Sub
    Private Sub btnEditarEntrada_Click(sender As Object, e As EventArgs) Handles btnEditarEntrada.Click
        Response.Redirect("~/management/almacen/entrada/frmListadoEntrada.aspx?band=edt")
    End Sub

    Private Sub btnActualizarEntrada_Click(sender As Object, e As EventArgs) Handles btnActualizarEntrada.Click
        Response.Redirect("~/management/almacen/entrada/frmListadoEntrada.aspx?band=act")
    End Sub

    Private Sub btnConsultarEntrada_Click(sender As Object, e As EventArgs) Handles btnConsultarEntrada.Click
        Response.Redirect("~/management/almacen/entrada/frmListadoEntrada.aspx?band=cons")
    End Sub
    Private Sub btnAgregarSalida_Click(sender As Object, e As EventArgs) Handles btnAgregarSalida.Click
        Response.Redirect("~/management/almacen/salida/frmAgregarSalida.aspx")
    End Sub
    Private Sub btnEditarSalida_Click(sender As Object, e As EventArgs) Handles btnEditarSalida.Click
        Response.Redirect("~/management/almacen/salida/frmConsultarSalidaParaEditar.aspx")
    End Sub

    Private Sub btnConsultarSalida_Click(sender As Object, e As EventArgs) Handles btnConsultarSalida.Click
        Response.Redirect("~/management/almacen/salida/frmConsultarSalida.aspx")
    End Sub
    'Private Sub btnAjustarInventario_Click(sender As Object, e As EventArgs) Handles btnAjustarInventario.Click
    '    Response.Redirect("~/management/almacen/inventario/frmListaArticulosAjustar.aspx?band=ajuste")
    'End Sub

    'Private Sub btnConsultarInventario_Click(sender As Object, e As EventArgs) Handles btnConsultarInventario.Click
    '    Response.Redirect("~/management/almacen/inventario/frmListaArticulosAjustar.aspx?band=cons")
    'End Sub

    'Private Sub btnEliminarInventario_Click(sender As Object, e As EventArgs) Handles btnEliminarInventario.Click
    '    Response.Redirect("~/management/almacen/inventario/frmDesactivar.aspx?band=elim")
    'End Sub

    'Private Sub lnkAlmacenEntradaArticulos_Click(sender As Object, e As EventArgs) Handles lnkAlmacenEntradaArticulos.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteEntradaxArticulo.aspx")
    'End Sub

    'Private Sub lnkAlmacenSalidaArticulos_Click(sender As Object, e As EventArgs) Handles lnkAlmacenSalidaArticulos.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteSalidaxArticulo.aspx")
    'End Sub

    'Private Sub lnkAlmacenSalidaPorCategoria_Click(sender As Object, e As EventArgs) Handles lnkAlmacenSalidaPorCategoria.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteSalidaPorCategoria.aspx")
    'End Sub

    'Private Sub lnkAlmacenSalidaPorArea_Click(sender As Object, e As EventArgs) Handles lnkAlmacenSalidaPorArea.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteSalidaPorArea.aspx")
    'End Sub

    'Private Sub lnkAlmacenListaArticulos_Click(sender As Object, e As EventArgs) Handles lnkAlmacenListaArticulos.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteListaArticulos.aspx")
    'End Sub

    'Private Sub lnkAlmacenEntradaPorCategoria_Click(sender As Object, e As EventArgs) Handles lnkAlmacenEntradaPorCategoria.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteEntradaPorCategoria.aspx")
    'End Sub

    'Private Sub lnkAlmacenGastoPorArea_Click(sender As Object, e As EventArgs) Handles lnkAlmacenGastoPorArea.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteGastoPorArea.aspx")
    'End Sub

    'Private Sub lnkAlmacenInvetario_Click(sender As Object, e As EventArgs) Handles lnkAlmacenInvetario.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteInventario.aspx")
    'End Sub

    'Private Sub lnkAlmacenEntradaArticulo_Click(sender As Object, e As EventArgs) Handles lnkAlmacenEntradaArticulo.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteEntradaArticulo.aspx")
    'End Sub

    'Private Sub lnkAlmacenConsumoArea_Click(sender As Object, e As EventArgs) Handles lnkAlmacenConsumoArea.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteConsumoArea.aspx")
    'End Sub

    'Private Sub lnkSeguimientoxArticulo_Click(sender As Object, e As EventArgs) Handles lnkSeguimientoxArticulo.Click
    '    Response.Redirect("~/management/almacen/reportes/frmReporteSeguimientoxArticulo.aspx")
    'End Sub

    Protected Sub lnkPrincipal_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/management/default.aspx")
    End Sub

    Protected Sub lnkReportes_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/management/frmReportes.aspx")
    End Sub

    Protected Sub lnkPrincipalFondo_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/management/default.aspx")
    End Sub

    'Private Sub btnReporteOficioAtendido_Click(sender As Object, e As EventArgs) Handles btnReporteOficioAtendido.Click
    '    Response.Redirect("~/management/oficio/reportes/frmReporteOficiosxAtender.aspx?band=atendidos")
    'End Sub

    'Private Sub btnReporteOficioOficioPorAtender_Click(sender As Object, e As EventArgs) Handles btnReporteOficioOficioPorAtender.Click
    '    Response.Redirect("~/management/oficio/reportes/frmReporteOficiosxAtender.aspx?band=porAtender")
    'End Sub

    'Private Sub btnReporteRecLiberadoComprobado_Click(sender As Object, e As EventArgs) Handles btnReporteRecLiberadoComprobado.Click
    '    Response.Redirect("~/management/solicitudGasto/reporteSolicitud/frmReporteSolicitudGastoComprobar.aspx?band=comp")
    'End Sub

    'Private Sub btnReporteRecLiberadoNoComprobado_Click(sender As Object, e As EventArgs) Handles btnReporteRecLiberadoNoComprobado.Click
    '    Response.Redirect("~/management/solicitudGasto/reporteSolicitud/frmReporteSolicitudGastoComprobar.aspx?band=Nocomp")
    'End Sub

    'Private Sub lnkCodigosBarras_Click(sender As Object, e As EventArgs) Handles lnkCodigosBarras.Click
    '    Response.Redirect("~/management/almacen/reportes/frmCodigosBarras.aspx")
    'End Sub
#End Region
#Region "COMPRAS-R_M"
    'Private Sub btnReporteComprasArea_Click(sender As Object, e As EventArgs) Handles btnReporteComprasArea.Click
    '    Response.Redirect("~/management/reportesCompras/frmReporteComprasArea.aspx")
    'End Sub

    'Private Sub btnReporteComprasProveedor_Click(sender As Object, e As EventArgs) Handles btnReporteComprasProveedor.Click
    '    Response.Redirect("~/management/reportesCompras/frmReporteComprasProveedor.aspx")
    'End Sub

    Private Sub btnHistorial_Click(sender As Object, e As EventArgs) Handles btnHistorial.Click
        Response.Redirect("~/management/historial/frmHistorial.aspx")
    End Sub



#End Region

    Private Sub construirMenu()

        Dim listaPermisos = New CRN.nspPermiso.Proceso_ObtenerPermisos() With {.tipoConsulta = CES.nspPermiso.tipoConsultapermiso.idRol, .idRol = paginaBase.IdRol}.Ejecutar()
        If listaPermisos.Count > 0 Then
            For i = 0 To listaPermisos.Count - 1
                If listaPermisos(i).esActivo = False Then
                    Select Case listaPermisos(i).nombrePagina
                        Case "Administración"
                            liAdministrador.Visible = False
                        Case "Usuario"
                            liUsuario.Visible = False
                        Case "Usuario - Alta"
                            btnAltaUsuario.Visible = False
                        Case "Usuario - Consulta"
                            btnConsultaUsuario.Visible = False
                        Case "Rol"
                            btnRol.Visible = False
                        Case "Permiso"
                            btnPermiso.Visible = False
                        Case "Página"
                            btnPagina.Visible = False
                        Case "Sistema"
                            liSistema.Visible = False
                        Case "Sistema - Alta"
                            btnAltaSistema.Visible = False
                        Case "Sistema - Consulta"
                            btnConsultaSistema.Visible = False
                        Case "Historial"
                            btnHistorial.Visible = False
                        'Case "Reportes"
                        '    liReportes.Visible = False
                        '    liReportesAlmacen.Visible = False
                        'Case "Reportes Recursos materiales"
                        '    liReporteRM.Visible = False
                        'Case "Reportes Recursos materiales - Oficios atendidos"
                        '    btnReporteOficioAtendido.Visible = False
                        'Case "Reportes Recursos materiales - Oficios por atender"
                        '    btnReporteOficioOficioPorAtender.Visible = False
                        'Case "Reportes Recursos materiales - Compras por área"
                        '    btnReporteComprasArea.Visible = False
                        'Case "Reportes Recursos materiales - Compras por proveedor"
                        '    btnReporteComprasProveedor.Visible = False
                        'Case "Reportes Recursos materiales - Recurso no liberado"
                        '    btnReporteRecNoLiberado.Visible = False
                        'Case "Reportes Recursos materiales - Recurso liberado pendiente comprobar"
                        '    btnReporteRecLiberadoNoComprobado.Visible = False
                        'Case "Reportes Recursos materiales - Recurso liberado y comprobado"
                        '    btnReporteRecLiberadoComprobado.Visible = False
                        'Case "Reportes Almacen"
                        '    liReportesAlmacen.Visible = False
                        'Case "Reportes Almacen - Consumo por área"
                        '    lnkAlmacenConsumoArea.Visible = False
                        'Case "Reportes Almacen - Entrada de artículos"
                        '    lnkAlmacenEntradaArticulos.Visible = False
                        'Case "Reportes Almacen - Entrada por artículo proveedor"
                        '    lnkAlmacenEntradaArticulo.Visible = False
                        'Case "Reportes Almacen - Entrada por categoría"
                        '    lnkAlmacenEntradaPorCategoria.Visible = False
                        'Case "Reportes Almacen - Gasto por área"
                        '    lnkAlmacenGastoPorArea.Visible = False
                        'Case "Reportes Almacen - Inventario por categoría"
                        '    lnkAlmacenInvetario.Visible = False
                        'Case "Reportes Almacen - Lista artículos"
                        '    lnkAlmacenListaArticulos.Visible = False
                        'Case "Reportes Almacen - Salida de artículos"
                        '    lnkAlmacenSalidaArticulos.Visible = False
                        'Case "Reportes Almacen - Salida por área"
                        '    lnkAlmacenSalidaPorArea.Visible = False
                        'Case "Reportes Almacen - Salida por categoría"
                        '    lnkAlmacenSalidaPorCategoria.Visible = False
                        'Case "Reportes Almacen - Seguimiento por artículo"
                        '    lnkSeguimientoxArticulo.Visible = False
                        Case "Catálogos"
                            liCatalogos.Visible = False
                        Case "Catálogos - Área"
                            liCatArea.Visible = False
                        Case "Catálogos - Área - Agregar"
                            btnAltaArea.Visible = False
                        Case "Catálogos - Área - Consultar"
                            btnConsultarArea.Visible = False
                        Case "Catálogos - Artículo"
                            liCatArticulo.Visible = False
                        Case "Catálogos - Artículo - Agregar"
                            btnAltaArticulo.Visible = False
                        Case "Catálogos - Artículo - Consultar"
                            btnConsultarArticulo.Visible = False
                        'Case "Catálogos - Partida presupuestal"
                        '    btnPartidaPresupuestal.Visible = False
                        Case "Catálogos - Proveedor"
                            liCatProveedor.Visible = False
                        Case "Catálogos - Proveedor - Agregar"
                            btnAltaProveedor.Visible = False
                        Case "Catálogos - Proveedor - Consultar"
                            btnConsultarProveedor.Visible = False
                        Case "Catálogos - Rubros"
                            btnRubros.Visible = False
                        'Case "Catálogos estáticos"
                        '    liCatEstaticos.Visible = False
                        'Case "Catálogos estáticos - Responsable"
                        '    btnResponsable.Visible = False
                        'Case "Catálogos estáticos - Categoría"
                        '    btnCategoria.Visible = False
                        'Case "Catálogos estáticos - Firmas"
                        '    btnFirmas.Visible = False
                        'Case "Catálogos estáticos - Doc. contable"
                        '    btnDocContable.Visible = False
                        'Case "Catálogos estáticos - Unidad medida"
                        '    btnUnidadMedida.Visible = False
                        'Case "Catálogos estáticos - Tipo pago"
                        '    btnTipoPago.Visible = False
                        'Case "Catálogos estáticos - Estatus oficio"
                        '    btnEstatusOficio.Visible = False
                        'Case "Principal"
                        '    lnkPrincipal.Visible = False
                        Case "Recursos materiales"
                            liRecMat.Visible = False
                        Case "Recursos materiales - Oficio"
                            liRMOficio.Visible = False
                        Case "Recursos materiales - Oficio - Agregar"
                            btnAltaOficio.Visible = False
                        Case "Recursos materiales - Oficio - Complementar"
                            btnComplementarOficio.Visible = False
                        Case "Recursos materiales - Oficio - Editar"
                            btnEditarOficio.Visible = False
                        Case "Recursos materiales - Oficio - Consultar"
                            btnConsultarOficio.Visible = False
                        Case "Recursos materiales - Pedido"
                            liRMPedido.Visible = False
                        Case "Recursos materiales - Pedido - Agregar"
                            btnAgregarPedido.Visible = False
                        Case "Recursos materiales - Pedido - Agregar sin DRM"
                            btnAgregarSinDRMPedido.Visible = False
                        Case "Recursos materiales - Pedido - Editar"
                            btnEditarPedido.Visible = False
                        Case "Recursos materiales - Pedido - Consultar"
                            btnConsultarPedido.Visible = False
                        'Case "Recursos materiales - Solicitud de gasto"
                        '    liRMSolicitudGasto.Visible = False
                        'Case "Recursos materiales - Solicitud de gasto - Agregar"
                        '    btnAgregarSolicitudGasto.Visible = False
                        'Case "Recursos materiales - Solicitud de gasto - Agregar con documento interno"
                        '    btnAgregarSolicitudConDocumentoInterno.Visible = False
                        'Case "Recursos materiales - Solicitud de gasto - Actualizar"
                        '    btnActualizarSolicitudGasto.Visible = False
                        'Case "Recursos materiales - Solicitud de gasto - Editar"
                        '    btnEditarSolicitudGasto.Visible = False
                        'Case "Recursos materiales - Solicitud de gasto - Consultar"
                        '    btnConsultarSolicitudGasto.Visible = False
                        'Case "Recursos materiales - Solicitud de gasto - Cancelar"
                        '    btnCancelarSolicitudGasto.Visible = False
                        'Case "Recursos materiales - Afectación presupuestal"
                        '    liRMAfectacionPresupuestal.Visible = False
                        'Case "Recursos materiales - Afectación presupuestal - Agregar"
                        '    btnAgregarAfectacion.Visible = False
                        'Case "Recursos materiales - Afectación presupuestal - Editar"
                        '    btnEditarAfectacion.Visible = False
                        'Case "Recursos materiales - Afectación presupuestal - Consultar"
                        '    btnConsultarAfectacion.Visible = False
                        'Case "Recursos materiales - Afectación presupuestal - Sustituir"
                        '    btnSustituirAfectacion.Visible = False
                        'Case "Recursos materiales - Alcance"
                        '    liRMAlcance.Visible = False
                        'Case "Recursos materiales - Alcance - Agregar"
                        '    btnAgregarAlcance.Visible = False
                        'Case "Recursos materiales - Alcance - Actualizar"
                        '    btnActualizarAlcance.Visible = False
                        'Case "Recursos materiales - Alcance - Editar"
                        '    btnEditarAlcance.Visible = False
                        'Case "Recursos materiales - Alcance - Consultar"
                        '    btnConsultarAlcance.Visible = False
                        'Case "Recursos materiales - Alcance - Cancelar"
                        '    btnCancelarAlcance.Visible = False
                        'Case "Recursos materiales - Comprobación"
                        '    liRMComprobacion.Visible = False
                        'Case "Recursos materiales - Comprobación - Agregar"
                        '    btnAgregarComprobacion.Visible = False
                        'Case "Recursos materiales - Comprobación - Consultar"
                        '    btnConsultarComprobacion.Visible = False
                        'Case "Recursos materiales - Devolución"
                        '    liRMDevolucion.Visible = False
                        'Case "Recursos materiales - Devolución - Agregar"
                        '    btnAgregarDevolucion.Visible = False
                        'Case "Recursos materiales - Devolución - Consultar"
                        '    btnConsultarDevolucion.Visible = False
                        Case "Almacén"
                            liAlmacen.Visible = False
                        Case "Almacén - Entrada"
                            liAlmacenEntrada.Visible = False
                        Case "Almacén - Entrada - Agregar"
                            btnAgregarEntrada.Visible = False
                        Case "Almacén - Entrada - Actualizar"
                            btnActualizarEntrada.Visible = False
                        Case "Almacén - Entrada - Editar"
                            btnEditarEntrada.Visible = False
                        Case "Almacén - Entrada - Consultar"
                            btnConsultarEntrada.Visible = False
                        Case "Almacén - Salida"
                            liAlmacenSalida.Visible = False
                        Case "Almacén - Salida - Agregar"
                            btnAgregarSalida.Visible = False
                        Case "Almacén - Salida - Editar"
                            btnEditarSalida.Visible = False
                        Case "Almacén - Salida - Consultar"
                            btnConsultarSalida.Visible = False
                            'Case "Almacén - Inventario"
                            '    liAlmacenInventario.Visible = False
                            'Case "Almacén - Inventario - Ajustar"
                            '    btnAjustarInventario.Visible = False
                            'Case "Almacén - Inventario - Consultar"
                            '    btnConsultarInventario.Visible = False
                            'Case "Almacén - Inventario - Eliminar"
                            '    btnEliminarInventario.Visible = False
                    End Select
                End If
            Next
        End If
    End Sub
    Private Sub btnAltaSistema_Click(sender As Object, e As EventArgs) Handles btnAltaSistema.Click
        Response.Redirect("~/management/sistema/frmAltaSistema.aspx")
    End Sub

    Private Sub btnConsultaSistema_Click(sender As Object, e As EventArgs) Handles btnConsultaSistema.Click
        Response.Redirect("~/management/sistema/frmListaSistemas.aspx")
    End Sub

    'Private Sub btnReporteRecNoLiberado_Click(sender As Object, e As EventArgs) Handles btnReporteRecNoLiberado.Click
    '    Response.Redirect("~/management/solicitudGasto/reporteSolicitud/frmReporteSolicitudGastoNoLiberado.aspx")
    'End Sub
End Class
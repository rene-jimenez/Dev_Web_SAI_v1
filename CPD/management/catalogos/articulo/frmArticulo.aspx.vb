Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspCategoria, CRN.nspCategoria
Imports CES.nspArticulo, CRN.nspArticulo
Imports CES.nspUnidadMedida, CRN.nspUnidadMedida
Imports CES.nspPopup
Public Class frmArticulo : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sistema As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("band").ToString = "add" Then
                txbNombre.Text = Request.QueryString("nombre")
                poblarCategoria()
                cmbCategoria.SelectedValue = Request.QueryString("idCategoria")
                poblarUnidadMedida()
                lblTitulo.Text = "ALTA ARTÍCULO"
            ElseIf Request.QueryString("band").ToString = "edo" Then
                poblarCategoria()
                poblarUnidadMedida()
                poblarFormularioArticulo()
                lblTitulo.Text = "EDITAR ARTÍCULO"
            End If

        End If
    End Sub

#Region "Métodos"
    Private Sub poblarCategoria()
        Dim listaCategoria = New Proceso_ObtenerCategorias() With {.tipoConsulta = tipoConsultaCategoria.esActivo, .esActivo = True}.Ejecutar()
        cmbCategoria.Items.Add("Seleccione una categoria")
        cmbCategoria.DataTextField = "nombre"
        cmbCategoria.DataValueField = "id"
        cmbCategoria.DataSource = listaCategoria
        cmbCategoria.DataBind()
    End Sub
    Private Sub poblarUnidadMedida()
        Dim listaUnidadMedida = New Proceso_ObtenerUnidadesMedida() With {.tipoConsulta = tipoConsultaUnidadMedida.esActivo, .esActivo = True}.Ejecutar()
        cmbUnidadMedida.Items.Add("Seleccione una unidad de medida")
        cmbUnidadMedida.DataTextField = "nombre"
        cmbUnidadMedida.DataValueField = "id"
        cmbUnidadMedida.DataSource = listaUnidadMedida
        cmbUnidadMedida.DataBind()
    End Sub
    Private Sub poblarFormularioArticulo()
        Dim articulo = New Proceso_ObtenerArticulo() With {.id = Guid.Parse(Request.QueryString("id").ToString)}.Ejecutar()

        cmbUnidadMedida.SelectedValue = articulo.idUnidadMedida.ToString
        txbNombre.Text = articulo.nombre
        txbCodigoBarras.Text = articulo.codigoBarras
        txbCantidadInicial.Text = articulo.cantidadInicial
        txbExistencia.Text = articulo.existencia
        txbStockMinimo.Text = articulo.stockMinimo
        txbStockMaximo.Text = articulo.stockMaximo
        txbUrl.Text = articulo.url
        cmbCategoria.SelectedValue = articulo.idCategoria.ToString
        txbUltimoPrecio.Text = articulo.ultimoPrecio.ToString(“##,##0.00")
        If articulo.entraAlmacen = True Then
            rdbSi.Checked = True
        Else
            rdbNo.Checked = True
        End If
    End Sub
#End Region
#Region "Funciones"
    Private Function ValidarCampos() As respuestaDelProceso

        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If (txbNombre.Text.Trim() = "") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "nombre"))
            End If
            If (cmbUnidadMedida.SelectedValue = "Seleccione una unidad de medida") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "unidad medida"))
            End If
            If (txbCodigoBarras.Text.Trim() = "") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "código de barras"))
            End If
            If (txbCantidadInicial.Text.Trim() = "") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "cantidad inicial"))
            End If
            If IsNumeric(txbCantidadInicial.Text) = False Then
                Throw New Exception("El campo existencia inicial debe contener solo números Ejem. 1500")
            End If
            If (txbExistencia.Text.Trim() = "") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "existencia"))
            End If
            If IsNumeric(txbExistencia.Text) = False Then
                Throw New Exception("El campo existencia debe contener solo números Ejem. 1500")
            End If
            If (txbStockMinimo.Text.Trim() = "") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "stock mínimo"))
            End If
            If IsNumeric(txbStockMinimo.Text) = False Then
                Throw New Exception("El campo stock mínimo debe contener solo números Ejem. 1500")
            End If
            If (txbStockMaximo.Text.Trim() = "") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "stock máximo"))
            End If
            If IsNumeric(txbStockMaximo.Text) = False Then
                Throw New Exception("El campo stock máximo debe contener solo números Ejem. 1500")
            End If
            If (txbUltimoPrecio.Text.Trim() = "") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "último precio"))
            End If
            If IsNumeric(txbUltimoPrecio.Text) = False Then
                Throw New Exception("El campo stock máximo debe contener solo números Ejem. 1500")
            End If
            If rdbNo.Checked = False And rdbSi.Checked = False Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "entra a almacén"))
            End If

        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
#End Region
#Region "eventos"
    Private Sub txbCodigoBarras_TextChanged(sender As Object, e As EventArgs) Handles txbCodigoBarras.TextChanged
        Me.txbCodigoBarras.Text = UCase(Me.txbCodigoBarras.Text)
    End Sub
    Private Sub rdbNo_CheckedChanged(sender As Object, e As EventArgs) Handles rdbNo.CheckedChanged
        If rdbNo.Checked Then
            rdbSi.Checked = False
        End If
    End Sub

    Private Sub rdbSi_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSi.CheckedChanged
        If rdbSi.Checked Then
            rdbNo.Checked = False
        End If
    End Sub

#End Region
#Region "btns"
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            If Request.QueryString("band").ToString = "add" Then
                Dim respuestaValidacion = ValidarCampos()
                If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                    OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario.ToString, tipoPopup.Naranja, False, "")
                    Exit Sub
                End If
                Dim articulo As New articulo
                articulo.id = Guid.NewGuid()
                articulo.idUnidadMedida = Guid.Parse(cmbUnidadMedida.SelectedValue.ToString)
                articulo.nombre = txbNombre.Text
                articulo.codigoBarras = txbCodigoBarras.Text
                articulo.cantidadInicial = txbCantidadInicial.Text
                articulo.existencia = txbExistencia.Text
                articulo.stockMinimo = txbStockMinimo.Text
                articulo.stockMaximo = txbStockMaximo.Text
                articulo.url = txbUrl.Text
                articulo.idCategoria = Guid.Parse(cmbCategoria.SelectedValue.ToString)
                articulo.ultimoPrecio = txbUltimoPrecio.Text
                If rdbSi.Checked = True Then
                    articulo.entraAlmacen = True
                Else
                    articulo.entraAlmacen = False
                End If
                articulo.tipoSistema = sistema.sistemaActivo.tipo
                articulo.ipUsuario = direccionIP
                articulo.idUsuarioMovimiento = IdUsuario
                Dim respuestaProceso = New Proceso_AgregarArticulo With {.entidad = articulo}.Ejecutar
                Select Case respuestaProceso.respuesta
                    Case tipoRespuestaDelProceso.Completado
                        OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Artículo"), tipoPopup.Verde, True, "management/default.aspx")
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                        OnMostrarMensajeAccion("Atención", respuestaProceso.comentario, tipoPopup.Naranja, False, "")
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                        OnMostrarMensajeAccion("Crítico", respuestaProceso.comentario, tipoPopup.Rojo, False, "")
                End Select
            ElseIf Request.QueryString("band").ToString = "edo" Then
                Dim mensaje = ValidarCampos()
                If mensaje.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                    OnMostrarMensajeAccion("Atención", mensaje.comentario, tipoPopup.Naranja, False, "")
                    Exit Sub
                End If
                Dim articulo As New articulo
                articulo.id = Guid.Parse(Request.QueryString("id").ToString)
                articulo.idUnidadMedida = Guid.Parse(cmbUnidadMedida.SelectedValue.ToString)
                articulo.nombre = txbNombre.Text
                articulo.codigoBarras = txbCodigoBarras.Text
                articulo.cantidadInicial = txbCantidadInicial.Text
                articulo.existencia = txbExistencia.Text
                articulo.stockMinimo = txbStockMinimo.Text
                articulo.stockMaximo = txbStockMaximo.Text
                articulo.url = txbUrl.Text
                articulo.idCategoria = Guid.Parse(cmbCategoria.SelectedValue.ToString)
                articulo.ultimoPrecio = txbUltimoPrecio.Text
                If rdbSi.Checked = True Then
                    articulo.entraAlmacen = True
                Else
                    articulo.entraAlmacen = False
                End If
                articulo.tipoSistema = sistema.sistemaActivo.tipo
                articulo.ipUsuario = direccionIP
                articulo.idUsuarioMovimiento = IdUsuario
                Dim respuestaProceso = New Proceso_ActualizarArticulo With {.entidad = articulo}.Ejecutar
                Select Case respuestaProceso.respuesta
                    Case tipoRespuestaDelProceso.Completado
                        OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "Artículo"), tipoPopup.Verde, True, "management/default.aspx")
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                        OnMostrarMensajeAccion("Atención", respuestaProceso.comentario, tipoPopup.Naranja, False, "")
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                        OnMostrarMensajeAccion("Crítico", respuestaProceso.comentario, tipoPopup.Rojo, False, "")
                End Select
            Else
                mandaDefault()
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        mandaDefault()
    End Sub



#End Region
End Class
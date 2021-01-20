Imports Contexto.Notificaciones.controladorMensajes
Imports CRN.nspCategoria, CES.nspCategoria, CES.nspPopup, CES.nspArticulo
Imports CRN.nspArticulo
Public Class frmBusquedaArticulo : Inherits paginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sistema As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarCategoria()
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Dim mensaje = validarCampos()
            If mensaje.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(mensaje.comentario)
            End If
            Dim resultadoBusqueda = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.nombre_x_categoria, .Nombre = txbNombre.Text, .idCategoria = Guid.Parse(cmbCategoria.SelectedValue), .tipoSistema = sistema.sistemaActivo.tipo}.Ejecutar()
            If resultadoBusqueda.Count > 0 Then
                Response.Redirect("~/management/catalogos/articulo/frmResultadoBusquedaArt.aspx?nombre=" + txbNombre.Text + "&idCategoria=" + cmbCategoria.SelectedValue.ToString)
            Else
                Dim resultadoBusquedados = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.nombre, .Nombre = txbNombre.Text, .tipoSistema = sistema.sistemaActivo.tipo}.Ejecutar()
                If resultadoBusquedados.Count > 0 Then
                    OnMostrarMensajeAccion("Advertencia", "Ya existe un artículo con ese nombre en la categoría  " + resultadoBusquedados(0).nombreCategoria, tipoPopup.Naranja, False, "")
                    Exit Sub
                Else
                    Response.Redirect("~/management/catalogos/articulo/frmResultadoBusquedaArt.aspx?nombre=" + txbNombre.Text + "&idCategoria=" + cmbCategoria.SelectedValue.ToString)
                End If
            End If

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        mandaDefault()
    End Sub
    Private Sub poblarCategoria()
        Dim listaCategoria = New Proceso_ObtenerCategorias() With {.tipoConsulta = tipoConsultaCategoria.esActivo, .esActivo = True}.Ejecutar()
        cmbCategoria.Items.Add("Seleccione una categoría")
        cmbCategoria.DataTextField = "nombre"
        cmbCategoria.DataValueField = "id"
        cmbCategoria.DataSource = listaCategoria.OrderBy(Function(o) o.nombre).ToList
        cmbCategoria.DataBind()
    End Sub
    Private Function validarCampos()
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbNombre.Text.ToString = "" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "artículo")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        ElseIf cmbCategoria.SelectedValue = "Seleccione una categoría" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "categoría")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        End If
        Return respuesta
    End Function
End Class

Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup, CRN.nspArticulo, CES.nspArticulo
Imports cadenero.entidades.nspBase
Public Class frmResultadoBusquedaArt : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarResultadoBusquedaArticulo()
        End If
    End Sub

    Private Sub poblarResultadoBusquedaArticulo()
        Dim nombreBusqueda = Request.QueryString("nombre")
        Dim idCategoria = Guid.Parse(Request.QueryString("idCategoria").ToString)
        Dim resultadoBusqueda = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.nombre_x_categoria, .Nombre = nombreBusqueda, .idCategoria = idCategoria, .tipoSistema = sistema.sistemaActivo.tipo}.Ejecutar()
        lvsResultadoBusquedaArticulo.DataSource = resultadoBusqueda.ToList
        lvsResultadoBusquedaArticulo.DataBind()
    End Sub
    Protected Sub btnEditar_Click(sender As Object, e As EventArgs)
        Try
            Dim btn As LinkButton = sender
            Dim indice As Integer = btn.TabIndex
            Dim id As Guid = Guid.Parse(btn.CommandArgument)
            Dim idString = id.ToString
            Dim miPagina = "frmArticulo.aspx?id=" & idString + "&band=edo"
            Response.Redirect(miPagina)
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Protected Sub btnActivoDesactivar_Click(sender As Object, e As EventArgs)
        Dim btnAD As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnAD.CommandArgument)
        Dim ide = id.ToString

        Dim editar = New Proceso_ObtenerArticulo() With {.id = (id)}.Ejecutar
        editar.id = id
        editar.esActivo = False
        Dim respuesta = New Proceso_DesactivarArticulo With {.entidad = editar}.Ejecutar()
        Select Case respuesta.respuesta
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_desactivo, "artículo"), tipoPopup.Verde, False, "management/area/frmConsultaArea.aspx")
                'Me.cardlista.Attributes.Remove("class")
                'cardlista.Attributes.Add("class", "card animated pulse")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Advertencia", respuesta.comentario, tipoPopup.Naranja, False, "")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select

        poblarResultadoBusquedaArticulo()


    End Sub

    Protected Sub btnDesactivoActivar_Click(sender As Object, e As EventArgs)
        Dim btnDA As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnDA.CommandArgument)
        Dim ide = id.ToString
        Dim editar = New Proceso_ObtenerArticulo() With {.id = (id)}.Ejecutar
        editar.id = id
        editar.esActivo = True
        Dim respuesta = New Proceso_DesactivarArticulo With {.entidad = editar}.Ejecutar()
        Select Case respuesta.respuesta
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_activo, "artículo"), tipoPopup.Verde, False, "management/area/frmConsultaArea.aspx")
                'cardlista.Attributes.Add("class", "card animated shake")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Advertencia", respuesta.comentario, tipoPopup.Naranja, False, "")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select
        poblarResultadoBusquedaArticulo()
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Response.Redirect("~/management/catalogos/articulo/frmArticulo.aspx?band=add&nombre=" + Request.QueryString("nombre") + "&idCategoria=" + Request.QueryString("idCategoria").ToString)
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Response.Redirect("~/management/catalogos/articulo/frmBusquedaArticulo.aspx")
    End Sub
End Class
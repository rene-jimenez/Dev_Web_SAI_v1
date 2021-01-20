Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup, CRN.nspArticulo, CES.nspArticulo
Imports cadenero.entidades.nspBase

Public Class frmPrincipalArticulo : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarArticulo()
        End If
    End Sub
    Private Sub poblarArticulo()
        Dim X As New nspPaginaBase.PaginaBase
        Dim resultado = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.todos, .tipoSistema = X.sistemaActivo.tipo}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        lvsArticulo.DataSource = resultado.ToList
        lvsArticulo.DataBind()
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
            OnMostrarMensajeAccion("Artículo", ex.Message.ToString, tipoPopup.Rojo, False, "")
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
                OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_desactivo, "artículo"), tipoPopup.Verde, True, "management/catalogos/articulo/frmPrincipalArticulo.aspx")
                UpdatePanelLista.Update()
                poblarArticulo()
                'Me.cardlista.Attributes.Remove("class")
                'cardlista.Attributes.Add("class", "card animated pulse")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select




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
                OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_activo, "artículo"), tipoPopup.Verde, True, "management/catalogos/articulo/frmPrincipalArticulo.aspx")
                UpdatePanelLista.Update()
                poblarArticulo()
                'cardlista.Attributes.Add("class", "card animated shake")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select
        poblarArticulo()
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        mandaDefault()
    End Sub
End Class
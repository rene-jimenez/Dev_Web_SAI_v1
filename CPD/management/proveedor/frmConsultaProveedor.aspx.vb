Imports CES, CRN, CES.nspPopup
Imports CRN.nspProveedor
Imports CES.nspProveedor
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmConsultaProveedor : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarLista()
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
    Protected Sub llenarLista()
        Dim lista = New Proceso_ObtenerProveedores() With {.tipoConsulta = tipoConsultaProveedor.todos}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        lsvLista.DataSource = lista.ToList
        lsvLista.DataBind()
    End Sub

#Region "btn"
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        mandaDefault()
    End Sub
#End Region
#Region "eventos"
    Protected Sub btnSeleccionar_Click1(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        Dim indice As Integer = btn.TabIndex
        Dim id As Guid = Guid.Parse(btn.CommandArgument)
        Dim idString = id.ToString
        Dim miPagina = "frmProveedor.aspx?band=edt&id=" & idString
        Response.Redirect(miPagina)

    End Sub

    Protected Sub btnActivoDesactivar_Click(sender As Object, e As EventArgs)
        Dim btnAD As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnAD.CommandArgument)
        Dim ide = id.ToString
        Dim editar = New Proceso_ObtenerProveedor() With {.id = Guid.Parse(ide)}.Ejecutar()
        editar.id = id
        editar.esActivo = False
        Dim respuesta = New Proceso_DesactivarProveedor() With {.entidad = editar}.Ejecutar()
        Select Case respuesta.respuesta
            Case tipoRespuestaDelProceso.Completado
                OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_desactivo, "proveedor"), nspPopup.tipoPopup.Verde, True, "management/Proveedor/frmConsultaProveedor.aspx")
                Me.cardlista.Attributes.Remove("class")
                cardlista.Attributes.Add("class", "card animated pulse")
            Case tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
            Case tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select
        llenarLista()
    End Sub

    Protected Sub btnDesactivoActivar_Click(sender As Object, e As EventArgs)
        Dim btnAD As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnAD.CommandArgument)
        Dim ide = id.ToString
        Dim editar = New Proceso_ObtenerProveedor() With {.id = Guid.Parse(ide)}.Ejecutar()
        editar.id = id
        editar.esActivo = True
        Dim respuesta = New Proceso_DesactivarProveedor() With {.entidad = editar}.Ejecutar()
        Select Case respuesta.respuesta
            Case tipoRespuestaDelProceso.Completado
                OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_activo, "proveedor"), nspPopup.tipoPopup.Verde, True, "management/Proveedor/frmConsultaProveedor.aspx")
                cardlista.Attributes.Add("class", "card animated shake")
            Case tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
            Case tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select
        llenarLista()
    End Sub

#End Region



End Class
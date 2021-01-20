Imports CES.nspPopup
Imports cadenero.CRN.nspUsuario
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmConsultaUsuario : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                poblarUsuarios()
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "botones"
    Protected Sub btnSeleccionar_OnClick(sender As Object, e As EventArgs)
        Try
            Dim btn As LinkButton = sender
            Dim indice As Integer = btn.TabIndex
            Dim id As Guid = Guid.Parse(btn.CommandArgument)
            Dim idString = id.ToString
            Dim miPagina = "frmAltaUsuario.aspx?band=edt&id=" & idString
            Response.Redirect(miPagina)
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Protected Sub btnActivoDesactivar_Click(sender As Object, e As EventArgs)
        Try
            Dim btnAD As LinkButton = sender
            Dim id As Guid = Guid.Parse(btnAD.CommandArgument)
            Dim ide = id.ToString
            Dim usuario = New Proceso_ObtenerUsuario() With {.id = (id)}.Ejecutar
            If id = IdUsuario Then
                OnMostrarMensajeAccion("Crítico", "No puede desactivar la cuenta con la que esta realizando la operación", tipoPopup.Rojo, False, "")
                Exit Sub
            End If
            usuario.esActivo = False
            usuario.idUsuarioMovimiento = IdUsuario
            usuario.ipUsuario = direccionIP
            usuario.idSistema = sistemaActivo.id
            Dim respuesta = New Proceso_ActualizarUsuario() With {.entidad = usuario}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_desactivo, "usuario"), tipoPopup.Verde, False, "")
                    poblarUsuarios()
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try


    End Sub

    Protected Sub btnDesactivoActivar_Click(sender As Object, e As EventArgs)
        Try
            Dim btnDA As LinkButton = sender
            Dim id As Guid = Guid.Parse(btnDA.CommandArgument)
            Dim ide = id.ToString
            Dim usuario = New Proceso_ObtenerUsuario() With {.id = (id)}.Ejecutar
            usuario.esActivo = True
            usuario.idUsuarioMovimiento = IdUsuario
            usuario.ipUsuario = direccionIP
            usuario.idSistema = sistemaActivo.id
            Dim respuesta = New Proceso_ActualizarUsuario() With {.entidad = usuario}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_activo, "usuario"), tipoPopup.Verde, False, "")
                    poblarUsuarios()
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")

            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try


    End Sub
    Protected Sub btnANuevo_Click(sender As Object, e As EventArgs)
        Response.Redirect("frmAltaUsuario.aspx?band=add")
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
#End Region
#Region "métodos"
    Private Sub poblarUsuarios()
        Dim lista = New Proceso_ObtenerUsuarios() With {.tipoConsulta = cadenero.entidades.nspUsuario.tipoConsultaUsuario.todos}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        lvsUsuarios.DataSource = lista.ToList
        lvsUsuarios.DataBind()
    End Sub
#End Region
End Class
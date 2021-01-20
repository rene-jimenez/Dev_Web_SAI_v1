Imports CES, CRN
Imports CRN.nspArea
Imports CES.nspArea
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmConsultaArea : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarLista()
            Catch ex As Exception
                OnMostrarMensajeAccion("Área", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
    Protected Sub llenarLista()
        Dim lista = New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.todos}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        lsvLista.DataSource = lista.ToList
        lsvLista.DataBind()
    End Sub
    Protected Sub btnSeleccionar_Click(sender As Object, e As EventArgs) '
        Dim btn As LinkButton = sender
        Dim indice As Integer = btn.TabIndex
        Dim id As Guid = Guid.Parse(btn.CommandArgument)
        Dim idString = id.ToString
        Dim miPagina = "frmArea.aspx?band=edt&id=" & idString
        Response.Redirect(miPagina)

    End Sub

    Private Sub lnkCerrar_Click(sender As Object, e As EventArgs) Handles lnkCerrar.Click
        mandaDefault()
    End Sub

    Protected Sub btnActivoDesactivar_Click(sender As Object, e As EventArgs)
        Dim btnAD As LinkButton = sender

        Dim id As Guid = Guid.Parse(btnAD.CommandArgument)
        Dim ide = id.ToString

        Dim editar = New CRN.nspArea.Proceso_ObtenerArea() With {.id = Guid.Parse(ide)}.Ejecutar()
        editar.id = id
        editar.esActivo = False
        Dim respuesta = New CRN.nspArea.Proceso_DesactivarArea() With {.entidad = editar}.Ejecutar()
        Select Case respuesta.respuesta
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_desactivo, "área"), nspPopup.tipoPopup.Verde, True, "management/area/frmConsultaArea.aspx")
                Me.cardlista.Attributes.Remove("class")
                cardlista.Attributes.Add("class", "card animated pulse")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
        End Select

        llenarLista()


    End Sub

    Protected Sub btnDesactivoActivar_Click(sender As Object, e As EventArgs)
        Dim btnDA As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnDA.CommandArgument)
        Dim ide = id.ToString
        Dim editar = New CRN.nspArea.Proceso_ObtenerArea() With {.id = Guid.Parse(ide)}.Ejecutar()
        editar.id = id
        editar.esActivo = True
        Dim respuesta = New CRN.nspArea.Proceso_DesactivarArea() With {.entidad = editar}.Ejecutar()
        Select Case respuesta.respuesta
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_activo, "área"), nspPopup.tipoPopup.Verde, True, "management/area/frmConsultaArea.aspx")
                cardlista.Attributes.Add("class", "card animated shake")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
            Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
        End Select
        llenarLista()
    End Sub

    Protected Sub btnBorrar_Click(sender As Object, e As EventArgs)
        'Try
        Dim btnEliminar As LinkButton = sender
        Dim sb As StringBuilder = New StringBuilder
        'Dim indice As Integer = btn.TabIndex
        'Dim idEliminar As Guid = Guid.Parse(btn.CommandArgument)
        btnEventoConfirmar.CommandArgument = btnEliminar.CommandName
        btnEventoConfirmar.TabIndex = btnEliminar.TabIndex
        lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-ban'></i> Está seguro que desea eliminar el área seleccionada?</div>"
            sb.Append("<script> $('#myModalConfirm').modal('show');</script>")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", sb.ToString, False)

        'Catch ex As Exception
        '    OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        'End Try
    End Sub

    Protected Sub btnEventoConfirmar_Click(sender As Object, e As EventArgs) Handles btnEventoConfirmar.Click
        Try
            Dim btnEliminar As Button = sender
            Dim idEliminar = Guid.Parse(btnEliminar.CommandArgument.ToString)
            Dim areaEliminar = New Proceso_ObtenerArea() With {.id = idEliminar}.Ejecutar()

            areaEliminar.idUsuarioMovimiento = IdUsuario
            areaEliminar.ipUsuario = direccionIP
            areaEliminar.idSistema = sistemaActivo.id

            Dim respuesta = New Proceso_EliminarArea() With {.id = idEliminar, .idSistema = sistemaActivo.id, .ipUsuario = direccionIP, .idUsuarioMovimiento = IdUsuario}.Ejecutar()
            Select Case respuesta.respuesta

                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                    If respuesta.comentario = "actualizado" Then
                        OnMostrarMensajeAccion("Completado", "El área se ha desactivado debido a que esta ocupado por otras entidades", nspPopup.tipoPopup.Verde, True, "management/area/frmConsultaArea.aspx")
                    Else
                        OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_elimino, "área"), nspPopup.tipoPopup.Verde, True, "management/area/frmConsultaArea.aspx")
                    End If

                    'llenarLista()
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try


    End Sub
End Class
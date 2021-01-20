Imports CES.nspPopup
Imports cadenero.CRN.nspUsuario
Imports CES.nspFirma
Imports CRN.nspFirma
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmFirma : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarComboUsuarios()
                cargarLista()
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiar()
        mandaDefault()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim respuestaValidacion = validarCampos()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario)
            End If
            Dim respuestaValidacionDuplicados = validarDuplicados()
            If respuestaValidacionDuplicados.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacionDuplicados.comentario)
            End If
            Dim X As New nspPaginaBase.PaginaBase
            Dim idSistema = X.sistemaActivo.id
            Dim firma = New CES.nspFirma.firma
            firma.nombre = cmbFirma.SelectedValue.ToString
            firma.id = Guid.NewGuid()
            firma.idUsuario = Guid.Parse(cmbUsuarios.SelectedValue)
            firma.ipUsuario = direccionIP
            firma.idSistema = idSistema
            firma.idUsuarioMovimiento = IdUsuario
            Dim respuesta = New Proceso_AgregarFirma() With {.entidad = firma}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "firma"), tipoPopup.Verde, True, "management/catalogos/Firmas/frmFirma.aspx")
                    cargarLista()
                    limpiar()
                Case tipoRespuestaDelProceso.Advertencia
                    Throw New Exception(respuesta.comentario)
                Case tipoRespuestaDelProceso.NoCompletado
                    Throw New Exception(respuesta.comentario)
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Protected Sub btnEliminar_OnClick(sender As Object, e As EventArgs)
        Try
            Dim btnEliminar As LinkButton = sender
            Dim sb As StringBuilder = New StringBuilder
            Dim indice As Integer = btnEliminar.TabIndex
            Dim id As Guid = Guid.Parse(btnEliminar.CommandArgument)
            btnEventoConfirmar.CommandArgument = btnEliminar.CommandArgument
            btnEventoConfirmar.TabIndex = btnEliminar.TabIndex
            lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-ban'></i> Está seguro que desea eliminar la firma seleccionada?</div>"
            sb.Append("<script> $('#myModalConfirm').modal('show');</script>")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", sb.ToString, False)
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try

    End Sub
    Protected Sub btnEventoConfirmar_Click(sender As Object, e As EventArgs) Handles btnEventoConfirmar.Click

        Dim btnEliminar As Button = sender
        Dim id = Guid.Parse(btnEliminar.CommandArgument.ToString)
        Dim firmaEliminar = New Proceso_ObtenerFirma() With {.id = id}.Ejecutar
        firmaEliminar.idUsuario = IdUsuario
        firmaEliminar.ipUsuario = direccionIP
        firmaEliminar.idSistema = sistemaActivo.id
        firmaEliminar.idUsuarioMovimiento = IdUsuario
        Dim respuesta = New Proceso_EliminarFirma() With {.id = id, .idSistema = sistemaActivo.id, .idUsuarioMovimiento = IdUsuario, .ipUsuario = direccionIP, .esActivo = False}.Ejecutar()
        Select Case respuesta.respuesta
            Case tipoRespuestaDelProceso.Completado
                Select Case respuesta.comentario
                    Case "Eliminó"
                        OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_elimino, "firma"), tipoPopup.Verde, True, "management/catalogos/Firmas/frmFirma.aspx")
                        cargarLista()
                    Case "Desactivó"
                        OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje("Tu firma se deactivó porque está usado por algún otro proceso", "firma"), tipoPopup.Verde, True, "management/catalogos/Firmas/frmFirma.aspx")
                        cargarLista()
                End Select
            Case tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
            Case tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select
    End Sub

#Region "metodos"
    Private Sub llenarComboUsuarios()

        ' Dim idsistemas As Guid = Sistema.id
        'falta la consulta de usuarios por idArea con area de almacen 
        ' Dim lista = New cadenero.CRN.nspUsuario.Proceso_ObtenerUsuarios() With {.tipoConsulta = cadenero.entidades.nspUsuario.tipoConsultaUsuario.esActivo, .esActivo = True}.Ejecutar.Where(Function(a) a.idSistema = Sistema.id)
        Dim lista = New Proceso_ObtenerUsuarios() With {.tipoConsulta = cadenero.entidades.nspUsuario.tipoConsultaUsuario.todos}.Ejecutar()
        cmbUsuarios.DataValueField = "id"
        cmbUsuarios.DataTextField = "nombre"
        cmbUsuarios.Items.Add("Seleccione un elemento de la lista")
        cmbUsuarios.DataSource = lista.OrderBy(Function(a) a.nombre).ToList
        cmbUsuarios.DataBind()
        cmbUsuarios.SelectedValue = "Seleccione un elemento de la lista"
    End Sub

    Private Sub cargarLista()
        Dim X As New nspPaginaBase.PaginaBase
        Dim idSistema = X.sistemaActivo.id
        Dim listaFirmas = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.esActivoXidSistema, .esActivo = True, .idSistema = idSistema}.Ejecutar()
        lvsFirmas.DataSource = listaFirmas.ToList
        lvsFirmas.DataBind()
    End Sub
    Private Sub limpiar()
        cmbFirma.SelectedValue = "Seleccione un elemento de la lista"
        cmbUsuarios.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Private Function validarCampos() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Dim mensaje As New notificacionesDeUsuario
        If Not cmbUsuarios.SelectedValue <> "Seleccione un elemento de la lista" Then
            respuesta.comentario = "El usuario es un campo obligatorio."
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            Throw New Exception(respuesta.comentario)
        End If
        If Not cmbFirma.SelectedValue <> "Elige un tipo de firma" Then
            respuesta.comentario = "El tipo de firma es un campo obligatorio."
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function

    Private Function validarDuplicados() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Dim mensaje As New notificacionesDeUsuario
        Dim X As New nspPaginaBase.PaginaBase
        Dim idSistema = X.sistemaActivo.id
        Dim listaFirmas = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.esActivoXidSistema, .esActivo = True, .idSistema = idSistema}.Ejecutar()
        For i = 0 To listaFirmas.Count - 1
            If listaFirmas(i).idUsuario = Guid.Parse(cmbUsuarios.SelectedValue) Then
                If listaFirmas(i).nombre = cmbFirma.SelectedValue Then
                    respuesta.comentario = "No se permite duplicar firma."
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            End If
        Next
        Return respuesta
    End Function
#End Region
End Class
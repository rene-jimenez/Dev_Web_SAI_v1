Imports CES, CRN
Imports CRN.nspRol
Imports CES.nspRol
Imports CPD
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup
Imports System.Web.UI.HtmlControls
Public Class frmRol : Inherits nspPaginaBase.PaginaBase

    Dim mensajePopop As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarListView()
            divPanelLateral.Visible = False
        End If
    End Sub

    Private Sub poblarListView()
        Dim lista = New cadenero.CRN.nspRol.Proceso_ObtenerListaRoles() With {.tipoConsulta = cadenero.entidades.nspRol.tipoConsultaRol.Todos}.Ejecutar()
        lsvRol.DataSource = lista
        lsvRol.DataBind()
    End Sub
    Protected Sub btnANuevo_Click(sender As Object, e As EventArgs)
        divPanelLateral.Visible = True
        updateAgregar.Update()
    End Sub
    '   myModalConfirm
    Protected Sub btnEventoConfirmar_Click(sender As Object, e As EventArgs) Handles btnEventoConfirmar.Click
        Dim btnActivoDesactivar As Button = sender
        Dim rolEliminar = New cadenero.CRN.nspRol.Proceso_ObtenerRol() With {.id = Guid.Parse(btnActivoDesactivar.CommandArgument.ToString)}.Ejecutar
        rolEliminar.nombre = rolEliminar.nombre.ToString
        rolEliminar.idUsuarioMovimiento = IdUsuario
        rolEliminar.ipUsuario = direccionIP
        Dim respuesta = New cadenero.CRN.nspRol.Proceso_EliminarRol() With {.entidad = rolEliminar, .estado = False}.Ejecutar()
        Select Case respuesta.respuesta
            Case tipoRespuestaDelProceso.Completado
                Select Case respuesta.comentario
                    Case "Eliminó"
                        OnMostrarMensajeAccion("Completado", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_elimino, "rol"), tipoPopup.Verde, True, "management/rol/frmRol.aspx")
                        poblarListView()
                    Case "Desactivó"
                        OnMostrarMensajeAccion("Completado", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_desactivo, "rol"), tipoPopup.Verde, True, "management/rol/frmRol.aspx")
                        poblarListView()
                    Case "Activó"
                        OnMostrarMensajeAccion("Completado", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_desactivo, "rol"), tipoPopup.Verde, True, "management/rol/frmRol.aspx")
                        poblarListView()
                        updateLista.Update()
                End Select
            Case tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
            Case tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select
    End Sub

    Protected Sub btnActivoEditar_Click(sender As Object, e As EventArgs)
        btnActualizar.Visible = True
        btnAgregarRol.Visible = False
        divPanelLateral.Visible = True
        divPanelLateral.Visible = True
        updateAgregar.Update()
        Dim btnActivoEditar As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnActivoEditar.CommandArgument)
        Dim ide = id.ToString
        lblHiddenId.Value = ide
        Dim rol = New cadenero.CRN.nspRol.Proceso_ObtenerRol() With {.id = id}.Ejecutar
        txbNuevoRol.Text = rol.nombre.ToString
    End Sub

    Protected Sub btnActivoDesactivar_Click(sender As Object, e As EventArgs)
        Dim btnActivoDesactivar As LinkButton = sender
        Dim index = btnActivoDesactivar.TabIndex
        Dim id As Guid = Guid.Parse(btnActivoDesactivar.CommandArgument)
        Dim sb As StringBuilder = New StringBuilder
        btnEventoConfirmar.CommandArgument = id.ToString
        btnEventoConfirmar.TabIndex = index
        lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-ban'></i> Está seguro que desea desactivar el registro seleccionado?</div>"
        sb.Append("<script> $('#myModalConfirm').modal('show');</script>")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", sb.ToString, False)
    End Sub
    Protected Sub btnDesactivoActivar_Click(sender As Object, e As EventArgs)
        Dim btnEditar As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnEditar.CommandArgument)
        Dim rolEditar = New cadenero.CRN.nspRol.Proceso_ObtenerRol() With {.id = id}.Ejecutar()
        rolEditar.id = id
        rolEditar.esActivo = True
        rolEditar.nombre = rolEditar.nombre.ToString
        rolEditar.idUsuarioMovimiento = IdUsuario
        rolEditar.ipUsuario = direccionIP
        Dim respuesta = New cadenero.CRN.nspRol.Proceso_ActualizarRol() With {.entidad = rolEditar}.Ejecutar()
        Select Case respuesta.respuesta
            Case tipoRespuestaDelProceso.Completado
                OnMostrarMensajeAccion("Completado", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_activo, "rol"), tipoPopup.Verde, True, "management/rol/frmRol.aspx")
                poblarListView()
            Case tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
            Case tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select
    End Sub
    'Protected Sub btnCancelar_OnClick(sender As Object, e As EventArgs)
    '    Dim btnCancelar As HtmlButton = sender
    '    Dim posicion As Integer = CInt(btnCancelar.Attributes("TabIndex"))
    '    ' Dim txbNombre As TextBox = CType(lsvRol.Items(posicion).FindControl("txbLsvNombre"), TextBox)
    '    Dim lblNombre As Label = CType(lsvRol.Items(posicion).FindControl("labelLsvNombre"), Label)
    '    Dim chkEsActivo As CheckBox = CType(lsvRol.Items(posicion).FindControl("chkEsActivo"), CheckBox)
    '    Dim btnActualizar As HtmlButton = CType(lsvRol.Items(posicion).FindControl("btnActualizar"), HtmlButton)
    '    Dim btnEditar As HtmlButton = CType(lsvRol.Items(posicion).FindControl("btnEditar"), HtmlButton)
    '    Dim btnEliminar As HtmlButton = CType(lsvRol.Items(posicion).FindControl("btnEliminar"), HtmlButton)

    '    lblNombre.Visible = True
    '    'txbNombre.Visible = False
    '    chkEsActivo.Visible = False
    '    btnActualizar.Visible = False
    '    btnCancelar.Visible = False
    '    btnEliminar.Visible = True
    '    btnEditar.Visible = True
    'End Sub
    'Protected Sub btnActualizar_OnClick(sender As Object, e As EventArgs)
    '    Try
    '        Dim btnActualizar As HtmlButton = sender
    '        Dim posicion As Integer = CInt(btnActualizar.Attributes("TabIndex"))
    '        Dim idX As Guid = Guid.Parse(btnActualizar.Attributes("CommandArgument"))
    '        ' Dim txbNombre As TextBox = CType(lsvRol.Items(posicion).FindControl("txbLsvNombre"), TextBox)
    '        Dim chkEsActivo As CheckBox = CType(lsvRol.Items(posicion).FindControl("chkEsActivo"), CheckBox)
    '        Dim rolEditar As New cadenero.entidades.nspRol.rol()
    '        rolEditar.id = idX
    '        ' rolEditar.nombre = txbNombre.Text
    '        rolEditar.esActivo = chkEsActivo.Checked
    '        rolEditar.idUsuarioMovimiento = Guid.Parse("11111111-0000-0000-1111-111111111111")
    '        rolEditar.ipUsuario = "1.1.1.1"
    '        Dim respuesta = New cadenero.CRN.nspRol.Proceso_ActualizarRol() With {.entidad = rolEditar}.Ejecutar()

    '        Select Case respuesta.respuesta
    '            Case tipoRespuestaDelProceso.Completado
    '                Dim lblNombre As Label = CType(lsvRol.Items(posicion).FindControl("labelLsvNombre"), Label)
    '                Dim btnEditar As HtmlButton = CType(lsvRol.Items(posicion).FindControl("btnEditar"), HtmlButton)
    '                Dim btnCancelar As HtmlButton = CType(lsvRol.Items(posicion).FindControl("btnCancelar"), HtmlButton)
    '                Dim btnEliminar As HtmlButton = CType(lsvRol.Items(posicion).FindControl("btnEliminar"), HtmlButton)
    '                lblNombre.Visible = True
    '                ' txbNombre.Visible = False
    '                chkEsActivo.Visible = False
    '                btnActualizar.Visible = False
    '                btnCancelar.Visible = False
    '                btnEliminar.Visible = True
    '                btnEditar.Visible = True
    '                OnMostrarMensajeAccion("Rol", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "rol"), tipoPopup.Verde, True, "management/rol/frmRol.aspx")
    '                poblarListView()
    '            Case tipoRespuestaDelProceso.Advertencia
    '                MsgBox(respuesta.comentario, MsgBoxStyle.Exclamation)
    '            Case tipoRespuestaDelProceso.NoCompletado
    '                MsgBox(respuesta.comentario, MsgBoxStyle.Critical)
    '        End Select
    '    Catch ex As Exception
    '        MsgBox(ex.Message.ToString)
    '    End Try
    'End Sub
    'Protected Sub btnEliminar_OnClick(sender As Object, e As EventArgs)
    '    Dim btnEliminar As LinkButton = sender
    '    Dim posicion As Integer = CInt(btnEliminar.Attributes("TabIndex"))
    '    Dim idTab As Guid = Guid.Parse(btnEliminar.Attributes("CommandArgument"))
    '    Dim sb As StringBuilder = New StringBuilder
    '    Dim chkEsActivo As CheckBox = CType(lsvRol.Items(posicion).FindControl("chkEsActivo"), CheckBox)
    '    btnEventoConfirmar.CommandArgument = idTab.ToString
    '    btnEventoConfirmar.TabIndex = posicion
    '    If chkEsActivo.Checked = True Then
    '        lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-ban'></i> Está seguro que desea desactivar el registro seleccionado?</div>"
    '    Else
    '        lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-info'></i> Está seguro que desea activar el registro seleccionado?</div>"
    '    End If
    '    sb.Append("<script> $('#myModalConfirm').modal('show');</script>")
    '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", sb.ToString, False)
    'End Sub
    Private Sub btnAgregarRol_Click(sender As Object, e As EventArgs) Handles btnAgregarRol.Click
        Try
            Dim nuevoRol As New cadenero.entidades.nspRol.rol()
            nuevoRol.id = Guid.NewGuid
            nuevoRol.nombre = txbNuevoRol.Text
            nuevoRol.esActivo = True
            nuevoRol.idUsuarioMovimiento = IdUsuario
            nuevoRol.ipUsuario = direccionIP
            Dim respuesta = New cadenero.CRN.nspRol.Proceso_AgregarRol() With {.entidad = nuevoRol}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    txbNuevoRol.Text = ""
                    SetFocus(txbNuevoRol)
                    OnMostrarMensajeAccion("Completado", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "rol"), tipoPopup.Verde, True, "management/rol/frmRol.aspx")
                    poblarListView()
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        mandaDefault()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            Dim id = lblHiddenId.Value
            Dim btnActualizar As LinkButton = sender
            Dim rolEditar = New cadenero.CRN.nspRol.Proceso_ObtenerRol() With {.id = Guid.Parse(id)}.Ejecutar()
            rolEditar.nombre = txbNuevoRol.Text
            rolEditar.esActivo = True
            rolEditar.idUsuarioMovimiento = IdUsuario
            rolEditar.ipUsuario = direccionIP
            Dim respuesta = New cadenero.CRN.nspRol.Proceso_ActualizarRol() With {.entidad = rolEditar}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Completado", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "rol"), tipoPopup.Verde, True, "management/rol/frmRol.aspx")
                    poblarListView()
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
End Class


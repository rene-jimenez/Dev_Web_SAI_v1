Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmAltaUsuario : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                poblarArea()
                poblarRol()
                poblarSistema()
                Dim band = Request.QueryString("band")
                Select Case band
                    Case "add"
                        If Not Session("listaRol") Is Nothing Then
                            Session.Remove("listaRol")
                        End If
                        divResetPsw.Visible = False
                    Case "edt"
                        poblarUsuario()
                        poblarListaRolUsuario()
                        divResetPsw.Visible = True
                End Select

            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "btn"
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dim band = Request.QueryString("band")
        Select Case band
            Case "add"
                mandaDefault()
            Case "edt"
                Response.Redirect("frmConsultaUsuario.aspx")
        End Select
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim band = Request.QueryString("band")
            Select Case band
                Case "add"
                    Dim listaRoles As List(Of cadenero.entidades.nspUsuarioRol.usuarioRol) = CType(Session("listaRol"), List(Of cadenero.entidades.nspUsuarioRol.usuarioRol))
                    Dim respuestaValidacion = validarAgregarUsuario(listaRoles)
                    If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                        OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                        Exit Sub
                    End If
                    Dim entidad = New cadenero.entidades.nspUsuario.detalleUsuario()
                    entidad.id = Guid.NewGuid()
                    entidad.nombre = txbNombre.Text
                    entidad.usuario = txbUsuario.Text
                    entidad.esActivo = True
                    entidad.email = txbCorreoElectronico.Text
                    entidad.foto = "avatar.png"
                    entidad.telefono = txbTelefono.Text
                    entidad.ultimaSesion = DateTime.Now
                    entidad.fechaAlta = Date.Now
                    entidad.idArea = Guid.Parse(cmbArea.SelectedValue.ToString)
                    entidad.idUsuarioMovimiento = IdUsuario
                    entidad.ipUsuario = direccionIP
                    entidad.idSistema = sistemaActivo.id
                    entidad.contrasena = txbContrasena.Text

                    Dim respuesta = New cadenero.CRN.nspUsuario.Proceso_AgregarUsuario() With {.entidad = entidad, .lista = listaRoles}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "usuario"), tipoPopup.Verde, True, "management/default.aspx")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "edt"
                    Dim respuestaValidacion = validarEditarUsuario()
                    If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                        OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                        Exit Sub
                    End If
                    Dim idUsuarioEditar = Request.QueryString("id")
                    Dim entidadEditarUsuario = New cadenero.CRN.nspUsuario.Proceso_ObtenerUsuario() With {.id = Guid.Parse(idUsuarioEditar)}.Ejecutar
                    entidadEditarUsuario.nombre = txbNombre.Text
                    entidadEditarUsuario.usuario = txbUsuario.Text
                    entidadEditarUsuario.email = txbCorreoElectronico.Text
                    entidadEditarUsuario.telefono = txbTelefono.Text
                    entidadEditarUsuario.ultimaSesion = DateTime.Now
                    entidadEditarUsuario.esResetContrasena = True
                    entidadEditarUsuario.idArea = Guid.Parse(cmbArea.SelectedValue.ToString)
                    entidadEditarUsuario.idUsuarioMovimiento = IdUsuario
                    entidadEditarUsuario.ipUsuario = direccionIP
                    entidadEditarUsuario.idSistema = sistemaActivo.id
                    entidadEditarUsuario.esResetContrasena = chkResetPsw.Checked
                    Dim respuesta = New cadenero.CRN.nspUsuario.Proceso_ActualizarUsuario() With {.entidad = entidadEditarUsuario, .reset = True}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "usuario"), tipoPopup.Verde, True, "management/usuario/frmConsultaUsuario.aspx")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Private Sub btnAgregarRol_Click(sender As Object, e As EventArgs) Handles btnAgregarRol.Click
        Try
            If cmbRol.SelectedValue = "Seleccione un elemento de la lista" Then
                OnMostrarMensajeAccion("Atención", "Inserte al menos un rol antes de continuar.", tipoPopup.Naranja, False, "")
                Exit Sub
                'Throw New Exception("Seleccione al menos un rol")
            End If

            If Not Request.QueryString("id") Is Nothing Then 'si es editar
                divListaroles.Visible = True
                Dim idUsuarioNuevo = Request.QueryString("id")
                Dim nuevoUsuarioRol As New cadenero.entidades.nspUsuarioRol.usuarioRol
                nuevoUsuarioRol.id = Guid.NewGuid
                nuevoUsuarioRol.idRol = Guid.Parse(cmbRol.SelectedValue)
                nuevoUsuarioRol.idUsuario = Guid.Parse(idUsuarioNuevo)
                nuevoUsuarioRol.idUsuarioMovimiento = IdUsuario
                nuevoUsuarioRol.idSistemaRol = Guid.Parse(cmbSistema.SelectedValue)
                nuevoUsuarioRol.ipUsuario = direccionIP
                nuevoUsuarioRol.idSistema = sistemaActivo.idSistema
                Dim usuarioRolEditar = New cadenero.CRN.nspUsuarioRol.Proceso_ObtenerUsuarioRoles() With {.tipoConsulta = cadenero.entidades.nspUsuarioRol.tipoConsultaUsuarioRol.idUsuario, .idUsuario = Guid.Parse(idUsuarioNuevo)}.Ejecutar
                For i = 0 To usuarioRolEditar.Count - 1
                    If usuarioRolEditar(i).idRol = Guid.Parse(cmbRol.SelectedValue) And usuarioRolEditar(i).idSistemaRol = Guid.Parse(cmbSistema.SelectedValue) Then
                        OnMostrarMensajeAccion("Atención", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_registro_estaria_duplicado, "rol"), tipoPopup.Naranja, False, "")
                        Exit Sub
                    End If
                Next
                Dim respuesta = New cadenero.CRN.nspUsuarioRol.Proceso_AgregarUsuarioRol() With {.entidad = nuevoUsuarioRol}.Ejecutar()
                poblarListaRolUsuario()
            Else
                divListaroles.Visible = True
                Dim nuevoRegistro As New cadenero.entidades.nspUsuarioRol.usuarioRol
                nuevoRegistro.id = Guid.NewGuid
                nuevoRegistro.idRol = Guid.Parse(cmbRol.SelectedValue)
                nuevoRegistro.nombre = cmbRol.SelectedItem.ToString
                nuevoRegistro.idSistemaRol = Guid.Parse(cmbSistema.SelectedValue)
                nuevoRegistro.idSistema = Guid.Parse("efd014a0-4091-4daa-80d6-0fa5f9deaa99")
                nuevoRegistro.idUsuarioMovimiento = IdUsuario
                nuevoRegistro.ipUsuario = direccionIP
                Dim listaSistemas = New CRN.nspSistema.Proceso_ObtenerSistema() With {.id = Guid.Parse(cmbSistema.SelectedValue)}.Ejecutar()

                nuevoRegistro._nombreSistema = listaSistemas.nombre
                nuevoRegistro._añoSistema = listaSistemas.año
                Dim lista As New List(Of cadenero.entidades.nspUsuarioRol.usuarioRol)
                If Session("listaRol") Is Nothing Then
                    lista.Add(nuevoRegistro)
                    Session("listaRol") = lista
                Else
                    lista = Session("listaRol")
                    If lista.Count = 0 Then
                        lista.Add(nuevoRegistro)
                        Session("listaRol") = lista
                    Else
                        Dim bandera As Boolean = False
                        Dim bandera2 As Boolean = False
                        For i = 0 To lista.Count - 1
                            If lista(i).idRol = nuevoRegistro.idRol And lista(i).idSistemaRol = nuevoRegistro.idSistemaRol Then
                                bandera = True

                            End If
                            If lista(i)._nombreSistema + lista(i)._añoSistema = nuevoRegistro._nombreSistema.ToString + nuevoRegistro._añoSistema.ToString Then
                                bandera2 = True
                            End If
                        Next
                        If bandera = False And bandera2 = False Then
                            lista.Add(nuevoRegistro)
                            Session("listaRol") = lista
                        Else
                            If bandera = True Then
                                OnMostrarMensajeAccion("Atención", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_registro_estaria_duplicado, "rol"), tipoPopup.Naranja, False, "")
                            End If
                            If bandera2 = True Then
                                OnMostrarMensajeAccion("Atención", "No se permiten dos roles por sistema.", tipoPopup.Naranja, False, "")
                            End If
                            Exit Sub
                        End If
                    End If
                End If
                lvsRoles.DataSource = lista.ToList
                lvsRoles.DataBind()
                divListaroles.Visible = True
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try

    End Sub
#End Region


    Protected Sub btnEliminarRol_OnClick(sender As Object, e As EventArgs)
        Try
            If Not Request.QueryString("id") Is Nothing Then   'idUsuario
                Dim respuestaValidacion = validarEliminarUsuarioRol()
                If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                    updatePanelBtns5.Update()
                    OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                    Exit Sub
                End If


                Dim boton As LinkButton = sender
                Dim idUsuarioRol = Guid.Parse(boton.ToolTip)
                If Request.QueryString("id").ToString = IdUsuario.ToString Then 'si el usuario que esta logeado es el que quiere editar
                    Dim consultarUsuarioRol = New cadenero.CRN.nspUsuarioRol.Proceso_ObtenerUsuarioRol With {.id = idUsuarioRol}.Ejecutar()
                    If consultarUsuarioRol.idSistemaRol = sistemaActivo.idSistema Then
                        OnMostrarMensajeAccion("Atención", "No puede eliminar el rol que estas usando", tipoPopup.Naranja, False, "")
                        Exit Sub
                    End If
                End If

                    Dim respuesta = New cadenero.CRN.nspUsuarioRol.Proceso_EliminarUsuarioRol() With {.id = idUsuarioRol, .idUsuarioMovimiento = IdUsuario, .ipPcUsuario = direccionIP}.Ejecutar()
                Select Case respuesta.respuesta
                    Case tipoRespuestaDelProceso.Completado
                        poblarListaRolUsuario()
                        OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_elimino, "rol"), tipoPopup.Verde, False, "")
                    Case tipoRespuestaDelProceso.Advertencia
                        OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                    Case tipoRespuestaDelProceso.NoCompletado
                        OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                End Select

            Else
                Dim boton As LinkButton = sender
                Dim lista As List(Of cadenero.entidades.nspUsuarioRol.usuarioRol) = CType(Session("listaRol"), List(Of cadenero.entidades.nspUsuarioRol.usuarioRol))
                For i = 0 To lista.Count - 1
                    If lista(i).idRol = Guid.Parse(boton.CommandArgument) Then
                        lista.RemoveAt(i)
                        Exit For
                    End If
                Next
                Session("listaRol") = lista
                lvsRoles.DataSource = lista.ToList
                lvsRoles.DataBind()
            End If

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub

#Region "métodos"
    Public Sub poblarArea()
        'Dim consultarArea = New CRN.nspArea.Proceso_ObtenerAreas() With {.tipoConsulta = CES.nspArea.tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar()
        Dim consultarArea = New CRN.nspArea.Proceso_ObtenerAreas() With {.tipoConsulta = CES.nspArea.tipoConsultaArea.todos}.Ejecutar()
        cmbArea.DataValueField = "id"
        cmbArea.DataTextField = "nombre"
        cmbArea.Items.Add("Seleccione un elemento de la lista")
        cmbArea.DataSource = consultarArea.OrderBy(Function(a) a.nombre).ToList
        cmbArea.DataBind()
        cmbArea.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Public Sub poblarRol()
        Dim listaRol = New cadenero.CRN.nspRol.Proceso_ObtenerListaRoles() With {.tipoConsulta = cadenero.entidades.nspRol.tipoConsultaRol.esActivo, .esActivo = True}.Ejecutar()
        cmbRol.DataValueField = "id"
        cmbRol.DataTextField = "nombre"
        cmbRol.Items.Add("Seleccione un elemento de la lista")
        cmbRol.DataSource = listaRol.OrderBy(Function(a) a.nombre).ToList
        cmbRol.DataBind()
        cmbRol.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Public Sub limpiarFormulario()
        txbNombre.Text = ""
        txbUsuario.Text = ""
        txbContrasena2.Text = ""
        txbContrasena.Text = ""
        txbCorreoElectronico.Text = ""
        txbTelefono.Text = ""
    End Sub
    Public Sub poblarUsuario()
        Dim idU = Request.QueryString("id")
        divPsw1.Visible = False
        divPsw2.Visible = False
        Dim usuario = New cadenero.CRN.nspUsuario.Proceso_ObtenerUsuarios() With {.tipoConsulta = cadenero.entidades.nspUsuario.tipoConsultaUsuario.id, .id = Guid.Parse(idU)}.Ejecutar
        txbNombre.Text = usuario(0).nombre.ToString
        txbUsuario.Text = usuario(0).usuario.ToString
        txbTelefono.Text = usuario(0).telefono
        lblRolesDeUsuario.Text = "del usuario " + usuario(0).usuario.ToString
        txbCorreoElectronico.Text = usuario(0).email
        cmbArea.SelectedValue = usuario(0).idArea.ToString
        chkResetPsw.Checked = usuario(0).esResetContrasena
    End Sub
    Public Sub poblarSistema()
        Dim listaSistemas = New CRN.nspSistema.Proceso_ObtenerSistemas() With {.tipoConsulta = CES.nspSistema.tipoSistemaConsulta.todos}.Ejecutar()
        For i = 0 To listaSistemas.Count - 1
            listaSistemas(i).nombre = listaSistemas(i).nombre.ToString + " " + listaSistemas(i).año.ToString
        Next
        cmbSistema.DataValueField = "id"
        cmbSistema.DataTextField = "nombre"
        cmbSistema.Items.Add("Seleccione un elemento de la lista")
        cmbSistema.DataSource = listaSistemas.OrderBy(Function(a) a.nombre).ToList
        cmbSistema.DataBind()
        cmbSistema.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Public Sub poblarListaRolUsuario()
        divListaroles.Visible = True
        Dim idU = Request.QueryString("id")
        Dim listaRol = New cadenero.CRN.nspUsuarioRol.Proceso_ObtenerUsuarioRoles() With {.tipoConsulta = cadenero.entidades.nspUsuarioRol.tipoConsultaUsuarioRol.idUsuario, .idUsuario = Guid.Parse(idU)}.Ejecutar()
        lvsRoles.DataSource = listaRol.ToList
        lvsRoles.DataBind()
        Session("listaRol") = listaRol
    End Sub
    Private Function validarEliminarUsuarioRol() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Dim mensaje As New notificacionesDeUsuario
        Dim consultaRoles = New cadenero.CRN.nspUsuarioRol.Proceso_ObtenerUsuarioRoles() With {.tipoConsulta = cadenero.entidades.nspUsuarioRol.tipoConsultaUsuarioRol.idUsuario, .idUsuario = Guid.Parse(Request.QueryString("id"))}.Ejecutar()
        If consultaRoles.Count = 1 Then
            respuesta.comentario = "No puedes dejar el usuario sin rol"
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        End If
        Return respuesta
    End Function
    Private Function validarAgregarUsuario(listaRoles As List(Of cadenero.entidades.nspUsuarioRol.usuarioRol)) As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If txbNombre.Text.Length = 0 Then
                Throw New Exception("El nombre del usuario es un campo obligatorio.")
                SetFocus(txbNombre)
            End If
            If txbUsuario.Text.Length = 0 Then
                Throw New Exception("El usuario es un campo obligatorio.")
            End If
            If txbContrasena.Text.Length = 0 Then
                Throw New Exception("La contraseña es un campo obligatorio.")
            End If
            If txbContrasena2.Text.Length = 0 Then
                Throw New Exception("Confirmar contraseña es un campo obligatorio.")
            End If
            If txbTelefono.Text.Length > 1 And txbTelefono.Text.Length < 15 Then
                Throw New Exception("Completa el número de teléfono es un campo obligatorio.")
            End If
            If Not cmbArea.SelectedValue <> "Seleccione un elemento de la lista" Then
                Throw New Exception("El área es un campo obligatorio.")
            End If

            If listaRoles Is Nothing Then
                Throw New Exception("Inserte al menos un rol antes de continuar.")
            End If
            Return respuesta
        Catch ex As Exception
            respuesta.comentario = ex.Message.ToString
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            Return respuesta
        End Try

    End Function
    Private Function validarEditarUsuario() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If txbNombre.Text.Length = 0 Then
                Throw New Exception("El nombre del usuario es un campo obligatorio.")
            End If
            If txbUsuario.Text.Length = 0 Then
                Throw New Exception("El usuario es un campo obligatorio.")
            End If
            If txbTelefono.Text.Length > 1 And txbTelefono.Text.Length < 15 Then
                Throw New Exception("Completa el número de teléfono es un campo obligatorio.")
            End If
            If Not cmbArea.SelectedValue <> "Seleccione un elemento de la lista" Then
                Throw New Exception("El área es un campo obligatorio.")
            End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        End Try
        Return respuesta
    End Function
#End Region
End Class
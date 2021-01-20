Imports CES, CES.nspPopup, CRN
Imports CRN.nspProveedor, CRN.nspTelefonoProveedor
Imports CES.nspProveedor, CES.nspTelefonoProveedor

Imports Contexto.Notificaciones.controladorMensajes
Public Class frmProveedor1 : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim accion = Request.QueryString("band").ToString
                Select Case accion
                    Case "edt"
                        lblTitulo.Text = "Editar el "
                        Dim id As Guid = Guid.Parse(Request.QueryString("id").ToString) 'recibe id 
                        divtelefonos.Visible = True
                        Dim ProveedorEditar = New Proceso_ObtenerProveedor() With {.id = Guid.Parse(Request.QueryString("id"))}.Ejecutar()
                        CargarProveedorEditar(id)
                        poblarListaTelefono(id)
                    Case "add"
                        lblTitulo.Text = "Crear nuevo "
                        divtelefonos.Visible = False
                        Dim listaTelefono As New List(Of telefonoProveedor)
                        Session("listaTelefono") = listaTelefono
                    Case Else
                        mandaDefault()
                End Select
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
        'listaTelefono.RemoveAt()
    End Sub

#Region "btns"
    Private Sub lnkGuardarSub_Click(sender As Object, e As EventArgs) Handles lnkGuardarSub.Click
        Try
            Dim resultadoValidacion = validarProveedor()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim accion = Request.QueryString("band").ToString
            Select Case accion
                Case "edt"
                    Dim id As String = Request.QueryString("id").ToString  'recibe id 
                    Dim listaTel = New Proceso_ObtenerTelefonosProveedor() With {.tipoConsulta = tipoConsultaTelefonoProveedor.idProveedor, .idProveedor = Guid.Parse(id)}.ejecutar()
                    If listaTel.count = 0 Then
                        Throw New Exception(" Debes ingresar al menos un número teléfonico")
                    End If
                    Dim editProveedor = New Proceso_ObtenerProveedor() With {.id = Guid.Parse(Request.QueryString("id"))}.Ejecutar
                    editProveedor.nombre = txbNombre.Text
                    editProveedor.ciudad = txbCiudad.Text
                    editProveedor.codigoPostal = txbCodigoPostal.Text
                    editProveedor.colonia = txbColonia.Text
                    editProveedor.contacto = txbContacto.Text
                    editProveedor.domicilioFiscal = txbdomicilioFiscal.Text
                    editProveedor.domicilio = txbdomicilio.Text
                    editProveedor.estado = txbEstado.Text
                    editProveedor.giro = txbGiro.Text
                    editProveedor.rfc = txbRfc.Text
                    editProveedor.idUsuarioMovimiento = IdUsuario
                    editProveedor.ipUsuario = direccionIP
                    editProveedor.idSistema = sistemaActivo.idSistema
                    Dim respuesta = New Proceso_ActualizarProveedor() With {.entidad = editProveedor}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "Proveedor"), nspPopup.tipoPopup.Verde, True, "management/Proveedor/frmConsultaProveedor.aspx")
                            Session.Remove("listaTelefono")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select

                Case "add"
                    Dim listaTelefono As List(Of telefonoProveedor) = CType(Session("listaTelefono"), List(Of telefonoProveedor))
                    If listaTelefono.Count = 0 Then
                        Throw New Exception(" Debes ingresar al menos un número teléfonico")
                    End If
                    Dim newProveedor As New proveedor
                        newProveedor.id = Guid.NewGuid
                        newProveedor.nombre = txbNombre.Text
                        newProveedor.ciudad = txbCiudad.Text
                        newProveedor.codigoPostal = txbCodigoPostal.Text
                        newProveedor.colonia = txbColonia.Text
                        newProveedor.contacto = txbContacto.Text
                        newProveedor.domicilioFiscal = txbdomicilioFiscal.Text
                        newProveedor.domicilio = txbdomicilio.Text
                        newProveedor.estado = txbEstado.Text
                        newProveedor.giro = txbGiro.Text
                        newProveedor.rfc = txbRfc.Text
                        newProveedor.idUsuarioMovimiento = IdUsuario
                        newProveedor.ipUsuario = direccionIP
                        newProveedor.idSistema = sistemaActivo.idSistema

                    Dim respuesta = New Proceso_AgregarProveedor() With {.entidad = newProveedor, .listaTelefono = listaTelefono}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "proveedor"), tipoPopup.Verde, True, "management/Proveedor/frmProveedor.aspx?band=add")
                            Session.Remove("listaTelefono")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Protected Sub btnEliminartel_Click(sender As Object, e As EventArgs)
        Try
            Dim btn As LinkButton = sender
            Dim indice As Integer = btn.TabIndex
            Dim idEliminar As Guid = Guid.Parse(btn.CommandArgument)
            If Not Request.QueryString("id") Is Nothing Then

                'VALIDAR SI LA LISTA TIEN AMS DE UN TELEFONO
                Dim listaTelefonoProveedor = New Proceso_ObtenerTelefonosProveedor() With {.tipoConsulta = tipoConsultaTelefonoProveedor.idProveedor, .idProveedor = Guid.Parse(Request.QueryString("id"))}.Ejecutar()
                If listaTelefonoProveedor.count = 1 Then
                    Dim sb As StringBuilder = New StringBuilder
                    btnEventoConfirmar.CommandArgument = btn.CommandArgument
                    btnEventoConfirmar.TabIndex = btn.TabIndex
                    lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-ban animated infinite wobble c-red fa-3x'></i></div> <br /><div style='text-align: center'> ¿Está seguro que desea eliminar el registro seleccionado?</div>"
                    sb.Append("<script> $('#myModalConfirm').modal('show');</script>")
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", sb.ToString, False)
                Else
                    Dim respuesta = New Proceso_EliminarTelefonoProveedor() With {.id = idEliminar, .ipUsuario = direccionIP, .idUsuarioMovimiento = IdUsuario}.Ejecutar
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            poblarListaTelefono(Guid.Parse(Request.QueryString("id")))
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                End If
            Else
                Dim listaTelefono As List(Of telefonoProveedor) = CType(Session("listaTelefono"), List(Of telefonoProveedor))
                listaTelefono.RemoveAt(indice)
                Session("listaTelefono") = listaTelefono
                lvsElemento.DataSource = listaTelefono
                lvsElemento.DataBind()
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Protected Sub btnEventoConfirmar_Click(sender As Object, e As EventArgs)
        Dim btnEliminar As LinkButton = sender
        Dim idEliminar = Guid.Parse(btnEliminar.CommandArgument.ToString)
        Dim respuesta = New Proceso_EliminarTelefonoProveedor() With {.id = idEliminar, .ipUsuario = direccionIP, .idUsuarioMovimiento = IdUsuario}.Ejecutar
        Select Case respuesta.respuesta
            Case tipoRespuestaDelProceso.Completado
                poblarListaTelefono(Guid.Parse(Request.QueryString("id")))
                updatepaneltab2.Update()
            Case tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
            Case tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
        End Select
    End Sub
    Protected Sub btnAgregarTelefono_Click(sender As Object, e As EventArgs)
        Try
            divtelefonos.Visible = True
            Dim resultadoValidacion = validartelefono()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            If Not Request.QueryString("id") Is Nothing Then
                Dim nuevoTelefono As New telefonoProveedor
                nuevoTelefono.id = Guid.NewGuid
                nuevoTelefono.idProveedor = Guid.Parse(Request.QueryString("id"))
                nuevoTelefono.codigoLargaDistancia = txblargaDistancia.Text
                nuevoTelefono.numero = txbNumero.Text
                nuevoTelefono.extension = txbExtension.Text
                nuevoTelefono.tipo = txbTipo.Text
                nuevoTelefono.idUsuarioMovimiento = IdUsuario
                nuevoTelefono.ipUsuario = direccionIP
                Dim respuesta = New Proceso_AgregarTelefonoProveedor() With {.entidad = nuevoTelefono}.Ejecutar
                Select Case respuesta.respuesta
                    Case tipoRespuestaDelProceso.Completado
                        limpiarTelefonos()
                        poblarListaTelefono(Guid.Parse(Request.QueryString("id")))
                    Case tipoRespuestaDelProceso.Advertencia
                        OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                    Case tipoRespuestaDelProceso.NoCompletado
                        OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                End Select
            Else
                'Try
                Dim listaRemplazar As List(Of telefonoProveedor) = CType(Session("listaTelefono"), List(Of telefonoProveedor))
                    For i = 0 To listaRemplazar.Count - 1
                        If (listaRemplazar(i).codigoLargaDistancia & listaRemplazar(i).numero) = txblargaDistancia.Text & txbNumero.Text Then
                            Throw New Exception(" El télefono estaría duplicado.")
                        End If
                    Next

                    Dim nuevoTelefono As New telefonoProveedor
                    nuevoTelefono.id = Guid.NewGuid
                    nuevoTelefono.codigoLargaDistancia = txblargaDistancia.Text
                    nuevoTelefono.numero = txbNumero.Text
                    nuevoTelefono.extension = txbExtension.Text
                    nuevoTelefono.tipo = txbTipo.Text
                    nuevoTelefono.idUsuarioMovimiento = IdUsuario
                    nuevoTelefono.ipUsuario = direccionIP
                    listaRemplazar.Add(nuevoTelefono)
                    Session("listaTelefono") = listaRemplazar
                    lvsElemento.DataSource = listaRemplazar
                    lvsElemento.DataBind()
                    limpiarTelefonos()
                ' Catch ex As Exception
                ' OnMostrarMensajeAccion("Proveedor", "Error: " & ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
                'End Try
                lvsElemento.Visible = True
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", "" & ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Private Sub lnkCerrar2_Click(sender As Object, e As EventArgs) Handles lnkCerrar2.Click
        mandaDefault()
    End Sub
#End Region
#Region "métodos"
    Private Sub poblarListaTelefono(idProveedor As Guid)
        Dim listaTelefonoProveedor = New Proceso_ObtenerTelefonosProveedor() With {.tipoConsulta = tipoConsultaTelefonoProveedor.idProveedor, .idProveedor = idProveedor}.Ejecutar
        lvsElemento.DataSource = listaTelefonoProveedor.ToList
        lvsElemento.DataBind()
    End Sub
    Private Sub limpiarTelefonos()
        txblargaDistancia.Text = String.Empty
        txbNumero.Text = String.Empty
        txbExtension.Text = String.Empty
        txbTipo.Text = String.Empty
    End Sub

    Private Sub CargarProveedorEditar(idProveedor As Guid)
        Dim consultaObtenerUnproveedor = New Proceso_ObtenerProveedor() With {.id = idProveedor}.Ejecutar
        txbNombre.Text = consultaObtenerUnproveedor.nombre
        txbCiudad.Text = consultaObtenerUnproveedor.ciudad
        txbCodigoPostal.Text = consultaObtenerUnproveedor.codigoPostal
        txbColonia.Text = consultaObtenerUnproveedor.colonia
        txbContacto.Text = consultaObtenerUnproveedor.contacto
        txbdomicilioFiscal.Text = consultaObtenerUnproveedor.domicilioFiscal
        txbdomicilio.Text = consultaObtenerUnproveedor.domicilio
        txbEstado.Text = consultaObtenerUnproveedor.estado
        txbGiro.Text = consultaObtenerUnproveedor.giro
        txbRfc.Text = consultaObtenerUnproveedor.rfc
    End Sub
#End Region

#Region "funciones"
    Private Function validarProveedor() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If txbNombre.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "nombre"))
            End If
            If txbCiudad.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "cuidad"))
            End If
            If txbdomicilio.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "domicilio"))
            End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function

    Private Function validartelefono() As respuestaDelProceso

        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If txblargaDistancia.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "larga distancia"))
            End If
            If txbNumero.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "número"))
            End If

            If txbTipo.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "tipo de número"))
            End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
#End Region




End Class
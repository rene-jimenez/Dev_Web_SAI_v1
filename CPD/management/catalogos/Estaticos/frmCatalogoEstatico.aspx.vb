Imports CES.nspPopup
Imports CES.nspResponsable, CES.nspCategoria, CES.nspIva, CES.nspDocumentoContable, CES.nspUnidadMedida, CES.nspEstatusOficio, CES.nspTipoPago
Imports CRN.nspResponsable, CRN.nspCategoria, CRN.nspIva, CRN.nspDocumentoContable, CRN.nspUnidadMedida, CRN.nspEstatusOficio, CRN.nspTipoPago
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmCatalogoEstatico : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                btnGuardar.Visible = True
                btnActualizar.Visible = False
                divLateral.Visible = False
                lblTitulo.Text = "Agregar"
                limpiarControles()
                poblarCatalogo()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try

            Dim mensaje = validarCampos()
            If mensaje.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(mensaje.comentario)
            End If
            Dim accionCatalogo As String = Request.QueryString("tipoCte")
            Select Case accionCatalogo
                Case "Responsable"
                    Dim repetido = New Proceso_ObtenerResponsables() With {.tipoConsulta = tipoConsultaResponsable.todos}.Ejecutar()
                    For i = 0 To repetido.Count - 1
                        If repetido(i).nombre = txbNombre.Text Then
                            OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Naranja, False, "")
                            Exit Sub
                        End If
                    Next
                    Dim catalogo As New responsable()
                    catalogo.id = Guid.NewGuid()
                    catalogo.nombre = txbNombre.Text
                    catalogo.ipUsuario = direccionIP
                    catalogo.idUsuarioMovimiento = IdUsuario
                    Dim respuesta = New Proceso_AgregarResponsable() With {.entidad = catalogo}.Ejecutar
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Responsable"), tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=Responsable")
                        Case tipoRespuestaDelProceso.Advertencia
                            Throw New Exception(respuesta.comentario)
                        Case tipoRespuestaDelProceso.NoCompletado
                            Throw New Exception(respuesta.comentario)
                    End Select
                Case "Categoria"
                    Dim repetido = New Proceso_ObtenerCategorias() With {.tipoConsulta = tipoConsultaCategoria.todos}.Ejecutar()
                    For i = 0 To repetido.Count - 1
                        If repetido(i).nombre = txbNombre.Text Then
                            OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Naranja, False, "")
                            Exit Sub
                        End If
                    Next
                    Dim catalogo As New categoria()
                    catalogo.id = Guid.NewGuid()
                    catalogo.nombre = txbNombre.Text
                    catalogo.ipUsuario = direccionIP
                    catalogo.idUsuarioMovimiento = IdUsuario
                    Dim respuesta = New Proceso_AgregarCategoria() With {.entidad = catalogo}.Ejecutar

                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Categoría"), tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=Categoria")
                        Case tipoRespuestaDelProceso.Advertencia
                            Throw New Exception(respuesta.comentario)
                        Case tipoRespuestaDelProceso.NoCompletado
                            Throw New Exception(respuesta.comentario)
                    End Select
                Case "DocumentoContable"
                    Dim repetido = New Proceso_ObtenerDocumentosContables() With {.tipoConsulta = tipoConsultaDocumentoContable.todos}.Ejecutar()
                    For i = 0 To repetido.Count - 1
                        If repetido(i).nombre = txbNombre.Text Then
                            OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Naranja, False, "")
                            Exit Sub
                        End If
                    Next
                    Dim catalogo As New documentoContable()
                    catalogo.id = Guid.NewGuid()
                    catalogo.nombre = txbNombre.Text
                    catalogo.ipUsuario = direccionIP
                    catalogo.idUsuarioMovimiento = IdUsuario
                    Dim respuesta = New Proceso_AgregarDocumentoContable() With {.entidad = catalogo}.Ejecutar
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Documento Contable"), tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=DocumentoContable")
                        Case tipoRespuestaDelProceso.Advertencia
                            Throw New Exception(respuesta.comentario)
                        Case tipoRespuestaDelProceso.NoCompletado
                            Throw New Exception(respuesta.comentario)
                    End Select
                Case "UnidadMedida"
                    Dim repetido = New Proceso_ObtenerUnidadesMedida() With {.tipoConsulta = tipoConsultaUnidadMedida.todos}.Ejecutar()
                    For i = 0 To repetido.Count - 1
                        If repetido(i).nombre = txbNombre.Text Then
                            OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Naranja, False, "")
                            Exit Sub
                        End If
                    Next
                    Dim catalogo As New unidadMedida()
                    catalogo.id = Guid.NewGuid()
                    catalogo.nombre = txbNombre.Text
                    catalogo.ipUsuario = direccionIP
                    catalogo.idUsuarioMovimiento = IdUsuario
                    Dim respuesta = New Proceso_AgregarUnidadMedida() With {.entidad = catalogo}.Ejecutar
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Unidad Medida"), tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=UnidadMedida")
                        Case tipoRespuestaDelProceso.Advertencia
                            Throw New Exception(respuesta.comentario)
                        Case tipoRespuestaDelProceso.NoCompletado
                            Throw New Exception(respuesta.comentario)
                    End Select
                Case "TipoPago"
                    Dim repetido = New Proceso_ObtenerTiposPagos() With {.tipoConsulta = tipoConsultaTipoPago.todos}.Ejecutar()
                    For i = 0 To repetido.Count - 1
                        If repetido(i).nombre = txbNombre.Text Then
                            OnMostrarMensajeAccion("Atención", "EEl dato estaría duplicado", tipoPopup.Naranja, False, "")
                            Exit Sub
                        End If
                    Next
                    Dim catalogo As New tipoPago()
                    catalogo.id = Guid.NewGuid()
                    catalogo.nombre = txbNombre.Text
                    catalogo.ipUsuario = direccionIP
                    catalogo.idUsuarioMovimiento = IdUsuario
                    Dim respuesta = New Proceso_AgregarTipoPago() With {.entidad = catalogo}.Ejecutar
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Tipo Pago"), tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=TipoPago")
                        Case tipoRespuestaDelProceso.Advertencia
                            Throw New Exception(respuesta.comentario)
                        Case tipoRespuestaDelProceso.NoCompletado
                            Throw New Exception(respuesta.comentario)
                    End Select
                Case "EstatusOficio"
                    Dim repetido = New Proceso_ObtenerEstatusOficios() With {.tipoConsulta = tipoConsultaEstatusOficio.todos}.Ejecutar()
                    For i = 0 To repetido.Count - 1
                        If repetido(i).nombre = txbNombre.Text Then
                            OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Naranja, False, "")
                            Exit Sub
                        End If
                    Next
                    Dim catalogo As New estatusOficio()
                    catalogo.id = Guid.NewGuid()
                    catalogo.nombre = txbNombre.Text
                    catalogo.ipUsuario = direccionIP
                    catalogo.idUsuarioMovimiento = IdUsuario
                    Dim respuesta = New Proceso_AgregarEstatusOficio() With {.entidad = catalogo}.Ejecutar
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Estatus Oficio"), tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=EstatusOficio")
                        Case tipoRespuestaDelProceso.Advertencia
                            Throw New Exception(respuesta.comentario)
                        Case tipoRespuestaDelProceso.NoCompletado
                            Throw New Exception(respuesta.comentario)
                    End Select
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try

    End Sub
    Protected Sub btnEditar_OnClick(sender As Object, e As EventArgs)
        Try
            divLateral.Visible = True
            updateAregar.Update()
            btnActualizar.Visible = True
            btnGuardar.Visible = False
            Dim btnEditar As LinkButton = sender
            Dim id As Guid = Guid.Parse(btnEditar.CommandArgument)
            Dim ide = id.ToString

            Dim accionCatalogo As String = Request.QueryString("tipoCte")
            Select Case accionCatalogo
                Case "Responsable"
                    Dim editar = New Proceso_ObtenerResponsable() With {.id = Guid.Parse(ide)}.Ejecutar()
                    lblTitulo.Text = "Actualizar Responsable"
                    lblHiddenId.Value = ide
                    limpiarControles()
                    txbNombre.Text = editar.nombre
                Case "Categoria"
                    Dim editar = New Proceso_ObtenerCategoria() With {.id = Guid.Parse(ide)}.Ejecutar()
                    lblTitulo.Text = "Actualizar Categoría"
                    lblHiddenId.Value = ide
                    limpiarControles()
                    txbNombre.Text = editar.nombre
                Case "DocumentoContable"
                    Dim editar = New Proceso_ObtenerDocumentoContable() With {.id = Guid.Parse(ide)}.Ejecutar()
                    lblTitulo.Text = "Actualizar Documento Contable"
                    lblHiddenId.Value = ide
                    limpiarControles()
                    txbNombre.Text = editar.nombre
                Case "UnidadMedida"
                    Dim editar = New Proceso_ObtenerUnidadMedida() With {.id = Guid.Parse(ide)}.Ejecutar()
                    lblTitulo.Text = "Actualizar Unidad de Medida"
                    lblHiddenId.Value = ide
                    limpiarControles()
                    txbNombre.Text = editar.nombre
                Case "TipoPago"
                    Dim editar = New Proceso_ObtenerTipoPago() With {.id = Guid.Parse(ide)}.Ejecutar()
                    lblTitulo.Text = "Actualizar Tipo de Pago"
                    lblHiddenId.Value = ide
                    limpiarControles()
                    txbNombre.Text = editar.nombre
                Case "EstatusOficio"
                    Dim editar = New Proceso_ObtenerEstatusOficio() With {.id = Guid.Parse(ide)}.Ejecutar()
                    lblTitulo.Text = "Actualizar Estatus Oficio"
                    lblHiddenId.Value = ide
                    limpiarControles()
                    txbNombre.Text = editar.nombre
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try


    End Sub
    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            Dim id = lblHiddenId.Value
            Dim accionCatalogo As String = Request.QueryString("tipoCte")
            Select Case accionCatalogo
                Case "Responsable"
                    Dim editar = New Proceso_ObtenerResponsable() With {.id = Guid.Parse(id)}.Ejecutar()
                    Dim repetido = New Proceso_ObtenerResponsables() With {.tipoConsulta = tipoConsultaResponsable.todos}.Ejecutar()

                    If repetido.Where(Function(rep) rep.nombre = txbNombre.Text And rep.id <> Guid.Parse(id)).Count > 1 Then
                        OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Naranja, False, "")
                        Exit Sub
                    End If

                    editar.nombre = txbNombre.Text.Trim()
                    editar.idUsuarioMovimiento = IdUsuario
                    editar.ipUsuario = direccionIP
                    poblarCatalogo()
                    limpiarControles()
                    Dim respuesta = New Proceso_ActualizarResponsable() With {.entidad = editar}.Ejecutar()
                    limpiarControles()
                    poblarCatalogo()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", "El catálogo se editó correctamente.", tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=Responsable")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "Categoria"
                    Dim editar = New Proceso_ObtenerCategoria() With {.id = Guid.Parse(id)}.Ejecutar()
                    Dim repetido = New Proceso_ObtenerCategorias() With {.tipoConsulta = tipoConsultaCategoria.todos}.Ejecutar()
                    If repetido.Where(Function(rep) rep.nombre = txbNombre.Text And rep.id <> Guid.Parse(id)).Count > 1 Then
                        OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Naranja, False, "")
                        Exit Sub
                    End If

                    editar.nombre = txbNombre.Text.Trim()
                    editar.idUsuarioMovimiento = IdUsuario
                    editar.ipUsuario = direccionIP
                    poblarCatalogo()
                    Dim respuesta = New Proceso_ActualizarCategoria() With {.entidad = editar}.Ejecutar()
                    limpiarControles()
                    poblarCatalogo()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", "El catálogo se editó correctamente.", tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=Categoria")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "DocumentoContable"
                    Dim editar = New Proceso_ObtenerDocumentoContable() With {.id = Guid.Parse(id)}.Ejecutar()
                    Dim repetido = New Proceso_ObtenerDocumentosContables() With {.tipoConsulta = tipoConsultaDocumentoContable.todos}.Ejecutar()
                    If repetido.Where(Function(rep) rep.nombre = txbNombre.Text And rep.id <> Guid.Parse(id)).Count > 1 Then
                        OnMostrarMensajeAccion("Documento Contable", "El dato estaría duplicado", tipoPopup.Rojo, False, "")
                        Exit Sub
                    End If
                    editar.nombre = txbNombre.Text.Trim()
                    editar.idUsuarioMovimiento = IdUsuario
                    editar.ipUsuario = direccionIP
                    poblarCatalogo()
                    Dim respuesta = New Proceso_ActualizarDocumentoContable() With {.entidad = editar}.Ejecutar()
                    limpiarControles()
                    poblarCatalogo()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", "El catálogo se editó correctamente.", tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=DocumentoContable")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "UnidadMedida"
                    Dim editar = New Proceso_ObtenerUnidadMedida() With {.id = Guid.Parse(id)}.Ejecutar()
                    Dim repetido = New Proceso_ObtenerUnidadesMedida() With {.tipoConsulta = tipoConsultaUnidadMedida.todos}.Ejecutar()
                    If repetido.Where(Function(rep) rep.nombre = txbNombre.Text And rep.id <> Guid.Parse(id)).Count > 1 Then
                        OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Naranja, False, "")
                        Exit Sub
                    End If
                    editar.nombre = txbNombre.Text.Trim()
                    editar.idUsuarioMovimiento = IdUsuario
                    editar.ipUsuario = direccionIP
                    poblarCatalogo()
                    Dim respuesta = New Proceso_ActualizarUnidadMedida() With {.entidad = editar}.Ejecutar()
                    limpiarControles()
                    poblarCatalogo()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", "El catálogo se editó correctamente.", tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=UnidadMedida")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "TipoPago"
                    Dim editar = New Proceso_ObtenerTipoPago() With {.id = Guid.Parse(id)}.Ejecutar()
                    Dim repetido = New Proceso_ObtenerTiposPagos() With {.tipoConsulta = tipoConsultaTipoPago.todos}.Ejecutar()
                    If repetido.Where(Function(rep) rep.nombre = txbNombre.Text And rep.id <> Guid.Parse(id)).Count > 1 Then
                        OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Naranja, False, "")
                        Exit Sub
                    End If
                    editar.nombre = txbNombre.Text.Trim()
                    editar.idUsuarioMovimiento = IdUsuario
                    editar.ipUsuario = direccionIP
                    Dim respuesta = New Proceso_ActualizarTipoPago() With {.entidad = editar}.Ejecutar()
                    limpiarControles()
                    poblarCatalogo()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", "El catálogo se editó correctamente.", tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=TipoPago")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "EstatusOficio"
                    Dim editar = New Proceso_ObtenerEstatusOficio() With {.id = Guid.Parse(id)}.Ejecutar()
                    Dim repetido = New Proceso_ObtenerEstatusOficios() With {.tipoConsulta = tipoConsultaEstatusOficio.todos}.Ejecutar()
                    If repetido.Where(Function(rep) rep.nombre = txbNombre.Text And rep.id <> Guid.Parse(id)).Count > 1 Then
                        OnMostrarMensajeAccion("Atención", "El dato estaría duplicado", tipoPopup.Rojo, False, "")
                        Exit Sub
                    End If
                    editar.nombre = txbNombre.Text.Trim()
                    editar.idUsuarioMovimiento = IdUsuario
                    editar.ipUsuario = direccionIP
                    poblarCatalogo()
                    Dim respuesta = New Proceso_ActualizarEstatusOficio() With {.entidad = editar}.Ejecutar()
                    limpiarControles()
                    poblarCatalogo()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            OnMostrarMensajeAccion("Completado", "El catálogo se editó correctamente.", tipoPopup.Verde, True, "management/catalogos/Estaticos/frmCatalogoEstatico.aspx?tipoCte=EstatusOficio")
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try

    End Sub
    Protected Sub btnDesactivar_OnClick(sender As Object, e As EventArgs)
        Try
            Me.cardlista.Attributes.Remove("class")
            Dim btn As LinkButton = sender
            Dim indice As Integer = btn.TabIndex
            Dim id As Guid = Guid.Parse(btn.CommandArgument)
            Dim lblEstatus As Label = CType(lsvCatalogos.Items(btn.TabIndex).FindControl("lblEstatus"), Label)
            Dim bandera As Boolean = False
            If lblEstatus.Text = True Then
                bandera = False

                cardlista.Attributes.Add("class", "card animated pulse")
            Else
                bandera = True
                cardlista.Attributes.Add("class", "card animated shake")
            End If
            Dim accionCatalogo As String = Request.QueryString("tipoCte")
            Select Case accionCatalogo
                Case "Responsable"
                    Dim catalogo = New Proceso_ObtenerResponsable() With {.id = (id)}.Ejecutar
                    catalogo.esActivo = bandera
                    catalogo.idUsuarioMovimiento = IdUsuario
                    catalogo.ipUsuario = direccionIP

                    Dim respuesta = New Proceso_DesactivarResponsable With {.entidad = catalogo}.Ejecutar()

                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            If bandera = True Then
                                OnMostrarMensajeAccion("Completado", "El responsable: " + catalogo.nombre + " se activó correctamente.", tipoPopup.Verde, False, "")
                            Else
                                OnMostrarMensajeAccion("Completado", "El responsable: " + catalogo.nombre + " se desactivó correctamente.", tipoPopup.Verde, False, "")
                            End If
                            poblarCatalogo()
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "Categoria"
                    Dim catalogo = New Proceso_ObtenerCategoria() With {.id = (id)}.Ejecutar

                    catalogo.esActivo = bandera
                    catalogo.idUsuarioMovimiento = IdUsuario
                    catalogo.ipUsuario = direccionIP
                    Dim respuesta = New Proceso_DesactivarCategoria With {.entidad = catalogo}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            If bandera = True Then
                                OnMostrarMensajeAccion("Completado", "La categotía: " + catalogo.nombre + " se activó correctamente.", tipoPopup.Verde, False, "")
                            Else
                                OnMostrarMensajeAccion("Completado", "La categotía: " + catalogo.nombre + " se desactivó correctamente.", tipoPopup.Verde, False, "")
                            End If
                            poblarCatalogo()
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "DocumentoContable"
                    Dim catalogo = New Proceso_ObtenerDocumentoContable() With {.id = (id)}.Ejecutar
                    catalogo.esActivo = bandera
                    catalogo.idUsuarioMovimiento = IdUsuario
                    catalogo.ipUsuario = direccionIP
                    Dim respuesta = New Proceso_DesactivarDocumentoContable With {.entidad = catalogo}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            If bandera = True Then
                                OnMostrarMensajeAccion("Completado", "El documentoto: " + catalogo.nombre + " se activó correctamente.", tipoPopup.Verde, False, "")
                            Else
                                OnMostrarMensajeAccion("Completado", "El documento: " + catalogo.nombre + " se desactivó correctamente.", tipoPopup.Verde, False, "")
                            End If
                            poblarCatalogo()
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "UnidadMedida"
                    Dim catalogo = New Proceso_ObtenerUnidadMedida() With {.id = (id)}.Ejecutar
                    catalogo.esActivo = bandera
                    catalogo.idUsuarioMovimiento = IdUsuario
                    catalogo.ipUsuario = direccionIP
                    Dim respuesta = New Proceso_DesactivarUnidadMedida With {.entidad = catalogo}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            If bandera = True Then
                                OnMostrarMensajeAccion("Completado", "La unidad: " + catalogo.nombre + " se activó correctamente.", tipoPopup.Verde, False, "")
                            Else
                                OnMostrarMensajeAccion("Completado", "La unidad: " + catalogo.nombre + " se desactivó correctamente.", tipoPopup.Verde, False, "")
                            End If
                            poblarCatalogo()
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select

                Case "TipoPago"
                    Dim catalogo = New Proceso_ObtenerTipoPago() With {.id = (id)}.Ejecutar
                    catalogo.esActivo = bandera
                    catalogo.idUsuarioMovimiento = IdUsuario
                    catalogo.ipUsuario = direccionIP
                    Dim respuesta = New Proceso_DesactivarTipoPago With {.entidad = catalogo}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            If bandera = True Then
                                OnMostrarMensajeAccion("Completado", "El pago: " + catalogo.nombre + " se activó correctamente.", tipoPopup.Verde, False, "")
                            Else
                                OnMostrarMensajeAccion("Completado", "El pago: " + catalogo.nombre + " se desactivó correctamente.", tipoPopup.Verde, False, "")
                            End If
                            poblarCatalogo()
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select
                Case "EstatusOficio"
                    Dim catalogo = New Proceso_ObtenerEstatusOficio() With {.id = (id)}.Ejecutar
                    catalogo.esActivo = bandera
                    catalogo.idUsuarioMovimiento = IdUsuario
                    catalogo.ipUsuario = direccionIP
                    Dim respuesta = New Proceso_DesactivarEstatusOficio With {.entidad = catalogo}.Ejecutar()
                    Select Case respuesta.respuesta
                        Case tipoRespuestaDelProceso.Completado
                            If bandera = True Then
                                OnMostrarMensajeAccion("Completado", "El oficio: " + catalogo.nombre + " se activó correctamente.", tipoPopup.Verde, False, "")
                            Else
                                OnMostrarMensajeAccion("Completado", "El oficio: " + catalogo.nombre + " se desactivó correctamente.", tipoPopup.Verde, False, "")
                            End If
                            poblarCatalogo()
                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, True, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                    End Select

            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Private Sub poblarCatalogo()
        Try
            Dim listaCatalogo As String = Request.QueryString("tipoCte")
            Select Case listaCatalogo

                Case "Responsable"
                    Dim lista = New Proceso_ObtenerResponsables() With {.tipoConsulta = tipoConsultaResponsable.todos}.Ejecutar().OrderBy(Function(o) o.nombre).ToList
                    lsvCatalogos.DataSource = lista
                    lsvCatalogos.DataBind()
                    tituloA.Text = "Responsable"
                    tituloB.Text = "Responsable"
                Case "Categoria"
                    Dim lista = New Proceso_ObtenerCategorias() With {.tipoConsulta = tipoConsultaCategoria.todos}.Ejecutar().OrderBy(Function(o) o.nombre).ToList
                    lsvCatalogos.DataSource = lista
                    lsvCatalogos.DataBind()
                    tituloA.Text = "Categoría"
                    tituloB.Text = "Categoría"
                Case "DocumentoContable"
                    Dim lista = New Proceso_ObtenerDocumentosContables() With {.tipoConsulta = tipoConsultaDocumentoContable.todos}.Ejecutar().OrderBy(Function(o) o.nombre).ToList
                    lsvCatalogos.DataSource = lista
                    lsvCatalogos.DataBind()
                    tituloA.Text = "Documento Contable"
                    tituloB.Text = "Documento Contable"
                Case "UnidadMedida"
                    Dim lista = New Proceso_ObtenerUnidadesMedida() With {.tipoConsulta = tipoConsultaUnidadMedida.todos}.Ejecutar().OrderBy(Function(o) o.nombre).ToList
                    lsvCatalogos.DataSource = lista
                    lsvCatalogos.DataBind()
                    tituloA.Text = "Unidad Medida"
                    tituloB.Text = "Unidad Medida"
                Case "TipoPago"
                    Dim lista = New Proceso_ObtenerTiposPagos() With {.tipoConsulta = tipoConsultaTipoPago.todos}.Ejecutar().OrderBy(Function(o) o.nombre).ToList
                    lsvCatalogos.DataSource = lista
                    lsvCatalogos.DataBind()
                    tituloA.Text = "Tipo Pago"
                    tituloB.Text = "Tipo Pago"
                Case "EstatusOficio"
                    Dim lista = New Proceso_ObtenerEstatusOficios() With {.tipoConsulta = tipoConsultaEstatusOficio.todos}.Ejecutar().OrderBy(Function(o) o.nombre).ToList
                    lsvCatalogos.DataSource = lista
                    lsvCatalogos.DataBind()
                    tituloA.Text = "Estatus Oficio"
                    tituloB.Text = "Estatus Oficio"
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try

    End Sub
    Protected Sub limpiarControles()
        txbNombre.Text = String.Empty
    End Sub
    Protected Sub llenarElegido(ByVal id As String)
        limpiarControles()
    End Sub
    Private Function validarCampos()
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbNombre.Text.ToString = "" Then
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "nombre")
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
        Else
            respuesta.respuesta = tipoRespuestaDelProceso.Completado
        End If
        Return respuesta
    End Function
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        mandaDefault()
    End Sub

    Protected Sub btnANuevo_Click(sender As Object, e As EventArgs)
        txbNombre.Text = ""
        divLateral.Visible = True
        cardlista.Attributes.Add("class", "card animated pulse")
        updateAregar.Update()
    End Sub

End Class
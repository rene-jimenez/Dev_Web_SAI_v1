Imports CRN.nspArticulo, CRN.nspPagina
Imports CES.nspArticulo, CES.nspPagina
Imports CES.nspPopup
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmHistorial : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ocultardiv()
        End If
    End Sub

#Region "funciones"
    Protected Sub ocultardiv()
        divModulo.Visible = False
        divUsuario.Visible = False
        divFecha.Visible = False
        divRangoFechas.Visible = False
        divEdicion.Visible = False
        divmoduloxFecha.Visible = False
        divmoduloxFechas.Visible = False
        divUsuarioModuloxFecha.Visible = False
        divUsuarioModuloxFechas.Visible = False
        divEdicionModuloxFecha.Visible = True
        divEdicionModuloxFechas.Visible = True

    End Sub
#End Region

#Region "combos"
    'consulta principal
    Private Sub cmbTipoHistorial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoHistorial.SelectedIndexChanged
        Dim valor As String = (cmbTipoHistorial.SelectedValue)
        lsvHistoria.Items.Clear()
        lsvHistoria.DataBind()
        updateListado.Update()


        Select Case valor
            Case "Todo"
                ocultardiv()
                lblhistorial.Text = " todo"
                divTodo.Visible = True
                'divModulo.Visible = True
                'divUsuario.Visible = False
                'divFecha.Visible = False
                'divRangoFechas.Visible = False
                'divEdicion.Visible = False
                hdfSelector.Value = "todo"
            Case "Módulo"
                llenarModulos()
                divModulo.Visible = True
                divUsuario.Visible = False
                divFecha.Visible = False
                divRangoFechas.Visible = False
                divEdicion.Visible = False
                divTodo.Visible = False
                divmoduloxFecha.Visible = False
                divmoduloxFechas.Visible = False
                hdfSelector.Value = "moduloxtodo"
                lblhistorial.Text = " modulo todo"
            Case "Usuario"
                llenarUsuarios()
                llenarModulos()
                divModulo.Visible = False
                divUsuario.Visible = True
                divFecha.Visible = False
                divRangoFechas.Visible = False
                divEdicion.Visible = False
                divTodo.Visible = False
                divUsuarioModuloxFecha.Visible = False
                divUsuarioModuloxFechas.Visible = False
                hdfSelector.Value = "usuarioxtodo"
                lblhistorial.Text = " usuario todo"
            Case "Fecha especifica"
                divModulo.Visible = False
                divUsuario.Visible = False
                divFecha.Visible = True
                divTodo.Visible = False
                divRangoFechas.Visible = False
                divEdicion.Visible = False
                hdfSelector.Value = "fecha"
                lblhistorial.Text = " fecha especifica"
            Case "Rango de fechas"
                divModulo.Visible = False
                divUsuario.Visible = False
                divFecha.Visible = False
                divTodo.Visible = False
                divRangoFechas.Visible = True
                divEdicion.Visible = False
                hdfSelector.Value = "rangofechas"
                lblhistorial.Text = " rango de fechas"
            Case "Tipo de edición"
                llenarModulos()
                divModulo.Visible = False
                divUsuario.Visible = False
                divFecha.Visible = False
                divRangoFechas.Visible = False
                divTodo.Visible = False
                divEdicion.Visible = True
                divEdicionModuloxFecha.Visible = False
                divEdicionModuloxFechas.Visible = False
                hdfSelector.Value = "operacionmodulotodo"
                lblhistorial.Text = " edicion modulo todo"
        End Select
    End Sub

    'por modulo
    Private Sub cmbporModuloporFecha_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbporModuloporFecha.SelectedIndexChanged
        Dim valor As String = (cmbporModuloporFecha.SelectedValue)
        Select Case valor
            Case "Fecha"
                divmoduloxFecha.Visible = True
                divmoduloxFechas.Visible = False
                hdfSelector.Value = "moduloxfecha"
                lblhistorial.Text = " modulo fecha"
            Case "Rango de fecha"
                divmoduloxFecha.Visible = False
                divmoduloxFechas.Visible = True
                hdfSelector.Value = "moduloxfechas"
                lblhistorial.Text = " modulo rango de fechas"
            Case Else 'todo
                divmoduloxFecha.Visible = False
                divmoduloxFechas.Visible = False
                hdfSelector.Value = "moduloxtodo"
                lblhistorial.Text = " modulo todo"
        End Select

    End Sub

    'por usuario
    Private Sub cmbusuarioModuloxFecha_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbusuarioModuloxFecha.SelectedIndexChanged
        Dim valor As String = (cmbusuarioModuloxFecha.SelectedValue)
        Select Case valor
            Case "Fecha"
                divUsuarioModuloxFecha.Visible = True
                divUsuarioModuloxFechas.Visible = False
                hdfSelector.Value = "usuariomoduloxfecha"
                lblhistorial.Text = " usuario modulo fecha"
            Case "Rango de fecha"
                divUsuarioModuloxFecha.Visible = False
                divUsuarioModuloxFechas.Visible = True
                hdfSelector.Value = "usuariomoduloxfechas"
                lblhistorial.Text = " usuario modulo rango de fechas"
            Case Else 'todo
                divUsuarioModuloxFecha.Visible = False
                divUsuarioModuloxFechas.Visible = False
                hdfSelector.Value = "usuariomodulotodo"
                lblhistorial.Text = " usuario modulo todo"
        End Select
    End Sub

    'por edicion
    Private Sub cmbEdicionModuloxFecha_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEdicionModuloxFecha.SelectedIndexChanged
        Dim valor As String = (cmbEdicionModuloxFecha.SelectedValue)
        Select Case valor
            Case "Fecha"
                divEdicionModuloxFecha.Visible = True
                divEdicionModuloxFechas.Visible = False
                hdfSelector.Value = "operacionmoduloxfecha"
                lblhistorial.Text = " operacion modulo fecha"
            Case "Rango de fecha"
                divEdicionModuloxFecha.Visible = False
                divEdicionModuloxFechas.Visible = True
                hdfSelector.Value = "operacionmoduloxfechas"
                lblhistorial.Text = " operacion modulo rango de fechas"
            Case Else 'todo
                divEdicionModuloxFecha.Visible = False
                divEdicionModuloxFechas.Visible = False
                hdfSelector.Value = "operacionmodulotodo"
                lblhistorial.Text = " operacion modulo todo"
        End Select
    End Sub

#End Region

#Region "llenarCombo"
    Protected Sub llenarUsuarios()
        Dim consulta = New CRN.nspUsuarios.Proceso_ObtenerUsuarios().Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbporUsuarioPorUsuario.DataValueField = "id"
        cmbporUsuarioPorUsuario.DataTextField = "nombre"
        cmbporUsuarioPorUsuario.DataSource = consulta.OrderBy(Function(a) a.nombre).ToList
        cmbporUsuarioPorUsuario.DataBind()
        cmbporUsuarioPorUsuario.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub llenarModulos()
        Dim consulta = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.moduloTodos, .idSistema = sistemaActivo.idSistema}.Ejecutar()
        Dim consultaedicion = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.edicionTodos, .idSistema = sistemaActivo.idSistema}.Ejecutar()
        cmbporUsuarioModulo.DataValueField = "modulo"
        cmbporUsuarioModulo.DataTextField = "modulo"
        cmbporUsuarioModulo.DataSource = consulta
        cmbporUsuarioModulo.DataBind()
        cmbEdicionModulo.SelectedValue = "Seleccione un elemento de la lista"
        cmbEdicionModulo.DataValueField = "modulo"
        cmbEdicionModulo.DataTextField = "modulo"
        cmbEdicionModulo.DataSource = consulta
        cmbEdicionModulo.DataBind()
        cmbModuloTipoModulo.SelectedValue = "Seleccione un elemento de la lista"
        cmbModuloTipoModulo.DataValueField = "modulo"
        cmbModuloTipoModulo.DataTextField = "modulo"
        cmbModuloTipoModulo.DataSource = consulta
        cmbModuloTipoModulo.DataBind()
        cmbModuloTipoModulo.SelectedValue = "Seleccione un elemento de la lista"
        cmbPorTipoEdicion.DataValueField = "operacion"
        cmbPorTipoEdicion.DataTextField = "operacion"
        cmbPorTipoEdicion.DataSource = consultaedicion
        cmbPorTipoEdicion.DataBind()
        cmbPorTipoEdicion.SelectedValue = "Seleccione un elemento de la lista"

    End Sub


#End Region

#Region "botones"

    Protected Sub lnkVer_Click(sender As Object, e As EventArgs)
        Dim clic As LinkButton = sender
        Dim idHistorial As Guid = Guid.Parse(clic.CommandName)
        Response.Redirect("~/management/historial/frmDetalleHistorial.aspx?idHistorial=" + idHistorial.ToString)
    End Sub
    Private Sub validaListado(lista As Integer)
        If lista = 0 Then
            lsvHistoria.Items.Clear()
            lsvHistoria.DataBind()
            updateListado.Update()
            Throw New Exception("La consulta no arrojo datos")
        End If
    End Sub
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)

            Select Case hdfSelector.Value
                Case "todo"
                    Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.todos, .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                    validaListado(listaTodo.Count)
                    lsvHistoria.DataSource = listaTodo
                    lsvHistoria.DataBind()
                    updateListado.Update()
                Case "moduloxfecha"
                    If txbporModuloFecha.Text.Length = 0 Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha")
                        Throw New Exception(respuesta.comentario)
                    Else
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.moduloXfecha, .modulo = (cmbModuloTipoModulo.SelectedValue), .fecha = CDate(txbporModuloFecha.Text), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "moduloxfechas"
                    If txbporModuloFechaInicial.Text.Length = 0 Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha inicial")
                        Throw New Exception(respuesta.comentario)
                    Else
                        If txbModuloFechaFinal.Text.Length = 0 Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha final")
                            Throw New Exception(respuesta.comentario)
                        End If
                        If (CDate(txbModuloFechaFinal.Text) < CDate(txbporModuloFechaInicial.Text)) Then
                            Throw New Exception("La fecha final no debe ser menor a la fecha inicial")
                        End If
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.moduloXfechas, .modulo = (cmbModuloTipoModulo.SelectedValue), .fechaInicial = (CDate(txbporModuloFechaInicial.Text).ToString("dd/MM/yyyy") + " 00:00:00"), .fechafinal = (CDate(txbModuloFechaFinal.Text).ToString("dd/MM/yyyy") + " 23:59:59"), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "moduloxtodo"
                    If cmbModuloTipoModulo.SelectedValue = "Seleccione un elemento de la lista" Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "módulo")
                        Throw New Exception(respuesta.comentario)
                    Else
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.modulo, .modulo = (cmbModuloTipoModulo.SelectedValue), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "usuarioxtodo"
                    If cmbporUsuarioPorUsuario.SelectedValue = "Seleccione un elemento de la lista" Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "usuario")
                        Throw New Exception(respuesta.comentario)
                    Else
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.usuario, .idUsuario = Guid.Parse(cmbporUsuarioPorUsuario.SelectedValue), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "usuariomoduloxfecha"
                    If cmbporUsuarioPorUsuario.SelectedValue = "Seleccione un elemento de la lista" Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "usuario")
                        Throw New Exception(respuesta.comentario)

                    Else
                        If cmbporUsuarioModulo.SelectedValue = "Seleccione un elemento de la lista" Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "módulo")
                            Throw New Exception(respuesta.comentario)
                        End If
                        If txbusuariomoduloxfecha.Text.Length = 0 Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha")
                            Throw New Exception(respuesta.comentario)
                        End If
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.usuarioModuloXfecha, .idUsuario = Guid.Parse(cmbporUsuarioPorUsuario.SelectedValue), .modulo = (cmbporUsuarioModulo.SelectedValue), .fecha = CDate(txbusuariomoduloxfecha.Text), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "usuariomoduloxfechas"
                    If cmbporUsuarioPorUsuario.SelectedValue = "Seleccione un elemento de la lista" Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "usuario")
                        Throw New Exception(respuesta.comentario)

                    Else
                        If cmbporUsuarioModulo.SelectedValue = "Seleccione un elemento de la lista" Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "módulo")
                            Throw New Exception(respuesta.comentario)
                        End If
                        If txbusuariomodulofInicial.Text.Length = 0 Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha inicial")
                            Throw New Exception(respuesta.comentario)
                        End If
                        If txbusuariomodulofFinal.Text.Length = 0 Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha final")
                            Throw New Exception(respuesta.comentario)
                        End If
                        If (CDate(txbusuariomodulofFinal.Text) < CDate(txbusuariomodulofInicial.Text)) Then
                            Throw New Exception("La fecha final no debe ser menor a la fecha inicial")
                        End If

                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.usuarioModuloXfechas, .idUsuario = Guid.Parse(cmbporUsuarioPorUsuario.SelectedValue), .modulo = (cmbporUsuarioModulo.SelectedValue), .fechaInicial = CDate(txbusuariomodulofInicial.Text).ToString("dd/MM/yyyy") + " 00:00:00", .fechafinal = CDate(txbusuariomodulofFinal.Text).ToString("dd/MM/yyyy") + " 23:59:59", .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "usuariomodulotodo"
                    If cmbporUsuarioModulo.SelectedValue = "Seleccione un elemento de la lista" Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "módulo")
                        Throw New Exception(respuesta.comentario)
                    Else
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.usuarioModulo, .idUsuario = Guid.Parse(cmbporUsuarioPorUsuario.SelectedValue), .modulo = (cmbporUsuarioModulo.SelectedValue), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "fecha"
                    If txbPorFecha.Text.Length = 0 Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha")
                        Throw New Exception(respuesta.comentario)
                    Else
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.fechaEspecifica, .fecha = CDate(txbPorFecha.Text), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "rangofechas"
                    If txbPorRangoFechaInicial.Text.Length = 0 Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha inicial")
                        Throw New Exception(respuesta.comentario)
                    Else
                        If txbPorRangoFechaFinal.Text.Length = 0 Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha final")
                            Throw New Exception(respuesta.comentario)
                        End If
                        If (CDate(txbPorRangoFechaFinal.Text) < CDate(txbPorRangoFechaInicial.Text)) Then
                            Throw New Exception("La fecha final no debe ser menor a la fecha inicial")
                        End If
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.rangoFecha, .fechaInicial = (CDate(txbPorRangoFechaInicial.Text).ToString("dd/MM/yyyy") + " 00:00:00"), .fechafinal = (CDate(txbPorRangoFechaFinal.Text).ToString("dd/MM/yyyy") + " 23:59:59"), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "operacionmoduloxfecha"
                    If cmbPorTipoEdicion.SelectedValue = "Seleccione un elemento de la lista" Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "edición u operación")
                        Throw New Exception(respuesta.comentario)
                    Else
                        If cmbEdicionModulo.SelectedValue = "Seleccione un elemento de la lista" Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "módulo")
                            Throw New Exception(respuesta.comentario)
                        End If

                        If txbedicionmodulofecha.Text.Length = 0 Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha")
                            Throw New Exception(respuesta.comentario)
                        End If
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.edicionXmoduloFecha, .operacion = (cmbPorTipoEdicion.SelectedValue), .modulo = (cmbEdicionModulo.SelectedValue), .fecha = CDate(txbedicionmodulofecha.Text), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "operacionmoduloxfechas"
                    If cmbPorTipoEdicion.SelectedValue = "Seleccione un elemento de la lista" Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "edición u operación")
                        Throw New Exception(respuesta.comentario)
                    Else
                        If cmbEdicionModulo.SelectedValue = "Seleccione un elemento de la lista" Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "módulo")
                            Throw New Exception(respuesta.comentario)
                        End If

                        If txbedicionmodulofechaInicial.Text.Length = 0 Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha inicial")
                            Throw New Exception(respuesta.comentario)
                        End If
                        If txbedicionmodulofechaFinal.Text.Length = 0 Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha final")
                            Throw New Exception(respuesta.comentario)
                        End If
                        If (CDate(txbedicionmodulofechaFinal.Text) < CDate(txbedicionmodulofechaInicial.Text)) Then
                            Throw New Exception("La fecha final no debe ser menor a la fecha inicial")
                        End If
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.edicionXModuloFechas, .operacion = (cmbPorTipoEdicion.SelectedValue), .modulo = (cmbEdicionModulo.SelectedValue), .fechaInicial = (CDate(txbedicionmodulofechaInicial.Text).ToString("dd/MM/yyyy") + " 00:00:00"), .fechafinal = (CDate(txbedicionmodulofechaFinal.Text).ToString("dd/MM/yyyy") + " 23:59:59"), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If
                Case "operacionmodulotodo"
                    If cmbPorTipoEdicion.SelectedValue = "Seleccione un elemento de la lista" Then
                        respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                        respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "edición u operación")
                        Throw New Exception(respuesta.comentario)
                    Else
                        If cmbEdicionModulo.SelectedValue = "Seleccione un elemento de la lista" Then
                            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "módulo")
                            Throw New Exception(respuesta.comentario)
                        End If
                        Dim listaTodo = New CRN.nspHistorial.Proceso_ObtenerHistorial() With {.tipoConsulta = CES.nspHistorial.tipoConsultaHistorial.edicionXmodulo, .operacion = (cmbPorTipoEdicion.SelectedValue), .modulo = (cmbEdicionModulo.SelectedValue), .idSistema = sistemaActivo.idSistema}.Ejecutar().OrderBy(Function(a) a.fechaOperacion).ToList
                        validaListado(listaTodo.Count)
                        lsvHistoria.DataSource = listaTodo
                        lsvHistoria.DataBind()
                        updateListado.Update()
                    End If

                Case Else
                    respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                    respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._La_consulta_no_arrojo_resultados, "historial")
                    Throw New Exception(respuesta.comentario)
                    'Throw New Exception("Se produjó un error, favor de verificar tus datos")
            End Select
            hdfSelector.Value = ""

        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub




#End Region

End Class
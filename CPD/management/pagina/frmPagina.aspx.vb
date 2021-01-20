
Imports Contexto.Notificaciones.controladorMensajes
Imports cadenero.entidades.nspUsuarioAutenticado
Imports CES.nspPopup
Public Class frmPagina : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                llenarCombo()
                Session("id") = Request.QueryString("id").ToString
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
    Private Sub btnGuardarPagina_Click(sender As Object, e As EventArgs) Handles btnGuardarPagina.Click
        Try
            Dim respuestaValidacion = validarAgregarPagina()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                'Throw New Exception(respuestaValidacion.comentario)
                OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            Dim entidadPagina = New CES.nspPagina.pagina()
            entidadPagina.id = Guid.NewGuid()
            entidadPagina.esActivo = True
            entidadPagina.nombre = txbNombre.Text
            entidadPagina.url = txbUrl.Text
            entidadPagina.jerarquia = "0"
            entidadPagina.ipUsuario = direccionIP
            entidadPagina.idUsuarioMovimiento = IdUsuario
            Dim respuesta = New CRN.nspPagina.Proceso_AgregarPagina() With {.entidad = entidadPagina}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "página"), tipoPopup.Verde, True, "management/pagina/frmPagina.aspx")
                    limpiar()
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Private Sub btnGuardarSub_Click(sender As Object, e As EventArgs) Handles btnGuardarSub.Click
        Try
            Dim respuestaValidacion = validarAgregarSubPagina()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                OnMostrarMensajeAccion("Atención", respuestaValidacion.comentario, tipoPopup.Naranja, False, "")
                Exit Sub
            End If
            Dim entidadPagina = New CES.nspPagina.pagina()
            entidadPagina.id = Guid.NewGuid()
            entidadPagina.idPaginaPadre = Guid.Parse(cmbPaginaPrincipal.SelectedValue)
            entidadPagina.esActivo = True
            entidadPagina.nombre = txbNombreSubPagina.Text
            entidadPagina.url = txbUrlSubPagina.Text
            entidadPagina.jerarquia = "1"
            entidadPagina.ipUsuario = direccionIP
            entidadPagina.idUsuarioMovimiento = IdUsuario
            Dim respuesta = New CRN.nspPagina.Proceso_AgregarPagina() With {.entidad = entidadPagina}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "sub página"), tipoPopup.Verde, True, "management/pagina/frmPagina.aspx")
                    limpiar()
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Private Sub btnCerrarPagina_Click(sender As Object, e As EventArgs) Handles btnCerrarPagina.Click
        mandaDefault()
    End Sub

    Private Sub btnCerrarSub_Click(sender As Object, e As EventArgs) Handles btnCerrarSub.Click
        mandaDefault()
    End Sub
#Region "métodos"
    Private Sub llenarCombo()
        Dim lista = New CRN.nspPagina.Proceso_ObtenerPaginas() With {.tipoConsulta = CES.nspPagina.tipoConsultaPagina.jerarquia, .jerarquia = "0"}.Ejecutar
        cmbPaginaPrincipal.DataValueField = "id"
        cmbPaginaPrincipal.DataTextField = "nombre"
        cmbPaginaPrincipal.Items.Add("Seleccione un elemento de la lista")
        cmbPaginaPrincipal.DataSource = lista.OrderBy(Function(a) a.nombre).ToList
        cmbPaginaPrincipal.DataBind()
        cmbPaginaPrincipal.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Private Sub limpiar()
        txbNombre.Text = ""
        txbNombreSubPagina.Text = ""
        txbUrl.Text = ""
        txbUrlSubPagina.Text = ""
        cmbPaginaPrincipal.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
#End Region
#Region "Funciones"
    Private Function validarAgregarPagina() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Dim mensaje As New notificacionesDeUsuario
        If txbNombre.Text.Length = 0 Then
            respuesta.comentario = "El nombre de la página es un campo obligatorio."
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            Throw New Exception(respuesta.comentario)
        End If
        If txbUrl.Text.Length = 0 Then
            respuesta.comentario = "La url de la página es un campo obligatorio."
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function
    Private Function validarAgregarSubPagina() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Dim mensaje As New notificacionesDeUsuario
        If cmbPaginaPrincipal.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.comentario = "La página principal es un campo obligatorio."
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            Throw New Exception(respuesta.comentario)
        End If
        If txbNombreSubPagina.Text.Length = 0 Then
            respuesta.comentario = "El nombre de la página es un campo obligatorio."
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            Throw New Exception(respuesta.comentario)
        End If
        If txbUrlSubPagina.Text.Length = 0 Then
            respuesta.comentario = "La url de la página es un campo obligatorio."
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            Throw New Exception(respuesta.comentario)
        End If

        Return respuesta
    End Function
#End Region


End Class
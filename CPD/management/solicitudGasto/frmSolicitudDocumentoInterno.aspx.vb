Imports CRN.nspGenerico, CES.nspPopup
Imports CES.nspPartidaPresupuestal, CRN.nspPartidaPresupuestal
Imports CES.nspArea, CRN.nspArea
Imports CES.nspSolicitudGasto, CRN.nspSolicitudGasto
Imports CES.nspFirma, CRN.nspFirma
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspGenerico
Imports Microsoft.Reporting.WebForms
Public Class frmSolicitudDocumentoInterno : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sistema As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            conDRM.Visible = False
            sinDRM.Visible = True
            poblarAreasActivas()
            txbFechaElaboracion.Text = DateTime.Now.ToString("dd/MM/yyyy")
            poblarCargosPresupuestalActivos()
            poblarPartidaPresupuestalesActivas()
        End If
        resetClases()
        Dim drm = New Proceso_ObtenerDatoGenerico() With {.tipoConsulta = CES.nspGenerico.tipoConsultaGenerico.ultimoTurnoDRM_Especial_ST, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
        Dim drmv = drm.valor
        txbFolioDocumentoInterno.Text = drm.valor.ToString
    End Sub
    Protected Sub poblarAreasActivas()
        Dim listaAreas = New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList

        cmbArea.DataValueField = "id"
        cmbArea.DataTextField = "nombre"
        cmbArea.DataSource = listaAreas.OrderBy(Function(a) a.nombre).ToList
        cmbArea.DataBind()
        cmbArea.SelectedValue = "Seleccione un elemento de la lista"

    End Sub
    Protected Sub poblarCargosPresupuestalActivos()
        Dim listaAreasAsignacion = New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbCargoPres.DataValueField = "id"
        cmbCargoPres.DataTextField = "nombre"
        cmbCargoPres.DataSource = listaAreasAsignacion.OrderBy(Function(a) a.nombre).ToList
        cmbCargoPres.DataBind()
        cmbCargoPres.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
    Protected Sub poblarPartidaPresupuestalesActivas()
        Dim consultaPartidas = New Proceso_ObtenerPartidasPresupuestales() With {.tipoConsulta = tipoConsultaPartidaPresupuestal.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbPartidaPres.DataValueField = "id"
        cmbPartidaPres.DataTextField = "nombre"
        cmbPartidaPres.DataSource = consultaPartidas.OrderBy(Function(a) a.nombre).ToList
        cmbPartidaPres.DataBind()
        cmbPartidaPres.SelectedValue = "Seleccione un elemento de la lista"
    End Sub

#Region "botones"
    Protected Sub btnDRMEditar_Click(sender As Object, e As EventArgs)
        sinDRM.Visible = False
        conDRM.Visible = True
    End Sub


    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Try
            Dim dtsReporteSolicitudGasto As New dtsReporteSolicitudGasto
            Dim respuestaValidacion = ValidarCampos()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If

            Dim turno As String
            If conDRM.Visible = False Then
                turno = txbFolioDocumentoInterno.Text
            Else
                turno = txbTurnoDRM.Text + cmbTurnoDRM.SelectedValue.ToString
            End If
            Dim resultado = New Proceso_AgregarSolicitudGastoSinDRM() With {
                                        .idPartidaPresupuestal = Guid.Parse(cmbPartidaPres.SelectedValue.ToString),
                                        .importe = Double.Parse(txbImporte.Text),
                                        .concepto = txbConcepto.Text,
                                        .marcaAgua = txbMarcaAgua.Text,
                                        .ipUsuario = direccionIP,
                                        .idSistema = sistema.sistemaActivo.id,
                                        .idUsuarioMovimiento = IdUsuario,
                                        .idArea = Guid.Parse(cmbArea.SelectedValue.ToString),
                                        .idCargoPresupuestal = Guid.Parse(cmbCargoPres.SelectedValue.ToString),
                                        .idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111115"),
                                        .turnoDRM = turno
            }.Ejecutar

            Select Case resultado.respuesta
                Case tipoRespuestaDelProceso.Completado
                    Dim autorizo = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombre, .Nombre = "Autoriza"}.Ejecutar.FirstOrDefault
                    Dim area = New Proceso_ObtenerArea() With {.id = Guid.Parse(cmbArea.SelectedValue.ToString)}.Ejecutar()
                    Dim partidaPresupuestal = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(cmbPartidaPres.SelectedValue.ToString)}.Ejecutar()
                    Dim Conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
                    Dim cadena = Conversor.dblToStrPesos(txbImporte.Text).ToString()
                    Dim imp As Decimal = txbImporte.Text
                    dtsReporteSolicitudGasto.tblReporteSolicitudGasto.AddtblReporteSolicitudGastoRow(turno, "", txbFechaElaboracion.Text.ToString, cmbArea.SelectedItem.ToString, cmbCargoPres.SelectedItem.ToString, cmbPartidaPres.SelectedItem.ToString, imp.ToString("C"), txbConcepto.Text.ToString, "", "", UCase(autorizo._nombreUsuario), cadena, area.codigo, partidaPresupuestal.numero, txbMarcaAgua.Text, "", sistema.sistemaActivo.nombre + " " + sistema.sistemaActivo.año.ToString, NombreUsuario)
                    Session("datasetReporteSolicitud") = dtsReporteSolicitudGasto
                    concepto.Attributes.Add("class", "col-sm-6")
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Solicitud de gasto"), tipoPopup.Verde, True, "/management/solicitudGasto/frmReporteGeneral.aspx")
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", resultado.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Critico", resultado.comentario, tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub
    Private Sub txbTurnoDRM_TextChanged(sender As Object, e As EventArgs) Handles txbTurnoDRM.TextChanged
        If txbTurnoDRM.Text = "" Then
            Exit Sub
        Else
            txbTurnoDRM.Text = String.Format("{0:D6}", CInt(txbTurnoDRM.Text))
            cmbTurnoDRM.Focus()
        End If
    End Sub
    Private Sub btnDRMRegresar_Click(sender As Object, e As EventArgs) Handles btnDRMRegresar.Click
        sinDRM.Visible = True
        conDRM.Visible = False
    End Sub
#End Region
#Region "funciones"
    Private Function ValidarCampos() As respuestaDelProceso

        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try

            If conDRM.Visible = True Then
                If (txbTurnoDRM.Text.Length() < 6) Then
                    resetClases()
                    txbTurnoDRM.Focus()
                    Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "turno DRM"))
                End If
                Dim drm = New Proceso_ObtenerDatoGenerico() With {.tipoConsulta = CES.nspGenerico.tipoConsultaGenerico.turnoDRM_Duplicado, .idSistema = sistema.sistemaActivo.id, .turnoDRM = txbTurnoDRM.Text + cmbTurnoDRM.SelectedValue}.Ejecutar()
                If drm.valor = 1 Then
                    Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_registro_estaria_duplicado, "turno DRM"))
                End If
            End If
            If (cmbArea.SelectedValue = "Seleccione un elemento de la lista") Then
                resetClases()
                area.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "area"))
            End If
            If (cmbCargoPres.SelectedValue = "Seleccione un elemento de la lista") Then
                resetClases()
                cargoPres.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "cargo presupuestal"))
            End If
            If (cmbPartidaPres.SelectedValue = "Seleccione un elemento de la lista") Then
                resetClases()
                partidaPres.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "partida presupuestal"))
            End If
            If (txbImporte.Text.Trim() = "") Then
                resetClases()
                importe.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "importe"))
            End If
            If IsNumeric(txbImporte.Text) = False Then
                resetClases()
                importe.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception("El campo importe debe contener solo números Ejem. 1500.00")
            End If
            If (txbMarcaAgua.Text.Trim() = "") Then
                resetClases()
                marcaAgua.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "marca de agua"))
            End If
            If (txbConcepto.Text.Trim() = "") Then
                resetClases()
                concepto.Attributes.Add("class", "col-sm-6 animated bounce")
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "concepto"))
            End If

        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
    Private Sub resetClases()
        'conDRM.Attributes.Add("class", "col-sm-6")
        area.Attributes.Add("class", "col-sm-6")
        cargoPres.Attributes.Add("class", "col-sm-6")
        partidaPres.Attributes.Add("class", "col-sm-6")
        importe.Attributes.Add("class", "col-sm-6")
        marcaAgua.Attributes.Add("class", "col-sm-6")
        concepto.Attributes.Add("class", "col-sm-6")
    End Sub
#End Region

End Class
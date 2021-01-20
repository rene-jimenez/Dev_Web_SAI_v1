Imports CES.nspSolicitudGasto, CRN.nspSolicitudGasto
Imports CES.nspOficio, CRN.nspOficio
Imports CES.nspPartidaPresupuestal, CRN.nspPartidaPresupuestal
Imports CES.nspArea, CRN.nspArea
Imports CES.nspFirma, CRN.nspFirma
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup
Imports Microsoft.Reporting.WebForms
Public Class frmSolicitudEditar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sistema As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarAreasActivas()
            poblarCargosPresupuestalActivos()
            poblarPartidaPresupuestalesActivas()
            poblarSolicitudGasto()
        End If
    End Sub
    Private Sub poblarSolicitudGasto()
        Dim idOficio = Guid.Parse(Request.QueryString("idOficio").ToString)
        Dim solicitudGasto = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = tipoConsultaSolicitudGasto.idOficio, .idOficio = idOficio}.Ejecutar().Where(Function(a) a.esCancelado = False).FirstOrDefault
        txbTurnoDRM.Text = solicitudGasto._turnoDRM
        cmbArea.SelectedValue = solicitudGasto._idArea.ToString
        txbFechaElaboracion.Text = CDate(solicitudGasto.fechaElaboracion).ToString("dd/MM/yyyy")
        cmbCargoPres.SelectedValue = solicitudGasto._idCargoPresupuestal.ToString
        cmbPartidaPres.SelectedValue = solicitudGasto.idPartidaPresupuestal.ToString
        txbImporte.Text = solicitudGasto.importe
        txbConcepto.Text = solicitudGasto.concepto
        txbMarcaAgua.Text = solicitudGasto.marcaAgua

        If solicitudGasto.folioCaja Is Nothing Then

            solicitudGasto.folioCaja = ""
        End If
        If solicitudGasto.folioTesoreria Is Nothing Then

            solicitudGasto.folioTesoreria = ""
        End If

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
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim dtsReporteSolicitudGasto As New dtsReporteSolicitudGasto
            Dim respuestaValidacion = ValidarCampos()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If

            Dim idOficio = Guid.Parse(Request.QueryString("idOficio").ToString)
            Dim solicitudGasto = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = tipoConsultaSolicitudGasto.idOficio, .idOficio = idOficio}.Ejecutar().Where(Function(a) a.esCancelado = False).FirstOrDefault
            Dim fechaRecepcion As String = ""
            If Not solicitudGasto.fechaRecepcion Is Nothing Then
                fechaRecepcion = CDate(solicitudGasto.fechaRecepcion).ToString("dd/MM/yyyy")
            End If
            Dim resultado = New Proceso_EditarSolicitudGasto() With {.id = solicitudGasto.id,
                                        .idOficio = Guid.Parse(Request.QueryString("idOficio").ToString),
                                        .idPartidaPresupuestal = Guid.Parse(cmbPartidaPres.SelectedValue.ToString),
                                        .importe = Decimal.Parse(txbImporte.Text.ToString),
                                        .concepto = txbConcepto.Text,
                                        .marcaAgua = txbMarcaAgua.Text,
                                        .ipUsuario = direccionIP,
                                        .idSistema = sistema.sistemaActivo.id,
                                        .idUsuarioMovimiento = IdUsuario,
                                        .idArea = Guid.Parse(cmbArea.SelectedValue.ToString),
                                        .idCargoPresupuestal = Guid.Parse(cmbCargoPres.SelectedValue.ToString),
                                        .idEstatusOficio = Guid.Parse("35747111-2222-3333-4444-111111111115")
            }.Ejecutar
            Select Case resultado.respuesta
                Case tipoRespuestaDelProceso.Completado
                    Dim autorizo = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombre, .Nombre = "Autoriza"}.Ejecutar.FirstOrDefault
                    Dim area = New Proceso_ObtenerArea() With {.id = Guid.Parse(cmbArea.SelectedValue.ToString)}.Ejecutar()
                    Dim partidaPresupuestal = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(cmbPartidaPres.SelectedValue.ToString)}.Ejecutar()
                    Dim Conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
                    Dim cadena = Conversor.dblToStrPesos(CDbl(txbImporte.Text)).ToString()
                    Dim imp As Decimal = txbImporte.Text
                    dtsReporteSolicitudGasto.tblReporteSolicitudGasto.AddtblReporteSolicitudGastoRow(solicitudGasto._turnoDRM, solicitudGasto._turnoSAF, CDate(txbFechaElaboracion.Text).ToString("dd/MM/yyyy"), cmbArea.SelectedItem.ToString, cmbCargoPres.SelectedItem.ToString, cmbPartidaPres.SelectedItem.ToString, imp.ToString("C"), txbConcepto.Text.ToString, solicitudGasto.folioCaja, fechaRecepcion, UCase(autorizo._nombreUsuario), cadena, area.codigo, partidaPresupuestal.numero, txbMarcaAgua.Text, solicitudGasto.folioTesoreria, sistema.sistemaActivo.nombre + " " + sistema.sistemaActivo.año.ToString, NombreUsuario)
                    Session("datasetReporteSolicitud") = dtsReporteSolicitudGasto
                    concepto.Attributes.Add("class", "col-sm-6")

                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "Solicitud de gasto"), tipoPopup.Verde, True, "/management/solicitudGasto/frmReporteGeneral.aspx")
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", resultado.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Critico", resultado.comentario, tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=SolicitudGastoEditar")
    End Sub


#End Region
#Region "funciones"
    Private Function ValidarCampos() As respuestaDelProceso

        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
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
                Throw New Exception("El campo importe debe contener solo números Ejem. 1500")
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

        area.Attributes.Add("class", "col-sm-6")
        cargoPres.Attributes.Add("class", "col-sm-6")
        partidaPres.Attributes.Add("class", "col-sm-6")
        importe.Attributes.Add("class", "col-sm-6")
        marcaAgua.Attributes.Add("class", "col-sm-6")
        concepto.Attributes.Add("class", "col-sm-6")
    End Sub

#End Region
End Class
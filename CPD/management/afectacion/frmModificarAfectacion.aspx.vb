Imports CES, CRN
Imports CRN.nspFirma, CRN.nspArea
Imports CES.nspFirma, CES.nspArea, CES.nspAfectacionPresupuestal

Imports System.Globalization
Imports Contexto.Notificaciones.controladorMensajes

Public Class frmModificarAfectacion : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try

                llenarcombos()
                llenarafectacion()
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")

            End Try
        End If

    End Sub
#Region "llenarcombos"
    Protected Sub llenarcombos()
        Dim consultaFirmasAut = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombre, .Nombre = "Autoriza"}.Ejecutar().OrderBy(Function(a) a._nombreUsuario).ToList
        cmbAutoriza.DataValueField = "id"
        cmbAutoriza.DataTextField = "_nombreUsuario"
        cmbAutoriza.DataSource = consultaFirmasAut.ToList
        cmbAutoriza.DataBind()
        cmbAutoriza.SelectedValue = "Seleccione un elemento de la lista"
        Dim consultaFirmasSol = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombre, .Nombre = "Solicita"}.Ejecutar().OrderBy(Function(a) a._nombreUsuario).ToList
        cmbSolicita.DataValueField = "id"
        cmbSolicita.DataTextField = "_nombreUsuario"
        cmbSolicita.DataSource = consultaFirmasSol.ToList
        cmbSolicita.DataBind()
        cmbSolicita.SelectedValue = "Seleccione un elemento de la lista"
    End Sub
#End Region

#Region "validar"
    Private Function validarAfectacion() As respuestaDelProceso

        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If cmbSolicita.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "Debes seleccionar quien solicita"
            Throw New Exception(respuesta.comentario)
        End If
        If cmbAutoriza.SelectedValue = "Seleccione un elemento de la lista" Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "Debes seleccionar quien autoriza la afectación"
            Throw New Exception(respuesta.comentario)
        End If
        If txbConcepto.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo fecha concepto es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        If txbMarcaAgua.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "El campo marca de agua es obligatorio"
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta

    End Function

#End Region

#Region "llenardatos"
    Private Sub llenarafectacion()
        Try
            Dim idAfectacion = Request.QueryString("idAfectacion")
            Dim Afectacion = New CRN.nspAfectacionPresupuestal.Proceso_ObtenerAfectacionPresupuestal() With {.id = Guid.Parse(idAfectacion.ToString)}.Ejecutar()



            cmbSolicita.SelectedValue = Afectacion.idSolicita.ToString
            cmbAutoriza.SelectedValue = Afectacion.idAutoriza.ToString
            txbConcepto.Text = Afectacion.concepto.ToString
            txbMarcaAgua.Text = Afectacion.marcaAgua.ToString
            txbImportePago.Text = FormatNumber(Afectacion.total.ToString, 2)
            lbldescuento.Text = "-Descuento: $ " + Afectacion.descuento.ToString("0,0.00", CultureInfo.InvariantCulture)
            lblIva.Text = "+ Iva:     $ " + Afectacion.iva.ToString("0,0.00", CultureInfo.InvariantCulture)
            lblSubtotal.Text = "Subtotal:   $ " + Afectacion.subtotal.ToString("0,0.00", CultureInfo.InvariantCulture)
            lblTotal.Text = "Total:  $ " + Afectacion.total.ToString("0,0.00", CultureInfo.InvariantCulture)
            Dim pedido = New CRN.nspPedido.Proceso_ObtenerPedido() With {.id = Guid.Parse(Afectacion.idPedido.ToString)}.Ejecutar()
            txbNumPedido.Text = pedido.numeroPedido.ToString
            txbProveedor.Text = pedido._nombreProveedor.ToString
            txbPartida.Text = pedido._nombrePartida.ToString
            Dim Oficio = New CRN.nspOficio.Proceso_ObtenerUnOficio() With {.id = Guid.Parse(pedido.idOficio.ToString)}.Ejecutar()

            If Not Oficio.turnoSAF Is Nothing Then
                txbTurnoSAF.Text = Oficio.turnoSAF.ToString
            End If
            txbAreaSolicitante.Text = Oficio._area.ToString
            txbCargoPresupuestal.Text = Oficio._cargoPresupuestal.ToString
            txbTurnoDRM_.Text = Oficio.turnoDRM.ToString
            txbTurnoSAF.Text = Oficio.turnoSAF.ToString

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")

        End Try

    End Sub



#End Region

#Region "botones"
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim resultadoValidacion = validarAfectacion()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim idAfectacion = Request.QueryString("idAfectacion")
            Dim Afectacion = New CRN.nspAfectacionPresupuestal.Proceso_ObtenerAfectacionPresupuestal() With {.id = Guid.Parse(idAfectacion.ToString)}.Ejecutar()

            Dim newAfectacion As New afectacionPresupuestal
            newAfectacion.id = Guid.Parse(idAfectacion.ToString)
            'newAfectacion.idPedido = Guid.Parse(Afectacion.idPedido.ToString)
            newAfectacion.concepto = txbConcepto.Text
            newAfectacion.idSolicita = Guid.Parse(cmbSolicita.SelectedValue)
            newAfectacion.idAutoriza = Guid.Parse(cmbAutoriza.SelectedValue)
            newAfectacion.marcaAgua = txbMarcaAgua.Text
            newAfectacion.idSistema = sistemaActivo.idSistema
            newAfectacion.ipUsuario = direccionIP
            newAfectacion.idUsuarioMovimiento = IdUsuario
            Dim respuesta = New CRN.nspAfectacionPresupuestal.Proceso_ActualizarAfectacionPresupuestal() With {.entidad = newAfectacion}.Ejecutar()
            Select Case respuesta.respuesta
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_editaron, "Afectación"), nspPopup.tipoPopup.Verde, True, "management/afectacion/reporteAfectacion/frmReporteAfectacion.aspx?idPedido=" + Afectacion.idPedido.ToString)
' aqui imprime
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")

        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub


#End Region
End Class
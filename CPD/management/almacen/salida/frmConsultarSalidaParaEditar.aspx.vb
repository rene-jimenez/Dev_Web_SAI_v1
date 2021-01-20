Imports CRN, CRN.nspDetalleSalidaAlmacen, CRN.nspSalidaAlmacen
Imports CES, CES.nspDetalleSalidaAlmacen, CES.nspSalidaAlmacen
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Net.NetworkInformation
Imports System.Globalization
Public Class frmConsultarSalidaParaEditar : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                btnEditar.Visible = False
                txbFechaInicial.Text = String.Empty
                txbFechaFinal.Text = String.Empty
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub

    Protected Sub btnEditar_Click(sender As Object, e As EventArgs)
        'Dim ert = New Proceso_ObtenerSalidaAlmacen With {.id = Guid.Parse(cmbFolio.SelectedValue)}.Ejecutar()
        Response.Redirect("~/management/almacen/salida/frmEditarSalida.aspx?id=" + cmbFolio.SelectedValue.ToString)
    End Sub

    Protected Sub txbFechaFinal_TextChanged(sender As Object, e As EventArgs)
        OP_SacaAreas()
    End Sub

    Protected Sub cmbArea_SelectedIndexChanged(sender As Object, e As EventArgs)
        OP_SacaFolios()
    End Sub

    Protected Sub cmbFolio_SelectedIndexChanged(sender As Object, e As EventArgs)
        OP_Manda()
    End Sub

    Public Sub OP_SacaAreas()
        Try
            btnEditar.Visible = False
            cmbArea.Items.Clear()
            Dim resultadoValidacion = validaFechas()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If

            Dim rango = New Proceso_ObtenerSalidasAlmacen With {.tipoConsulta = tipoConsultaSalidaAlmacen.rangoFechasObtenerAreas, .idSistema = sistemaActivo.idSistema, .fechaFinal = txbFechaFinal.Text, .fechaInicial = txbFechaInicial.Text}.Ejecutar().OrderBy(Function(r) r._codigoArea).ToList

            cmbArea.Items.Add(New ListItem("Seleccione un elemento de la lista", "0"))
            cmbArea.DataSource = rango
            cmbArea.DataTextField = "_codigoConNombreArea"
            cmbArea.DataValueField = "idArea"
            cmbArea.DataBind()
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try

    End Sub

    Public Sub OP_SacaFolios()
        Dim area As Guid
        cmbFolio.Items.Clear()
        cmbFolio.Items.Add(New ListItem("Seleccione un elemento de la lista", "0"))
        btnEditar.Visible = False
        If (cmbArea.SelectedItem.Value <> "0") Then
            area = Guid.Parse(cmbArea.SelectedValue)

            Dim folioVale = New Proceso_ObtenerSalidasAlmacen With {.tipoConsulta = tipoConsultaSalidaAlmacen.valesSalidaPoridArea, .idSistema = sistemaActivo.idSistema, .idArea = area}.Ejecutar().OrderBy(Function(t) t.numVale)

            cmbFolio.DataSource = folioVale
            cmbFolio.DataTextField = "numVale"
            cmbFolio.DataValueField = "id"
            cmbFolio.DataBind()
        End If

    End Sub

    Public Sub OP_Manda()
        Dim detalleSalida As New List(Of detalleSalidaAlmacen)
        If (cmbFolio.SelectedItem.Value <> "0") Then
            'Dim listadoSalidaAlmacen = New Proceso_ObtenerSalidasAlmacen With {.tipoConsulta = tipoConsultaSalidaAlmacen.id, .id = Guid.Parse(cmbFolio.SelectedValue)}.Ejecutar().FirstOrDefault
            Dim listadoSalidaAlmacen = New Proceso_ObtenerSalidaAlmacen With {.id = Guid.Parse(cmbFolio.SelectedValue)}.Ejecutar()
            If (Not IsNothing(listadoSalidaAlmacen)) Then
                detalleSalida = New Proceso_ObtenerDetallesSalidaAlmacen With {.tipoConsulta = tipoConsultaDetalleSalidaAlmacen.idSalida, .idSalida = listadoSalidaAlmacen.id}.Ejecutar().OrderBy(Function(l) l.fecha).ToList
            End If
        End If
        Dim Este = Guid.Parse(cmbFolio.SelectedValue)
        btnEditar.Visible = True

    End Sub

    Protected Function controladorVistas(vst As String)
        Select Case vst
            Case "vst_fechaFinal"
                btnEditar.Visible = False
                cmbArea.Items.Clear()
                cmbFolio.Items.Clear()
            Case "vst_areas"
                btnEditar.Visible = False
                cmbArea.Items.Clear()
                cmbFolio.Items.Clear()
            Case "vst_folios"
                btnEditar.Visible = True

        End Select
        Return vst
    End Function

    Public Function validaFechas() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        If txbFechaInicial.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Fecha inicial")
            Throw New Exception(respuesta.comentario)
        End If
        If CDate(txbFechaInicial.Text) > Date.Now Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha inicial no puede ser mayor a la fecha de hoy"
            Throw New Exception(respuesta.comentario)
        End If
        If CDate(txbFechaFinal.Text) < CDate(txbFechaInicial.Text) Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha final no puede ser mayor a la fecha inicial"
            Throw New Exception(respuesta.comentario)
        End If
        If CDate(txbFechaFinal.Text) > Date.Now Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "La fecha final no puede ser mayor a la fecha de hoy"
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta
    End Function

    Protected Sub go2Default_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub
End Class
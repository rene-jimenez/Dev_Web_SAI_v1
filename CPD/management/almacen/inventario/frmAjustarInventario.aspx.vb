Imports CRN.nspArticulo, CRN.nspAjusteArticuloInventario
Imports CES.nspArticulo, CES.nspAjusteArticuloInventario
Imports CES.nspPopup
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmAjustarInventario : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idArticulo = Request.QueryString("idArticulo")

                poblarDatos(idArticulo)
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "datos"
    Public Sub poblarDatos(idArticulo As String)
        Try
            Dim articulo = New Proceso_ObtenerArticulo() With {.id = Guid.Parse(idArticulo)}.Ejecutar()
            lblArticulo.Text = articulo.nombre.ToString
            txbCantidad.Text = articulo.existencia.ToString
            lblCodigoBarras.Text = articulo.codigoBarras.ToString
            lblfecha.Text = CDate(Date.Now).ToString("D", CultureInfo.CreateSpecificCulture("es-MX"))
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub
#End Region

#Region "botones"
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try

            Dim idArticulo = Request.QueryString("idArticulo")
            Dim articulo = New Proceso_ObtenerArticulo() With {.id = Guid.Parse(idArticulo)}.Ejecutar()
            Dim exist As Integer = articulo.existencia
            Dim resultadoValidacion = validarAjuste()
            If resultadoValidacion.respuesta = tipoRespuestaDelProceso.Advertencia Then
                Throw New Exception(resultadoValidacion.comentario)
            End If

            Dim nuevoAjuste As New ajusteArticuloInventario
            nuevoAjuste.id = Guid.NewGuid
            nuevoAjuste.idArticulo = articulo.id
            If rdbRestar.Checked = True Then
                nuevoAjuste.tipoOperacion = "Restar"

            End If
            If rdbSumar.Checked = True Then
                nuevoAjuste.tipoOperacion = "Sumar"
            End If
            nuevoAjuste.cantidad = txbCantidad.Text
            Dim existNew As Integer = nuevoAjuste.cantidad
            nuevoAjuste.explicacion = txbComentario.Text
            nuevoAjuste.fecha = Date.Now
            nuevoAjuste.ipUsuario = direccionIP
            nuevoAjuste.idSistema = sistemaActivo.idSistema
            nuevoAjuste.idUsuarioMovimiento = IdUsuario
            'existencia articulo < exist nueva
            If nuevoAjuste.tipoOperacion = "Restar" Then
                If exist < existNew Then
                    Throw New Exception("la cantidad en existencias es menor que la que intententas restar")
                End If
                'Else
                '    If exist > existNew Then
                '        Throw New Exception("El tipo de operación no corresponde a la cantidad que quieres restar")
                '    End If

            End If

            Dim respuesta = New Proceso_AgregarAjusteArticuloInventario() With {.entidad = nuevoAjuste}.Ejecutar()
            Select Case respuesta.respuesta
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                    If nuevoAjuste.tipoOperacion = "Restar" Then
                        articulo.ipUsuario = direccionIP
                        articulo.tipoSistema = sistemaActivo.tipo
                        articulo.idSistema = sistemaActivo.idSistema
                        articulo.idUsuarioMovimiento = IdUsuario
                        articulo.existencia = exist - existNew
                        articulo.entraAlmacen = True
                        Dim respuesta2 = New Proceso_ActualizarArticulo() With {.entidad = articulo}.Ejecutar()

                    End If
                    If nuevoAjuste.tipoOperacion = "Sumar" Then
                        'Dim ajusteArticulo As New articulo
                        articulo.ipUsuario = direccionIP
                        articulo.tipoSistema = sistemaActivo.tipo
                        articulo.idSistema = sistemaActivo.idSistema
                        articulo.idUsuarioMovimiento = IdUsuario
                        articulo.existencia = exist + existNew
                        articulo.entraAlmacen = True
                        Dim respuesta2 = New Proceso_ActualizarArticulo() With {.entidad = articulo}.Ejecutar()

                    End If

                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Ajuste inventario"), tipoPopup.Verde, True, "/management/default.aspx")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", "" & ex.Message.ToString, tipoPopup.Rojo, False, "")

        End Try
    End Sub


    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub


#End Region

#Region "validarDatos"
    Private Function validarAjuste() As respuestaDelProceso

        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)

        If txbCantidad.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "cantidad")
            Throw New Exception(respuesta.comentario)
        End If

        If txbComentario.Text.Length = 0 Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "comentario")
            Throw New Exception(respuesta.comentario)
        End If

        If rdbRestar.Checked = False And rdbSumar.Checked = False Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "Debes de seleccionar una opción de operación "
            Throw New Exception(respuesta.comentario)
        End If
        If rdbRestar.Checked = True And rdbSumar.Checked = True Then
            respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
            respuesta.comentario = "Solo debes de seleccionar una opción de operación "
            Throw New Exception(respuesta.comentario)
        End If
        Return respuesta

    End Function

    Private Sub rdbRestar_CheckedChanged(sender As Object, e As EventArgs) Handles rdbRestar.CheckedChanged
        If rdbRestar.Checked = True Then
            rdbSumar.Checked = False
        End If
    End Sub

    Private Sub rdbSumar_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSumar.CheckedChanged
        If rdbSumar.Checked = True Then
            rdbRestar.Checked = False
        End If
    End Sub


#End Region



End Class
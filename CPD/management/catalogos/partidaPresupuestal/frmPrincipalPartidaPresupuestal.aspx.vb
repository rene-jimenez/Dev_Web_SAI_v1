Imports CES
Imports CES.nspPopup
Imports CRN.nspPartidaPresupuestal
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Public Class frmPrincipalPartidaPresupuestal : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                btnGuardar.Visible = True
                btnActualizar.Visible = False
                lblTitleCardNuevo.Text = "Crea una partida presupuestal"
                cardForm.Attributes.Remove("class")
                cardForm.Attributes.Add("class", "card")
                limpiarControles()
                llenarListado()
            Catch ex As Exception
            End Try
        End If
    End Sub

#Region "metodos"
    Protected Sub llenarListado()
        Dim lista = New Proceso_ObtenerPartidasPresupuestales() With {.tipoConsulta = nspPartidaPresupuestal.tipoConsultaPartidaPresupuestal.todos}.Ejecutar().OrderBy(Function(o) o.nombre).ToList
        lvListaPartida.DataSource = lista.ToList
        lvListaPartida.DataBind()
    End Sub
    Protected Sub limpiarControles()
        txbNombre.Text = String.Empty
        txbNumero.Text = String.Empty
    End Sub

    Protected Sub guardarNuevo()
        Try
            Dim respuestaValidacion = ValidarControles()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If
            Dim mc = getMacAddress()
            Dim respuesta = New Proceso_AgregarPartidaPresupuestal() With {.entidad = New nspPartidaPresupuestal.partidaPresupuestal() With {
                .id = Guid.NewGuid,
                .nombre = txbNombre.Text.Trim(),
                .numero = txbNumero.Text.Trim(),
                .ipUsuario = mc,
                .idSistema = sisActivo.sistemaActivo.id,
            .idUsuarioMovimiento = IdUsuario
                }}.Ejecutar()
            cardForm.Attributes.Remove("class")
            cardForm.Attributes.Add("class", "card animated pulse")
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "partida presupuestal"), nspPopup.tipoPopup.Verde, False, "")
                    limpiarControles()
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, nspPopup.tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, nspPopup.tipoPopup.Rojo, False, "")
            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Private Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function

    Private Function ValidarControles() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If (txbNombre.Text.Trim() = "") Then
                Throw New Exception("El campo de nombre es obligatorio.")
            End If
            If (txbNumero.Text.Trim() = "") Then
                Throw New Exception("El campo de número partida es obligatorio.")
            End If
            If (txbNumero.Text.Length() < 4) Then
                Throw New Exception("El campo debe tener exactamente 4 digitos.")
            End If

        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
#End Region
#Region "Botones"
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiarControles()
        mandaDefault()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        guardarNuevo()
        llenarListado()
    End Sub
    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            Dim id = lblHiddenId.Value
            Dim editar = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(id)}.Ejecutar()
            editar.nombre = txbNombre.Text.Trim()
            editar.numero = txbNumero.Text.Trim()
            editar.idSistema = sisActivo.sistemaActivo.id
            editar.idUsuarioMovimiento = IdUsuario
            editar.ipUsuario = direccionIP
            Dim respuesta = New Proceso_ActualizarPartidaPresupuestal() With {.entidad = editar}.Ejecutar()
            limpiarControles()
            llenarListado()
            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "partida presupuestal"), nspPopup.tipoPopup.Verde, True, "management/catalogos/partidaPresupuestal/frmPrincipalpartidaPresupuestal.aspx")
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Protected Sub btnActivoEditar_Click(sender As Object, e As EventArgs)
        Try
            btnActualizar.Visible = True
            btnGuardar.Visible = False
            Dim btnEd As LinkButton = sender
            Dim id As Guid = Guid.Parse(btnEd.CommandArgument)
            Dim ide = id.ToString
            Dim elegido = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(ide)}.Ejecutar()
            lblTitleCardNuevo.Text = "Actualizando " + " a " + elegido.nombre + " ..."
            lblHiddenId.Value = ide
            limpiarControles()
            txbNombre.Text = elegido.nombre
            txbNumero.Text = elegido.numero
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Protected Sub btnActivoDesactivar_Click(sender As Object, e As EventArgs)
        Try
            Dim btnAD As LinkButton = sender
            Dim id As Guid = Guid.Parse(btnAD.CommandArgument)
            Dim ide = id.ToString
            lblHiddenId.Value = ide
            Dim idq = lblHiddenId.Value
            Dim editar = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(ide)}.Ejecutar()
            editar.id = id
            editar.esActivo = False
            Dim respuesta = New Proceso_DesactivarPartidaPresupuestal() With {.entidad = editar}.Ejecutar()
            limpiarControles()
            llenarListado()
            cardlista.Attributes.Remove("class")
            cardlista.Attributes.Add("class", "card animated pulse")
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Protected Sub btnDesactivoActivar_Click(sender As Object, e As EventArgs)
        Try
            Dim btnDA As LinkButton = sender
            Dim id As Guid = Guid.Parse(btnDA.CommandArgument)
            Dim ide = id.ToString
            lblHiddenId.Value = ide
            Dim idq = lblHiddenId.Value
            Dim editar = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(ide)}.Ejecutar()
            editar.id = id
            editar.esActivo = True
            Dim respuesta = New Proceso_DesactivarPartidaPresupuestal() With {.entidad = editar}.Ejecutar()
            limpiarControles()
            llenarListado()
            cardlista.Attributes.Remove("class")
            cardlista.Attributes.Add("class", "card animated shake")
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
#End Region
End Class
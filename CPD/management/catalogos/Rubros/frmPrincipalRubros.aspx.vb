Imports CES
Imports CES.nspPopup
Imports CRN.nspRubroRequerimiento
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmPrincipalRubros : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                btnGuardar.Visible = True
                btnActualizar.Visible = False
                lblTitleCardNuevo.Text = "Crea un rubro"
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
        Dim lista = New Proceso_ObtenerRubroRequerimientos() With {.tipoConsulta = nspRubroRequerimiento.tipoConsultaRubroRequerimiento.todos}.Ejecutar().OrderBy(Function(o) o.nombre).ToList
        lvListaRubro.DataSource = lista.ToList
        lvListaRubro.DataBind()
    End Sub
    Protected Sub limpiarControles()
        txbNombre.Text = String.Empty
        chkCotizable.Checked = False
    End Sub
    Protected Sub guardarNuevo()
        Try
            Dim respuestaValidacion = ValidarControles()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If

            Dim mc = getMacAddress()
            Dim respuesta = New Proceso_AgregarRubroRequerimiento() With {.entidad = New nspRubroRequerimiento.rubroRequerimiento() With {
                .id = Guid.NewGuid,
                .nombre = txbNombre.Text.Trim(),
                .esCotizable = chkCotizable.Checked(),
                .ipUsuario = mc,
                .idSistema = sisActivo.sistemaActivo.id,
            .idUsuarioMovimiento = IdUsuario
                }}.Ejecutar()
            cardForm.Attributes.Remove("class")
            cardForm.Attributes.Add("class", "card animated pulse")
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    llenarListado()
                    limpiarControles()
                    OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_guardaron, "rubro"), nspPopup.tipoPopup.Verde, True, "management/catalogos/rubros/frmPrincipalRubros.aspx")
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
        Catch ex As Exception
            respuesta.comentario = ex.Message.ToString
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
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
    End Sub
    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            Dim id = lblHiddenId.Value
            Dim editar = New Proceso_ObtenerRubroRequerimiento() With {.id = Guid.Parse(id)}.Ejecutar()
            editar.nombre = txbNombre.Text.Trim()
            editar.esCotizable = chkCotizable.Checked()
            editar.idSistema = sisActivo.sistemaActivo.id
            editar.idUsuarioMovimiento = IdUsuario
            editar.ipUsuario = direccionIP
            Dim respuesta = New Proceso_ActualizarRubroRequerimiento() With {.entidad = editar}.Ejecutar()
            limpiarControles()
            llenarListado()
            OnMostrarMensajeAccion("Completado", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_edito, "rubro"), nspPopup.tipoPopup.Verde, True, "management/catalogos/rubros/frmPrincipalRubros.aspx")
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Protected Sub btnActivoEditar_Click(sender As Object, e As EventArgs)
        btnActualizar.Visible = True
        btnGuardar.Visible = False
        Dim btnEd As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnEd.CommandArgument)
        Dim ide = id.ToString
        Dim elegido = New Proceso_ObtenerRubroRequerimiento() With {.id = Guid.Parse(ide)}.Ejecutar()
        lblTitleCardNuevo.Text = "Actualizando " + " a " + elegido.nombre + " ..."
        lblHiddenId.Value = ide
        limpiarControles()
        txbNombre.Text = elegido.nombre
        chkCotizable.Checked = elegido.esCotizable
    End Sub
    Protected Sub btnActivoDesactivar_Click(sender As Object, e As EventArgs)
        Dim btnAD As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnAD.CommandArgument)
        Dim ide = id.ToString
        lblHiddenId.Value = ide
        Dim idq = lblHiddenId.Value
        Dim editar = New Proceso_ObtenerRubroRequerimiento() With {.id = Guid.Parse(ide)}.Ejecutar()
        editar.id = id
        editar.esActivo = False
        Dim respuesta = New Proceso_DesactivarRubroRequerimiento() With {.entidad = editar}.Ejecutar()
        limpiarControles()
        llenarListado()
        Me.cardlista.Attributes.Remove("class")
        cardlista.Attributes.Add("class", "card animated pulse")

    End Sub
    Protected Sub btnDesactivoActivar_Click(sender As Object, e As EventArgs)
        Dim btnDA As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnDA.CommandArgument)
        Dim ide = id.ToString
        lblHiddenId.Value = ide
        Dim idq = lblHiddenId.Value
        Dim editar = New Proceso_ObtenerRubroRequerimiento() With {.id = Guid.Parse(ide)}.Ejecutar()
        editar.id = id
        editar.esActivo = True
        Dim respuesta = New Proceso_DesactivarRubroRequerimiento() With {.entidad = editar}.Ejecutar()
        limpiarControles()
        llenarListado()
        cardlista.Attributes.Remove("class")
        cardlista.Attributes.Add("class", "card animated shake")

    End Sub




#End Region
End Class
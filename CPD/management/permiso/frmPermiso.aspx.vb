Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmPermiso : Inherits nspPaginaBase.PaginaBase
    Dim mensajePopop As New notificacionesDeUsuario
    Dim bandera As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarRol()
        End If

    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            Dim lista As New List(Of cadenero.entidades.nspPermiso.permiso)
            Dim permiso As Integer = 0
            Select Case Session("bandera")
                Case "permisos"
                    For i = 0 To lsvPermiso.Items.Count - 1
                        Dim esActivo As CheckBox = CType(lsvPermiso.Items(i).FindControl("chkEsActivo"), CheckBox)
                        Dim registro As New cadenero.entidades.nspPermiso.permiso
                        Dim id As Guid = Guid.Parse(CType(lsvPermiso.Items(i).FindControl("labelLsvPermiso"), Label).ToolTip)
                        Dim espermitido As Boolean = False
                        If esActivo.Checked Then
                            espermitido = True
                            permiso = permiso + 1
                        End If
                        registro.id = Guid.NewGuid
                        registro.idRol = Guid.Parse(cmbRol.SelectedValue.ToString)
                        registro.idPagina = id
                        registro.esActivo = espermitido
                        registro.idUsuarioMovimiento = IdUsuario
                        registro.ipUsuario = direccionIP
                        lista.Add(registro)
                    Next
                Case "paginas"
                    For i = 0 To lvsPaginas.Items.Count - 1
                        Dim registro As New cadenero.entidades.nspPermiso.permiso
                        Dim id As Guid = Guid.Parse(CType(lvsPaginas.Items(i).FindControl("labelLsvPermiso"), Label).ToolTip)
                        Dim espermitido As Boolean = False
                        If CType(lvsPaginas.Items(i).FindControl("chkEsActivo"), CheckBox).Checked Then
                            espermitido = True
                            permiso = permiso + 1
                        End If
                        registro.id = Guid.NewGuid
                        registro.idRol = Guid.Parse(cmbRol.SelectedValue.ToString)
                        registro.idPagina = id
                        registro.esActivo = espermitido
                        registro.idUsuarioMovimiento = IdUsuario
                        registro.ipUsuario = direccionIP
                        lista.Add(registro)
                    Next
            End Select
            If permiso > 0 Then
                Dim respuesta = New cadenero.CRN.nspPermiso.Proceso_AgregarPermiso() With {.listaEntidad = lista}.Ejecutar
                Select Case respuesta.respuesta
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Completado
                        cmbRol.SelectedValue = "Selecciona un elemento de la lista"
                        OnMostrarMensajeAccion("Confirmación", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "permiso"), tipoPopup.Verde, True, "management/permiso/frmPermiso.aspx")
                        divPaginas.Visible = False
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.Advertencia
                        OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                    Case Contexto.Notificaciones.controladorMensajes.tipoRespuestaDelProceso.NoCompletado
                        divPaginas.Visible = False
                        OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
                End Select
            Else
                Dim respuesta = New cadenero.CRN.nspPermiso.Proceso_AgregarPermiso() With {.listaEntidad = lista}.Ejecutar
                OnMostrarMensajeAccion("Confirmación", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_desactivo, "permiso"), tipoPopup.Verde, True, "management/permiso/frmPermiso.aspx")
            End If

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", mensajePopop.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "rol"), tipoPopup.Rojo, True, "management/permiso/frmPermiso.aspx")
        End Try
    End Sub

    Protected Sub btnMarcar_OnClick(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        Dim listaPermisos = New cadenero.CRN.nspPagina.proceso_ObtenerPaginas() With {.tipoConsulta = cadenero.entidades.nspPagina.tipoConsultaPagina.todos}.Ejecutar

        Select Case Session("bandera")
            Case "permisos"
                lvsPaginas.Visible = False
                lsvPermiso.Visible = True
                lvsPaginas.DataSource = listaPermisos.OrderBy(Function(a) a.nombre).ToList
                lvsPaginas.DataBind()
                For i = 0 To listaPermisos.Count - 1
                    Dim chkMarcar As CheckBox = CType(lsvPermiso.Items(i).FindControl("chkEsActivo"), CheckBox)
                    chkMarcar.Checked = True
                Next
            Case "paginas"
                lvsPaginas.Visible = True
                lsvPermiso.Visible = False
                lvsPaginas.DataSource = listaPermisos.OrderBy(Function(a) a.nombre).ToList
                lvsPaginas.DataBind()
                For i = 0 To listaPermisos.Count - 1
                    Dim chkMarcar As CheckBox = CType(lvsPaginas.Items(i).FindControl("chkEsActivo"), CheckBox)
                    chkMarcar.Checked = True
                Next
        End Select
    End Sub

    Protected Sub btnDesmarcar_OnClick(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        Dim listaPermisos = New cadenero.CRN.nspPagina.proceso_ObtenerPaginas() With {.tipoConsulta = cadenero.entidades.nspPagina.tipoConsultaPagina.todos}.Ejecutar
        Select Case Session("bandera")
            Case "permisos"
                lvsPaginas.Visible = False
                lsvPermiso.Visible = True
                lvsPaginas.DataSource = listaPermisos.OrderBy(Function(a) a.nombre).ToList
                lvsPaginas.DataBind()
                For i = 0 To listaPermisos.Count - 1
                    Dim chkMarcar As CheckBox = CType(lsvPermiso.Items(i).FindControl("chkEsActivo"), CheckBox)
                    chkMarcar.Checked = False
                Next
            Case "paginas"
                lvsPaginas.Visible = True
                lsvPermiso.Visible = False
                lvsPaginas.DataSource = listaPermisos.OrderBy(Function(a) a.nombre).ToList
                lvsPaginas.DataBind()
                For i = 0 To listaPermisos.Count - 1
                    Dim chkMarcar As CheckBox = CType(lvsPaginas.Items(i).FindControl("chkEsActivo"), CheckBox)
                    chkMarcar.Checked = False
                Next
        End Select
    End Sub

    Private Sub btnAgregarRol_Click(sender As Object, e As EventArgs) Handles btnAgregarRol.Click
        poblarListView()
        lblNombreRol.Text = cmbRol.SelectedItem.ToString
        divPaginas.Visible = True
    End Sub


#Region "funciones"

    Private Sub poblarListView()
        If cmbRol.SelectedValue = "Selecciona un elemento de la lista" Then
            OnMostrarMensajeAccion("Permiso", "Seleccione al menos un rol antes de continuar.", tipoPopup.Naranja, False, "")
            ' OnMostrarMensajeAccion("Atención", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Seleccione_al_menos_un_registro, "rol"), tipoPopup.Naranja, False, "")
            Exit Sub
        End If
        Dim listaPermisos = New cadenero.CRN.nspPermiso.Proceso_ObtenerPermisos() With {.tipoConsulta = cadenero.entidades.nspPermiso.tipoConsultapermiso.idRol, .idRol = Guid.Parse(cmbRol.SelectedValue.ToString)}.Ejecutar().OrderBy(Function(a) a.nombrePagina).ToList

        If listaPermisos.Count > 0 Then
            Session("bandera") = "permisos"
            lsvPermiso.DataSource = listaPermisos
            lsvPermiso.DataBind()
            lvsPaginas.Visible = False
            lsvPermiso.Visible = True

        Else
            Session("bandera") = "paginas"
            lvsPaginas.Visible = True
            lsvPermiso.Visible = False

            Dim listaPaginas = New cadenero.CRN.nspPagina.proceso_ObtenerPaginas() With {.tipoConsulta = cadenero.entidades.nspPagina.tipoConsultaPagina.todos}.Ejecutar().OrderBy(Function(a) a.nombre).ToList

            lvsPaginas.DataSource = listaPaginas
            lvsPaginas.DataBind()
        End If
    End Sub

    Public Sub poblarRol()
        Dim listaRol = New cadenero.CRN.nspRol.Proceso_ObtenerListaRoles() With {.tipoConsulta = cadenero.entidades.nspRol.tipoConsultaRol.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        cmbRol.DataValueField = "id"
        cmbRol.DataTextField = "nombre"
        cmbRol.Items.Add("Selecciona un elemento de la lista")
        cmbRol.DataSource = listaRol
        cmbRol.DataBind()
        cmbRol.SelectedValue = "Selecciona un elemento de la lista"
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub




#End Region


End Class
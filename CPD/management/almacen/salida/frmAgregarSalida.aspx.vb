Imports System.Globalization
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports CES, CES.nspPopup, CES.nspDetalleSalidaAlmacen, CES.nspArticulo
Imports CRN.nspSalidaAlmacen, CRN.nspDetalleSalidaAlmacen, CRN.nspArea, CRN.nspGenerico, CRN.nspArticulo
Public Class frmAgregarSalida : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarArea()
            Session("lista") = Nothing
            txbFechaSalida.Text = Today
            divCodigoBarras.Visible = True
            divNombreArticulo.Visible = True
            divCmbArticulo.Visible = False

        End If
    End Sub
#Region "Metodos"
    Public Sub llenarArticulos()
        Dim listaArticulos = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.entraAlmacen, .entraAlmacen = True, .tipoSistema = sistemaActivo.tipo}.Ejecutar()
        cmbArticulo.DataSource = listaArticulos
        cmbArticulo.Items.Add("Seleccione un elemento de la lista")
        cmbArticulo.DataTextField = "nombre"
        cmbArticulo.DataValueField = "id"
        cmbArticulo.DataBind()
    End Sub
    Public Sub poblarArea()
        Dim area = New Proceso_ObtenerAreas() With {.tipoConsulta = nspArea.tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        For i = 0 To area.Count - 1
            area(i).nombre = area(i).codigo + " - " + area(i).nombre
        Next
        cmbArea.DataSource = area
        cmbArea.DataTextField = "nombre"
        cmbArea.DataValueField = "id"
        cmbArea.DataBind()
    End Sub
    'Private Sub txbFoliooficio_TextChanged(sender As Object, e As EventArgs) Handles txbFoliooficio.TextChanged
    '    txbFoliooficio.Text = String.Format("{0:D6}", CInt(txbFoliooficio.Text))
    '    txbFoliooficio.Focus()
    'End Sub
    Private Sub chkCodigo_CheckedChanged(sender As Object, e As EventArgs) Handles chkCodigo.CheckedChanged
        If chkCodigo.Checked Then
            lblArticuloCodigo.Text = ""
            divCodigoBarras.Visible = True
            divNombreArticulo.Visible = True
            divCmbArticulo.Visible = False
            chkNombre.Checked = False

        End If
    End Sub
    Private Sub chkNombre_CheckedChanged(sender As Object, e As EventArgs) Handles chkNombre.CheckedChanged
        If chkNombre.Checked Then
            cmbArticulo.Items.Clear()
            llenarArticulos()
            divCodigoBarras.Visible = False
            divNombreArticulo.Visible = False
            divCmbArticulo.Visible = True
            chkCodigo.Checked = False

        End If
    End Sub

#End Region
#Region "Funciones"

    Private Function validarControles() As respuestaDelProceso
        Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If (cmbArea.SelectedValue = "Seleccione un elemento de la lista") Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "área"))
            End If
            If txbFechaSalida.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "fecha salida"))
            End If
            If lsvListado.Items.Count = 0 Then
                Throw New Exception("Inserta al menos un artículo a la lista.")
            End If
        Catch ex As Exception
            respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuesta.comentario = ex.Message.ToString
        End Try
        Return respuesta
    End Function
    Private Function validarArticulo() As respuestaDelProceso
        Dim respuestaArticulo As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            If chkCodigo.Checked = True Then
                If txbCodigo.Text = "" Then
                    Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Código de barras"))
                End If
            ElseIf cmbArticulo.SelectedValue = "Seleccione un elemento de la lista" Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "Artículo"))
            End If

            If txbCantidad.Text.Length = 0 Then
                Throw New Exception(controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_campo_X_es_obligatorio, "cantidad"))
            End If
            If txbCantidad.Text = 0 Then
                Throw New Exception("Debe agregar una cantidad")
            End If
            respuestaArticulo.respuesta = tipoRespuestaDelProceso.Completado
        Catch ex As Exception
            respuestaArticulo.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuestaArticulo.comentario = ex.Message.ToString
        End Try
        Return respuestaArticulo
    End Function
    Private Function limpiarFormulario() As respuestaDelProceso
        Dim respuestaLimpiar As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
        Try
            cmbArea.ClearSelection()
            txbFoliooficio.Text = ""
            txbCodigo.Text = ""
            lblArticuloCodigo.Text = ""
            txbCantidad.Text = ""
            txbFolioVale.Text = ""
            lsvListado.Items.Remove(Items)
        Catch ex As Exception
            respuestaLimpiar.respuesta = tipoRespuestaDelProceso.NoCompletado
            respuestaLimpiar.comentario = ex.Message.ToString
        End Try
        Return respuestaLimpiar
    End Function

#End Region
#Region "Botones"
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim respuestaValidacion = validarControles()
            If respuestaValidacion.respuesta = tipoRespuestaDelProceso.NoCompletado Then
                Throw New Exception(respuestaValidacion.comentario.ToString)
            End If

            Dim entidad = New CES.nspSalidaAlmacen.salidaAlmacen
            entidad.id = Guid.NewGuid()
            entidad.esVale = True
            entidad.idArea = Guid.Parse(cmbArea.SelectedValue)
            entidad.numVale = txbFolioVale.Text
            If txbFechaSalida.Text > Today Then
                Throw New Exception("La fecha no debe ser mayor a hoy")
            End If
            entidad.numOficio = txbFoliooficio.Text
            entidad.fechaSalida = txbFechaSalida.Text
            entidad.ipUsuario = direccionIP
            entidad.idUsuarioMovimiento = IdUsuario
            entidad.idSistema = sistemaActivo.id

            Dim lista = CType(Session("lista"), List(Of listViewArticuloSalida))
            Dim nuevaLista As New List(Of detalleSalidaAlmacen)

            For i = 0 To lista.Count - 1
                Dim editarArticulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = nspArticulo.tipoConsultaArticulo.id, .id = Guid.Parse(lista(i).idArticulo.ToString)}.Ejecutar().FirstOrDefault
                Dim exist = editarArticulo.existencia

                Dim nuevoRegistro As New detalleSalidaAlmacen
                nuevoRegistro.id = lista(i).id
                nuevoRegistro.idSalida = lista(i).idSalida
                nuevoRegistro.idArticulo = lista(i).idArticulo
                nuevoRegistro.cantidad = CInt(lista(i).cantidad)
                nuevoRegistro.fecha = Date.Now
                nuevoRegistro.ipUsuario = direccionIP
                nuevoRegistro.idUsuarioMovimiento = IdUsuario
                nuevoRegistro.idSistema = sistemaActivo.id
                Dim nuevaExistencia = exist - nuevoRegistro.cantidad
                editarArticulo.existencia = nuevaExistencia
                Dim respuesta2 = New Proceso_ActualizarArticulo() With {.entidad = editarArticulo}.Ejecutar()
                nuevaLista.Add(nuevoRegistro)
            Next
            Dim respuesta = New Proceso_AgregarSalidaAlmacen() With {.entidad = entidad, .listaDetalleSalida = nuevaLista}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    Session("lista") = Nothing
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Salida almacen"), tipoPopup.Verde, True, "/management/almacen/salida/frmAgregarSalida.aspx")
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Private Sub cmbArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArea.SelectedIndexChanged
        Try
            Dim vale = New Proceso_ObtenerDatoGenerico() With {.tipoConsulta = nspGenerico.tipoConsultaGenerico.ultimoNumValeSalida_x_Area, .idArea = Guid.Parse(cmbArea.SelectedValue.ToString), .idSistema = sisActivo.sistemaActivo.idSistema}.Ejecutar()
            txbFolioVale.Text = vale.valor
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try

    End Sub

    Private Sub txbCodigo_TextChanged(sender As Object, e As EventArgs) Handles txbCodigo.TextChanged
        Try
            Dim articulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = nspArticulo.tipoConsultaArticulo.codigoBarras, .codigoBarras = txbCodigo.Text, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar().FirstOrDefault()
            If Not articulo Is Nothing Then
                lblArticuloCodigo.Text = articulo.nombre
                btnAgregarCodigo.CommandArgument = "Encontrado"
            Else
                btnAgregarCodigo.CommandArgument = "No Encontrado"
            End If
            If articulo Is Nothing Then
                Throw New Exception("Articulo no encontrado")
            End If
            lblstockActual.Text = articulo.existencia
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub


    Private Sub btnAgregarCodigo_Click(sender As Object, e As EventArgs) Handles btnAgregarCodigo.Click
        Try

            If btnAgregarCodigo.CommandArgument <> "Encontrado" Then
                Throw New Exception("Consulte un artículo antes de continuar")

            End If

            Dim resultadoValidacion = validarArticulo()
            If resultadoValidacion.respuesta <> tipoRespuestaDelProceso.Completado Then
                Throw New Exception(resultadoValidacion.comentario)
            End If
            Dim articulo
            If chkCodigo.Checked = True Then
                articulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = nspArticulo.tipoConsultaArticulo.codigoBarras, .codigoBarras = txbCodigo.Text, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar()
            Else
                articulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = nspArticulo.tipoConsultaArticulo.nombre, .Nombre = cmbArticulo.SelectedItem.ToString, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar()
            End If

            If Not Session("lista") Is Nothing Then

                Dim lista = CType(Session("lista"), List(Of listViewArticuloSalida))
                For i = 0 To lista.Count - 1
                    If lista(i).idArticulo = articulo(0).id Then
                        Throw New Exception(" El artículo estaría duplicado.")
                    End If
                Next
                Dim nuevoRegistro As New listViewArticuloSalida
                paIdArticulo.Value = lblIdArticulo.Text
                nuevoRegistro.id = Guid.NewGuid()
                nuevoRegistro.idArticulo = articulo(0).id
                If txbCantidad.Text > articulo(0).existencia Then
                    Throw New Exception("No es posible retirar la cantidad solicitada, reviza la existencia")
                End If
                nuevoRegistro.cantidad = CInt(txbCantidad.Text)
                nuevoRegistro._precioUnitario = CDbl(articulo(0).ultimoPrecio.ToString)
                nuevoRegistro._importe = CDbl(txbCantidad.Text * articulo(0).ultimoPrecio.ToString)
                nuevoRegistro.fecha = Date.Now
                nuevoRegistro._nombreArticulo = articulo(0).nombre
                nuevoRegistro.ipUsuario = direccionIP
                nuevoRegistro.idUsuarioMovimiento = IdUsuario
                nuevoRegistro.idSistema = sistemaActivo.id
                lista.Add(nuevoRegistro)
                Session("lista") = lista
                DivLista.Visible = True
                lsvListado.DataSource = lista
                lsvListado.DataBind()


            Else
                Dim lista As New List(Of listViewArticuloSalida)
                Dim nuevoRegistro As New listViewArticuloSalida
                paIdArticulo.Value = lblIdArticulo.Text
                nuevoRegistro.id = Guid.NewGuid()
                nuevoRegistro.idArticulo = articulo(0).id
                If txbCantidad.Text > articulo(0).existencia Then
                    Throw New Exception("No es posible retirar la cantidad solicitada, revisar la existencia")
                End If
                nuevoRegistro.cantidad = CInt(txbCantidad.Text)
                nuevoRegistro._precioUnitario = CDbl(articulo(0).ultimoPrecio.ToString)
                nuevoRegistro._importe = CDbl(txbCantidad.Text * articulo(0).ultimoPrecio.ToString)
                nuevoRegistro.fecha = Date.Now
                nuevoRegistro._nombreArticulo = articulo(0).nombre
                nuevoRegistro.ipUsuario = direccionIP
                nuevoRegistro.idUsuarioMovimiento = IdUsuario
                nuevoRegistro.idSistema = sistemaActivo.id
                lista.Add(nuevoRegistro)
                Session("lista") = lista
                DivLista.Visible = True
                lsvListado.DataSource = lista
                lsvListado.DataBind()
            End If
            txbCodigo.Text = ""
            lblArticuloCodigo.Text = ""
            txbCantidad.Text = ""
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", "" & ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try

    End Sub

    Protected Sub lnkEliminar_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        Dim indice As Integer = btn.TabIndex

        Dim lista = CType(Session("lista"), List(Of listViewArticuloSalida))
        lista.RemoveAt(indice)
        Session("lista") = lista
        lsvListado.DataSource = lista
        lsvListado.DataBind()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Session("lista") = Nothing
        mandaDefault()
    End Sub

    Protected Sub cmbArticulo_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim articulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = nspArticulo.tipoConsultaArticulo.nombre, .Nombre = cmbArticulo.SelectedItem.ToString, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar().FirstOrDefault()


            If Not articulo Is Nothing Then
                lblArticuloCodigo.Text = articulo.nombre

                btnAgregarCodigo.CommandArgument = "Encontrado"
            Else
                btnAgregarCodigo.CommandArgument = "No Encontrado"
            End If
            If articulo Is Nothing Then
                Throw New Exception("Articulo no encontrado")
            End If
            lblstockActual.Text = articulo.existencia
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub


#End Region

End Class
Public Class listViewArticuloSalida : Inherits detalleSalidaAlmacen
    Public Property _nombreArticulo
    Public Property _cantidadEntregar
    Public Property _precioUnitario
    Public Property _importe

End Class
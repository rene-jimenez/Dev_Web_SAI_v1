Imports System.Globalization
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports CES, CES.nspPopup, CES.nspDetalleSalidaAlmacen, CES.nspSalidaAlmacen, CES.nspArticulo
Imports CRN.nspSalidaAlmacen, CRN.nspDetalleSalidaAlmacen, CRN.nspArea, CRN.nspArticulo
Imports System.Web.UI
Public Class frmEditarSalida : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                txbCodigo.Text = String.Empty
                Dim SalidaArticulos As New List(Of detalleSalidaAlmacen)
                Session("listaArticulos") = SalidaArticulos
                Session("listaArticulosEliminados") = New List(Of detalleSalidaAlmacen)
                llena()
                llenarListado()
                btnAgregarCodigo.Visible = False
                divCodigoBarras.Visible = True
                divNombreArticulo.Visible = True
                divCmbArticulo.Visible = False
            Catch ex As Exception
                OnMostrarMensajeAccion("Critico", ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub

    Protected Sub txbFechaSalida_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim fecha As Date
            fecha = Date.Now
            If txbFechaSalida.Text > fecha Then
                Throw New Exception(" No puedes poner una fecha mayor a hoy")
                txbFechaSalida.Text = String.Empty
                txbFechaSalida.Focus()
            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Atención", ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub


    Protected Sub txbCodigo_TextChanged(sender As Object, e As EventArgs)

        Try
            Dim listaAgregar As List(Of detalleSalidaAlmacen) = CType(Session("listaArticulos"), List(Of detalleSalidaAlmacen))


            Dim codigo = txbCodigo.Text.Trim
            Dim articulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.codigoBarras, .codigoBarras = codigo, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar().FirstOrDefault()

            If Not articulo Is Nothing Then
                lblArticuloCodigo.Text = articulo.nombre
                txbCodigo.Focus()
                btnAgregarCodigo.Visible = True

            Else
                xAnim("xCodigoInexistente")
                Throw New Exception(" No hay artículos con ese código de barras.")
            End If

            For Each v In listaAgregar
                If v.idArticulo = articulo.id Then
                    Throw New Exception(" El artículo estaría duplicado.")
                End If
            Next





            lblstockActual.Text = articulo.existencia

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
            xAnim("xLimpiarCodigo")
            txbCodigo.Focus()
        End Try


    End Sub


    Public Sub llena()
        Dim idx As String = Request.QueryString("id")
        Dim area = New Proceso_ObtenerAreas() With {.tipoConsulta = nspArea.tipoConsultaArea.esActivo, .esActivo = True}.Ejecutar().OrderBy(Function(a) a.nombre).ToList
        Dim selArea = New Proceso_ObtenerSalidaAlmacen With {.id = Guid.Parse(idx)}.Ejecutar()
        lblArea.Text = selArea._codigoConNombreArea
        hdfArea.Value = selArea.idArea.ToString
        txbFoliooficio.Text = selArea.numOficio
        lblFolioVale.Text = selArea.numVale
        txbObservaciones.Text = selArea.comentario
        txbFechaSalida.Text = selArea.fechaSalida
    End Sub
    Public Sub llenarListado()
        Dim idc = Guid.Parse(Request.QueryString("id"))
        Dim listadoInicial = New Proceso_ObtenerDetallesSalidaAlmacen() With {.tipoConsulta = tipoConsultaDetalleSalidaAlmacen.idSalida, .idSalida = idc}.Ejecutar().OrderBy(Function(xs) xs._codigoBarras).ToList
        Dim listaRemplazar As List(Of detalleSalidaAlmacen) = CType(Session("listaArticulos"), List(Of detalleSalidaAlmacen))
        For Each x In listadoInicial
            listaRemplazar.Add(x)
        Next
        Session("listaArticulos") = listaRemplazar
        lsvListado.DataSource = listaRemplazar
        lsvListado.DataBind()
        'lblTituloArticulos.Text = "Listado de " + listaRemplazar.Count() + "artículos"
        updateAgregarArticulo.Update()

    End Sub
    Public Function xAnim(x77 As String)

        Select Case x77
            Case "xListado"

            Case "xCodigoInexistente"
                lblTituloAgregarCodigos.Text = "Código inexistente!!"
                headerAgregarArticulos.Attributes.Remove("class")
                headerAgregarArticulos.Attributes("class") = "card-header bgm-red"
                lblTituloAgregarCodigos.Attributes("class") = "c-white"
                txbCodigo.Text = String.Empty
                lblArticuloCodigo.Text = String.Empty
                txbCantidad.Text = String.Empty
                btnAgregarCodigo.Visible = False
                lblstockActual.Text = String.Empty
            Case "xCancelar"
            Case "xLimpiarCodigo"
                txbCodigo.Text = String.Empty
                lblArticuloCodigo.Text = String.Empty
                txbCantidad.Text = String.Empty
                lblstockActual.Text = String.Empty
                btnAgregarCodigo.Visible = False
                lblTituloAgregarCodigos.Text = "Agrega artículo"
                headerAgregarArticulos.Attributes.Remove("class")
                headerAgregarArticulos.Attributes("class") = "card-header"
                lblTituloAgregarCodigos.Attributes.Remove("class")
                lblTituloAgregarCodigos.Attributes("class") = "c-bluegray"
        End Select
        Return x77
    End Function
    Public Sub llenarArticulos()
        Dim listaArticulos = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.todos, .tipoSistema = sistemaActivo.tipo}.Ejecutar()
        cmbArticulo.DataSource = listaArticulos
        cmbArticulo.Items.Add("Seleccione un elemento de la lista")
        cmbArticulo.DataTextField = "nombre"
        cmbArticulo.DataValueField = "id"
        cmbArticulo.DataBind()
    End Sub
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
    Protected Sub btnAgregarCodigo_Click(sender As Object, e As EventArgs)
        Dim idc = Guid.Parse(Request.QueryString("id"))

        Try
            Dim listaAgregar As List(Of detalleSalidaAlmacen) = CType(Session("listaArticulos"), List(Of detalleSalidaAlmacen))

            Dim nuevoArticulo As New detalleSalidaAlmacen
            Dim articulo
            If chkCodigo.Checked = True Then
                If txbCodigo.Text.Trim = "" Then
                    Throw New Exception(" No puedes agregar un artículo sin código de barras")
                End If
                nuevoArticulo._codigoBarras = txbCodigo.Text.Trim
                articulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.codigoBarras, .codigoBarras = txbCodigo.Text.Trim, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar()
            Else
                If cmbArticulo.SelectedValue = "Seleccione un elemento de la lista" Then
                    Throw New Exception(" Debe seleccionar un elemento de la lista")
                End If

                articulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.nombre, .Nombre = cmbArticulo.SelectedItem.ToString, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar()
                nuevoArticulo._codigoBarras = articulo(0).codigoBarras
            End If

            Dim cantidad = articulo(0).existencia
            If txbFechaSalida.Text = "" Then
                Throw New Exception(" La fecha de salida no debe estar vacía")
            End If
            If txbCantidad.Text = "" Then
                Throw New Exception(" Debes agregar una cantidad")
            End If
            If cantidad = 0 Then
                Throw New Exception(" No puedes dar salida a este producto, no cuenta con existencias")
            End If
            If txbCantidad.Text = 0 Then
                Throw New Exception(" Debes agregar una cantidad")
            End If

            If txbCantidad.Text > cantidad Then
                Throw New Exception(" No es posible retirar la cantidad solicitada, revisa la existencia")
            End If


            nuevoArticulo.id = articulo(0).id
            nuevoArticulo.cantidad = txbCantidad.Text
            nuevoArticulo.fecha = CDate(txbFechaSalida.Text)
            nuevoArticulo.idArticulo = articulo(0).id
            nuevoArticulo.idSalida = idc
            nuevoArticulo.ipUsuario = hdfNuevo.Value.ToString
            nuevoArticulo.idUsuarioMovimiento = IdUsuario
            nuevoArticulo.idSistema = sistemaActivo.id
            nuevoArticulo._ultimoPrecio = articulo(0).ultimoPrecio.ToString
            nuevoArticulo._importe = CDbl(txbCantidad.Text * articulo(0).ultimoPrecio.ToString)
            nuevoArticulo._nombreArticulo = articulo(0).nombre


            listaAgregar.Add(nuevoArticulo)
            Session("listaArticulos") = listaAgregar

            lsvListado.DataSource = listaAgregar.OrderBy(Function(g) g._codigoBarras)
            lsvListado.DataBind()
            xAnim("xListado")
            xAnim("xLimpiarCodigo")

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

    Protected Sub btnBorrar_Click(sender As Object, e As EventArgs)
        xAnim("xLimpiarCodigo")
    End Sub

    Protected Sub btnEditar_Click(sender As Object, e As EventArgs)
        Try
            Dim salida = New salidaAlmacen
            salida.esVale = True
            salida.idArea = Guid.Parse(hdfArea.Value)
            salida.numVale = lblFolioVale.Text.Trim
            salida.numOficio = txbFoliooficio.Text.Trim
            salida.fechaSalida = txbFechaSalida.Text
            salida.ipUsuario = direccionIP
            salida.idUsuarioMovimiento = IdUsuario
            salida.idSistema = sistemaActivo.id
            salida.comentario = txbObservaciones.Text.Trim
            salida.id = Guid.Parse(Request.QueryString("id"))

            Dim lista = CType(Session("listaArticulos"), List(Of detalleSalidaAlmacen))
            Dim nuevaLista As New List(Of detalleSalidaAlmacen)
            Dim nuevalinea As New detalleSalidaAlmacen
            For Each linea In lista
                Dim obtenerArticulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = nspArticulo.tipoConsultaArticulo.id, .id = Guid.Parse(linea.idArticulo.ToString)}.Ejecutar().FirstOrDefault()
                Dim actualizarArticulo As New articulo
                If linea.ipUsuario = "Nuevo" Then

                    nuevalinea.ipUsuario = direccionIP
                    nuevalinea.id = Guid.NewGuid()
                    nuevalinea.idSalida = linea.idSalida
                    nuevalinea.idArticulo = linea.idArticulo
                    Dim oldStock = obtenerArticulo.existencia
                    Dim newStock = oldStock - linea.cantidad
                    nuevalinea.cantidad = linea.cantidad
                    nuevalinea.fecha = DateTime.Now
                    nuevalinea.idUsuarioMovimiento = IdUsuario
                    nuevalinea.idSistema = sistemaActivo.id

                    actualizarArticulo.existencia = newStock
                    actualizarArticulo.id = nuevalinea.idArticulo
                    actualizarArticulo.ipUsuario = direccionIP
                    actualizarArticulo.idUsuarioMovimiento = IdUsuario
                    actualizarArticulo.idSistema = sistemaActivo.id
                    actualizarArticulo.esActivo = True
                    actualizarArticulo.tipoSistema = sistemaActivo.tipo
                    actualizarArticulo.idCategoria = obtenerArticulo.idCategoria
                    actualizarArticulo.idUnidadMedida = obtenerArticulo.idUnidadMedida
                    actualizarArticulo.nombre = obtenerArticulo.nombre
                    actualizarArticulo.nombreCategoria = obtenerArticulo.nombreCategoria
                    actualizarArticulo.nombreUnidadMedida = obtenerArticulo.nombreUnidadMedida
                    actualizarArticulo.stockMaximo = obtenerArticulo.stockMaximo
                    actualizarArticulo.stockMinimo = obtenerArticulo.stockMinimo
                    actualizarArticulo.ultimoPrecio = obtenerArticulo.ultimoPrecio
                    actualizarArticulo.url = obtenerArticulo.url
                    actualizarArticulo.cantidadInicial = obtenerArticulo.cantidadInicial
                    actualizarArticulo.codigoBarras = obtenerArticulo.codigoBarras
                    actualizarArticulo.entraAlmacen = obtenerArticulo.entraAlmacen

                    nuevaLista.Add(nuevalinea)
                    Dim nkn = New Proceso_AgregarDetalleSalidaAlmacen() With {.entidad = nuevalinea}.Ejecutar()
                    Dim ent = New Proceso_ActualizarArticulo() With {.entidad = actualizarArticulo}.Ejecutar()

                    Select Case ent.respuesta
                        Case tipoRespuestaDelProceso.Completado

                            OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Salida almacén"), tipoPopup.Verde, True, "management/default.aspx")


                        Case tipoRespuestaDelProceso.Advertencia
                            OnMostrarMensajeAccion("Atención", ent.comentario, tipoPopup.Naranja, False, "")
                        Case tipoRespuestaDelProceso.NoCompletado
                            OnMostrarMensajeAccion("Crítico", ent.comentario, tipoPopup.Rojo, False, "")

                    End Select

                Else
                    Continue For
                End If
            Next

            Dim listaEliminar = CType(Session("listaArticulosEliminados"), List(Of detalleSalidaAlmacen))
            If listaEliminar.Count > 0 Then

                For Each dato In listaEliminar
                    Dim obtenerArticulo1 = New Proceso_ObtenerArticulos() With {.tipoConsulta = nspArticulo.tipoConsultaArticulo.id, .id = Guid.Parse(dato.idArticulo.ToString)}.Ejecutar().FirstOrDefault()
                    Dim actualizarArticulo1 As New articulo
                    Dim detalleSalidaBorrar As New detalleSalidaAlmacen

                    detalleSalidaBorrar.id = dato.id
                    detalleSalidaBorrar.idSalida = dato.idSalida '???

                    actualizarArticulo1.id = dato.idArticulo
                    actualizarArticulo1.idUnidadMedida = obtenerArticulo1.idUnidadMedida
                    actualizarArticulo1.nombre = obtenerArticulo1.nombre
                    actualizarArticulo1.codigoBarras = obtenerArticulo1.codigoBarras
                    actualizarArticulo1.cantidadInicial = obtenerArticulo1.cantidadInicial
                    actualizarArticulo1.existencia = obtenerArticulo1.existencia + dato.cantidad
                    actualizarArticulo1.stockMinimo = obtenerArticulo1.stockMinimo
                    actualizarArticulo1.stockMaximo = obtenerArticulo1.stockMaximo
                    actualizarArticulo1.url = obtenerArticulo1.url
                    actualizarArticulo1.idCategoria = obtenerArticulo1.idCategoria
                    actualizarArticulo1.ultimoPrecio = obtenerArticulo1.ultimoPrecio
                    actualizarArticulo1.entraAlmacen = obtenerArticulo1.entraAlmacen
                    actualizarArticulo1.esActivo = True
                    actualizarArticulo1.tipoSistema = sistemaActivo.tipo
                    actualizarArticulo1.ipUsuario = direccionIP
                    actualizarArticulo1.idUsuarioMovimiento = IdUsuario
                    actualizarArticulo1.idSistema = sistemaActivo.id
                    actualizarArticulo1.nombreCategoria = obtenerArticulo1.nombreCategoria
                    actualizarArticulo1.nombreUnidadMedida = obtenerArticulo1.nombreUnidadMedida


                    Dim fgh = New Proceso_EliminarDetalleSalidaAlmacen() With {.id = dato.id}.Ejecutar()
                    Dim sad = New Proceso_ActualizarArticulo() With {.entidad = actualizarArticulo1}.Ejecutar()
                Next
            End If
            Dim respuesta = New Proceso_ActualizarSalidaAlmacen() With {.entidad = salida}.Ejecutar()
            respuesta.comentario = respuesta.comentario.Replace(Chr(10), " ").Replace(Chr(13), " ")
            xAnim("xLimpiarCodigo")

            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    Session("listaArticulosEliminados") = Nothing
                    Session("listaArticulos") = Nothing
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tu_entidad_se_guardo, "Salida almacén"), tipoPopup.Verde, True, "management/default.aspx")
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Atención", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")

            End Select

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", "" & ex.Message.ToString, nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub


    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub
    Protected Sub lnkEliminar_Click(sender As Object, e As EventArgs)
        Try
            Dim listaEliminar = CType(Session("listaArticulos"), List(Of detalleSalidaAlmacen))
            Session("listaArticulos") = listaEliminar
            If listaEliminar.Count = 1 Then
                Throw New Exception(" No puedes eliminar todos los artículos, debe de haber mínimo uno")
            End If
            Dim lnkEliminar As LinkButton = sender
            Dim id As Guid = Guid.Parse(lnkEliminar.CommandArgument)
            idEliminar.Value = id.ToString()
            hdfTipoEliminar.Value = lnkEliminar.CommandName
            Dim sb As StringBuilder = New StringBuilder
            If lnkEliminar.CommandName = "Nuevo" Then
                lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-ban animated infinite wobble c-red fa-3x'></i></div><br /><div style='text-align: center'> Está seguro que desea eliminar el artículo seleccionado?</div>"
            Else
                lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-question-circle animated infinite wobble c-red fa-3x'></i></div><br /><div style='text-align: center'> Está seguro que desea eliminar el artículo seleccionado?</div>"
            End If

            sb.Append("<script> $('#myModalConfirm').modal('show');</script>")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", sb.ToString, False)
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", "" & ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
        End Try

    End Sub

    Protected Sub btnEventoConfirmar_Click(sender As Object, e As EventArgs)
        Try
            Dim listaEliminar = CType(Session("listaArticulos"), List(Of detalleSalidaAlmacen))
            Session("listaArticulos") = listaEliminar
            Dim id As Guid = Guid.Parse(idEliminar.Value)
            If hdfTipoEliminar.Value = "Nuevo" Then
                Dim entidad = listaEliminar.Where(Function(c) c.id = id).FirstOrDefault()
                listaEliminar.Remove(entidad)
                Session("listaArticulos") = listaEliminar
                lsvListado.DataSource = listaEliminar.OrderBy(Function(g) g._codigoBarras)
                lsvListado.DataBind()
                xAnim("xListado")
                If listaEliminar.Count > 0 Then
                    lblTituloArticulos.Text = "Total Artículos:" + listaEliminar.Count().ToString
                Else
                    lblTituloArticulos.Text = "No hay artículos"
                End If
            Else

                Dim EliminaArticulos As List(Of detalleSalidaAlmacen) = CType(Session("listaArticulosEliminados"), List(Of detalleSalidaAlmacen))


                Dim parsear = New Proceso_ObtenerDetallesSalidaAlmacen() With {.tipoConsulta = tipoConsultaDetalleSalidaAlmacen.id, .id = id}.Ejecutar().FirstOrDefault()
                Dim obtenerArticulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = nspArticulo.tipoConsultaArticulo.id, .id = parsear.idArticulo}.Ejecutar().FirstOrDefault

                Dim oldStock = obtenerArticulo.existencia
                Dim newStock = oldStock + parsear.cantidad
                Dim nuevalinea As New detalleSalidaAlmacen

                nuevalinea.ipUsuario = direccionIP
                nuevalinea.idUsuarioMovimiento = IdUsuario
                nuevalinea.idSistema = sistemaActivo.id


                nuevalinea.id = parsear.id
                nuevalinea.idSalida = parsear.idSalida
                nuevalinea.idArticulo = parsear.idArticulo
                nuevalinea.cantidad = parsear.cantidad
                nuevalinea.fecha = parsear.fecha

                EliminaArticulos.Add(nuevalinea)
                Session("listaArticulosEliminados") = EliminaArticulos
                'LsvEliminar.DataSource = EliminaArticulos
                'LsvEliminar.DataBind()

                Dim entidad = listaEliminar.Where(Function(c) c.id = parsear.id).FirstOrDefault()
                listaEliminar.Remove(entidad)
                Session("listaArticulos") = listaEliminar
                lsvListado.DataSource = listaEliminar.OrderBy(Function(g) g._codigoBarras)
                lsvListado.DataBind()
                xAnim("xListado")
                If listaEliminar.Count > 0 Then
                    lblTituloArticulos.Text = "Total artículos:" + listaEliminar.Count().ToString
                Else
                    lblTituloArticulos.Text = "No hay artículos"
                End If


            End If
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", "" & ex.Message.ToString, nspPopup.tipoPopup.Rojo, False, "")
        End Try
    End Sub


    Protected Sub go2Default_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub

    Protected Sub cmbArticulo_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim listaAgregar As List(Of detalleSalidaAlmacen) = CType(Session("listaArticulos"), List(Of detalleSalidaAlmacen))


            'Dim codigo = txbCodigo.Text.Trim
            Dim articulo = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.nombre, .Nombre = cmbArticulo.SelectedItem.ToString, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar().FirstOrDefault()

            If Not articulo Is Nothing Then
                lblArticuloCodigo.Text = articulo.nombre
                txbCodigo.Focus()
                btnAgregarCodigo.Visible = True

            Else
                xAnim("xCodigoInexistente")
                Throw New Exception(" No hay artículos con ese nombre.")
            End If
            For Each v In listaAgregar
                If v.idArticulo = articulo.id Then
                    Throw New Exception(" El artículo estaría duplicado.")
                End If
            Next
            lblstockActual.Text = articulo.existencia

        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Naranja, False, "")
            xAnim("xLimpiarCodigo")
            txbCodigo.Focus()
        End Try
    End Sub
End Class
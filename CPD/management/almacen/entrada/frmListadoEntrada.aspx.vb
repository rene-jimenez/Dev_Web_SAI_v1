Imports CRN.nspPartidaPresupuestal, CRN.nspAlcance, CRN.nspSolicitudGasto, CRN.nspArea, CRN.nspEntrada, CRN.nspDetallePedido, CRN.nspPedido, CRN.nspDetalleEntrada
Imports CES.nspArea, CES.nspAlcance, CES.nspEntrada, CES.nspDetalleEntrada, CES.nspDetallePedido, CES.nspPedido
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Net.NetworkInformation
Imports System.Globalization
Public Class frmListadoEntrada : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim Entrada As New List(Of pedido)
                Session("ListaEntrada") = Entrada
                primero()
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
    Public Sub primero()
        Dim bandera = Request.QueryString("band")
        If bandera IsNot Nothing Then

            listado.Attributes.Remove("class")
            listado.Attributes("class") = "card animated wooble"

            Select Case bandera
                Case "alta"
                    lblBreadcrumb.Text = "Alta"
                    lblTituloListado.Text = "Listado de pedidos sin entradas"
                    Dim resultado = New Proceso_ObtenerPedidos_Con_Entradas() With {.idSistema = sistema.sistemaActivo.id, .ConEntrada = False}.Ejecutar().OrderBy(Function(d) d.fechaElaboracion).ToList
                    lsvListado.DataSource = resultado
                    lsvListado.DataBind()
                Case "edt"
                    lblBreadcrumb.Text = "Editar"
                    lblTituloListado.Text = "Listado de pedidos con entradas parciales"
                    Dim resultado = New Proceso_ObtenerEntradas() With {.tipoConsulta = tipoConsultaEntrada.tipo, .tipo = False, .idSistema = sistema.sistemaActivo.id}.Ejecutar().OrderBy(Function(d) d.fechaEntrada).ToList
                    LsvListado2.DataSource = resultado
                    lsvListado2.DataBind()
                Case "act"
                    lblBreadcrumb.Text = "Actualizar"
                    lblTituloListado.Text = "Listado de pedidos con entradas parciales"
                    Dim resultado = New Proceso_ObtenerEntradas() With {.tipoConsulta = tipoConsultaEntrada.tipo, .tipo = False, .idSistema = sistema.sistemaActivo.id}.Ejecutar().OrderBy(Function(d) d.fechaEntrada).ToList
                    LsvListado2.DataSource = resultado
                    lsvListado2.DataBind()
                Case "cons"
                    lblBreadcrumb.Text = "Consultar"
                    lblTituloListado.Text = "Listado de pedidos con entradas completas"
                    Dim resultado = New Proceso_ObtenerEntradas() With {.tipoConsulta = tipoConsultaEntrada.todos, .idSistema = sistema.sistemaActivo.id}.Ejecutar().OrderBy(Function(d) d.fechaEntrada).ToList
                    LsvListado2.DataSource = resultado
                    lsvListado2.DataBind()
                Case ""
                    mandaDefault()
            End Select
        End If
    End Sub
    Protected Sub btnSalir_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub

    Protected Sub lnkConsultar2_Click(sender As Object, e As EventArgs)
        Dim clic As LinkButton = sender
        Dim indice As Integer = clic.TabIndex
        Dim idEntrada = Guid.Parse(clic.CommandName)
        Dim bandera = Request.QueryString("band")
        Select Case bandera
            Case "edt"
                Response.Redirect("~/management/almacen/entrada/frmEditarEntrada.aspx?idEntrada=" + idEntrada.ToString)
            Case "act"
                Response.Redirect("~/management/almacen/entrada/frmActualizarEntrada.aspx?idEntrada=" + idEntrada.ToString)
            Case "cons"
                Response.Redirect("~/management/almacen/entrada/frmConsultarEntrada.aspx?idEntrada=" + idEntrada.ToString)
        End Select
    End Sub

    Protected Sub lnkConsultarA_Click(sender As Object, e As EventArgs)
        Dim clic As LinkButton = sender
        Dim indice As Integer = clic.TabIndex
        Dim idPedido = Guid.Parse(clic.CommandName)
        Response.Redirect("~/management/almacen/entrada/frmAgregarEntrada.aspx?idPedido=" + idPedido.ToString)
    End Sub
    Protected Sub lnkEliminar_Click(sender As Object, e As EventArgs)
        Dim btnEliminar As LinkButton = sender
        Dim sb As StringBuilder = New StringBuilder
        btnEventoConfirmar.CommandArgument = btnEliminar.CommandName
        btnEventoConfirmar.TabIndex = btnEliminar.TabIndex
        lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-ban animated infinite wobble c-red fa-3x'></i></div> <br /><div style='text-align: center'> ¿Está seguro que desea eliminar el registro seleccionado?</div>"
        sb.Append("<script> $('#myModalConfirm').modal('show');</script>")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", sb.ToString, False)
    End Sub
    Protected Sub btnEventoConfirmar_Click(sender As Object, e As EventArgs) Handles btnEventoConfirmar.Click
        Dim btnEliminar As Button = sender
        Dim idEntrada = Guid.Parse(btnEliminar.CommandArgument.ToString)
        Dim EntradaEliminar = New Proceso_ObtenerEntrada() With {.id = idEntrada}.Ejecutar()
        Dim listaDetallesEliminar = New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = tipoConsultaDetalleEntrada.idEntrada, .idEntrada = idEntrada}.Ejecutar()
        Dim respuestaEliminarEntrada = New Proceso_EliminarEntrada() With {.id = idEntrada, .idSistema = sistemaActivo.idSistema, .ipUsuario = direccionIP, .idUsuarioMovimiento = IdUsuario, .listaDetalleEntrada = listaDetallesEliminar}.Ejecutar()
        Select Case respuestaEliminarEntrada.respuesta
            Case tipoRespuestaDelProceso.Completado
                OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_eliminaron, "Entrada a almacén"), tipoPopup.Verde, True, "management/almacen/entrada/frmListadoEntrada.aspx?band=cons")
            Case tipoRespuestaDelProceso.Advertencia
                OnMostrarMensajeAccion("Atención", respuestaEliminarEntrada.comentario, tipoPopup.Naranja, False, "")
            Case tipoRespuestaDelProceso.NoCompletado
                OnMostrarMensajeAccion("Crítico", respuestaEliminarEntrada.comentario, tipoPopup.Rojo, False, "")
        End Select
    End Sub

    Protected Sub lvsListado2_OnItemDataBound(sender As Object, e As ListViewItemEventArgs)
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim btnEliminar As LinkButton = CType(e.Item.FindControl("lnkEliminar"), LinkButton)
            Dim btnConsultar As LinkButton = CType(e.Item.FindControl("lnkConsultar2"), LinkButton)
            Dim cns = New cadenero.CRN.nspUsuarioRol.Proceso_ObtenerUsuarioRoles() With {.tipoConsulta = cadenero.entidades.nspUsuarioRol.tipoConsultaUsuarioRol.idUsuario, .idUsuario = IdUsuario}.Ejecutar()
            Dim rol = New CRN.nspRol.Proceso_ObtenerUnRol() With {.id = cns(0).idRol}.Ejecutar()
            Dim bandera = Request.QueryString("band")
            Select Case bandera
                Case "edt"
                    btnConsultar.Visible = True
                    btnEliminar.Visible = False
                Case "act"
                    btnConsultar.Visible = True
                    btnEliminar.Visible = False
                Case "cons"
                    btnConsultar.Visible = True
                    btnConsultar.Text = "Consultar entrada"
                    If rol.rol = "Administrador" Then  'mostrar boton eliminar si es administrador
                        btnEliminar.Visible = True
                    Else
                        btnEliminar.Visible = False
                    End If
            End Select
        End If
    End Sub
End Class
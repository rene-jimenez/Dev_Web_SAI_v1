Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup, CRN.nspOficio, CES.nspOficio
Imports cadenero.entidades.nspBase
Public Class frmConsultarOficio : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim accion = Request.QueryString("band")
            Select Case accion
                'Oficio
                Case "Consultar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.todos, .idSistema = sistema.sistemaActivo.id}.Ejecutar
                    poblarOficios(resultado)
                Case "Complementar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.complementarOficio, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "Editar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.todos, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "PedidoAgregar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.pedidoAgregar, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "PedidoEditar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.pedidoModificar, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "PedidoConsultar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.pedidoConsultar, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                    'Solicitud de gasto
                Case "SolicitudGastoAgregar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.solicitudAgregar, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "SolicitudGastoEditar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.editarSolicitud, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "SolicitudGastoActualizar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.actualizarSolicitud, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "SolicitudGastoCancelar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.cancelarSolicitud, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "SolicitudGastoConsultar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.consultarSolicitud, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                    'Afectacion presupuestal
                Case "afectacionAgregar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.afectacionAgregar, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "AfectacionPresupuestalEditar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.afectacionEditar, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "AfectacionPresupuestalSustituir"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.afectacionSustitucion, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "AfectacionPresupuestalConsultar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.afectacionConsultar, .idSistema = sistema.sistemaActivo.id}.Ejecutar()
                    poblarOficios(resultado)
                Case "alcanceAgregar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.alcanceAgregar, .idSistema = sistema.sistemaActivo.id}.Ejecutar
                    poblarOficios(resultado)
                Case "alcanceEditar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.alcanceEditar, .idSistema = sistema.sistemaActivo.id}.Ejecutar
                    poblarOficios(resultado)
                Case "alcanceActualizar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.alcanceActualizar, .idSistema = sistema.sistemaActivo.id}.Ejecutar

                    poblarOficios(resultado)
                Case "alcanceCancelar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.alcanceCancelar, .idSistema = sistema.sistemaActivo.id}.Ejecutar
                    poblarOficios(resultado)
                Case "alcanceConsultar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.alcanceConsultar, .idSistema = sistema.sistemaActivo.id}.Ejecutar
                    poblarOficios(resultado)
                Case "comprobacionAgregar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.comprobacionAgregar, .idSistema = sistema.sistemaActivo.id}.Ejecutar
                    poblarOficios(resultado)
                Case "comprobacionConsultar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.comprobacionConsultar, .idSistema = sistema.sistemaActivo.id}.Ejecutar
                    poblarOficios(resultado)
                Case "devolucionAgregar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.devolucionAgregar, .idSistema = sistema.sistemaActivo.id}.Ejecutar
                    poblarOficios(resultado)
                Case "devolucionConsultar"
                    Dim resultado = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.devolucionConsultar, .idSistema = sistema.sistemaActivo.id}.Ejecutar
                    poblarOficios(resultado)
                Case Else
                    mandaDefault()
            End Select
        End If
    End Sub
    Private Sub poblarOficios(resultado As List(Of oficio))
        lvsOficio.DataSource = resultado.OrderByDescending(Function(a) a.fechaCaptura).OrderByDescending(Function(a) a.turnoDRM)
        lvsOficio.DataBind()
    End Sub

    Protected Sub btnSeleccionar_OnClick(sender As Object, e As EventArgs)
        Try
            Dim btnSeleccionar As LinkButton = sender
            Dim id As Guid = Guid.Parse(btnSeleccionar.CommandArgument)
            Dim accion = Request.QueryString("band")
            Select Case accion
                Case "Consultar"
                    Response.Redirect("~/management/oficio/frmConsultaOficio.aspx?id=" + id.ToString)
                Case "Complementar"
                    Response.Redirect("~/management/oficio/frmComplementarOficio.aspx?id=" + id.ToString)
                Case "Editar"
                    Response.Redirect("~/management/oficio/frmEditarOficio.aspx?id=" + id.ToString)
                Case "PedidoAgregar"
                    Response.Redirect("~/management/pedido/frmPedidoAgregar.aspx?idOficio=" + id.ToString)
                Case "PedidoEditar"
                    Response.Redirect("~/management/pedido/frmListaPedido.aspx?idOficio=" + id.ToString + "&band=0")
                Case "PedidoConsultar"
                    Response.Redirect("~/management/pedido/frmListaPedido.aspx?idOficio=" + id.ToString + "&band=1")
                    'Solicitud de gasto
                Case "SolicitudGastoAgregar"
                    Response.Redirect("~/management/solicitudGasto/frmSolicitudAgregar.aspx?idOficio=" + id.ToString)
                Case "SolicitudGastoEditar"
                    Response.Redirect("~/management/solicitudGasto/frmSolicitudEditar.aspx?idOficio=" + id.ToString)
                Case "SolicitudGastoActualizar"
                    Response.Redirect("~/management/solicitudGasto/frmSolicitudActualizar.aspx?idOficio=" + id.ToString)
                Case "SolicitudGastoCancelar"
                    Response.Redirect("~/management/solicitudGasto/frmSolicitudCancelar.aspx?idOficio=" + id.ToString)
                Case "SolicitudGastoConsultar"
                    Response.Redirect("~/management/solicitudGasto/frmListaSolicitudGasto.aspx?idOficio=" + id.ToString)
                    'Afectacion presupuestal
                Case "afectacionAgregar"
                    Response.Redirect("~/management/afectacion/frmListaPedidosAfectacion.aspx?idOficio=" + id.ToString + "&band=add")
                Case "AfectacionPresupuestalEditar"
                    Response.Redirect("~/management/afectacion/frmListaPedidosAfectacion.aspx?idOficio=" + id.ToString + "&band=edt")
                Case "AfectacionPresupuestalSustituir"
                    Response.Redirect("~/management/afectacion/frmListaPedidosAfectacion.aspx?idOficio=" + id.ToString + "&band=stc")
                Case "AfectacionPresupuestalConsultar"
                    Response.Redirect("~/management/afectacion/frmListaPedidosAfectacion.aspx?idOficio=" + id.ToString + "&band=cst")
                Case "alcanceAgregar"
                    Dim idSolicitud = New CRN.nspSolicitudGasto.Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = CES.nspSolicitudGasto.tipoConsultaSolicitudGasto.idOficio, .idOficio = id}.Ejecutar.FirstOrDefault().id
                    Response.Redirect("~/management/alcance/frmAlcanceAgregar.aspx?idSolicitud=" + idSolicitud.ToString)
                Case "alcanceEditar"
                    Dim listaAlcances = New CRN.nspAlcance.Proceso_ObtenerAlcances() With {.tipoConsulta = CES.nspAlcance.tipoConsultaAlcance.idOficio, .idOficio = id}.Ejecutar()
                    Dim noCancelados = listaAlcances.Where(Function(a) a.esCancelado = 0)
                    If noCancelados.Count = 1 Then
                        Dim idAlcance = noCancelados.FirstOrDefault.id
                        Response.Redirect("~/management/alcance/frmAlcanceEditar.aspx?idAlcance=" + idAlcance.ToString)
                    Else
                        Response.Redirect("~/management/alcance/frmAlcanceEditar.aspx?idAlcance=" + listaAlcances.FirstOrDefault.id.ToString)
                    End If

                Case "alcanceActualizar"
                    Dim listaAlcances = New CRN.nspAlcance.Proceso_ObtenerAlcances() With {.tipoConsulta = CES.nspAlcance.tipoConsultaAlcance.idOficio, .idOficio = id}.Ejecutar()
                    Dim noCancelados = listaAlcances.Where(Function(a) a.esCancelado = 0)
                    If noCancelados.Count = 1 Then
                        Dim idAlcance = noCancelados.FirstOrDefault.id
                        Response.Redirect("~/management/alcance/frmAlcanceActualizar.aspx?idAlcance=" + idAlcance.ToString)
                    Else
                        Response.Redirect("~/management/alcance/frmAlcanceActualizar.aspx?idAlcance=" + listaAlcances.FirstOrDefault.id.ToString)
                    End If
                Case "alcanceCancelar"
                    Dim listaAlcances = New CRN.nspAlcance.Proceso_ObtenerAlcances() With {.tipoConsulta = CES.nspAlcance.tipoConsultaAlcance.idOficio, .idOficio = id}.Ejecutar()
                    Dim noCancelados = listaAlcances.Where(Function(a) a.esCancelado = 0)
                    If noCancelados.Count = 1 Then
                        Dim idAlcance = noCancelados.FirstOrDefault.id
                        Response.Redirect("~/management/alcance/frmAlcanceCancelar.aspx?idAlcance=" + idAlcance.ToString)
                    Else
                        Response.Redirect("~/management/alcance/frmAlcanceCancelar.aspx?idAlcance=" + listaAlcances.FirstOrDefault.id.ToString)
                    End If
                Case "alcanceConsultar"
                    Response.Redirect("~/management/alcance/listaAlcance.aspx?id=" + id.ToString)
                Case "comprobacionAgregar"
                    Response.Redirect("~/management/comprobacion/frmAgregarComprobacion.aspx?idOficio=" + id.ToString)
                Case "comprobacionConsultar"
                    Dim idComprobacion = New CRN.nspComprobacion.Proceso_ObtenerComprobaciones() With {.tipoConsulta = CES.nspComprobacion.tipoConsultaComprobacion.idOficio, .idOficio = id}.Ejecutar
                    Response.Redirect("~/management/comprobacion/frmConsultarComprobacion.aspx?idComp=" + idComprobacion(0).id.ToString)
                Case "devolucionAgregar"
                    Response.Redirect("~/management/comprobacion/frmAgregarDevolucion.aspx?idOficio=" + id.ToString)
                Case "devolucionConsultar"
                    Dim idComprobacion = New CRN.nspComprobacion.Proceso_ObtenerComprobaciones() With {.tipoConsulta = CES.nspComprobacion.tipoConsultaComprobacion.idOficio, .idOficio = id}.Ejecutar.FirstOrDefault.id
                    Response.Redirect("~/management/comprobacion/frmConsultarDevolucion.aspx?idComp=" + idComprobacion.ToString)
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Oficio", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Private Sub btn_default_Click(sender As Object, e As EventArgs) Handles btn_default.Click
        mandaDefault()
    End Sub
End Class
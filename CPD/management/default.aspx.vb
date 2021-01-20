Imports CRN.nspArea, CRN.nspPedido, CRN.nspOficio, CRN.nspRubroRequerimiento, CRN.nspPartidaPresupuestal, CRN.nspFirma, CRN.nspProveedor, CRN.nspTipoPago, CES.nspDetallePedido, CRN.nspDetallePedido, CRN.nspUnidadMedida, CRN.nspArticulo, CRN.nspDashboard, CES.nspArticulo, CES.nspDashboard
Imports CES, CES.nspOficio
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Imports cadenero

Public Class _default : Inherits paginaBase
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim x As New nspPaginaBase.PaginaBase
                Dim id = x.sistemaActivo.id

                Session("datasetReporteSolicitud") = Nothing
                Session("lista") = Nothing
                llena()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub llena()
        Dim fecha As DateTime
        fecha = DateTime.Now()
        lblFecha.Text = fecha.ToString("f", CultureInfo.CreateSpecificCulture("es-MX"))
        Dim usuario As String
        usuario = myUsuario
        lblUsuario.Text = usuario

        If RolUsuario = "Almacén - Administrador" Or RolUsuario = "Almacén - Captura" Then
            divDashboard.Visible = False
        Else
            'Dim articulin = New Proceso_ObtenerArticulos() With {.tipoConsulta = .tipoConsulta.todos, .tipoSistema = sisActivo.sistemaActivo.tipo}.Ejecutar().OrderBy(Function(x) x.nombre).Where((Function(d) d.existencia <= d.stockMinimo)).ToList
            'lsvArticulos.DataSource = articulin
            'lsvArticulos.DataBind()

            Dim dashboardcito = New Proceso_ObtenerDashboard() With {.idSistema = sisActivo.sistemaActivo.idSistema}.Ejecutar().FirstOrDefault()
            lblOEP.Text = dashboardcito.oficiosEnproceso
            lblOA.Text = dashboardcito.oficiosAtendidos
            lblSGNL.Text = dashboardcito.solGastoNoLiberados
            lblSGLP.Text = dashboardcito.solGastoLiberadosPendientes
            lblSGC.Text = dashboardcito.solGastoComprobados
            lblPPE.Text = dashboardcito.pedidoPendientesEntrega
            lblPE.Text = dashboardcito.pedidosEntregados
            lblPSA.Text = dashboardcito.pedidoSinAfectacion

            Dim ofix = New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.todos, .idSistema = sisActivo.sistemaActivo.idSistema}.Ejecutar().ToList.OrderByDescending(Function(t) t.fechaCaptura).Take(4)

            If ofix(0).turnoDRM Is Nothing Then
                ofix(0).turnoDRM = "---"
            End If
            If ofix(0).turnoSAF Is Nothing Then
                ofix(0).turnoSAF = "---"
            End If
            If ofix(0).asunto Is Nothing Then
                ofix(0).asunto = "---"
            End If
            lsvOfix.DataSource = ofix
            lsvOfix.DataBind()

        End If
    End Sub

    'Private Sub btn_Click(sender As Object, e As EventArgs) Handles btn.Click
    '    OnMostrarMensajeAccion("mensaje", "saludo", CES.nspPopup.tipoPopup.Verde, False, "")
    'End Sub
End Class
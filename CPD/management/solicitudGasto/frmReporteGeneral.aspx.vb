Imports Microsoft.Reporting.WebForms
Public Class frmReporteGeneral : Inherits nspPaginaBase.PaginaBase


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarComboSolicita()
            poblarComboAutoriza()
            btnCerrar.Visible = False
        End If
    End Sub


    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim dataSource As ReportDataSource = New ReportDataSource()
            rptSolicitudGasto.LocalReport.DataSources.Clear()
            Dim dtsReporteSolicitudGasto As dtsReporteSolicitudGasto = Session("datasetReporteSolicitud")
            Dim consultaSolicita = New CRN.nspFirma.Proceso_ObtenerFirma() With {.id = Guid.Parse(cmbSolicita.SelectedValue.ToString)}.Ejecutar()
            Dim consultaAutoriza = New CRN.nspFirma.Proceso_ObtenerFirma() With {.id = Guid.Parse(cmbAutoriza.SelectedValue)}.Ejecutar()
            Dim parametros As New List(Of ReportParameter)
            parametros.Add(New ReportParameter("solicita", consultaSolicita._nombreUsuario))
            parametros.Add(New ReportParameter("autorizaNuevo", consultaAutoriza._nombreUsuario))

            dataSource = New ReportDataSource("dtsReporteSolicitudGasto", dtsReporteSolicitudGasto.tblReporteSolicitudGasto.DefaultView)
            rptSolicitudGasto.ProcessingMode = ProcessingMode.Local
            rptSolicitudGasto.LocalReport.ReportPath = "management/solicitudGasto/rptReporteSolicitudGasto/rptReporteSolicitudGasto.rdlc"
            rptSolicitudGasto.LocalReport.SetParameters(parametros)
            rptSolicitudGasto.LocalReport.DataSources.Add(dataSource)
            rptSolicitudGasto.LocalReport.Refresh()
            divCmb.Visible = False
            btnCerrar.Visible = True
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, CES.nspPopup.tipoPopup.Naranja, False, "")
        End Try
    End Sub
    Public Sub poblarComboAutoriza()
        Dim autoriza = New CRN.nspFirma.Proceso_ObtenerFirmas() With {.tipoConsulta = CES.nspFirma.tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Autoriza", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(ew) ew._nombreUsuario).ToList
        cmbAutoriza.DataSource = autoriza
        cmbAutoriza.DataTextField = "_nombreUsuario"
        cmbAutoriza.DataValueField = "id"
        cmbAutoriza.DataBind()
    End Sub
    Public Sub poblarComboSolicita()
        Dim solicita = New CRN.nspFirma.Proceso_ObtenerFirmas() With {.tipoConsulta = CES.nspFirma.tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Solicita", .esActivo = True, .idSistema = sistemaActivo.id}.Ejecutar().OrderBy(Function(ew) ew._nombreUsuario).ToList
        cmbSolicita.DataSource = solicita
        cmbSolicita.DataTextField = "_nombreUsuario"
        cmbSolicita.DataValueField = "id"
        cmbSolicita.DataBind()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        mandaDefault()
    End Sub
End Class
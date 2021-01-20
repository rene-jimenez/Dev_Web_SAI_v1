Imports CES.nspReporteSolicitud, CRN.nspSolicitudGasto
Imports CES.nspPopup
Imports Microsoft.Reporting.WebForms
Public Class frmReporteSolicitud : Inherits nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            divCmb.Visible = True
            poblarComboSolicita()
            poblarComboAutoriza()
            btnCerrar.Visible = False
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim consultaSolicitud
            If Request.QueryString("idS") <> Nothing Then
                consultaSolicitud = New Proceso_ObtenerSolicitudGasto() With {.id = Guid.Parse(Request.QueryString("idS"))}.Ejecutar()
            Else

                'El siguiente codigo se mantiene así creyendo que solo existirá UNA solicitud sin cancelar por oficio (no apto para cuando existe mas de una)
                Dim solicitudgasto = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = CES.nspSolicitudGasto.tipoConsultaSolicitudGasto.idOficio, .idOficio = Guid.Parse(Request.QueryString("idOficio"))}.Ejecutar().Where(Function(a) a.esCancelado = 0).FirstOrDefault()
                consultaSolicitud = New Proceso_ObtenerSolicitudGasto() With {.id = Guid.Parse(solicitudgasto.id.ToString)}.Ejecutar()
            End If

            Dim dataSource As ReportDataSource = New ReportDataSource()
            rptReporteSolicitudGastos.LocalReport.DataSources.Clear()
            Dim dtsSolicitudGasto As New dtsReporteSolicitudGasto
            Dim turnoDRM = consultaSolicitud._turnoDRM.ToString
            Dim turnoSAF = consultaSolicitud._turnoSAF.ToString
            Dim folioTesoreria As String
            If consultaSolicitud.folioTesoreria Is Nothing Then
                folioTesoreria = " "
            Else
                folioTesoreria = consultaSolicitud.folioTesoreria.ToString
            End If
            Dim fechaElaboracion = FormatDateTime(consultaSolicitud.fechaElaboracion, DateFormat.ShortDate)
            Dim nombrePartidaPres = UCase(consultaSolicitud._nombrePartidaPresupuestal.ToString)
            Dim importe = FormatNumber(consultaSolicitud.importe, 2)
            Dim concepto = UCase(consultaSolicitud.concepto.ToString)
            Dim marcaAgua = UCase(consultaSolicitud.marcaAgua.ToString)
            Dim folioCaja As String
            If consultaSolicitud.folioCaja Is Nothing Then
                folioCaja = " "
            Else
                folioCaja = consultaSolicitud.folioCaja.ToString
            End If
            Dim fechaRecepcion As String
            If consultaSolicitud.fechaRecepcion Is Nothing Then
                fechaRecepcion = " "
            Else
                fechaRecepcion = FormatDateTime(consultaSolicitud.fechaRecepcion, DateFormat.ShortDate)
            End If
            Dim area = New CRN.nspArea.Proceso_ObtenerArea() With {.id = consultaSolicitud._idArea}.Ejecutar()
            Dim numArea = area.codigo
            Dim nomArea = area.nombre
            Dim cargoPresupuestal = New CRN.nspArea.Proceso_ObtenerArea() With {.id = consultaSolicitud._idCargoPresupuestal}.Ejecutar()
            Dim nomDAreaCargoPresup = cargoPresupuestal.nombre.ToString
            Dim sistema As New nspPaginaBase.PaginaBase
            Dim partidaPresupuestal = New CRN.nspPartidaPresupuestal.Proceso_ObtenerPartidaPresupuestal() With {.id = consultaSolicitud.idPartidaPresupuestal}.Ejecutar()
            Dim numPartidaPres = partidaPresupuestal.numero.ToString
            Dim autoriza = New CRN.nspFirma.Proceso_ObtenerFirmas() With {.tipoConsulta = CES.nspFirma.tipoConsultaFirma.nombre, .Nombre = "Autoriza"}.Ejecutar().FirstOrDefault
            Dim nombreUsuarioAutoriza = autoriza._nombreUsuario.ToString
            Dim conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
            Dim cadena = conversor.dblToStrPesos(consultaSolicitud.importe).ToString()
            dtsSolicitudGasto.tblReporteSolicitudGasto.AddtblReporteSolicitudGastoRow(turnoDRM, turnoSAF, fechaElaboracion, nomArea, nomDAreaCargoPresup, nombrePartidaPres, importe, concepto, folioCaja, fechaRecepcion, UCase(nombreUsuarioAutoriza), cadena, numArea, numPartidaPres, marcaAgua, folioTesoreria, UCase(sistema.sistemaActivo.nombre) + " " + sistema.sistemaActivo.año.ToString, UCase(NombreUsuario))
            Dim consultaSolicita = New CRN.nspFirma.Proceso_ObtenerFirma() With {.id = Guid.Parse(cmbSolicita.SelectedValue.ToString)}.Ejecutar()
            Dim consultaAutoriza = New CRN.nspFirma.Proceso_ObtenerFirma() With {.id = Guid.Parse(cmbAutoriza.SelectedValue)}.Ejecutar()
            Dim parametros As New List(Of ReportParameter)
            parametros.Add(New ReportParameter("solicita", consultaSolicita._nombreUsuario))
            parametros.Add(New ReportParameter("autorizaNuevo", consultaAutoriza._nombreUsuario))

            If Request.QueryString("ca") = "1" Then
                rptReporteSolicitudGastos.LocalReport.ReportPath = "management/solicitudGasto/reporteSolicitud/rpt/rptSolicitudCancelada.rdlc"
            Else

                rptReporteSolicitudGastos.LocalReport.ReportPath = "management/solicitudGasto/rptReporteSolicitudGasto/rptReporteSolicitudGasto.rdlc"
            End If
            dataSource = New ReportDataSource("dtsReporteSolicitudGasto", dtsSolicitudGasto.tblReporteSolicitudGasto.DefaultView)
            rptReporteSolicitudGastos.ProcessingMode = ProcessingMode.Local
            rptReporteSolicitudGastos.LocalReport.DataSources.Add(dataSource)
            rptReporteSolicitudGastos.LocalReport.SetParameters(parametros)
            rptReporteSolicitudGastos.LocalReport.Refresh()
            rptReporteSolicitudGastos.LocalReport.EnableExternalImages = True
            divCmb.Visible = False
            btnCerrar.Visible = True
        Catch ex As Exception
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
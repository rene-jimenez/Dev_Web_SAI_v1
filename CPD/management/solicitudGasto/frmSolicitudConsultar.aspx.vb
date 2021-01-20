Imports CES.nspSolicitudGasto, CRN.nspSolicitudGasto
Imports CES.nspPartidaPresupuestal, CRN.nspPartidaPresupuestal
Imports CES.nspArea, CRN.nspArea
Imports CES.nspFirma, CRN.nspFirma
Public Class frmSolicitudConsultar : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarSolicitudGasto()
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=SolicitudGastoConsultar")
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Response.Redirect("~/management/solicitudGasto/frmReporteGeneral.aspx")
    End Sub

    Private Sub poblarSolicitudGasto()
        Dim dtsReporteSolicitudGasto As New dtsReporteSolicitudGasto
        Dim id = Guid.Parse(Request.QueryString("id").ToString)
        Dim solicitudGasto = New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = tipoConsultaSolicitudGasto.id, .id = id}.Ejecutar().FirstOrDefault

        If Not solicitudGasto Is Nothing Then
            txbTurnoDRM.Text = solicitudGasto._turnoDRM
            txbArea.Text = solicitudGasto._nombreArea
            txbFechaElaboracion.Text = solicitudGasto.fechaElaboracion
            txbCargoPres.Text = solicitudGasto._nombreCargoPresupuestal
            txbPartidaPresupuestal.Text = solicitudGasto._nombrePartidaPresupuestal
            txbImporte.Text = "$" + (Convert.ToDecimal(solicitudGasto.importe).ToString("N2")).ToString
            txbConcepto.Text = solicitudGasto.concepto
            txbMarcaAgua.Text = solicitudGasto.marcaAgua
            If Not solicitudGasto.folioCaja Is Nothing Then
                txbFolioCaja.Text = solicitudGasto.folioCaja.ToString
            Else
                txbFolioCaja.Text = ""
            End If
            If Not solicitudGasto.folioTesoreria Is Nothing Then
                txbFolioTesoreria.Text = solicitudGasto.folioTesoreria.ToString
            Else
                txbFolioTesoreria.Text = ""
            End If
            If Not solicitudGasto.fechaRecepcion Is Nothing Then
                txbFechaRecepcion.Text = solicitudGasto.fechaRecepcion
            Else
                txbFechaRecepcion.Text = ""
            End If
            txbFechaRecepcion.Text = solicitudGasto.fechaRecepcion.ToString

            txbFechaCancelacion.Text = solicitudGasto.fechaCancelacion.ToString
            txbResponsableCancelacion.Text = solicitudGasto.responsableCancelacion
            txbObservacionCancelacion.Text = solicitudGasto.observacionCancelacion
            If solicitudGasto.esCancelado = True Then
                esCancelado.Visible = True
            Else
                esCancelado.Visible = False
            End If
            Dim fechaRecepcion As String = ""
            If Not solicitudGasto.fechaRecepcion Is Nothing Then
                fechaRecepcion = CDate(solicitudGasto.fechaRecepcion).ToString("dd/MM/yyyy")
            End If
            Dim autorizo = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombre, .Nombre = "Autoriza"}.Ejecutar.FirstOrDefault
            Dim area = New Proceso_ObtenerArea() With {.id = Guid.Parse(solicitudGasto._idArea.ToString)}.Ejecutar()
            Dim partidaPresupuestal = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(solicitudGasto.idPartidaPresupuestal.ToString)}.Ejecutar()
            Dim Conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
            Dim cadena = Conversor.dblToStrPesos(txbImporte.Text).ToString()
            Dim imp = txbImporte.Text
            dtsReporteSolicitudGasto.tblReporteSolicitudGasto.AddtblReporteSolicitudGastoRow(solicitudGasto._turnoDRM, solicitudGasto._turnoSAF, CDate(solicitudGasto.fechaElaboracion).ToString("dd/MM/yyyy"), solicitudGasto._nombreArea, solicitudGasto._nombreCargoPresupuestal, UCase(solicitudGasto._nombrePartidaPresupuestal), imp.ToString, UCase(txbConcepto.Text.ToString), txbFolioCaja.Text, fechaRecepcion, UCase(autorizo._nombreUsuario), cadena, area.codigo, partidaPresupuestal.numero, UCase(txbMarcaAgua.Text), txbFolioTesoreria.Text, UCase(sistema.sistemaActivo.nombre) + " " + sistema.sistemaActivo.año.ToString, UCase(NombreUsuario))
            Session("datasetReporteSolicitud") = dtsReporteSolicitudGasto
        End If


    End Sub
End Class
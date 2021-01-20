Imports CRN.nspPartidaPresupuestal, CRN.nspAlcance, CRN.nspSolicitudGasto, CRN.nspArea
Imports CES.nspArea, CES.nspAlcance
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Net.NetworkInformation
Imports System.Globalization
Public Class frmAlcanceConsultar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim idAlcance = Request.QueryString("idAlcance")
                poblarCampos(idAlcance)

            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "Métodos"
    Public Sub poblarCampos(id As String)

        Dim alcance = New Proceso_ObtenerAlcance() With {.id = Guid.Parse(id)}.Ejecutar()
        Dim solicitud = New Proceso_ObtenerSolicitudGasto() With {.id = alcance.idSolicitud}.Ejecutar()
        lblDRM.Text = solicitud._turnoDRM.ToString
        lblSAF.Text = solicitud._turnoSAF.ToString
        lblCantidad.Text = solicitud.importe.ToString
        Dim area = New Proceso_ObtenerArea() With {.id = solicitud._idArea}.Ejecutar()
        lblNombreArea.Text = area.nombre.ToString
        lblArea.Text = area.codigo.ToString
        Dim cargoPres = New Proceso_ObtenerArea() With {.id = solicitud._idCargoPresupuestal}.Ejecutar()
        lblCargo.Text = cargoPres.nombre.ToString
        lblConcepto.Text = solicitud.concepto.ToString
        If solicitud.folioCaja <> "" Then
            lblFolioCaja.Text = solicitud.folioCaja.ToString
        Else
            lblFolioCaja.Text = "Sin folio de caja"
        End If
        If solicitud.fechaRecepcion.ToString <> "" Then
            lblFechaLiberacion.Text = CDate(solicitud.fechaRecepcion).ToString("D",
                        CultureInfo.CreateSpecificCulture("es-MX"))
        Else
            lblFechaLiberacion.Text = "Sin Fecha de recepción"
        End If

        lblFechaCaptura.Text = CDate(solicitud.fechaElaboracion).ToString("D", CultureInfo.CreateSpecificCulture("es-MX"))
        If solicitud.folioTesoreria <> "" Then
            lblFolioTesoreria.Text = solicitud.folioTesoreria.ToString
        Else
            lblFolioTesoreria.Text = "Sin folio de Tesorería"
        End If

        'Datos detalle solicitud
        Dim partidaDetalles = New Proceso_ObtenerPartidaPresupuestal() With {.id = solicitud.idPartidaPresupuestal}.Ejecutar()
        lblPartida.Text = partidaDetalles.numero.ToString
        lblDescripcion.Text = partidaDetalles.nombre.ToString

        'Datos partida
        If alcance.folioCaja <> "" Then
            lblFolioAlcanceCaja.Text = alcance.folioCaja.ToString
        Else
            lblFolioAlcanceCaja.Text = "Sin folio de caja"
        End If
        If alcance.folioTesoreria <> "" Then
            lblFolioAlcanceTesoreria.Text = alcance.folioTesoreria.ToString
        Else
            lblFolioAlcanceTesoreria.Text = "Sin folio de tesorería"
        End If
        If alcance.fechaRecepcion Is Nothing Then
            lblFechaLiberacion.Text = "No contiene fecha de recepción"
        Else
            lblFechaRecepcionAlcance.Text = CDate(alcance.fechaRecepcion).ToString("D",
                        CultureInfo.CreateSpecificCulture("es-MX"))
        End If

        If alcance.esCancelado Then
            txbFechaCancelacion.Text = alcance.fechaCancelacion
            txbResponsableCancelacion.Text = alcance.responsableCancelacion
            txbObservacionCancelacion.Text = alcance.observacionCancelacion
        Else
            esCancelado.Visible = False
        End If

        lblFechaCapturaAlcance.Text = CDate(alcance.fechaCaptura).ToString("D",
                        CultureInfo.CreateSpecificCulture("es-MX"))
        lblImporteAlcance.Text = alcance.importe.ToString
        Dim partidaDeAlcance = New Proceso_ObtenerPartidaPresupuestal() With {.id = alcance.idPartida}.Ejecutar()
        lblPartidaPresupuestalAlcance.Text = partidaDeAlcance.nombre.ToString
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim idAlcance = Request.QueryString("idAlcance")
        Response.Redirect("reporteAlcance/frmReporteAlcance.aspx?idAlcance=" + idAlcance.ToString)
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
#End Region
#Region "Botones"


#End Region
End Class
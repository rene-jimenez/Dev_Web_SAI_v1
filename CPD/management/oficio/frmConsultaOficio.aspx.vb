Imports CES.nspOficio, CRN.nspOficio
Imports CRN.nspEstatusOficio, CES.nspEstatusOficio
Imports CRN.nspArea, CES.nspArea
Imports CRN.nspResponsable, CES.nspResponsable
Imports CRN.nspRubroRequerimiento, CES.nspRubroRequerimiento
Public Class frmConsultaOficio : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            poblarFormularioOficio()
        End If
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect("~/management/oficio/frmConsultarOficio.aspx?band=Consultar")
    End Sub

    Private Sub poblarFormularioOficio()
        Dim oficio = New Proceso_ObtenerUnOficio With {.id = Guid.Parse(Request.QueryString("id").ToString)}.Ejecutar()
        lblTurno.Text = oficio.turnoSAF
        lblDRM.Text = separarDRM(oficio.turnoDRM)
        lblArea.Text = oficio._area
        lblResponsable.Text = oficio._responsable
        lblRubro.Text = oficio._rubro
        lblAsunto.Text = oficio.asunto
        lblIndicaciones.Text = oficio.indicaciones
        lblCargoPresupuestal.Text = oficio._cargoPresupuestal
        lblFechaCaptura.Text = oficio.fechaCaptura
        chkDocInterno.Checked = oficio.esDocInterno
        lblObservaciones.Text = oficio.comentarios
        lblEstatusOficio.Text = obtenerEstatusOficio(oficio.idEstatusOficio.ToString)
        lblFechaAtendido.Text = oficio.fechaAtendido.ToString
        chkAtenderOficio.Checked = oficio.esAtendido
        If obtenerEstatusOficio(oficio.idEstatusOficio.ToString) = "Consolidado" Then
            chkConsolidar.Checked = True
        Else
            chkConsolidar.Checked = False
        End If


    End Sub

    Private Function obtenerEstatusOficio(id As String) As String
        Dim estatusOficio = New Proceso_ObtenerEstatusOficio() With {.id = Guid.Parse(id)}.Ejecutar()
        Return estatusOficio.nombre
    End Function
    Private Function separarDRM(DRM As String) As String
        Dim cadenaDRM As String = ""
        For i = 1 To 6
            cadenaDRM = cadenaDRM + Mid(DRM, i, 1)
        Next
        Return cadenaDRM
    End Function

End Class
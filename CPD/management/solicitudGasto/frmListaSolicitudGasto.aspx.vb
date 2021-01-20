Imports CES
Imports CRN.nspSolicitudGasto
Public Class frmListaSolicitudGasto
    Inherits nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            llenaLista()
        End If
    End Sub
    Protected Sub llenaLista()
        Dim idx As String = Request.QueryString("idOficio")
        Dim llenado = New Proceso_ObtenerSolicitudGastos With {.tipoConsulta = nspSolicitudGasto.tipoConsultaSolicitudGasto.idOficio, .idOficio = Guid.Parse(idx)}.Ejecutar().OrderBy(Function(f) f.fechaElaboracion).ToList
        lsvPedidos.DataSource = llenado
        lsvPedidos.DataBind()
    End Sub
    Protected Sub lnkVer_Click(sender As Object, e As EventArgs)
        Dim clic As LinkButton = sender
        Dim id As Guid = Guid.Parse(clic.CommandName)
        Response.Redirect("~/management/solicitudGasto/frmSolicitudConsultar.aspx?id=" + id.ToString)
    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs)
        mandaDefault()
    End Sub

End Class
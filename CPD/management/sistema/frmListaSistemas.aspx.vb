Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPopup, CES.nspSistema
Imports CRN.nspSistema
Public Class frmListaSistemas : Inherits nspPaginaBase.PaginaBase
    Dim sistema As New nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            poblarLista()
        End If
    End Sub
    Private Sub poblarLista()
        Dim consulta = New Proceso_ObtenerSistemas() With {.tipoConsulta = tipoSistemaConsulta.todos}.Ejecutar()
        lvsSistemas.DataSource = consulta.ToList
        lvsSistemas.DataBind()
    End Sub

    Protected Sub btnEditar_Click(sender As Object, e As EventArgs)
        Try
            Dim btn As LinkButton = sender
            Dim indice As Integer = btn.TabIndex
            Dim id As Guid = Guid.Parse(btn.CommandArgument)
            Dim idString = id.ToString
            Dim miPagina = "frmAltaSistema.aspx?id=" & idString + "&band=edt"
            Response.Redirect(miPagina)
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub
    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs)
        Try
            Dim btn As LinkButton = sender
            Dim indice As Integer = btn.TabIndex
            Dim id As Guid = Guid.Parse(btn.CommandArgument)
            Dim idString = id.ToString
            Dim miPagina = "frmAltaSistema.aspx?id=" & idString + "&band=cns"
            Response.Redirect(miPagina)
        Catch ex As Exception
            OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        mandaDefault()
    End Sub
End Class
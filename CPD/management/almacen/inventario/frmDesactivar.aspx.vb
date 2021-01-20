Imports CRN.nspArticulo
Imports CES.nspArticulo
Imports CES.nspPopup
Imports System.Net.NetworkInformation
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Web.UI
Imports System.Globalization
Public Class frmDesactivar : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Dim sisActivo As New nspPaginaBase.PaginaBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                poblarLista()
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub
#Region "datos"
    Public Sub poblarLista()
        Try
            Dim sistema = sistemaActivo.tipo.ToString
            Dim articulos = New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.xExistencia, .existencia = "0", .tipoSistema = sistema}.Ejecutar().Where(Function(a) a.esActivo = True).ToList
            lsvListado.DataSource = articulos
            lsvListado.DataBind()
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", "" & ex.Message.ToString, tipoPopup.Naranja, False, "")

        End Try
    End Sub


#End Region
#Region "botones"
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        mandaDefault()
    End Sub

    Protected Sub lnkDesactivar_Click(sender As Object, e As EventArgs)

        Dim btnEliminar As LinkButton = sender
        Dim id As Guid = Guid.Parse(btnEliminar.CommandArgument)
        idEliminar.Value = id.ToString()
        Dim sb As StringBuilder = New StringBuilder
        btnEventoConfirmar.CommandArgument = btnEliminar.CommandArgument
        btnEventoConfirmar.TabIndex = btnEliminar.TabIndex
        lblConfirmacionCuerpo.Text = "<div style='text-align: center'> <i class='icon fa fa-question-circle animated infinite wobble c-red fa-3x'></i></div><br /><div style='text-align: center'> Está seguro que desea eliminar el artículo seleccionado?</div>"
        sb.Append("<script> $('#myModalConfirm').modal('show');</script>")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", sb.ToString, False)
    End Sub

    Protected Sub btnEventoConfirmar_Click(sender As Object, e As EventArgs)
        Try
            Dim id As Guid = Guid.Parse(idEliminar.Value)
            Dim respuesta = New Proceso_EliminarArticulo() With {.id = id, .ipUsuario = direccionIP, .idUsuarioMovimiento = IdUsuario, .idSistema = sistemaActivo.idSistema, .esActivo = False}.Ejecutar()
            Select Case respuesta.respuesta
                Case tipoRespuestaDelProceso.Completado
                    updateArticulo.Update()
                    OnMostrarMensajeAccion("Confirmación", controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._Tus_datos_se_eliminaron, "Desactivar inventario"), tipoPopup.Verde, True, "management/almacen/inventario/frmDesactivar.aspx?band=elim")
                Case tipoRespuestaDelProceso.Advertencia
                    OnMostrarMensajeAccion("Advertencia", respuesta.comentario, tipoPopup.Naranja, False, "")
                Case tipoRespuestaDelProceso.NoCompletado
                    OnMostrarMensajeAccion("Crítico", respuesta.comentario, tipoPopup.Rojo, False, "")
            End Select
        Catch ex As Exception
            OnMostrarMensajeAccion("Advertencia", "" & ex.Message.ToString, tipoPopup.Naranja, False, "")
        End Try
    End Sub

#End Region
End Class
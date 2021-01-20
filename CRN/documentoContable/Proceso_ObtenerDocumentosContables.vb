Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspDocumentoContable
Namespace nspDocumentoContable
    Public Class Proceso_ObtenerDocumentosContables : Inherits Accion(Of List(Of documentoContable))

        Public Property tipoConsulta As tipoConsultaDocumentoContable
        Public Property id As Guid
        Public Property esActivo As Boolean?
        Public Sub New()
            MyBase.New("Proceso_ObtenerDocumentoContable", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of documentoContable)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaDocumentoContable.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaDocumentoContable.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                Case tipoConsultaDocumentoContable.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaDocumentoContable.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspDocumentoContable.daoDocumentoContable)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace


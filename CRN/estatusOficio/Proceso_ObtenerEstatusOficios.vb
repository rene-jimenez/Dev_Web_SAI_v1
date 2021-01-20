Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspEstatusOficio
Namespace nspEstatusOficio
    Public Class Proceso_ObtenerEstatusOficios : Inherits Accion(Of List(Of estatusOficio))
        Public Property tipoConsulta As tipoConsultaEstatusOficio
        Public Property id As Guid
        Public Property esActivo As Boolean?
        Public Sub New()
            MyBase.New("Proceso_ObtenerEstatusOficio", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of estatusOficio)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaEstatusOficio.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaEstatusOficio.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                Case tipoConsultaEstatusOficio.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaEstatusOficio.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspEstatusOfcio.daoEstatusOficio)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace


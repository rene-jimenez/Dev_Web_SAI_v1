Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspResponsable
Namespace nspResponsable
    Public Class Proceso_ObtenerResponsables : Inherits Accion(Of List(Of responsable))
        Public Property tipoConsulta As tipoConsultaResponsable
        Public Property id As Guid

        Public Property esActivo As Boolean?
        Public Sub New()
            MyBase.New("Proceso_ObtenerResponsables", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of responsable)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaResponsable.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaResponsable.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                Case tipoConsultaResponsable.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaResponsable.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspResponsable.daoResponsable)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace


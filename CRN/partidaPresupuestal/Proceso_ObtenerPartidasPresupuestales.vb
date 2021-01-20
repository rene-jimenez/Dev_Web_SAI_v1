Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPartidaPresupuestal
Namespace nspPartidaPresupuestal
    Public Class Proceso_ObtenerPartidasPresupuestales : Inherits Accion(Of List(Of partidaPresupuestal))
        Public Property tipoConsulta As tipoConsultaPartidaPresupuestal
        Public Property id As Guid
        Public Property esActivo As Boolean?


        Public Sub New()
            MyBase.New("Proceso_ObtenerPartidasPresupuestales", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of partidaPresupuestal)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaPartidaPresupuestal.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaPartidaPresupuestal.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                Case tipoConsultaPartidaPresupuestal.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaPartidaPresupuestal.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPartidaPresupuestal.daoPartidaPresupuestal)().ObtenerConjunto(New Predicado("", parametros.ToArray()))


        End Function
    End Class
End Namespace


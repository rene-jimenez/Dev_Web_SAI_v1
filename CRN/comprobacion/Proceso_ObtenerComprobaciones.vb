Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspComprobacion
Namespace nspComprobacion
    Public Class Proceso_ObtenerComprobaciones : Inherits Accion(Of List(Of comprobacion))
        Public Property tipoConsulta As tipoConsultaComprobacion
        Public Property id As Guid
        Public Property idOficio As Guid?
        Public Property esDevolucion As Boolean?

        Public Sub New()
            MyBase.New("Proceso_ObtenerComprobacion", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of comprobacion)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaComprobacion.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaComprobacion.idOficio
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                Case tipoConsultaComprobacion.esDevolucion
                    parametros.Add(New ParametroPredicado("esDevolucion", esDevolucion))
                Case tipoConsultaComprobacion.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspComprobacion.daoComprobacion)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace


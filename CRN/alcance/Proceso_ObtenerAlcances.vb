Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspAlcance
Namespace nspAlcance
    Public Class Proceso_ObtenerAlcances : Inherits Accion(Of IList(Of alcance))
        Public Property tipoConsulta As tipoConsultaAlcance
        Public Property id As Guid
        Public Property idSolicitud As Guid
        Public Property idOficio As Guid

        Public Sub New()
            MyBase.New("Proceso_ObtenerAreas", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As IList(Of alcance)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaAlcance.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaAlcance.idSolicitud
                    parametros.Add(New ParametroPredicado("idSolicitud", idSolicitud))
                Case tipoConsultaAlcance.idOficio
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                Case tipoConsultaAlcance.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspAlcance.daoAlcance)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class

End Namespace


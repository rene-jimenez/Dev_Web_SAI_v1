
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspSolicitudGasto
Namespace nspSolicitudGasto
    Public Class Proceso_ObtenerSolicitudGastos : Inherits Accion(Of List(Of solicitudGasto))
        Public Property tipoConsulta As tipoConsultaSolicitudGasto
        Public Property id As Guid?
        Public Property idOficio As Guid?
        Public Property idSistema As Guid?
        Public Sub New()
            MyBase.New("Proceso_ObtenerListaEntidad", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of solicitudGasto)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaSolicitudGasto.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaSolicitudGasto.idOficio
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                Case tipoConsultaSolicitudGasto.todos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspSolicitudGasto.daoSolicitudGasto)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace

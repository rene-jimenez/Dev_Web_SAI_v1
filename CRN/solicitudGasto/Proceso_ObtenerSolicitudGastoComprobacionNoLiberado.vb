Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspSolicitudGastoComprobacionNoLiberado
Namespace nspSolicitudGastoComprobacionNoLiberado
    Public Class Proceso_ObtenerSolicitudGastoComprobacionNoLiberado : Inherits Accion(Of List(Of solicitudGastoComprobacionNoLiberado))

        Public Property fechaInicial As Date?
        Public Property fechaFinal As Date?
        Public Property idSistema As Guid?

        Public Sub New()
            MyBase.New("Proceso_ObtenerSolicitudGastoComprobacionNoLiberado", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of solicitudGastoComprobacionNoLiberado)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
            parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
            parametros.Add(New ParametroPredicado("idSistema", idSistema))

            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspSolicitudGastoComprobacionNoLiberado.daoSolicitudGastoComprobarNoLiberado)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace

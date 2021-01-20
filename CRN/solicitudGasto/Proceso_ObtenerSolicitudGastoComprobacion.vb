Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspSolicitudGastoComprobacion
Namespace nspSolicitudGastoComprobacion
    Public Class Proceso_ObtenerSolicitudGastoComprobacion : Inherits Accion(Of List(Of solicitudGastoComprobacion))
        Public Property accion As Integer
        Public Property fechaInicial As Date?
        Public Property fechaFinal As Date?
        Public Property idSistema As Guid?

        Public Sub New()
            MyBase.New("Proceso_ObtenerSolicitudGastoComprobacionoliberado", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of solicitudGastoComprobacion)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("Accion", accion))
            parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
            parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
            parametros.Add(New ParametroPredicado("idSistema", idSistema))
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspSolicitudGastoComprobacion.daoSolicitudGastoComprobar)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace

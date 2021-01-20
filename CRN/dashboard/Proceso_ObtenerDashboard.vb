Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspDashboard
Namespace nspDashboard
    Public Class Proceso_ObtenerDashboard : Inherits Accion(Of List(Of dashboard))

        Public Property idSistema As Guid?

        Public Sub New()
            MyBase.New("Proceso_ObtenerDashboard", "Obtiene datos para el Dashboard")
        End Sub


        Protected Overrides Function OnEjecutar() As List(Of dashboard)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("idSistema", idSistema))

            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspDashboard.daoDashboard)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function

    End Class
End Namespace
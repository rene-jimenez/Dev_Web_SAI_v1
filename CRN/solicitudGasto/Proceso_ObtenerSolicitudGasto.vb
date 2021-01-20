Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspSolicitudGasto
Namespace nspSolicitudGasto
    Public Class Proceso_ObtenerSolicitudGasto : Inherits Accion(Of solicitudGasto)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerSolicitudGasto", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As solicitudGasto
            Return New Proceso_ObtenerSolicitudGastos() With {.tipoConsulta = tipoConsultaSolicitudGasto.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class

End Namespace



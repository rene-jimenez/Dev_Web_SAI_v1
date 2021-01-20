Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPartidaPresupuestal
Namespace nspPartidaPresupuestal
    Public Class Proceso_ObtenerPartidaPresupuestal : Inherits Accion(Of partidaPresupuestal)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnaPartidaPresupuestal", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As partidaPresupuestal
            Return New Proceso_ObtenerPartidasPresupuestales() With {.tipoConsulta = tipoConsultaPartidaPresupuestal.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


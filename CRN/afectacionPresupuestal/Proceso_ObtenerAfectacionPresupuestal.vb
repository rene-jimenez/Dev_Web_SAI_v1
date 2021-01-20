Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspAfectacionPresupuestal
Namespace nspAfectacionPresupuestal
    Public Class Proceso_ObtenerAfectacionPresupuestal : Inherits Accion(Of afectacionPresupuestal)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerAfectacionPresupuestal", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As afectacionPresupuestal
            Return New Proceso_ObtenerAfectacionPresupuestales() With {.tipoConsulta = tipoConsultaAfectacionPresupuestal.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace
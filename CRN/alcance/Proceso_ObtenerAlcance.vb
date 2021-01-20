Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspAlcance
Namespace nspAlcance
    Public Class Proceso_ObtenerAlcance : Inherits Accion(Of alcance)

        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnaAlcance", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As alcance
            Return New Proceso_ObtenerAlcances() With {.tipoConsulta = tipoConsultaAlcance.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


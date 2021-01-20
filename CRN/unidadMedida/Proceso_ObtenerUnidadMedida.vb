Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspUnidadMedida
Namespace nspUnidadMedida
    Public Class Proceso_ObtenerUnidadMedida : Inherits Accion(Of unidadMedida)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnaUnidadMedida", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As unidadMedida
            Return New Proceso_ObtenerUnidadesMedida() With {.tipoConsulta = tipoConsultaUnidadMedida.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


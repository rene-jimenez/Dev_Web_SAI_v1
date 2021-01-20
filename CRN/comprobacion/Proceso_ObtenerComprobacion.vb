Imports Contexto.Accion.Accion
Imports CES.nspComprobacion

Namespace nspComprobacion
    Public Class Proceso_ObtenerComprobacion : Inherits Accion(Of comprobacion)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnaComprobacion", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As comprobacion
            Return New Proceso_ObtenerComprobaciones() With {.tipoConsulta = tipoConsultaComprobacion.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


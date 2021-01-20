Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspCategoria
Namespace nspCategoria
    Public Class Proceso_ObtenerCategoria : Inherits Accion(Of categoria)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnaCategoria", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As categoria
            Return New Proceso_ObtenerCategorias() With {.tipoConsulta = tipoConsultaCategoria.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


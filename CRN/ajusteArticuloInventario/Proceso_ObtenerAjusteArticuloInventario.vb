Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspAjusteArticuloInventario
Namespace nspAjusteArticuloInventario
    Public Class Proceso_ObtenerAjusteArticuloInventario : Inherits Accion(Of ajusteArticuloInventario)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerAjusteArticuloInventario", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As ajusteArticuloInventario
            Return New Proceso_ObtenerAjustesArticuloInventario() With {.tipoConsulta = tipoConsultaAjusteArticuloInventario.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class

End Namespace




Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspSistema
Namespace nspSistema
    Public Class Proceso_ObtenerSistema : Inherits Accion(Of sistema)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerSistema", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As sistema
            Return New Proceso_ObtenerSistemas() With {.tipoConsulta = tipoSistemaConsulta.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class

End Namespace

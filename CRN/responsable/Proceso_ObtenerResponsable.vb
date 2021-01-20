Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspResponsable
Namespace nspResponsable
    Public Class Proceso_ObtenerResponsable : Inherits Accion(Of responsable)

        Public tipoConsulta As tipoConsultaResponsable
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnResponsable", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As responsable
            Return New Proceso_ObtenerResponsables() With {.tipoConsulta = tipoConsultaResponsable.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


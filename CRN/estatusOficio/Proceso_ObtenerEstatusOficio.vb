Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspEstatusOficio
Namespace nspEstatusOficio
    Public Class Proceso_ObtenerEstatusOficio : Inherits Accion(Of estatusOficio)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnaEstatusOficio", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As estatusOficio
            Return New Proceso_ObtenerEstatusOficios() With {.tipoConsulta = tipoConsultaEstatusOficio.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


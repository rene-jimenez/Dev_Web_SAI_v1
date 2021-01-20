Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspDocumentoContable
Namespace nspDocumentoContable
    Public Class Proceso_ObtenerDocumentoContable : Inherits Accion(Of documentoContable)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnaDocumentoContable", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As documentoContable
            Return New Proceso_ObtenerDocumentosContables() With {.tipoConsulta = tipoConsultaDocumentoContable.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


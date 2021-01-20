Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspFirma
Namespace nspFirma
    Public Class Proceso_ObtenerFirma : Inherits Accion(Of firma)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnaFirma", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As firma
            Return New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


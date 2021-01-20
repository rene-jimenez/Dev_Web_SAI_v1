Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspTipoPago
Namespace nspTipoPago
    Public Class Proceso_ObtenerTipoPago : Inherits Accion(Of tipoPago)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnTipoPago", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As tipoPago
            Return New Proceso_ObtenerTiposPagos() With {.tipoConsulta = tipoConsultaTipoPago.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace


Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspSolicitudGasto
Namespace nspSolicitudGasto
    Public Class Proceso_CancelarSolicitudGasto : Inherits Accion(Of respuestaDelProceso)
        Public Property id As Guid?
        Public Property esCancelado As Boolean
        Public Property FechaCancelacion As Date?
        Public Property responsableCancelacion As String
        Public Property observacionCancelacion As String
        Public Property idUsuarioMovimiento As Guid?
        Public Property ipUsuario As String

        Public Sub New()
            MyBase.New("Proceso_CancelarSolicitudGasto", "Actualiza un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspSolicitudGasto.daoSolicitudGasto)()
                Dim solicitudCancelar = New Proceso_ObtenerSolicitudGasto() With {.id = id}.Ejecutar
                solicitudCancelar.esCancelado = esCancelado
                solicitudCancelar.fechaCancelacion = FechaCancelacion
                solicitudCancelar.responsableCancelacion = responsableCancelacion
                solicitudCancelar.observacionCancelacion = observacionCancelacion
                solicitudCancelar.ipUsuario = ipUsuario
                solicitudCancelar.idUsuarioMovimiento = idUsuarioMovimiento
                solicitudCancelar._tipoEdicion = tipoEdicionSolicitudGasto.Cancelar
                dao.Actualizar(solicitudCancelar)
                respuesta.comentario = "ok"
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = "No puede duplicar la información en la base de datos."
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                Else
                    respuesta.comentario = ex.Message.ToString
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respuesta.comentario = "Ocurrió un error al agregar un registro en la base de datos"
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
        Private Function validar() As respuestaDelProceso
            Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                If id Is Nothing Then
                    Throw New Exception("El id es campo obligatorio")
                End If
                If FechaCancelacion.ToString.Length Then
                    Throw New Exception("El FechaCancelacion es campo obligatorio")
                End If
                If responsableCancelacion.Length = 0 Then
                    Throw New Exception("El responsableCancelacion movimiento es campo obligatorio")
                End If
                If observacionCancelacion.Length = 0 Then
                    Throw New Exception("El observacionCancelacion movimiento es campo obligatorio")
                End If
                If idUsuarioMovimiento Is Nothing Then
                    Throw New Exception("El idUsuarioMovimiento es campo obligatorio")
                End If
                If ipUsuario.Length = 0 Then
                    Throw New Exception("Ip usuario es campo obligatorio")
                End If
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch ex As Exception
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = ex.Message.ToString
            End Try
            Return respuesta
        End Function
    End Class
End Namespace
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspSolicitudGasto
Namespace nspSolicitudGasto
    Public Class Proceso_ActualizarSolicitudGasto : Inherits Accion(Of respuestaDelProceso)
        Public Property id As Guid?
        Public Property folioCaja As String
        Public Property folioTesoreria As String
        Public Property fechaRecepcion As Date?
        Public Property idUsuarioMovimiento As Guid?
        Public Property ipUsuario As String

        Public Sub New()
            MyBase.New("Proceso_ActualizarSolicitudGasto", "Actualiza un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspSolicitudGasto.daoSolicitudGasto)()
                Dim solicitudActualizar = New Proceso_ObtenerSolicitudGasto() With {.id = id}.Ejecutar
                solicitudActualizar.folioCaja = folioCaja
                solicitudActualizar.folioTesoreria = folioTesoreria
                solicitudActualizar.fechaRecepcion = fechaRecepcion
                solicitudActualizar.ipUsuario = ipUsuario
                solicitudActualizar.idUsuarioMovimiento = idUsuarioMovimiento
                solicitudActualizar._tipoEdicion = tipoEdicionSolicitudGasto.Actualizar
                dao.Actualizar(solicitudActualizar)
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
                If folioCaja.Length = 0 Then
                    Throw New Exception("El folioCaja es campo obligatorio")
                End If
                If folioTesoreria.Length = 0 Then
                    Throw New Exception("El folioTesoreria movimiento es campo obligatorio")
                End If
                If fechaRecepcion.ToString.Length Then
                    Throw New Exception("El fechaRecepcion es campo obligatorio")
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



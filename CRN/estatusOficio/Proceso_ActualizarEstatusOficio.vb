﻿Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspEstatusOficio
Namespace nspEstatusOficio
    Public Class Proceso_ActualizarEstatusOficio : Inherits Accion(Of respuestaDelProceso)
        Public Property entidad As estatusOficio
        Dim controladorMensajes As New notificacionesDeUsuario
        Public Sub New()
            MyBase.New("Proceso_ActualizarEstatusOficio", "Actualiza un registro a la base de datos")
        End Sub

        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try

                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspEstatusOfcio.daoEstatusOficio)()
                entidad._tipoEdicion = tipoEdicionEstatusOficio.editar
                dao.Actualizar(entidad)
                respuesta.comentario = "ok"
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_registro_estaria_duplicado, "categoria")
                    respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                Else
                    respuesta.comentario = ex.Message.ToString
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respuesta.comentario = ex.Message.ToString
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
    End Class
End Namespace

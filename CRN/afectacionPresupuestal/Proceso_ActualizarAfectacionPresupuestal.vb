Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports Contexto.Notificaciones
Imports System.Data.SqlClient
Imports CES.nspAfectacionPresupuestal
Imports System.Transactions
Namespace nspAfectacionPresupuestal
    Public Class Proceso_ActualizarAfectacionPresupuestal : Inherits Accion(Of respuestaDelProceso)
        Public Property entidad As afectacionPresupuestal
        Dim controladorMensajes As New notificacionesDeUsuario

        Public Sub New()
            MyBase.New("Proceso_ActualizarAfectacionPresupuestal", "Actualiza un registro a la base de datos")
        End Sub

        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Using scope As New TransactionScope()

                    Dim resultadoValidación = validar()
                    If resultadoValidación.respuesta = tipoRespuestaDelProceso.Completado Then

                        Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspAfectacionPresupuestal.daoAfectacionPresupuestal)()
                        dao.Actualizar(entidad)
                        respuesta.comentario = "ok"
                        respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    Else
                        Throw New Exception(resultadoValidación.comentario)
                    End If
                    scope.Complete()
                End Using
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = "El registro estaría duplicado"
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
        Private Function validar() As respuestaDelProceso
            Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try

                If entidad.concepto Is Nothing Then
                    Throw New Exception("El concepto es campo obligatorio")
                End If
                If entidad.marcaAgua Is Nothing Then
                    Throw New Exception("La marca de Agua es campo obligatorio")
                End If
                If entidad.idSolicita Is Nothing Then
                    Throw New Exception("La persona que solicita es campo obligatorio")
                End If
                If entidad.idAutoriza Is Nothing Then
                    Throw New Exception("La persona que Autoriza es campo obligatorio")
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


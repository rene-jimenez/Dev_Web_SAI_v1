Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspFirma
Namespace nspFirma
    Public Class Proceso_EliminarFirma : Inherits Accion(Of respuestaDelProceso)
        Public Property id As Guid
        Public Property idUsuarioMovimiento As Guid
        Public Property ipUsuario As String
        Public Property esActivo As Boolean
        Public Property idSistema As Guid
        Public Sub New()
            MyBase.New("Proceso_EliminarFirma", "Elimina un registro en la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso

            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Dim entidad = New Proceso_ObtenerFirma() With {.id = id}.Ejecutar()
            Try
                '// AQUI CHECAR POR QUE NO CARGA VARIABLES GLOBALES
                entidad.idUsuarioMovimiento = idUsuarioMovimiento
                entidad.ipUsuario = ipUsuario
                entidad.idSistema = idSistema
                'entidad.idUsuarioMovimiento = Guid.Parse("11111111-0000-0000-1111-111111111111")
                'entidad.ipUsuario = "::1"
                'entidad.idSistema = Guid.Parse("efd014a0-4091-4daa-80d6-0fa5f9deaa99")
                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspFirma.daoFirma)()
                dao.Eliminar(entidad)
                respuesta.comentario = "Eliminó"
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch exSQL As SqlException
                If exSQL.Number = 547 Then ' Cuando un registro ya fue ocupado, las siguientes lineas desactivan la entidad, solo cuando aplique.
                    Try
                        Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspFirma.daoFirma)()
                        entidad.esActivo = esActivo
                        dao.Actualizar(entidad)
                        respuesta.comentario = "Desactivó"
                        respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    Catch ex2 As Exception
                        respuesta.comentario = "Ocurrió un error al eliminar el registro seleccionado."
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                    End Try
                Else
                    respuesta.comentario = exSQL.Message.ToString
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respuesta.comentario = "Ocurrió un error al eliminar el registro seleccionado."
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
    End Class
End Namespace


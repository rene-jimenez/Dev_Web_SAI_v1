Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspSolicitudGasto
Imports System.Transactions
Namespace nspSolicitudGasto
    Public Class Proceso_AgregarSolicitudGasto : Inherits Accion(Of respuestaDelProceso)
        Public Property idPartidaPresupuestal As Guid?
        Public Property importe As Double?
        Public Property concepto As String
        Public Property marcaAgua As String
        Public Property idSistema As Guid?
        Public Property idUsuarioMovimiento As Guid?
        Public Property ipUsuario As String
        Public Property idOficio As Guid?
        Public Property idArea As Guid?
        Public Property idCargoPresupuestal As Guid?
        Public Property idEstatusOficio As Guid?
        Public Sub New()
            MyBase.New("Proceso_AgregarSolicitudGasto", "Agrega un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Using scope As New TransactionScope

                    Dim resultadoValidación = validar()
                    If resultadoValidación.respuesta = tipoRespuestaDelProceso.Completado Then

                        Dim respuestaOficio = New CRN.nspOficio.Proceso_EditarOficio_EspecialPedidoSolicitud() With {.idOficio = idOficio, .idArea = idArea, .idCargoPresupuestal = idCargoPresupuestal, .idEstatusOficio = idEstatusOficio,
                                                                                               .idSistema = idSistema, .idUsuarioMovimiento = idUsuarioMovimiento, .ipUsuario = ipUsuario}.Ejecutar
                        If respuestaOficio.respuesta <> tipoRespuestaDelProceso.Completado Then
                            Throw New Exception(resultadoValidación.comentario)
                        End If


                        Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspSolicitudGasto.daoSolicitudGasto)()
                        Dim solicitudGasto As New solicitudGasto
                        solicitudGasto.idOficio = idOficio
                        solicitudGasto.idPartidaPresupuestal = idPartidaPresupuestal
                        solicitudGasto.importe = importe
                        solicitudGasto.concepto = concepto
                        solicitudGasto.marcaAgua = marcaAgua
                        solicitudGasto.idSistema = idSistema
                        solicitudGasto.idUsuarioMovimiento = idUsuarioMovimiento
                        solicitudGasto.ipUsuario = ipUsuario
                        dao.Agregar(solicitudGasto)
                        respuesta.comentario = "ok"
                        respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    Else
                        Throw New Exception(resultadoValidación.comentario)
                    End If
                    scope.Complete()
                End Using

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
                If idOficio Is Nothing Then
                    Throw New Exception("idOficio es campo obligatorio")
                End If
                If idArea Is Nothing Then
                    Throw New Exception("idArea es campo obligatorio")
                End If
                If idCargoPresupuestal Is Nothing Then
                    Throw New Exception("idCargoPresupuestal es campo obligatorio")
                End If
                If idEstatusOficio Is Nothing Then
                    Throw New Exception("idEstatusOficio es campo obligatorio")
                End If

                If idPartidaPresupuestal Is Nothing Then
                    Throw New Exception("El idPartidaPresupuestal es campo obligatorio")
                End If
                If idUsuarioMovimiento Is Nothing Then
                    Throw New Exception("El idUsuarioMovimiento es campo obligatorio")
                End If
                If idSistema Is Nothing Then
                    Throw New Exception("El idSistema es campo obligatorio")
                End If
                If importe.ToString.Length = 0 Then
                    Throw New Exception("El importe es campo obligatorio")
                End If
                If concepto.Length = 0 Then
                    Throw New Exception("El concepto es campo obligatorio")
                End If
                If marcaAgua.Length = 0 Then
                    Throw New Exception("El marcaAgua es campo obligatorio")
                End If
                If ipUsuario.Length = 0 Then
                    Throw New Exception("La ipUsuario es campo obligatorio")
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

Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspAlcance
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspAlcance
    Public Class daoAlcance : Inherits DaoSql(Of alcance)

        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As alcance)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarAlcance"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idPartida", entidad.idPartida)
                comando.Parameters.AddWithValue("@idSolicitud", entidad.idSolicitud)
                comando.Parameters.AddWithValue("@importe", entidad.importe)
                comando.Parameters.AddWithValue("@no", entidad.no)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As alcance)
            If (Not IsNothing(entidad)) Then
                Select Case entidad._tipoEdicion
                    Case tipoEdicionAlcance.editar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_EditarAlcance"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@idPartida", entidad.idPartida)
                        comando.Parameters.AddWithValue("@importe", entidad.importe)
                        comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                    Case tipoEdicionAlcance.Actualizar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_ActualizarAlcance"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@folioCaja", entidad.folioCaja)
                        comando.Parameters.AddWithValue("@folioTesoreria", entidad.folioTesoreria)
                        comando.Parameters.AddWithValue("@fechaRecepcion", entidad.fechaRecepcion)
                        comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                    Case tipoEdicionAlcance.Cancelar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_CancelarAlcance"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@esCancelado", entidad.esCancelado)
                        comando.Parameters.AddWithValue("@fechaCancelacion", entidad.fechaCancelacion)
                        comando.Parameters.AddWithValue("@responsableCancelacion", entidad.responsableCancelacion)
                        comando.Parameters.AddWithValue("@observacionCancelacion", entidad.observacionCancelacion)
                        comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)

                End Select
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaAlcance = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaAlcance)
            Select Case tipoConsulta
                Case tipoConsultaAlcance.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaAlcance.idSolicitud
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idSolicitud", predicado.Parametros("idSolicitud").Valor)
                Case tipoConsultaAlcance.idOficio
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@idOficio", predicado.Parametros("idOficio").Valor)
                Case tipoConsultaAlcance.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select

            comando.CommandText = "proAlm_ObtenerAlcance"
            comando.CommandType = CommandType.StoredProcedure

        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As alcance
            Dim alcance As New alcance
            alcance.id = Guid.Parse(lectorRenglonActual("id").ToString)
            If lectorRenglonActual("idSolicitud").ToString <> "" Then
                alcance.idSolicitud = Guid.Parse(lectorRenglonActual("idSolicitud").ToString)
            End If
            If lectorRenglonActual("idPartida").ToString <> "" Then
                alcance.idPartida = Guid.Parse(lectorRenglonActual("idPartida").ToString)
            End If
            If lectorRenglonActual("folioTesoreria").ToString <> "" Then
                alcance.folioTesoreria = lectorRenglonActual("folioTesoreria").ToString
            End If
            If lectorRenglonActual("folioCaja").ToString <> "" Then
                alcance.folioCaja = lectorRenglonActual("folioCaja").ToString
            End If
            If lectorRenglonActual("fechaRecepcion").ToString <> "" Then
                alcance.fechaRecepcion = CDate(lectorRenglonActual("fechaRecepcion").ToString)
            End If
            If lectorRenglonActual("fechaCaptura").ToString <> "" Then
                alcance.fechaCaptura = CDate(lectorRenglonActual("fechaCaptura").ToString)
            End If
            If lectorRenglonActual("importe").ToString <> "" Then
                alcance.importe = lectorRenglonActual("importe").ToString
            End If
            If lectorRenglonActual("no").ToString <> "" Then
                alcance.no = lectorRenglonActual("no").ToString
            End If
            If lectorRenglonActual("esCancelado").ToString <> "" Then
                alcance.esCancelado = lectorRenglonActual("esCancelado").ToString
            End If
            If lectorRenglonActual("fechaCancelacion").ToString <> "" Then
                alcance.fechaCancelacion = CDate(lectorRenglonActual("fechaCancelacion").ToString)
            End If
            If lectorRenglonActual("responsableCancelacion").ToString <> "" Then
                alcance.responsableCancelacion = lectorRenglonActual("responsableCancelacion").ToString
            End If
            If lectorRenglonActual("observacionCancelacion").ToString <> "" Then
                alcance.observacionCancelacion = lectorRenglonActual("observacionCancelacion").ToString
            End If
            If lectorRenglonActual("_folioCajaSolicitud").ToString <> "" Then
                alcance._folioCajaSolicitud = lectorRenglonActual("_folioCajaSolicitud").ToString
            End If
            If lectorRenglonActual("_folioTesoreriaSolicitud").ToString <> "" Then
                alcance._folioTesoreriaSolicitud = lectorRenglonActual("_folioTesoreriaSolicitud").ToString
            End If
            If lectorRenglonActual("_importeSolicitud").ToString <> "" Then
                alcance._importeSolicitud = lectorRenglonActual("_importeSolicitud").ToString
            End If
            If lectorRenglonActual("_idArea").ToString <> "" Then
                alcance._idArea = Guid.Parse(lectorRenglonActual("_idArea").ToString)
            End If
            If lectorRenglonActual("_Area").ToString <> "" Then
                alcance._Area = lectorRenglonActual("_Area").ToString
            End If
            If lectorRenglonActual("_turnoDrm").ToString <> "" Then
                alcance._turnoDrm = lectorRenglonActual("_turnoDrm").ToString
            End If
            If lectorRenglonActual("_turnoSaf").ToString <> "" Then
                alcance._turnoSaf = lectorRenglonActual("_turnoSaf").ToString
            End If
            If lectorRenglonActual("_cargoPresupuestal").ToString <> "" Then
                alcance._CargoPresupuestal = lectorRenglonActual("_cargoPresupuestal").ToString
            End If
            If lectorRenglonActual("_conceptoSolicitud").ToString <> "" Then
                alcance._conceptoSolicitud = lectorRenglonActual("_conceptoSolicitud").ToString
            End If
            If lectorRenglonActual("_fechaCapturaSolicitud").ToString <> "" Then
                alcance._fechaCapturaSolicitud = CDate(lectorRenglonActual("_fechaCapturaSolicitud").ToString)
            End If
            If lectorRenglonActual("_fechaRecepcionSolicitud").ToString <> "" Then
                alcance._fechaRecepcionSolicitud = CDate(lectorRenglonActual("_fechaRecepcionSolicitud").ToString)
            End If
            alcance.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            alcance.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            alcance.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)

            Return alcance
        End Function
    End Class
End Namespace
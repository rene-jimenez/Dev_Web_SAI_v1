Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspSolicitudGasto
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports System.Web.Mvc
Namespace nspSolicitudGasto
    Public Class daoSolicitudGasto : Inherits DaoSql(Of solicitudGasto)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotImplementedException()
            End If
            Dim tipoConsulta As tipoConsultaSolicitudGasto = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaSolicitudGasto)
            Select Case tipoConsulta
                Case tipoConsultaSolicitudGasto.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaSolicitudGasto.idOficio
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idOficio", predicado.Parametros("idOficio").Valor)
                Case tipoConsultaSolicitudGasto.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select
            comando.CommandText = "proAlm_ObtenerSolicitudGastos"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As solicitudGasto
            Dim solicitudGasto As New solicitudGasto
            solicitudGasto.id = Guid.Parse(lectorRenglonActual("id").ToString)
            solicitudGasto.idOficio = Guid.Parse(lectorRenglonActual("idOficio").ToString)
            solicitudGasto.idPartidaPresupuestal = Guid.Parse(lectorRenglonActual("idPartidaPresupuestal").ToString)
            solicitudGasto.importe = Decimal.Parse(lectorRenglonActual("importe").ToString)
            solicitudGasto.fechaElaboracion = CDate(lectorRenglonActual("fechaElaboracion").ToString)
            If lectorRenglonActual("folioCaja").ToString <> "" Then
                solicitudGasto.folioCaja = lectorRenglonActual("folioCaja").ToString
            End If
            If lectorRenglonActual("folioTesoreria").ToString <> "" Then
                solicitudGasto.folioTesoreria = lectorRenglonActual("folioTesoreria").ToString
            End If
            If lectorRenglonActual("fechaRecepcion").ToString <> "" Then
                solicitudGasto.fechaRecepcion = CDate(lectorRenglonActual("fechaRecepcion").ToString)
            End If
            solicitudGasto.concepto = lectorRenglonActual("concepto").ToString
            solicitudGasto.esCancelado = CBool(lectorRenglonActual("esCancelado").ToString)
            If lectorRenglonActual("fechaCancelacion").ToString <> "" Then
                solicitudGasto.fechaCancelacion = CDate(lectorRenglonActual("fechaCancelacion").ToString)
            End If

            solicitudGasto.responsableCancelacion = lectorRenglonActual("responsableCancelacion").ToString
            solicitudGasto.observacionCancelacion = lectorRenglonActual("observacionCancelacion").ToString
            solicitudGasto.marcaAgua = lectorRenglonActual("marcaAgua").ToString
            solicitudGasto._nombreArea = lectorRenglonActual("_nombreArea").ToString
            solicitudGasto._nombreCargoPresupuestal = lectorRenglonActual("_nombreCargoPresupuestal").ToString
            solicitudGasto._nombreEstatusOficio = lectorRenglonActual("_nombreEstatusOficio").ToString
            solicitudGasto._nombrePartidaPresupuestal = lectorRenglonActual("_nombrePartidaPresupuestal").ToString
            solicitudGasto._idArea = Guid.Parse(lectorRenglonActual("_idArea").ToString)
            If lectorRenglonActual("_idCargoPresupuestal").ToString <> "" Then
                solicitudGasto._idCargoPresupuestal = Guid.Parse(lectorRenglonActual("_idCargoPresupuestal").ToString)
            End If
            solicitudGasto._idEstatusOficio = Guid.Parse(lectorRenglonActual("_idEstatusOficio").ToString)
            solicitudGasto._esDocInterno = CBool(lectorRenglonActual("_esDocInterno").ToString)
            solicitudGasto._turnoDRM = lectorRenglonActual("_turnoDRM").ToString
            solicitudGasto._turnoSAF = lectorRenglonActual("_turnoSAF").ToString
            Return solicitudGasto
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As solicitudGasto)
            comando.CommandType = CommandType.StoredProcedure
            comando.CommandText = "proAlm_AgregarSolicitudGasto"
            comando.Parameters.AddWithValue("@idOficio", entidad.idOficio)
            comando.Parameters.AddWithValue("@idPartidaPresupuestal", entidad.idPartidaPresupuestal)
            comando.Parameters.AddWithValue("@importe", entidad.importe)
            comando.Parameters.AddWithValue("@concepto", entidad.concepto)
            comando.Parameters.AddWithValue("@marcaAgua", entidad.marcaAgua)
            comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
            comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
            comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As solicitudGasto)
            If (Not IsNothing(entidad)) Then
                Select Case entidad._tipoEdicion
                    Case tipoEdicionSolicitudGasto.Cancelar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_CancelarSolicitudGasto"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@esCancelado", entidad.esCancelado)
                        comando.Parameters.AddWithValue("@fechaCancelacion", entidad.fechaCancelacion)
                        comando.Parameters.AddWithValue("@responsableCancelacion", entidad.responsableCancelacion)
                        comando.Parameters.AddWithValue("@observacionCancelacion", entidad.observacionCancelacion)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                    Case tipoEdicionSolicitudGasto.Actualizar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_ActualizarSolicitudGasto"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@folioCaja", entidad.folioCaja)
                        comando.Parameters.AddWithValue("@folioTesoreria", entidad.folioTesoreria)
                        comando.Parameters.AddWithValue("@fechaRecepcion", entidad.fechaRecepcion)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                    Case tipoEdicionSolicitudGasto.editar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_EditarSolicitudGasto"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@idPartidaPresupuestal", entidad.idPartidaPresupuestal)
                        comando.Parameters.AddWithValue("@importe", entidad.importe)
                        comando.Parameters.AddWithValue("@concepto", entidad.concepto)
                        comando.Parameters.AddWithValue("@marcaAgua", entidad.marcaAgua)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                End Select
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class

End Namespace


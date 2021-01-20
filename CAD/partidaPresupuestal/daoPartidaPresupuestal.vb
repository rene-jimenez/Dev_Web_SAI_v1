Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspPartidaPresupuestal
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspPartidaPresupuestal
    Public Class daoPartidaPresupuestal : Inherits DaoSql(Of partidaPresupuestal)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaPartidaPresupuestal = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaPartidaPresupuestal)
            Select Case tipoConsulta
                Case tipoConsultaPartidaPresupuestal.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaPartidaPresupuestal.nombre
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@nombre", predicado.Parametros("nombre").Valor)
                Case tipoConsultaPartidaPresupuestal.esActivo
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultaPartidaPresupuestal.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select

            comando.CommandText = "proAlm_ObtenerPartidaPresupuestal"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As partidaPresupuestal
            Dim partidaPresupuestal As New partidaPresupuestal
            partidaPresupuestal.id = Guid.Parse(lectorRenglonActual("id").ToString)
            partidaPresupuestal.nombre = lectorRenglonActual("nombre").ToString
            partidaPresupuestal.esActivo = CBool(lectorRenglonActual("esActivo").ToString)
            partidaPresupuestal.numero = lectorRenglonActual("numero").ToString
            partidaPresupuestal.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            partidaPresupuestal.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            Return partidaPresupuestal
        End Function

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As partidaPresupuestal)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarPartidaPresupuestal"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                comando.Parameters.AddWithValue("@numero", entidad.numero)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As partidaPresupuestal)
            If (Not IsNothing(entidad)) Then
                Select Case entidad._tipoEdicion
                    Case tipoEdicionPartidaPresupuestal.cambiarEstado
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_CambiarEstadoPartidaPresupuestal"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                    Case tipoEdicionPartidaPresupuestal.editar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_EditarPartidaPresupuestal"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                        comando.Parameters.AddWithValue("@numero", entidad.numero)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                End Select
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As partidaPresupuestal)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarPartidaPresupuestal"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace


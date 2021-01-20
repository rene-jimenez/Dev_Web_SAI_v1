Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspSistema
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspSistema
    Public Class daoSistema : Inherits DaoSql(Of sistema)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoSistemaConsulta = CType(predicado.Parametros("tipoConsulta").Valor, tipoSistemaConsulta)
            Select Case tipoConsulta
                Case tipoSistemaConsulta.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoSistemaConsulta.tipo
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@tipo", predicado.Parametros("tipo").Valor)
                Case tipoSistemaConsulta.año
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@año", predicado.Parametros("año").Valor)
                Case tipoSistemaConsulta.Todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select

            comando.CommandText = "proAlm_ObtenerSistema"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As sistema
            Dim sistema As New sistema
            sistema.id = Guid.Parse(lectorRenglonActual("id").ToString)
            sistema.nombre = lectorRenglonActual("nombre").ToString
            sistema.tipo = lectorRenglonActual("tipo").ToString
            sistema.año = lectorRenglonActual("año").ToString
            sistema.logo = lectorRenglonActual("logo").ToString
            sistema.fondo = lectorRenglonActual("fondo").ToString
            sistema.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            sistema.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)

            Return sistema
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As sistema)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarSistema"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                comando.Parameters.AddWithValue("@tipo", entidad.tipo)
                comando.Parameters.AddWithValue("@año", entidad.año)
                comando.Parameters.AddWithValue("@logo", entidad.logo)
                comando.Parameters.AddWithValue("@fondo", entidad.fondo)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)

            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As sistema)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarSistema"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                comando.Parameters.AddWithValue("@tipo", entidad.tipo)
                comando.Parameters.AddWithValue("@año", entidad.año)
                comando.Parameters.AddWithValue("@logo", entidad.logo)
                If Not entidad.fondo Is Nothing Then
                    comando.Parameters.AddWithValue("@fondo", entidad.fondo)
                End If
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)

            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As sistema)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarSistema"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)

            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace


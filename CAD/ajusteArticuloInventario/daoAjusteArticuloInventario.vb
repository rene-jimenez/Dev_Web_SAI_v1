Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspAjusteArticuloInventario
Namespace nspAjusteArticuloInventario
    Public Class daoAjusteArticuloInventario : Inherits DaoSql(Of ajusteArticuloInventario)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Contexto.Entidades.Persistencia.Relacional.Daos.Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaAjusteArticuloInventario = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaAjusteArticuloInventario)
            Select Case tipoConsulta
                Case tipoConsultaAjusteArticuloInventario.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaAjusteArticuloInventario.idArticulo
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idArticulo", predicado.Parametros("idArticulo").Valor)
                Case tipoConsultaAjusteArticuloInventario.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select

            comando.CommandText = "proAlm_ObtenerAjusteArticuloInventario"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As ajusteArticuloInventario
            Dim ajusteArticuloInventario As New ajusteArticuloInventario
            ajusteArticuloInventario.id = Guid.Parse(lectorRenglonActual("id").ToString)
            ajusteArticuloInventario.idArticulo = Guid.Parse(lectorRenglonActual("idArticulo").ToString)
            ajusteArticuloInventario.tipoOperacion = lectorRenglonActual("tipoOperacion").ToString
            ajusteArticuloInventario.explicacion = lectorRenglonActual("explicacion").ToString
            ajusteArticuloInventario.fecha = CDate(lectorRenglonActual("fecha").ToString)
            ajusteArticuloInventario.cantidad = CInt(lectorRenglonActual("cantidad").ToString)
            ajusteArticuloInventario.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            ajusteArticuloInventario.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            ajusteArticuloInventario.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)

            Return ajusteArticuloInventario
        End Function

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As ajusteArticuloInventario)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarAjusteArticuloInventario"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idArticulo", entidad.idArticulo)
                comando.Parameters.AddWithValue("@tipoOperacion", entidad.tipoOperacion)
                comando.Parameters.AddWithValue("@explicacion", entidad.explicacion)
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

    End Class
End Namespace



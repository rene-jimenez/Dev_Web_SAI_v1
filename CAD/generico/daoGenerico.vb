Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspGenerico
Namespace nspGenerico
    Public Class daoGenerico : Inherits DaoSql(Of generico)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Contexto.Entidades.Persistencia.Relacional.Daos.Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaGenerico = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaGenerico)
            Select Case tipoConsulta
                Case tipoConsultaGenerico.ultimoNumeroPedido
                    comando.CommandText = "proAlm_ObtenerUltimoNumeroPedido"
                Case tipoConsultaGenerico.ultimoTurnoDRM_Especial_SP
                    comando.CommandText = "proAlm_ObtenerUltimoTurno_SP"
                Case tipoConsultaGenerico.ultimoTurnoDRM_Especial_ST
                    comando.CommandText = "proAlm_ObtenerUltimoTurno_ST"
                Case tipoConsultaGenerico.turnoDRM_Duplicado
                    comando.CommandText = "proAlm_ObtenerTurnoDRM_Duplicado"
                    comando.Parameters.AddWithValue("@turnoDRM", predicado.Parametros("turnoDRM").Valor)
                Case tipoConsultaGenerico.ultimoNumValeSalida_x_Area
                    comando.CommandText = "proAlm_ObtenerUltimoNumValeSalida"
                    comando.Parameters.AddWithValue("@idArea", predicado.Parametros("idArea").Valor)

            End Select
            comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As generico
            Return New generico() With {.valor = lectorRenglonActual("valor").ToString}
        End Function

    End Class
End Namespace


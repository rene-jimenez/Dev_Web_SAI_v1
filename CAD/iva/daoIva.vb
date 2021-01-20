Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspIva
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspIva
    Public Class daoIva : Inherits DaoSql(Of iva)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("fecha")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro fecha para hacer consulta.")
            End If       
            comando.Parameters.AddWithValue("@fecha", predicado.Parametros("fecha").Valor)
            comando.CommandText = "proAlm_ObtenerIva"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As iva
            Dim iva As New iva
            iva.valor = lectorRenglonActual("valor").ToString
            Return iva
        End Function
    End Class
End Namespace


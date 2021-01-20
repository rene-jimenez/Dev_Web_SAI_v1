Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports CES.nspDashboard
Namespace nspDashboard
    Public Class daoDashboard : Inherits DaoSql(Of dashboard)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)

            comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)

            comando.CommandText = "[proAlm_ObtenerDashboard]"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As dashboard

            Dim dashboard As New dashboard

            dashboard.oficiosEnproceso = lectorRenglonActual("oficiosEnProceso").ToString
            dashboard.oficiosAtendidos = lectorRenglonActual("oficiosAtendidos").ToString
            dashboard.solGastoNoLiberados = lectorRenglonActual("solGastoNoLiberados").ToString
            dashboard.solGastoLiberadosPendientes = lectorRenglonActual("solGastoLiberadosPendientes").ToString
            dashboard.solGastoComprobados = lectorRenglonActual("solGastoComprobados").ToString
            dashboard.pedidoPendientesEntrega = lectorRenglonActual("pedidoPendientesEntrega").ToString
            dashboard.pedidosEntregados = lectorRenglonActual("pedidosEntregados").ToString
            dashboard.pedidoSinAfectacion = lectorRenglonActual("pedidoSinAfectacion").ToString
            Return dashboard
        End Function
    End Class
End Namespace
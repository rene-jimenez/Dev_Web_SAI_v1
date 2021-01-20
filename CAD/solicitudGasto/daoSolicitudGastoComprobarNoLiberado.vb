Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.sql
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports CES.nspSolicitudGastoComprobacionNoLiberado
Namespace nspSolicitudGastoComprobacionNoLiberado
    Public Class daoSolicitudGastoComprobarNoLiberado : Inherits DaoSql(Of solicitudGastoComprobacionNoLiberado)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)

            comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
            comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
            comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)

            comando.CommandText = "[proAlm_ObtenerSolicitudGastoComprobarNoLiberados]"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As solicitudGastoComprobacionNoLiberado

            Dim solicitudGastoComprobacionNoLiberado As New solicitudGastoComprobacionNoLiberado

            solicitudGastoComprobacionNoLiberado.fechadeCaptura = lectorRenglonActual("fechadeCaptura").ToString
            solicitudGastoComprobacionNoLiberado.turnoSAF = lectorRenglonActual("turnoSAF").ToString
            solicitudGastoComprobacionNoLiberado.turnoDRM = lectorRenglonActual("turnoDRM").ToString
            solicitudGastoComprobacionNoLiberado.importeSolicitud = lectorRenglonActual("importeSolicitud").ToString
            solicitudGastoComprobacionNoLiberado.concepto = lectorRenglonActual("concepto").ToString
            solicitudGastoComprobacionNoLiberado.diasTranscurridos = lectorRenglonActual("diasTranscurridos").ToString
            Return solicitudGastoComprobacionNoLiberado
        End Function

    End Class
End Namespace

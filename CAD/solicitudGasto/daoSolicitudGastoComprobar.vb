Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports CES.nspSolicitudGastoComprobacion
Namespace nspSolicitudGastoComprobacion
    Public Class daoSolicitudGastoComprobar : Inherits DaoSql(Of solicitudGastoComprobacion)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            comando.Parameters.AddWithValue("@Accion", predicado.Parametros("Accion").Valor)
            comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
            comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
            comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            comando.CommandText = "proAlm_ObtenerSolicitudesdeGastosComprobar"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As solicitudGastoComprobacion
            Dim solicitudGastoComprobacion As New solicitudGastoComprobacion
            solicitudGastoComprobacion.turnoSAF = lectorRenglonActual("turnoSAF").ToString
            solicitudGastoComprobacion.turnoDRM = lectorRenglonActual("turnoDRM").ToString
            solicitudGastoComprobacion.folioTes = lectorRenglonActual("folioTesoreria").ToString
            solicitudGastoComprobacion.folioCaja = lectorRenglonActual("folioCaja").ToString
            solicitudGastoComprobacion.importe = lectorRenglonActual("importe").ToString
            solicitudGastoComprobacion.concepto = lectorRenglonActual("concepto").ToString
            solicitudGastoComprobacion.fechadeCaptura = lectorRenglonActual("fechaCaptura").ToString
            solicitudGastoComprobacion.fechadeLiberacion = lectorRenglonActual("fechaRecepcion").ToString
            solicitudGastoComprobacion.diasTranscurridos = lectorRenglonActual("diasTranscurridos").ToString
            Return solicitudGastoComprobacion
        End Function
    End Class
End Namespace


Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspReporteCompras
Imports Contexto.Entidades.Persistencia.Relacional.Daos

Namespace nspReporteCompras
    <Serializable>
    Public Class daoReporteCompras : Inherits DaoSql(Of reporteCompras)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Contexto.Entidades.Persistencia.Relacional.Daos.Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotImplementedException("Imposible continuar: no contiene parametros.")
            End If
            Dim tipoCosulta As tipoConsultaReporteCompras = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaReporteCompras)
            Select Case tipoCosulta
                Case tipoConsultaReporteCompras.comprasPorProveedor
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaReporteCompras.comprasPorArea
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select
            comando.CommandText = "proAlm_ObtenerReporteCompras"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As reporteCompras
            Dim accion As Integer = lectorRenglonActual("accion").ToString
            Dim reporteCompras As New reporteCompras
            Select Case accion
                Case 1
                    reporteCompras.nombre = lectorRenglonActual("nombre").ToString
                    reporteCompras.fechaElaboracion = CDate(lectorRenglonActual("fechaElaboracion").ToString)
                    reporteCompras.turnoDRM = lectorRenglonActual("turnoDRM").ToString
                    reporteCompras.numeroPedido = CInt(lectorRenglonActual("numeroPedido").ToString)
                    reporteCompras.tipoPago = lectorRenglonActual("tipoPago").ToString
                    reporteCompras.importe = Double.Parse(lectorRenglonActual("importe").ToString)
                Case 2
                    reporteCompras.nombreArea = lectorRenglonActual("nombreArea").ToString
                    reporteCompras.fechaElaboracion = CDate(lectorRenglonActual("fechaElaboracion").ToString)
                    reporteCompras.turnoDRM = lectorRenglonActual("turnoDRM").ToString
                    reporteCompras.numeroPedido = CInt(lectorRenglonActual("numeroPedido").ToString)
                    reporteCompras.tipoPago = lectorRenglonActual("tipoPago").ToString
                    reporteCompras.partida = lectorRenglonActual("partida").ToString
                    reporteCompras.importe = Double.Parse(lectorRenglonActual("importe").ToString)
            End Select
            Return reporteCompras
        End Function
    End Class
End Namespace


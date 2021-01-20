Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports CES.nspReporteSeguimientoxArticulo
Imports Contexto.Persistencia.Relacional.Sql
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspReporteSeguimientoxArticulo
    Public Class daoReporteInventario : Inherits DaoSql(Of ReporteSeguimientoxArticulo)

        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)

            comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
            comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
            If Not predicado.Parametros("idArticulo").Valor Is Nothing Then
                comando.Parameters.AddWithValue("@idArticulo", predicado.Parametros("idArticulo").Valor)
            End If

            comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            comando.CommandText = "proAlm_ObtenerSeguimientoxArticulo"
            comando.CommandType = CommandType.StoredProcedure

        End Sub
        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As ReporteSeguimientoxArticulo
            Dim seguimientoxArticulo As New ReporteSeguimientoxArticulo
            seguimientoxArticulo.articulo = lectorRenglonActual("articulo").ToString
            seguimientoxArticulo.stockMinimo = CInt(lectorRenglonActual("stockMinimo").ToString)
            seguimientoxArticulo.stockMaximo = CInt(lectorRenglonActual("stockMaximo").ToString)
            seguimientoxArticulo.fecha = CDate(lectorRenglonActual("fecha").ToString)
            seguimientoxArticulo.folio = CInt(lectorRenglonActual("folio").ToString)
            seguimientoxArticulo.tipoOperacion = lectorRenglonActual("tipoOperacion").ToString
            seguimientoxArticulo.existencia = CInt(lectorRenglonActual("existencia").ToString)
            seguimientoxArticulo.cantidad = CInt(lectorRenglonActual("cantidad").ToString)
            seguimientoxArticulo.area = lectorRenglonActual("area").ToString
            seguimientoxArticulo.suma = lectorRenglonActual("suma").ToString
            Return seguimientoxArticulo
        End Function

    End Class


End Namespace

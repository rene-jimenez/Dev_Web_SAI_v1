Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspReporteAlmacen
Namespace nspReportealmacen
    Public Class daoReporteAlmacen : Inherits DaoSql(Of reporteAlmacen)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Contexto.Entidades.Persistencia.Relacional.Daos.Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaReporteAlmacen = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaReporteAlmacen)
            Select Case tipoConsulta
                Case tipoConsultaReporteAlmacen.salidaPorCategoria
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaReporteAlmacen.salidaPorArea
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaReporteAlmacen.listaArticulos
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
                Case tipoConsultaReporteAlmacen.entradaPorCategoria
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaReporteAlmacen.gastoPorArea
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaReporteAlmacen.consumoPorArea
                    comando.Parameters.AddWithValue("@Accion", 6)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select

            comando.CommandText = "proAlm_ObtenerReporteAlmacen"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As reporteAlmacen
            Dim accion As Integer = lectorRenglonActual("accion").ToString
            Dim reporteAlmacen As New reporteAlmacen
            Select Case accion
                Case 1
                    reporteAlmacen.idCategoria = Guid.Parse(lectorRenglonActual("idCategoria").ToString)
                    reporteAlmacen.nombreCategoria = lectorRenglonActual("nombreCategoria").ToString
                    reporteAlmacen.nombreArea = lectorRenglonActual("nombreArea").ToString
                    reporteAlmacen.cantidad = CInt(lectorRenglonActual("cantidad").ToString)
                    reporteAlmacen.gastoTotal = Double.Parse(lectorRenglonActual("gastoTotal").ToString)
                Case 2
                    reporteAlmacen.idArea = Guid.Parse(lectorRenglonActual("idArea").ToString)
                    reporteAlmacen.nombreArea = lectorRenglonActual("nombreArea").ToString
                    reporteAlmacen.fechaSalida = CDate(lectorRenglonActual("fechaSalida").ToString)
                    reporteAlmacen.nombreArticulo = lectorRenglonActual("nombreArticulo").ToString
                    reporteAlmacen.cantidad = CInt(lectorRenglonActual("cantidad").ToString)
                    If lectorRenglonActual("numVale").ToString <> "" Then
                        reporteAlmacen.numVale = CInt(lectorRenglonActual("numVale").ToString)
                    End If

                Case 3
                    reporteAlmacen.nombreCategoria = lectorRenglonActual("nombreCategoria").ToString
                    reporteAlmacen.nombreArticulo = lectorRenglonActual("nombreArticulo").ToString
                    reporteAlmacen.ultimoPrecio = Double.Parse(lectorRenglonActual("ultimoPrecio").ToString)
                    reporteAlmacen.existencia = CInt(lectorRenglonActual("existencia").ToString)
                    reporteAlmacen.total = Double.Parse(lectorRenglonActual("total").ToString)
                Case 4
                    reporteAlmacen.idCategoria = Guid.Parse(lectorRenglonActual("idCategoria").ToString)
                    reporteAlmacen.fechaEntrada = CDate(lectorRenglonActual("fechaEntrada").ToString)
                    reporteAlmacen.nombreCategoria = lectorRenglonActual("nombreCategoria").ToString
                    reporteAlmacen.cantidad = CInt(lectorRenglonActual("cantidad").ToString)
                    reporteAlmacen.gastoTotal = Double.Parse(lectorRenglonActual("gastoTotal").ToString)
                Case 5
                    reporteAlmacen.idArea = Guid.Parse(lectorRenglonActual("idArea").ToString)
                    reporteAlmacen.fechaSalida = CDate(lectorRenglonActual("fechaSalida").ToString)
                    reporteAlmacen.nombreArea = lectorRenglonActual("nombreArea").ToString
                    reporteAlmacen.gastoTotal = Double.Parse(lectorRenglonActual("gastoTotal").ToString)
                Case 6
                    reporteAlmacen.idArea = Guid.Parse(lectorRenglonActual("idArea").ToString)
                    reporteAlmacen.nombreArea = lectorRenglonActual("nombreArea").ToString
                    reporteAlmacen.nombreCategoria = lectorRenglonActual("nombreCategoria").ToString
                    reporteAlmacen.nombreArticulo = lectorRenglonActual("nombreArticulo").ToString
                    reporteAlmacen.ultimoPrecio = Double.Parse(lectorRenglonActual("ultimoPrecio").ToString)
                    reporteAlmacen.cantidad = CInt(lectorRenglonActual("cantidad").ToString)
                    reporteAlmacen.gastoTotal = Double.Parse(lectorRenglonActual("gastoTotal").ToString)
            End Select
            Return reporteAlmacen
        End Function


    End Class
End Namespace



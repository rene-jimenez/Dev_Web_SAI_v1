Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspDetalleSalidaAlmacen
Namespace nspDetalleSalidaAlmacen
    Public Class daoDetalleSalidaAlmacen : Inherits DaoSql(Of detalleSalidaAlmacen)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Contexto.Entidades.Persistencia.Relacional.Daos.Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaDetalleSalidaAlmacen = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaDetalleSalidaAlmacen)
            Select Case tipoConsulta
                Case tipoConsultaDetalleSalidaAlmacen.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaDetalleSalidaAlmacen.idSalida
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idSalida", predicado.Parametros("idSalida").Valor)
                Case tipoConsultaDetalleSalidaAlmacen.rangofechas
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idArticulo", predicado.Parametros("idArticulo").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaDetalleSalidaAlmacen.soloRangoFechas
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaDetalleSalidaAlmacen.todos
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select

            comando.CommandText = "proAlm_ObtenerDetalleSalida"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As detalleSalidaAlmacen
            Dim detalleSalidaAlmacen As New detalleSalidaAlmacen
            detalleSalidaAlmacen.id = Guid.Parse(lectorRenglonActual("id").ToString)
            detalleSalidaAlmacen.idSalida = Guid.Parse(lectorRenglonActual("idSalida").ToString)
            detalleSalidaAlmacen.idArticulo = Guid.Parse(lectorRenglonActual("idArticulo").ToString)
            detalleSalidaAlmacen.cantidad = lectorRenglonActual("cantidad").ToString
            detalleSalidaAlmacen.fecha = CDate(lectorRenglonActual("fecha").ToString)
            detalleSalidaAlmacen.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            detalleSalidaAlmacen.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            detalleSalidaAlmacen.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            detalleSalidaAlmacen._nombreArticulo = lectorRenglonActual("nombreArticulo").ToString
            detalleSalidaAlmacen._codigoBarras = lectorRenglonActual("codigoBarras").ToString
            detalleSalidaAlmacen._ultimoPrecio = lectorRenglonActual("ultimoPrecio").ToString
            detalleSalidaAlmacen._importe = lectorRenglonActual("importe").ToString
            detalleSalidaAlmacen._nombreArea = lectorRenglonActual("nombreArea").ToString
            Return detalleSalidaAlmacen
        End Function

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As detalleSalidaAlmacen)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarDetalleSalidaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idSalida", entidad.idSalida)
                comando.Parameters.AddWithValue("@idArticulo", entidad.idArticulo)
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad)
                comando.Parameters.AddWithValue("@fecha", entidad.fecha)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As detalleSalidaAlmacen)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarDetalleSalidaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As detalleSalidaAlmacen)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarDetalleSalidaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace



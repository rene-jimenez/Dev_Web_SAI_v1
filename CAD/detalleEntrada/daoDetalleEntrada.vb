Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspDetalleEntrada
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspDetalleEntrada
    Public Class daoDetalleEntrada : Inherits DaoSql(Of detalleEntrada)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaDetalleEntrada = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaDetalleEntrada)
            Select Case tipoConsulta
                Case tipoConsultaDetalleEntrada.idEntrada
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@idEntrada", predicado.Parametros("idEntrada").Valor)
                Case tipoConsultaDetalleEntrada.idEntradaEsParcial
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idEntrada", predicado.Parametros("idEntrada").Valor)
                    comando.Parameters.AddWithValue("@esParcial", predicado.Parametros("esParcial").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaDetalleEntrada.id
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaDetalleEntrada.rangoFechas
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idArticulo", predicado.Parametros("idArticulo").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaDetalleEntrada.rangoFechasProvedor
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaDetalleEntrada.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)

            End Select
            comando.CommandText = "proAlm_ObtenerDetalleEntradaAlmacen"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As detalleEntrada
            Dim detalleEntrada As New detalleEntrada
            detalleEntrada.id = Guid.Parse(lectorRenglonActual("id").ToString)
            If lectorRenglonActual("idEntrada").ToString <> "" Then
                detalleEntrada.idEntrada = Guid.Parse(lectorRenglonActual("idEntrada").ToString)
            End If
            If lectorRenglonActual("idArticulo").ToString <> "" Then
                detalleEntrada.idArticulo = Guid.Parse(lectorRenglonActual("idArticulo").ToString)
            End If
            If lectorRenglonActual("cantidad").ToString <> "" Then
                detalleEntrada.cantidad = CInt(lectorRenglonActual("cantidad").ToString)
            End If
            If lectorRenglonActual("esParcial").ToString <> "" Then
                detalleEntrada.esParcial = CBool(lectorRenglonActual("esParcial").ToString)
            End If
            If lectorRenglonActual("fechaEntrada").ToString <> "" Then
                detalleEntrada.fechaEntrada = CDate(lectorRenglonActual("fechaEntrada").ToString)
            End If
            If lectorRenglonActual("fechaElaboracionPedido").ToString <> "" Then
                detalleEntrada._fechaElaboracionPedido = CDate(lectorRenglonActual("fechaElaboracionPedido").ToString)
            End If
            If lectorRenglonActual("numEntrada").ToString <> "" Then
                detalleEntrada._numEntrada = lectorRenglonActual("numEntrada").ToString
            End If
            If lectorRenglonActual("esNota").ToString <> "" Then
                detalleEntrada._esNota = CBool(lectorRenglonActual("esNota").ToString)
            End If
            If lectorRenglonActual("numRemision").ToString <> "" Then
                detalleEntrada._numRemision = lectorRenglonActual("numRemision").ToString
            End If
            If lectorRenglonActual("codigoBarras").ToString <> "" Then
                detalleEntrada._codigoBarras = lectorRenglonActual("codigoBarras").ToString
            End If
            If lectorRenglonActual("cantidadPedido").ToString <> "" Then
                detalleEntrada._cantidadPedido = CInt(lectorRenglonActual("cantidadPedido").ToString)
            End If
            If lectorRenglonActual("iva").ToString <> "" Then
                detalleEntrada._iva = CBool(lectorRenglonActual("iva").ToString)
            End If
            If lectorRenglonActual("nombreArticulo").ToString <> "" Then
                detalleEntrada._nombreArticulo = lectorRenglonActual("nombreArticulo").ToString
            End If
            If lectorRenglonActual("unidadMedidaArticulo").ToString <> "" Then
                detalleEntrada._unidadMedidaArticulo = lectorRenglonActual("unidadMedidaArticulo").ToString
            End If
            If lectorRenglonActual("precioUnitario").ToString <> "" Then
                detalleEntrada._precioUnitario = lectorRenglonActual("precioUnitario").ToString
            End If
            If lectorRenglonActual("nombreProveedor").ToString <> "" Then
                detalleEntrada._nombreProveedor = lectorRenglonActual("nombreProveedor").ToString
            End If
            If lectorRenglonActual("cantidadFaltante").ToString <> "" Then
                detalleEntrada._cantidadFaltante = lectorRenglonActual("cantidadFaltante").ToString
            End If
            If lectorRenglonActual("valorIva").ToString <> "" Then
                detalleEntrada._valorIva = lectorRenglonActual("valorIva").ToString
            End If
            If lectorRenglonActual("subTotal").ToString <> "" Then
                detalleEntrada._subTotal = lectorRenglonActual("subTotal").ToString
            End If
            If lectorRenglonActual("total").ToString <> "" Then
                detalleEntrada._total = lectorRenglonActual("total").ToString
            End If
            detalleEntrada.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            detalleEntrada.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            detalleEntrada.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            Return detalleEntrada
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As detalleEntrada)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarDetalleEntradaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idEntrada", entidad.idEntrada)
                comando.Parameters.AddWithValue("@idArticulo", entidad.idArticulo)
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad)
                comando.Parameters.AddWithValue("@esParcial", entidad.esParcial)
                comando.Parameters.AddWithValue("@fecha", entidad.fechaEntrada)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As detalleEntrada)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarDetalleEntradaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad)
                comando.Parameters.AddWithValue("@esParcial", entidad.esParcial)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As detalleEntrada)

            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarDetalleEntradaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace


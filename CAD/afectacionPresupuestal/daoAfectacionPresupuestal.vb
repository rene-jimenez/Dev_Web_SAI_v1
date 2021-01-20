Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspAfectacionPresupuestal
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspAfectacionPresupuestal
    Public Class daoAfectacionPresupuestal : Inherits DaoSql(Of afectacionPresupuestal)

        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As afectacionPresupuestal)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarAfectacion"
                'comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idPedido", entidad.idPedido)
                comando.Parameters.AddWithValue("@concepto", entidad.concepto)
                comando.Parameters.AddWithValue("@marcaAgua", entidad.marcaAgua)
                comando.Parameters.AddWithValue("@subtotal", entidad.subtotal)
                comando.Parameters.AddWithValue("@descuento", entidad.descuento)
                comando.Parameters.AddWithValue("@iva", entidad.iva)
                comando.Parameters.AddWithValue("@total", entidad.total)
                comando.Parameters.AddWithValue("@idSolicita", entidad.idSolicita)
                comando.Parameters.AddWithValue("@idAutoriza", entidad.idAutoriza)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As afectacionPresupuestal)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarAfectacion"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@concepto", entidad.concepto)
                comando.Parameters.AddWithValue("@marcaAgua", entidad.marcaAgua)
                comando.Parameters.AddWithValue("@idSolicita", entidad.idSolicita)
                comando.Parameters.AddWithValue("@idAutoriza", entidad.idAutoriza)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaAfectacionPresupuestal = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaAfectacionPresupuestal)
            Select Case tipoConsulta
                Case tipoConsultaAfectacionPresupuestal.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaAfectacionPresupuestal.idPedido
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idPedido", predicado.Parametros("idPedido").Valor)
                Case tipoConsultaAfectacionPresupuestal.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select

            comando.CommandText = "proAlm_ObtenerAfectacionPresupuestal"
            comando.CommandType = CommandType.StoredProcedure

        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As afectacionPresupuestal
            Dim afectacion As New afectacionPresupuestal
            afectacion.id = Guid.Parse(lectorRenglonActual("id").ToString)
            afectacion.idPedido = Guid.Parse(lectorRenglonActual("idPedido").ToString)
            afectacion.fechaElaboracion = lectorRenglonActual("fechaElaboracion").ToString
            afectacion.concepto = lectorRenglonActual("concepto").ToString
            afectacion.marcaAgua = lectorRenglonActual("marcaAgua").ToString
            afectacion.subtotal = lectorRenglonActual("subtotal").ToString
            afectacion.descuento = lectorRenglonActual("descuento").ToString
            afectacion.iva = lectorRenglonActual("iva").ToString
            afectacion.total = lectorRenglonActual("total").ToString
            afectacion.idSolicita = Guid.Parse(lectorRenglonActual("idSolicita").ToString)
            afectacion.idAutoriza = Guid.Parse(lectorRenglonActual("idAutoriza").ToString)
            afectacion.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            afectacion.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            afectacion.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            afectacion._nombreAutoriza = lectorRenglonActual("_nombreAutoriza").ToString
            afectacion._nombreSolicita = lectorRenglonActual("_nombreSolicita").ToString
            Return afectacion
        End Function
    End Class
End Namespace
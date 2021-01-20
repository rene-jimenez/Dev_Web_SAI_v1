Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspHistorial
Namespace nspHistorial
    Public Class daoHistorial : Inherits DaoSql(Of historial)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Contexto.Entidades.Persistencia.Relacional.Daos.Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaHistorial = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaHistorial)
            Select Case tipoConsulta
                Case tipoConsultaHistorial.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaHistorial.modulo
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@modulo", predicado.Parametros("modulo").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.moduloXfecha
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@modulo", predicado.Parametros("modulo").Valor)
                    comando.Parameters.AddWithValue("@fecha", predicado.Parametros("fecha").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.moduloXfechas
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@modulo", predicado.Parametros("modulo").Valor)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.usuario
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@idUsuario", predicado.Parametros("idUsuario").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.usuarioModulo
                    comando.Parameters.AddWithValue("@Accion", 6)
                    comando.Parameters.AddWithValue("@idUsuario", predicado.Parametros("idUsuario").Valor)
                    comando.Parameters.AddWithValue("@modulo", predicado.Parametros("modulo").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.usuarioModuloXfecha
                    comando.Parameters.AddWithValue("@Accion", 7)
                    comando.Parameters.AddWithValue("@idUsuario", predicado.Parametros("idUsuario").Valor)
                    comando.Parameters.AddWithValue("@modulo", predicado.Parametros("modulo").Valor)
                    comando.Parameters.AddWithValue("@fecha", predicado.Parametros("fecha").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.usuarioModuloXfechas
                    comando.Parameters.AddWithValue("@Accion", 8)
                    comando.Parameters.AddWithValue("@idUsuario", predicado.Parametros("idUsuario").Valor)
                    comando.Parameters.AddWithValue("@modulo", predicado.Parametros("modulo").Valor)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.fechaEspecifica
                    comando.Parameters.AddWithValue("@Accion", 9)
                    comando.Parameters.AddWithValue("@fecha", predicado.Parametros("fecha").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.rangoFecha
                    comando.Parameters.AddWithValue("@Accion", 10)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.edicionXmodulo
                    comando.Parameters.AddWithValue("@Accion", 11)
                    comando.Parameters.AddWithValue("@operacion", predicado.Parametros("operacion").Valor)
                    comando.Parameters.AddWithValue("@modulo", predicado.Parametros("modulo").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.edicionXmoduloFecha
                    comando.Parameters.AddWithValue("@Accion", 12)
                    comando.Parameters.AddWithValue("@operacion", predicado.Parametros("operacion").Valor)
                    comando.Parameters.AddWithValue("@modulo", predicado.Parametros("modulo").Valor)
                    comando.Parameters.AddWithValue("@fecha", predicado.Parametros("fecha").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.edicionXModuloFechas
                    comando.Parameters.AddWithValue("@Accion", 13)
                    comando.Parameters.AddWithValue("@operacion", predicado.Parametros("operacion").Valor)
                    comando.Parameters.AddWithValue("@modulo", predicado.Parametros("modulo").Valor)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.moduloTodos
                    comando.Parameters.AddWithValue("@Accion", 14)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.edicionTodos
                    comando.Parameters.AddWithValue("@Accion", 15)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaHistorial.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select

            comando.CommandText = "proAlm_ObtenerHistorial"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As historial
            If lectorRenglonActual("accion") = 1 Then
                Return New historial() With {.modulo = lectorRenglonActual("modulo").ToString}
            End If
            If lectorRenglonActual("accion") = 2 Then
                Return New historial() With {.operacion = lectorRenglonActual("operacion").ToString}

            Else
            Return New historial() With {.id = Guid.Parse(lectorRenglonActual("id").ToString),
                                         .modulo = lectorRenglonActual("modulo").ToString,
                                         .operacion = lectorRenglonActual("operacion").ToString,
                                         .contenidoNuevo = lectorRenglonActual("contenidoNuevo").ToString,
                                         .contenidoAnterior = lectorRenglonActual("ContenidoAnterior").ToString,
                                         .fechaOperacion = lectorRenglonActual("fechaOperacion").ToString,
                                         ._Usuario = lectorRenglonActual("nombre").ToString
                                       }

            End If
        End Function

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As historial)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregaHistorial"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@modulo", entidad.modulo)
                comando.Parameters.AddWithValue("@operacion", entidad.operacion)
                comando.Parameters.AddWithValue("@contenido", entidad.contenidoAnterior)
                comando.Parameters.AddWithValue("@fechaOperacion", entidad.fechaOperacion)
                comando.Parameters.AddWithValue("@Usuario", entidad.idUsuario)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub


    End Class
End Namespace



Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspValidarEstatusOficio
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspValidarEstatusOficio
    Public Class daoValidarEstatusOficio : Inherits DaoSql(Of validarEstatusOficio)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If

            Dim tipoConsulta As tipoConsultaValidacion = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaValidacion)
            Select Case tipoConsulta
                Case tipoConsultaValidacion.valiSolicitud
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@idOficio", predicado.Parametros("idOficio").Valor)
                Case tipoConsultaValidacion.valiPedidos
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idOficio", predicado.Parametros("idOficio").Valor)
            End Select
            comando.CommandText = "proAlm_ValidarEstatusOficio"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As validarEstatusOficio
            Return New validarEstatusOficio() With {.bandera = lectorRenglonActual("bandera").ToString}
        End Function
    End Class
End Namespace


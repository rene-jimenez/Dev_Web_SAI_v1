Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspUltimoPrecioArticulo
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspUltimoPrecioArticulo
    Public Class daoUltimoPrecioArticulo : Inherits DaoSql(Of ultimoPrecioArticulo)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaUltimoPrecioArticulo = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaUltimoPrecioArticulo)
            Select Case tipoConsulta
                Case tipoConsultaUltimoPrecioArticulo.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaUltimoPrecioArticulo.idArticulo
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idArticulo", predicado.Parametros("idArticulo").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaUltimoPrecioArticulo.rangoFecha
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select
            comando.CommandText = "proAlm_ObtenerUltimoPrecioArticulo"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As ultimoPrecioArticulo
            Dim ultimoPrecioArticulo As New ultimoPrecioArticulo
            ultimoPrecioArticulo.id = Guid.Parse(lectorRenglonActual("id").ToString)
            If lectorRenglonActual("idArticulo").ToString <> "" Then
                ultimoPrecioArticulo.idArticulo = Guid.Parse(lectorRenglonActual("idArticulo").ToString)
            End If
            If lectorRenglonActual("ultimoPrecio").ToString <> "" Then
                ultimoPrecioArticulo.ultimoPrecio = lectorRenglonActual("ultimoPrecio").ToString
            End If
            If lectorRenglonActual("fecha").ToString <> "" Then
                ultimoPrecioArticulo.fecha = lectorRenglonActual("fecha").ToString
            End If
            ultimoPrecioArticulo.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            ultimoPrecioArticulo.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            ultimoPrecioArticulo.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            Return ultimoPrecioArticulo
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As ultimoPrecioArticulo)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarUltimoPrecioArticulo"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idArticulo", entidad.idArticulo)
                comando.Parameters.AddWithValue("@ultimoPrecio", entidad.ultimoPrecio)
                comando.Parameters.AddWithValue("@fecha", entidad.fecha)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If

        End Sub


    End Class
End Namespace


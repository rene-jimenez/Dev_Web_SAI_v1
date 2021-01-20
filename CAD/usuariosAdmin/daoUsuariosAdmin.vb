Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspUsuariosAdmin
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspUsuariosAdmin
    Public Class daoUsuariosAdmin : Inherits DaoSql(Of usuarioAdmin)

        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            comando.CommandText = "proAlm_ObtenerUsuariosAdmin"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As usuarioAdmin
            Dim usuariosAdmin As New usuarioAdmin
            usuariosAdmin.idUsuarioAdministrador = Guid.Parse(lectorRenglonActual("idUsuarioAdministrador").ToString)
            Return usuariosAdmin
        End Function
    End Class

End Namespace

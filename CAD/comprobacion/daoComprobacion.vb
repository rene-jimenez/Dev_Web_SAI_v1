Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspComprobacion
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspComprobacion
    Public Class daoComprobacion : Inherits DaoSql(Of comprobacion)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaComprobacion = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaComprobacion)
            Select Case tipoConsulta
                Case tipoConsultaComprobacion.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaComprobacion.idOficio
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idOficio", predicado.Parametros("idOficio").Valor)
                Case tipoConsultaComprobacion.esDevolucion
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@devolucion", predicado.Parametros("devolucion").Valor)
                Case tipoConsultaComprobacion.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select
            comando.CommandText = "proAlm_ObtenerComprobacionDevolucion"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As comprobacion
            Dim comprobacion As New comprobacion
            comprobacion.id = Guid.Parse(lectorRenglonActual("id").ToString)
            If lectorRenglonActual("idOficio").ToString <> "" Then
                comprobacion.idOficio = Guid.Parse(lectorRenglonActual("idOficio").ToString)
            End If
            If lectorRenglonActual("idResponsable").ToString <> "" Then
                comprobacion.idResponsable = Guid.Parse(lectorRenglonActual("idResponsable").ToString)
            End If
            If lectorRenglonActual("fechaElaboracion").ToString <> "" Then
                comprobacion.fechaElaboracion = lectorRenglonActual("fechaElaboracion").ToString
            End If
            If lectorRenglonActual("concepto").ToString <> "" Then
                comprobacion.concepto = lectorRenglonActual("concepto").ToString
            End If
            If lectorRenglonActual("idAutoriza").ToString <> "" Then
                comprobacion.idAutoriza = Guid.Parse(lectorRenglonActual("idAutoriza").ToString)
            End If
            If lectorRenglonActual("devolucion").ToString <> "" Then
                comprobacion.devolucion = lectorRenglonActual("devolucion").ToString
            End If
            If lectorRenglonActual("marcaAgua").ToString <> "" Then
                comprobacion.marcaAgua = lectorRenglonActual("marcaAgua").ToString
            End If
            If lectorRenglonActual("_nombreAutoriza").ToString <> "" Then
                comprobacion._nombreAutoriza = lectorRenglonActual("_nombreAutoriza").ToString
            End If
            If lectorRenglonActual("_nombreResponsable").ToString <> "" Then
                comprobacion._nombreResponsable = lectorRenglonActual("_nombreResponsable").ToString
            End If
            comprobacion.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            comprobacion.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            comprobacion.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            Return comprobacion
        End Function

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As comprobacion)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarComprobacionDevolucion"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idOficio", entidad.idOficio)
                comando.Parameters.AddWithValue("@idResponsable", entidad.idResponsable)
                comando.Parameters.AddWithValue("@fechaElaboracion", entidad.fechaElaboracion)
                comando.Parameters.AddWithValue("@concepto", entidad.concepto)
                comando.Parameters.AddWithValue("@idAutoriza", entidad.idAutoriza)
                comando.Parameters.AddWithValue("@devolucion", entidad.devolucion)
                comando.Parameters.AddWithValue("@marcaAgua", entidad.marcaAgua)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

    End Class
End Namespace


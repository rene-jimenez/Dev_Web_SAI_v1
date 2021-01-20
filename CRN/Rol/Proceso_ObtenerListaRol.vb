Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspRol
Namespace nspRol
    Public Class Proceso_ObtenerListaRol : Inherits Accion(Of List(Of rol))
        Public Property tipoConsulta As tipoConsultaRol
        Public Property id As Guid
        Public Property esActivo As Boolean?

        Public Sub New()
            MyBase.New("Proceso_ObtenerListaRol", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of rol)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", TipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaRol.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaRol.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspRol.daoRol)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace


'Imports System.Data.SqlClient
'Imports CAD.nspControladorDaos
'Imports Contexto.Persistencia.Relacional.Sql
'Imports CES.nspAlgo
'Namespace nspAlgo
'    Public Class daoNose : Inherits DaoSql(Of entidadAlgo)
'        Public Sub New(controladorDaos As ControladorDaosBase)
'            MyBase.New(controladorDaos)
'        End Sub

'        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Contexto.Entidades.Persistencia.Relacional.Daos.Predicado = Nothing)
'            If (Not predicado.ContieneParametro("tipoConsulta")) Then
'                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
'            End If
'            Dim tipoConsulta As tipoConsulta = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsulta)
'            Select Case tipoConsulta
'                Case tipoConsulta.id
'                    comando.Parameters.AddWithValue("@Accion", 1)
'                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
'                Case tipoConsulta.esActivo
'                    comando.Parameters.AddWithValue("@Accion", 2)
'                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)

'                Case tipoConsulta.todos
'                    comando.Parameters.AddWithValue("@Accion", 99)
'            End Select

'            comando.CommandText = "proAlm_Obtener"
'            comando.CommandType = CommandType.StoredProcedure
'        End Sub

'        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As entidadAlgo
'            Return New entidadAlgo() With {.id = Guid.Parse(lectorRenglonActual("id").ToString),
'                                       .esActivo = Boolean.Parse(lectorRenglonActual("esActivo").ToString)}
'        End Function

'        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As entidadAlgo)
'            If (Not IsNothing(entidad)) Then
'                comando.CommandType = CommandType.StoredProcedure
'                comando.CommandText = "proAlm_Agregar"
'                comando.Parameters.AddWithValue("@id", entidad.id)
'                ' Si hay mas campos escribelos aquí
'            Else
'                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
'            End If
'        End Sub

'        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As entidadAlgo)
'            If (Not IsNothing(entidad)) Then
'                comando.CommandType = CommandType.StoredProcedure
'                comando.CommandText = "proAlm_Editar"
'                comando.Parameters.AddWithValue("@id", entidad.id)
'                ' Si hay mas campos escribelos aquí
'            Else
'                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
'            End If
'        End Sub

'        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As entidadAlgo)
'            If (Not IsNothing(entidad)) Then
'                comando.CommandType = CommandType.StoredProcedure
'                comando.CommandText = "proAlm_Eliminar"
'                comando.Parameters.AddWithValue("@id", entidad.id)
'            Else
'                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
'            End If
'        End Sub
'    End Class
'End Namespace


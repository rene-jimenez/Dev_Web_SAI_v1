Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspSistema
Namespace nspSistema
    Public Class Proceso_ObtenerSistemas : Inherits Accion(Of List(Of sistema))
        Public Property tipoConsulta As tipoSistemaConsulta
        Public Property id As Guid
        Public Property tipo As String
        Public Property año As Integer
        Public Sub New()
            MyBase.New("Proceso_ObtenerSistemas", "Obtiene lista ordenada")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of sistema)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoSistemaConsulta.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoSistemaConsulta.tipo
                    parametros.Add(New ParametroPredicado("tipo", tipo))
                Case tipoSistemaConsulta.año
                    parametros.Add(New ParametroPredicado("año", año))
                Case tipoSistemaConsulta.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspSistema.daoSistema)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace


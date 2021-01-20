Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports Contexto.Notificaciones
Imports System.Data.SqlClient
Imports CES.nspAfectacionPresupuestal
Imports System.Transactions
Namespace nspAfectacionPresupuestal
    Public Class Proceso_ObtenerAfectacionesPresupuestales : Inherits Accion(Of List(Of afectacionPresupuestal))

        Public Property id As Guid
        Public Property idPedido As Guid

        Public Property tipoConsulta As tipoConsultaAfectacionPresupuestal
        Dim controladorMensajes As New notificacionesDeUsuario

        Public Sub New()
            MyBase.New("Proceso_ObtenerAfectacionesPresupuestales", "Actualiza un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of afectacionPresupuestal)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaAfectacionPresupuestal.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaAfectacionPresupuestal.idPedido
                    parametros.Add(New ParametroPredicado("idPedido", idPedido))
                Case tipoConsultaAfectacionPresupuestal.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspAfectacionPresupuestal.daoAfectacionPresupuestal)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace


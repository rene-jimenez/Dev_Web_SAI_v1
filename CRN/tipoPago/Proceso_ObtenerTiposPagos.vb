Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspTipoPago
Namespace nspTipoPago
    Public Class Proceso_ObtenerTiposPagos : Inherits Accion(Of List(Of tipoPago))
        Public Property tipoConsulta As tipoConsultaTipoPago
        Public Property id As Guid
        Public Property esActivo As Boolean?
        Public Sub New()
            MyBase.New("Proceso_ObtenerTipoPago", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of tipoPago)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaTipoPago.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaTipoPago.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                Case tipoConsultaTipoPago.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaTipoPago.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspTipoPago.daoTipoPago)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace


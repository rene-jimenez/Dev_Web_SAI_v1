Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspAjusteArticuloInventario
Namespace nspAjusteArticuloInventario
    Public Class Proceso_ObtenerAjustesArticuloInventario : Inherits Accion(Of List(Of ajusteArticuloInventario))
        Public Property tipoConsulta As tipoConsultaAjusteArticuloInventario
        Public Property id As Guid
        Public Property idArticulo As Guid?
        Public Property idSistema As Guid?
        Public Sub New()
            MyBase.New("Proceso_ObtenerAjustesArticuloInventario", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of ajusteArticuloInventario)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaAjusteArticuloInventario.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaAjusteArticuloInventario.idArticulo
                    parametros.Add(New ParametroPredicado("idArticulo", idArticulo))
                Case tipoConsultaAjusteArticuloInventario.todos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspAjusteArticuloInventario.daoAjusteArticuloInventario)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace


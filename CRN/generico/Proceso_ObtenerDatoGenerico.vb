Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspGenerico
Namespace nspGenerico
    Public Class Proceso_ObtenerDatoGenerico : Inherits Accion(Of generico)
        Public Property idSistema As Guid
        Public Property idArea As Guid
        Public Property turnoDRM As String
        Public Property tipoConsulta As tipoConsultaGenerico
        Public Sub New()
            MyBase.New("Proceso_ObtenerDatoGenerico", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As generico
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            parametros.Add(New ParametroPredicado("idSistema", idSistema))
            parametros.Add(New ParametroPredicado("turnoDRM", turnoDRM))
            parametros.Add(New ParametroPredicado("idArea", idArea))
            Dim lista = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspGenerico.daoGenerico)().ObtenerConjunto(New Predicado("", parametros.ToArray())).FirstOrDefault

            Select Case lista.valor
                Case "*SP", "*ST"
                    lista.valor = lista.valor + "0001"
                Case ""
                    lista.valor = "1"
            End Select
            Return lista
        End Function
    End Class

End Namespace



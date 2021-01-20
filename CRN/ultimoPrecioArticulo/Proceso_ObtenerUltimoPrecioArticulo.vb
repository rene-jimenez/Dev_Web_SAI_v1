Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports CES.nspUltimoPrecioArticulo
Namespace nspUltimoPrecioArticulo
    Public Class Proceso_ObtenerUltimoPrecioArticulo : Inherits Accion(Of List(Of ultimoPrecioArticulo))
        Public Property tipoConsulta As tipoConsultaUltimoPrecioArticulo
        Public Property id As Guid
        Public Property idArticulo As Guid?
        Public Property idSistema As Guid?
        Public Property fechaInicial As Date
        Public Property fechaFinal As Date

        Public Sub New()
            MyBase.New("Proceso_ObtenerUltimoPrecioArticulo", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of ultimoPrecioArticulo)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaUltimoPrecioArticulo.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaUltimoPrecioArticulo.idArticulo
                    parametros.Add(New ParametroPredicado("idArticulo", idArticulo))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaUltimoPrecioArticulo.rangoFecha
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspUltimoPrecioArticulo.daoUltimoPrecioArticulo)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace


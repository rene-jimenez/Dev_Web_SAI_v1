Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspReporteSeguimientoxArticulo
Namespace nspReporteSeguimientoxArticulo
    Public Class Proceso_ObtenerReporteSeguimientoxArticulo : Inherits Accion(Of List(Of ReporteSeguimientoxArticulo))
        Public Property id As Guid
        Public Property fechaInicial As Date?
        Public Property fechaFinal As Date?
        Public Property idSistema As Guid?
        Public Property idArticulo As Guid?

        Public Sub New()
            MyBase.New("Proceso_ObtenerReporteSeguimientoxArticulo", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of ReporteSeguimientoxArticulo)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
            parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
            parametros.Add(New ParametroPredicado("idArticulo", idArticulo))
            parametros.Add(New ParametroPredicado("idSistema", idSistema))
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspReporteSeguimientoxArticulo.daoReporteInventario)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function

    End Class
End Namespace

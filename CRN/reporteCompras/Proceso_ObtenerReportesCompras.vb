Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports CES.nspReporteCompras
Namespace nspReporteCompras
    Public Class Proceso_ObtenerReportesCompras : Inherits Accion(Of List(Of reporteCompras))
        Public Property tipoConsulta As tipoConsultaReporteCompras
        Public Property fechaInicial As Date?
        Public Property fechaFinal As Date?
        Public Property idSistema As Guid?
        Public Sub New()
            MyBase.New("Proceso_ObtenerReportesCompras", "Obtiene el lsitado de los registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of reporteCompras)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaReporteCompras.comprasPorProveedor
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaReporteCompras.comprasPorArea
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspReporteCompras.daoReporteCompras)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function

    End Class
End Namespace


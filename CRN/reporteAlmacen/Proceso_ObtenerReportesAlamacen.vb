Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspReporteAlmacen
Namespace nspReporteAlmacen
    Public Class Proceso_ObtenerReportesAlamacen : Inherits Accion(Of List(Of reporteAlmacen))
        Public Property tipoConsulta As tipoConsultaReporteAlmacen
        Public Property fechaInicial As Date?
        Public Property fechaFinal As Date?
        Public Property tipoSistema As String
        Public Property idSistema As Guid?
        Public Sub New()
            MyBase.New("Proceso_ObtenerReportesAlamacen", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of reporteAlmacen)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaReporteAlmacen.salidaPorCategoria
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaReporteAlmacen.salidaPorArea
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaReporteAlmacen.listaArticulos
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
                Case tipoConsultaReporteAlmacen.entradaPorCategoria
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaReporteAlmacen.gastoPorArea
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaReporteAlmacen.consumoPorArea
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspReportealmacen.daoReporteAlmacen)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace


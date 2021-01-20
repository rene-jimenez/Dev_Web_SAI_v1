Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspFirma
Namespace nspFirma
    Public Class Proceso_ObtenerFirmas : Inherits Accion(Of List(Of firma))
        Public Property tipoConsulta As tipoConsultaFirma
        Public Property id As Guid
        Public Property esActivo As Boolean?
        Public Property idSistema As Guid?
        Public Sub New()
            MyBase.New("Proceso_ObtenerFirmas", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of firma)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaFirma.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaFirma.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                Case tipoConsultaFirma.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaFirma.esActivoXidSistema
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaFirma.nombreActivoXidSistema
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaFirma.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspFirma.daoFirma)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace


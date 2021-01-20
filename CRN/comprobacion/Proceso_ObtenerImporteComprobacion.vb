Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspImporteComprobacion
Namespace nspImporteComprobacion
    Public Class Proceso_ObtenerImporteComprobacion : Inherits Accion(Of List(Of importeComprobacion))
        Public Property tipoConsulta As tipoConsultaImporteComprobacion
        Public Property idOficio As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerImporteComprobacion", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of importeComprobacion)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaImporteComprobacion.idOficio
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspImporteComprobacion.daoImporteComprobacion)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace


Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspValidarEstatusOficio
Namespace nspValidarEstatusOficio
    Public Class Proceso_ObtenerValidacionEstatusOficio : Inherits Accion(Of validarEstatusOficio)
        Public Property tipoConsulta As tipoConsultaValidacion
        Public Property idOficio As Guid?
        Public Property accion As Integer
        Public Sub New()
            MyBase.New("proAlm_ValidarEstatusOficio", "Obtiene una respuesta")
        End Sub
        Protected Overrides Function OnEjecutar() As validarEstatusOficio
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaValidacion.valiSolicitud
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                    parametros.Add(New ParametroPredicado("accion", accion))
                Case tipoConsultaValidacion.valiPedidos
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                    parametros.Add(New ParametroPredicado("accion", accion))
            End Select
            Dim validacion = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspValidarEstatusOficio.daoValidarEstatusOficio)().ObtenerConjunto(New Predicado("", parametros.ToArray())).FirstOrDefault
            Return validacion
        End Function
    End Class
End Namespace


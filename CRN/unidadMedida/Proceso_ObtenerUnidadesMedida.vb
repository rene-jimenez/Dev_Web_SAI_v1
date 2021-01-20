Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspUnidadMedida
Namespace nspUnidadMedida
    Public Class Proceso_ObtenerUnidadesMedida : Inherits Accion(Of List(Of unidadMedida))
        Public Property tipoConsulta As tipoConsultaUnidadMedida
        Public Property id As Guid
        Public Property esActivo As Boolean?
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnidadesMedida", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of unidadMedida)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaUnidadMedida.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaUnidadMedida.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                Case tipoConsultaUnidadMedida.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaUnidadMedida.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspUnidadMedida.daoUnidadMedida)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace


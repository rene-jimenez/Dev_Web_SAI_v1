Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspCategoria
Namespace nspCategoria
    Public Class Proceso_ObtenerCategorias : Inherits Accion(Of List(Of categoria))
        Public Property tipoConsulta As tipoConsultaCategoria
        Public Property id As Guid

        Public Property esActivo As Boolean?


        Public Sub New()
            MyBase.New("Proceso_ObtenerCategorias", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of categoria)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaCategoria.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaCategoria.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                Case tipoConsultaCategoria.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaCategoria.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspCategoria.daoCategoria)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace


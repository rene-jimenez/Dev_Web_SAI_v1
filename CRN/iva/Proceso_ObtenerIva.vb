Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspIva
Namespace nspIva
    Public Class Proceso_ObtenerIva : Inherits Accion(Of Double)
        Public Property fecha As Date
        Public Sub New()
            MyBase.New("Proceso_ObtenerIva", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As Double
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("fecha", fecha))
            Dim iva = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspIva.daoIva)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
            Return iva.FirstOrDefault.valor
        End Function
    End Class
End Namespace


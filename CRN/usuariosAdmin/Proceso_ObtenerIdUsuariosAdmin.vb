Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports CES.nspUsuariosAdmin
Namespace nspUsuariosAdmin
    Public Class Proceso_ObtenerIdUsuariosAdmin : Inherits Accion(Of List(Of usuarioAdmin))
        Public Sub New()
            MyBase.New("Proceso_ObtenerUltimoPrecioArticulo", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of usuarioAdmin)
            Dim parametros As New List(Of ParametroPredicado)
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspUsuariosAdmin.daoUsuariosAdmin)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace



Imports cadenero.entidades.nspBase
Namespace nspUltimoPrecioArticulo
    <Serializable>
    Public Class ultimoPrecioArticulo : Inherits base
        Public Property idArticulo As Guid?
        Public Property ultimoPrecio As String
        Public Property fecha As Date

        Public Sub New()

        End Sub
    End Class
End Namespace


Namespace nspAjusteArticuloInventario
    <Serializable>
    Public Class ajusteArticuloInventario
        Public Property id As Guid
        Public Property idArticulo As Guid?
        Public Property tipoOperacion As String
        Public Property explicacion As String
        Public Property fecha As Date?
        Public Property cantidad As Integer
        Public Property idSistema As Guid?
        Public Property ipUsuario As String
        Public Property idUsuarioMovimiento As Guid?
    End Class
End Namespace


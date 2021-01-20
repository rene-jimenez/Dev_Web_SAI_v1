Imports System.IO
Namespace nspHistorial
    <Serializable>
    Public Class historial
        Public Property id As Guid
        Public Property modulo As String
        Public Property operacion As String
        Public Property contenidoNuevo As String
        Public Property contenidoAnterior As String
        Public Property fechaOperacion As DateTime
        Public Property idUsuario As Guid
        Public Property ipUsuario As String
        Public Property idSistema As Guid
        Public Property _tipoSistema As String
        Public Property _Usuario As String
    End Class
End Namespace




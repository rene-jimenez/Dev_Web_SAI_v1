Namespace nspImporteComprobacion
    <Serializable>
    Public Class importeComprobacion
        ' datos para la consulta de ObtenerImporteComprobación
        Public Property importeTotalPedido As String
        Public Property importeTotalSolicitado As String
        Public Property importeDevolucion As String
        Public Property idOficioCons As Guid?
        Public Property turnoDrm As String
        Public Property turnoSaf As String
        Public Property CargoPresupuestal As String
        Public Property folioCajaSolicitud As String
        Public Property folioTesoreriaSolicitud As String
        Public Property folioCajaAlcance As String
        Public Property folioTesoreriaAlcance As String
    End Class
End Namespace



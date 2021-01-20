Imports cadenero.entidades.nspBase
Namespace nspAlcance
    <Serializable>
    Public Class alcance : Inherits base
        Public Property idPartida As Guid?
        Public Property idSolicitud As Guid?
        Public Property folioTesoreria As String
        Public Property folioCaja As String
        Public Property fechaRecepcion As Date?
        Public Property fechaCaptura As Date
        Public Property importe As Double
        Public Property no As Integer
        Public Property esCancelado As Boolean
        Public Property fechaCancelacion As Date?
        Public Property responsableCancelacion As String
        Public Property observacionCancelacion As String
        Public Property _folioCajaSolicitud As String
        Public Property _folioTesoreriaSolicitud As String
        Public Property _importeSolicitud As String
        Public Property _conceptoSolicitud As String
        Public Property _idArea As Guid?
        Public Property _Area As String
        Public Property _turnoDrm As String
        Public Property _turnoSaf As String
        Public Property _CargoPresupuestal As String
        Public Property _fechaCapturaSolicitud As Date
        Public Property _fechaRecepcionSolicitud As Date
        Public Property _tipoEdicion As String




        Public Sub New()

        End Sub
    End Class
End Namespace

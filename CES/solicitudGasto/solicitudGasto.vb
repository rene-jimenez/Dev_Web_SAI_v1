Imports cadenero.entidades.nspBase
Namespace nspSolicitudGasto
    <Serializable>
    Public Class solicitudGasto : Inherits base
        Public Property idOficio As Guid?
        Public Property idPartidaPresupuestal As Guid?
        Public Property importe As Decimal
        Public Property fechaElaboracion As DateTime?
        Public Property folioCaja As String
        Public Property folioTesoreria As String
        Public Property fechaRecepcion As Date?
        Public Property concepto As String
        Public Property esCancelado As Boolean?
        Public Property fechaCancelacion As Date?
        Public Property responsableCancelacion As String
        Public Property observacionCancelacion As String
        Public Property marcaAgua As String
        Public Property _nombrePartidaPresupuestal As String
        Public Property _nombreArea As String
        Public Property _nombreCargoPresupuestal As String
        Public Property _nombreEstatusOficio As String
        Public Property _idArea As Guid?
        Public Property _idCargoPresupuestal As Guid?
        Public Property _idEstatusOficio As Guid?
        Public Property _esDocInterno As Boolean?
        Public Property _turnoDRM As String
        Public Property _turnoSAF As String
        Public Property _tipoEdicion
        Public Sub New()

        End Sub


    End Class
End Namespace


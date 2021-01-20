Imports cadenero.entidades.nspBase
Namespace nspAfectacionPresupuestal
    <Serializable>
    Public Class afectacionPresupuestal : Inherits base
        Public Property idPedido As Guid?
        Public Property fechaElaboracion As Date?
        Public Property concepto As String
        Public Property marcaAgua As String
        Public Property subtotal As Double
        Public Property descuento As Double
        Public Property iva As Double
        Public Property total As Double
        Public Property idSolicita As Guid?
        Public Property idAutoriza As Guid?
        Public Property _nombreSolicita As String
        Public Property _nombreAutoriza As String
        Public Sub New()

        End Sub
    End Class
End Namespace


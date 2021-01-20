Imports cadenero.entidades.nspBase
Namespace nspComprobacion
        <Serializable>
        Public Class comprobacion : Inherits base
        Public Property idOficio As Guid?
        Public Property idAutoriza As Guid?
        Public Property idResponsable As Guid?
        Public Property fechaElaboracion As Date
        Public Property concepto As String
        Public Property devolucion As Boolean
        Public Property marcaAgua As String
        Public Property _nombreAutoriza As String
        Public Property _nombreResponsable As String
        Public Sub New()

        End Sub
        End Class   
End Namespace


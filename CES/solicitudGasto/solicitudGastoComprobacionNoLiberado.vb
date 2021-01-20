Namespace nspSolicitudGastoComprobacionNoLiberado
    <Serializable>
    Public Class solicitudGastoComprobacionNoLiberado
        Public Property fechadeCaptura As Date?
        Public Property turnoSAF As String
        Public Property turnoDRM As String
        Public Property importeSolicitud As Double
        Public Property concepto As String
        Public Property diasTranscurridos As Integer

        Public Sub New()

        End Sub
    End Class
End Namespace

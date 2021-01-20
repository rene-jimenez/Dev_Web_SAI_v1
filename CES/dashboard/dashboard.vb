Imports cadenero.entidades.nspBase
Namespace nspDashboard
    <Serializable>
    Public Class dashboard : Inherits base
        Public Property oficiosEnproceso As Integer
        Public Property oficiosAtendidos As Integer
        Public Property solGastoNoLiberados As Integer
        Public Property solGastoLiberadosPendientes As Integer
        Public Property solGastoComprobados As Integer
        Public Property pedidoPendientesEntrega As Integer
        Public Property pedidosEntregados As Integer
        Public Property pedidoSinAfectacion As Integer

        Public Sub New()

        End Sub
    End Class
End Namespace
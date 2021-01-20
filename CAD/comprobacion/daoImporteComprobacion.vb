Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspImporteComprobacion
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspImporteComprobacion
    Public Class daoImporteComprobacion : Inherits DaoSql(Of importeComprobacion)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaImporteComprobacion = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaImporteComprobacion)
            Select Case tipoConsulta
                Case tipoConsultaImporteComprobacion.idOficio
                    comando.Parameters.AddWithValue("@idOficio", predicado.Parametros("idOficio").Valor)
            End Select
            comando.CommandText = "proAlm_ObtenerImporteComprobacion"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As importeComprobacion
            Dim importeComprobacion As New importeComprobacion
            If lectorRenglonActual("importeTotalPedido").ToString <> "" Then
                importeComprobacion.importeTotalPedido = lectorRenglonActual("importeTotalPedido").ToString
            End If
            If lectorRenglonActual("importeTotalSolicitado").ToString <> "" Then
                importeComprobacion.importeTotalSolicitado = lectorRenglonActual("importeTotalSolicitado").ToString
            End If
            If lectorRenglonActual("importeDevolucion").ToString <> "" Then
                importeComprobacion.importeDevolucion = lectorRenglonActual("importeDevolucion").ToString
            End If
            If lectorRenglonActual("idOficioCons").ToString <> "" Then
                importeComprobacion.idOficioCons = Guid.Parse(lectorRenglonActual("idOficioCons").ToString)
            End If
            If lectorRenglonActual("turnoDrm").ToString <> "" Then
                importeComprobacion.turnoDrm = lectorRenglonActual("turnoDrm").ToString
            End If
            If lectorRenglonActual("turnoSaf").ToString <> "" Then
                importeComprobacion.turnoSaf = lectorRenglonActual("turnoSaf").ToString
            End If
            If lectorRenglonActual("CargoPresupuestal").ToString <> "" Then
                importeComprobacion.CargoPresupuestal = lectorRenglonActual("CargoPresupuestal").ToString
            End If
            If lectorRenglonActual("folioCajaSolicitud").ToString <> "" Then
                importeComprobacion.folioCajaSolicitud = lectorRenglonActual("folioCajaSolicitud").ToString
            End If
            If lectorRenglonActual("folioTesoreriaSolicitud").ToString <> "" Then
                importeComprobacion.folioTesoreriaSolicitud = lectorRenglonActual("folioTesoreriaSolicitud").ToString
            End If
            If lectorRenglonActual("folioCajaAlcance").ToString <> "" Then
                importeComprobacion.folioCajaAlcance = lectorRenglonActual("folioCajaAlcance").ToString
            End If
            If lectorRenglonActual("folioTesoreriaAlcance").ToString <> "" Then
                importeComprobacion.folioTesoreriaAlcance = lectorRenglonActual("folioTesoreriaAlcance").ToString
            End If
            Return importeComprobacion
        End Function
    End Class
End Namespace


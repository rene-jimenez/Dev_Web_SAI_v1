Imports CES.nspAlcance, CRN.nspAlcance
Imports CES.nspSolicitudGasto, CRN.nspSolicitudGasto
Imports CES.nspOficio, CRN.nspOficio
Imports CES.nspArea, CRN.nspArea
Imports CES.nspFirma, CRN.nspFirma
Imports CES.nspPartidaPresupuestal, CRN.nspPartidaPresupuestal
Imports Microsoft.Reporting.WebForms
Imports CES.nspPopup
Imports Contexto.Notificaciones.controladorMensajes
Public Class frmReporteAlcance : Inherits nspPaginaBase.PaginaBase
    Dim controladorMensajes As New notificacionesDeUsuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim sistema As New nspPaginaBase.PaginaBase
                Dim idAlcance = Request.QueryString("idAlcance")
                Dim consultaAlcance = New Proceso_ObtenerAlcance() With {.id = Guid.Parse(idAlcance)}.Ejecutar()
                Dim unaSolicitud = New Proceso_ObtenerSolicitudGasto With {.id = Guid.Parse(consultaAlcance.idSolicitud.ToString)}.Ejecutar()
                Dim datosOficio = New Proceso_ObtenerUnOficio With {.id = Guid.Parse(unaSolicitud.idOficio.ToString)}.Ejecutar()
                Dim area = New Proceso_ObtenerArea() With {.id = Guid.Parse(datosOficio.idArea.ToString)}.Ejecutar()
                Dim partidaPresupuestal = New Proceso_ObtenerPartidaPresupuestal() With {.id = Guid.Parse(consultaAlcance.idPartida.ToString)}.Ejecutar()
                Dim autorizo = New Proceso_ObtenerFirmas() With {.tipoConsulta = tipoConsultaFirma.nombreActivoXidSistema, .Nombre = "Autoriza", .esActivo = True, .idSistema = sistema.sistemaActivo.id}.Ejecutar.FirstOrDefault

                Dim Conversor = New Contexto.Biblioteca.controladorDeFunciones.Conversion()
                Dim cadena = Conversor.dblToStrPesos(consultaAlcance.importe).ToString()

                Dim autoriza As New List(Of ReportParameter)
                autoriza.Add(New ReportParameter("autoriza", autorizo._nombreUsuario))
                Dim letra As New List(Of ReportParameter)
                letra.Add(New ReportParameter("cadenaLetra", cadena))
                Dim deOficio As New List(Of ReportParameter)
                deOficio.Add(New ReportParameter("saf", datosOficio.turnoSAF))
                deOficio.Add(New ReportParameter("cargoPresupuestal", datosOficio._cargoPresupuestal))
                deOficio.Add(New ReportParameter("area", datosOficio._area))

                Dim alcances As New List(Of ReportParameter)
                alcances.Add(New ReportParameter("tes", consultaAlcance.folioTesoreria))
                alcances.Add(New ReportParameter("fechaElaboracion", consultaAlcance.fechaCaptura.ToString("dd/MM/yyyy")))
                alcances.Add(New ReportParameter("importeAlcance", "$ " + consultaAlcance.importe.ToString("N2")))
                alcances.Add(New ReportParameter("cajaFolioAlcance", consultaAlcance.folioCaja))
                If Not consultaAlcance.fechaRecepcion Is Nothing Then
                    alcances.Add(New ReportParameter("fechaRecepcion", consultaAlcance.fechaRecepcion))
                End If

                Dim codigo As New List(Of ReportParameter)
                codigo.Add(New ReportParameter("claveArea", area.codigo))
                Dim numero As New List(Of ReportParameter)
                numero.Add(New ReportParameter("clavePartida", partidaPresupuestal.numero))

                Dim solicitud As New List(Of ReportParameter)
                solicitud.Add(New ReportParameter("concepto", UCase(unaSolicitud.concepto)))
                solicitud.Add(New ReportParameter("folioSolicitud", unaSolicitud.folioCaja))
                solicitud.Add(New ReportParameter("importeSolicitud", "$ " + unaSolicitud.importe.ToString("N2")))
                solicitud.Add(New ReportParameter("partidaPresupuestal", UCase(partidaPresupuestal.nombre)))
                Dim dataSource As ReportDataSource = New ReportDataSource()
                rptAlcance.LocalReport.DataSources.Clear()
                Dim dtsAlcance As New dtsAlcance
                Dim listaAlcance = New Proceso_ObtenerAlcances() With {.tipoConsulta = tipoConsultaAlcance.id, .id = Guid.Parse(idAlcance)}.Ejecutar().ToList
                If listaAlcance.Count > 0 Then
                    For i = 0 To listaAlcance.Count - 1
                        Dim drm As String = listaAlcance(i)._turnoDrm
                        'Dim folio As String = listaAlcance(i).folioTesoreria
                        'Dim fechaElaboracion As String = listaAlcance(i).fechaCaptura
                        'Dim cargoPresupuestal As String = listaAlcance(i)._CargoPresupuestal
                        'Dim area As String = listaAlcance(i)._Area
                        'Dim importe As String = listaAlcance(i).importe
                        'Dim concepto As String = listaAlcance(i)._conceptoSolicitud
                        'Dim caja As String = listaAlcance(i).folioCaja
                        'Dim fechaRecepcion As String = listaAlcance(i).fechaRecepcion
                        'Dim autoriza As String = listaAlcance(i).nombre
                        dtsAlcance.tblAlcance.AddtblAlcanceRow(drm, UCase(sistema.sistemaActivo.nombre) + " " + sistema.sistemaActivo.año.ToString, UCase(NombreUsuario))

                    Next
                    dataSource = New ReportDataSource("dtsAlcance", dtsAlcance.tblAlcance.DefaultView)
                    rptAlcance.ProcessingMode = ProcessingMode.Local
                    rptAlcance.LocalReport.ReportPath = "management/alcance/reporteAlcance/rpt/rptAlcance.rdlc"
                    rptAlcance.LocalReport.SetParameters(deOficio)
                    rptAlcance.LocalReport.SetParameters(alcances)
                    rptAlcance.LocalReport.SetParameters(solicitud)
                    rptAlcance.LocalReport.SetParameters(letra)
                    rptAlcance.LocalReport.SetParameters(codigo)
                    rptAlcance.LocalReport.SetParameters(numero)
                    rptAlcance.LocalReport.SetParameters(autoriza)
                    rptAlcance.LocalReport.DataSources.Add(dataSource)
                    rptAlcance.LocalReport.Refresh()
                End If
            Catch ex As Exception
                OnMostrarMensajeAccion("Crítico", ex.Message.ToString, tipoPopup.Rojo, False, "")
            End Try
        End If
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
End Class
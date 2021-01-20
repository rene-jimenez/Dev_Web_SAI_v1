Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspHistorial
Namespace nspHistorial
    Public Class Proceso_ObtenerHistorial : Inherits Accion(Of List(Of historial))
        Public Property tipoConsulta As tipoConsultaHistorial
        Public Property id As Guid
        Public Property idUsuario As Guid
        Public Property idSistema As Guid
        Public Property modulo As String
        Public Property operacion As String
        Public Property fechaInicial As DateTime
        Public Property fechafinal As DateTime
        Public Property fecha As DateTime

        Public Sub New()
            MyBase.New("Proceso_ObtenerHistorial", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of historial)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaHistorial.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaHistorial.modulo
                    parametros.Add(New ParametroPredicado("modulo", modulo))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.moduloXfecha
                    parametros.Add(New ParametroPredicado("modulo", modulo))
                    parametros.Add(New ParametroPredicado("fecha", fecha))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.moduloXfechas
                    parametros.Add(New ParametroPredicado("modulo", modulo))
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechafinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.usuario
                    parametros.Add(New ParametroPredicado("idUsuario", idUsuario))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.usuarioModulo
                    parametros.Add(New ParametroPredicado("modulo", modulo))
                    parametros.Add(New ParametroPredicado("idUsuario", idUsuario))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.usuarioModuloXfecha
                    parametros.Add(New ParametroPredicado("modulo", modulo))
                    parametros.Add(New ParametroPredicado("idUsuario", idUsuario))
                    parametros.Add(New ParametroPredicado("fecha", fecha))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.usuarioModuloXfechas
                    parametros.Add(New ParametroPredicado("modulo", modulo))
                    parametros.Add(New ParametroPredicado("idUsuario", idUsuario))
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechafinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.fechaEspecifica
                    parametros.Add(New ParametroPredicado("fecha", fecha))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.rangoFecha
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechafinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.edicionXmodulo
                    parametros.Add(New ParametroPredicado("modulo", modulo))
                    parametros.Add(New ParametroPredicado("operacion", operacion))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.edicionXmoduloFecha
                    parametros.Add(New ParametroPredicado("modulo", modulo))
                    parametros.Add(New ParametroPredicado("operacion", operacion))
                    parametros.Add(New ParametroPredicado("fecha", fecha))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.edicionXmoduloFecha
                    parametros.Add(New ParametroPredicado("modulo", modulo))
                    parametros.Add(New ParametroPredicado("operacion", operacion))
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechafinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.moduloTodos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.edicionTodos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaHistorial.todos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspHistorial.daoHistorial)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace



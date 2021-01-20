Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspArticulo
Namespace nspArticulo
    Public Class Proceso_ObtenerArticulos : Inherits Accion(Of List(Of articulo))
        Public Property tipoConsulta As tipoConsultaArticulo
        Public Property id As Guid
        Public Property idCategoria As Guid?
        Public Property tipoSistema As String
        Public Property esActivo As Boolean?
        Public Property existencia As Integer?
        Public Property entraAlmacen As Boolean?
        Public Property codigoBarras As String

        Public Sub New()
            MyBase.New("Proceso_ObtenerArticulo", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of articulo)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaArticulo.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaArticulo.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
                Case tipoConsultaArticulo.nombre_x_categoria
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                    parametros.Add(New ParametroPredicado("idCategoria", idCategoria))
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
                Case tipoConsultaArticulo.entraAlmacen
                    parametros.Add(New ParametroPredicado("entraAlmacen", entraAlmacen))
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
                Case tipoConsultaArticulo.xExistencia
                    parametros.Add(New ParametroPredicado("existencia", existencia))
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
                Case tipoConsultaArticulo.codigoBarras
                    parametros.Add(New ParametroPredicado("codigoBarras", codigoBarras))
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
                Case tipoConsultaArticulo.reporteXCategoria
                    parametros.Add(New ParametroPredicado("idCategoria", idCategoria))
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
                Case tipoConsultaArticulo.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
                Case tipoConsultaArticulo.activosParaCodigoBarras
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
                Case tipoConsultaArticulo.todos
                    parametros.Add(New ParametroPredicado("tipoSistema", tipoSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspArticulo.daoArticulo)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace


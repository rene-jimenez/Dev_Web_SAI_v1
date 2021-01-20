Imports CES, CRN
Imports CRN.nspHistorial
Imports CES.nspHistorial
Imports Contexto.Notificaciones.controladorMensajes
Imports System.IO, System.Xml, System.Xml.Serialization
Imports System.Reflection
Public Class frmDetalleHistorial
    Inherits nspPaginaBase.PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim idHistorial As Guid
            If Not Request.QueryString("idHistorial") Is Nothing Then
                idHistorial = Guid.Parse(Request.QueryString("idHistorial"))
            Else
                idHistorial = Guid.Parse("c7859687-f268-441f-8e07-006876aa17e6")

            End If
            ObtenterDetalleHistorial(idHistorial)
        End If
    End Sub
    Protected Sub ObtenterDetalleHistorial(idHistorial As Guid)


        Dim historial = New Proceso_ObtenerHistorial With {.tipoConsulta = tipoConsultaHistorial.id, .id = idHistorial}.Ejecutar()
        Dim usuario = New cadenero.CRN.nspUsuario.Proceso_ObtenerUsuario() With {.id = historial(0).idUsuario}.Ejecutar
        lblUsuario.Text = historial(0)._Usuario
        lblFechaOperacion.Text = historial(0).fechaOperacion
        lblModulo.Text = historial(0).modulo
        lblUsuario2.Text = historial(0)._Usuario
        lblFechaOperacion2.Text = historial(0).fechaOperacion
        lblModulo2.Text = historial(0).modulo
        Select Case historial(0).modulo.ToString
            Case "Usuario"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As cadenero.entidades.nspUsuario.usuario
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspUsuario.usuario))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un Usuario"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As cadenero.entidades.nspUsuario.usuario
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspUsuario.usuario))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As cadenero.entidades.nspUsuario.usuario
                        Dim serializerNuevo As New XmlSerializer(GetType(cadenero.entidades.nspUsuario.usuario))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el Usuario"
                        lblOperacion2.Text = " Editó el Usuario"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                End Select

            Case "Rol"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As cadenero.entidades.nspRol.rol
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspRol.rol))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un rol"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As cadenero.entidades.nspRol.rol
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspRol.rol))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As cadenero.entidades.nspRol.rol
                        Dim serializerNuevo As New XmlSerializer(GetType(cadenero.entidades.nspRol.rol))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el rol"
                        lblOperacion2.Text = " Editó el rol"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As cadenero.entidades.nspRol.rol
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspRol.rol))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un rol"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "UsuarioRol"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As cadenero.entidades.nspUsuarioRol.usuarioRol
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspUsuarioRol.usuarioRol))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un usuarioRol"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As cadenero.entidades.nspUsuarioRol.usuarioRol
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspUsuarioRol.usuarioRol))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As cadenero.entidades.nspUsuarioRol.usuarioRol
                        Dim serializerNuevo As New XmlSerializer(GetType(cadenero.entidades.nspUsuarioRol.usuarioRol))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el usuarioRol"
                        lblOperacion2.Text = " Editó el usuarioRol"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As cadenero.entidades.nspUsuarioRol.usuarioRol
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspUsuarioRol.usuarioRol))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un usuarioRol"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Permiso"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As cadenero.entidades.nspPermiso.permiso
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspPermiso.permiso))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un permiso"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As cadenero.entidades.nspPermiso.permiso
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspPermiso.permiso))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As cadenero.entidades.nspPermiso.permiso
                        Dim serializerNuevo As New XmlSerializer(GetType(cadenero.entidades.nspPermiso.permiso))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el permiso"
                        lblOperacion2.Text = " Editó el permiso"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As cadenero.entidades.nspPermiso.permiso
                        Dim serializer As New XmlSerializer(GetType(cadenero.entidades.nspPermiso.permiso))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un permiso"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Responsable"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspResponsable.responsable
                        Dim serializer As New XmlSerializer(GetType(CES.nspResponsable.responsable))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un responsable"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspResponsable.responsable
                        Dim serializer As New XmlSerializer(GetType(CES.nspResponsable.responsable))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspResponsable.responsable
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspResponsable.responsable))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el responsable"
                        lblOperacion2.Text = " Editó el responsable"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspResponsable.responsable
                        Dim serializer As New XmlSerializer(GetType(CES.nspResponsable.responsable))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un responsable"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "UnidadMedida"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspUnidadMedida.unidadMedida
                        Dim serializer As New XmlSerializer(GetType(CES.nspUnidadMedida.unidadMedida))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó una UnidadMedida"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspUnidadMedida.unidadMedida
                        Dim serializer As New XmlSerializer(GetType(CES.nspUnidadMedida.unidadMedida))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspUnidadMedida.unidadMedida
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspUnidadMedida.unidadMedida))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó la UnidadMedida"
                        lblOperacion2.Text = " Editó la UnidadMedida"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspUnidadMedida.unidadMedida
                        Dim serializer As New XmlSerializer(GetType(CES.nspUnidadMedida.unidadMedida))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó una UnidadMedida"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Firma"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspFirma.firma
                        Dim serializer As New XmlSerializer(GetType(CES.nspFirma.firma))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó una firma"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspFirma.firma
                        Dim serializer As New XmlSerializer(GetType(CES.nspFirma.firma))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspFirma.firma
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspFirma.firma))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó la firma"
                        lblOperacion2.Text = " Editó la firma"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspFirma.firma
                        Dim serializer As New XmlSerializer(GetType(CES.nspFirma.firma))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó una firma"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "DetalleSalidaAlmacen"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspDetalleSalidaAlmacen.detalleSalidaAlmacen
                        Dim serializer As New XmlSerializer(GetType(CES.nspDetalleSalidaAlmacen.detalleSalidaAlmacen))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un detalleSalidaAlmacen"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspDetalleSalidaAlmacen.detalleSalidaAlmacen
                        Dim serializer As New XmlSerializer(GetType(CES.nspDetalleSalidaAlmacen.detalleSalidaAlmacen))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspDetalleSalidaAlmacen.detalleSalidaAlmacen
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspDetalleSalidaAlmacen.detalleSalidaAlmacen))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el detalleSalidaAlmacen"
                        lblOperacion2.Text = " Editó el detalleSalidaAlmacen"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspDetalleSalidaAlmacen.detalleSalidaAlmacen
                        Dim serializer As New XmlSerializer(GetType(CES.nspDetalleSalidaAlmacen.detalleSalidaAlmacen))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un detalleSalidaAlmacen"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "SalidaAlmacen"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspSalidaAlmacen.salidaAlmacen
                        Dim serializer As New XmlSerializer(GetType(CES.nspSalidaAlmacen.salidaAlmacen))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó una salidaAlmacen"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspSalidaAlmacen.salidaAlmacen
                        Dim serializer As New XmlSerializer(GetType(CES.nspSalidaAlmacen.salidaAlmacen))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspSalidaAlmacen.salidaAlmacen
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspSalidaAlmacen.salidaAlmacen))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó la salidaAlmacen"
                        lblOperacion2.Text = " Editó la salidaAlmacen"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspSalidaAlmacen.salidaAlmacen
                        Dim serializer As New XmlSerializer(GetType(CES.nspSalidaAlmacen.salidaAlmacen))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó una salidaAlmacen"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Pedido"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspPedido.pedido
                        Dim serializer As New XmlSerializer(GetType(CES.nspPedido.pedido))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un pedido"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspPedido.pedido
                        Dim serializer As New XmlSerializer(GetType(CES.nspPedido.pedido))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspPedido.pedido
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspPedido.pedido))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el pedido"
                        lblOperacion2.Text = " Editó el pedido"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspPedido.pedido
                        Dim serializer As New XmlSerializer(GetType(CES.nspPedido.pedido))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un pedido"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "DetallePedido"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspDetallePedido.detallePedido
                        Dim serializer As New XmlSerializer(GetType(CES.nspDetallePedido.detallePedido))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un detallePedido"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspDetallePedido.detallePedido
                        Dim serializer As New XmlSerializer(GetType(CES.nspDetallePedido.detallePedido))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspDetallePedido.detallePedido
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspDetallePedido.detallePedido))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el detallePedido"
                        lblOperacion2.Text = " Editó el detallePedido"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspDetallePedido.detallePedido
                        Dim serializer As New XmlSerializer(GetType(CES.nspDetallePedido.detallePedido))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un detallePedido"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Categoría"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspCategoria.categoria
                        Dim serializer As New XmlSerializer(GetType(CES.nspCategoria.categoria))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó una categoría"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspCategoria.categoria
                        Dim serializer As New XmlSerializer(GetType(CES.nspCategoria.categoria))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspCategoria.categoria
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspCategoria.categoria))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó la categoría"
                        lblOperacion2.Text = " Editó la categoría"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspCategoria.categoria
                        Dim serializer As New XmlSerializer(GetType(CES.nspCategoria.categoria))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó una categoría"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "DocumentoContable"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspDocumentoContable.documentoContable
                        Dim serializer As New XmlSerializer(GetType(CES.nspDocumentoContable.documentoContable))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un documentoContable"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspDocumentoContable.documentoContable
                        Dim serializer As New XmlSerializer(GetType(CES.nspDocumentoContable.documentoContable))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspDocumentoContable.documentoContable
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspDocumentoContable.documentoContable))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el documentoContable"
                        lblOperacion2.Text = " Editó el documentoContable"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspDocumentoContable.documentoContable
                        Dim serializer As New XmlSerializer(GetType(CES.nspDocumentoContable.documentoContable))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un documentoContable"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "TipoPago"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspTipoPago.tipoPago
                        Dim serializer As New XmlSerializer(GetType(CES.nspTipoPago.tipoPago))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un tipoPago"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspTipoPago.tipoPago
                        Dim serializer As New XmlSerializer(GetType(CES.nspTipoPago.tipoPago))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspTipoPago.tipoPago
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspTipoPago.tipoPago))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el tipoPago"
                        lblOperacion2.Text = " Editó el tipoPago"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspTipoPago.tipoPago
                        Dim serializer As New XmlSerializer(GetType(CES.nspTipoPago.tipoPago))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un tipoPago"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "RubroRequerimiento"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspRubroRequerimiento.rubroRequerimiento
                        Dim serializer As New XmlSerializer(GetType(CES.nspRubroRequerimiento.rubroRequerimiento))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un rubroRequerimiento"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspRubroRequerimiento.rubroRequerimiento
                        Dim serializer As New XmlSerializer(GetType(CES.nspRubroRequerimiento.rubroRequerimiento))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspRubroRequerimiento.rubroRequerimiento
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspRubroRequerimiento.rubroRequerimiento))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el rubroRequerimiento"
                        lblOperacion2.Text = " Editó el rubroRequerimiento"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspRubroRequerimiento.rubroRequerimiento
                        Dim serializer As New XmlSerializer(GetType(CES.nspRubroRequerimiento.rubroRequerimiento))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un rubroRequerimiento"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Artículo"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspArticulo.articulo
                        Dim serializer As New XmlSerializer(GetType(CES.nspArticulo.articulo))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un artículo"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspArticulo.articulo
                        Dim serializer As New XmlSerializer(GetType(CES.nspArticulo.articulo))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspArticulo.articulo
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspArticulo.articulo))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el artículo"
                        lblOperacion2.Text = " Editó el artículo"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspArticulo.articulo
                        Dim serializer As New XmlSerializer(GetType(CES.nspArticulo.articulo))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un artículo"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "PartidaPresupuestal"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspPartidaPresupuestal.partidaPresupuestal
                        Dim serializer As New XmlSerializer(GetType(CES.nspPartidaPresupuestal.partidaPresupuestal))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó una partidaPresupuestal"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspPartidaPresupuestal.partidaPresupuestal
                        Dim serializer As New XmlSerializer(GetType(CES.nspPartidaPresupuestal.partidaPresupuestal))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspPartidaPresupuestal.partidaPresupuestal
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspPartidaPresupuestal.partidaPresupuestal))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó la partidaPresupuestal"
                        lblOperacion2.Text = " Editó la partidaPresupuestal"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosEliminado As CES.nspPartidaPresupuestal.partidaPresupuestal
                        Dim serializer As New XmlSerializer(GetType(CES.nspPartidaPresupuestal.partidaPresupuestal))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó una partidaPresupuestal"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "TelefonoProveedor"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspTelefonoProveedor.telefonoProveedor
                        Dim serializer As New XmlSerializer(GetType(CES.nspTelefonoProveedor.telefonoProveedor))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un telefonoProveedor"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspTelefonoProveedor.telefonoProveedor
                        Dim serializer As New XmlSerializer(GetType(CES.nspTelefonoProveedor.telefonoProveedor))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspTelefonoProveedor.telefonoProveedor
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspTelefonoProveedor.telefonoProveedor))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el telefonoProveedor"
                        lblOperacion2.Text = " Editó el telefonoProveedor"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesAnterior.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosEliminado As CES.nspTelefonoProveedor.telefonoProveedor
                        Dim serializer As New XmlSerializer(GetType(CES.nspTelefonoProveedor.telefonoProveedor))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un telefonoProveedor"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Área"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspArea.area
                        Dim serializer As New XmlSerializer(GetType(CES.nspArea.area))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un área"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspArea.area
                        Dim serializer As New XmlSerializer(GetType(CES.nspArea.area))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspArea.area
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspArea.area))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el área"
                        lblOperacion2.Text = " Editó el área"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspArea.area
                        Dim serializer As New XmlSerializer(GetType(CES.nspArea.area))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un área"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "EstatusOficio"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspEstatusOficio.estatusOficio
                        Dim serializer As New XmlSerializer(GetType(CES.nspEstatusOficio.estatusOficio))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un estatusOficio"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspEstatusOficio.estatusOficio
                        Dim serializer As New XmlSerializer(GetType(CES.nspEstatusOficio.estatusOficio))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspEstatusOficio.estatusOficio
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspEstatusOficio.estatusOficio))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el estatusOficio"
                        lblOperacion2.Text = " Editó el estatusOficio"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspEstatusOficio.estatusOficio
                        Dim serializer As New XmlSerializer(GetType(CES.nspEstatusOficio.estatusOficio))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un estatusOficio"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Proveedor"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspProveedor.proveedor
                        Dim serializer As New XmlSerializer(GetType(CES.nspProveedor.proveedor))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un proveedor"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspProveedor.proveedor
                        Dim serializer As New XmlSerializer(GetType(CES.nspProveedor.proveedor))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspProveedor.proveedor
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspProveedor.proveedor))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el proveedor"
                        lblOperacion2.Text = " Editó el proveedor"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspProveedor.proveedor
                        Dim serializer As New XmlSerializer(GetType(CES.nspProveedor.proveedor))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un proveedor"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "EntradaAlmacen"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspEntrada.entrada
                        Dim serializer As New XmlSerializer(GetType(CES.nspEntrada.entrada))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó una entradaAlmacen"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspEntrada.entrada
                        Dim serializer As New XmlSerializer(GetType(CES.nspEntrada.entrada))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspEntrada.entrada
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspEntrada.entrada))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó la entradaAlmacen"
                        lblOperacion2.Text = " Editó la entradaAlmacen"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspEntrada.entrada
                        Dim serializer As New XmlSerializer(GetType(CES.nspEntrada.entrada))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó una entradaAlmacen"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "DetalleEntradaAlmacen"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspDetalleEntrada.detalleEntrada
                        Dim serializer As New XmlSerializer(GetType(CES.nspDetalleEntrada.detalleEntrada))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un detalleEntradaAlmacen"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspDetalleEntrada.detalleEntrada
                        Dim serializer As New XmlSerializer(GetType(CES.nspDetalleEntrada.detalleEntrada))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspDetalleEntrada.detalleEntrada
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspDetalleEntrada.detalleEntrada))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el detalleEntradaAlmacen"
                        lblOperacion2.Text = " Editó el detalleEntradaAlmacen"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspDetalleEntrada.detalleEntrada
                        Dim serializer As New XmlSerializer(GetType(CES.nspDetalleEntrada.detalleEntrada))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un detalleEntradaAlmacen"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Oficio"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspOficio.oficio
                        Dim serializer As New XmlSerializer(GetType(CES.nspOficio.oficio))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un oficio"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspOficio.oficio
                        Dim serializer As New XmlSerializer(GetType(CES.nspOficio.oficio))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspOficio.oficio
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspOficio.oficio))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el oficio"
                        lblOperacion2.Text = " Editó el oficio"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspOficio.oficio
                        Dim serializer As New XmlSerializer(GetType(CES.nspOficio.oficio))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un oficio"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "SolicitudGasto"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspSolicitudGasto.solicitudGasto
                        Dim serializer As New XmlSerializer(GetType(CES.nspSolicitudGasto.solicitudGasto))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó una SolicitudGasto"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspSolicitudGasto.solicitudGasto
                        Dim serializer As New XmlSerializer(GetType(CES.nspSolicitudGasto.solicitudGasto))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspSolicitudGasto.solicitudGasto
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspSolicitudGasto.solicitudGasto))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó la SolicitudGasto"
                        lblOperacion2.Text = " Editó la SolicitudGasto"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspSolicitudGasto.solicitudGasto
                        Dim serializer As New XmlSerializer(GetType(CES.nspSolicitudGasto.solicitudGasto))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó una SolicitudGasto"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "AfectacionPresupuestal"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspAfectacionPresupuestal.afectacionPresupuestal
                        Dim serializer As New XmlSerializer(GetType(CES.nspAfectacionPresupuestal.afectacionPresupuestal))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó una AfectacionPresupuestal"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspAfectacionPresupuestal.afectacionPresupuestal
                        Dim serializer As New XmlSerializer(GetType(CES.nspAfectacionPresupuestal.afectacionPresupuestal))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspAfectacionPresupuestal.afectacionPresupuestal
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspAfectacionPresupuestal.afectacionPresupuestal))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó la AfectacionPresupuestal"
                        lblOperacion2.Text = " Editó la AfectacionPresupuestal"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspAfectacionPresupuestal.afectacionPresupuestal
                        Dim serializer As New XmlSerializer(GetType(CES.nspAfectacionPresupuestal.afectacionPresupuestal))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó una AfectacionPresupuestal"
                        mostrarDatosEliminados(datosEliminado)
                End Select
            Case "Alcance"
                Select Case historial(0).operacion
                    Case "Agregar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        divContainer.Attributes.Add("class", "flex col-md-7")
                        divContenidoAterior.Attributes.Add("class", "col-md-12")
                        divDetallesAnterior.Attributes.Add("class", "col-md-12")

                        Dim cadenaXML As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspAlcance.alcance
                        Dim serializer As New XmlSerializer(GetType(CES.nspAlcance.alcance))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Agregó un alcance presupuestal"
                        mostrarDatosAgregados(datosNuevos)

                    Case "Editar"
                        divContenidoAterior.Visible = False
                        divContenidoModificado.Attributes.Add("class", "col-md-12")
                        Dim cadenaXMLDatosAnteriore As String = historial(0).contenidoAnterior.ToString
                        Dim datosAnteriores As CES.nspAlcance.alcance
                        Dim serializer As New XmlSerializer(GetType(CES.nspAlcance.alcance))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosAnteriore)
                            datosAnteriores = serializer.Deserialize(reader)
                        End Using
                        Dim cadenaXMLDatosNuevos As String = historial(0).contenidoNuevo.ToString
                        Dim datosNuevos As CES.nspAlcance.alcance
                        Dim serializerNuevo As New XmlSerializer(GetType(CES.nspAlcance.alcance))
                        Using reader As TextReader = New StringReader(cadenaXMLDatosNuevos)
                            datosNuevos = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Editó el alcance presupuestal"
                        lblOperacion2.Text = " Editó el alcance presupuestal"
                        mostrarDatosEdidatos(datosAnteriores, datosNuevos)

                    Case "Eliminar"
                        divContenidoModificado.Visible = False
                        divDetallesModificado.Visible = False
                        Dim cadenaXML As String = historial(0).contenidoAnterior.ToString
                        Dim datosEliminado As CES.nspAlcance.alcance
                        Dim serializer As New XmlSerializer(GetType(CES.nspAlcance.alcance))
                        Using reader As TextReader = New StringReader(cadenaXML)
                            datosEliminado = serializer.Deserialize(reader)
                        End Using
                        lblOperacion.Text = " Eliminó un alcance presupuestal"
                        mostrarDatosEliminados(datosEliminado)
                End Select
        End Select
    End Sub
    Private Sub mostrarDatosAgregados(datosNuevos As Object)
        Dim llenarLiteral As New System.Text.StringBuilder

        'Cabecera de la tabla
        llenarLiteral.Append(" <div class='table-responsive'><table class='table table-striped'><thead class='c-white bgm-bluegray'><td style='width:25%;'></td><td style='width:75%;'><i class='fa fa-file-text-o' aria-hidden='true'></i> Datos agregados</td></thead>")
        ' Cuerpo de la tabla
        For Each Propiedad As PropertyInfo In datosNuevos.GetType().GetProperties
            If Propiedad.Name.Substring(0, 1) <> "_" Then
                llenarLiteral.Append("<tr><td><strong>").Append(Propiedad.Name).Append("</strong></td><td><small>").Append(Propiedad.GetValue(datosNuevos, Nothing)).Append("</small></td></tr>")
            End If
        Next
        'Cierre de la tabla
        llenarLiteral.Append("</table></div>")
        litContenidoHistorial.Text = Server.HtmlDecode(llenarLiteral.ToString)
    End Sub

    Private Sub mostrarDatosEdidatos(datosAnteriores As Object, datosNuevos As Object)
        Dim llenarLiteral As New System.Text.StringBuilder
        Dim llenarLiteral2 As New System.Text.StringBuilder
        llenarLiteral.Append(" <div class='table-responsive'><table class='table table-striped'><thead class='c-white bgm-bluegray'><td style='width:25%;'></td><td style='width:75%;'><i class='fa fa-file-text-o' aria-hidden='true'></i> Datos anteriores</td></thead>")

        For Each Propiedad As PropertyInfo In datosNuevos.GetType().GetProperties
            If Propiedad.Name.Substring(0, 1) <> "_" Then
                llenarLiteral.Append("<tr><td><strong>").Append(Propiedad.Name).Append("</strong></td><td><small>").Append(Propiedad.GetValue(datosAnteriores, Nothing)).Append("</small></td></tr>")
            End If
        Next
        llenarLiteral.Append("</table></div>")
        litContenidoHistorial.Text = Server.HtmlDecode(llenarLiteral.ToString)
        llenarLiteral2.Append(" <div class='table-responsive'><table class='table table-striped'><thead class='c-white bgm-bluegray'><td style='width:25%;'></td><td style='width:75%;'><i class='fa fa-file-text-o' aria-hidden='true'></i> Datos modificados</td></thead>")

        For Each Propiedad As PropertyInfo In datosNuevos.GetType().GetProperties
            If Propiedad.Name.Substring(0, 1) <> "_" Then
                If Propiedad.GetValue(datosAnteriores, Nothing) = Propiedad.GetValue(datosNuevos, Nothing) Then
                    llenarLiteral2.Append("<tr><td><strong>").Append(Propiedad.Name).Append("</strong></td><td><small>").Append(Propiedad.GetValue(datosNuevos, Nothing)).Append("</small></td></tr>")
                Else
                    llenarLiteral2.Append("<tr><td><strong>").Append(Propiedad.Name).Append("</strong></td><td><small><strong>").Append(Propiedad.GetValue(datosNuevos, Nothing)).Append("</strong></small></td></tr>")
                End If
            End If
        Next
        llenarLiteral2.Append("</table></div>")
        litContenidoHistorialModificado.Text = Server.HtmlDecode(llenarLiteral2.ToString)
    End Sub
    Private Sub mostrarDatosEliminados(datosEliminado As Object)
        Dim llenarLiteral2 As New System.Text.StringBuilder
        llenarLiteral2.Append(" <div class='table-responsive'><table class='table table-striped'><thead class='c-white bgm-bluegray'><td style='width:25%;'></td><td style='width:75%;'><i class='fa fa-file-text-o' aria-hidden='true'></i> Datos eliminados</td></thead>")
        ' Cuerpo de la tabla
        For Each Propiedad As PropertyInfo In datosEliminado.GetType().GetProperties
            If Propiedad.Name.Substring(0, 1) <> "_" Then
                llenarLiteral2.Append("<tr><td><strong>").Append(Propiedad.Name).Append("</strong></td><td><small>").Append(Propiedad.GetValue(datosEliminado, Nothing)).Append("</small></td></tr>")
            End If
        Next
        'Cierre de la tabla
        llenarLiteral2.Append("</table></div>")
        litContenidoHistorial.Text = Server.HtmlDecode(llenarLiteral2.ToString)
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/management/historial/frmHistorial.aspx")
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        mandaDefault()
    End Sub
End Class
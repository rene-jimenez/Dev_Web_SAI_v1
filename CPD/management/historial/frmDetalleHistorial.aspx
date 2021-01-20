<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmDetalleHistorial.aspx.vb" Inherits="CPD.frmDetalleHistorial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a class="text-uppercase">ADMINISTRACIÓN</a></li>
            <li class="active text-uppercase">Detalle Historial</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Historial
            </h2>
        </div>
        <div class="clearfix"></div>

        <div class="flex" id="divContainer" runat="server">

            <div class="col-md-6" id="divContenidoAterior" runat="server">
                <div class="card">
                    <div class="card-header ch-alt bgm-gray">
                        <h2>Contenido </h2>
                    </div>

                    <div class="card-body card-padding">


                        <div class="list-group">
                            <button type="button" class="list-group-item">Usuario: <strong><asp:Label id="lblUsuario" runat="server" /></strong></button>
                            <button type="button" class="list-group-item">Módulo: <strong><asp:Label id="lblModulo" runat="server" /></strong></button>
                            <button type="button" class="list-group-item">Tipo operación: <strong><asp:Label id="lblOperacion" runat="server" /></strong></button>
                            <button type="button" class="list-group-item">Fecha y hora: <strong><asp:Label id="lblFechaOperacion" runat="server" /></strong></button>
                           <%-- <button type="button" class="list-group-item">Contenido: <strong><asp:Label id="Label4" runat="server" /></strong></button>--%>
                            
                        </div>


                    </div>
                </div>
            </div>
            <div class="col-md-6" id="divContenidoModificado" runat="server">

                <div class="card">
                    <div class="card-header ch-alt bgm-bluegray">
                        <h2>Contenido modificado</h2>
                    </div>

                    <div class="card-body card-padding">

                        <div class="list-group">
                            <button type="button" class="list-group-item">Usuario: <strong><asp:Label id="lblUsuario2" runat="server" /></strong></button>
                            <button type="button" class="list-group-item">Módulo: <strong><asp:Label id="lblModulo2" runat="server" /></strong></button>
                            <button type="button" class="list-group-item">Tipo operación: <strong><asp:Label id="lblOperacion2" runat="server" /></strong></button>
                            <button type="button" class="list-group-item">Fecha y hora: <strong><asp:Label id="lblFechaOperacion2" runat="server" /></strong></button>
                      <%--      <button type="button" class="list-group-item">Contenido: <strong><asp:Label id="Label6" runat="server" /></strong></button>--%>
                            
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-6" id="divDetallesAnterior" runat="server">
                <div class="card">           

                    <div class="card-body card-padding">
                        <div class="card-body p-t-0">
                                 <asp:Literal ID="litContenidoHistorial" runat="server"></asp:Literal>
                            </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6" id="divDetallesModificado" runat="server">
                <div class="card">           

                    <div class="card-body card-padding">
                        <div class="card-body p-t-0">
                                 <asp:Literal ID="litContenidoHistorialModificado" runat="server"></asp:Literal>
                            </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="text text-right p-b-25 p-r-25">
                    <asp:LinkButton class="btn btn-info" runat="server" ID="btnVolver" TabIndex="10"><i class="fa fa-history"></i> Volver a historial</asp:LinkButton>
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="11"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>

    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

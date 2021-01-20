<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAgregarSalida.aspx.vb" Inherits="CPD.frmAgregarSalida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var nav4 = window.Event ? true : false;
        function acceptNum(evt) {
            var key = nav4 ? evt.which : evt.keyCode;
            return (key <= 13 || (key >= 48 && key <= 57) || key == 40);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">

    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Almacén</a></li>
            <li><a href="#" class="text-uppercase">Salida</a></li>
            <li class="active text-uppercase">Agregar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Almacén
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Agregar salida
                </h2>
            </div>
            <div class="card-body card-padding">
                <div class="row">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-sm-6">
                                <div class="form-group m-b-30 ">
                                    <div class="fg-line">
                                        <label class="fg-label">Area <span class="text-danger">(*)</span></label>
                                        <div class="select">
                                            <asp:DropDownList ID="cmbArea" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="1">
                                                <asp:ListItem Text="Seleccione un elemento de la lista" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <div class="col-sm-6">
                        <div class="input-group">
                            <div class="fg-line">
                                <label class="fg-label">Fecha de salida <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbFechaSalida" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" TabIndex="2" />
                            </div>
                            <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-sm-6">
                                <div class="fg-line">
                                    <label class="fg-label">Folio vale <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbFolioVale" runat="server" class="form-control input-sm" MaxLength="6" placeholder="000000" TabIndex="3" disabled="false" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-sm-6">
                                <div class="fg-line">
                                    <label class="fg-label">Folio oficio </label>
                                    <asp:TextBox ID="txbFoliooficio" runat="server" class="form-control input-sm" TabIndex="4" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>



                </div>
            </div>
        </div>
      <div class="clearfix"></div>
        <div class="row">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-header c-bluegray" runat="server" id="headerAgregarArticulos">
                                <h2>
                                    <asp:Label runat="server" ID="lblTituloAgregarCodigos" Text="Artículos" />
                                </h2>
                            </div>
                            <div class="card-body card-padding">
                                <div class="form-horizontal m-10">
                                    <div class="col-sm-12">
                                         <div class="form-group">
                                            
                                            <div class="fg-line">
                                                <label class="fg-label">Elige tipo de busqueda:</label>  <br />
                                                <br />
                                             
                                                    <label class="radio radio-inline m-r-20" style="padding: 0px; padding-left: 8%;">
                                                            <asp:RadioButton checked="true" runat="server" ID="chkCodigo" AutoPostBack="true" />
                                                            <i class="input-helper"></i>Código de Barras
                                                        </label>
                                                        <label class="radio radio-inline m-r-20" style="padding: 0px; padding-left: 8%;">
                                                            <asp:RadioButton runat="server" ID="chkNombre" AutoPostBack="true" />

                                                            <i class="input-helper"></i>Nombre
                                                        </label>
                                            
                                                        
                                            
                                            </div>
                                        </div>
                                    </div>
                                       
                                   
                                     <div class="form-group p-t-20" id="divCodigoBarras" runat="server">
                                        <label class="col-sm-4 control-label">Código de barras  <span class="text-danger">(*)</span></label>
                                        <asp:Label ID="lblIdArticulo" runat="server" Visible="false" />
                                        <asp:HiddenField ID="paIdArticulo" runat="server" />

                                        <div class="col-sm-8">
                                            <div class="fg-line">
                                                <asp:TextBox runat="server" ID="txbCodigo" AutoPostBack="true" CssClass="form-control input-sm" TabIndex="5" />
                                            </div>
                                        </div>                             

                                    </div>

                                    <div class="form-group" id="divNombreArticulo" runat="server">
                                        <label class="col-sm-4 control-label">Artículo </label>
                                        <div class="col-sm-8">
                                            <div class="fg-line">
                                                <!-- MUCHO OJO, es una etiqueta este campo-->
                                                <asp:TextBox runat="server" CssClass="form-control input-sm" TabIndex="5" ID="lblArticuloCodigo" TextMode="MultiLine" readonly='true' Rows="3"/>
                                                <%--<asp:Label runat="server" ID="lblArticuloCodigo" CssClass="form-control input-sm" TabIndex="5" />--%>
                                            </div>
                                        </div>
                                    </div>
                                   
                                    
                                    <div class="form-group" id="divCmbArticulo" runat="server">
                                        <div class="col-sm-12">
                                            <div class="fg-line">
                                                <label class="col-sm-4 control-label">Artículo <span class="text-danger">(*)</span></label>
                                                <div class="select">
                                                    <asp:DropDownList ID="cmbArticulo" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True" OnTextChanged="cmbArticulo_TextChanged" AutoPostBack="true" TabIndex="1">
                                                        <asp:ListItem Text="Seleccione un elemento de la lista" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Cantidad  <span class="text-danger">(*)</span></label>
                                        <div class="col-sm-6">
                                            <div class="fg-line">
                                                <asp:TextBox runat="server" ID="txbCantidad" CssClass="form-control input-sm" onKeyPress="return acceptNum(event);" MaxLength="10" TabIndex="7" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <ul class="actions">
                                                <li class="dropdown">
                                                    <a href="" data-toggle="dropdown" aria-expanded="false">
                                                        <i class="zmdi zmdi-more-vert"></i>
                                                    </a>

                                                    <ul class="dropdown-menu dropdown-menu-right">
                                                        <li>
                                                            <i class="fa fa-thumbs-up animated rubberBand c-bluegray"></i>Existencia:
                                                        </li>
                                                        <li>
                                                            <span class="f-400">
                                                                <asp:Label runat="server" ID="lblstockActual" Text="" /></span>

                                                        </li>

                                                    </ul>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="text text-right p-b-25 p-r-25">
                                            <asp:LinkButton class="btn btn-primary" runat="server" ID="btnAgregarCodigo" TabIndex="8"><i class="fa fa-check-circle"></i> Agregar</asp:LinkButton>
                                            <%--<asp:LinkButton class="btn btn-default" runat="server" ID="btnBorrar" TabIndex="9"><i class="fa fa-cog fa-spin"></i> Borrar</asp:LinkButton>--%>
                                        </div>
                                    </div>



                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="col-md-8">
                <div class="card" id="DivLista" runat ="server" visible ="false" >
                    <div class="card-header">
                        <h2>Lista de artículos
                        </h2>
                    </div>
                    <div class="card-body">
                        <!--Borras esta tabla es solo de ejemplo  y descomentas el update panel de abajo para llenar tu listview -->

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="lsvListado" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                                    <LayoutTemplate>
                                        <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                            <thead class="cf">
                                                <tr>

                                                    <th data-column-id="id" data-type="numeric" style="width: 5%;">NÚM.</th>
                                                    <th style="width: 40%;" class="text-center">Artículo</th>
                                                    <th style="width: 15%;" class="text-center">Cant. Entregada</th>
                                                    <th style="width: 15%;" class="text-center">Precio</th>
                                                    <th style="width: 15%;" class="text-center">Total</th>
                                                    <th style="width: 10%;" class="text-center">Acción</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="text-center">
                                                <asp:Label ID="lblIDLista" Text='<%#Container.DataItemIndex + 1 %> ' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblArticulo" Text='<%#Eval("_nombreArticulo")%>' runat="server" />
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblCantidadEntregada" Text='<%#Eval("cantidad")%>' runat="server" />
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblPrecioUnitario" Text='<%#String.Format("{0:C4}", Eval("_precioUnitario"))%>' runat="server" />
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblImporte" Text='<%#String.Format("{0:C2}", Eval("_importe"))%>' class="text-center" runat="server" />
                                            </td>
                                            <td style="text-align: right">
                                                <ul class="actions">

                                                    <li class="dropdown">
                                                        <a href="" data-toggle="dropdown" aria-expanded="false">
                                                            <i class="zmdi zmdi-more-vert"></i>
                                                        </a>

                                                        <ul class="dropdown-menu dropdown-menu-right">
                                                            <li>
                                                                <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="lnkEliminar_Click" CommandName='<%#Eval("id") %>' TabIndex='<%#Container.DataItemIndex%>'><i class="fa fa-times animated infinite wobble c-red"></i> Eliminar</asp:LinkButton>
                                                            </li>

                                                        </ul>
                                                    </li>
                                                </ul>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bgm-deeporange">
                                            <div>
                                                <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                                No existen registros, revisa la información o llama a la Coordinación de Informática, al área de Sistemas
                                            </div>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="text text-right p-b-25 p-r-25">
                        <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar" TabIndex="10"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                        <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="11"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
    <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".conFiltro").select2();
                   }
               });
           };
</script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

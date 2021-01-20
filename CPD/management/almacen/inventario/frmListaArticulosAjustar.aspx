<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmListaArticulosAjustar.aspx.vb" Inherits="CPD.frmListaArticulosAjustar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">

    <div class="container">
    <ol class="breadcrumb">
            <li><a href="~/default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Almacén</a></li>
            <li><a href="#" class="text-uppercase">Inventario</a></li>
            <li class="active text-uppercase">Desactivar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Almacén
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>
                    <asp:Label ID="lblTitulo" runat="server" Text="Lista"></asp:Label>
                </h2>
            </div>
            <div class="card-body" runat="server" id="consultar" >
              <asp:UpdatePanel  UpdateMode="Conditional" ID="updateArticulo" runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lsvConsultar" runat="server" ItemPlaceholderID="elementoPlaceHolder" ViewStateMode="Enabled">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                     
                                    <thead class="cf">
                                        <tr>
                                             <th style="width: 10%;" class="text-center">Código barras</th>
                                            <th style="width: 35%;" class="text-center">Artículo</th>
                                            <th style="width: 10%;" class="text-center"  >Stock mínimo</th>
                                            <th style="width: 10%;" class="text-center">Stock máximo.</th>
                                            <th style="width: 10%;" class="text-center">Cantidad inicial</th>
                                            <th style="width: 9%;" class="text-center">Unidad medida</th>
                                            <th style="width: 10%;" class="text-center">Existencia</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCodebar" Text='<%#Eval("codigoBarras")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblArticulo" Text='<%#Eval("nombre")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblStockMinimo" Text='<%#Eval("stockMinimo")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblStockMaximo" Text='<%#Eval("stockMaximo")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblCantidadInicial" Text='<%#Eval("cantidadInicial")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblUnidadMedida" Text='<%#Eval("nombreUnidadMedida")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblExistencia" Text='<%#Eval("existencia")%>'  runat="server" />
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

            <div class="card-body" runat="server" id="demas" > 
              <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("id")%>' />
              <asp:UpdatePanel  UpdateMode="Conditional" ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lsvListado" runat="server" ItemPlaceholderID="elementoPlaceHolder" ViewStateMode="Enabled">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                     
                                    <thead class="cf">
                                        <tr>
                                             <th style="width: 10%;" class="text-center">Código barras</th>
                                            <th style="width: 35%;" class="text-center">Artículo</th>
                                            <th style="width: 10%;" class="text-center"  >Stock mínimo</th>
                                            <th style="width: 10%;" class="text-center">Stock máximo.</th>
                                            <th style="width: 10%;" class="text-center">Cantidad inicial</th>
                                            <th style="width: 9%;" class="text-center">Unidad medida</th>
                                            <th style="width: 10%;" class="text-center">Existencia</th>
                                            <th style="width: 6%;" class="text-center" runat="server" id="thx">Acción</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCodebar" Text='<%#Eval("codigoBarras")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblArticulo" Text='<%#Eval("nombre")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblStockMinimo" Text='<%#Eval("stockMinimo")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblStockMaximo" Text='<%#Eval("stockMaximo")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblCantidadInicial" Text='<%#Eval("cantidadInicial")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblUnidadMedida" Text='<%#Eval("nombreUnidadMedida")%>'  runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblExistencia" Text='<%#Eval("existencia")%>'  runat="server" />
                                    </td>
                                   
                                    <td style="text-align: right" id="tdx" runat="server"  >
                                        <ul class="actions">
                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>
                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                     <li>
                                                                        <asp:LinkButton ID="lnkSeleccionar" runat="server" OnClick="lnkSeleccionar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-border-color"></span> Seleccionar </asp:LinkButton>
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

            <div class="card-footer">
               <div class="col-sm-12 text-right">
            <div class="form-group m-b-10">
                <div class="fg-line">                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnSalir"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
           </div>

              <br />
            <br />
              <br /> 
        </div>        
 </div>
         

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

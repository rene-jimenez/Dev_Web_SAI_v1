<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmListaSolicitudGasto.aspx.vb" Inherits="CPD.frmListaSolicitudGasto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
        <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">SOLICITUD DE GASTO</a></li><asp:HiddenField ID="hdfMoreVert" runat="server" />
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">SOLICITUD DE GASTO</h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Lista de solicitudes de gasto</h2>
            </div>
            <div class="card-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="lsvPedidos" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>

                                            <th data-column-id="id" data-type="numeric" width="5%">Núm.</th>
                                            <th style="width: 10%;" class="text-center">Turno DRM</th>
                                            <th style="width: 35%;" class="text-left">Folio caja</th>
                                            <th style="width: 20%;" class="text-center">Folio tesoreria</th>
                                            <th style="width: 10%;" class="text-center">Importe solicitado</th>
                                            <th style="width: 10%;" class="text-center">Fecha de recepción</th>                                            
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
                                    <td>
                                        <asp:Label ID="lblIDLista" Text='<%#Container.DataItemIndex + 1 %> ' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label id="lblNumPedido" Text='<%#Eval("_turnoDRM")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProveedor" Text='<%#Eval("folioCaja")%>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNombrePedido" Text='<%#Eval("folioTesoreria")%>' runat="server" />
                                    </td>
                                    
                                    <td>
                                        <asp:Label ID="lblTipoPago" Text='<%#Eval("importe")%>' runat="server" />
                                    </td>
                                     <td>
                                        <asp:Label ID="lblFechaPAgo" Text='<%#Eval("fechaRecepcion")%>' runat="server" />
                                    </td>
                                    <td style="text-align: center">
                                        <ul class="actions">

                                                            <li class="dropdown">
                                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                                    <i class="zmdi zmdi-more-vert"></i>
                                                                </a>

                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                     
                                                                    <li>
                                                                   <asp:LinkButton ID="lnkVer" runat="server" OnClick="lnkVer_Click" CommandName='<%#Eval("id") %>' > Consultar solicitud </asp:LinkButton>                                                                   
                                                                  </li>
                                                                    

                                                                </ul>
                                                            </li>
                                                        </ul>
                                        
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                  <tr class="text-center" >
                                                    <div class="bgm-deeporange p-20">
                                                        <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble zmdi-hc-fw mdc-text-blue"></span>
                                                        No existen registros, revisa la información o llama a la Coordinación de Informática, al área de sistemas</h5>
                                                    </div>
                                                </tr>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="col-sm-12 text-right">
            <div class="form-group m-b-10">
                <div class="fg-line">                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnSalir" OnClick="btnSalir_Click"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

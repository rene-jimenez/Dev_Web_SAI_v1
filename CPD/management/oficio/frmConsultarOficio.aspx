<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmConsultarOficio.aspx.vb" Inherits="CPD.frmConsultarOficio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
  


    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">OFICIO</a></li>
            <li class="active text-uppercase">CONSULTAR</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">OFICIO
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>LISTA DE OFICIOS</h2>
            </div>
            <div class="card-body card-padding">


                        <asp:ListView ID="lvsOficio" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>
                                            <th  style="width: 10%;" class="text-left">Doc. Interno</th>
                                            <th style="width: 10%;" class="text-left">Fecha</th>
                                             <th style="width: 10%;" class="text-left">DRM</th>
                                            <th style="width: 10%;" class="text-left">SAF</th>
                                            <th style="width: 15%;" class="text-left">Cargo presupuestal</th>
                                            <th style="width: 15%;" class="text-center">Área</th>
                                            <th style="width: 20%;" class="text-center">Asunto</th>  
                                            <th style="width: 10%;" class="text-center">Ver</th>
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
                                        <asp:Label ID="lblDocInterno" runat="server" Text='<%#IIf(Eval("esDocInterno"), "Si", "No")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblFecha" runat="server" Text='<%#String.Format(FormatDateTime(Eval("fechaCaptura")))%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDRM" runat="server" Text='<%#Eval("turnoDRM")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblSAF" runat="server" Text='<%#Eval("turnoSAF")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCargoPresupuestal" runat="server" Text='<%#Eval("_cargoPresupuestal")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblArea" runat="server" Text='<%#Eval("_area")%>'></asp:Label></td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblAsunto" runat="server" Text='<%#Eval("asunto")%>'></asp:Label></td>

                                    <td style="text-align: center">

                                        <asp:LinkButton runat="server" ID="btnSeleccionar" OnClick="btnSeleccionar_OnClick" CommandArgument='<%#Eval("id")%>' TabIndex='<%#Container.DataItemIndex %>' CssClass="btn btn-icon command-edit waves-effect waves-circle  waves-float bgm-bluegray">
												<span class="zmdi zmdi-eye"></span>
                                        </asp:LinkButton>
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
                <div class="row">
                <div class="col-sm-12 text-right">
                <div class="form-group m-b-30">
                    <div class="fg-line">
                        <asp:LinkButton class="btn btn-default" runat="server" ID="btn_default"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                    </div>
                </div>
            </div>
            </div>
            </div>
            
        </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
          
</asp:Content>

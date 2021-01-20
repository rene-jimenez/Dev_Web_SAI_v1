<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmListaSistemas.aspx.vb" Inherits="CPD.frmListaSistemas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
     <div class="container">
        <ol class="breadcrumb">
            <li><a><asp:LinkButton ID="lnkPrincipal" runat="server" PostBackUrl="~/management/default.aspx">PRINCIPAL</asp:LinkButton></a></li>
            <li><a href="#" class="text-uppercase">ARTÍCULO</a></li>
            <li class="active text-uppercase">RESULTADO</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">lista sistemas
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Sistemas</h2>
            </div>              
            <div class="card-body">
                    <asp:UpdatePanel runat ="server" UpdateMode="Conditional" ID="UpdatePanelLista"  >
                        <ContentTemplate >
                            <asp:ListView ID="lvsSistemas" runat="server"  ItemPlaceholderID="elementoPlaceHolder" >
                            <LayoutTemplate>
                                <table id="data-table-command" class="table table-striped table-vmiddle listaConFiltro">
                                    <thead class="cf">
                                        <tr>
                                           <th style="width: 5%;" class="text-left">NÚM.</th>
                                           <th style="width: 40%;" class="text-left">Nombre</th>
                                           <th style="width: 10%;" class="text-center">Tipo</th>
                                           <th style="width: 15%;" class="text-center">Año</th>
                                           <th style="width: 20%;" class="text-center">Acción</th>
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
                                         <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblNombre" ClientIDMode="AutoID" Text='<%#Eval("nombre")%>' ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbltipo" runat="server" Text='<%#Eval("tipo")%>'></asp:Label>
                                    </td>
                                    <td class="text-center">
                                        <asp:Label ID="lblanio" runat="server" Text='<%#Eval("año")%>'></asp:Label>
                                    </td>                                                                                                                
                                    <td runat="server"  class="text-center">
                                        <ul class ="actions">
                                            <li class ="dropdown">
                                                <a href="" data-toggle="dropdown" aria-expanded="false">
                                                      <i class="zmdi zmdi-more-vert"></i>
                                                </a>
                                                <ul class ="dropdown-menu dropdown-menu-right">
                                                    <li>
                                                         <asp:LinkButton ID="btnEditar" runat="server" OnClick="btnEditar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-border-color"></span> Editar</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                         <asp:LinkButton ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" ClientIDMode="AutoID" CommandArgument='<%#Eval("Id") %>'><span class="zmdi zmdi-power zmdi-hc-rotate-180"></span> Consultar</asp:LinkButton>
                                                    </li>
                                                </ul>

                                            </li>
                                        </ul>                                                                          
                                    </td>
                                    
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div class="alert alert-danger">No hay registros</div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        
                   
                   
        </div>
            <div class="card-footer">
                 <div class="col-sm-12 text-right">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <asp:LinkButton class="btn bgm-gray" runat="server" ID="btnCancelar"><i class="fa fa fa-times"></i> Cancelar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
             </div>  
               <br />
            <br />         
        </div>        
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

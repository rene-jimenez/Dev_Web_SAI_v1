<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAgregarAfectacion.aspx.vb" Inherits="CPD.frmAgregarAfectacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Afectación presupuestal</a></li>
            <li class="active text-uppercase">Alta</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Afectación presupuestal
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Agregar afectación presupuestal</h2>
            </div>
           <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-3 c-gray" style="background: #fdfafc">

                        <small><strong>Turno SAF</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="lblTurnoSAF" Text="-" runat="server" /></h5>
                        <br />
                        <small><strong>Turno DRM</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="lblTurnoDRM" Text="Aquí va el DRM" runat="server" /></h5>
                        <br />
                         <small><strong>Núm. Pedido</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="lblNumPedido" Text="Aquí va el num pedido" runat="server" /></h5>
                        <br />                       
                        <small><strong>Área solicitante</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="lblAreaSolicitante" Text="Aquí va el área" runat="server" /></h5>
                        <br />
                         <small><strong>Cargo presupuestal</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="lblCargoPresupuestal" Text="Aquí va el cargo" runat="server" /></h5>
                        <br />
                         <small><strong>Partida presupuestal</strong> </small>
                        <h5 class="m-t-5 f-400"> <asp:Label id="lblPartida" Text="Aquí va la partida" runat="server" /></h5>
                        <br />
                    </div>
                           <div class="col-sm-9">
                               <div class="row">
                                <div class="col-sm-6">
                                        <small><strong>Solicitud de expedición de pago a favor de:</strong> </small>
                                        <h5 class="m-5 f-600"> <asp:Label id="lblProveedor" runat="server" Text="Razón social proveedor" /></h5>
                                </div>
                                <div class="col-sm-6 block-header" style="background: #f4f7f7">
                                    <small><strong>Por la cantidad de:</strong> </small>
                                        <h5 class="m-t-5 f-600"> <asp:Label id="lblImportePago" Text="$0.00" runat="server" /></h5>

                                    <div class="card-body" id="listaOculta" runat="server" visible="false">
                        <asp:ListView ID="lvsListado" runat="server" ItemPlaceholderID="elemento">
                            <LayoutTemplate>
                                <table id="data-table" class="table table-striped table-vmiddle">
                                    
                                    <tbody>
                                        <asp:PlaceHolder ID="elemento" runat="server" />
                                    </tbody>
                                    <tr class="rating-list">
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>

                                <tr class="rating-list">
                                    <td>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Container.DataItemIndex + 1 %> '></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblArticulo" runat="server" Text='<%#Eval("_articulo")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblUnidad" runat="server" Text='<%#Eval("_unidadMedidaArticulo")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("cantidad")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("precioUnitario")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("_total")%>'></asp:Label></td>

                                </tr>

                            </ItemTemplate>
                            <InsertItemTemplate>
                            </InsertItemTemplate>
                           
                        </asp:ListView>
                       
                    </div>
                                    <ul class="actions p-t-15">
                                      <%--   <li class="dropdown">
                                              <a href="" data-toggle="dropdown" aria-expanded="false">
                                                  <i class="zmdi zmdi-search"></i></a>
                                             <ul class="dropdown-menu dropdown-menu-right">
                                                 <div class="pmo-contact">
                            <ul>
                                                
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblFechaElaboracion" runat="server" Text="aqui va fecha elaboración"></asp:Label>
                                </li>
                                
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblElaboro" runat="server" Text="elaboró:"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblReviso7" runat="server" Text="revisó:"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblAutorizo" runat="server" Text="autorizó"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblFechaSolicitud" runat="server" Text="fecha Solicitud"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblFechaAcordada" runat="server" Text="fecha Acordada"></asp:Label>
                                </li>
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblFechaRecibida" runat="server" Text="fecha Recibida"></asp:Label>
                                </li>
                               
                                <li class="ng-binding"><i class="zmdi zmdi-minus"></i>
                                    <asp:Label ID="lblObservaciones" runat="server" Text="aqui van las observaciones"></asp:Label>
                                </li>
                                 </ul>
                               </div>
                                            </ul>
                                         </li>--%>

                                         <li class="dropdown">
                                            <a href="" data-toggle="dropdown" aria-expanded="false">
                                                <i class="zmdi zmdi-more-vert"></i></a>

                                            <ul class="dropdown-menu dropdown-menu-right">
                                                <li>
                                                    <asp:LinkButton ID="lnkSubtotal" runat="server">Subtotal: $</asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton ID="lnkDescuento" runat="server">-Descuento: $</asp:LinkButton>
                                                </li>
                                                 <li>
                                                    <asp:LinkButton ID="lnkIva" runat="server" >+ Iva: $</asp:LinkButton>
                                                </li>
                                                 <li class="f-14" style="border-top:solid 1px #c4c7c7">
                                                    <asp:LinkButton ID="lnkTotal" runat="server">Total: $</asp:LinkButton>
                                                </li>

                                            </ul>
                                           </li>
                                   </ul>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group m-b-30">
                                    <div class="fg-line">
                                <label class="fg-label">Solicita: <span class="text-danger">(*)</span></label>
                                        <asp:DropDownList ID="cmbSolicita" CssClass="form-control conFiltro" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Selected="True" Text="Seleccione un elemento de la lista" />
                                        </asp:DropDownList>
                            </div>
                        </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group m-b-30">
                                    <div class="fg-line">
                                <label class="fg-label">Autoriza: <span class="text-danger">(*)</span></label>
                                        <asp:DropDownList ID="cmbAutoriza" CssClass="form-control conFiltro" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Selected="True" Text="Seleccione un elemento de la lista" />
                                        </asp:DropDownList>
                            </div>
                        </div>
                                </div>
                                                             <div class="col-sm-12">
                                    <div class="form-group m-b-30">
                                    <div class="fg-line">
                                    <label class="fg-label">Concepto <span class="text-danger">(*)</span></label>
                                    <asp:TextBox runat="server" id="txbConcepto" class="form-control input-sm" />  
                            </div>
                        </div>
                                </div>
                                   <div class="col-sm-12">
                                    <div class="form-group m-b-30">
                                    <div class="fg-line">
                                    <label class="fg-label">Marca de agua <span class="text-danger">(*)</span></label>
                                    <asp:TextBox runat="server" id="txbMarcaAgua" class="form-control input-sm" />  
                            </div>
                        </div>
                                </div>
  
                               </div>
                                
                       
                        <br />
                    </div>

                     
                </div>


               
               <asp:UpdatePanel runat="server" ID="updateBotonesAddCan">
                                <ContentTemplate>
                <div class="text text-right p-b-25 p-r-25">
                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" OnClick="btnCerrar_Click"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
                                    </ContentTemplate>
                   </asp:UpdatePanel>


            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

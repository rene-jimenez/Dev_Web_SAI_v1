<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmEditarOficio.aspx.vb" Inherits="CPD.frmEditarOficio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
        <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">OFICIO</a></li>
            <li class="active text-uppercase">EDITAR</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Oficio
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Editar oficio</h2>
            </div>
            <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno DRM <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbTurnoDRM" runat="server" class="form-control input-sm" AutoPostBack="true" placeholder="Escribe el numero DRM" />                                
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno SAF <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbTurnoSAF" runat="server" class="form-control input-sm" AutoPostBack="true" placeholder="Escribe el numero SAF" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label"></label>
                                <asp:DropDownList ID="cmbTurnoDRM" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                    <asp:ListItem Text="A" Enabled></asp:ListItem>
                                    <asp:ListItem Text="B"></asp:ListItem>
                                    <asp:ListItem Text="C"></asp:ListItem>
                                    <asp:ListItem Text="D"></asp:ListItem>
                                    <asp:ListItem Text="E"></asp:ListItem>
                                    <asp:ListItem Text="F"></asp:ListItem>
                                    <asp:ListItem Text="G"></asp:ListItem>
                                    <asp:ListItem Text="H"></asp:ListItem>
                                    <asp:ListItem Text="I"></asp:ListItem>
                                    <asp:ListItem Text="J"></asp:ListItem>
                                    <asp:ListItem Text="K"></asp:ListItem>
                                    <asp:ListItem Text="L"></asp:ListItem>
                                    <asp:ListItem Text="M"></asp:ListItem>
                                    <asp:ListItem Text="N"></asp:ListItem>
                                    <asp:ListItem Text="Ñ"></asp:ListItem>
                                    <asp:ListItem Text="O"></asp:ListItem>
                                    <asp:ListItem Text="P"></asp:ListItem>
                                    <asp:ListItem Text="Q"></asp:ListItem>
                                    <asp:ListItem Text="R"></asp:ListItem>
                                    <asp:ListItem Text="S"></asp:ListItem>
                                    <asp:ListItem Text="T"></asp:ListItem>
                                    <asp:ListItem Text="U"></asp:ListItem>
                                    <asp:ListItem Text="V"></asp:ListItem>
                                    <asp:ListItem Text="X"></asp:ListItem>
                                    <asp:ListItem Text="Y"></asp:ListItem>
                                    <asp:ListItem Text="Z"></asp:ListItem>


                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class ="col-sm-4">
                           <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha captura</label>
                                <asp:Label ID="lblFechaCap" runat="server" Text="Label" readonly="true" CssClass="form-control"></asp:Label>
                            </div>
                           </div>
                      </div>
                    <div class ="col-sm-2">
                         <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Es documento interno </label>                               
                                <asp:Label ID="lblDocInternoSi" runat="server" visible="false"  CssClass="form-control"> <span class="input-group-addon"><i class="fa fa-check-square fa-2"></i></span></asp:Label>
                                <asp:Label ID="lblDocInternoNo" runat="server" visible="false"  CssClass="form-control"><span class="input-group-addon"><i class="fa fa-window-close fa-2"></i></span></asp:Label>
                              </div>
                           </div>
                      </div>
                </div>
                    
                <div class="row">
                 <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Área <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbArea" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">                                        
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                    </div>
                 <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                 <label class="fg-label">Cargo Presupuestal <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbCargoPres" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">                                        
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                  </div>
             </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Responsable <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbResponsable" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                        
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Rubro <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbRubro" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">                                        
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Asunto <span class="text-danger">(*)</span></label>
                                <asp:TextBox runat="server" ID="txbAsunto" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Indicaciones <span class="text-danger">(*)</span></label>
                                <asp:TextBox runat="server" ID="txbIndicaciones" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                     <div class="col-sm-12">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Observaciones</label>
                                <asp:TextBox runat="server" ID="txbObservaciones" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                            </div>
                        </div>
                    </div>
                </div>
                 <div class="row">
                     <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Estado oficio</label>
                                <asp:TextBox ID="txbEstatusOficio" runat="server" class="form-control input-sm" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                     <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha atendido</label>
                                <asp:TextBox ID="txbFechaAtendido" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" placeholder="Seleccione una fecha" />
                            </div>
                        </div>
                    </div>                                          
                     <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Consolidar </label>
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkConsolidar" runat="server" />
                                            <i class="input-helper"></i>
                                         <small> Marque la casilla para consolidar el oficio</small>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                 </div>

                <div class="text text-right p-b-25 p-r-25">
                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnGuardar"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btn_Cancelar"><i class="fa fa-cog fa-spin"></i> Cancelar</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
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

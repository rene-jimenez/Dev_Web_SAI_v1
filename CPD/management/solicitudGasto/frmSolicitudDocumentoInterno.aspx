<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmSolicitudDocumentoInterno.aspx.vb" Inherits="CPD.frmSolicitudDocumentoInterno" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">




<div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Gastos</a></li>
            <li class="active text-uppercase">Solicitud de gastos</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Solicitud de gastos (Documento Interno)
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Agregar solicitud</h2>
            </div>
            <div class="card-body card-padding">
                <div class="row">

                    <div runat="server" id="sinDRM">
                        <div class="col-sm-4">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <label class="fg-label">Turno DRM </label>
                                    <asp:TextBox ID="txbFolioDocumentoInterno" runat="server" class="form-control input-sm" disabled="" />

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group m-b-30">
                                <div class="fg-line text-center">
                                    <label class="fg-label">Editar el DRM </label>
                                    <br />                                    
                                    <asp:LinkButton ID="btnDRMEditar" runat="server" CssClass="btn command-edit bgm-orange" OnClick="btnDRMEditar_Click"><span class="zmdi zmdi-border-color"></span></asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </div>
                  

                    <div runat="server" id="conDRM">
                        <div class="col-sm-4">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <label class="fg-label">Turno DRM <span class="text-danger">(*)</span></label>
                                    <asp:TextBox ID="txbTurnoDRM" runat="server" MaxLength="6" class="form-control input-sm" AutoPostBack="true" placeholder="Escribe el numero DRM" />

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <div class="form-group m-b-30">
                                <div class="fg-line">
                                    <label class="fg-label"></label>
                                    <asp:DropDownList ID="cmbTurnoDRM" runat="server" CssClass="form-control conFiltro input-sm" AppendDataBoundItems="True">
                                        <asp:ListItem Text="A"></asp:ListItem>
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
                        <div class="col-sm-1">
                            <div class="form-group m-b-30">
                                <div class="fg-line text-center">
                                    <label class="fg-label">Regresar</label>   <br />                                 
                                    <asp:LinkButton ID="btnDRMRegresar" runat="server" CssClass="btn command-edit bgm-gray" ><i class="fa fa-undo" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                       

                    </div>

                    <br />
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha elaboración </label>
                                <asp:TextBox ID="txbFechaElaboracion" runat="server" class="form-control input-sm" disabled="" />

                            </div>
                        </div>
                    </div>
                    

                    <div class="col-sm-6" id="area" runat="server">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Área <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbArea" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Seleccione un elemento de la lista" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6" id="cargoPres" runat="server">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Cargo presupuestal <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbCargoPres" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Seleccione un elemento de la lista" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-6" id="partidaPres" runat="server">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Partida presupuestal <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbPartidaPres" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Seleccione un elemento de la lista" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6" id="importe" runat="server">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Importe <span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbImporte" runat="server" class="form-control input-sm" placeholder="Importe" />
                                
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6" id="marcaAgua" runat="server">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Marca de agua <span class="text-danger">(*)</span></label>
                                <asp:TextBox runat="server" ID="txbMarcaAgua" MaxLength="300" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6" id="concepto" runat="server">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Concepto <span class="text-danger">(*)</span></label>
                                <asp:TextBox runat="server" ID="txbConcepto" MaxLength="300" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text text-right p-b-25 p-r-25">
                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click"><i class="fa fa-plus-circle"></i> Agregar</asp:LinkButton>
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" OnClick="btnCerrar_Click"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
     
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">

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


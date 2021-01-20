<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmSolicitudEditar.aspx.vb" Inherits="CPD.frmSolicitudEditar" %>
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
            <h2 class="active text-uppercase">Solicitud de gastos
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Modificar solicitud</h2>
            </div>
           <div class="card-body card-padding">
                <div class="row">

                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno SAF </label>
                                <asp:TextBox ID="txbTurnoSAF" runat="server" class="form-control input-sm" disabled=""/>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno DRM </label>
                                <asp:TextBox ID="txbTurnoDRM" runat="server" class="form-control input-sm" disabled="" />

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
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
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="txbImporte" ErrorMessage="El campo importe solo acepta números enteros con 2 decimales P. Ejem. 17.50"
                                    ValidationExpression="\d+(.\d{1,2})?"></asp:RegularExpressionValidator>
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
                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnModificar"><i class="fa fa-pencil-square-o"></i> Modificar</asp:LinkButton>                   
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar"><i class="fa fa-undo"></i> Regresar</asp:LinkButton>
                </div>


            </div>
        </div>
    </div>
    
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


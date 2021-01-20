<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmComplementarOficio.aspx.vb" Inherits="CPD.frmComplementarOficio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
        <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Oficio</a></li>
            <li class="active text-uppercase">Complementar</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Oficio
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>Complementar oficio</h2>
            </div>
           <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno DRM</label>
                                <asp:TextBox ID="txbTurnoDRM" runat="server" class="form-control input-sm" ReadOnly="true" />                                
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno SAF</label>
                                <asp:TextBox ID="txbTurnoSAF" runat="server" class="form-control input-sm" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Área</label>
                                     <asp:TextBox ID="txbArea" runat="server" class="form-control input-sm" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Asunto</label>
                                <asp:TextBox runat="server" ID="txbAsunto" MaxLength="300" CssClass="form-control" TextMode="MultiLine" ReadOnly="true" Rows="5" />
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Responsable <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbResponsable" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                    </div><div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Rubro <span class="text-danger">(*)</span></label>
                                <div class="select">
                                    <asp:DropDownList ID="cmbRubro" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-12">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Indicaciones <span class="text-danger">(*)</span></label>
                                <asp:TextBox runat="server" ID="txbIndicaciones"  CssClass="form-control" TextMode="MultiLine"  Rows="5" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text text-right p-b-25 p-r-25">
                    <asp:LinkButton class="btn btn-success" runat="server" OnClick="btnComplementar_Click" ID="btnComplementar"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                    <asp:LinkButton class="btn btn-default" runat="server" OnClick="btnCerrar_Click" ID="btnCerrar"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
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
     <script type="text/javascript">
        function HideModal()
        {
            $('#preventClick').modal("hide");
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function showModal()
        {
            $('#preventClick').modal({ backdrop: 'static', 'keyboard': true, 'show': true });
        }
       
        with (Sys.WebForms.PageRequestManager.getInstance()) {
            add_endRequest(carga);
        }
       

        
    </script>
</asp:Content>

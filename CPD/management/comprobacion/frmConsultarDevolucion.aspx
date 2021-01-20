<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmConsultarDevolucion.aspx.vb" Inherits="CPD.frmConsultarDevolucion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">

    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Comprobación de gastos</a></li>
            <li class="active text-uppercase">Consulta</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Consultar Devolución de gastos
            </h2>
        </div>
        <div class="clearfix"></div>

        <div class="row">
            <div class="col-md-3">
                <div class="card">
                    <div class="card-header ch-alt">
                        <h2>Oficio:<asp:Label ID="lblDRM" runat="server" ></asp:Label>
                            <small>
                                <asp:Label runat="server" ID="lblFechaElaboracion" Text="Fecha de elaboración: 18/08/2017"></asp:Label></small>
                        </h2>
                    </div>
                    <div class="card-body">
                        <ul class="p-l-0">
                            <li class="list-group-item">
                                <h5><strong>SAF:</strong><asp:Label ID="lblSAF" runat="server" Text=""></asp:Label></h5>
                            </li>
                            <li class="list-group-item">
                                <h5><strong>N° Pedido:</strong><asp:Label ID="lblPedido1" runat="server" Text=""></asp:Label></h5>
                            </li>
                            <li class="list-group-item">
                                <h5><strong>F-Tesoreria Solicitud:</strong><asp:Label ID="lblFolioTesoreriaSolicitud" runat="server" Text=""></asp:Label></h5>
                            </li>
                            <li class="list-group-item">
                                <h5><strong>F-Caja Solicitud:</strong><asp:Label ID="lblFolioCajaSolicitud" runat="server" Text=""></asp:Label></h5>
                            </li>
                            <li class="list-group-item">
                                <h5><strong>F-Tesoreria Alcance:</strong><asp:Label ID="lblFolioTesoreriaAlcance" runat="server" Text=""></asp:Label></h5>
                            </li>
                            <li class="list-group-item">
                                <h5><strong>F-Caja Alcance:</strong><asp:Label ID="lblFolioCajaAlcance" runat="server" Text=""></asp:Label></h5>
                            </li>
                        </ul>
                    </div>

                </div>
            </div>
            <div class="col-md-9">
                <div class="card" id="profile-main">
                    <div class="pm-overview " style="overflow: visible;">
                        <div class="pmo-block pmo-contact">
                            <ul>
                                <li class="list-group-item">
                                    <h5><strong>Importe total a comprobar:</strong><asp:Label ID="lblTotalComprobar" runat="server" ></asp:Label></h5>
                                </li>
                                <li class="list-group-item">
                                    <h5><strong>Importe a Devolver:</strong><asp:Label ID="lblDevolucion" runat="server" ></asp:Label></h5>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="pm-body clearfix">

                        <div class="pmb-block">
                            <div class="pmbb-header">
                                <h2><i class="zmdi zmdi-refresh m-r-10 c-bluegray"></i>Datos de la devolución </h2>
                            </div>

                            <div class="pmbb-body p-l-30">
                                <div class="pmbb-view">
                                      <dl class="dl-horizontal">
                                        <dt class="p-t-10">Con fecha: </dt>
                                        <dd>
                                                <asp:Label runat="server" ID="lblFechas" ></asp:Label>
                                           </dd>
                                        </dl>
                                    <dl class="dl-horizontal">
                                        <dt class="p-t-10">Cargo presupuestal: </dt>
                                        <dd>
                                                <asp:Label runat="server" ID="lblCargoPresupuestal" ></asp:Label>
                                        </dd>
                                    </dl>
                                        <dl class="dl-horizontal">
                                            <dt class="p-t-10">Autoriza: </dt>
                                            <dd><asp:Label runat="server" ID="lblAutoriza" ></asp:Label>
                                            </dd>
                                        </dl>
                                    <dl class="dl-horizontal">
                                            <dt class="p-t-10">Autoriza: </dt>
                                            <dd><asp:Label runat="server" ID="lblResponsable" ></asp:Label>
                                            </dd>
                                        </dl>
                                        <dl class="dl-horizontal">
                                            <dt class="p-t-10">Concepto: </dt>
                                            <dd>
                                                <asp:Label runat="server" ID="lblConcepto" ></asp:Label>
                                            </dd>
                                        </dl>

                                        <div class="text text-right m-t-25 p-b-25 p-r-25">
                                            <asp:LinkButton class="btn btn-primary" runat="server" ID="btnImprimir" TabIndex="5"><i class="fa fa-print"></i> Imprimir</asp:LinkButton>
                                            <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="6"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                        </div>
                                </div>
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

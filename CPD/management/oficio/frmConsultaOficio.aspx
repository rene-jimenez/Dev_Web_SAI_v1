<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmConsultaOficio.aspx.vb" Inherits="CPD.frmConsultaOficio" %>
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
                <h2>Consulta oficio</h2>
            </div>
            <div class="card-body card-padding">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno DRM</label>
                                <asp:Label ID="lblDRM" runat="server" class="form-control input-sm" />                                
                                <%--<asp:TextBox ID="txbTurnoSAF" runat="server" class="form-control input-sm" AutoPostBack="true" placeholder="Escribe el numero SAF" disabled/>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Turno SAF</label>
                                <asp:Label ID="lblTurno" runat="server" class="form-control input-sm"/>
                                <%--<asp:TextBox ID="txbTurnoDRM" runat="server" class="form-control input-sm" AutoPostBack="true" placeholder="Escribe el numero DRM" disabled/>--%>
                            </div>
                        </div>
                    </div>
                    <%--<div class="col-sm-1">
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
                    </div>--%>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Área</label>
                                <asp:Label ID="lblArea" runat="server" class="form-control input-sm"/>
                                <%--<asp:TextBox ID="txbArea" runat="server" class="form-control input-sm" disabled></asp:TextBox> --%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Responsable</label>
                                <asp:Label ID="lblResponsable" class="form-control input-sm" runat="server"/>
                                <%--<asp:TextBox ID="txbResponsable" class="form-control input-sm" runat="server" disabled/>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group m-b-30 ">
                            <div class="fg-line">
                                <label class="fg-label">Rubro</label>
                                <asp:Label ID="lblRubro" class="form-control input-sm" runat="server"/>
                                <%--<asp:TextBox ID="txbRubro" class="form-control input-sm" runat="server" disabled/>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Asunto</label>
                                <asp:Label ID="lblAsunto" CssClass="form-control" runat="server"/>
                                <%--<asp:TextBox runat="server" ID="txbAsunto" CssClass="form-control" TextMode="MultiLine" Rows="3" disabled/>--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Indicaciones</label>
                                <asp:Label runat="server" ID="lblIndicaciones" CssClass="form-control"/>
                                <%--<asp:TextBox runat="server" ID="txbIndicaciones" CssClass="form-control" TextMode="MultiLine" Rows="3" disabled/>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Cargo Presupuestal</label>
                                <asp:Label ID="lblCargoPresupuestal" CssClass="form-control" runat="server"/>
                                <%--<asp:TextBox runat="server" ID="txbAsunto" CssClass="form-control" TextMode="MultiLine" Rows="3" disabled/>--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha Captura</label>
                                <asp:Label runat="server" ID="lblFechaCaptura" CssClass="form-control"/>
                                <%--<asp:TextBox runat="server" ID="txbIndicaciones" CssClass="form-control" TextMode="MultiLine" Rows="3" disabled/>--%>
                            </div>
                        </div>
                    </div>
                    <%--<div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Documento Interno </label>
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkDocInterno" runat="server"/>
                                            <i class="input-helper"></i>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
                
                <div class="row">
                     <div class="col-sm-12">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Observaciones</label>
                                <asp:Label runat="server" ID="lblObservaciones" CssClass="form-control"/>
                                <%--<asp:TextBox runat="server" ID="txbObservaciones" CssClass="form-control" TextMode="MultiLine" Rows="2" disabled/>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Estado oficio</label>
                                <asp:Label ID="lblEstatusOficio" runat="server" class="form-control input-sm" />
                                <%--<asp:TextBox ID="txbEstatusOficio" runat="server" class="form-control input-sm" ReadOnly="true" disabled/>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Fecha atendido</label>
                                <asp:Label ID="lblFechaAtendido" class="form-control input-sm" runat="server" />
                                <%--<asp:TextBox ID="txbFechaAtendido" runat="server" class="form-control input-sm conMascaraFecha conVentanaFecha" disabled/>--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Atendido </label>
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkAtenderOficio" runat="server" />
                                            <i class="input-helper"></i>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Consolidar </label>
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkConsolidar" runat="server" />
                                            <i class="input-helper"></i>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2" >
                        <div class="form-group m-b-30">
                            <div class="fg-line">
                                <label class="fg-label">Documento interno </label>
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkDocInterno" runat="server"/>
                                            <i class="input-helper"></i>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="text text-right p-b-25 p-r-25">                    
                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnRegresar"><i class="fa fa-undo"></i> Regresar</asp:LinkButton>
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
</asp:Content>

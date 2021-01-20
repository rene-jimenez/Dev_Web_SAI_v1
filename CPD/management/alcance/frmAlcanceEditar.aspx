<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAlcanceEditar.aspx.vb" Inherits="CPD.frmAlcanceEditar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="conUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Alcance</a></li>
            <li class="active text-uppercase">Editar Alcance</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Alcance
            </h2>
        </div>
        <div class="clearfix"></div>
        


        <div class="card" id="profile-main">
                        <div class="pm-overview c-overflow mCustomScrollbar " style="overflow: visible;">
                            <div class="mCustomScrollBox mCSB_vertical_horizontal mCSB_outside" style="max-height: none;" tabindex="0">
                                <div id="mCSB_3_container" class="mCSB_container mCS_no_scrollbar_x" style="position: relative; top: 0px; left: 0px; width: 100%;" dir="ltr">
                                    <div class="pmo-block pmo-contact">
                                 <h2><asp:Label ID="LBLTITULO" runat="server" Text="Datos de la solicitud de gasto" CssClass="f-15"></asp:Label></h2>
                       

                            <ul>
                                <li class="ng-binding">
                                     <b> DRM :</b>
                               <asp:Label ID="lblDRM" runat="server" Text="aqui va el Turno DRM" ></asp:Label> 
                                </li>

                                <li class="ng-binding">
                                    <b> SAF : </b>
                                    <asp:Label ID="lblSAF" runat="server" Text="aqui va el SAF"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Área :</b>
                                    <asp:Label ID="lblNombreArea" runat="server" Text="aqui va el área"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Cargo :</b>
                                    <asp:Label ID="lblCargo" runat="server" Text="aqui va el cargo pres"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Concepto :</b>
                                     <asp:Label ID="lblConcepto" runat="server" Text="aqui va el concepto" CssClass="text text-uppercase"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Folio Tesoreria :</b>
                                    <asp:Label ID="lblFolioTesoreria" runat="server" Text="f-tesorería ant."></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Folio Caja :</b>
                                    <asp:Label ID="lblFolioCaja" runat="server" Text="f-caja ant."></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Fecha recepción :</b>
                                     <asp:Label ID="lblFechaLiberacion" runat="server" Text="fecha recepcion" CssClass="text text-uppercase"></asp:Label> 
                                </li>
                                <li class="ng-binding">
                                    <b>Fecha captura :</b>
                                     <asp:Label ID="lblFechaCaptura" runat="server" Text="fecha captura" CssClass="text text-uppercase"></asp:Label> 
                                </li>
                                
                            </ul>
                               
                            </div>
                            
                        </div>

                            </div>
                          
                        </div>

                       
            <div class="pm-body clearfix">
                <div class="pmb-block">

                     <div class="pmbb-header">
                                    <h2><i class="zmdi zmdi-assignment m-r-10"></i> Detalles de la solicitud</h2>
                    </div>
                    <div class="pmbb-body p-l-50">
                        <div class="pmbb-view">
                            
                            <table id="data-table" class="table table-striped table-vmiddle">
                                <thead>
                                    <tr class="success">
                                        <th data-column-id="area" width="15%">Área</th>
                                        <th data-column-id="partida" width="35%">Partida</th>
                                        <th data-column-id="descripcion" width="35%">Descripción</th>
                                        <th data-column-id="importe" width="15%">Importe</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <tr style="border-top: double 2px #525353; background-color: #f7f7f7">

                                        <td>
                                            <asp:Label ID="lblArea" runat="server" ></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblPartida" runat="server" ></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblDescripcion" runat="server" ></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblimporte" runat="server" ></asp:Label></td>

                                    </tr>

                                </tbody>
                                <tfoot>
                                    <tr style="border-top: solid 1.5px #808080; vertical-align: middle; background-color: #ffffff">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </tfoot>
                            </table>


                        </div>
                    </div>
                   
                    <div style="background: #f4f7f7">
                        <div class="pmbb-header f-15">
                            <h2><i class="zmdi zmdi-plus-circle m-r-10 c-deeporange"></i>Datos de la solicitud de alcance</h2>

                        </div>
                        <div class="pmbb-body p-l-50">
                            <div class="">
                                <div class="col-sm-12" id="divImporte" runat="server">
                                    <div class="col-sm-10">
                                    <dl class="dl-horizontal">
                                        <dt class="p-t-10">Importe: <span class="text-danger">(*)</span></dt>
                                       <dd>
                                           <div class="fg-line">
                                                <asp:TextBox ID="txbImporte"  runat="server" class="form-control" MaxLength ="9" placeholder=" 0.00" onkeydown="return thisIsDouble(event);" TabIndex="1"></asp:TextBox>
                                            </div>

                                        </dd>
                                    </dl>
                                    <dl class="dl-horizontal">
                                    <dt class="p-t-10">Partida Presupuestal: <span class="text-danger">(*)</span></dt>
                                    <dd>
                                           <div class="fg-line">
                                            
                                                <div class="select">
                                                <asp:DropDownList ID="cmbPartida" runat="server" CssClass="form-control conFiltro" AppendDataBoundItems="True" TabIndex="2">
                                                    <asp:ListItem Text="Seleccione un elemento de la lista" Enabled></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </dd>
                                </dl>

                                    </div>

                                </div>

                                
                                <div class="">

                                <div class="text text-right m-t-25 p-b-25 p-r-25">
                                    <asp:LinkButton class="btn btn-success" runat="server" ID="btnModificar" TabIndex="3"><i class="fa fa-check-circle"></i> Modificar</asp:LinkButton>
                                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="4"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
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
    
     <script>
        function filterFloat(evt, input) {
            // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
            var key = window.Event ? evt.which : evt.keyCode;
            var chark = String.fromCharCode(key);
            var tempValue = input.value + chark;
            if (key >= 48 && key <= 57) {
                if (filter(tempValue) === false) {
                    return false;
                } else {
                    return true;
                }
            } else {
                if (key == 8 || key == 13 || key == 0) {
                    return true;
                } else if (key == 46) {
                    if (filter(tempValue) === false) {
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    return false;
                }
            }
        }
        function filter(__val__) {
            var preg = /^([0-9]+\.?[0-9]{0,2})$/;
            if (preg.test(__val__) === true) {
                return true;
            } else {
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
    

</asp:Content>

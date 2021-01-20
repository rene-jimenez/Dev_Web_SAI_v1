<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmAlcanceActualizar.aspx.vb" Inherits="CPD.frmAlcanceActualizar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="#" class="text-uppercase">Alcance</a></li>
            <li class="active text-uppercase">Actualizar Alcance</li>
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
                               <asp:Label ID="lblDRM" runat="server" Text="aqui va el Turno DRM" CssClass="f-15"></asp:Label>
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
                                    <b>Folio tesoreria :</b>
                                   <asp:Label ID="lblFolioTesoreria" runat="server" Text="f-tesorería ant."></asp:Label>
                                </li>
                                <li class="ng-binding">
                                    <b>Folio caja :</b>
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
                        <h2><i class="zmdi zmdi-plus-circle-o-duplicate m-r-10 c-deeporange"></i> Detalles de la solicitud</h2>

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
                            <hr />
                            <div class="pmbb-body p-l-30">
                                <div class="">
                                    <div class="col-sm-12" id="divActualiza" runat="server">

                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-10">

                                                    <dl class="dl-horizontal">
                                                        <dt class="p-t-10">Importe Alcance: </dt>
                                                        <dd>
                                                            <div class="fg-line">
                                                                <asp:TextBox ID="txtImporteAlcance" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                            </div>

                                                        </dd>
                                                    </dl>
                                                    <dl class="dl-horizontal">
                                                        <dt class="p-t-10">Folio tesoreria:  <span class="text-danger">(*)</span></dt>
                                                        <dd>
                                                            <div class="fg-line">
                                                                <asp:TextBox ID="txbNuevoFolioTesoreri" runat="server" AutoPostBack="true" placeholder="nuevo folio de tesoreria" onKeyPress="return acceptNum(event);" MaxLength="6" class="form-control" TabIndex="1"></asp:TextBox>
                                                            </div>

                                                        </dd>
                                                    </dl>

                                                    <dl class="dl-horizontal">
                                                        <dt class="p-t-10">Folio caja:  <span class="text-danger">(*)</span></dt>
                                                        <dd>
                                                            <div class="fg-line">
                                                                <asp:TextBox ID="txbNuevoFolioCaja" runat="server" AutoPostBack="true" placeholder="nuevo folio de caja" class="form-control" onKeyPress="return acceptNum(event);" MaxLength="6" TabIndex="2"></asp:TextBox>
                                                            </div>

                                                        </dd>
                                                    </dl>


                                                    <dl class="dl-horizontal">
                                                        <dt class="p-t-10">Fecha recepción:  <span class="text-danger">(*)</span></dt>
                                                        <dd>
                                                            <div class="fg-line">
                                                                <%--<div class="col-sm-7">--%>
                                                                <asp:TextBox ID="txbFechaRecepcion" runat="server" class="form-control conMascaraFecha conVentanaFecha" TabIndex="3" />
                                                                <%-- <span class="input-group-addon"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>--%>
                                                                <%--</div>--%>
                                                            </div>

                                                        </dd>
                                                    </dl>

                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                     
                                    </div>
                                       </div>
                                      <div class="">
                                    <asp:UpdatePanel UpdateMode="Conditional" ID="updateAgregarArticulo" runat="server">
                                     <ContentTemplate>
                                        <div class="text text-right m-t-25 p-b-25 p-r-25">
                                            <asp:LinkButton class="btn btn-success" runat="server" ID="btnActualizar" TabIndex="4"><i class="fa fa-check-circle"></i> Actualizar</asp:LinkButton>
                                            <asp:LinkButton class="btn btn-default" runat="server" ID="btnCerrar" TabIndex="5"><i class="fa fa-cog fa-spin"></i> Cerrar</asp:LinkButton>
                                        </div>
                                           </ContentTemplate>
                                     </asp:UpdatePanel>
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
     <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".conVentanaFecha").datepicker({ format: 'dd/mm/yyyy', autoclose: true });
                   }
               });
           };
</script>
     <script type="text/javascript">
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {
                       $(".conMascaraFecha").inputmask("99/99/9999");
                   }
               });
           };

</script>
     <script>
        var nav4 = window.Event ? true : false;
        function acceptNum(evt) {
            var key = nav4 ? evt.which : evt.keyCode;
            return (key <= 13 || (key >= 48 && key <= 57) || key == 40);
        }

    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

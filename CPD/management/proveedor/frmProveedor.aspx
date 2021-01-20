<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmProveedor.aspx.vb" Inherits="CPD.frmProveedor1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
function valida(e){
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla==8){
        return true;
    }
        
    // Patron de entrada, en este caso solo acepta numeros
    patron =/[0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}
</script>
    <script>
    function soloLetras(e){
       key = e.keyCode || e.which;
       tecla = String.fromCharCode(key).toLowerCase();
       letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
       especiales = "8-37-39-46";

       tecla_especial = false
       for(var i in especiales){
            if(key == especiales[i]){
                tecla_especial = true;
                break;
            }
        }

        if(letras.indexOf(tecla)==-1 && !tecla_especial){
            return false;
        }
    }
</script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="sinUpdatePanel" runat="server">
    <div class="container">
        <ol class="breadcrumb">
            <li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
            <li><a href="frmConsultaProveedor.aspx" class="text-uppercase">PROVEEDOR</a></li>
            <li class="active text-uppercase">ALTA PROVEEDOR</li>
        </ol>
        <div class="block-header">
            <h2 class="active text-uppercase">Proveedor
            </h2>
        </div>
        <div class="clearfix"></div>
        <div class="card">
            <div class="card-header ch-alt bgm-bluegray">
                <h2>
                    <asp:Label runat="server" ID="lblTitulo"></asp:Label> proveedor</h2>
            </div>
            <div class="card-body card-padding">
                <div class="form-wizard form-wizard-basic form-wizard-horizontal fw-container">          
                    <div class="form-wizard-nav">
                        <div class="progress">
                            <div class="progress-bar progress-bar-primary"></div>
                        </div>
                        <ul class="nav nav-justified">
                            <li class="active"><a href="#tab1" data-toggle="tab"><span class="step">1</span> <span class="title">Datos de proveedor</span></a></li>
                            <li><a href="#tab2" data-toggle="tab"><span class="step">2</span> <span class="title">Teléfono(s)</span></a></li>
                        </ul>


                    </div>
                    <div class="tab-content clearfix">
                        <div class="tab-pane active" id="tab1">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Nombre completo<span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbNombre" runat="server" class="form-control input-sm" MaxLength="50" onkeypress="return soloLetras(event)" placeholder="Juan Pérez" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Contacto</label>
                                <asp:TextBox ID="txbContacto" runat="server" class="form-control input-sm" MaxLength="50" onkeypress="return soloLetras(event)" placeholder="Persona para contactar al proveedor" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">R. F. C.<span class="text-danger">(*)</span></label>
                                            <asp:TextBox ID="txbRfc" runat="server" class="form-control input-sm" MaxLength="50" placeholder="R.F.C. del proveedor" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Código postal</label>
                                            <asp:TextBox ID="txbCodigoPostal" runat="server" class="form-control input-sm" MaxLength="5" onkeypress="return valida(event)" placeholder="C.P." />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Estado</label>
                                            <asp:TextBox ID="txbEstado" runat="server" class="form-control input-sm" MaxLength="50" onkeypress="return soloLetras(event)" placeholder="Entidad (estado) del proveedor" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Ciudad<span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbCiudad" runat="server" class="form-control input-sm" MaxLength="50" onkeypress="return soloLetras(event)" placeholder="Ciudad de procedencia" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Colonia</label>
                                            <asp:TextBox ID="txbColonia" runat="server" class="form-control input-sm" MaxLength="50" placeholder="Colonia" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Domicilio <span class="text-danger">(*)</span></label>
                                            <asp:TextBox ID="txbdomicilio" runat="server" class="form-control input-sm" MaxLength="50" placeholder="Domicilio del proveedor" />
                                        </div>
                                    </div>
                                </div>


                                <div class="col-sm-6">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Domicilio fiscal</label>
                                            <asp:TextBox ID="txbdomicilioFiscal" runat="server" class="form-control input-sm" MaxLength="50" placeholder="Domicilio fiscal del proveedor" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group m-b-30">
                                        <div class="fg-line">
                                            <label class="fg-label">Giro</label>
                                            <asp:TextBox ID="txbGiro" runat="server" class="form-control input-sm" MaxLength="50" onkeypress="return soloLetras(event)" placeholder="Giro comercial del proveedor" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab2">
                            <asp:UpdatePanel runat="server" UpdateMode ="Conditional"  id="updatepaneltab2">
                                <ContentTemplate>
                                    
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group m-b-30">
                                                <div class="fg-line">
                                                    <label class="fg-label">Larga distancia<span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txblargaDistancia" runat="server" class="form-control input-sm" onkeypress="return valida(event)" MaxLength="5" placeholder="045" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group m-b-30">
                                                <div class="fg-line">
                                                    <label class="fg-label">Número telefónico<span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbNumero" runat="server" class="form-control input-sm" onkeypress="return valida(event)" MaxLength="10" placeholder="1234567890" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group m-b-30">
                                                <div class="fg-line">
                                                    <label class="fg-label">Extensión </label>
                                <asp:TextBox ID="txbExtension" runat="server" class="form-control input-sm" onkeypress="return valida(event)" MaxLength="5" placeholder="115" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group m-b-30">
                                                <div class="fg-line">
                                                    <label class="fg-label">Tipo<span class="text-danger">(*)</span></label>
                                <asp:TextBox ID="txbTipo" runat="server" class="form-control input-sm" MaxLength="20" onkeypress="return soloLetras(event)" placeholder="Celular" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class ="col-sm-2">
                                            <div class="form-group m-b-30">
                                                <div class="fg-line">
                                                    <div class="btn-demo">
                                                        <asp:LinkButton ClientIDMode="AutoID" runat="server" ID="LinkButton1" OnClick="btnAgregarTelefono_Click" ValidationGroup="val" class="btn btn-info">+ Telefóno(s)
                                                    <i class="fa fa-info-circle"></i>
                                                </asp:LinkButton>
                                            </div>
                                                </div>
                                            </div>
                                        </div>
                                        <p></p>
                                        <div></div>
                                        <div class="col-md-12 col-sm-12" style="align-content: stretch">
                                            <div id="divtelefonos" visible="true" runat="server" class="card">
                                                <div class="card-header ch-alt bgm-bluegray">
                                                    <h2>Teléfonos del proveedor</h2>
                                                </div>
                                                <div class="card-body">
                                                    <asp:ListView ID="lvsElemento" runat="server" ItemPlaceholderID="elementoPlaceHolder">
                                                        <LayoutTemplate>
                                                            <table class="table table-hover table-striped table-flip-scroll cf" id="filtroGJS">
                                                                <thead>
                                                                    <tr class="uppercase">
                                                                        <th style="text-align: center">Larga distancia</th>
                                                                        <th style="text-align: center">Número</th>
                                                                        <th style="text-align: center">Extensión</th>
                                                                        <th style="text-align: center">Tipo</th>
                                                                        <th style="text-align: center">Acción</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:PlaceHolder ID="elementoPlaceHolder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblLargaDistancia" runat="server" Text='<%#Eval("codigoLargaDistancia")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblNumero" runat="server" Text='<%#Eval("numero")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblExtension" runat="server" Text='<%#Eval("extension")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lbltipo" runat="server" Text='<%#Eval("tipo")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <div class="btn-demo">
                                                                        <asp:LinkButton ClientIDMode="AutoID" runat="server" ID="btnEliminartel" OnClick="btnEliminartel_Click" TabIndex='<%#Container.DataItemIndex %>' CommandArgument='<%#Eval("id")%>' CssClass="btn btn-icon command-delete waves-effect waves-circle  waves-float bgm-deeporange">
                                                                     <i class="zmdi zmdi-delete"></i>
                                                                        </asp:LinkButton>

                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <EmptyDataTemplate>
                                                            <tr>
                                                                <div class="alert alert-danger">
                                                                    <h5 class="c-white"><span class="zmdi zmdi-alert-triangle zmdi-hc-2x animated infinite wobble  zmdi-hc-fw mdc-text-blue"></span>
                                                                    Lo sentimos, No hay registros
                                                                </div>
                                                            </tr>
                                                        </EmptyDataTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-right">
                                        <div class="form-group m-b-30">
                                            <div class="fg-line">
                                                <asp:LinkButton class="btn btn-success" runat="server" ID="lnkGuardarSub"><i class="fa fa-check-circle"></i> Guardar</asp:LinkButton>
                                                <asp:LinkButton class="btn bgm-gray" runat="server" ID="lnkCerrar2"><i class="fa fa-times"></i> Cancelar</asp:LinkButton>                                               
                                            </div>
                                        </div>
                                    </div>
                                          
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                   </div>

  <div class="text text-center">
                                <ul class="fw-footer pagination wizard text-center" >
                                <li class="previous first"><a class="a-prevent" href=""><i><span class="zmdi zmdi-more-horiz"></span></i></a></li>
                                <li class="previous"><a class="a-prevent" href=""><i><span class="zmdi zmdi-long-arrow-left"></span></i></a></li>
                                <li class="next"><a class="a-prevent" href=""><i><span class="zmdi zmdi-long-arrow-right"></span></i></a></li>
                                <li class="next last"><a class="a-prevent" href=""><i><span class="zmdi zmdi-more-horiz"></span></i></a></li>
                            </ul>
                            </div>

                    <%-- </form>--%>
                </div>
               
            </div>
            <%--card body--%>
        </div>
    </div>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="conUpdatePanel" runat="server">
      <div class="modal fade" id="myModalConfirm" tabindex="-2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type:"button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <span class="text text-info"><h4 class="text-info">Confirmación</h4></span>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="grid simple horizontal gray" id="divRespuesta" runat="server">
                            <div class="grid-title">
                                <asp:Label ID="lblConfirmacionCuerpo" runat="server"/> 
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="text-right" style="padding:25px;">
                                <asp:linkButton ID="btnEventoConfirmar" runat="server" class="btn btn-success btn-sm" OnClick="btnEventoConfirmar_Click" OnClientClick="HideModal()"><i class="fa fa-thumbs-up"></i> Confirmar</asp:linkButton>
                                <%--<asp:Button ID="btnEventoConfirmar" Text="Confirmar" runat="server" class="btn btn-success"   />--%>
                                <asp:Button ID="btnCerrarConfirmacíon" Text="Cancelar" runat="server" class="btn btn-default"  data-dismiss="modal" aria-hidden="true" OnClientClick="HideModal()" />
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
		function carga() {
		    $(".listaConFiltro").DataTable();          
		}
		  
	</script>
</asp:Content>
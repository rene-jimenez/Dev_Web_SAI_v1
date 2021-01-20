<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/management/Site.Master" CodeBehind="frmReportes.aspx.vb" Inherits="CPD.frmReportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conUpdatePanel" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sinUpdatePanel" runat="server">
     <div class="container">
        <ol class="breadcrumb">
			<li><a href="../default.aspx" class="text-uppercase">PRINCIPAL</a></li>
			<li><a href="#"class="text-uppercase">Reportes</a></li>
			
		</ol>
			<div class="block-header">
				<h2 class="active text-uppercase">Reportes
					</h2>                
			</div>
    <div class="clearfix"></div>

         <div class="card">
			        <div class="card-header ch-alt bgm-bluegray">
			            <h2>Listado de reportes</h2>
			        </div>
			
			        <div class="card-body">
			        	 <table id="data-table" class="table table-striped table-vmiddle">
                            <thead>
                                <tr>
                                    <th data-column-id="id" data-type="numeric">Núm.</th>
                                    <th data-column-id="txt" width="55%">Reporte</th>
                                    <th data-column-id="axn" width="35%">Ver</th>
                                </tr>
                            </thead>
                             <tbody>
                                 <tr>
                                    <td>1</td>
                                     <td>Sujeto 1</td>
                                      <td>Sujeto 1</td>
                                </tr>
                                 <tr>
                                    <td>2</td>
                                     <td>Sujeto 2</td>
                                      <td>Sujeto 1</td>
                                </tr>
                                 <tr>
                                    <td>3</td>
                                     <td>Sujeto 3</td>
                                      <td>Sujeto 1</td>
                                </tr>
                             </tbody>
                            </table>
                    </div>
                </div>

       


	</div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jquery" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

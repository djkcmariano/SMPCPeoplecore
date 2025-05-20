<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/masterblank.master" AutoEventWireup="false" CodeFile="PageExpired.aspx.vb" Inherits="PageExpired" %>




<asp:Content ID="Content2" contentplaceholderid="cphBody" Runat="Server">

    <link rel="stylesheet" type="text/css" href="css/bootstrap/bootstrap.min.css" /> 

    <%--<div class="panel-body">
    <div class="form-group">
        <div class="col-md-12">
            <li class="list-group-item list-group-item-danger"><i class="fa fa-info-circle fa-lg"></i> To deactivate the password expiry just set the no. of days of expired into 0.</li>
            <i class="fa fa-exclamation-triangle fa-li fa-lg"></i>
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
        </div>
    </div>
    </div>--%>

        <div class="alert alert-danger" runat="server">
            <ul class="fa-ul" runat="server">
                <li runat="server">

                    <i class="fa fa-exclamation-circle fa-li fa-lg"></i>
                    <asp:PlaceHolder runat="server" ID="ph">
                        <asp:Label runat="server" ID="txtWarningMessage"></asp:Label>
                        <br /><br />
                        <div runat="server" ID="panelBack">
                            Please <asp:LinkButton ID="lnkBack" OnClick="lnkBack_Click" runat="server" Text="click here" Font-Bold="true" /> to go back in Log In page.            
                        </div> 
                    </asp:PlaceHolder>                    
                </li>
            </ul>
        </div>


</asp:Content>


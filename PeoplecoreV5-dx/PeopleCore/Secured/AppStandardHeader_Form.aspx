<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="AppStandardHeader_Form.aspx.vb" Inherits="Secured_AppStandardHeader_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:Panel runat="server" ID="pForm" />
                <div style="padding:20px;text-align:center;">
                    <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary" Visible="false" ValidationGroup="EvalValidationGroup" Text="Save" OnClick="lnkSave_Click" />
                </div>
            </div>
        </div>
    </div>    
</div>                   

</asp:Content>
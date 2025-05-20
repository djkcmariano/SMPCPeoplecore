<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfEvalTemplateForm.aspx.vb" Inherits="SecuredSelf_SelfEvalTemplateForm" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:Panel runat="server" ID="pForm" />
                <div style="padding:20px;text-align:center;">
                    <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary" ValidationGroup="EvalValidationGroup" Text="Save" OnClick="lnkSave_Click" />
                </div>
            </div>
        </div>
    </div>    
</div>                   

</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppStandardTemplateForm.aspx.vb" Inherits="Secured_AppStandardTemplateForm" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">
    function CheckItem(sender, args) {
        var chkControlId = sender.id.replace("cblValidator_", "");
        var options = document.getElementById(chkControlId).getElementsByTagName('input');
        var ischecked = false;
        args.IsValid = false;
        for (i = 0; i < options.length; i++) {
            var opt = options[i];
            if (opt.type == "checkbox") {
                if (opt.checked) {
                    ischecked = true;
                    args.IsValid = true;
                }
            }
        }
    }
</script>

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


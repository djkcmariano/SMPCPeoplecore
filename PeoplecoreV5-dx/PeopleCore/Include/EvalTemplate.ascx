<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EvalTemplate.ascx.vb" Inherits="Include_EvalTemplate" %>

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
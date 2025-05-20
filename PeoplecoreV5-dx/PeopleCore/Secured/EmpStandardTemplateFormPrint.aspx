<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmpStandardTemplateFormPrint.aspx.vb" Inherits="EmpStandardTemplateFormPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
<link rel='stylesheet' type='text/css' id='theme' href="../css/theme-light.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="page-content-wrap">             
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <asp:Label runat="server" ID="lblName" />
                        <asp:Panel runat="server" ID="pForm" />
                        <div style="padding:20px;text-align:center;">
                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary" ValidationGroup="EvalValidationGroup" Text="Save" OnClick="lnkSave_Click" />
                        </div>
                    </div>
                </div>
            </div>    
        </div>
    </div>
    </form>
</body>
</html>

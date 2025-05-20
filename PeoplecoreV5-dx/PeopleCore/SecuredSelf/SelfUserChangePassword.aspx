<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfUserChangePassword.aspx.vb" Inherits="SecuredSelf_SelfUserChangePassword" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="page-content-wrap">         
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-body">
                    <br />
                    <fieldset class="form" id="fsd">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Username :</label>
                                <div class="col-md-5">
                                    <asp:TextBox runat="server" ID="txtUserCode" CssClass="form-control" />
                                </div>
                             </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Enter old password :</label>
                                <div class="col-md-5">
                                    <asp:TextBox runat="server" ID="txtOldPassword" CssClass="form-control required" TextMode="Password" />
                                </div>
                             </div>                        
                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Enter new password :</label>
                                <div class="col-md-5">
                                    <asp:TextBox runat="server" ID="txtNewPassword" CssClass="form-control required" TextMode="Password" />
                                </div>
                             </div>                        
                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Re enter new password :</label>
                                <div class="col-md-5">
                                    <asp:TextBox runat="server" ID="txtNewPassword2" CssClass="form-control required" TextMode="Password" />
                                </div>
                             </div>
                             <div class="form-group">
                                <label class="col-md-4 control-label">&nbsp;</label>
                                <div class="col-md-5">
                                    <asp:Button runat="server"  ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                                </div>
                             </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>                        
    </div>
</asp:Content>


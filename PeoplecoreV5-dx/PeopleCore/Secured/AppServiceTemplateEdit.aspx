<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="AppServiceTemplateEdit.aspx.vb" Inherits="Secured_AppServiceTemplateEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <asp:Panel runat="server" ID="Panel1">
        <fieldset class="form" id="fsd">
             <div  class="form-horizontal">
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Transaction No. :</label>
                    <div class="col-md-9">
                        <asp:Textbox ID="txtCode"  runat="server" CssClass="form-control" ReadOnly="true" />
                        <asp:HiddenField runat="server" ID="hifApplicantCoreTemplateNo" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Category :</label>
                    <div class="col-md-9">
                        <asp:DropDownList ID="cboApplicantCoreCateNo" DataMember="EApplicantCoreCate" runat="server" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Template Message :</label>
                    <div class="col-md-9">
                         <dx:ASPxHtmlEditor ID="txtMessages" runat="server" Width="100%" Height="600px"/>
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-6">
                        <asp:Button runat="server" ID="lnkSave" CssClass="btn btn-default submit fsd" OnClick="lnkSave_Click" Text="Save"></asp:Button>
                        <asp:Button runat="server" ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" UseSubmitBehavior="false"></asp:Button>
                    </div>
                </div> 


             </div>
        </fieldset>
    </asp:Panel>
</asp:Content>
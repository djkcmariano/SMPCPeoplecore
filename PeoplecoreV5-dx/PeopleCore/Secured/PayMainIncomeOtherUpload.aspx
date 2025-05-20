<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayMainIncomeOtherUpload.aspx.vb" Inherits="Secured_PayMainIncomeOtherUpload" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">

    <br />
    <div  class="form-horizontal">
         <div class="form-group">
                <label class="col-md-2 control-label">Payroll No. :</label>
                <div class="col-md-6">
                <asp:Textbox ID="txtPayCode" ReadOnly="true" runat="server" CssClass="form-control"
                ></asp:Textbox>
                </div>
        </div>
   
        <div class="form-group">
                <label class="col-md-2 control-label">Payroll group :</label>
                <div class="col-md-6">
                <asp:Dropdownlist ID="cboPayClassNo" DataMember="EPayClass" runat="server" CssClass="form-control" 
                ></asp:Dropdownlist>
            </div>
        </div>
   
        <div class="form-group">
                <label class="col-md-2 control-label">Select filename :</label>
                <div class="col-md-6">
                <asp:FileUpload ID="txtFile" runat="server" Width="350" CssClass="form-control" />
            </div>
        </div>
   
        <div class="form-group">
                <label class="col-md-2 control-label has-required">Income type :</label>
                <div class="col-md-6">
                <asp:Dropdownlist ID="cboPayIncomeTypeNo"  runat="server" CssClass="required form-control"
                ></asp:Dropdownlist>
            </div>
        </div>
   
        <div class="form-group">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-6">

                <asp:Button runat="server"  ID="lnkSubmit" CssClass="form-control-search" OnClick= "btnUpload_Click" Text="Upload"></asp:Button>

                <asp:Button runat="server"  ID="lnkCancel" CssClass="form-control-search" CausesValidation="false" Text="<< Back/Cancel" UseSubmitBehavior="false"></asp:Button>
      
            </div>
        </div>
    </div>
</asp:content>
 
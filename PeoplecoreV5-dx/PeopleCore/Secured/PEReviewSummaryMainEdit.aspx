<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PEReviewSummaryMainEdit.aspx.vb" Inherits="Secured_PEReviewSummaryMainEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>                       
            <asp:Label runat="server" ID="lbl" />            
        </Header>
        <Content>
        <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div  class="form-horizontal">
                        <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label">Transaction No :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtPEReviewSummaryMainNo" CssClass="form-control" runat="server" ></asp:Textbox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Summary No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                     </div>
                </div>
            
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Applicable Year :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtApplicableyear" runat="server" CssClass="form-control required" ></asp:Textbox>
                    </div>
                </div>

                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space">Performance Period Type :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPEPeriodNo" runat="server" DataMember="EPEPeriod" CssClass="form-control"></asp:Dropdownlist>
                    </div>
                </div>
            
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Description :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control required" TextMode="MultiLine" ></asp:Textbox>
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Performance Norms :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPENormsNo" runat="server" DataMember="EPENorms" CssClass="form-control required"></asp:Dropdownlist>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="txtIsManual" onclick="disableenable(this);"  Text="&nbsp; Manual uploading/encoding of rating." />
                    </div>
                </div>

                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-required" runat="server" id="lblpereviewno">PE Review No. :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPEReviewMainNo" runat="server" DataMember="EPEReviewMainL" CssClass="form-control"></asp:Dropdownlist>
                    </div>
                </div>




                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />                            
                        </div>
                    </div>

                    <br /><br />                     
                </div>                                                
            </fieldset>
        </asp:Panel>
        </Content>
    </uc:Tab>    
</asp:Content>


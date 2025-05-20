<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="frmContractTempEdit.aspx.vb" Inherits="Secured_SecCMSTemplateEdit" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:Panel runat="server" ID="pnlPopupMain">  
                <fieldset class="form" id="fsMain">

                   <div  class="form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-2 control-label has-space">Reference No :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtContractTempNo" CssClass="form-control" runat="server" ></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label has-space">Reference No. :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label has-required">Code :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtContractTempCode" runat="server" CssClass="form-control required" ></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label has-required">Description :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtContractTempDesc" runat="server" CssClass="form-control required" ></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label has-required">Report Type :</label>
                            <div class="col-md-5">
                                <asp:Dropdownlist ID="cboReportTypeNo" runat="server"  CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboReportTypeNo_SelectedIndexChanged"  ></asp:Dropdownlist>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label has-required">Content :</label>
                            <div class="col-md-8">
                                <dx:ASPxHtmlEditor ID="txtContractTempCont" runat="server" Width="100%" Height="800px" >
                                </dx:ASPxHtmlEditor>
                            </div>
                            <div class="col-md-2">
                                <dx:ASPxListBox runat="server" ID="lstLegend" Width="100%" Height="800px" OnSelectedIndexChanged="lstLegend_ValueChanged" AutoPostBack="true"  Font-Size="Small" /> 
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label has-space">&nbsp;</label>
                            <div class="col-md-5">
                                <asp:CheckBox runat="server" ID="txtIsArchived" Text="&nbsp; Archived" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label has-space">&nbsp;</label>
                            <div class="col-md-5">
                                <asp:CheckBox runat="server" ID="chkIsIncludeCompanyLogo" Text="&nbsp; Use Company Logo for Header" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none">
                            <label class="col-md-2 control-label has-space">Company Name :</label>
                            <div class="col-md-5">
                                <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass="number form-control" >
                                </asp:Dropdownlist>
                            </div>
                        </div>
                       <div class="form-group">
                       </div>
                        <div class="form-group">
                            <div class="col-md-8 col-md-offset-2">
                                <div class="pull-left">
                                    <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                    <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />   
                                    <asp:Button runat="server"  ID="lnkPreview" CssClass="btn btn-default" CausesValidation="false" Text="Preview" OnClick="lnkPreview_Click" OnPreRender="lnkPrint_PreRender" />                  
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                       </div>
                    </div>

                </fieldset>
                </asp:Panel>
            </div>
        </div>
     </div>
 </div>
                


</asp:content>

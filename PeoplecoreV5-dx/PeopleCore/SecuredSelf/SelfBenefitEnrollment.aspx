<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfBenefitEnrollment.aspx.vb" Inherits="SecuredSelf_SelfBenefitEnrollment" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                    
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                    </asp:UpdatePanel> 
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitEnrollmentNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("BenefitEnrollmentNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataDateColumn FieldName="DateFiled" Caption="Date Filed" />
                                <dx:GridViewDataComboBoxColumn FieldName="BenefitTypeDesc" Caption="Benefit Type" />
                                <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" /> 
                                <dx:GridViewDataColumn Caption="Attachment" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkAttachment" OnClick="lnkAttachment_Click" CssClass="fa fa-paperclip " CommandArgument='<%# Eval("BenefitEnrollmentNo") & "|" & Eval("Fullname")  %>' Enabled='<%# Bind("IsEnabled") %>' Font-Size="Medium" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                                 
                                <dx:GridViewDataCheckColumn FieldName="IsReady" Caption="Ready for Posting" Width="2%" Visible="false" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>


            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date Filed :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtDateFiled" runat="server" CssClass="form-control required" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtDateFiled" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDateFiled" Mask="99/99/9999" MaskType="Date" />
                </div>
            </div>
                        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Benefit Type :</label>
                <div class="col-md-6">
                    <asp:HiddenField runat="server" ID="hifBenefitCateNo" />
                    <asp:DropDownList runat="server" ID="cboBenefitTypeNo" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">HMO Plan Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboBenefitHMOTypeNo" DataMember="EBenefitHMOType" CssClass="form-control required" AutoPostBack="true" />
                </div>
            </div>


            <div class="form-group">
                <h5><label class="col-md-4 control-label has-space">FOR DEPENDENT(S)</label></h5>
                <div class="col-md-6">
                        
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">HMO Plan Bracket :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboBenefitHMOPlanTypeNo2" DataMember="EBenefitHMOPlanType" CssClass="form-control" AutoPostBack="true" />
                </div>
            </div>

            <div class="form-group" style="visibility:hidden; position:absolute;">
                <label class="col-md-4 control-label has-required">HMO Plan :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboBenefitHMOPlanNo2" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Dependent 1 :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboEmployeeDepeNo1" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Plan Amount :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtAmount2" runat="server" CssClass="form-control number" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Excess :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtExcess2" runat="server" CssClass="form-control number" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsPHRider1" CausesValidation="false" OnCheckedChanged="TotalCostUpdate" AutoPostBack="true" Text="&nbsp;Tick here if dependent is a PhilHealth rider" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">PhilHealth Fee :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtPHFee1" runat="server" CssClass="form-control number" ReadOnly="true" />
                </div>
            </div>

            <div class="form-group">
                <h5><label class="col-md-4 control-label has-space"></label></h5>
                <div class="col-md-6">
                        
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">HMO Plan Bracket :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboBenefitHMOPlanTypeNo3" DataMember="EBenefitHMOPlanType" CssClass="form-control" AutoPostBack="true" />
                </div>
            </div>

            <div class="form-group" style="visibility:hidden; position:absolute;">
                <label class="col-md-4 control-label has-space">HMO Plan :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboBenefitHMOPlanNo3" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Dependent 2 :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboEmployeeDepeNo2" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Plan Amount :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtAmount3" runat="server" CssClass="form-control number" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Excess :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtExcess3" runat="server" CssClass="form-control number" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsPHRider2" CausesValidation="false" OnCheckedChanged="TotalCostUpdate" AutoPostBack="true" Text="&nbsp;Tick here if dependent is a PhilHealth rider" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">PhilHealth Fee :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtPHFee2" runat="server" CssClass="form-control number" ReadOnly="true" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Total amount to be deducted :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtTotalCost" runat="server" CssClass="form-control number" />
                </div>
            </div>

            
            <br />                                    
        </div>                    
    </fieldset>
</asp:Panel>

</asp:Content>


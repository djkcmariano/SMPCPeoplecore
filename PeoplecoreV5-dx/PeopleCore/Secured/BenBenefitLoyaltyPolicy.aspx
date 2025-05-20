<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BenBenefitLoyaltyPolicy.aspx.vb" Inherits="Secured_BenBenefitLoyaltyPolicy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<br />
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
                            <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                    
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Are you sure you want to archive selected item(s)?"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitLoyaltyPolicyNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Referrence No." />
                                <dx:GridViewDataTextColumn FieldName="BenefitLoyaltyPolicyCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="BenefitLoyaltyPolicyDesc" Caption="Description" /> 
                                <dx:GridViewDataTextColumn FieldName="FromYears" Caption="From Year" />
                                <dx:GridViewDataTextColumn FieldName="ToYears" Caption="To Year" />
                                <dx:GridViewDataTextColumn FieldName="YearsGiven" Caption="Year(s) Given" />
                                <dx:GridViewDataTextColumn FieldName="AmountProrate" Caption="Proportionate Amount" Width="10%" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Width="10%" Visible="false" />
                                <dx:GridViewDataCheckColumn FieldName="IsWithGoldRing" Caption="With Gold Ring" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder" Visible="false" />
                                <dx:GridViewDataCheckColumn FieldName="IsSuspended" Caption="Suspended" Visible="false" />                                                          
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
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
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none" >
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        `
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Code :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtBenefitLoyaltyPolicyCode" runat="server" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtBenefitLoyaltyPolicyDesc" runat="server" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">From Year(s) :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtFromYears" runat="server" CssClass="form-control required number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">To Year(s) :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtToYears" runat="server" CssClass="form-control required number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Year(s) Given :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtYearsGiven" runat="server" CssClass="form-control required number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Amount :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtAmount" runat="server" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Proportionate Amount :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtAmountProrate" runat="server" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsWithGoldRing" Text="&nbsp;Tick if Loyalty Policy is with gold ring" />
                    </div>
                </div>  

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsSuspended" Text="&nbsp;Tick if Loyalty Policy is suspended" />
                    </div>
                </div>  
                
                <div class="form-group">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
                </div>  
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>                       
                               
            </div>                    
        </fieldset>
    </asp:Panel>   
</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="BenBenefitType.aspx.vb" Inherits="Secured_BenBenefitType" Theme="PCoreStyle" %>

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
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitTypeNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                <dx:GridViewDataTextColumn FieldName="BenefitTypeCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="BenefitTypeDesc" Caption="Description" />
                                <dx:GridViewDataColumn FieldName="BenefitCateDesc" Caption="Category" />
                                <%--<dx:GridViewDataCheckColumn FieldName="IsWithDepe" Caption="With Dependent" />--%>
                                <dx:GridViewDataCheckColumn FieldName="IsEnrollOnline" Caption="Enroll Online" />
                                <dx:GridViewDataCheckColumn FieldName="IsViewOnline" Caption="View Online" />
                                <dx:GridViewDataCheckColumn FieldName="IsMaintainBalance" Caption="With Maintaining Balance" />   
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                <dx:GridViewDataComboBoxColumn FieldName="PaylocDesc" Caption="Company" />                                                        
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>
    <asp:Button ID="Button1" runat="server" style="display:none;" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Reference No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Code :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtBenefitTypeCode" runat="server" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtBenefitTypeDesc" runat="server" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Category :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboBenefitCateNo" DataMember="EBenefitCateL" CssClass="form-control required" />
                    </div>
                </div>

                <div class="form-group" style="visibility:hidden; position:absolute;">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsWithDepe" Text="&nbsp;With dependent(s)" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsEnrollOnline" Text="&nbsp;Tick to enable online enrollment" AutoPostBack="true" OnCheckedChanged="chkIsEnrollOnline_CheckedChanged" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsViewOnline" Text="&nbsp;Tick to enable online viewing" />
                    </div>
                </div>                        
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsUploadDoc" Text="&nbsp;Tick to enable document uploading" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="txtIsMaintainBalance" Text="&nbsp;Tick if with maintaining balance" AutoPostBack="true" OnCheckedChanged="txtIsMaintainBalance_OnCheckedChanged" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Balance Policy :</label>
                    <div class="col-md-6">
                        <asp:RadioButton ID="txtIsRefresh"  GroupName="MaintainingBalance" Text="&nbsp; Balance refresh every year" runat="server" AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" /><br />
                        <asp:RadioButton ID="txtIsAccumulated" GroupName="MaintainingBalance" Text="&nbsp; Accumulated balance" runat="server" AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" />
                        <%--<asp:RadioButton ID="txtIsForefeited" GroupName="MaintainingBalance" Text="&nbsp; With forfeiture" runat="server" AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" style="visibility:hidden; position:absolute;" />--%>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Type :</label>
                    <div class="col-md-6">
                        <asp:RadioButton ID="chkIsForDeduction"  GroupName="Type" Text="&nbsp; For Deduction" runat="server" AutoPostBack="true" OnCheckedChanged="txtIsDeductIncome_OnCheckedChanged"  /><br />
                        <asp:RadioButton ID="chkIsForIncome" GroupName="Type" Text="&nbsp; For Income" runat="server" AutoPostBack="true" OnCheckedChanged="txtIsDeductIncome_OnCheckedChanged" />
                    </div>
                </div>

                <asp:PlaceHolder runat="server" ID="phDeduct" Visible="false">
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Deduction Type :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboPayDeductTypeNo" DataMember="EPayDeductType" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Deduction forward to :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboDeduct" CssClass="form-control">
                                <asp:ListItem Text="-- Select --" Value="" Selected="True" />
                                <asp:ListItem Text="Template" Value="1" />
                                <asp:ListItem Text="Forwarded" Value="2" />
                                <asp:ListItem Text="Loan" Value="3" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder runat="server" ID="phIncome" Visible="false">
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Income Type :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboPayIncomeTypeNo" DataMember="EPayIncomeType" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Maximum Amount :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtAmount" runat="server" CssClass="form-control number" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Income forward to :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboIncome" CssClass="form-control">
                                <asp:ListItem Text="-- Select --" Value="" Selected="True" />
                                <asp:ListItem Text="Template" Value="1" />
                                <asp:ListItem Text="Forwarded" Value="2" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </asp:PlaceHolder> 

                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label  has-space">Cut-off (Refresh Balance) :</label>
                    <div class="col-md-3">
                        <asp:DropdownList ID="cboRefreshCutOffNo" DataMember="EMonth" runat="server" CssClass="form-control">
                        </asp:DropdownList>
                     </div>
                     <label class="col-md-1 control-label  has-space">Day</label>
                     <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="cboRefreshCutOffDays" CssClass="form-control">
                        </asp:DropDownList>
                     </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Note :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtNote" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group" style="visibility:hidden; position:absolute;">
                    <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass="number form-control" >
                        </asp:Dropdownlist>
                    </div>
                </div>   
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                    </div>
                </div>
            </div>
                  
        </fieldset>
    </asp:Panel>   
</asp:Content>


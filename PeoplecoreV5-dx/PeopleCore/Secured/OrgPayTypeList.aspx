<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="OrgPayTypeList.aspx.vb" Inherits="Secured_OrgPayTypeList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<br />
<div class="page-content-wrap">            
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    &nbsp;
                </div>
                <div>                                  
                    <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                            
                    </ul>
                    <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="lnkDelete" />
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayTypeNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                <dx:GridViewDataTextColumn FieldName="PayTypeCode" Caption="Code" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="PayTypeDesc" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="PayPeriods" Caption="Payroll Period" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />                            
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

<div class="page-content-wrap">            
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    Reference No. :&nbsp;<asp:Label ID="lblDetl" runat="server"></asp:Label>
                </div>
                <div>
                    &nbsp;                   
                </div>                                                                                                   
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <asp:HiddenField runat="server" ID="hifPayTypeNo" />
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdMain" runat="server" Width="100%" KeyFieldName="PayTypeSchedNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="MonthDesc" Caption="Applicable Month" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="PayScheduleDesc" Caption="Payroll Schedule" />
                                <dx:GridViewDataTextColumn FieldName="PayPeriod" Caption="Payroll Period" />                                                                
                            </Columns> 
                            <SettingsContextMenu Enabled="true">                                
                                <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                            </SettingsContextMenu>                                                                                            
                            <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                            <SettingsSearchPanel Visible="false" />  
                            <Settings ShowFooter="false" />   
                            <SettingsPager EllipsisMode="OutsideNumeric" NumericButtonCount="7">
                                <PageSizeItemSettings Visible="true" Items="10, 20, 50, 100" />        
                            </SettingsPager>                                                  
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>
<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">                    
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayTypeCode" runat="server" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayTypeDesc" runat="server" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Total Pay Periods :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayPeriods" runat="server" CssClass="form-control required number" />
                </div>
            </div>      
            <div class="form-group">
                <label class="col-md-4 control-label has-space">
                Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div>                                    
            <br />
        </div>                    
    </fieldset>
</asp:Panel>

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsDetail">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetail" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>                                             
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Applicable Month :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Payroll Schedule :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboPaySchedNo" DataMember="EPaySchedule" runat="server" CssClass="form-control" />
                </div>
            </div>            
            <br />
        </div>                    
    </fieldset>
</asp:Panel>    
</asp:Content>
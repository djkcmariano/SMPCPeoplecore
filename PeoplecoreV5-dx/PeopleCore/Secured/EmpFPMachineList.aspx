<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpFPMachineList.aspx.vb" Inherits="Secured_EmpFPMachineList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">   
<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    &nbsp;
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" CausesValidation="false" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" CausesValidation="false" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" CausesValidation="false" /></li>                    
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="FPMachineNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("FPMachineNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="FPMachineCode" Caption="Machine No." />                                                                           
                                <dx:GridViewDataTextColumn FieldName="FPMachineDescription" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="RegCode" Caption="Serial No." />
                                <dx:GridViewDataTextColumn FieldName="IpAddress" Caption="IP Address" />
                                <dx:GridViewDataTextColumn FieldName="PortNo" Caption="Port No." />
                                <dx:GridViewDataComboBoxColumn FieldName="KeyCode" Caption="Key Code" />
                                <dx:GridViewDataTextColumn FieldName="Status" Caption="Status" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="IsActive" Caption="Active" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn> 
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
<br /><br />
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">
                    <div class="panel-title">
                        <asp:Label runat="server" ID="lbl" />
                    </div>
                </div>
                <div>                    
                    <ul class="panel-controls">                            
                        <li><asp:LinkButton runat="server" ID="lnkAddDeti" OnClick="lnkAddDeti_Click" Text="Add" CssClass="control-primary" CausesValidation="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDeleteDeti" OnClick="lnkDeleteDeti_Click" Text="Delete" CssClass="control-primary" CausesValidation="false" /></li>
                    </ul>                       
                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDeleteDeti" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                 
                </div> 
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="FPMachineDetiNo" Width="100%">
                        <Columns>                            
                            <dx:GridViewDataTextColumn FieldName="CodeDeti" Caption="Detail No." />                                                        
                            <dx:GridViewDataTextColumn FieldName="FPActionDesc" Caption="Action" />
                            <dx:GridViewDataTextColumn FieldName="StartDate" Caption="Start Date" />
                            <dx:GridViewDataTextColumn FieldName="EndDate" Caption="End Date" />                            
                            <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Posted Date" />
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                        </Columns>                            
                    </dx:ASPxGridView>                    
                </div>                            
            </div>
        </div>
    </div>
</div>
<asp:Button ID="btnShowRate" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlRate" runat="server" TargetControlID="btnShowRate" PopupControlID="pnlPopupRate" CancelControlID="imgClose" BackgroundCssClass="modalBackground" />        
<asp:Panel id="pnlPopupRate" runat="server" CssClass="entryPopup" style="display:none">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">                
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFPMachineNo" runat="server" ReadOnly="true" CssClass="form-control" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" ReadOnly="True" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Machine number :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFPMachineCode" runat="server" CssClass="required form-control" />                       
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description of Machine :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFPMachineDescription" Textmode="MultiLine" Rows="4" runat="server" CssClass="required form-control" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Machine serial no. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRegCode" runat="server" CssClass="required form-control" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">IP Address :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtIPAddress" runat="server"  CssClass="required form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Port no. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtPortNo" runat="server" CssClass="required form-control" />
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Branch :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBranchId" DataMember="EBranch" runat="server" CssClass="form-control" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Key Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtKeyCode" runat="server" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                    <asp:Checkbox ID="chkIsFPTemplateSource" runat="server" Text="&nbsp;Accept fingerprint template from this device." />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                    <asp:Checkbox ID="chkIsActive" runat="server" Text="&nbsp;Active" />
                </div>
            </div>
        </div>
        <br />
         </fieldset>
    </asp:Panel>


<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkCloseDeti" BackgroundCssClass="modalBackground" />        
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
       <fieldset class="form" id="fsDeti">
        <!-- Header here -->
         <div class="cf popupheader">                
                <asp:Linkbutton runat="server" ID="lnkCloseDeti" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveDeti" CssClass="fa fa-floppy-o submit fsDeti lnkSaveDeti" OnClick="lnkSaveDeti_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDeti" runat="server" ReadOnly="True" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Action :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cbofpactionno" runat="server" CssClass="form-control required" AutoPostBack="true" OnTextChanged="cbofpactionno_TextChanged">                        
                    </asp:DropDownList>                    
                </div>
            </div>           
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Start Date :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Enabled="false" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="Please enter valid entry" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">End Date :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Enabled="false" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator2" ErrorMessage="Please enter valid entry" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate" />
                </div>
            </div>
            
        </div>
        <br />
         </fieldset>
</asp:Panel>

</asp:Content>
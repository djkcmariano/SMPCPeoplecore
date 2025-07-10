<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="HousingSubsidyList.aspx.vb" Inherits="Secured_HousingSubsidyList"%>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">      
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                    </div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HousingSubsidyNo"
                                OnFillContextMenuItems="MyGridView_FillContextMenuItems">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="HousingClassDesc" Caption="Housing Classification" />
                                    <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Employee Classification" />
                                    <dx:GridViewDataTextColumn FieldName="Effectivity" Caption="Effective Date" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Subsidy" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encode By" />
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encode Date" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>                           
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtHousingSubsidyNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Housing Classification :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboHousingClassNo" runat="server" DataMember="EHousingClass" CssClass="required form-control" ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Employee Classification :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboEmployeeClassNo" runat="server" DataMember="EEmployeeClass" CssClass="required form-control" ></asp:Dropdownlist>
                </div>
            </div>
                        <div class="form-group">
                <label class="col-md-4 control-label has-space">Effective Date :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtEffectivity" runat="server" SkinID="txtdate" CssClass="form-control"></asp:TextBox> 
                                                                    
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                        TargetControlID="txtEffectivity"
                        Format="MM/dd/yyyy" />  
                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtEffectivity"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                                    
                        <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtEffectivity"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                    
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender1"
                        TargetControlID="RangeValidator4" />                                                                           
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Amount :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtAmount" runat="server" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
             </div>
            <br />
        </div>
        
         </fieldset>
</asp:Panel>
</asp:content>

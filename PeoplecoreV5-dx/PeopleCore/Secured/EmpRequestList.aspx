<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpRequestList.aspx.vb" Inherits="Secured_EmpRequestList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap" >         
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
                            <li><asp:LinkButton runat="server" ID="lnkServe" OnClick="lnkServe_Click" Text="Serve" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                                                    
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                            
                            <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkServe" ConfirmMessage="Are you sure you want to tag as serve?"  /> 
                        </ul>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeRequestNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click"/>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                                                                                                                                   
                                <dx:GridViewDataComboBoxColumn FieldName="RequestTypeDesc" Caption="Request Type" />
                                <dx:GridViewDataTextColumn FieldName="Message" Caption="Reason" />
                                <dx:GridViewDataTextColumn FieldName="DateRequested" Caption="Date Requested" /> 
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="DateServed" Caption="Date Served" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="RequestStatDesc" Caption="Status" Visible="false" />
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

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow" PopupControlID="Panel1" CancelControlID="imgClose" BackgroundCssClass="modalBackground" />

<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label  has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtEmployeeRequestNo" runat="server" ReadOnly="True" CssClass="form-control"
                         ></asp:Textbox>
                    </div>
                </div>

                 <div class="form-group">
                    <label class="col-md-4 control-label  has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" ></asp:Textbox>
                    </div>
                </div>
                 <div class="form-group">
                    <label class="col-md-4 control-label has-required">Employee Name :</label>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="0" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                            <script type="text/javascript">
                                function getRecord(source, eventArgs) {
                                    document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                }
                                </script>                                               
                    </div>
                </div>

                 <div class="form-group">
                    <label class="col-md-4 control-label has-required">Request Type :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboRequestTypeNo" DataMember="ERequestType" runat="server" CssClass="form-control required" ></asp:Dropdownlist>
                    </div>
                </div>

                 <div class="form-group">
                    <label class="col-md-4 control-label has-required">Date Requested :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtDateRequested" runat="server" CssClass="form-control required" ></asp:Textbox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDateRequested" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtDateRequested" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                        <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtDateRequested" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator4" />  
                    </div>
                </div>

                 <div class="form-group">
                    <label class="col-md-4 control-label has-required">Reason :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtMessage" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control required" ></asp:Textbox>
                    </div>
                </div>

                 <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-7">
                        <asp:Checkbox ID="chkIsServed" runat="server" Text="&nbsp;Served" Enabled="false" />
                   </div>
                </div>

                 <div class="form-group">
                    <label class="col-md-4 control-label has-space">Date served :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtDateServed" runat="server" CssClass="form-control"  Enabled="false" 
                            ></asp:Textbox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateServed" Format="MM/dd/yyyy"  />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtDateServed" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtDateServed" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="RangeValidator3" />                        
                    </div>
                </div>

                 <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtRemarks" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:Textbox>
                    </div>
            </div>
        </div>
        <br />
         </fieldset>
    </asp:Panel>


</asp:Content> 
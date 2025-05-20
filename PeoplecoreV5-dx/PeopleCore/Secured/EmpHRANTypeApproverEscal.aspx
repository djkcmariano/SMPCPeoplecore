<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="EmpHRANTypeApproverEscal.aspx.vb" Inherits="Secured_EmpHRANTypeApproverEscal" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>         
                                    <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false"/></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="cfbArchive" TargetControlID="lnkArchive" ConfirmMessage="Selected items will be archived. Proceed?"  />
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HRANTypeApproverEscalNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="HRANTypeDesc" Caption="HRAN Type" />
                                    <dx:GridViewDataTextColumn FieldName="HRANTypeApproverEscalDesc" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="NoofScal" Caption="No. of Escalation" />
                                    <dx:GridViewDataTextColumn FieldName="OrganizationDesc" Caption="Org. Limit" />
                                    <dx:GridViewDataTextColumn FieldName="SectionPositionGrpDesc" Caption="Escalation Group"/>
                                    <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="JobGradeGroupDesc" Caption="Job Grade Group" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="JobGradeDesc" Caption="Job Grade" Visible="false"/>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                                    <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                                    <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                                    <dx:GridViewDataComboBoxColumn FieldName="PayLocDesc" Caption="Company" />
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
 
 
 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <div class="col-md-6 panel-title">
                            Reference No.: &nbsp;<asp:Label ID="lblDetl" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  /> 
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExportDetl" />
                            </Triggers>
                        </asp:UpdatePanel>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="HRANTypeApproverEscalDetiNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="OrganizationDesc" Caption="Organization Level" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name of Approver" />
                                    <dx:GridViewDataTextColumn FieldName="OrderLevel" Caption="Order Level" />
                                    <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>               


<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtHRANTypeApproverEscalNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            
            

            <div class="form-group">
                <label class="col-md-4 control-label has-required">HRAN Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboHRANTypeNo" DataMember="EHRANType" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtHRANTypeApproverEscalDesc" runat="server" CssClass="form-control required" TextMode="MultiLine"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">No. of Escalation :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtNOOfScal" runat="server" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtNOOfScal" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Organization Limit :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboOrganizationLimitNo" DataMember="EOrganization" runat="server" CssClass="form-control required"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Escalation Group :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboSectionPositionGrpNo" DataMember="ESectionPositionGrp" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>
            
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Employee Class :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Job Grade Group :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboJobGradeGroupNo" DataMember="EJobGradeGroup" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Job Grade :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboJobGradeNo" DataMember="EJobGrade" runat="server" CssClass="form-control"></asp:DropdownList>
                </div>
            </div>                  
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                </div>
            </div> 
                                                        
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>




<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsDetl">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveDetl" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtHRANTypeApproverEscalDetiNo" CssClass="form-control" runat="server"></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDeti" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Organization Level :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboOrganizationNo" DataMember="EOrganization" runat="server" CssClass="form-control required" AutoPostBack="true"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Name of Approver :</label>
                    <div class="col-md-7">
                       <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" style="display:inline-block;" onblur="ResetEmployeeNo()" Placeholder="Type here..." /> 
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getMain" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    </div>

                    <script type="text/javascript">

                        function getMain(source, eventArgs) {
                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                        }

                        function Split(obj, index) {
                            var items = obj.split("|");
                            for (i = 0; i < items.length; i++) {
                                if (i == index) {
                                    return items[i];
                                }
                            }
                        }

                        function ResetEmployeeNo() {
                            if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                            }
                        } 
                    </script>

                </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Order Level :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtOrderLevel" runat="server" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtOrderLevel" />
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control required" TextMode="MultiLine"></asp:Textbox>
                </div>
            </div>

            <div class="form-group" style="visibility:hidden;position:absolute;">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:Checkbox ID="txtIsAlternate" runat="server" Text="&nbsp;Please check here if alternative approver."></asp:Checkbox>
                </div>
            </div>  
                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>

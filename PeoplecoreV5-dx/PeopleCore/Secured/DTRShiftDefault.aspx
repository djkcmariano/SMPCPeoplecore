<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRShiftDefault.aspx.vb" Inherits="Secured_DTRShiftDefaultList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">   
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRShiftDefaultNo" OnCustomButtonCallback="lnkEdit_Click">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee Code" />                                
                                <dx:GridViewDataTextColumn FieldName="Fullname" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="EffectiveDate" Caption="Effective Date" />
                                <dx:GridViewDataTextColumn FieldName="DefaultShift" Caption="Shift" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="DefaultDayOff" Caption="Day Off" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="AlteShift" Caption="Alternate Shift" PropertiesTextEdit-EncodeHtml="false" Visible="true"  />
                                <dx:GridViewDataTextColumn FieldName="AlteDayOff" Caption="Alternate Day Off" PropertiesTextEdit-EncodeHtml="false" Visible="true"  />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Facility" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" Visible="false" />                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GenderDesc" Caption="Gender" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CivilStatDesc" Caption="Civil Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="&nbsp;" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="imgClose" BackgroundCssClass="modalBackground" />

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">        
         <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
         </div>         
         <div  class="entryPopupDetl form-horizontal">                  
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                <div class="col-md-8">                     
                    <asp:TextBox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-required">Name of Employee :</label>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" CompletionSetCount="1" 
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
                <label class="col-md-3 control-label has-required">Effectivity Date :</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txtEffectivity" runat="server" SkinID="txtdate" CssClass="required form-control" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEffectivity" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEffectivity" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEffectivity" />
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-3 control-label">&nbsp</label>
                <div class="col-md-4">
                    <h5><b>Shift Default</b></h5>
                </div>
                <div class="col-md-4" style=" visibility:hidden">
                    <h5><b>Alternate</b></h5>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label">Monday :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboShiftNoMon" DataMember="EShiftL" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteShiftNoMon" DataMember="EShiftL" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label">Tuesday :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboShiftNoTue" DataMember="EShiftL" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteShiftNoTue" DataMember="EShiftL" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label ">Wednesday :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboShiftNoWed" DataMember="EShiftL" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteShiftNoWed" DataMember="EShiftL" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label ">Thursday :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboShiftNoThu" DataMember="EShiftL" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteShiftNoThu" DataMember="EShiftL" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label ">Friday :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboShiftNoFri" DataMember="EShiftL" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteShiftNoFri" DataMember="EShiftL" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label ">Saturday :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboShiftNoSat" DataMember="EShiftL" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteShiftNoSat" DataMember="EShiftL" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label ">Sunday :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboShiftNoSun" DataMember="EShiftL" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteShiftNoSun" DataMember="EShiftL" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-3 control-label"><b>Day Off</b></label>
                <div class="col-md-8">
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label ">1 :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboDayOffNo" DataMember="EDayOff" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteDayOffNo" DataMember="EDayOff" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label ">2 :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboDayOffNo2" DataMember="EDayOff" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteDayOffNo2" DataMember="EDayOff" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label ">3 :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboDayOffNo3" DataMember="EDayOff" CssClass="form-control"></asp:DropDownList>
                </div>                
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboAlteDayOffNo3" DataMember="EDayOff" CssClass="form-control" Visible="true" ></asp:DropDownList>
                </div>
            </div>
            <br /><br />
           </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>

</asp:Content>


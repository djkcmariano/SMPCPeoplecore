<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="BenLoyaltyList.aspx.vb" Inherits="Secured_BenLoyaltyList" %>

<%@ Register Src="~/Include/wucFilterGeneric.ascx" TagName="Filter" TagPrefix="wuc" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<br />
<script type="text/javascript">
    function ProcessMemo(key, txt) {
        var currentKey = "key" + key.toString();
        if (!chfMemo.Contains(currentKey)) {
            chfMemo.Add(currentKey, txt);
        }
        else {
            chfMemo.Set(currentKey, txt);
        }
    }

    function ProcessRingDate(key, txt) {
        var currentKey = "key" + key.toString();
        if (!chfRingDate.Contains(currentKey)) {
            chfRingDate.Add(currentKey, txt);
        }
        else {
            chfRingDate.Set(currentKey, txt);
        }
    }

</script>

<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">  
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div class="col-md-2" style=" text-align:right;">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control" CausesValidation="false" Placeholder="Effective Date"
                        ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                        TargetControlID="txtEffectiveDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtEffectiveDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                        <asp:RangeValidator
                        ID="RangeValidator1"
                        runat="server"
                        ControlToValidate="txtEffectiveDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender1"
                        TargetControlID="RangeValidator1"
                        />  
                </div>
                <div class="col-md-2" style=" padding-left:0px;">
                    <ul class="panel-controls" style=" text-align:left; float:left;">
                        <li><asp:LinkButton runat="server" ID="lnkGenerateLoyalty" OnClick="lnkGenerateLoyalty_Click" Text="Generate Loyalty" ToolTip="Generate Employee(s) with Due for Loyalty" CssClass="control-primary" /></li>
                        <uc:ConfirmBox runat="server" ID="cfbGenerateLoyalty" TargetControlID="lnkGenerateLoyalty" ConfirmMessage="Are you sure you want to generate loyalty award?"  />
                    </ul> 
                </div>
                <div>                
                    
                    <uc:Filter runat="server" ID="Filter1" EnableContent="true" Visible="true">
                    <Content>
<%--                            <div class="form-group">
                            <label class="col-md-4 control-label">Filter By :</label>
                            <div class="col-md-8">
                                <asp:DropDownList runat="server" ID="cbofilterby"  CssClass="form-control" />
                                </div>
                                <ajaxToolkit:CascadingDropDown ID="cdlfilterby" TargetControlID="cbofilterby" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterBy" runat="server" Category="tNo" LoadingText="Loading..." />
                            </div>
		                    <div class="form-group">
                            <label class="col-md-4 control-label">Filter Value :</label> 
                            <div class="col-md-8">
                                <asp:DropDownList runat="server" ID="cbofiltervalue" CssClass="form-control" />
                            </div>
                            <ajaxToolkit:CascadingDropDown ID="cdlfiltervalue" TargetControlID="cbofiltervalue" PromptValue="" ServicePath="~/asmx/WebService.asmx" ServiceMethod="GetFilterValue" runat="server" Category="tNo" ParentControlID="cbofilterby" LoadingText="Loading..." PromptText="-- Select --" />
                            </div>--%>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Filter By :</label>
                            <div class="col-md-8">
                                <asp:Dropdownlist ID="cboFilteredbyNo" AutoPostBack="true"  runat="server" CssClass="form-control" 
                                    OnSelectedIndexChanged="cboFilteredbyNo_SelectedIndexChanged" ></asp:Dropdownlist>
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-4 control-label has-space">Filter Value :</label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                                <asp:HiddenField runat="server" ID="hiffilterbyid"/>
                                <ajaxToolkit:AutoCompleteExtender ID="drpAC" runat="server"  
                                TargetControlID="txtName" MinimumPrefixLength="2" 
                                CompletionInterval="250" ServiceMethod="populateDataDropdown" CompletionSetCount="0"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="getRecordx" FirstRowSelected="true" UseContextKey="true" />
                                 <script type="text/javascript">
                                     function getRecordx(source, eventArgs) {
                                         document.getElementById('<%= hiffilterbyid.ClientID %>').value = eventArgs.get_value();
                                     }
                                 </script>
                    
                            </div>

                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Applicable Month :</label>
                            <div class="col-md-8">
                                <asp:Dropdownlist ID="cboApplicableMonth" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-4 control-label has-space">Applicable Year :</label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtApplicableYear" CssClass="form-control number" style="display:inline-block;" Placeholder="Applicable Year..." /> 
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-4 control-label has-space">Date From :</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" CausesValidation="false" Placeholder="Date From"
                                    ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                    TargetControlID="txtDateFrom"
                                    Format="MM/dd/yyyy" />  
                                                                          
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                    TargetControlID="txtDateFrom"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Date"
                                    DisplayMoney="Left"
                                    AcceptNegative="Left" 
                                    ErrorTooltipEnabled ="true" 
                                    ClearTextOnInvalid="true"  
                                    />
                                                                        
                                    <asp:RangeValidator
                                    ID="RangeValidator4"
                                    runat="server"
                                    ControlToValidate="txtDateFrom"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                                    ID="ValidatorCalloutExtender3"
                                    TargetControlID="RangeValidator4"
                                    /> 
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-4 control-label has-space">Date To :</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" CausesValidation="false" Placeholder="Date To"
                                    ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                    TargetControlID="txtDateTo"
                                    Format="MM/dd/yyyy" />  
                                                                          
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                    TargetControlID="txtDateTo"
                                    Mask="99/99/9999"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Date"
                                    DisplayMoney="Left"
                                    AcceptNegative="Left" 
                                    ErrorTooltipEnabled ="true" 
                                    ClearTextOnInvalid="true"  
                                    />
                                                                        
                                    <asp:RangeValidator
                                    ID="RangeValidator5"
                                    runat="server"
                                    ControlToValidate="txtDateTo"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                                    ID="ValidatorCalloutExtender4"
                                    TargetControlID="RangeValidator5"
                                    /> 
                            </div>
                        </div>

                        <div class="form-group" >
                            <label class="col-md-4 control-label has-space">Loyalty Policy Type :</label>
                            <div class="col-md-8">
                                <asp:Dropdownlist ID="cboBenefitLoyaltyPolicyNo" runat="server" CssClass="form-control" DataMember="EBenefitLoyaltyPolicy" ></asp:Dropdownlist>
                            </div>
                        </div>

                    </Content>
                    </uc:Filter>
                                                                                                                                                                    
                </div>
                </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
            <div class="panel-heading">

                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                                                
                <ul class="panel-controls">                                                        
                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" Visible="false" /></li>

                    <%--<li><asp:LinkButton runat="server" ID="lnkGenerateLoyalty" OnClick="lnkGenerateLoyalty_Click" Text="Generate Loyalty" CssClass="control-primary" /></li>--%>
                    <li><asp:LinkButton runat="server" ID="lnkForward" OnClick="lnkForward_Click" Text="Forward to Payroll" CssClass="control-primary" ToolTip="Forward record(s) to Payroll for Processing" /></li>
                    <li><asp:LinkButton runat="server" ID="lnkPayment" OnClick="lnkPayment_Click" Text="For Review" CssClass="control-primary" ToolTip="Forward record(s) for Review" /></li>
                    <li><asp:LinkButton runat="server" ID="lnkOnHold" OnClick="lnkOnHold_Click" Text="On Hold" CssClass="control-primary" ToolTip="Tag record(s) as On hold" /></li>
                    <li><asp:LinkButton runat="server" ID="lnkRingReleased" OnClick="lnkRingReleased_Click" Text="Gold Ring Issued" CssClass="control-primary" ToolTip="Tag if Gold Ring is already released or issued." /></li>

                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>

                    <uc:ConfirmBox runat="server" ID="cfbForward" TargetControlID="lnkForward" ConfirmMessage="Are you sure you want to forward selected transaction(s) to payroll?"  />
                    <uc:ConfirmBox runat="server" ID="cfbPayment" TargetControlID="lnkPayment" ConfirmMessage="Are you sure you want to post selected transaction(s) for review?"  />
                    <uc:ConfirmBox runat="server" ID="cfbOnHold" TargetControlID="lnkOnHold" ConfirmMessage="Are you sure you want to on hold selected transaction(s)?"  />
                    <uc:ConfirmBox runat="server" ID="cfbRingReleased" TargetControlID="lnkRingReleased" ConfirmMessage="Are you sure you want to tag selected transaction(s) as Gold Ring released or issued?"  />
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                </ul>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkExport" />
                </Triggers>
                </asp:UpdatePanel>     


            </div>


            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="BenefitLoyaltyNo">                                                                                   
                            <Columns>
<%--                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Visible="false" >
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>--%>

                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Width="10%" />

                                <dx:GridViewDataTextColumn FieldName="FDS201" Caption="FDS<br/>Government - Z2" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="HiredDate201" Caption="CBP/BSP Date - Z1" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="DateFrom" Caption="Date From<br/>(actual basis for loyalty)" Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="ActualEffectiveDate" Caption="<center>Actual<br/>Anniversary Date - Z8</center>" Visible="true" />
                                <dx:GridViewDataTextColumn FieldName="DateTo" Caption="<center>Adjusted<br/>Anniversary Date - Z8</center>" Width="7%" Visible="true" />
                                <dx:GridViewDataTextColumn FieldName="LoyaltyTypeDesc" Caption="Loyalty Award" />
                                <dx:GridViewDataTextColumn FieldName="OnHoldDate" Caption="Date On Hold" />
                                <dx:GridViewDataTextColumn FieldName="OnHoldRemarks" Caption="On Hold Remarks" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="SuspendedDate" Caption="Date Suspended" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="ReviewDate" Caption="Date Reviewed" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="DateForwarded" Caption="Date Forwarded" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="DatePaid" Caption="Date Paid" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PolicyRemarks" Caption="Policy Remarks" Visible="false" />


                                <dx:GridViewDataTextColumn FieldName="GAPS" Caption="Service Gap"/>

                                <dx:GridViewDataTextColumn FieldName="StatusRemarks" Caption="Status" />


                                <dx:GridViewDataTextColumn FieldName="NetPay" Caption="Net Pay" />

                                <dx:GridViewDataCheckColumn FieldName="IsWithGoldRing" Caption="With<br/>Gold Ring" Visible="true" />
                                <dx:GridViewDataCheckColumn FieldName="IsRingReleased" Caption="Gold Ring<br/>Released" Visible="true" />
                                <dx:GridViewDataCheckColumn FieldName="IsPaid" Caption="Paid" Visible="true" />
                                <dx:GridViewDataCheckColumn FieldName="IsForwarded" Caption="Forwarded to Payroll" Visible="true" />

                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" FieldName="Remarks" Caption="Remarks" HeaderStyle-HorizontalAlign="Center" Width="10%" Visible="true">
                                    <DataItemTemplate>
                                        <%--<asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' TextMode="MultiLine" Rows="2" style=" font-family: verdana; font-size:11px; border: solid 1px #757575; width: 150px; height:auto" CssClass="form-control" ToolTip='<%# Bind("Remarks") %>' Enabled='<%# Bind("xIsEnabled") %>'></asp:TextBox> --%>    
                                        <dx:ASPxMemo ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' Rows="3" AutoResizeWithContainer="true" style=" font-family: verdana; font-size:11px; border: solid 1px #757575; width: 150px; height:auto" CssClass="form-control" ToolTip='<%# Bind("Remarks") %>' Enabled='<%# Bind("xIsEnabled") %>' OnDataBound="txtRemarks_Load"></dx:ASPxMemo>                            
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn  CellStyle-HorizontalAlign="Center" FieldName="RingIssuedDate"  Caption="<center>Ring</br>Issued Date</center>" HeaderStyle-HorizontalAlign="Center" Visible="true">
                                    <DataItemTemplate>
                                        
<%--                                        <asp:TextBox ID="txtRingIssuedDate" runat="server" Text='<%# Bind("RingIssuedDate") %>' Placeholder="Issued Date"
                                            CssClass="form-control" Width="93px" Visible='<%# Bind("yIsEnabled") %>' Enabled='<%# Bind("y1IsEnabled") %>'></asp:TextBox>  --%>   
<%--                                        <dx:ASPxTextBox ID="txtRingIssuedDate" runat="server" Text='<%# Bind("RingIssuedDate") %>' Width="105px" CssClass="form-control" Visible='<%# Bind("yIsEnabled") %>' Enabled='<%# Bind("y1IsEnabled") %>' OnDataBound="txtRingIssuedDate_Load">
                                            <MaskSettings ShowHints="true" Mask="99/99/9999" />
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="None" />  
                                            <RootStyle BackColor="Transparent" ></RootStyle>  
                                        </dx:ASPxTextBox>  --%>
                                        <dx:ASPxDateEdit ID="txtRingIssuedDate" runat="server" Text='<%# Bind("RingIssuedDate") %>' Width="105px" UseMaskBehavior="true" EditFormatString="MM/dd/yyyy" DisplayFormatString="MM/dd/yyyy" EditFormat="Custom" Visible='<%# Bind("yIsEnabled") %>' Enabled='<%# Bind("y1IsEnabled") %>' ToolTip='<%# Bind("RingIssuedDate") %>' style=" font-family: verdana; font-size:11px; border: solid 1px #757575;" OnDataBound="txtRingIssuedDate_Load" >

                                        </dx:ASPxDateEdit>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" ToolTip="View AWOL/Rating/DA Details" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="LWOP/AWOL" HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="false">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetlAWOL" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetailsAWOL_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="RATING" HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="false">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetlRating" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetailsRating_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="ADU" HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="false">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetlADU" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetailsADU_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>--%>
                                

                                <%--<dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Facility" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" Visible="false" />      
                                <dx:GridViewDataComboBoxColumn FieldName="LocationDesc" Caption="Location" Visible="false" />                                                                                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="ProjectDesc" Caption="Project" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />--%>      
                                                         
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />  
                        <asp:SqlDataSource runat="server" ID="SqlDataSource1">
                            <%--<SelectParameters>
                                <asp:Parameter Name="OnlineUserNo" Type="Int32" DefaultValue="-99" />
                                <asp:Parameter Name="PayLocNo" Type="Int32" DefaultValue="0" />
                                <asp:Parameter Name="TabIndex" Type="Int32" DefaultValue="1" />
                            </SelectParameters>--%>
                        </asp:SqlDataSource>   
                        <dx:ASPxHiddenField ID="hfMemo" runat="server" ClientInstanceName="chfMemo" />  
                        <dx:ASPxHiddenField ID="hfRingDate" runat="server" ClientInstanceName="chfRingDate" />                     
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
<%--</div>--%>
<br />

<%--<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    <asp:Label runat="server" ID="lbl" />
                </div>
                <div>                
                    <ul class="panel-controls"> 
                        &nbsp;                                                       
                        <li><asp:LinkButton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" Text="Save" CssClass="control-primary" Visible="false" /></li>                        
                    </ul>                                                                                                                                                                         
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="LeaveApplicationDetiNo" Width="100%">
                            <Columns>                                
                                <dx:GridViewDataTextColumn FieldName="LeaveApplicationDetiTransNo" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="xDTRDate" Caption="DTR Date" />
                                <dx:GridViewDataTextColumn FieldName="DayDesc" Caption="Day" />
                                <dx:GridViewDataTextColumn FieldName="PaidHrs" Caption="Paid Hours"  />                                                                                                                        
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>--%>

<%--<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style=" display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtLeaveApplicationNo" CssClass="form-control" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtLeaveApplicationTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-6">
                     <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
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
                <label class="col-md-4 control-label has-required">Leave Type :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboLeavetypeNo" DataMember="ELeaveType" runat="server" CssClass="form-control required"
                        ></asp:Dropdownlist>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control required" Placeholder="From"
                        ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtStartDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                        <asp:RangeValidator
                        ID="RangeValidator3"
                        runat="server"
                        ControlToValidate="txtStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender6"
                        TargetControlID="RangeValidator3"
                        />   
                </div>

                <!-- <label class="col-md-1 control-label">To :</label> -->
                <div class="col-md-3">
                    <asp:TextBox ID="txtEndDate" runat="server"  CssClass="form-control required" Placeholder="To"
                        ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtEndDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                            <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />

                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender2"
                        TargetControlID="RangeValidator4"
                        />       

                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Filed Hrs :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAppliedHrs" runat="server" CssClass="form-control required" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtAppliedHrs" />
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-6">
                    <asp:Checkbox ID="txtISForAM" Text="&nbsp; Check for half day leave (AM only)" runat="server" >
                        </asp:Checkbox>
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Reason :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtReason" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control required" 
                        ></asp:Textbox>
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-4 control-label has-required ">Approval Status :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboApprovalStatNo" DataMember="EApprovalStat" runat="server"  CssClass="form-control required"
                        ></asp:Dropdownlist>
                </div>
            </div>
        </div>
        <br />
        
         </fieldset>
</asp:Panel>--%>


<!-- VIEWING OF AWOL HERE... -->

<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    <asp:Label runat="server" ID="lblAwol" />
                </div>
                <div>                
                    <ul class="panel-controls"> 
                        &nbsp;                                                       
                        <li><asp:LinkButton runat="server" ID="lnkAddAWOL" OnClick="lnkAddAWOL_Click" Text="Add" CssClass="control-primary" /></li>        
                        <li><asp:LinkButton runat="server" ID="lnkDeleteAWOL" OnClick="lnkDeleteAWOL_Click" Text="Delete" CssClass="control-primary" /></li>   
                        
                        <uc:ConfirmBox runat="server" ID="cfbDeleteAWOL" TargetControlID="lnkDeleteAWOL" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />         
                    </ul>                                                                                                                                                                         
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdDetlAWOL" ClientInstanceName="grdDetlAWOL" runat="server" KeyFieldName="BenefitLoyaltyAwolNo" Width="100%">
                            <Columns>      
                            
                                <dx:GridViewDataTextColumn FieldName="BenefitLoyaltyAwolTransNo" Caption="Trans. No." Visible="false" />
                                
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditAWOL" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditAWOL_Click" CausesValidation="false" ToolTip='<%# Bind("Code") %>' Enabled='<%# Bind("IsEnabled") %>' Visible='<%# Bind("IsEnabled") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataTextColumn FieldName="DTRDate" Caption="DTR Date" />
                                <dx:GridViewDataTextColumn FieldName="LeaveCode" Caption="Leave Code" />
                                <dx:GridViewDataTextColumn FieldName="AWOLHRS" Caption="TOTAL HRS" />
                                <dx:GridViewDataTextColumn FieldName="AWOLDAYS" Caption="TOTAL NO. OF DAYS"  />     
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Width="20%" />
                                          
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />                                             
                        
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>
<br />

<!-- VIEWING OF RATING HERE... -->

<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    <asp:Label runat="server" ID="lblRating" />
                </div>
                <div>                
                    <ul class="panel-controls"> 
                        &nbsp;                                                                         
                    </ul>                                                                                                                                                                         
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdDetlRating" ClientInstanceName="grdDetlRating" runat="server" KeyFieldName="BenefitLoyaltyNo" Width="100%">
                            <Columns>      
                            
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="YEAR" />
                                <dx:GridViewDataTextColumn FieldName="AveRating" Caption="Rating" />
                                <dx:GridViewDataTextColumn FieldName="AdjectivalRating" Caption="Adjectival Rating" />
                                                 
                        
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>
<br />

<!-- VIEWING OF ADU HERE... -->

<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    <asp:Label runat="server" ID="lblADU" />
                </div>
                <div>                
                    <ul class="panel-controls"> 
                        &nbsp;                                                                         
                    </ul>                                                                                                                                                                         
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                    
                        <dx:ASPxGridView ID="grdDetlADU" ClientInstanceName="grdDetlADU" runat="server" KeyFieldName="BenefitLoyaltyNo" Width="100%">
                            <Columns>      
                            
                                <dx:GridViewDataTextColumn FieldName="DateOfCharged" Caption="Date Of Charge" Width="15%" />
                                <dx:GridViewDataTextColumn FieldName="DAARInvestStatus" Caption="Investigation Status" Width="25%" />
                                <dx:GridViewDataTextColumn FieldName="OffenseClassification" Caption="Offense Classification" Width="30%" />
                                <dx:GridViewDataTextColumn FieldName="FinalExecutoryDate" Caption="Final and Executory Date" Width="20%" />                                      
                        
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>
<br />



<!-- ENCODING OF AWOL HERE... -->

<asp:Button ID="btnShowAWOLAdd" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetlAWOLAdd" runat="server" TargetControlID="btnShowAWOLAdd" PopupControlID="pnlPopupDetlAWOLAdd"
    CancelControlID="imgCloseAWOL" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetlAWOLAdd" runat="server" CssClass="entryPopup" style=" display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgCloseAWOL" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveAWOL" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSaveAWOL_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtBenefitLoyaltyAwolNo" runat="server" CssClass="form-control" Visible="false"></asp:Textbox>
                    <asp:Textbox ID="txtBenefitLoyaltyNo" runat="server" CssClass="form-control" Visible="false"></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtBenefitLoyaltyAwolTransNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDTRDate" runat="server" CssClass="form-control" Placeholder="DTR Date"
                        ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtDTRDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtDTRDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                        <asp:RangeValidator
                        ID="RangeValidator3"
                        runat="server"
                        ControlToValidate="txtDTRDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender6"
                        TargetControlID="RangeValidator3"
                        />   
                </div>

            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">No. of Days (AWOL/LWOP) :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTotalDays" runat="server" CssClass="form-control required" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtTotalDays" />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" Type="Integer" 
                        ControlToValidate="txtTotalDays" ErrorMessage="Value must be a whole number" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Remarks :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRemarks" TextMode="MultiLine" Rows="3"  runat="server" CssClass="form-control required" 
                        ></asp:Textbox>
                </div>
            </div>
        

        </div>
        <br />
        
         </fieldset>
</asp:Panel>


</asp:Content>
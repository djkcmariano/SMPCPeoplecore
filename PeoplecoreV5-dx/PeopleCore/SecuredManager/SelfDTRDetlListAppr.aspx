<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfDTRDetlListAppr.aspx.vb" Inherits="SecuredMananger_SelfDTRDetlListAppr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">
    //	    function OnTextChanged(keyValue, reason) {
    //	        var Key = keyValue.toString();
    //	        hfChanges.Set(Key, reason);
    //	    }

    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }


    //	    function cbCheckAllMain_CheckedChanged(s, e) {
    //	        grdDetl.PerformCallback(s.GetChecked().toString());

    //	    }
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" CssClass="form-control" runat="server" OnSelectedIndexChanged="lnkSearch_Click" AutoPostBack="true">
                            <asp:ListItem Value="1">DTR</asp:ListItem>
                            <asp:ListItem Value="2">DTR Discrepancy</asp:ListItem>
                        </asp:Dropdownlist>
                    </div>
                    <div class="col-md-4 pull-right">
                            <div class="input-group">
                                <asp:DropDownList ID="cboDTRSource" runat="server" CssClass="form-control" OnSelectedIndexChanged="lnkSearch_Click" AutoPostBack="true">
                                    <asp:ListItem Value="1">DTR Summary</asp:ListItem>
                                    <asp:ListItem Value="2">DTR Detail</asp:ListItem>
                                </asp:DropDownList>
                                
                                <div class="input-group-btn">    
                                    <asp:Button runat="server" ID="lnkSearch" OnClick="lnkSearch_Click" CausesValidation="false" CssClass="btn btn-default" ToolTip="Click here to search" Text="Go!" />            
                                    <asp:PlaceHolder runat="server" ID="PlaceHolder2">            
                                        <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">&nbsp;<span class="caret"></span></button>
                                        <div class="dropdown-menu dropdown-menu-right drp-menu-size">
                                          <div class="form-horizontal">
                                                <div class="panel-body">
                                                      <div class="form-group">
                                                            <label class="col-md-4 control-label">Start Date :</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtStartDate" CssClass="form-control required" SkinID="txtdate" />
                                                            </div>
                                                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtStartDate" Format="MM/dd/yyyy" />
                                                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtStartDate" Mask="99/99/9999" MaskType="Date" />
                                                            <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtStartDate" Display="Dynamic" />
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-4 control-label">End Date :</label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtEndDate" CssClass="form-control required" SkinID="txtdate" />
                                                            </div>
                                                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtEndDate" Format="MM/dd/yyyy" />
                                                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtEndDate" Mask="99/99/9999" MaskType="Date" />
                                                            <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtEndDate" Display="Dynamic" />

                                                        </div>   
                                                </div>       
                                           </div>                                   
                                        </div>            
                                    </asp:PlaceHolder>
                                    
                                </div>
                            </div>
                    </div>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>                    
                    <ul class="panel-controls" style="right: 20px; top:10px"> 
                        <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDisApproved" OnClick="lnkDisApproved_Click" Text="Disapprove" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkApproved" ConfirmMessage="Are you sure you want to approve?"  />
                    <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDisApproved" ConfirmMessage="Are you sure you want to disapprove?"  />                              
                </ContentTemplate>
                <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                </Triggers>
                
                </asp:UpdatePanel>

            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRDetiNo" OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                                         
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Width="5%" />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Width="14%" />     
                                <dx:GridViewDataTextColumn FieldName="DTRCutoff" Caption="DTR Cut-off" />    
                                <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Req.<br/>Hrs" />                                                                     
                                <dx:GridViewDataTextColumn FieldName="WorkingHrs" Caption="Hrs<br/>Work" />                  
                                <dx:GridViewDataTextColumn FieldName="HolidayHrs" Caption="Paid<br/>Hol."  Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PaidLeave" Caption="Paid<br/>Leave" />
                                <dx:GridViewDataTextColumn FieldName="DOvt" Caption="OT" />
                                <dx:GridViewDataTextColumn FieldName="DOvt8" Caption="OT8" />
                                <dx:GridViewDataTextColumn FieldName="DNP" Caption="NP" />
                                <dx:GridViewDataTextColumn FieldName="DNP8" Caption="NP8" />
                                <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Absent" />
                                <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" />
                                <dx:GridViewDataTextColumn FieldName="Under" Caption="Undertime" />
                                <dx:GridViewDataTextColumn FieldName="ApprovalStatus" Caption="Approval Status" />
                                <dx:GridViewDataTextColumn FieldName="ApprovedDisapprovedBy" Caption="Approved/Disapproved By" />
                                <dx:GridViewDataTextColumn FieldName="ActualLate" Caption="<center>Actual<br/>Late</center>" PropertiesTextEdit-EncodeHtml="false" Visible="False" />
                                <dx:GridViewDataTextColumn FieldName="ActualUnder" Caption="<center>Actual<br/>Undertime</center>" PropertiesTextEdit-EncodeHtml="false" Visible="False" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="2%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                <%--<dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
				                </dx:GridViewCommandColumn>--%>                                          
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
                    <asp:Label ID="lblDetl" runat="server" />
                </div>
                <div> 
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">                                    
                            <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                    </Triggers>
                    </asp:UpdatePanel>                                                                                                                                                                                                                            
                </div>                                                                                                                                      
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">                        
                        <dx:ASPxGridView ID="grdDetl" runat="server" ClientInstanceName="grdDetl" KeyFieldName="DTRDetiLogNo" Width="100%" EnableRowsCache="false" >                                                                                 
                            <Columns>
                                <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                <dx:GridViewDataColumn FieldName="DTRDate" Caption="Date" />
                                <dx:GridViewDataColumn FieldName="DayOffDesc" Caption="Day" /> 
                                <dx:GridViewDataColumn Caption="Shift Code" FieldName="ShiftCode" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkViewShift"  Font-Size="X-Small" Text='<%# Bind("ShiftCode") %>' CommandArgument='<%# Bind("ShiftNo") %>' OnClick="lnkViewShift_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                                             
                                <dx:GridViewDataColumn FieldName="DayTypeCode" Caption="D-Type" /> 
                                <dx:GridViewDataColumn FieldName="DayTypeCode2" Caption="D-Type2" Visible="false" /> 
                                <dx:GridViewDataColumn FieldName="Hrs" Caption="Req.<br/>Hrs" />                 
                                <dx:GridViewDataColumn FieldName="ActualHrs" Caption="Actual Hrs" Visible="false" />
                                <dx:GridViewDataColumn FieldName="HolidayHrs" Caption="<center>Holiday<br/>Hrs</center>" Visible="false" />
                                <dx:GridViewDataColumn FieldName="In1" Caption="In1" />
                                <dx:GridViewDataColumn FieldName="Out1" Caption="Out1" />
                                <dx:GridViewDataColumn FieldName="In2" Caption="In2" />
                                <dx:GridViewDataColumn FieldName="Out2" Caption="Out2" />
                                <dx:GridViewDataColumn FieldName="OvtIn1" Caption="OT<br/>In1" />
                                <dx:GridViewDataColumn FieldName="OvtOut1" Caption="OT<br/>Out1" />
                                <dx:GridViewDataColumn FieldName="OvtIn2" Caption="OT<br/>In2" />
                                <dx:GridViewDataColumn FieldName="OvtOut2" Caption="OT<br/>Out2" />
                                <dx:GridViewDataColumn FieldName="WorkingHrs" Caption="Work<br/>Hrs" />
                                <dx:GridViewDataColumn FieldName="Ovt" Caption="OT" />
                                <dx:GridViewDataColumn FieldName="Ovt8" Caption="OT8" />    
                                <dx:GridViewDataColumn FieldName="NP" Caption="NP" />
                                <dx:GridViewDataColumn FieldName="NP8" Caption="NP8" />                            
                                <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Absent" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="Under" Caption="Undertime" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="ActualLate" Caption="<center>Actual<br/>Late</center>" PropertiesTextEdit-EncodeHtml="false" Visible="False" />
                                <dx:GridViewDataTextColumn FieldName="ActualUnder" Caption="<center>Actual<br/>Undertime</center>" PropertiesTextEdit-EncodeHtml="false" Visible="False" />
                                <%--<dx:GridViewDataColumn FieldName="LeaveTypeCode" Caption="<center>Charged to</center>" />--%>
                                <dx:GridViewDataColumn Caption="Charged to" FieldName="LeaveTypeCodes" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkViewLeaveType"  Font-Size="X-Small" Text='<%# Bind("LeaveTypeCodes") %>' ToolTip='<%# Bind("LeaveTypeCodes_Hrs") %>' CommandArgument='<%# Bind("DTRNo") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LeaveHrs" Caption="<center>Leave<br/>Hrs</center>" />    
                                <dx:GridViewDataColumn FieldName="Remarks" Caption="Remarks" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Work Allocation" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Width="1%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkView_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                              
                            </Columns>
                            <SettingsPager PageSize="60" /> 
                            <SettingsContextMenu Enabled="true">                                
                                <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                            </SettingsContextMenu>                                                                                            
                            <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="True" />      
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdDetl"  />
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

</asp:Content>


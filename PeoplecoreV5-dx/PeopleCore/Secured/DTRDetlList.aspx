<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DTRDetlList.aspx.vb" Inherits="Secured_DTRDetlList" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/History.ascx" TagName="History" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--<script type="text/javascript">
    function UpdateDetailGrid(s, e) {
        grdDetl.PerformCallback(grdMain.GetFocusedRowIndex());
    }
    </script>--%>
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

<uc:DTRHeader runat="server" ID="DTRHeader" />
     
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">                                                 
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>                                       
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkApproved" OnClick="lnkApproved_Click" Text="Approve" Visible="False" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDisApproved" OnClick="lnkDisApproved_Click" Text="Disapprove" CssClass="control-primary" Visible="False" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>                    
                        </ul>   
                            <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkApproved" ConfirmMessage="Are you sure you want to approve?"  />
                            <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="lnkDisApproved" ConfirmMessage="Are you sure you want to disapprove?"  />                                                  
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                         
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRDetiNo" OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                                         
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Visible="False">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Caption="History" CellStyle-HorizontalAlign="Center" Width="10" Visible="False">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkHistory" OnClick="lnkHistory_Click" CssClass="fa fa-paperclip " CommandArgument='<%# Eval("DTRDetiNo") %>' Font-Size="Medium"  />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />    
                                <dx:GridViewDataTextColumn FieldName="Hrs" Caption="Req. Hrs" />                                                                       
                                <dx:GridViewDataTextColumn FieldName="WorkingHrs" Caption="Work Hrs" />                  
                                <dx:GridViewDataTextColumn FieldName="PaidLeave" Caption="Leave Hrs" />
                                <dx:GridViewDataTextColumn FieldName="HolidayHrs" Caption="Paid Hol." />
                                <dx:GridViewDataTextColumn FieldName="DOvt" Caption="OT" />
                                <dx:GridViewDataTextColumn FieldName="DOvt8" Caption="OT8" />
                                <dx:GridViewDataTextColumn FieldName="DNP" Caption="NP" />
                                <dx:GridViewDataTextColumn FieldName="DNP8" Caption="NP8" />
                                <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Absent" />
                                <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" />
                                <dx:GridViewDataTextColumn FieldName="Under" Caption="Under" />
                               <%-- <dx:GridViewDataTextColumn FieldName="ApprovalStatus" Caption="Approval Status" />
                                <dx:GridViewDataTextColumn FieldName="ApprovedDisapprovedBy" Caption="Approved/Disapproved By"  />--%>
                                <dx:GridViewDataTextColumn FieldName="ProjectDesc" Caption="Project" Visible="false" />  
                                <dx:GridViewDataTextColumn FieldName="LocationDesc" Caption="Location" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="GroupbyDesc" Caption="Group By" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="SectionDesc" Caption="Section" Visible="false" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" /> 
                                <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="" HeaderStyle-HorizontalAlign="Center" >
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkProcess" CssClass="control-primary" Text="Process" OnClick="lnkProcess_Click" />
                                       
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn> --%>                                         
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
                <div class="panel-title">
                    Name:&nbsp;&nbsp;<asp:Label ID="lblDetl" runat="server" />
                </div>
                <div> 
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">                 
                            <li><asp:LinkButton runat="server" ID="lnkPreviousDTR" Text="Previous DTR" CssClass="control-primary" data-toggle="collapse" href="#collapse1" /></li>                
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
                    <div id="collapse1" class="panel-collapse collapse">

                          <h3>Previous DTR</h3>
                          <div class="table-responsive">                        
                            <dx:ASPxGridView ID="grdDetlPrev" runat="server" ClientInstanceName="grdDetlPrev" KeyFieldName="DTRDetiLogNo" Width="100%" EnableRowsCache="false"  Border-BorderColor="Red"  >                                                                                 
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="DTRDate" Caption="Date" />
                                    <dx:GridViewDataColumn FieldName="DayOffDesc" Caption="Day" /> 
                                    <dx:GridViewDataColumn Caption="Shift Code" CellStyle-HorizontalAlign="Center" Width="10">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkViewShift"  Font-Size="X-Small" Text='<%# Bind("ShiftCode") %>' CommandArgument='<%# Bind("ShiftNo") %>' OnClick="lnkViewShift_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                                                                            
                                    <dx:GridViewDataColumn FieldName="DayTypeCode" Caption="D-Type" /> 
                                    <dx:GridViewDataColumn FieldName="DayTypeCode2" Caption="D-Type2" /> 
                                    <dx:GridViewDataColumn FieldName="Hrs" Caption="Req.<br/>Hrs" />                  
                                    <dx:GridViewDataColumn FieldName="ActualHrs" Caption="Actual Hrs" Visible="false" />
                                    <dx:GridViewDataColumn FieldName="In1" Caption="In1" />
                                    <dx:GridViewDataColumn FieldName="Out1" Caption="Out1" />
                                    <dx:GridViewDataColumn FieldName="In2" Caption="In2" Visible="false"/>
                                    <dx:GridViewDataColumn FieldName="Out2" Caption="Out2" Visible="false"/>
                                    <dx:GridViewDataColumn FieldName="OvtIn1" Caption="OT<br/>In1" />
                                    <dx:GridViewDataColumn FieldName="OvtOut1" Caption="OT<br/>Out1" />
                                    <dx:GridViewDataColumn FieldName="OvtIn2" Caption="OT<br/>In2" />
                                    <dx:GridViewDataColumn FieldName="OvtOut2" Caption="OT<br/>Out2" />
                                    <dx:GridViewDataColumn FieldName="WorkingHrs" Caption="Work<br/>Hrs" />
                                    <dx:GridViewDataColumn FieldName="Ovt" Caption="OT" />
                                    <dx:GridViewDataColumn FieldName="Ovt8" Caption="OT8" />        
                                    <%--<dx:GridViewDataColumn FieldName="NP" Caption="NP" />--%>
                                    <dx:GridViewDataColumn FieldName="CNP" Caption="NP" />
                                    <dx:GridViewDataColumn FieldName="CNPOT" Caption="NPOT" />  
                                    <dx:GridViewDataColumn FieldName="NP8" Caption="NP8" />  
                                                          
                                    <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Abs." PropertiesTextEdit-EncodeHtml="false" />
                                    <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" PropertiesTextEdit-EncodeHtml="false" />
                                    <dx:GridViewDataTextColumn FieldName="Under" Caption="Under" PropertiesTextEdit-EncodeHtml="false" />
                                    <dx:GridViewDataColumn FieldName="LeaveTypeCode" Caption="L-Type" />
                                    <dx:GridViewDataColumn FieldName="LeaveHrs" Caption="L-Hrs" />    
                                    <dx:GridViewDataColumn FieldName="HolidayHrs" Caption="Holiday Pay" />
                                                           
                                </Columns>
                                <SettingsPager PageSize="60" /> 
                                <SettingsContextMenu Enabled="true">                                
                                    <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                </SettingsContextMenu>                                                                                            
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="True" />      
                            </dx:ASPxGridView>
                        </div>
                      <br />
                    </div>

                    <h3><asp:Label ID="lblTab" runat="server" /></h3>
                    <div class="table-responsive">                        
                        <dx:ASPxGridView ID="grdDetl" runat="server" ClientInstanceName="grdDetl" KeyFieldName="DTRDetiLogNo" Width="100%" EnableRowsCache="false" >                                                                                 
                            <Columns>
                                <dx:GridViewDataColumn FieldName="DTRDate" Caption="Date" />
                                <dx:GridViewDataColumn FieldName="DayOffDesc" Caption="Day" />
                                <dx:GridViewDataColumn Caption="Shift Code" FieldName="ShiftCode" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkViewShift"  Font-Size="X-Small" Text='<%# Bind("ShiftCode") %>' CommandArgument='<%# Bind("ShiftNo") %>' OnClick="lnkViewShift_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                                             
                                <dx:GridViewDataColumn FieldName="DayTypeCode" Caption="D-Type" /> 
                                <dx:GridViewDataColumn FieldName="DayTypeCode2" Caption="D-Type2"  Visible="false"/> 
                                <dx:GridViewDataColumn FieldName="Hrs" Caption="Req.<br/>Hrs" />   
                                <dx:GridViewDataColumn FieldName="ActualHrs" Caption="Actual Hrs" Visible="false" />
                                <dx:GridViewDataColumn FieldName="In1" Caption="In1" />
                                <dx:GridViewDataColumn FieldName="Out1" Caption="Out1" />
                                <dx:GridViewDataColumn FieldName="In2" Caption="In2" Visible="false"/>
                                <dx:GridViewDataColumn FieldName="Out2" Caption="Out2" Visible="false"/>
                                <dx:GridViewDataColumn FieldName="OvtIn1" Caption="OT<br/>In1" />
                                <dx:GridViewDataColumn FieldName="OvtOut1" Caption="OT<br/>Out1" />
                                <dx:GridViewDataColumn FieldName="OvtIn2" Caption="OT<br/>In2" />
                                <dx:GridViewDataColumn FieldName="OvtOut2" Caption="OT<br/>Out2" />
                                <dx:GridViewDataColumn FieldName="WorkingHrs" Caption="Work<br/>Hrs" />
                                <dx:GridViewDataColumn FieldName="Ovt" Caption="OT" />
                                <dx:GridViewDataColumn FieldName="Ovt8" Caption="OT8" />        
                                <%--<dx:GridViewDataColumn FieldName="NP" Caption="NP" />--%>
                                <dx:GridViewDataColumn FieldName="CNP" Caption="NP" />
                                <dx:GridViewDataColumn FieldName="CNPOT" Caption="NPOT" />
                                <dx:GridViewDataColumn FieldName="NP8" Caption="NP8" />                        
                                <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Abs." PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" PropertiesTextEdit-EncodeHtml="false" />
                                <dx:GridViewDataTextColumn FieldName="Under" Caption="Under" PropertiesTextEdit-EncodeHtml="false" />
                                <%--<dx:GridViewDataColumn FieldName="LeaveTypeCode" Caption="L-Type" />--%>
                                <dx:GridViewDataColumn Caption="Charged to" FieldName="LeaveTypeCodes" CellStyle-HorizontalAlign="Center" Width="10">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkViewLeaveType"  Font-Size="X-Small" Text='<%# Bind("LeaveTypeCodes") %>' ToolTip='<%# Bind("LeaveTypeCodes_Hrs") %>' CommandArgument='<%# Bind("DTRNo") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LeaveHrs" Caption="<center>Leave<br>Hrs</center>" />  
                                <dx:GridViewDataColumn FieldName="LeaveHrs" Caption="L-Hrs" />      
                                <dx:GridViewDataColumn FieldName="HolidayHrs" Caption="Holiday Pay" /> 
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
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdDetl" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            
            <div class="form-group"">
                <label class="col-md-4 control-label">DTR Summary No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDTRDetiNo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group"">
                <label class="col-md-4 control-label">Employee Code :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEmployeeCode" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group"">
                <label class="col-md-4 control-label">Employee Name :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtFullName" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Overtime Hrs :</label>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Ovt</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Ovt>8</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Ovt NP</label><br />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Ovt NP>8</label><br />
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Regular Working Day :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtNPOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtNPOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Rest Day :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRDOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRDOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRDOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRDOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Regular Holiday RWD :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHNROvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHNROvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHNROvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHNROvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Regular Holiday RD :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHRDOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHRDOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHRDOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtRHRDOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Special Holiday RWD :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHNROvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHNROvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHNROvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHNROvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-3 control-label has-space">Special Holiday RD :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHRDOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHRDOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHRDOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtSHRDOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                    </center>
                </div>
            </div>
             
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pInfomation" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="Fieldset1">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
        </div>
        <div class="container-fluid entryPopupDetl">        
            <div class="row">
                <div class="page-content-wrap">         
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                <ContentTemplate>
                                    <div class="col-md-6">
                                        <asp:HiddenField runat="server" ID="hifNo" />
                                    </div>                                                                                                      
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>                            
                            <div class="panel-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <dx:ASPxGridView ID="grdHistory" ClientInstanceName="grdHistory" runat="server" KeyFieldName="DTRDetiNo" Width="100%">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No"/> 
                                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name"/> 
                                                <dx:GridViewDataTextColumn FieldName="UserName" Caption="Encoded By"/> 
                                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date"/> 
                                                <dx:GridViewBandColumn Caption="Regular Work Day" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Ovt" Caption="Ovt"/>   
                                                    <dx:GridViewDataTextColumn FieldName="Ovt8" Caption="Ovt8"/>   
                                                    <dx:GridViewDataTextColumn FieldName="OvtNP" Caption="Np"/>   
                                                    <dx:GridViewDataTextColumn FieldName="Ovt8NP" Caption="Np8"/> 
                                                </Columns>
                                                </dx:GridViewBandColumn>  
                                                <dx:GridViewBandColumn Caption="Rest Day" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="RDOvt" Caption="Ovt"/>   
                                                    <dx:GridViewDataTextColumn FieldName="RDOvt8" Caption="Ovt8"/>   
                                                    <dx:GridViewDataTextColumn FieldName="RDOvtNP" Caption="Np"/>   
                                                    <dx:GridViewDataTextColumn FieldName="RDOvt8NP" Caption="Np8"/> 
                                                 </Columns> 
                                                </dx:GridViewBandColumn>     
                                                <dx:GridViewBandColumn Caption="Special Holiday Non Rest Day" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="SHNROvt" Caption="Ovt"/>   
                                                    <dx:GridViewDataTextColumn FieldName="SHNROvt8" Caption="Ovt8"/>   
                                                    <dx:GridViewDataTextColumn FieldName="SHNROvtNP" Caption="Np"/>   
                                                    <dx:GridViewDataTextColumn FieldName="SHNROvt8NP" Caption="Np8"/> 
                                                 </Columns> 
                                                </dx:GridViewBandColumn>  
                                                <dx:GridViewBandColumn Caption="Special Holiday Rest Day" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="SHRDOvt" Caption="Ovt"/>   
                                                    <dx:GridViewDataTextColumn FieldName="SHRDOvt8" Caption="Ovt8"/>   
                                                    <dx:GridViewDataTextColumn FieldName="SHRDOvtNP" Caption="Np"/>   
                                                    <dx:GridViewDataTextColumn FieldName="SHRDOvt8NP" Caption="Np8"/> 
                                                 </Columns> 
                                                </dx:GridViewBandColumn>   
                                                <dx:GridViewBandColumn Caption="Regular Holiday Non Rest Day" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="RHNROvt" Caption="Ovt"/>   
                                                    <dx:GridViewDataTextColumn FieldName="RHNROvt8" Caption="Ovt8"/>   
                                                    <dx:GridViewDataTextColumn FieldName="RHNROvtNP" Caption="Np"/>   
                                                    <dx:GridViewDataTextColumn FieldName="RHNROvt8NP" Caption="Np8"/> 
                                                 </Columns> 
                                                </dx:GridViewBandColumn>  
                                                <dx:GridViewBandColumn Caption="Regular Holiday Rest Day" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="RHRDOvt" Caption="Ovt"/>   
                                                    <dx:GridViewDataTextColumn FieldName="RHRDOvt8" Caption="Ovt8"/>   
                                                    <dx:GridViewDataTextColumn FieldName="RHRDOvtNP" Caption="Np"/>   
                                                    <dx:GridViewDataTextColumn FieldName="RHRDOvt8NP" Caption="Np8"/> 
                                                 </Columns> 
                                                </dx:GridViewBandColumn>                                                   
                                            </Columns>
                                            <SettingsBehavior AllowSort="false" AllowGroup="false" />                            
                                        </dx:ASPxGridView>                                
                                    </div>
                                </div>                                                           
                            </div>                   
                        </div>
                    </div>
                </div>
            </div>        
        </div>
    </fieldset>
</asp:Panel>

</asp:Content>


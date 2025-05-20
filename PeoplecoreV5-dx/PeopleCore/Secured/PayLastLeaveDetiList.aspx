<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayLastLeaveDetiList.aspx.vb" Inherits="Secured_PayLastLeaveDetiList" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/HeaderInfo.ascx" TagName="HeaderInfo" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc:HeaderInfo runat="server" ID="HeaderInfo1" />

<uc:Tab runat="server" ID="Tab">
    <Header>        
        <asp:Label runat="server" ID="lbl" />     
        <div style="display:none;">
            <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
        </div>      
    </Header>
    <Content>            
        <br />
        <div class="page-content-wrap" >         
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
                                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grd" runat="server" KeyFieldName="PayLeaveDetiNo" Width="100%">                                                                                   
                                    <Columns>   
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." />                         
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                        <dx:GridViewDataTextColumn FieldName="NoOfLeaves" Caption="No. of Leaves" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="GrossAmount" Caption="Gross" PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="Adjustment" Caption="Adjustment"  PropertiesTextEdit-DisplayFormatString="{0:N2}" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeRateClassDesc" Caption="Rate Class" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="CalendarYear" Caption="Calendar Year" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="HourlyRate" Caption="Hourly Rate"  PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewBandColumn Caption="Leave Hrs" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Leavehrs" Caption="Non-Tax" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                                <dx:GridViewDataTextColumn FieldName="LeaveHrsTax" Caption="Taxable" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                                <dx:GridViewDataTextColumn FieldName="TotalLeaveHrs" Caption="Total" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            </Columns>
                                        </dx:GridViewBandColumn>
                                        <dx:GridViewBandColumn Caption="Net Pay" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="NetPay" Caption="Non-Tax" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                                <dx:GridViewDataTextColumn FieldName="NetPayTax" Caption="Taxable" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                                <dx:GridViewDataTextColumn FieldName="TotalNetPay" Caption="Total" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                            </Columns>
                                        </dx:GridViewBandColumn>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Detail" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>  
                                    </Columns>    
                                    <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />                         
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
                        <div class="panel-title" >
                            Transaction No. :&nbsp; <asp:Label runat="server" ID="lblDetl" />
                        </div>                   
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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
                                <h3>Leave Adjustment</h3>
                                <div class="table-responsive">                    
                                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="LeaveCreditNo" Width="100%" >                                                                                   
                                        <Columns>      
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                     
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />                            
                                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />                                                        
                                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" Visible="false" />
                                            <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                            <dx:GridViewDataTextColumn FieldName="AcquireDate" Caption="Acquired Date" />
                                            <dx:GridViewDataTextColumn FieldName="LeaveHrs" Caption="Leave Hrs" />
                                            <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remarks" />
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />                                                                          
                                        </Columns>
                                        <SettingsPager Mode="ShowPager" />                                
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />  
                                </div>                            
                            </div>
                    </div>
                </div>
            </div>
        </div>  
        
                   
    </Content>
</uc:Tab>



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
                    <asp:Textbox ID="txtLeaveCreditNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control"  Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Leave Type :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboLeaveTypeNo" CausesValidation="false" DataMember="ELeaveType" runat="server" CssClass="required form-control" ReadOnly="true"></asp:DropdownList>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Leave Hrs. :</label>
                <div class="col-md-3">
                   <asp:TextBox ID="txtLeaveHrs" runat="server" CssClass="required number form-control"></asp:TextBox>
                   <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtLeaveHrs" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender1" runat="server" />
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Acquired Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAcquireDate" runat="server" CssClass="required form-control"> </asp:TextBox>

                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAcquireDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtAcquireDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtAcquireDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="01-01-1900" MaximumValue="12-31-3000" Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator3" /> 
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Remark :</label>
                <div class="col-md-6">
                   <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="3" runat="server" CssClass="required form-control"></asp:TextBox>
                 </div>
            </div>

        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

     
</asp:Content>


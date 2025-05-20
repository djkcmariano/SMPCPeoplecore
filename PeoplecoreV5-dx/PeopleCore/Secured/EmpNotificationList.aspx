<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpNotificationList.aspx.vb" Inherits="Secured_EmpNotificationList" %>
<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>

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
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" Width="100%" KeyFieldName="EmployeeNotificationNo" Styles-Cell-Paddings-PaddingTop="10" Styles-Cell-Paddings-PaddingBottom="10" >                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center" Width="6%" >
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Trans. No." Visible="false" />
                                <dx:GridViewDataDateColumn FieldName="DateNotified"  Width="10%" Caption="Date" GroupIndex="0" SortOrder="Descending" Visible="false" >
                                      <PropertiesDateEdit DisplayFormatString="g" />
			                    </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name" />    
                                <dx:GridViewDataComboBoxColumn FieldName="NotificationTypeDesc" Caption="Subject" />
                                <dx:GridViewDataTextColumn FieldName="DueDate" Caption="Date Due" Width="10%" />        
                                <dx:GridViewDataTextColumn FieldName="HRANTypeDesc" Caption="HRAN" />    
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Width="12%" />     
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Visible="false"/> 
                                <dx:GridViewDataTextColumn FieldName="ServedBy" Caption="Served By" Visible="false"/>  
                                <dx:GridViewDataTextColumn FieldName="DateServed" Caption="Date Served" Visible="false"/>   
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false"/>                                                                           
                                <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Business Unit" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group" Visible="false" />                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Team" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="GenderDesc" Caption="Gender" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Sub Team" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="DateHired" Caption="Date Hired" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="RegularizedDate" Caption="Date Regularized" Visible="false" />                                                                                                                          
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="6%" />
                            </Columns>    
                                
                            <SettingsContextMenu Enabled="true">                                
                                <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                            </SettingsContextMenu>   
                            <Settings ShowGroupPanel="True" VerticalScrollBarMode="Auto" VerticalScrollableHeight="350" ShowGroupedColumns="True" GridLines="Vertical" />
		                    <SettingsSearchPanel Visible="true" />
		                    <SettingsBehavior AutoExpandAllGroups="true" EnableRowHotTrack="True" ColumnResizeMode="NextColumn" EnableCustomizationWindow="true" />
		                    <SettingsPager Mode="ShowAllRecords" />
                            <Styles>
			                    <Row Cursor="pointer" />
		                    </Styles>                  
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                           
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>       






<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />     
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtEmployeeNotificationNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Name of Employee :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" ReadOnly="true" /> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Subject :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtNotificationTypeDesc" CssClass="form-control" ReadOnly="true" /> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Date Due :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDueDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDueDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtDueDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">HRAN No. :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboHRANNo" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>

             <div class="form-group" >
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtRemarks" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

         </div>
          <!-- Footer here -->
         <br />
    </fieldset>
</asp:Panel>


</asp:Content> 
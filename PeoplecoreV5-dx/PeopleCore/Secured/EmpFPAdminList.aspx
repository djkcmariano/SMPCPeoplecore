<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpFPAdminList.aspx.vb" Inherits="Secured_EmpFPAdminList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <h3>&nbsp;</h3>
                </div>
                <div>
                    &nbsp;                  
                </div> 
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataTextColumn FieldName="FPId" Caption="FP ID" />                                                        
                            <%--<dx:GridViewDataTextColumn FieldName="PrivilegeDesc" Caption="Privilege" />--%>
                            <%--<dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="FP Template" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>                                    
                                    <asp:Image runat="server" ID="img" ImageUrl="~/images/fingure.png" Height="21" Width="20" Visible='<%# Bind("HasFP") %>' />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>--%>  
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Enroll Type" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:Image runat="server" ID="img" ImageUrl="~/images/fingure.png" Height="21" Width="20" Visible='<%# Bind("HasFP") %>' />
                                    <asp:Label ID="Label1" runat="server" CssClass='<%# Bind("CardNo") %>' Font-Size="Medium" />
                                    <asp:Label ID="Label2" runat="server" CssClass='<%# Bind("Password") %>' Font-Size="Medium" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetails" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetails_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                                        
                            <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
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
                            <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" Visible="false" />                            
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" />
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
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                    </ul>                       
                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />                                                 
                </div> 
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="EmployeeFPMachineDetiNo" Width="100%">
                        <Columns>                            
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                            <dx:GridViewDataTextColumn FieldName="FPMachineDescription" Caption="Machine Description" />
                            <dx:GridViewDataTextColumn FieldName="PrivilegeDesc" Caption="Privilege" />                                                        
                            <dx:GridViewDataTextColumn FieldName="FPActionDesc" Caption="Action" />
                            <dx:GridViewDataTextColumn FieldName="EffectiveDate" Caption="Effective Date" />                            
                            <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Posted Date" />
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>
<asp:Button ID="btnShowRate" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlRate" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlPopupRate" TargetControlID="btnShowRate" />
<asp:Panel id="pnlPopupRate" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Employee Name :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtFullName" runat="server" ReadOnly="true" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Effective Date :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="required form-control required" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEffectiveDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEffectiveDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEffectiveDate" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Action :</label>
                <div class="col-md-7">
                    <%--<asp:DropDownList ID="cbofpactionno" runat="server" CssClass="form-control required" DataMember="EFPAction" />                    --%>
                    <asp:DropDownList ID="cbofpactionno" runat="server" CssClass="form-control required" AutoPostBack="true" OnTextChanged="cbofpactionno_TextChanged" />
                </div>
            </div>                       
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Privilege :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPrivilegeNo" runat="server" CssClass="form-control required" DataMember="EPrivilege" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Machine No. :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboFPMachineNo" runat="server" CssClass="form-control required" DataMember="EFPMachineL" />                    
                </div>
            </div>                                  
        </div>
        <br />
        </fieldset>
    </asp:Panel>

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose" PopupControlID="Panel1" TargetControlID="Button1" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
       <fieldset class="form" id="fsDetl">
        <!-- Header here -->
         <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsDetl lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Employee Name :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtFullName2" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Download from machine :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboFPMachineNo2" runat="server" CssClass="form-control required" DataMember="EFPMachineL" />                    
                </div>
            </div>                                 
            <br /><br />
        </div>        
        </fieldset>
</asp:Panel>


</asp:Content>
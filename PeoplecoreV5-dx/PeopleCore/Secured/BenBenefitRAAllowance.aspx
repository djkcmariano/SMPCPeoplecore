<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BenBenefitRAAllowance.aspx.vb" Inherits="Secured_BenBenefitRAAllowance" %>

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
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeNo">                                                                                   
                        <Columns>
                            
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataTextColumn FieldName="BirthDate" Caption="Birth Date" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="BirthAge" Caption="Birth Age" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />
                            <dx:GridViewDataComboBoxColumn FieldName="FacilityDesc" Caption="Business Unit" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="GroupDesc" Caption="Group/District" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" />
                            <dx:GridViewDataComboBoxColumn FieldName="SectionDesc" Caption="Section/Store" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="DivisionDesc" Caption="Division" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="ProjectDesc" Caption="Project" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Staus" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="GenderDesc" Caption="Gender" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="CivilStatDesc" Caption="Civil Status" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group"/>
                            <dx:GridViewDataComboBoxColumn FieldName="CostCenterDesc" Caption="Cost Center" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="UnitDesc" Caption="Unit" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="DateHired" Caption="Date Hired" />
                            <dx:GridViewDataTextColumn FieldName="DateHired" Caption="FDS" ToolTip="First Day in Service" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="RegularizedDate" Caption="Date of<br />Regularization" />
                            <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="TINNo" Caption="TIN" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="SSSNo" Caption="SSS" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="PHNo" Caption="PhilHealth" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="HDMFNo" Caption="HDMF" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="SeparatedDate" Caption="Separated Date " Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="BankTypeDesc" Caption="Bank Type" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="BankAccountNo" Caption="Bank Account No." Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="TaxExemptDesc" Caption="Tax Code" Visible="false" />                                    
                            <dx:GridViewDataTextColumn FieldName="FPId" Caption="FPId" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="LastLoyaltyDate" Caption="Last Loyalty<br/>Anniversary Date" Visible="false" />
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                              
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
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
                        <asp:Label ID="lblDetl" runat="server"></asp:Label>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="BenefitRAAllowanceNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" />
                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Maximum Amount" />
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
<br /><br />
   
<asp:Button ID="btnShowRate" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlRate" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgClose" PopupControlID="pnlPopupRate" TargetControlID="btnShowRate" />

<asp:Panel ID="pnlPopupRate" runat="server"  CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">    
        <div class="cf popupheader">            
            <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="lnkSave_Click"  />      
        </div>
        <!-- Body here -->
        <div  class="entryPopupDetl form-horizontal">          
            <div class="form-group">
                <label class="col-md-4 control-label">Trans No. :</label>
                <div class="col-md-7">
                    <asp:HiddenField runat="server" ID="hifBenefitRAAllowanceNo" />
                    <asp:TextBox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Maximum Amount :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control number" SkinID="txtdate"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Income Type :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboPayIncomeTypeNo"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
        </div>
        <br />
    </fieldset>
</asp:Panel>
</asp:Content>

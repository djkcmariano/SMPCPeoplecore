<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpRateList.aspx.vb" Inherits="Secured_EmpRateList" %>

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
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                            <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeRateClassDesc" Caption="Rate Class" />                                                        
                            <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Salary" PropertiesTextEdit-DisplayFormatString="{0:N2}" />                                                        
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
                            <dx:GridViewDataComboBoxColumn FieldName="HiredDate" Caption="Hired Date" Visible="false" />  
                            <dx:GridViewDataComboBoxColumn FieldName="RegularizedDate" Caption="Regularization Date" Visible="false" />  
                            <dx:GridViewDataComboBoxColumn FieldName="BirthDate" Caption="Birth Date" Visible="false" />  
                            <dx:GridViewDataComboBoxColumn FieldName="BirthAge" Caption="Age" Visible="false" />  
                            <dx:GridViewDataComboBoxColumn FieldName="GenderDesc" Caption="Gender" Visible="false" />   
                            <dx:GridViewDataComboBoxColumn FieldName="CivilStatDesc" Caption="Civil Status" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="Tenure" Caption="Tenure" Visible="false" />                              
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
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
                <label class="col-md-4 control-label">Employee No. :</label>
                <div class="col-md-7">
                    <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                    <asp:TextBox ID="txtEmployeeCode" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Name of employee :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtFullName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Current salary :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCurrentSalary" runat="server" CssClass="form-control number" SkinID="txtdate"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
    </fieldset>
</asp:Panel>
</asp:Content>
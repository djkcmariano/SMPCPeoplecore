<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="EmpPayInfoList.aspx.vb" Inherits="Secured_PayInfoList" %>


<%@ Register Src="../Include/wucFilterGeneric.ascx" TagName="wucFilter"   TagPrefix="uc1" %>

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
                            <li><asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" Visible="false" /></li>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeNo" OnCustomButtonCallback="lnkEdit_Click">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="BirthDate" Caption="Birth Date" Visible="false" />                  
                                <dx:GridViewDataTextColumn FieldName="BirthAge" Caption="Birth Age" Visible="false" />                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />                                                                           
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
                                <dx:GridViewDataTextColumn FieldName="DateHired" Caption="Date Hired" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="RegularizedDate" Caption="Date of Regularization" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="TINNo" Caption="TIN" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="SSSNo" Caption="SSS" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PHNo" Caption="PhilHealth" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="HDMFNo" Caption="HDMF" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="BankTypeDesc" Caption="Bank Type" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="BankAccountNo" Caption="Bank Account No." Visible="false" />                                
                                <dx:GridViewDataComboBoxColumn FieldName="TaxExemptDesc" Caption="Tax Code" Visible="false" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>


<asp:Button ID="btnUpload" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlUpload" runat="server" TargetControlID="btnUpload" PopupControlID="pnlUpload"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlUpload" runat="server" CssClass="entryPopup2">
        <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="fa fa-times cancel" ToolTip="Close" />&nbsp;   
         </div>
        <div  class="entryPopupDetl form-horizontal"> 
            <div class="form-group" >
                <label class="col-md-4 control-label">Filename :</label>
                <div class="col-md-7">
                    <asp:FileUpload ID="txtFile" runat="server" Width="350" />
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-2 control-label"></label>
                <div class="col-md-6">
                    <div class="btn-group">
                        <asp:Button runat="server" ID="btnUploadx" CssClass="btn btn-default submit fsMain" Text="Upload" OnClick="lnkUploadSave_Click" />
                    </div> 
                    
                </div>
            </div>
        </div>
        
    </fieldset>
</asp:Panel>  

</asp:Content>

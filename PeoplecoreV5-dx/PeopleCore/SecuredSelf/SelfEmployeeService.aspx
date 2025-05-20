<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfEmployeeService.aspx.vb" Inherits="Secured_EmpUpdExamList" %>

<asp:Content id="Content1" contentplaceholderid="cphBody" runat="server">
<uc:TabSelf runat="server" ID="TabSelf">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />                
            </center>            
            <asp:Label runat="server" ID="lbl" /> 
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
                                 
                                </ul>                                                                                                                                                     
                                </ContentTemplate>
                                
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeServiceNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="View" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeServiceNo") %>' OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />                                            
                                            <dx:GridViewDataTextColumn FieldName="DateFrom" Caption="Date From" />
                                            <dx:GridViewDataTextColumn FieldName="DateTo" Caption="Date To" />
                                            <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                                            <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Employment Status" />
                                            <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />
                                            <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Salary" />                                                                                                                                   
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                        </Columns>                            
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                                           
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
                </div>
            </div>                         
        </Content>
    </uc:TabSelf>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Date :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtDateFrom" runat="server" CssClass="form-control" placeholder="From" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDateFrom" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtDateFrom" />
                    <asp:CompareValidator runat="server" ID="CompareValidator4" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtDateFrom" />
                </div>
                <div class="col-md-3">
                    <asp:Textbox ID="txtDateTo" runat="server" CssClass="form-control" placeholder="To" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDateTo" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtDateTo" />
                    <asp:CompareValidator runat="server" ID="CompareValidator5" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtDateTo" />
                </div>                            
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">HRAN Type :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtHRANTypeDesc" CssClass="form-control"/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Plantilla No. :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtPlantillaCode" CssClass="form-control"/>
                </div>
            </div>  
            <div class="form-group">
                    <label class="col-md-4 control-label has-required">Position :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPositionDesc" CssClass="form-control required"/>
                    </div>
            </div>  
            <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">Separation Date :</label>
                    <div class="col-md-3">
                    <asp:Textbox ID="txtSeparationDate" runat="server" CssClass="form-control" ReadOnly="false" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtSeparationDate" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtSeparationDate" />
                    <asp:CompareValidator runat="server" ID="CompareValidator3" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtSeparationDate" />
             </div>
            </div>            
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Facility :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtFacilityDesc" CssClass="form-control"/>
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Unit :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtUnitDesc" CssClass="form-control"/>
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Department :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtDepartmentDesc" CssClass="form-control"/>
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Group :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtGroupDesc" CssClass="form-control"/>
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Division :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtDivisionDesc" CssClass="form-control"/>
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Section :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtSectionDesc" CssClass="form-control"/>
                    </div>
            </div>            
            <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">Station :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtStationDesc" CssClass="form-control"/>
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Employee Status :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtEmployeeStatDesc" CssClass="form-control"/>
                    </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Salary :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtCurrentSalary" CssClass="form-control"/>
                    </div>
            </div>
            <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">LWOP :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtLWOP" CssClass="form-control" SkinID="txtdate"/>
                    </div>
            </div>  
            <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtReason" CssClass="form-control"/>
                    </div>
            </div>  
            <br />                        
        </fieldset>
    </asp:Panel>  
</asp:Content>

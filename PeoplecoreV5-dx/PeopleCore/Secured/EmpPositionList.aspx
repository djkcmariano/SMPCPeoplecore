<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpPositionList.aspx.vb" Inherits="Secured_EmpPositionList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" runat="server">
<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">                    
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control"  runat="server" />            
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                           
                            <li><asp:LinkButton runat="server" ID="lnkArchive" OnClick="lnkArchive_Click" Text="Archive" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                            <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PositionNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                            <dx:GridViewDataTextColumn FieldName="PositionCode" Caption="Code" />
                            <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                            <dx:GridViewDataComboBoxColumn FieldName="PositionLevelDesc" Caption="Position Level" Visible="false" />
                            <dx:GridViewDataCheckColumn FieldName="IsApplicant" Caption="For Applicant Service Core" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" Visible="false" />
                            <dx:GridViewDataComboBoxColumn FieldName="JobGradeDesc" Caption="Job Grade" Visible="false" />    
                            <%--<dx:GridViewBandColumn Caption="Salary" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="MinSalary" Caption="Minimum" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="MidSalary" Caption="Median" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                    <dx:GridViewDataTextColumn FieldName="MaxSalary" Caption="Maximum" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                </Columns>
                            </dx:GridViewBandColumn>--%>   
                            <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" /> 
                            <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" /> 
                            <dx:GridViewDataTextColumn FieldName="ModifiedBy" Caption="Last Modified By" Visible="false"/> 
                            <dx:GridViewDataTextColumn FieldName="ModifiedDate" Caption="Last Modified Date" Visible="false"/> 
                            <dx:GridViewDataComboBoxColumn FieldName="PaylocDesc" Caption="Company" />  
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
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
                <div class="col-md-7">
                    <h4 class="panel-title">Reference No.: <asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
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
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" Width="100%" KeyFieldName="PositionAllowanceNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="CodeDeti" Caption="Detail No." />
                            <dx:GridViewDataComboBoxColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" />
                            <dx:GridViewDataComboBoxColumn FieldName="PayScheduleDesc" Caption="Schedule Type" />
                            <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPositionNo" runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber"
                        ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPositionCode" runat="server" CssClass="required form-control" />
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPositionDesc" runat="server" CssClass="required form-control" />
                </div>
            </div> 
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Position Level :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPositionLevelNo" runat="server" DataMember = "EPositionLevel" CssClass="form-control" />
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Employee Class :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboEmployeeClassNo" runat="server" DataMember = "EEmployeeClass" CssClass="form-control" />
                </div>
            </div> 
 
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Job Grade :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboJobGradeNo" runat="server" DataMember="EJobGrade" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group" >
                <label class="col-md-4 control-label has-space">Job Level :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboSalaryGradeNo" runat="server" DataMember="ESalaryGrade" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Step :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboStepNo" runat="server" DataMember="EStep" CssClass="form-control" />
                </div>
            </div>                                                             

            <div class="form-group" style="display:none" >
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Minimum</label><br />   
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Median</label><br />    
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <label class="control-label">Maximum</label><br />   
                    </center>
                </div>
            </div>

            <div class="form-group" style="display:none" >
                <label class="col-md-4 control-label has-space">Salary :</label>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtMinSalary" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtMinSalary" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender25" runat="server" />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtMidSalary" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtMidSalary" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender26" runat="server" />
                    </center>
                </div>
                <div class="col-md-2">
                    <center>
                        <asp:TextBox ID="txtMaxSalary" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtMaxSalary" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender27" runat="server" />
                    </center>
                </div>
            </div>


            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">
                Company Name :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass="number form-control" >
                    </asp:Dropdownlist>
                </div>
            </div>                        

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsKCP" Text="&nbsp;Tick if Key and Critical position" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Effective Date :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEffectiveDate" runat="server" CssClass="form-control" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtEffectiveDate" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtEffectiveDate" Display="Dynamic" />
                </div>
            </div>
           <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                </div>
           </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                </div>
            </div>

            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>

<asp:Button ID="btnShowD" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShowDetl" runat="server" TargetControlID="btnShowD" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClosed" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsDetl">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsDetl btnSaveDetl" OnClick="btnSaveDetl_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Reference No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPositionAllowanceNo" runat="server" CssClass="form-control" 
                        ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDeti" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>
             
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Income Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayIncomeTypeNo" runat="server" DataMember = "EPayIncomeType" CssClass="required form-control"  />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Schedule Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayScheduleNo" runat="server" DataMember = "EPaySchedule" CssClass="required form-control"  />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="required form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtAmount" />
                </div>
            </div>
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>
</asp:Content>

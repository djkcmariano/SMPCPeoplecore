<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayBonusEntitleList.aspx.vb" Inherits="Secured_PayBonusEntitleList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">

    function DisableIsUpload(chk) {

        if (chk.checked) {

            document.getElementById("ctl00_cphBody_Tab_cboEmployeeStatNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeStatNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeClassNo").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeClassNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtEligibleDate").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtEligibleDate").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtMinServiceYear").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtMinServiceYear").value = "";

        } else {

            document.getElementById("ctl00_cphBody_Tab_cboEmployeeStatNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeStatNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeClassNo").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_cboEmployeeClassNo").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtEligibleDate").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtEligibleDate").value = "";
            document.getElementById("ctl00_cphBody_Tab_txtMinServiceYear").disabled = false;
            document.getElementById("ctl00_cphBody_Tab_txtMinServiceYear").value = "";

        }
    }

    function disableenable(chk) {
        var fval = chk.value;
        if (fval == '7' || fval == '8' || fval == '9') {
            document.getElementById("ctl00_cphBody_Tab_txtFixedAmount").disabled = false;

        } else {

            document.getElementById("ctl00_cphBody_Tab_txtFixedAmount").disabled = true;
        };
    };
    function disableenable_behind(fval) {

        if (fval == '7' || fval == '8' || fval == '9') {
            document.getElementById("ctl00_cphBody_Tab_txtFixedAmount").disabled = false;
        } else {

            document.getElementById("ctl00_cphBody_Tab_txtFixedAmount").disabled = true;

        };
    };
</script>

<uc:Tab runat="server" ID="Tab">
    <Header>        
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>      
    </Header>
     <Content>
        <br />

<div class="row">
    <div class="panel panel-default">
        <div class="panel-heading">                                
            <div class="col-md-10">                                           
            
            </div>               
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                            <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
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
            <div class="row">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayBonusEntitledNo" Width="100%">
                        <Columns>
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                            <dx:GridViewDataTextColumn FieldName="BonusTypeDesc" Caption="Bonus Type" />
                            <dx:GridViewDataTextColumn FieldName="CStartDate" Caption="From" />
                            <dx:GridViewDataTextColumn FieldName="CEndDate" Caption="To" />
                            <dx:GridViewDataTextColumn FieldName="BonusBasisDesc" Caption="Bonus Basis" />
                            <dx:GridViewDataTextColumn FieldName="DateBaseDesc" Caption="Base Date" />
                            <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="% Factor" />
                            <dx:GridViewDataTextColumn FieldName="NoofmonthsAssume" Caption="Month(s)<br/>Assume" Visible="false" />
                            <dx:GridViewDataTextColumn FieldName="PEPeriodDesc" Caption="Period Type"  Visible="false"/>
                            <dx:GridViewDataTextColumn FieldName="EligibleDate" Caption="Eligibility<br/>Date" />
                            <dx:GridViewDataTextColumn FieldName="MinServiceYear" Caption="Min. YS" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Status" />
                            <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Classification" />

                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />                                        
                                </DataItemTemplate>
                            </dx:GridViewDataColumn> 
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Factor Rate" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkFactor" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkFactor_Click" />                                        
                                </DataItemTemplate>
                            </dx:GridViewDataColumn> 
                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Additional Income" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <DataItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkIncome" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkIncome_Click" />                                        
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                             
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                        </Columns>
                        
                        <SettingsContextMenu Enabled="true">                                
                            <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                        </SettingsContextMenu>                                                                                            
                        <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />   
                        <SettingsSearchPanel Visible="false" />  
                        <Settings ShowFooter="true" />       
                                   
                    </dx:ASPxGridView>    
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                
                </div>
            </div>                                                           
        </div>                   
    </div>
</div>

<div class="page-content-wrap" id="divDetl" runat="server">         
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="panel-title" >
                        Transaction No.:&nbsp; <asp:Label runat="server" ID="lblDetl" />
                    </div>                   
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" /></li> 
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
                            <div class="table-responsive">                    
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayBonusEntitledDetiNo" Width="100%" >                                                                                   
                                    <Columns>                           
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />                            
                                        <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />                                                        
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                        <dx:GridViewDataTextColumn FieldName="HiredDate" Caption="Date Hired" />  
                                        <dx:GridViewDataTextColumn FieldName="RegularizedDate" Caption="Date Regularized" />  
                                        <dx:GridViewDataTextColumn FieldName="Tenure" Caption="Years of Service" />
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />                                                                          
                                    </Columns>
                                    <SettingsPager Mode="ShowPager" /> 
                                    <SettingsContextMenu Enabled="true">                                
                                        <RowMenuItemVisibility CollapseDetailRow="false" CollapseRow="false" DeleteRow="false" EditRow="false" ExpandDetailRow="false" ExpandRow="false" NewRow="false" Refresh="false" /> 
                                    </SettingsContextMenu>                                                                                            
                                    <SettingsBehavior EnableCustomizationWindow="true" />  
                                    <SettingsSearchPanel Visible="true" />                                   
                                </dx:ASPxGridView>
                                            
                                <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />  
                            </div>                            
                        </div>
                </div>
            </div>
        </div>
    </div> 

<div class="page-content-wrap" id="div1" runat="server">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title" >
                    Transaction No.:&nbsp; <asp:Label runat="server" ID="Label1" />
                </div>                   
                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="lnkAddFactor" OnClick="lnkAddfactor_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                            <li><asp:LinkButton runat="server" ID="lnkDeleteFactor" OnClick="lnkDeleteFactor_Click" Text="Delete" CssClass="control-primary" /></li>
                            <li><asp:LinkButton runat="server" ID="LinkButton4" OnClick="lnkExportFactor_Click" Text="Export" CssClass="control-primary" /></li>
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDeleteFactor" TargetControlID="lnkDeleteFactor" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkExportDetl" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">

                <div class="row">
                        <div class="table-responsive">                    
                            <dx:ASPxGridView ID="grdFactor" ClientInstanceName="grdFactor" runat="server" KeyFieldName="PayBonusEntitledFactorNo" Width="100%">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditFactor_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="SFrom" Caption="From (LS in month)" />
                                    <dx:GridViewDataTextColumn FieldName="STo" Caption="To (LS in month)" />
                                    <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="Factor Rate" />

                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                                </Columns>
                        
                                <Settings ShowFooter="true" />       
                                   
                            </dx:ASPxGridView>    
                            <dx:ASPxGridViewExporter ID="grdExportFactor" runat="server" GridViewID="grdFactor" />   
                        </div>                            
                    </div>
            </div>
        </div>
    </div>
</div> 
<div class="page-content-wrap" id="div2" runat="server">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title" >
                    Transaction No.:&nbsp; <asp:Label runat="server" ID="Label2" />
                </div>                   
                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                    <ContentTemplate>
                        <ul class="panel-controls">
                            <li><asp:LinkButton runat="server" ID="LinkButton5" OnClick="lnkAddIncome_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                            <li><asp:LinkButton runat="server" ID="LinkButton6" OnClick="lnkDeleteIncome_Click" Text="Delete" CssClass="control-primary" /></li>
                            
                        </ul>
                        <uc:ConfirmBox runat="server" ID="cfbDeleteIncome" TargetControlID="LinkButton6" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ContentTemplate>
                    <Triggers>
                        
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel-body">

                <div class="row">
                        <div class="table-responsive">                    
                            <dx:ASPxGridView ID="grdIncometype" ClientInstanceName="grdFactor" runat="server" KeyFieldName="PayBonusEntitledAllowanceNo" Width="100%">
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditIncome" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditIncome_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Income Type" />
                                    

                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                                </Columns>
                        
                                <Settings ShowFooter="true" />       
                                   
                            </dx:ASPxGridView>    
                           
                        </div>                            
                    </div>
            </div>
        </div>
    </div>
</div> 
<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShow" PopupControlID="Panel2" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />

<asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />   
        </div>         
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayBonusEntitledNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />                        
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Bonus Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBonusTypeNo" DataMember="EBonusType" runat="server" CssClass="form-control required" />                        
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required ">Cut Off Date :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtcStartDate" runat="server" CssClass="required form-control" Placeholder="From"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4xx" runat="server" TargetControlID="txtcStartDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy" />                                                 
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4xx" runat="server" TargetControlID="txtcStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator1xx" runat="server" ControlToValidate="txtcStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender6" TargetControlID="RangeValidator1xx" />

                </div>

                <div class="col-md-3">
                    <asp:Textbox ID="txtcEndDate" runat="server" CssClass="required form-control" Placeholder="To"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtcEndDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />                                               
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtcEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtcEndDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator3" />
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Bonus Basis :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBonusBasisNo" DataMember="EBonusbasis"   runat="server" CssClass="form-control required" onchange="disableenable(this);"/>                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Fixed Amount :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtFixedAmount" runat="server" CssClass="form-control" Enabled="false" /> 
                                     
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Base Date :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboDatebaseNo" DataMember="EDateBase" runat="server" CssClass="form-control required" />                        
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Percent factor :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPercentFactor" runat="server" CssClass="required form-control" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtPercentFactor" />                   
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">No. of months to assume :</label>
                <div class="col-md-3">  
                    <asp:TextBox ID="txtnoofmonthsassume" CssClass="form-control" runat="server" />      
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtnoofmonthsassume" />                         
                </div>
                
            </div>

            <div class="form-group" runat="server" id="divPEPeriod">
                <label class="col-md-4 control-label has-space">Performance Period Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPEPeriodNo" runat="server" CssClass="form-control" />                        
                </div>
            </div>
             
            <div class="form-group">  
                <h5 class="col-md-8">
                    <label class="control-label">CRITERIA&nbsp;&nbsp;OF&nbsp;&nbsp;ENTITLEMENT</label>
                </h5>
            </div>

            <div class="form-group" runat="server" id="divUpload" >
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-8">
                    <asp:CheckBox ID="txtIsUpload" runat="server" onclick="DisableIsUpload(this);" Text="&nbsp; Please check here for uploading or manual encoding of eligible employee" />
                </div>
            </div>

            <div class="form-group" runat="server" id="divEligibility">
                <label class="col-md-4 control-label has-space">Eligibility Date (Base Date) :</label>
                <div class="col-md-3">  
                    <asp:Textbox ID="txtEligibleDate" runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEligibleDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />                                               
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEligibleDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtEligibleDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator1" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Minimum years of service :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtMinServiceYear"  runat="server" CssClass="form-control" />      
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMinServiceYear" />               
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Employee Status :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Employee Classification :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control" />                        
                </div>
            </div>
            <div class="form-group" style="visibility:hidden;position:absolute;">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsApplytoAll" runat="server" Text="&nbsp;Apply to all employee" />
                </div>
            </div>
            
            

            <br />
        </div>                
    </fieldset>
</asp:Panel>

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
                    <asp:Textbox ID="txtPayBonusEntitledDetiNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDetl" ReadOnly="true"  runat="server" CssClass="form-control"  Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" onblur="ResetEmployeeNo()" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" CompletionSetCount="0" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                         }

                         function ResetEmployeeNo() {
                             if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                 document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                             }
                         } 
                     </script>
                    
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Percent Tile :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtPercentFactorDeti" runat="server" CssClass="form-control" SkinID="txtdate"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtPercentFactorDeti" />
                </div>
            </div>

        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>



<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="lnkClose2" PopupControlID="Panel3" TargetControlID="Button1" />
<asp:Panel id="Panel3" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsUpload">      
         <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                <ContentTemplate>
                    <asp:Linkbutton runat="server" ID="lnkClose2" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    <asp:LinkButton runat="server" ID="lnkSave2" CssClass="fa fa-floppy-o submit fsUpload lnkSave2" OnClick="lnkSave2_Click"  />   
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkSave2" />
                </Triggers>
            </asp:UpdatePanel>            
         </div>         
         <div  class="entryPopupDetl2 form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label has-space">File Format :</label>
                <div class="col-md-7">
                    <code>File must be .csv(comma delimited) with following column : <br />Employee No., Employee Name</code>                     
                </div>     
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Filename :</label>
                <div class="col-md-7">
                    <asp:FileUpload runat="server" ID="fuFilename" Width="100%" CssClass="required" />                   
                </div>
            </div>          
            <br /> 
        </div>         
        <div class="cf popupfooter">
        </div> 
    </fieldset>
</asp:Panel>   


<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button2" PopupControlID="Panel1" CancelControlID="Linkbutton1" BackgroundCssClass="modalBackground" />

<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="Fieldset1">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkSaveFactor" CssClass="fa fa-floppy-o submit Fieldset1 lnkSave" OnClick="lnkSaveFactor_Click"  />   
        </div>         
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayBonusEntitledFactorNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="Textbox1" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />                        
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">From (LS in month):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSFrom" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">To (LS in month):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSTo" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Percent factor :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>


            <br />
        </div>                
    </fieldset>
</asp:Panel>


<asp:Button ID="Button3" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="Button3" PopupControlID="Pnlincome" CancelControlID="Linkbutton2" BackgroundCssClass="modalBackground" />

<asp:Panel id="Pnlincome" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="Fieldset2">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="Linkbutton2" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="LinkButton3" CssClass="fa fa-floppy-o submit Fieldset2 lnkSave" OnClick="lnkSaveIncome_Click"  />   
        </div>         
        <div  class="entryPopupDetl2 form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayBonusEntitledAllowanceNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="Textbox4" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />                        
                </div>
            </div>
            
            

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Income Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPayIncomeTypeNo" DataMember="EPayIncomeType" runat="server" CssClass="form-control required" />    
    
                </div>
            </div>


            <br />
        </div>                
    </fieldset>
</asp:Panel>
</asp:Content> 
<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayTemplate_Bonus.aspx.vb" Inherits="Secured_PayTemplate_Bonus" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
    function disableenable(chk) {
        var fval = chk.value;
        if (fval == '7' || fval == '8' || fval == '9' || fval == '10') {
            document.getElementById("ctl00_cphBody_txtFixedAmount").disabled = false;
            
        } else{

            document.getElementById("ctl00_cphBody_txtFixedAmount").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtFixedAmount").value = '';
        };   
    };
    function disableenable_behind(fval) {

        if (fval == '7' || fval == '8' || fval == '9' || fval == '10') {
            document.getElementById("ctl00_cphBody_txtFixedAmount").disabled = false;
        } else{

            document.getElementById("ctl00_cphBody_txtFixedAmount").disabled = true;
            document.getElementById("ctl00_cphBody_Tab_txtFixedAmount").value = '';
        };
    };
    

</script>
<uc:Tab runat="server" menustyle="TabRef" ID="Tab">
    <Content>
        <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                                <h4 class="panel-title">Bonus Components</h4>
                                &nbsp;
                            </div>
                            <div>                                                
                                <ul class="panel-controls">                                                                                
                                    <li><asp:LinkButton runat="server" ID="lnkAddB" OnClick="lnkAddB_Click" Text="Add" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteB" OnClick="lnkDeleteB_Click" Text="Delete" CssClass="control-primary" /></li>                                                                      
                                    <uc:ConfirmBox runat="server" ID="cfbDeleteb" TargetControlID="lnkDeleteB" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ul>                                                                                                                                                     
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <dx:ASPxGridView ID="grdPay" ClientInstanceName="grdPay" runat="server" KeyFieldName="PayTemplateNo" Width="100%">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditb_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                                    
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                            <dx:GridViewDataTextColumn FieldName="PayScheduleDesc" Caption="Payroll Schedule" />
                                            <dx:GridViewBandColumn Caption="Applicable Deduction" HeaderStyle-HorizontalAlign="Center">
                                                <Columns> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductTax" Caption="Tax"/> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductSSS" Caption="SSS" Visible="false"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductPH" Caption="PH" Visible="false"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductHDMF" Caption="HDMF" Visible="false"/>
                                                 </Columns>
                                            </dx:GridViewBandColumn> 
                                            <dx:GridViewBandColumn Caption="Payroll Components" HeaderStyle-HorizontalAlign="Center">
                                                <Columns> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsAttendanceBase" Caption="DTR Base" Visible="false"/> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsIncludeForw" Caption="Forwarded"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsIncludeMass" Caption="Template"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsIncludeOther" Caption="Other"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsIncludeLoan" Caption="Loan"/>
                                                 </Columns>
                                            </dx:GridViewBandColumn>                                                        
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />

                                        </Columns>                            
                                    </dx:ASPxGridView>                                
                                </div>
                            </div>                                                           
                        </div>                   
                    </div>
                </div>
            </div> 
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-4">
                            <h4 class="panel-title">13th Month/Bonus Entitlement</h4>
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                                                                
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                                      
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayTemplateBonusNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                        <dx:GridViewDataTextColumn FieldName="BonusTypeDesc" Caption="Bonus Type" />
                                        <dx:GridViewDataTextColumn FieldName="BonusBasisDesc" Caption="Bonus Basis" />
                                        <dx:GridViewDataTextColumn FieldName="FixedAmount" Caption="Fixed Amount" />
                                        <dx:GridViewDataTextColumn FieldName="DateBaseDesc" Caption="Base Date" />
                                        <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="% Factor" />
                                        <dx:GridViewDataTextColumn FieldName="NoofmonthsAssume" Caption="Month(s)<br/>Assume" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="PEPeriodDesc" Caption="Period Type" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="EligibleDate" Caption="Eligibility<br/>Date" />
                                        <dx:GridViewDataTextColumn FieldName="MinServiceYear" Caption="Min. Mos" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Status" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Classification" Visible="false" />
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Detail of Factor Rate" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Additional Allowance" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDetailIncome" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetailIncome_Click" />
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
        </div> 
        <br />
        <div class="page-content-wrap" runat="server" id="divfactor" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-4">
                            <h4 class="panel-title">13th Month/Bonus Factor Rate</h4>
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                                                                
                                <li><asp:LinkButton runat="server" ID="LinkButton2" OnClick="lnkAddfactor_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="LinkButton3" OnClick="lnkDeletefactor_Click" Text="Delete" CssClass="control-primary" /></li>                                                                      
                                <uc:ConfirmBox runat="server" ID="cfbDeletefactor" TargetControlID="LinkButton3" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdFactor" ClientInstanceName="grdFactor" runat="server" KeyFieldName="PayTemplateBonusDetiNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditfactor_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="SFrom" Caption="From (LS in Days)" />
                                        <dx:GridViewDataTextColumn FieldName="STo" Caption="To (LS in Days)" />
                                        <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="Factor Rate" />

                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                                    </Columns>
                        
                                    <Settings ShowFooter="true" />       
                                   
                                </dx:ASPxGridView>    
                                <dx:ASPxGridViewExporter ID="grdFactorExp" runat="server" GridViewID="grdFactor" />                                  
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div> 
        <br />
        <div class="page-content-wrap" runat="server" id="divIncome" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-4">
                            <h4 class="panel-title">13th Month/Bonus - Additional Allowance</h4>
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                                                                
                                <li><asp:LinkButton runat="server" ID="LinkButton8" OnClick="lnkAddIncome_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="LinkButton9" OnClick="lnkDeleteIncome_Click" Text="Delete" CssClass="control-primary" /></li>                                                                      
                                <uc:ConfirmBox runat="server" ID="cfbDeleteincome" TargetControlID="LinkButton9" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdIncometype" ClientInstanceName="grdIncometype" runat="server" KeyFieldName="PayTemplateBonusAllowanceNo" Width="100%">
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
        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-4">
                            <h4 class="panel-title">AWOP Policy</h4>
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                                                                
                                <li><asp:LinkButton runat="server" ID="lnkAddD" OnClick="lnkAddD_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDeleteD" OnClick="lnkDeleteD_Click" Text="Delete" CssClass="control-primary" /></li>                                                                      
                                <uc:ConfirmBox runat="server" ID="cfbDeleted" TargetControlID="lnkDeleteD" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdDedu" ClientInstanceName="grdDedu" runat="server" KeyFieldName="PayTemplateLWOPNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditd_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                        <dx:GridViewBandColumn Caption="DTR Cut-off" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="StartDateX" Caption="Date From" />
                                                <dx:GridViewDataTextColumn FieldName="EndDateX" Caption="Date To" />
                                            </Columns>
                                        </dx:GridViewBandColumn> 
<%--                                        <dx:GridViewBandColumn Caption="Percent Factor" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="PercentLate" Caption="Late" />
                                                <dx:GridViewDataTextColumn FieldName="PercentUnder" Caption="Under" />
                                                <dx:GridViewDataTextColumn FieldName="PercentAbsent" Caption="Absent" />
                                            </Columns>
                                        </dx:GridViewBandColumn> --%>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Detail of Factor Rate" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDetailawop" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetailawop_Click" />                                        
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
                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdDedu" />                                  
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div> 

        <br />
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-4">
                            <h4 class="panel-title">AWOP Policy Detail</h4>
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                                                                
                                <li><asp:LinkButton runat="server" ID="lnkaddawop" OnClick="lnkAddawop_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                        <li><asp:LinkButton runat="server" ID="lnkdeleteawop" OnClick="lnkDeleteawop_Click" Text="Delete" CssClass="control-primary" /></li>                                                              
                                <uc:ConfirmBox runat="server" ID="cfdeletefactor" TargetControlID="lnkdeleteawop" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdAwop" ClientInstanceName="grdAwop" runat="server" KeyFieldName="PayTemplateLWOPDetiNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditAWOP_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="AWOPFrom" Caption="From (AWOP Count)" />
                                        <dx:GridViewDataTextColumn FieldName="AWOPTo" Caption="To (AWOP Count)" />
                                        <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="Factor Rate" />
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
        
    </Content>
</uc:Tab> 
    
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
                    <asp:Textbox ID="txtPayTemplateBonusNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />                        
                </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label has-required">Payroll Group :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayClassNo" runat="server" CssClass="form-control required" DataMember="EPayClass" />                        
                   </div>
                </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Bonus Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBonusTypeNo" DataMember="EBonusType" runat="server" CssClass="form-control required" />                        
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
                    <asp:TextBox ID="txtFixedAmount" runat="server" CssClass="form-control number" Enabled="false" /> 
                                     
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
                    <asp:TextBox ID="txtPercentFactor" runat="server" CssClass="required form-control number required" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbe1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtPercentFactor" />  
                    <asp:RangeValidator runat="server" ID="rv1" ControlToValidate="txtPercentFactor" MinimumValue="" MaximumValue="100" Type="Double" ErrorMessage="Value is out of Range."></asp:RangeValidator>  
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">No. of months to assume :</label>
                <div class="col-md-3">  
                    <asp:TextBox ID="txtnoofmonthsassume" CssClass="form-control" runat="server" />      
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtnoofmonthsassume" />          
                    <asp:RangeValidator runat="server" ID="rv2" ControlToValidate="txtnoofmonthsassume" MinimumValue="" MaximumValue="100" Type="Double" ErrorMessage="Value is out of Range."></asp:RangeValidator>                
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

            <div class="form-group" runat="server" id="divUpload" visible="false" >
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
                <label class="col-md-4 control-label has-space">Minimum months of service :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtMinServiceYear"  runat="server" CssClass="form-control" />      
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMinServiceYear" />   
                    <asp:RangeValidator runat="server" ID="rv3" ControlToValidate="txtMinServiceYear" MinimumValue="" MaximumValue="100" Type="Double" ErrorMessage="Value is out of Range."></asp:RangeValidator>            
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


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="Fieldset1">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveb" CssClass="fa fa-floppy-o submit fsMain lnkSaveb" OnClick="lnkSaveb_Click"  />      
         </div>
         <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">              
                <div class="form-group">
                    <label class="col-md-4 control-label">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPayTemplateNo"  runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label">Payroll Group :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayClassNoL" runat="server" CssClass="form-control" DataMember="EPayClass" />                        
                   </div>
                </div>
                <div class="form-group" runat="server" visible="false">
                    <label class="col-md-4 control-label">Payroll Source :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPaySourceNo" runat="server" CssClass="form-control" DataMember="EPaySource" />                        
                   </div>
                </div>
                
                <div class="form-group" runat="server" visible="false">
                    <label class="col-md-4 control-label  has-required">Payroll Schedule :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayScheduleNo" DataMember="EPaySchedule" runat="server" CssClass="form-control"></asp:Dropdownlist>
                    </div>
                </div> 
               
             
                <div class="form-group">
                    <label class="col-md-4 control-label">
                        <h5><b>Applicable Deduction</b></h5>
                    </label>
                </div>            
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsDeductTax" runat="server" Text="&nbsp;Tax" />                    
                    </div>
                    <div class="col-md-3">
                        <asp:Checkbox ID="txtIsDeductSSS" runat="server" Text="&nbsp;SSS" Visible="false" />
                    </div>
                </div>
                <div class="form-group" runat="server" visible="false">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">
                        <asp:Checkbox ID="txtIsDeductPH" runat="server" Text="&nbsp;PhilHealth" Visible="false" />
                    </div>
                    <div class="col-md-3">
                        <asp:Checkbox ID="txtIsDeductHDMF" runat="server" Text="&nbsp;Pag-ibig" Visible="false" />
                    </div>                                 
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                        <h5><b>Payroll Components</b></h5>
                    </label>
                </div>                      
                <div class="form-group" runat="server" visible="false">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsAttendanceBase" runat="server" Text="&nbsp;DTR Base" Visible="false" />
                    </div>
                    <div class="col-md-3" style="visibility:hidden; position:absolute;">                    
                        <asp:Checkbox ID="txtIsAttendanceNonBasic" runat="server" Text="&nbsp;DTR Base (Non-basic)" />
                    </div>
                </div>             
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsIncludeMass" runat="server" Text="&nbsp;Template" />
                    </div>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsIncludeForw" runat="server" Text="&nbsp;Forwarded" />
                    </div>
                </div>            
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsIncludeLoan" runat="server" Text="&nbsp;Loan" />
                    </div>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsIncludeOther" runat="server" Text="&nbsp;Other" />
                    </div>
                </div>
                <div class="form-group" style="visibility:hidden; position:absolute;">
                    <label class="col-md-4 control-label">
                        <h5><b>OTHER COMPONENTS</b></h5>
                    </label>
                </div>
                <div class="form-group" style="visibility:hidden; position:absolute;">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-2">                    
                        <asp:Checkbox ID="txtIsRATA" runat="server" Text="&nbsp;RATA" />
                    </div>
                    <div class="col-md-2">                    
                        <asp:Checkbox ID="txtIsRice" runat="server" Text="&nbsp;Rice" />
                    </div>
                    <div class="col-md-2">                    
                        <asp:Checkbox ID="txtIsMedical" runat="server" Text="&nbsp;Medical" />
                    </div>
                </div>
                <br />          
                <br />
                
              </div>     
          <!-- Footer here -->
         <br />   
        
    </fieldset>

</asp:Panel>

<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="Linkbutton1" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="Fieldset2">
        <!-- Header here -->
            <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaved" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSaved_Click"  />   
            </div>
            <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayTemplateLWOPNo" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-6">                                        
                    <asp:Textbox ID="txtCoded" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" />                        
                </div>
            </div>
            <div class="form-group">
                    <label class="col-md-4 control-label">Payroll Group :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPayClassNoD" runat="server" CssClass="form-control" DataMember="EPayClass" />                        
                   </div>
                </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Date :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtStartDateX" runat="server" CssClass="form-control" placeholder="From" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDateX" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDateX" />
                    <asp:CompareValidator runat="server" ID="CompareValidator4" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDateX" />
                </div>
                <div class="col-md-3">
                    <asp:Textbox ID="txtEndDateX" runat="server" CssClass="form-control" placeholder="To" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDateX" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDateX" />
                    <asp:CompareValidator runat="server" ID="CompareValidator5" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDateX" />
                </div>
            </div>
            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label">
                    <h5><b>Percent Factor</b></h5>
                </label>
            </div>                        
            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label">Late :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtLate" runat="server" CssClass="number form-control" />                                                          
                </div>
            </div>
            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label">Undertime :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtUnder" runat="server" CssClass="number form-control" />                                                                              
                </div>
            </div>
            <div class="form-group" style=" display:none;">
                <label class="col-md-4 control-label">Absent :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtAbsent" runat="server" CssClass="number form-control" />                                                               
                </div>
            </div>

            <br />
            </div>
            <!-- Footer here -->
         
            </fieldset>
</asp:Panel>

<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel3" CancelControlID="Linkbutton4" BackgroundCssClass="modalBackground" />

<asp:Panel id="Panel3" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="Fieldset3">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="Linkbutton4" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="LinkButton5" CssClass="fa fa-floppy-o submit Fieldset3 LinkButton5" OnClick="lnkSaveFactor_Click"  />   
        </div>         
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayTemplateBonusDetiNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="Textbox1" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />                        
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">From (LS in Days):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSFrom" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">To (LS in Days):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSTo" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Percent factor :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtxPercentFactor" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>


            <br />
        </div>                
    </fieldset>
</asp:Panel>

<asp:Button ID="Button3" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button3" PopupControlID="Panel4" CancelControlID="Linkbutton6" BackgroundCssClass="modalBackground" />

<asp:Panel id="Panel4" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="Fieldset4">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="Linkbutton6" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="LinkButton7" CssClass="fa fa-floppy-o submit Fieldset4 LinkButton7" OnClick="lnkSaveAwop_Click"  />   
        </div>         
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayTemplateLWOPDetiNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="Textbox4" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />                        
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">From (AWOP Days):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAWOPFrom" runat="server" CssClass="required form-control number" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtAWOPFrom" />
                    <asp:RangeValidator runat="server" ID="RangeValidator2" ControlToValidate="txtAWOPFrom" MinimumValue="" MaximumValue="1000" Type="Double" ErrorMessage="Value is out of Range." SetFocusOnError="true"></asp:RangeValidator> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">To (AWOP Days):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAWOPTO" runat="server" CssClass="required form-control number" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtAWOPTO" />
                    <asp:RangeValidator runat="server" ID="RangeValidator3" ControlToValidate="txtAWOPTO" MinimumValue="" MaximumValue="1000" Type="Double" ErrorMessage="Value is out of Range." SetFocusOnError="true"></asp:RangeValidator> 
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Percent factor :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="required form-control number" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbe3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="TextBox7" /> 
                    <asp:RangeValidator runat="server" ID="RangeValidator4" ControlToValidate="TextBox7" MinimumValue="" MaximumValue="100" Type="Double" ErrorMessage="Value is out of Range." SetFocusOnError="true"></asp:RangeValidator>  
                </div>
            </div>


            <br />
        </div>                
    </fieldset>
</asp:Panel>

<asp:Button ID="Button4" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="Button4" PopupControlID="Pnlincome" CancelControlID="Linkbutton10" BackgroundCssClass="modalBackground" />

<asp:Panel id="Pnlincome" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="Fieldset5">        
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="Linkbutton10" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="LinkButton11" CssClass="fa fa-floppy-o submit Fieldset5 LinkButton11" OnClick="lnkSaveIncome_Click"  />   
        </div>         
        <div  class="entryPopupDetl2 form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtPayTemplateBonusAllowanceNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="Textbox3" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />                        
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

<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayTemplate_Leave.aspx.vb" Inherits="Secured_PayTemplate_Leave" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<uc:Tab runat="server" menustyle="TabRef" ID="Tab">
    <Content>
        <br />
        <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-4">
                                <h4 class="panel-title">Leave Conversion Components</h4>
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
                        <div class="col-md-2">
                            <%--<asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />--%>
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayTemplateLeaveNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                        <dx:GridViewDataTextColumn FieldName="CStartDate" Caption="From" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="CEndDate" Caption="To" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="BonusBasisDesc" Caption="Conversion Basis" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="LeaveMonitizedTypeDesc" Caption="Monetization Type" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="ExcessOf" Caption="Retention" />
                                        <dx:GridViewDataTextColumn FieldName="MaximumOf" Caption="Max. Allowable" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Status" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeClassDesc" Caption="Classification" />
                                        

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
    </Content>
</uc:Tab> 
    
<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="Panel2"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none;">
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
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPayTemplateLeaveNo" runat="server" ReadOnly="true" CssClass="form-control" />                        
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
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPayClassNo" runat="server" CssClass="form-control required" DataMember="EPayClass" />                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Leave Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboLeaveTypeNo" runat="server" CssClass="form-control required" />                        
               </div>
            </div> 

            

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-required">Conversion Basis :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboBonusBasisNo" DataMember="EBonusbasis" runat="server" CssClass="form-control" />                        
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Monetization Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboLeaveMonitizedtypeNo" DataMember="ELeaveMonitizedtype" runat="server" CssClass="form-control" />                         
                </div>
            </div>
            
            <div class="form-group" runat="server" id="divRetention">
                <label class="col-md-4 control-label has-space">Retention :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtExcessOf"  runat="server" CssClass="number form-control" />  
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtExcessOf" />                                 
                </div>
            </div> 
            <div class="form-group" runat="server" id="divPercent">
                <label class="col-md-4 control-label has-space">Maximum allowable leave(s) for conversion :</label>
                <div class="col-md-3">                   
                    <asp:TextBox ID="txtMaximumOf"  runat="server" CssClass="form-control" />                   
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtMaximumOf" />               
                </div>
            </div> 
            <div class="form-group" runat="server" id="divIsPercent">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtismaximumInPercent" runat="server" Text="&nbsp; Tick if Maximum allowable leave(s) is in percentage"/>
                </div>
            </div> 

            <div class="form-group">  
                <h5 class="col-md-8">
                    <label class="control-label">CRITERIA&nbsp;&nbsp;OF&nbsp;&nbsp;ENTITLEMENT</label>
                </h5>
            </div>

            <div class="form-group" runat="server" id="divIsUpload" >
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-8">
                    <asp:CheckBox ID="txtIsUpload" runat="server" onclick="DisableIsUpload(this);" Text="&nbsp; Tick to allow uploading or manual encoding of eligible employee" />
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
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>

<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="Linkbutton1" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="Fieldset2">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSaveb" CssClass="fa fa-floppy-o submit Fieldset2 lnkSaveb" OnClick="lnkSaveb_Click"  />      
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
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPayClassNoB" runat="server" CssClass="form-control" DataMember="EPayClass" />                        
                   </div>
                </div>
                <div id="Div1" class="form-group" runat="server" visible="false">
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
                <div class="form-group">
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
                <div class="form-group">
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
</asp:Content>

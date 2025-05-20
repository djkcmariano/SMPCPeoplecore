<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BENLeavePolicyList.aspx.vb" Inherits="Secured_BENLeavePolicyList" %>



<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">


<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6">
                        
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process" />
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="LeavePolicyNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="LeavePolicyCode" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="LeavePolicyDesc" Caption="Description" />
                                    <dx:GridViewDataComboBoxColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                    <dx:GridViewDataComboBoxColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeStatDesc" Caption="Employee Status" />
                                    <dx:GridViewDataComboBoxColumn FieldName="EmployeeClassDesc" Caption="Employee Class" />
                                    <dx:GridViewDataTextColumn FieldName="NoOfMonth" Caption="No. Of Months" />
                                    <dx:GridViewDataCheckColumn FieldName="IsSuspended" Caption="Suspended?" />
                                    <dx:GridViewDataComboBoxColumn FieldName="SalaryGradeDesc" Caption="Salary Grade" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="JobGradeDesc" Caption="Job Grade" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="RankDesc" Caption="Rank" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DateBaseDesc" Caption="Date Base" Visible="false" />
                                    <dx:GridViewDataComboBoxColumn FieldName="DateModeDesc" Caption="Date Mode" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MaxCreditAccumulated" Caption="Max. Credit" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MaxCreditAccumulatedYear" Caption="Max. Credit per Year" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MaxYearService" Caption="Max. Year in Service" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="MaxCreditAdd" Caption="Max. Addt'l Credit" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="AddCreditPerYr" Caption="Addt'l Credit per Year" Visible="false" />
                                    <dx:GridViewDataCheckColumn FieldName="IsApplyToAll" Caption="Apply to All?" Visible="false" />
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" >
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
 </div>
 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6 panel-title">
                        <asp:Label ID="lblDetl" runat="server"></asp:Label>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                            </ul>
                            <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" SkinID="grdDX" KeyFieldName="LeavePolicyDetiNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                    <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remarks" />
                                    <dx:GridViewDataTextColumn FieldName="LeaveHrs" Caption="Leave Hr/s" />
                                    <dx:GridViewDataTextColumn FieldName="FromYear" Caption="From Year" />
                                    <dx:GridViewDataTextColumn FieldName="ToYear" Caption="To Year" />
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

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground">
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup">
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
                <div class="col-md-6">
                    <asp:TextBox ID="txtLeavePOlicyCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hifLeavePolicyNo" />
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Leave Type :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboLeavetypeno" runat="server" CssClass="required form-control" >
                    </asp:DropdownList>
               </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtLeavepolicyDesc" TextMode="MultiLine" Rows="2" runat="server" CssClass="required form-control"></asp:TextBox>
                </div>
            </div>
     
            <div class="form-group">
                <label class="col-md-4 control-label">Payroll Group :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboPayclassNo"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Employee Status :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Employee Class :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Job Grade :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboJobGradeNo" DataMember="EJobgrade" runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Rank :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboRankNo" DataMember="ERank"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Date Mode :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboDateModeNo" DataMember="EDateMOde" runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Date Mode for prorated :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboDateModeProrateNo" DataMember="EDateMOde" runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Date Base :</label>
                <div class="col-md-6">
                    <asp:DropdownList ID="cboDateBaseNo" DataMember="EDateBase"  runat="server" CssClass="form-control">
                    </asp:DropdownList>
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">No. of months in service :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtnoofmonth" runat="server" SkinID="txtDate" CssClass="number form-control"></asp:TextBox> 
      
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">EARNING CREDIT(S) :</label>
                <div class="col-md-6">
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Maximum accumulated credits :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMaxCreditAccumulated" runat="server" SkinID="txtDate" CssClass="number form-control"></asp:TextBox>   
        
               </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Maximum accumulated years (reset) :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMaxCreditAccumulatedYear" runat="server" SkinID="txtDate" CssClass="number form-control"></asp:TextBox>   
       
                </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">ADDITIONAL CREDIT(S) :</label>
                <div class="col-md-6">
                 </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Maximum years in service :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMaxYearService" runat="server" SkinID="txtDate" CssClass="number form-control"></asp:TextBox>   
                                                        
                 </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">Maximum additional credits :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMaxCreditAdd" runat="server" SkinID="txtDate" CssClass="number form-control"></asp:TextBox>   
                                                      
                 </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Additional credit/year :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtAddCreditPerYr" runat="server" SkinID="txtDate" CssClass="number form-control"></asp:TextBox>   
                                                        
                 </div>
            </div>
      
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-6">

                     <asp:CheckBox ID="txtIsSuspended" runat="server" Text="&nbsp;Tick to suspend the policy" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-6">
                    <asp:CheckBox ID="txtIsCreditAutomated" runat="server" Text="&nbsp;Tick to automate the crediting" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox ID="txtIsApplyToAll" runat="server" Text="&nbsp;Apply to all" Visible="false" />
                </div>
            </div>
            <br />
        </div>
        
         </fieldset>
</asp:Panel>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClosed" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server"  CssClass="entryPopup2">
       <fieldset class="form" id="fsDetl">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClosed" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsDetl btnSaveDetl" OnClick="btnSaveDetl_Click"  />      
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl2 form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtleavepolicyDetiNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Remarks :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRemark" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control"
                    ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Leave Hr/s :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtleavehrs" SkinID="txtdate" CssClass="number required form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">From year :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtfromYear" SkinID="txtdate" CssClass="number required form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">To year :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txttoyear" SkinID="txtdate" CssClass="number required form-control" runat="server" 
                        ></asp:Textbox>
                </div>
            </div>
        </div>
        
         </fieldset>
</asp:Panel>

<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup"
    CancelControlID="Linkbutton1" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup2" style="display:none;">
       <fieldset class="form" id="Fieldset1">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4></h4>
                <asp:Linkbutton runat="server" ID="Linkbutton1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    
         </div>
         <!-- Body here -->
         <div class="entryPopupDetl2 form-horizontal">
           

            <div class="form-group" style=" display: none;">
                <label class="col-md-4 control-label">CUT-OFF DATE</label>
                <div class="col-md-6">
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Acquired Date :</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPayStartDate" runat="server" SkinID="txtdate" CssClass="required form-control"></asp:TextBox> 
                                                                    
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtPayStartDate"
                        Format="MM/dd/yyyy" />  
                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtPayStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />
                                    
                        <asp:RangeValidator
                        ID="RangeValidator2"
                        runat="server"
                        ControlToValidate="txtPayStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                    
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender3"
                        TargetControlID="RangeValidator2" />                                                                           
                </div>

                <label class="col-md-1 control-label has-space" style=" display: none;">To :</label>
                <div class="col-md-2" style=" display: none;">
                    <asp:TextBox ID="txtPayEndDate" runat="server" SkinID="txtdate" CssClass="form-control"></asp:TextBox> 
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtPayEndDate"
                        Format="MM/dd/yyyy" />
                                      
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtPayEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" />

                        <asp:RangeValidator
                        ID="RangeValidator5"
                        runat="server"
                        ControlToValidate="txtPayEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                    
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender4"
                        TargetControlID="RangeValidator5" />                                                                           
                                  
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-6">
                    <asp:Button runat="server" ID="LinkButton2" CssClass="btn btn-default submit  Fieldset1 LinkButton2" Text="Proceed" OnClick="btnSaveDetail_Click"  />  
                </div>
            </div>
           

        </div>
        
         </fieldset>
</asp:Panel>

</asp:Content>

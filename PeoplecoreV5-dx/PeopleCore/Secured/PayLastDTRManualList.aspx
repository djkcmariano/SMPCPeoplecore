<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayLastDTRManualList.aspx.vb" Inherits="Secured_PayLastDTRManualList" %>
<%@ Register Src="~/Include/HeaderInfo.ascx" TagName="HeaderInfo" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
      
    <uc:HeaderInfo runat="server" ID="HeaderInfo1" />

    <uc:Tab runat="server" ID="Tab">
        <Header>        
            <asp:Label runat="server" ID="lbl" />  
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>    
        </Header>
        <Content>    
            <br /> 
            
        <div class="page-content-wrap">         
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
                    </div>
                    <div class="panel-body">
                         <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="PayDTRManualNo" Width="100%" >                                                                                   
                                    <Columns>           
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>    
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Visible="false" />  
                                    <dx:GridViewDataTextColumn FieldName="Description" Caption="Date Covered" />              
                                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />     
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />    
                                    <dx:GridViewDataTextColumn FieldName="WorkingCutOff" Caption="No. of Cutoffs" />                                                              
                                    <dx:GridViewDataTextColumn FieldName="WorkingHrs" Caption="Work Hrs" />                  
                                    <dx:GridViewDataTextColumn FieldName="PaidLeave" Caption="Leave Hrs" />
                                    <dx:GridViewDataTextColumn FieldName="HolidayHrs" Caption="Paid Hol." />
                                    <dx:GridViewDataTextColumn FieldName="DOvt" Caption="OT" />
                                    <dx:GridViewDataTextColumn FieldName="DOvt8" Caption="OT8" />
                                    <dx:GridViewDataTextColumn FieldName="DNP" Caption="NP" />
                                    <dx:GridViewDataTextColumn FieldName="DNP8" Caption="NP8" />
                                    <dx:GridViewDataTextColumn FieldName="AbsHrs" Caption="Absent" />
                                    <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" />
                                    <dx:GridViewDataTextColumn FieldName="Under" Caption="Under" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" /> 
                                </Columns>    
                                <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />
                                    
                                </dx:ASPxGridView> 
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                   
                            </div>
                        </div>   
                                                                           
                    </div>                   
                </div>

         </div>
     </div>
     
                
<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlShow" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" PopupControlID="pnlPopup" TargetControlID="btnShow"></ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server"  CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsd">
    <!-- Header here -->
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="btnCancel" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsd btnSave" OnClick="btnSave_Click"  />
        </div>
        <!-- Body here -->
        <div  class="entryPopupDetl form-horizontal">
                <div class="form-horizontal">
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-8">
                            <asp:Textbox ID="txtPayDTRManualNo" ReadOnly="true" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group" runat="server" visible="false">
                        <label class="col-md-3 control-label has-required">Name of Employee :</label>
                        <div class="col-md-6">
                             <asp:Dropdownlist ID="cboEmployeeNo" runat="server" CssClass="required form-control" ></asp:Dropdownlist>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Date Covered :</label>
                        <div class="col-md-3">                    
                            <asp:Textbox ID="txtStartDate" runat="server" CssClass="required form-control" placeholder="From" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" />
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDate" />
                            <asp:CompareValidator runat="server" ID="CompareValidator3" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate" />                    
                        </div>
                        <div class="col-md-3">                    
                            <asp:Textbox ID="txtEndDate" runat="server" CssClass="required form-control" placeholder="To" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" />
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDate" />
                            <asp:CompareValidator runat="server" ID="CompareValidator4" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate" />                    
                        </div>
                    </div>
                    <br />

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">REGULAR&nbsp;&nbsp;HOURS&nbsp;&nbsp;</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">Working Hrs</label><br />
                                <asp:TextBox ID="txtWorkingHrs" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtWorkingHrs" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender1" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">NP Hrs</label><br />
                                <asp:TextBox ID="txtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtNP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender2" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">No. of Cutoffs</label><br />
                                <asp:TextBox ID="txtWorkingCutOff" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtWorkingCutOff" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender3" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-3">
                            <code>No. of cutoffs is applicable to monthly rate employees only.</code>
                        </div>
                    </div>


                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">DEDUCTIONS&nbsp;&nbsp;HOURS&nbsp;&nbsp;</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">Absent</label><br />
                                <asp:TextBox ID="txtAbsHrs" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtAbsHrs" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender4" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">Late</label><br />
                                <asp:TextBox ID="txtLate" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtLate" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender5" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">Undertime</label><br />
                                <asp:TextBox ID="txtUnder" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtUnder" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender6" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">LEAVE&nbsp;&nbsp;HOURS&nbsp;&nbsp;</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">VL</label><br />
                                <asp:TextBox ID="txtVL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtVL" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender7" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">SL</label><br />
                                <asp:TextBox ID="txtSL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSL" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender8" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">OB</label><br />
                                <asp:TextBox ID="txtOB" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtOB" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender9" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">Other Leave(s)</label><br />
                                <asp:TextBox ID="txtOL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtOL" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender10" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">EL</label><br />
                                <asp:TextBox ID="txtEL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtEL" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender11" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">ML</label><br />
                                <asp:TextBox ID="txtML" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtML" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender12" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">PTL</label><br />
                                <asp:TextBox ID="txtPTL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtPTL" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender13" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">BD</label><br />
                                <asp:TextBox ID="txtBD" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtBD" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender14" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">FL</label><br />
                                <asp:TextBox ID="txtFL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtFL" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender15" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">PL</label><br />
                                <asp:TextBox ID="txtPL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtPL" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender16" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">SPL</label><br />
                                <asp:TextBox ID="txtSPL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSPL" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender17" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">BL</label><br />
                                <asp:TextBox ID="txtBL" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtBL" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender18" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">HOLIDAY&nbsp;&nbsp;HOURS&nbsp;&nbsp;</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">RWD</label><br />
                                <asp:TextBox ID="txtSHRDCount" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSHRDCount" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender19" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">RD</label><br />
                                <asp:TextBox ID="txtLHRDCount" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtLHRDCount" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender20" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">OVERTIME&nbsp;&nbsp;HOURS&nbsp;&nbsp;</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">OT</label><br />
                                
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">OT>8</label><br />
                                
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">OT NP</label><br />
                                
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <label class="control-label">OT NP>8</label><br />
                                
                            </center>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label-left has-space" style="padding-left:70px;">RWD</label>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtOvt" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender25" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtOvt8" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender26" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtOvtNP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender27" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtOvt8NP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender28" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label-left has-space" style="padding-left:70px;">RD</label>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRDOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRDOvt" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender29" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRDOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRDOvt8" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender30" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRDOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRDOvtNP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender31" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRDOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRDOvt8NP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender32" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label-left has-space" style="padding-left:70px;">ROT</label>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtROvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtROvt" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender21" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtROvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtROvt8" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender22" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtROvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtROvtNP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender23" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtROvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtROvt8NP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender24" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label-left has-space" style="padding-left:70px;">RHNR</label>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRHNROvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRHNROvt" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender33" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRHNROvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRHNROvt8" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender34" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRHNROvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRHNROvtNP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender35" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRHNROvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRHNROvt8NP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender36" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label-left has-space" style="padding-left:70px;">RHRD</label>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRHRDOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRHRDOvt" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender37" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRHRDOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRHRDOvt8" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender38" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRHRDOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRHRDOvtNP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender39" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtRHRDOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtRHRDOvt8NP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender40" runat="server" />
                            </center>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label-left has-space" style="padding-left:70px;">SHNR</label>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtSHNROvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSHNROvt" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender41" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtSHNROvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSHNROvt8" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender42" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtSHNROvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSHNROvtNP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender43" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtSHNROvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSHNROvt8NP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender44" runat="server" />
                            </center>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label-left has-space" style="padding-left:70px;">SHRD</label>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtSHRDOvt" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSHRDOvt" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender45" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtSHRDOvt8" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSHRDOvt8" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender46" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtSHRDOvtNP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSHRDOvtNP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender47" runat="server" />
                            </center>
                        </div>
                        <div class="col-md-2">
                            <center>
                                <asp:TextBox ID="txtSHRDOvt8NP" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender TargetControlID="txtSHRDOvt8NP" FilterType="Numbers, Custom" ValidChars="-." ID="FilteredTextBoxExtender48" runat="server" />
                            </center>
                        </div>
                    </div>

                </div>                               
                <br /><br />  
               </div>
            </fieldset>
            </asp:Panel>                                                  
        </Content>
    </uc:Tab>
</asp:Content>

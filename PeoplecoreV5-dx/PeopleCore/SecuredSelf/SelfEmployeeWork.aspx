<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SelfEmployeeWork.aspx.vb" Inherits="Secured_EmpWorkList" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:TabSelf runat="server" ID="TabSelf" HeaderVisible="true">
    <Header>
        <center>
            <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
            <br />            
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeExpeNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("EmployeeExpeNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="xFromDate" Caption="From" />
                                        <dx:GridViewDataTextColumn FieldName="xToDate" Caption="To" />
                                        <dx:GridViewDataTextColumn FieldName="Position" Caption="Position" />
                                        <dx:GridViewDataTextColumn FieldName="ExpeComp" Caption="Employer" />
                                        <dx:GridViewDataTextColumn FieldName="ExpeTypeDesc" Caption="Experience" />
                                        <dx:GridViewDataTextColumn FieldName="EmployeeStatDesc" Caption="Employment Status" Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="CurrentSalary" Caption="Monthly Salary" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>                                
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
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder= "Autonumber"/>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsGov" Text="&nbsp; Tick if government" />
                </div>
            </div>            
            <h5><b>INCLUSIVE DATES OF EMPLOYMENT</b></h5>
           <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Date From :</label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="cboFromMonth" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                   <asp:DropDownList runat="server" ID="cboFromDay" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtFromYear" CssClass="form-control" placeholder="Year" />        
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtFromYear" />            
                </div>
            </div>
            <div class="form-group" style="display:none">
                <label class="col-md-4 control-label has-space">Date To :</label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="cboToMonth" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="cboToDay" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtToYear" CssClass="form-control" placeholder="Year" /> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtToYear" />                   
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required" id="lblFrom" runat="server">From :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control required" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtFromDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtFromDate" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtFromDate" Display="Dynamic" ForeColor="Red" Font-Size="11px" />
                </div>
                <div class="col-md-4">
                    <asp:CheckBox runat="server" ID="chkIsPresent" Text="&nbsp;Please tick if up to present" AutoPostBack="true" OnCheckedChanged="chkIsPresent_CheckedChanged" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required" id="lblTo" runat="server">To :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control required" />
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtToDate" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtToDate" Mask="99/99/9999" MaskType="Date" />
                    <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtToDate" Display="Dynamic" ForeColor="Red" Font-Size="11px" />
                </div>
            </div>
            <h5><b>OTHER DETAILS</b></h5>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Position Title :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtPosition" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Department / Agency / Office / Company :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtExpeComp" CssClass="form-control required" TextMode="MultiLine" Rows="2"  />
                </div>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Address :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtExpeCompAdd" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Contact No. :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCompPhone" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789-" TargetControlID="txtCompPhone" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Status of Employment / Appointment :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboEmployeeStatNo" DataMember="EEmployeeStat" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Salary/Job/Pay Grade/Step Increment :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtSalaryLevel" CssClass="form-control"  />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Immediate Supervisor :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtImmediateSuperior" CssClass="form-control space" />                    
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label has-space">Industry Type :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtIndustry" CssClass="form-control" Visible='false' />
                    <asp:DropDownList ID="cboIndustryNo" runat="server" DataMember="EIndustry" CssClass="form-control" />
                </div>
            </div>            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Experience Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboExpeTypeNo" runat="server" DataMember="EExpeType" CssClass="form-control" />
                    <asp:HiddenField runat="server" ID="hifExpeTypeNo" />
                    <asp:TextBox runat="server" ID="txtExpeTypeDesc" CssClass="form-control" onblur="ResetExpe()" Visible="false" />                    
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                            TargetControlID="txtExpeTypeDesc" MinimumPrefixLength="2" CompletionSetCount="12" 
                            CompletionInterval="500" ServiceMethod="PopulateExpe" CompletionListCssClass="autocomplete_completionListElement"
                            OnClientItemSelected="GetRecordExpe" FirstRowSelected="true" />
                    <script type="text/javascript">
                        function GetRecordExpe(source, eventArgs) {
                            document.getElementById('<%= hifExpeTypeNo.ClientID %>').value = eventArgs.get_value();
                        }

                        function ResetExpe() {
                            if (document.getElementById('<%= txtExpeTypeDesc.ClientID %>').value == "") {
                                document.getElementById('<%= hifExpeTypeNo.ClientID %>').value = "0";
                            }
                        } 
                    </script>
                </div>
            </div>                                               
            <div class="form-group">
                <label class="col-md-4 control-label has-space"><asp:CheckBox runat="server" ID="txtIsOtherExpe" AutoPostBack="true" OnCheckedChanged="txtIsOtherExpe_CheckedChanged" />  If others (please specify) :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtOtherExpe" CssClass="form-control" TextMode="MultiLine" Rows="2" Placeholder="Experience" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">List of Accomplishments and Contributions :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtAccomplishment" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Summary of Actual Duties :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtDuties" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Monthly Basic Salary :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCurrentSalary" CssClass="form-control required number" />                    
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Monthly Allowances :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtAllowances" CssClass="form-control number" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Reason for Leaving :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtReasonsForLeaving" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                </div>
            </div>
            <br />
        </div>                    
    </fieldset>
</asp:Panel>
</asp:Content>
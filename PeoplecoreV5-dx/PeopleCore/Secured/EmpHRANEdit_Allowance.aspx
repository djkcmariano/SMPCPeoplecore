<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpHRANEdit_Allowance.aspx.vb" Inherits="Secured_EmpHRANEdit_Allowance" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HRANAllowanceNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("HRANAllowanceNo") %>' OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="Allowance Type" />
                                        <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                                        <dx:GridViewDataTextColumn FieldName="StartDate" Caption="Start Date" />
                                        <dx:GridViewDataTextColumn FieldName="Enddate" Caption="End Date" />
                                        <dx:GridViewDataTextColumn FieldName="PayScheduleDesc" Caption="Payroll Schedule" />                                        
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>                                
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>                                       
    </Content>        
    </uc:Tab>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">    
             
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtHRANAllowanceNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>                        

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Allowance Type :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboPayIncomeTypeNo" CssClass="form-control required" runat="server"></asp:DropdownList>
                    </div>
                </div>
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Amount :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="required form-control"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtAmount" />
                    </div>
                </div>  

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Payroll Schedule :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboPayscheduleNo" CssClass="form-control required" DataMember="EPaySchedule" runat="server"></asp:DropdownList>
                    </div>
                </div>
                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Start Date :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtStartDate" runat="server" CssClass="form-control"></asp:Textbox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="MM/dd/yyyy" />                   
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" /> 
                    </div>
                </div>     

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">End Date :</label>
                    <div class="col-md-3">
                        <asp:Textbox ID="txtEndDate" runat="server" CssClass="form-control"></asp:Textbox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="MM/dd/yyyy" />                   
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtEndDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator2" /> 
                    </div>
                </div> 
             
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-8">
                        <asp:CheckBox ID="txtIsPerDay" runat="server" Text="&nbsp; Amount is per day" />
                    </div>
                </div>
             
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-8">
                        <asp:CheckBox ID="txtIsAtleastWithDTR" runat="server" Text="&nbsp; Allowance will give to those who have at least 1 day DTR" Visible="false" AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-8">
                        <asp:CheckBox ID="txtIsDTRBase" runat="server" Text="&nbsp; Prorated base on DTR" AutoPostBack="true" Visible="false" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" />   
                    </div>
                </div>

                
                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-8">
                        <asp:CheckBox ID="txtIsIncludeBonus" runat="server" Text="&nbsp; Include in 13th month/Bonus Processing" Visible="false" />
                    </div>
                </div>
             
                <div class="form-group">
                    <label class="col-md-4 control-label"></label>
                    <div class="col-md-7">
                        <asp:CheckBox ID="txtIsServedA" runat="server" Visible="false" />
                    </div>
                </div>    
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpRateAllow.aspx.vb" Inherits="Secured_EmpRateAllow" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<uc:Tab runat="server" ID="Tab">
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
                            <ul class="panel-controls">                                    
                                <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="EmployeeAllowanceno">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
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

<asp:Button ID="btnShowAllow" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlAllow" runat="server" 
BackgroundCssClass="modalBackground" CancelControlID="btnCancelAllow" 
PopupControlID="pnlPopupAllow" TargetControlID="btnShowAllow">
</ajaxToolkit:ModalPopupExtender>

<asp:Panel ID="pnlPopupAllow" runat="server"  CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsd">
    <!-- Header here -->
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="btnCancelAllow" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="btnSaveDetl" CssClass="fa fa-floppy-o submit fsd btnSaveDetl" OnClick="btnSaveDetl_Click"  />
        </div>
        <!-- Body here -->
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtEmployeeAllowanceNo" runat="server" Enabled="false" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode" runat="server" Enabled="false" CssClass="form-control" Placeholder="Autonumber" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Allowance Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayIncomeTypeNo" runat="server" CssClass="form-control required" >
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control required number" MaxLength="8"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtAmount" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Payroll Schedule :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPayscheduleNo" runat="server" CssClass="form-control" DataMember="EPaySchedule">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Start Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                        Format="MM/dd/yyyy" TargetControlID="txtStartDate" />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ControlToValidate="txtStartDate" Display="None" 
                        ErrorMessage="&lt;b&gt;Please enter valid entry&lt;/b&gt;" 
                        MaximumValue="3000-12-31" MinimumValue="1900-01-01" Type="Date" />
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" 
                        runat="Server" TargetControlID="RangeValidator1" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                        AcceptNegative="Left" ClearTextOnInvalid="true" DisplayMoney="Left" 
                        ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" 
                        MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                        OnInvalidCssClass="MaskedEditError" TargetControlID="txtStartDate">
                    </ajaxToolkit:MaskedEditExtender>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">End Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                        Format="MM/dd/yyyy" TargetControlID="txtEndDate" />
                    <asp:RangeValidator ID="RangeValidator2" runat="server" 
                        ControlToValidate="txtEndDate" Display="None" 
                        ErrorMessage="&lt;b&gt;Please enter valid entry&lt;/b&gt;" 
                        MaximumValue="3000-12-31" MinimumValue="1900-01-01" Type="Date" />
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" 
                        runat="Server" TargetControlID="RangeValidator1" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                        AcceptNegative="Left" ClearTextOnInvalid="true" DisplayMoney="Left" 
                        ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" 
                        MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                        OnInvalidCssClass="MaskedEditError" TargetControlID="txtEndDate" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-8">
                    <asp:CheckBox ID="txtIsPerDay" runat="server" Text="&nbsp; Amount is per day" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsAtleastWithDTR" runat="server" Text="&nbsp; Allowance will give to those who have at least 1 day DTR"  AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" visible="false"/>                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsDTRBase" runat="server" Text="&nbsp; Prorated base on DTR" AutoPostBack="true" OnCheckedChanged="txtIsRefresh_OnCheckedChanged" visible="false" />                        
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox ID="txtIsIncludeBonus" runat="server" Text="&nbsp; Include 13th month/bonus processing" visible="false"/>                        
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-7">
                    <asp:CheckBox ID="chkIsServedA" runat="server" visible="false"/>
                </div>
            </div>
        </div>
        </fieldset>
</asp:Panel>
     
</asp:Content>


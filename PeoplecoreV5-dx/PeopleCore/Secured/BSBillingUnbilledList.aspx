<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSBillingUnbilledList.aspx.vb" Inherits="Secured_BSBillingUnbilledList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">


<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                
                <div>
                    <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                        <Content>
                                <asp:Panel ID="Panel1" runat="server">

                                    <div class="form-group">
                                        <label class="col-md-4 control-label">Payroll Group :</label>
                                        <div class="col-md-6">
                                            <asp:Dropdownlist ID="cboPayClassNo" runat="server" CssClass="form-control"
                                                ></asp:Dropdownlist>
                                       </div>
                                    </div>
		                           
                                    <div class="form-group">
                                        <label class="col-md-4 control-label">Date From :</label>
                                        <div class="col-md-8">
                                            <asp:Textbox runat="server" ID="fltxtStartDate" SkinID="txtdate" CssClass="form-control" ></asp:Textbox>
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="fltxtStartDate" Format="MM/dd/yyyy"/>  
                                        <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="fltxtStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator2" /> 
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="fltxtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />

                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-4 control-label">Date To :</label>
                                        <div class="col-md-8">
                                            <asp:Textbox runat="server" ID="fltxtEndDate" SkinID="txtdate" CssClass="form-control" ></asp:Textbox>
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="fltxtEndDate" Format="MM/dd/yyyy"/>  
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="fltxtEndDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5" TargetControlID="RangeValidator2" /> 
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="fltxtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />

                                    </div>
                                </asp:Panel>
                          </Content>
                    </uc:Filter>
                </div>
                
                      
                       
            <div>
                &nbsp;                  
            </div> 
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PayNo">                                                                                   
                        <Columns>                            
                            <dx:GridViewDataTextColumn FieldName="PayCode" Caption="Payroll No." />
                            <dx:GridViewDataTextColumn FieldName="ProjectDesc" Caption="Project Name" />
                            <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                            <dx:GridViewDataTextColumn FieldName="PayDate" Caption="Payroll Date" />      
                            <dx:GridViewDataTextColumn FieldName="PayPeriod" Caption="Period" />                                                  
                            <dx:GridViewDataTextColumn FieldName="MonthDesc" Caption="Month" /> 
                            <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Year" /> 
                                                                               
                            <dx:GridViewDataTextColumn FieldName="BSClientDesc" Caption="Client Name" Visible="false" />
                           
                                               
                        </Columns>                            
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                        
                </div>                            
            </div>
        </div>
    </div>
</div>
<br /><br />


</asp:Content>
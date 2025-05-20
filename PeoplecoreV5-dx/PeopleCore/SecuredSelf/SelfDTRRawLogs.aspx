<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfDTRRawLogs.aspx.vb" Inherits="SecuredSelf_SelfDTRRawLogs" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>                                                
                            <ul class="panel-controls pull-left">                                                        
                                <%--<li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>--%>
                                <asp:Button ID="btnIn" Text="Time In" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="lnkIn_Click" ToolTip="Click here to time in" ></asp:Button>
                                <asp:Button ID="btnOut" Text="Time Out" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="lnkOut_Click"  ToolTip="Click here to time out" ></asp:Button>
                               <%-- <li><asp:LinkButton runat="server" ID="lnkIn" OnClick="lnkIn_Click" Text="IN" CssClass="control-primary" /></li> 
                                <li><asp:LinkButton runat="server" ID="lnkOut" OnClick="lnkOut_Click" Text="OUT" CssClass="control-primary" /></li>--%>                                                                                
                            </ul>
                        </ContentTemplate>
                        <%--<Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </div>
                <div>
                    <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                        <Content>
                                <asp:Panel ID="Panel1" runat="server">
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
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="FPDTRId">                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="FPId" Caption="FPID" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataDateColumn FieldName="DTRDate" Caption="DTR Date" />
                                <dx:GridViewDataTextColumn FieldName="LogType" Caption="Log Type" />
                                <dx:GridViewDataTextColumn FieldName="DTRTime" Caption="DTR Time" />
                                <dx:GridViewBandColumn Caption="Converted" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <Columns>
                                        <dx:GridViewDataDateColumn FieldName="CDTRDate" Caption="DTR Date" />
                                        <dx:GridViewDataTextColumn FieldName="CDTRTime" Caption="DTR Time" />
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataTextColumn FieldName="FPMachineDescription" Caption="Machine Name"/>
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="EncodedDate" Caption="Date Uploaded" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>



</asp:Content>

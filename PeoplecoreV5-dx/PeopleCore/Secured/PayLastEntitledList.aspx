<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayLastEntitledList.aspx.vb" Inherits="Secured_PayLastEntitledList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                            <div class="form-group">

                            </div>                
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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PayLastEntitledNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="BonusTypeDesc" Caption="Bonus Type" />
                                        <dx:GridViewDataTextColumn FieldName="LeaveTypeDesc" Caption="Leave Type" />
                                        <dx:GridViewDataTextColumn FieldName="BonusBasisDesc" Caption="Bonus Basis" />
                                        <dx:GridViewDataTextColumn FieldName="DateBaseDesc" Caption="Base Date" />
                                        <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="Percent Factor" />
                                        <dx:GridViewDataTextColumn FieldName="CStartDate" Caption="Start Date" />
                                        <dx:GridViewDataTextColumn FieldName="CEndDate" Caption="End Date" />
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                                    </Columns>
                                    <SettingsBehavior EnableCustomizationWindow="true" AllowFocusedRow="true"  />       
                                </dx:ASPxGridView>       
                                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />               
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>

            <asp:Button ID="btnShow" runat="server" style="display:none" />
            <ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="Panel2" CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
            </ajaxtoolkit:ModalPopupExtender>

            <asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
                    <fieldset class="form" id="fsMain">
                    <!-- Header here -->
                     <div class="cf popupheader">
                            <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                            <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
                     </div>
                     <!-- Body here -->
                     <div  class="entryPopupDetl form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtPayLastEntitledNo" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-6">
                                <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Benefit Source :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboBenefitSourceNo"  runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboBenefitSourceNo_SelectedIndexChanged" >
                                    <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                                    <asp:ListItem Value="1" Text="Bonus/other Benefit" />
                                    <asp:ListItem Value="2" Text="Leave" />
                                    
                                </asp:Dropdownlist>
                           </div>
                        </div>
                        <asp:PlaceHolder runat="server" ID="phleave" Visible="false">
                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Leave Type :</label>
                                <div class="col-md-6">
                                    <asp:Dropdownlist ID="cboLeaveTypeNo"  runat="server" CssClass="form-control required" ></asp:Dropdownlist>
                               </div>
                            </div> 
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="phbonus" Visible="false">
                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Bonus Type :</label>
                                <div class="col-md-6">
                                    <asp:Dropdownlist ID="cboBonusTypeNo" DataMember="EBonusType" runat="server" CssClass="form-control required" ></asp:Dropdownlist>
                               </div>
                            </div> 
                        </asp:PlaceHolder>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Bonus Basis :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboBonusBasisNo" DataMember="EBonusBasis" runat="server" CssClass="form-control required" ></asp:Dropdownlist>
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Base Date :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboDateBaseNo" DataMember="EDateBase" runat="server" CssClass="form-control required"></asp:Dropdownlist>
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Percent Factor :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtPercentFactor"  runat="server" CssClass="number form-control required"></asp:TextBox>   
                            </div>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Minimum years of service :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtMinServiceYear"  runat="server" CssClass="number form-control required"></asp:TextBox>   
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label ">CUT-OFF DATE :</label>
                            <div class="col-md-6">
           
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required ">Date :</label>
                            <div class="col-md-3">
                                <asp:Textbox ID="txtcStartDate" runat="server" CssClass="required form-control" Placeholder="Start Date"></asp:Textbox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4xx" runat="server" TargetControlID="txtcStartDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy" />                                                 
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4xx" runat="server" TargetControlID="txtcStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                                <asp:RangeValidator ID="RangeValidator1xx" runat="server" ControlToValidate="txtcStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender6" TargetControlID="RangeValidator1xx" />

                            </div>

                            <div class="col-md-3">
                                <asp:Textbox ID="txtcEndDate" runat="server" CssClass="required form-control" Placeholder="End Date"></asp:Textbox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtcEndDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />                                               
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtcEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtcEndDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator3" />
                            </div>
                        </div> 
           
                        <br />
                        </div>
                      <!-- Footer here -->
         
                     </fieldset>
            </asp:Panel>

    </Content>
</uc:Tab>

</asp:Content> 
<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayBonusDeduPolicyList.aspx.vb" Inherits="Secured_PayBonusDeduPolicyList" %>


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
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="PayBonusDeduPolicyNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                          <dx:GridViewBandColumn Caption="DTR Cut-off" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="StartDateX" Caption="Date From" />
                                                <dx:GridViewDataTextColumn FieldName="EndDateX" Caption="Date To" />
                                            </Columns>
                                        </dx:GridViewBandColumn> 

                                        <dx:GridViewBandColumn Caption="Percent Factor (Count)" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="Late" Caption="Late" />
                                                <dx:GridViewDataTextColumn FieldName="Under" Caption="Under" />
                                                <dx:GridViewDataTextColumn FieldName="Absent" Caption="Absent" />
                                            </Columns>
                                        </dx:GridViewBandColumn> 
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />                                        
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn> 
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
            <br />
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">                                
                        <div class="col-md-10">                                           
                            <div class="form-group">

                            </div>                
                        </div>               
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <ul class="panel-controls">
                                        <li><asp:LinkButton runat="server" ID="lnkaddd" OnClick="lnkAddfactor_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                        <li><asp:LinkButton runat="server" ID="lnkDeleted" OnClick="lnkDeletefactor_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <%--<li><asp:LinkButton runat="server" ID="lnkExportd" OnClick="lnkExportfactor_Click" Text="Export" CssClass="control-primary" /></li>--%>
                                    </ul>
                                    <uc:ConfirmBox runat="server" ID="cfbDeletex" TargetControlID="lnkDeleted" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExport" />
                                </Triggers>
                            </asp:UpdatePanel> 
                        </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdFactor" ClientInstanceName="grdFactor" runat="server" KeyFieldName="PayBonusDeduPolicyDetiNo" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEditfactor_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="SFrom" Caption="From (AWOP Count)" />
                                        <dx:GridViewDataTextColumn FieldName="STo" Caption="To (AWOP Count)" />
                                        <dx:GridViewDataTextColumn FieldName="PercentFactor" Caption="Factor Rate" />

                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="5%" />                                                             
                                    </Columns>
                        
                                    <Settings ShowFooter="true" />       
                                   
                                </dx:ASPxGridView>    
                                <dx:ASPxGridViewExporter ID="grdDetlExport" runat="server" GridViewID="grdFactor" />                  
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
                                <asp:Textbox ID="txtPayBonusDeduPolicyNo" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-6">                                        
                                <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber" />                        
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
                        <div class="form-group">
                            <label class="col-md-4 control-label">
                                <h5><b>Percent Factor</b></h5>
                            </label>
                        </div>                        
                        <div class="form-group">
                            <label class="col-md-4 control-label">Late :</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtLate" runat="server" CssClass="number form-control" />                                                          
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Undertime :</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtUnder" runat="server" CssClass="number form-control" />                                                                              
                            </div>
                        </div>
                        <div class="form-group">
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
                <asp:LinkButton runat="server" ID="lnkSavefactor" CssClass="fa fa-floppy-o submit Fieldset3 lnkSavefactor" OnClick="lnkSaveFactor_Click"  />   
            </div>         
            <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-6">
                        <asp:Textbox ID="txtPayBonusDeduPolicyDetiNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="Textbox1" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />                        
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">From (AWOP Count):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSFrom" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">To (AWOP Count):</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSTo" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Percent factor :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPercentFactor" runat="server" CssClass="required form-control number" /> 
                              
                </div>
            </div>


            <br />
        </div>                
    </fieldset>
</asp:Panel>

    </Content>
</uc:Tab>

</asp:Content> 
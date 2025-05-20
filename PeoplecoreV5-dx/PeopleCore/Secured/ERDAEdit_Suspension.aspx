<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="ERDAEdit_Suspension.aspx.vb" Inherits="Secured_ERProgramCostEdit" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">



<uc:Tab runat="server" ID="Tab">
    <Header>        
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />            
            </center> 
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>      
    </Header>
     <Content>
             <script type="text/javascript">
                         function cbCheckAll_CheckedChanged(s, e) {
                             grdMain.PerformCallback(s.GetChecked().toString());
                         }
            </script>
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
                                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post to DTR" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                    </ul>
                                    <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?"  />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkExport" />
                                </Triggers>
                            </asp:UpdatePanel> 
                        </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="DADetiNo" SkinId="grdDX"
                                OnCommandButtonInitialize="grdMain_CommandButtonInitialize"  OnCustomCallback="gridMain_CustomCallback">
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                        <dx:GridViewDataTextColumn FieldName="SuspensionDate" Caption="Suspension Date" />  
                                        <dx:GridViewDataTextColumn FieldName="PostedDate" Caption="Date Posted" />  
                                        <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" />   
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                        <HeaderTemplate>
                                                <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                                </dx:ASPxCheckBox>
                                            </HeaderTemplate>
				                        </dx:GridViewCommandColumn>                                                         
                                    </Columns>  
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
                                <asp:Textbox ID="txtDADetiNo" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label">Transaction No. :</label>
                            <div class="col-md-7">
                                    <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                                </div>
                        </div>
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Suspension Date :</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtSuspensionDate" runat="server"  CssClass="required form-control"  ></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSuspensionDate" Format="MM/dd/yyyy" />  
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtSuspensionDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtSuspensionDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender9" TargetControlID="RangeValidator3" /> 
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
<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PEMeritList.aspx.vb" Inherits="Secured_PEMeritList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">                                
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>                
                 <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>                    
                    <ul class="panel-controls">                        
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be modified, Proceed?" MessageType="Post"  />

                    <uc:ConfirmBox runat="server" ID="cfblnkProcess" TargetControlID="lnkProcess" ConfirmMessage="Do you want to proceed?" MessageType="Process"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEMeritNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil control-primary" Font-Size="Medium" CommandArgument='<%# Bind("PEMeritNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Merit No." />    
                                <dx:GridViewDataTextColumn FieldName="PEReviewSummaryMainCode" Caption="PE Summary No." />                                                           
                                <dx:GridViewDataTextColumn FieldName="Applicableyear" Caption="Applicable Year" CellStyle-HorizontalAlign="Left" />                                                                                
                                <dx:GridViewDataTextColumn FieldName="PEPeriodDesc" Caption="Period Type" />                                                     
                                <dx:GridViewDataTextColumn FieldName="xEffectivity" Caption="Effectivity Date" />
                                <dx:GridViewDataTextColumn FieldName="xPreparedDate" Caption="Prepared Date" Visible="False"/>
                                <dx:GridViewDataCheckColumn FieldName="Isbonus" Caption="Bonus" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoded By" Visible="False" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Encoded Date" Visible="False" />  
                                <dx:GridViewDataCheckColumn FieldName="IsPosted" Caption="Posted" Visible="False" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" />
                                <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" Visible="False" />                                                                           
                                <dx:GridViewDataTextColumn FieldName="DatePosted" Caption="Posted Date" Visible="False" />
                                <dx:GridViewDataTextColumn FieldName="LastDateProcess" Caption="Date Processed"/> 
                                
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkView" CssClass="fa fa-external-link" Font-Size="Medium" OnClick="lnkView_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>

                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Select" />     
                            </Columns>                         
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />

                        

                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopup" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPEMeritNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">PE Summary No. :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPEReviewSummaryMainLNo" runat="server" DataMember="EPEReviewSummaryMainL" CssClass="form-control required"></asp:Dropdownlist>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Effectivity Date :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtEffectivity" SkinID="txtdate" runat="server" CssClass="form-control required"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffectivity" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEffectivity" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtEffectivity" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />     
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" /> 
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Prepared Date :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPreparedDate" SkinID="txtdate" runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPreparedDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtPreparedDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPreparedDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                       
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator2" /> 

                </div>
            </div>  
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Remarks :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtRemark" runat="server" CssClass="form-control required" TextMode="Multiline" Rows="3" ></asp:Textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">HRAN Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboHRANTypeNo" runat="server" DataMember="EHRANType" CssClass="form-control required"></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label"></label>
                <div class="col-md-7">
                    <label class="check">
                        <asp:Checkbox ID="txtIsBonus"  runat="server" CssClass="icheckbox"></asp:Checkbox>
                        Please check here to identify if use for bonus.
                    </label>
                </div>
            </div> 
                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


</asp:content>

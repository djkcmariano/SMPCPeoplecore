<%@ Page Language="VB" AutoEventWireup="false"  Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRScheduler.aspx.vb" Inherits="Secured_DTRDTRScheduler" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">
                    <div class="form-group">
                        <div class="col-md-4">
                            <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                        </div>
                        <ul class="panel-controls pull-left">
                            <li>
                                <a style="text-decoration:none;color:Black;">DTR Date between &nbsp;&nbsp;</a>
                            </li>
                            <li >
                                <asp:TextBox ID="txtDate"  SkinID="txtdate" runat="server" CssClass="form-control" placeholder="date 1"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate" Format="MM/dd/yyyy" />  
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true" />
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                             
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender6" TargetControlID="RangeValidator1" />
                            </li>
                            <li>
                                <a style="text-decoration:none;color:Black;">And &nbsp;&nbsp;</a>
                            </li>
                            <li >
                                <asp:TextBox ID="txtDate2"  SkinID="txtdate" runat="server" CssClass="form-control" placeholder="date 2"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate2" Format="MM/dd/yyyy" />  
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDate2" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true" />
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                             
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator2" />
                            </li>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkFilter" OnClick="lnkSearch_Click" Text="Filter" CssClass="control-primary" />
                            </li>

                        </ul>
                    </div>
                </div>
                <div>                                                                 
                    <ul class="panel-controls">                                                                                
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkUpload" OnClick="lnkUpload_Click" Text="Upload" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                                                                        
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>                    
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRSchedulerNo">                                                                                   
                            <Columns>                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="xDTRDate" Caption="Date" />
                                <dx:GridViewDataTextColumn FieldName="In1" Caption="In 1" />
                                <dx:GridViewDataTextColumn FieldName="Out1" Caption="Out 1" />
                                <dx:GridViewDataCheckColumn FieldName="IsRD" Caption="Day Off" ReadOnly="true"/>
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" />
                                <dx:GridViewDataTextColumn FieldName="EncodeDate" Caption="Uploaded Date" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Uploaded By" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PostedDate" Caption="Posted Date" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PostedBy" Caption="Posted By" Visible="false" />                                                                                                                                                                                                                                                                                                                                                                                                     
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>                            
                        </dx:ASPxGridView>                        
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="imgClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">        
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" >
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkSave" />
            </Triggers>
            <ContentTemplate>
                <div class="cf popupheader">
                    <h4>&nbsp;</h4>                
                    <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                    <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />     
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>                    
        <div class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Files :</label>
                <div class="col-md-6">
                    <asp:FileUpload ID="txtFile" runat="server" CssClass="required"  Width="404px" />
                </div>
                <br /><br />
            </div>
        </div>        
    </fieldset>
</asp:Panel>
</asp:Content>

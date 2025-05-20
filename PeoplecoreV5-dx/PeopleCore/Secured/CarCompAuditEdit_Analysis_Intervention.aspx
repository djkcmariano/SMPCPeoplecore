<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="CarCompAuditEdit_Analysis_Intervention.aspx.vb" Inherits="Secured_CarCompAuditEdit_Analysis_Intervention" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">                                
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="CompInterventionNo">                                                                                   
                                <Columns>
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("CompInterventionNo") %>' OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                    <dx:GridViewDataTextColumn FieldName="CompInterventionTypeDesc" Caption="Intervention Type"/>
                                    <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="Projecteddate" Caption="Projected Date" />         
                                    <dx:GridViewDataTextColumn FieldName="PercentToComplete" Caption="Percent To Complete" />                              
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
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
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
         <div  class="entryPopupDetl form-horizontal">
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCompInterventionNo" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>     
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                    </div>
                </div>             
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Intervention Type :</label>
                    <div class="col-md-7">                        
                        <asp:Dropdownlist ID="cboCompInterventionTypeNo" DataMember="ECompInterventionType" CssClass="form-control required" OnSelectedIndexChanged="cboCompInterventionTypeNo_Change" AutoPostBack="true" runat="server" ></asp:Dropdownlist>
                    </div>
                </div>
                                       
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Training Title :</label>
                    <div class="col-md-7">
                        <asp:DropdownList ID="cboTrnTitleNo" DataMember="ETrnTitle" runat="server" CssClass="form-control"></asp:DropdownList>
                        <asp:Checkbox ID="chkIsfortraining" style="visibility:hidden;position:absolute;" runat="server" ></asp:Checkbox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Description :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtDescription" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control required"
                        ></asp:Textbox>
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Projected Date :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtprojecteddate"  runat="server" CssClass="form-control required" ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                        TargetControlID="txtprojecteddate"
                        Format="MM/dd/yyyy" />  
                                                                      
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtprojecteddate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  />
                                                                        
                        <asp:RangeValidator
                        ID="RangeValidator1"
                        runat="server"
                        ControlToValidate="txtprojecteddate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender 
                        runat="Server" 
                        ID="ValidatorCalloutExtender2"
                        TargetControlID="RangeValidator1" />
                   </div>
                </div>  
                            
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Percent Completed :</label>
                    <div class="col-md-3">
                         <asp:Textbox ID="txtPercentToComplete" runat="server" CssClass="form-control" ></asp:Textbox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtPercentToComplete" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space"></label>
                    <div class="col-md-7">
                        <asp:Checkbox ID="txtIsCompleted" runat="server" Text="&nbsp; Completed?" ></asp:Checkbox>
                    </div>
                </div> 
                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


</asp:content>
<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayTemplate.aspx.vb" Inherits="Secured_PayTemplate" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<uc:Tab runat="server" menustyle="TabRef" ID="Tab">
        <Content>
            <br />
            <div class="page-content-wrap" >         
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-md-2">
                                <%--<asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />--%>
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
                                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" Width="100%" KeyFieldName="PayTemplateNo">                                                                                   
                                        <Columns>
                                            <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>                                    
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                            <dx:GridViewDataTextColumn FieldName="PayClassDesc" Caption="Payroll Group" />
                                            <dx:GridViewDataTextColumn FieldName="PayScheduleDesc" Caption="Payroll Schedule" />
                                            <dx:GridViewDataTextColumn FieldName="PaySourceDesc" Caption="Payroll Source" />                                                              
                                            <dx:GridViewBandColumn Caption="Applicable Deductions" HeaderStyle-HorizontalAlign="Center">
                                                <Columns> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductTax" Caption="Tax"/> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductSSS" Caption="SSS"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductPH" Caption="PH"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductHDMF" Caption="HDMF"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductPF" Caption="PF" Visible="False"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsDeductIHP" Caption="HF" Visible="False"/>
                                                 </Columns>
                                            </dx:GridViewBandColumn> 
                                            <dx:GridViewBandColumn Caption="Payroll Components" HeaderStyle-HorizontalAlign="Center">
                                                <Columns> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsAttendanceBase" Caption="DTR Base"/> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsIncludeForw" Caption="Forwarded"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsIncludeMass" Caption="Template"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsIncludeOther" Caption="Other"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsIncludeLoan" Caption="Loan"/>
                                                 </Columns>
                                            </dx:GridViewBandColumn> 
                                            <dx:GridViewBandColumn Caption="Other Components" HeaderStyle-HorizontalAlign="Center" Visible="False">
                                                <Columns> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsRATA" Caption="RATA"/> 
                                                    <dx:GridViewDataCheckColumn FieldName="IsLoyalTy" Caption="Loyalty"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsRice" Caption="Rice"/>
                                                    <dx:GridViewDataCheckColumn FieldName="IsMedical" Caption="Medical"/>
                                                 </Columns>
                                            </dx:GridViewBandColumn>                                                        
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />

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
    
<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl"
    CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup" style=" display:none;">
    <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="lnkSave" CssClass="fa fa-floppy-o submit fsMain lnkSave" OnClick="lnkSave_Click"  />      
         </div>
         <!-- Body here -->
            <div  class="entryPopupDetl form-horizontal">              
                <div class="form-group">
                    <label class="col-md-4 control-label">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCode"  runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">Payroll Group :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPayClassNo" runat="server" CssClass="form-control required" DataMember="EPayClass" />                        
                   </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">Payroll Source :</label>
                    <div class="col-md-6">
                        <asp:Dropdownlist ID="cboPaySourceNo" runat="server" CssClass="form-control" DataMember="EPaySource" />                        
                   </div>
                </div>
                
                <div class="form-group">
                    <label class="col-md-4 control-label  has-required">Payroll Schedule :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayScheduleNo" runat="server" CssClass="form-control required" AutoPostBack="true" OnTextChanged="cboPaySchedule_TextChanged" ></asp:Dropdownlist>
                    </div>
                </div> 
               
             
                <div class="form-group">
                    <label class="col-md-4 control-label">
                        <h5><b>Applicable Deduction</b></h5>
                    </label>
                </div>            
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsDeductTax" runat="server" Text="&nbsp;Tax" />                    
                    </div>
                    <div class="col-md-3">
                        <asp:Checkbox ID="txtIsDeductSSS" runat="server" Text="&nbsp;SSS" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">
                        <asp:Checkbox ID="txtIsDeductPH" runat="server" Text="&nbsp;PhilHealth" />
                    </div>
                    <div class="col-md-3">
                        <asp:Checkbox ID="txtIsDeductHDMF" runat="server" Text="&nbsp;Pag-ibig" />
                    </div>                                 
                </div>
                <div class="form-group" style="Display: none">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">
                        <asp:Checkbox ID="txtIsDeductPF" runat="server" Text="&nbsp;PF" />
                    </div>
                    <div class="col-md-3">
                        <asp:Checkbox ID="txtIsDeductIHP" runat="server" Text="&nbsp;HF" />
                    </div>                                 
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">
                        <h5><b>Payroll Components</b></h5>
                    </label>
                </div>                      
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsAttendanceBase" runat="server" Text="&nbsp;DTR Base" />
                    </div>
                    <div class="col-md-3" style="visibility:hidden; position:absolute;">                    
                        <asp:Checkbox ID="txtIsAttendanceNonBasic" runat="server" Text="&nbsp;DTR Base (Non-basic)" />
                    </div>
                </div>             
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsIncludeMass" runat="server" Text="&nbsp;Template" />
                    </div>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsIncludeForw" runat="server" Text="&nbsp;Forwarded" />
                    </div>
                </div>            
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsIncludeLoan" runat="server" Text="&nbsp;Loan" />
                    </div>
                    <div class="col-md-3">                    
                        <asp:Checkbox ID="txtIsIncludeOther" runat="server" Text="&nbsp;Other" />
                    </div>
                </div>
                <div class="form-group" Style="Display: none">
                    <label class="col-md-4 control-label">
                        <h5><b>OTHER COMPONENTS</b></h5>
                    </label>
                </div>
                <div class="form-group" Style="Display: none">
                    <label class="col-md-4 control-label has-space">&nbsp;</label>
                    <div class="col-md-2">                    
                        <asp:Checkbox ID="txtIsRATA" runat="server" Text="&nbsp;RATA" />
                    </div>
                    <div class="col-md-2">                    
                        <asp:Checkbox ID="txtIsRice" runat="server" Text="&nbsp;Rice" Visible="true" />
                    </div>
                    <div class="col-md-2">                    
                        <asp:Checkbox ID="txtIsMedical" runat="server" Text="&nbsp;Medical" Visible="true" />
                    </div>
                    <div class="col-md-2">                    
                        <asp:Checkbox ID="txtIsLoyalty" runat="server" Text="&nbsp;Loyalty" />
                    </div>
                </div>
                <br />          
                <br />
                
              </div>     
          <!-- Footer here -->
         <br />   
        
    </fieldset>

</asp:Panel>


</asp:Content>

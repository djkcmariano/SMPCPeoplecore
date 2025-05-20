<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="BenRaffleList.aspx.vb" Inherits="Secured_BenRaffleList" EnableEventValidation="false" %>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

     <script type="text/javascript">
         function cbCheckAll_CheckedChanged(s, e) {
             grdDetl.PerformCallback(s.GetChecked().toString());
         }

    </script>

<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>                                                
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <ul class="panel-controls">
                                <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkGenerate" OnClick="lnkGenerate_Click" Text="Generate" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                              
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                <uc:ConfirmBox runat="server" ID="ConfirmBox2" TargetControlID="lnkPost" ConfirmMessage="Posted record cannot be recovered, Proceed?" />     
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkExport" />
                        </Triggers>
                    </asp:UpdatePanel>
                                                                                                                                                                             
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="RaffleNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="RaffleCode" Caption="Code" />
                                <dx:GridViewDataTextColumn FieldName="RaffleDesc" Caption="Description" />
                                <dx:GridViewDataTextColumn FieldName="PayIncomeTypeDesc" Caption="PayIncome Type" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="RaffleEntryTypeDesc" Caption="Type of Entry" />
                                <dx:GridViewDataDateColumn FieldName="DateFrom" Caption="Date From" />
                                <dx:GridViewDataDateColumn FieldName="DateTo" Caption="Date To" />
                                <dx:GridViewDataTextColumn FieldName="NoOfWinners" Caption="No. of Winner(s)" />
                                <dx:GridViewDataTextColumn FieldName="Remarks" Caption="Remarks" Visible="false" />
                                <dx:GridViewDataColumn Caption="Prize(s)"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkPrice" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkPrice_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                         
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                                                        
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="5%" />
                            </Columns>                            
                        </dx:ASPxGridView> 
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />                                
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>
<br />
<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6 panel-title">
                    Transaction No. :&nbsp;<asp:Label runat="server" ID="lbl" />
                </div>
                <div>                                                
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">                                                
                                    <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkExportDetl" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>                                                                                                                                                     
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" SkinID="grdDX" runat="server" KeyFieldName="RaffleWinnerNo" Width="100%">
                            <Columns>                           
                                <dx:GridViewDataTextColumn FieldName="RankDesc" Caption="Prize" />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />                                                              
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />
                                <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />                                                               
                            </Columns>                     
                        </dx:ASPxGridView>     
                        <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />                            
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>
</div>

<asp:Button ID="btnShowMain" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlPopupMain" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRaffleNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRaffleCode" runat="server" CssClass="form-control required"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtRaffleDesc" runat="server" CssClass="form-control required"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Income Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboPayIncomeTypeNo" DataMember="EPayIncomeType" runat="server" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Type of Entry :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboRaffleEntryTypeNo" DataMember="ERaffleEntryType" runat="server" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">
                Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="required form-control" style="display:inline-block;" Placeholder="From"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                    TargetControlID="txtDateFrom"
                    Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                    TargetControlID="txtDateFrom"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left" />
                    <asp:RangeValidator
                    ID="RangeValidator3"
                    runat="server"
                    ControlToValidate="txtDateFrom"
                    ErrorMessage="<b>Please enter valid entry</b>"
                    MinimumValue="1900-01-01"
                    MaximumValue="3000-12-31"
                    Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender 
                    runat="Server" 
                    ID="ValidatorCalloutExtender4"
                    TargetControlID="RangeValidator3" />
                </div>
                <%--<label class="col-md-1 control-label">To :</label>--%>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="required form-control" style="display:inline-block;"  Placeholder="To"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                    TargetControlID="txtDateTo"
                    Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                    TargetControlID="txtDateTo"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Date"
                    DisplayMoney="Left"
                    AcceptNegative="Left" />
                    <asp:RangeValidator
                    ID="RangeValidator4"
                    runat="server"
                    ControlToValidate="txtDateTo"
                    ErrorMessage="<b>Please enter valid entry</b>"
                    MinimumValue="1900-01-01"
                    MaximumValue="3000-12-31"
                    Type="Date" Display="None"  />
                    <ajaxToolkit:ValidatorCalloutExtender 
                    runat="Server" 
                    ID="ValidatorCalloutExtender2"
                    TargetControlID="RangeValidator4" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">No. of Winner(s) :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtNoOfWinners" runat="server" CssClass="number form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Remarks :</label>
                <div class="col-md-6"> 
                    <asp:Textbox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsWithPay" Text="&nbsp;Tick here to forward to Forwarded Income." />
                </div>
            </div>

        </div>

        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


</asp:content>

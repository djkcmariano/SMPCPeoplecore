<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="BenFidelityBondList.aspx.vb" Inherits="Secured_BenFidelityBondList" EnableEventValidation="false" %>


<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Comma(Num) {
            Num += '';
            Num = Num.replace(/,/g, '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            //var rgx = /(\d)((\d)(\d{2}?)+)$/;
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            return x1 + x2;
        }
    </script>
</asp:Content>
<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                                <li><asp:LinkButton runat="server" ID="lnkCancel" OnClick="lnkCancel_Click" Text="Cancel" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                              
                                <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>       
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                                <uc:ConfirmBox runat="server" ID="cfbPost" TargetControlID="lnkPost" ConfirmMessage="Are you sure you want to post transaction?" MessageType="Post"  />
                                <uc:ConfirmBox runat="server" ID="cfbCancel" TargetControlID="lnkCancel" ConfirmMessage="Are you sure you want to cancel transaction?" MessageType="Post"  />
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="FidelityNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataComboBoxColumn FieldName="PositionDesc" Caption="Position" />
                                <dx:GridViewDataComboBoxColumn FieldName="DepartmentDesc" Caption="Department" />
                                <dx:GridViewBandColumn Caption="Total Accountability" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Cash" Caption="Cash" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        <dx:GridViewDataTextColumn FieldName="Property" Caption="Property" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        <dx:GridViewDataTextColumn FieldName="Form" Caption="Forms" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        <dx:GridViewDataTextColumn FieldName="TotalAccountability" Caption="Total" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Total Bond" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="CashBond" Caption="Cash" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        <dx:GridViewDataTextColumn FieldName="PropertyBond" Caption="Property" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        <dx:GridViewDataTextColumn FieldName="FormBond" Caption="Forms" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                        <dx:GridViewDataTextColumn FieldName="TotalBond" Caption="Total" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="Remarks" Caption="Remarks" />
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Bond" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkBond" CssClass="fa fa-file-text-o" Font-Size="Medium" OnClick="lnkBond_Click" />                                        
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
                                    <li><asp:LinkButton runat="server" ID="lnkAddDetl" OnClick="lnkAddDetl_Click" Text="Add" CssClass="control-primary" /></li>                                                   
                                    <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExportDetl" OnClick="lnkExportDetl_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
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

                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" SkinID="grdDXTotal" runat="server" KeyFieldName="FidelityDetiNo" Width="100%"
                            OnCustomCallback="grdDetl_CustomCallback"  >
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                           
                                <dx:GridViewDataTextColumn FieldName="CodeDeti" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" Visible="false" />
                                <dx:GridViewDataComboBoxColumn FieldName="FidelityCateDesc" Caption="Category">
                                    <Settings SortMode="Custom" />
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="FidelityTypeDesc" Caption="Accountability" />                                                              
                                <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"/>                                               
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%" />

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
                    <asp:Textbox ID="txtFidelityNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                    </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Employee Name :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" onblur="ResetEmployee()" CssClass="form-control required" Placeholder="Type here..."/> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderHRAN" runat="server"  
                            TargetControlID="txtFullName" MinimumPrefixLength="2" 
                            CompletionInterval="500" ServiceMethod="PopulateHranEmployee" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" EnableCaching="false"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getEmployee" FirstRowSelected="true" UseContextKey="true" />
                            <script type="text/javascript">
                                function SplitH(obj, index) {
                                    var items = obj.split("|");
                                    for (i = 0; i < items.length; i++) {
                                        if (i == index) {
                                            return items[i];
                                        }
                                    }
                                }

                                function ResetEmployee() {
                                    if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                                        document.getElementById('<%= cboPositionNo.ClientID %>').value = "";
                                        document.getElementById('<%= cboDepartmentNo.ClientID %>').value = "";
                                    }
                                }

                                function getEmployee(source, eventArgs) {
                                    document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                    document.getElementById('<%= cboPositionNo.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                    document.getElementById('<%= cboDepartmentNo.ClientID %>').value = SplitH(eventArgs.get_value(), 2);
                                }

                                                
                                                
                            </script>
                </div>
                               
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Position :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboPositionNo" DataMember="EPosition" runat="server" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Department :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboDepartmentNo" DataMember="EDepartment" runat="server" CssClass="form-control" />
                </div>
            </div>


            <asp:PlaceHolder ID="phDetail" runat="server" Visible="false">
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Risk Number :</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtRiskNumber" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Effectivity Date :</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtEffectivityDate" runat="server" SkinID="txtdate" CssClass="form-control"></asp:TextBox> 
                                                                    
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                            TargetControlID="txtEffectivityDate"
                            Format="MM/dd/yyyy" />  
                                      
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                            TargetControlID="txtEffectivityDate"
                            Mask="99/99/9999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            DisplayMoney="Left"
                            AcceptNegative="Left" />
                                    
                            <asp:RangeValidator
                            ID="RangeValidator2"
                            runat="server"
                            ControlToValidate="txtEffectivityDate"
                            ErrorMessage="<b>Please enter valid entry</b>"
                            MinimumValue="1900-01-01"
                            MaximumValue="3000-12-31"
                            Type="Date" Display="None"  />
                                    
                            <ajaxToolkit:ValidatorCalloutExtender 
                            runat="Server" 
                            ID="ValidatorCalloutExtender3"
                            TargetControlID="RangeValidator2" />                                                                           
                    </div>

                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Cancelled Date :</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtCancelledDate" runat="server" SkinID="txtdate" CssClass="form-control"></asp:TextBox> 
                                                                    
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                            TargetControlID="txtCancelledDate"
                            Format="MM/dd/yyyy" />  
                                      
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                            TargetControlID="txtCancelledDate"
                            Mask="99/99/9999"
                            MessageValidatorTip="true"
                            OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError"
                            MaskType="Date"
                            DisplayMoney="Left"
                            AcceptNegative="Left" />
                                    
                            <asp:RangeValidator
                            ID="RangeValidator1"
                            runat="server"
                            ControlToValidate="txtCancelledDate"
                            ErrorMessage="<b>Please enter valid entry</b>"
                            MinimumValue="1900-01-01"
                            MaximumValue="3000-12-31"
                            Type="Date" Display="None"  />
                                    
                            <ajaxToolkit:ValidatorCalloutExtender 
                            runat="Server" 
                            ID="ValidatorCalloutExtender1"
                            TargetControlID="RangeValidator1" />                                                                           
                    </div>

                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label">TOTAL ACCOUNTABILITIES</label>
                    <div class="col-md-6">
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Cash :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtCash" runat="server" CssClass="form-control number" style="text-align:right" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCash" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Property :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtProperty" runat="server" CssClass="form-control number" style="text-align:right" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtProperty" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Form :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtForm" runat="server" CssClass="form-control number" style="text-align:right" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtForm" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Total :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtTotalAccountability" runat="server" CssClass="form-control number" style="text-align:right" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtTotalAccountability" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label">TOTAL BOND</label>
                    <div class="col-md-6">
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Cash :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtCashBond" runat="server" CssClass="form-control number" style="text-align:right" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtCashBond" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Property :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtPropertyBond" runat="server" CssClass="form-control number" style="text-align:right" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtPropertyBond" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Form :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtFormBond" runat="server" CssClass="form-control number" style="text-align:right" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFormBond" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Total :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtTotalBond" runat="server" CssClass="form-control number" style="text-align:right" Enabled="false"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtTotalBond" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label">TOTAL</label>
                    <div class="col-md-6">
                    </div>
                </div>

                <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">Total Bond Amount :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtTotalBondAmount" runat="server" CssClass="form-control"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtTotalBondAmount" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">Total (A) :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtTotalA" runat="server" CssClass="form-control"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtTotalA" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group" style="display:none">
                    <label class="col-md-4 control-label has-space">Total (B) :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="txtTotalB" runat="server" CssClass="form-control"></asp:TextBox>
                       <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtTotalB" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                     </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Remarks :</label>
                    <div class="col-md-6">
                       <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                     </div>
                </div>
                <br />
            </asp:PlaceHolder>

        </div>

        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>


<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2">
    <fieldset class="form" id="Fieldset1">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveDetl" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtFidelityDetiNo" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCodeDeti" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                </div>
            </div>

            <div class="form-group" style="visibility:hidden; position:absolute;">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Category :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboFidelityCateNo" DataMember="EFidelityCate" runat="server" CssClass="form-control required" OnSelectedIndexChanged="cboFidelityCateNo_SelectedIndexChanged" AutoPostBack="true" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Accountability :</label>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboFidelityTypeNo" runat="server" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Amount :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control required number" style="text-align:right" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                    <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtTotalA" FilterType="Numbers, Custom" ValidChars="-." /> --%>
                    </div>
            </div>

        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>

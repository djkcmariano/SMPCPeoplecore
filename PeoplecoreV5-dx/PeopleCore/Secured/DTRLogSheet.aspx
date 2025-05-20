<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" EnableEventValidation="false" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="DTRLogSheet.aspx.vb" Inherits="Secured_DTRLogSheetList" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
    <script type="text/javascript">
    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }

</script>
    <div class="page-content-wrap" >
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-6">
                        <div class="panel-title">
                            <asp:Label runat="server" ID="lbl" />
                            <asp:HiddenField runat="server" ID="hif" />
                        </div>
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkSave" OnClick="lnkSave_Click" Text="Save" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" />
                                    </li>
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
                            <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" KeyFieldName="DTRLogSheetNo" Width="100%">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="DTRDate" Caption="DTR Date" />
                                    <dx:GridViewDataTextColumn FieldName="DayWeek" Caption="Day Week" />
                                    <dx:GridViewDataTextColumn FieldName="ShiftCode" Caption="Shift" />
                                    <dx:GridViewDataTextColumn FieldName="DayTypeCode" Caption="Day Type" />
                                    <dx:GridViewDataColumn Caption="In 1" Width="10%">
                                        <DataItemTemplate>
                                            <asp:HiddenField runat="server" ID="hifID" Value='<%# Bind("DTRLogSheetNo") %>' />
                                            <asp:HiddenField runat="server" ID="hifDate" Value='<%# Bind("DTRDate") %>' />
                                            <asp:TextBox runat="server" ID="txtIn1" Text='<%# Bind("In1") %>' Width="90%" Enabled='<%# Bind("IsPosted") %>' />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtIn1" Mask="99:99" MaskType="Time" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Out 1" Width="10%">
                                        <DataItemTemplate>
                                            <asp:TextBox runat="server" ID="txtOut1" Text='<%# Bind("Out1") %>' Width="90%" Enabled='<%# Bind("IsPosted") %>' />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtOut1" Mask="99:99" MaskType="Time" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="In 2" Width="10%">
                                        <DataItemTemplate>
                                            <asp:TextBox runat="server" ID="txtIn2" Text='<%# Bind("In2") %>' Width="90%" Enabled='<%# Bind("IsPosted") %>' />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtIn2" Mask="99:99" MaskType="Time" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Out 2" Width="10%">
                                        <DataItemTemplate>
                                            <asp:TextBox runat="server" ID="txtOut2" Text='<%# Bind("Out2") %>' Width="90%" Enabled='<%# Bind("IsPosted") %>' />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtOut2" Mask="99:99" MaskType="Time" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" />
                                </Columns>
                                <SettingsPager Mode="ShowAllRecords" />
                                <SettingsBehavior AllowSort="false" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnShow" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShow" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup" style="display:none;">
        <fieldset class="form" id="fsMain">
            <div class="cf popupheader">
                <h4>
                    &nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />
                &nbsp;
                <%--<asp:LinkButton runat="server" ID="lnkGenerate" CssClass="fa fa-undo submit fsMain lnkGenerate" OnClick="lnkGenerate_Click" ToolTip="Generate"  />--%>
                <asp:LinkButton runat="server" ID="lnkGenerate" OnClick="lnkGenerate_Click" Text="Generate" ToolTip="Generate" style="padding-right:50px; border:1px; background-color:transparent; text-align:center"  />
            </div>

            
            <div class="entryPopupDetl form-horizontal">
                <br />
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Employee Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" />
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                        <script type="text/javascript">

                        function getRecord(source, eventArgs) {
                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-required">
                    Date :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="required form-control" placeholder="From" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDate" />
                        <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtEndDate" runat="server"  CssClass="required form-control" placeholder="To" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDate" />
                        <asp:CompareValidator runat="server" ID="CompareValidator2" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate" />
                    </div>
                </div>
                <br />
                <br />
                <br />
            </div>
        </fieldset>
    </asp:Panel>
<%--<script type="text/javascript">
    function SelectAllCheckboxes(spanChk) {

        // Added as ASPX uses SPAN for checkbox
        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" && elm[i].name.indexOf("txtIsSelect") > 0 &&
            elm[i].id != theBox.id) {
                //elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;
            }
    }
</script>



<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-body" style="border:2px solid #f5f5f5;">
                     <div class="form-group">
                        
                        <div class="col-md-6">
                            <label class="col-md-4 control-label">Employee Name :</label>
                            <asp:label ID="lblName" runat="server" class="col-md-8 control-label"></asp:label>
                        </div>
                    
                    </div>
                </div>
                <div class="panel-heading">
                        
                    <div class="row">
                        <div class="col-md-4 pull-right">
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" placeholder="enter keyword"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                    TargetControlID="txtSearch" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" 
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                     <script type="text/javascript">
                                         function getRecord(source, eventArgs) {
                                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                         }
                                     </script>

                                <div class="input-group-btn">    
                                    <asp:Button runat="server" ID="lnkSearch" CausesValidation="false" CssClass="btn btn-default" OnClick="lnkGo_Click" ToolTip="Click here to search" Text="Go!" />            
                                                      
                                        <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">&nbsp;<span class="caret"></span></button>
                                        <div class="dropdown-menu dropdown-menu-right drp-menu-size">
                                            <div class="form-horizontal">
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Start Date :</label>
                                                        <div class="col-md-7">
                                                            <asp:Textbox runat="server" ID="fltxtStartDate" SkinID="txtdate" CssClass="form-control" ></asp:Textbox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server"
                                                                TargetControlID="fltxtStartDate"
                                                                Format="MM/dd/yyyy"/>  
                
                                                            <asp:RangeValidator
                                                            ID="RangeValidator9"
                                                            runat="server"
                                                            ControlToValidate="fltxtStartDate"
                                                            ErrorMessage="<b>Please enter valid entry</b>"
                                                            MinimumValue="1900-01-01"
                                                            MaximumValue="3000-12-31"
                                                            Type="Date" Display="None"  />
                
                                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                                                            ID="ValidatorCalloutExtender7"
                                                            TargetControlID="RangeValidator9"
                                                            /> 
                
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender11" runat="server"
                                                            TargetControlID="fltxtStartDate"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            DisplayMoney="Left"
                                                            AcceptNegative="Left"  
                                                            ErrorTooltipEnabled ="true" 
                                                            ClearTextOnInvalid="true"  ></ajaxToolkit:MaskedEditExtender>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">End Date :</label>
                                                            <div class="col-md-7">
                                                                <asp:Textbox runat="server" ID="fltxtEndDate" SkinID="txtdate" CssClass="form-control" >
                                                                </asp:Textbox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server"
                                                                TargetControlID="fltxtEndDate"
                                                                Format="MM/dd/yyyy"/>  
                
                                                                <asp:RangeValidator
                                                                ID="RangeValidator10"
                                                                runat="server"
                                                                ControlToValidate="fltxtEndDate"
                                                                ErrorMessage="<b>Please enter valid entry</b>"
                                                                MinimumValue="1900-01-01"
                                                                MaximumValue="3000-12-31"
                                                                Type="Date" Display="None"  />
                
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                                                                ID="ValidatorCalloutExtender3"
                                                                TargetControlID="RangeValidator10"
                                                                /> 
                
                                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender12" runat="server"
                                                                TargetControlID="fltxtEndDate"
                                                                Mask="99/99/9999"
                                                                MessageValidatorTip="true"
                                                                OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError"
                                                                MaskType="Date"
                                                                DisplayMoney="Left"
                                                                AcceptNegative="Left"  
                                                                ErrorTooltipEnabled ="true" 
                                                                ClearTextOnInvalid="true"  
                                                                />
                                                            </div>
                                                    </div>
                                                </div>       
                                            </div>                                   
                                        </div>            
                                            
                                </div>
                            </div>
                        </div>
                    </div>
         
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <!-- Gridview here -->
                        <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="EmployeeNo" >
                        <Columns>
                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblId1" runat="server" Text='<%# Bind("DTRLogNo") %>'></asp:Label>
                                <asp:Label ID="lblId2" runat="server" Text='<%# Bind("DTRLogNo") %>'></asp:Label>
                                <asp:Label ID="lblId3" runat="server" Text='<%# Bind("DTRNo") %>'></asp:Label>
                                <asp:Label ID="lblId4" runat="server" Text='<%# Bind("DTRDetiLogNo") %>'></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("EmployeeNo") %>'></asp:Label>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("DTRDate") %>'></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DTRNo" HeaderText="DTR No." Visible="false">
                                <ItemStyle HorizontalAlign="Center" Font-Size="11px" />
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DTRLogSheetNo" HeaderText="Transaction No.">
                                <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="DTRDate" HeaderText="Date" >
                                <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="Shift">
                            <ItemTemplate >
                                <asp:LinkButton  ID="lnkCaption" runat="server"  Text='<%# Bind("ShiftCode") %>' Enabled="false" style="font-size:11px" ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="D-Type">
                                <ItemTemplate >
                                <asp:LinkButton  ID="lnkType" runat="server"  Text='<%# Bind("DayTypeCode") %>' Enabled="false" style="font-size:11px" ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IN1">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtIn1" runat="server" style="border: solid 1px #757575;" cssclass="form-control" Width="50%" Text='<%# Bind("IN1") %>' Enabled='<%# Bind("IsPosted") %>'></asp:TextBox>   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                    TargetControlID="txtIn1" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                                    CultureName="en-US" />  
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OUT1">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtOut1"  runat="server" style="border: solid 1px #757575;" cssclass="form-control" Width="50%" Text='<%# Bind("OUT1") %>' Enabled='<%# Bind("IsPosted") %>'></asp:TextBox>   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                    TargetControlID="txtOut1" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                                    CultureName="en-US" />  
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IN2" Visible="false" >
                                <ItemTemplate >
                                    <asp:TextBox ID="txtIn2"  runat="server" style="border: solid 1px #757575;" cssclass="form-control"   Width="98%" Text='<%# Bind("IN2") %>' Enabled='<%# Bind("IsPosted") %>'></asp:TextBox>   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                    TargetControlID="txtIn2" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                                    CultureName="en-US" />  
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OUT2" Visible="false">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtOut2"  runat="server" style="border: solid 1px #757575;"  cssclass="form-control"  Width="98%" Text='<%# Bind("OUT2") %>' Enabled='<%# Bind("IsPosted") %>'></asp:TextBox>   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                    TargetControlID="txtOut2" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                                    CultureName="en-US" />  
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IN3" Visible="false">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtIn3"  runat="server" style="border: solid 1px #757575;"  cssclass="form-control"  Width="98%" Text='<%# Bind("IN3") %>' Enabled='<%# Bind("IsPosted") %>'></asp:TextBox>   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                    TargetControlID="txtIn3" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                                    CultureName="en-US" />  
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OUT3" Visible="false">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtOut3"  runat="server" style="border: solid 1px #757575;" cssclass="form-control"   Width="98%" Text='<%# Bind("OUT3") %>' Enabled='<%# Bind("IsPosted") %>'></asp:TextBox>   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                    TargetControlID="txtOut3" 
                                    Mask="99:99"
                                    MessageValidatorTip="true"
                                    OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError"
                                    MaskType="Time"
                                    AcceptAMPM="false" 
                                    CultureName="en-US" />  
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="xIsPosted" HeaderText="Is Posted?">
                            <ItemStyle HorizontalAlign="Center" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DatePosted" HeaderText="Date Posted">
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
      
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate >
                                <div><asp:Label runat="server" ID="lblRemarks" Text='<%# BIND("Remarks") %>' cssclass="form-control" ToolTip='<%# BIND("xxEncoded") %>'></asp:Label></div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Font-Size="11px" />
                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select"  >
                            <HeaderTemplate>
                                <asp:Label ID="Label6" runat="server" Text="Select"/><br />
                                <asp:CheckBox ID="txtIsSelectAll" onclick ="SelectAllCheckboxes(this);"  runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="txtIsSelect" runat="server" Enabled='<%# BIND("IsDTRLog") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                        </Columns>
                    
                        </mcn:DataPagerGridView> 
                     </div> 
                    <div class="row">
                        <div class="col-md-4">
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="grdMain" PageSize="10">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/images/arrow_first.png" PreviousPageImageUrl="~/images/arrow_previous.png" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>Page
                                                <asp:Label ID="CurrentPageLabel" runat="server" Text="<%# IIf(Container.TotalRowCount>0,  (Container.StartRowIndex / Container.PageSize) + 1 , 0) %>" /> of
                                                <asp:Label ID="TotalPagesLabel" runat="server" Text="<%# Math.Ceiling (System.Convert.ToDouble(Container.TotalRowCount) / Container.PageSize) %>" /> (
                                                <asp:Label ID="TotalItemsLabel" runat="server" Text="<%# Container.TotalRowCount%>" /> records )
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    <asp:NextPreviousPagerField ButtonType="Image" LastPageImageUrl="~/images/arrow_last.png" NextPageImageUrl="~/images/arrow_next.png" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />                              
                                </Fields>
                            </asp:DataPager>
                        </div>
                        <div class="col-md-6 col-md-offset-2">
                            <div class="btn-group pull-right">
                                <asp:Button ID="lnkSave" runat="server" CausesValidation="false"  UseSubmitBehavior="false" cssClass="btn btn-default" OnClick="lnkSave_Click" Text="Save">
                                </asp:Button>
            
                                <asp:Button ID="lnkClearLogs" runat="server" CausesValidation="false" cssClass="btn btn-default" OnClick="lnkClearLogs_Click" Text="Clear All" UseSubmitBehavior="false" > 
                                </asp:Button>
           
                                <asp:Button ID="lnkDeleteLog" runat="server" CausesValidation="false"  UseSubmitBehavior="false" 
                                    cssClass="btn btn-default" OnClick="lnkDelete_Click" Text="Delete">
                                </asp:Button>
                            </div>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDeleteLog" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </div>
                    </div>       
                </div>
            </div>
       </div>
 </div> --%>   

</asp:Content>

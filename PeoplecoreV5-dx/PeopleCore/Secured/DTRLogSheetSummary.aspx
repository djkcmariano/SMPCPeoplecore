<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" EnableEventValidation="false" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="DTRLogSheetSummary.aspx.vb" Inherits="Secured_DTRLogSheetSummary" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap" >
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-4">
                    <div class="panel-title">                        
                        <div class="input-group">
                            <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" />
                            <div class="input-group-btn">
                                <asp:Button runat="server" ID="lnkSearch" CausesValidation="false" CssClass="btn btn-default" OnClick="lnkSearch_Click" ToolTip="Click here to search" Text="Go!" />
                            </div>
                        </div>
                        <asp:Label runat="server" ID="lbl" />
                        <asp:HiddenField runat="server" ID="hif" />
                    </div>                    
                </div>
                <div>                  
                    <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkAddGroup" OnClick="lnkAddMass_Click" Text="Create Filter" CssClass="control-primary" /></li>
                        <li style="display:none"><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkSave" OnClick="lnkSave_Click" Text="Save" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                        
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>                    
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdMain" runat="server" OnPageIndexChanging="grdMain_PageIndexChanging">
                            <Columns>                                
                                <asp:BoundField DataField="FullName" HeaderText="EmployeeName">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DTRDate" HeaderText="DTR Date">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DayWeek" HeaderText="Day Week">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="ShiftCode" HeaderText="Shift">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Day Type">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hifxRow" Value='<%# Bind("xRow") %>' />                                        
                                        <asp:HiddenField runat="server" ID="hifDate" Value='<%# Bind("DTRDate") %>' />
                                        <asp:HiddenField runat="server" ID="hifEmployeeNo" Value='<%# Bind("EmployeeNo") %>' />
                                        <asp:HiddenField runat="server" ID="hifPayClassNo" Value='<%# Bind("PayClassNo") %>' />
                                        <asp:HiddenField runat="server" ID="hifDayTypeNo" Value='<%# Bind("DayTypeNo") %>' />
                                        <asp:HiddenField runat="server" ID="hifIsPosted" Value='<%# Bind("IsPosted") %>' />
                                        <asp:HiddenField runat="server" ID="hifDTRLogNo" Value='<%# Bind("DTRLogNo") %>' />
                                        <asp:HiddenField runat="server" ID="hifDTROTNo" Value='<%# Bind("DTROTNo") %>' />
                                        <asp:DropDownList runat="server" ID="cboDayTypeNo" Width="90%" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%"  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Working Hours">
                                    <ItemTemplate>                                            
                                            <asp:TextBox runat="server" ID="txtWorkingHrs" Text='<%# Bind("WorkingHrs") %>' Width="90%" Enabled='<%# Bind("IsPosted") %>' />                                            
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%"  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ovt">
                                    <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtOvt" Text='<%# Bind("Ovt") %>' Width="90%" Enabled='<%# Bind("IsPosted") %>' />                                            
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%"  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ovt > 8">
                                    <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtOvt8" Text='<%# Bind("Ovt8") %>' Width="90%" Enabled='<%# Bind("IsPosted") %>' />                                            
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%"  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NP">
                                    <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtNP" Text='<%# Bind("NP") %>' Width="90%" Enabled='<%# Bind("IsPosted") %>' />                                            
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%"  />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="NP > 8">
                                    <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtNP8" Text='<%# Bind("NP8") %>' Width="90%" Enabled='<%# Bind("IsPosted") %>' />                                            
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%"  />
                                </asp:TemplateField>                                                                
                                <asp:TemplateField ShowHeader="false" >
                                    <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chk" />
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%"  />
                                </asp:TemplateField>                                                                                                       
                            </Columns>
                        </mcn:DataPagerGridView>                                                
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">                                
                        <asp:DataPager ID="dpMain" runat="server" PagedControlID="grdMain" PageSize="10">
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
            <h4>&nbsp;</h4>                
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkGenerate" CssClass="fa fa-undo submit fsMain lnkGenerate" OnClick="lnkGenerate_Click" ToolTip="Generate"  />     
        </div>            
        <div class="entryPopupDetl form-horizontal">
            <br />
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Employee Name :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" /> 
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
                <label class="col-md-4 control-label has-space">Date :</label>
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
            <br /><br /><br />            
        </div>        
    </fieldset>
</asp:Panel>


<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel2" CancelControlID="lnkClose1" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsMain1">                
        <div class="cf popupheader">
            <h4>&nbsp;</h4>                
            <asp:Linkbutton runat="server" ID="lnkClose1" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
            <asp:LinkButton runat="server" ID="lnkGenerate1" CssClass="fa fa-undo submit fsMain lnkGenerate1" OnClick="lnkGenerate1_Click" ToolTip="Generate"  />     
        </div>            
        <div class="entryPopupDetl form-horizontal">
            <br />
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Filter By :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboFilterByAllNo" CssClass="form-control" DataMember="EFilteredByAll" OnSelectedIndexChanged="cboFilterBy_SelectedIndexChanged" AutoPostBack="true" />
                </div>                                
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Filter Value :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtFilterValueDesc" runat="server" CssClass="form-control" />                    
                    <asp:HiddenField ID="hifFilterValueNo" runat="server" />
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                    TargetControlID="txtFilterValueDesc" MinimumPrefixLength="2" 
                    CompletionInterval="500" ServiceMethod="populateDataDropdown" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" EnableCaching="false"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="GetRecord" FirstRowSelected="true" UseContextKey="true" />

                    <script type="text/javascript">
                        function GetRecord(source, eventArgs) {
                            document.getElementById('<%= hifFilterValueNo.ClientID %>').value = eventArgs.get_value();
                        }
                    </script>

                </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Date :</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtStartDate1" runat="server" CssClass="form-control" placeholder="Start Date" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate1" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDate1" />
                    <asp:CompareValidator runat="server" ID="CompareValidator3" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate1" />                    
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEndDate1" runat="server" CssClass="form-control" placeholder="End Date" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate1" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDate1" />
                    <asp:CompareValidator runat="server" ID="CompareValidator4" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate1" />                    
                </div>
            </div>                        
            <br /><br />
        </div>        
    </fieldset>
</asp:Panel>  

</asp:Content>

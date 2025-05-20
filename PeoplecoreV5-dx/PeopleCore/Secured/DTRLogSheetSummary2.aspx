
<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" EnableEventValidation="false" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="DTRLogSheetSummary2.aspx.vb" Inherits="Secured_DTRLogSheetSummary2" %>


<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap" >
    <br />    
    <div class="row" runat="server" id="Panel1">
        <div class="col-md-4">    
            Payroll Group : <asp:DropDownList ID="cboPayClassNo" runat="server" DataMember="EPayClass" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPayClassNo_SelectedIndexChanged" />
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="This field is required." ControlToValidate="cboPayClassNo" InitialValue="" Display="Dynamic" ValidationGroup="vgSave" />
        </div>
        <div class="col-md-4">    
            DTR Date : <asp:TextBox ID="txtDTRDate" runat="server" CssClass="form-control" />
                      <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtDTRDate" Operator="DataTypeCheck" Type="Date" ErrorMessage="Invalid date" Display="Dynamic" />
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDTRDate" />
                      <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" ClearTextOnInvalid="true" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtDTRDate" />            
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="This field is required." ControlToValidate="txtDTRDate" InitialValue="" Display="Dynamic" ValidationGroup="vgSave" />
        </div>
    </div>     
    <br />
    <div class="row">
        <div class="panel panel-default">            
            <div class="panel-heading">
            <div class="row" runat="server" id="Panel2">
                <div class="col-md-2">
                    Record No.
                    <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" Enabled="false" ReadOnly="true" />
                </div>
                <div class="col-md-2">
                    Employee Name :
                    <asp:TextBox runat="server" ID="txtFullname" CssClass="form-control" Placeholder="Employee Name..." />
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getMain1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    <script type="text/javascript">

                        function SplitH(obj, index) {
                            var items = obj.split("|");
                            for (i = 0; i < items.length; i++) {
                                if (i == index) {
                                    return items[i];
                                }
                            }
                        }

                        function getMain1(source, eventArgs) {
                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                        }

                        function ResetEmployeeNo() {
                            if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                            }
                        } 
                        </script>
                </div>
                <div class="col-md-1">
                    Day Type: <asp:DropDownList runat="server" ID="cboDayTypeNo" CssClass="form-control required" />                    
                </div>
                <div class="col-md-1">
                    Hrs :<asp:TextBox runat="server" ID="txtWorkingHrs" CssClass="form-control" />
                </div>
                <div class="col-md-1">
                    Ovt :<asp:TextBox runat="server" ID="txtOvt" CssClass="form-control" />    
                </div>
                <div class="col-md-1">
                    Ovt8 : <asp:TextBox runat="server" ID="txtOvt8" CssClass="form-control" />
                </div>
                <div class="col-md-1">
                    NP :<asp:TextBox runat="server" ID="txtNP" CssClass="form-control" />
                </div>
                <div class="col-md-1">
                    NP8 :<asp:TextBox runat="server" ID="txtNP8" CssClass="form-control"  />
                </div>                                                                              
                <div class="col-md-2 right">
                    <ul class="panel-controls">                                                
                        <li><asp:LinkButton runat="server" ID="lnkSave" OnClick="lnkSave_Click" Text="Save" CssClass="control-primary" ValidationGroup="vgSave" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                        
                        <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                    </ul>
                </div>
            </div>
            <div class="row" runat="server" id="Panel3">
                <div class="col-md-2">
                    Client ID No :<asp:TextBox runat="server" ID="txtClientID" CCsslass="form-control"  />
                </div>
                <div class="col-md-2">
                    Operation Code :<asp:TextBox runat="server" ID="txtOperationCode" CssClass="form-control"  />
                </div>                
                <div class="col-md-2">
                    Work Order :<asp:TextBox runat="server" ID="txtWorkOrder" CssClass="form-control"  />
                </div>
            </div>               
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DTRLogSheetNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" CommandArgument='<%# Bind("DTRLogSheetNo") %>' OnClick="lnkEdit_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Record No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="DTRDate" Caption="DTR Date" />
                                <dx:GridViewDataTextColumn FieldName="DayTypeCode" Caption="Day Type" />
                                <dx:GridViewDataTextColumn FieldName="WorkingHrs" Caption="Working Hrs" />
                                <dx:GridViewDataTextColumn FieldName="Ovt" Caption="Ovt" />                                                                
                                <dx:GridViewDataTextColumn FieldName="Ovt" Caption="Ovt > 8" />
                                <dx:GridViewDataTextColumn FieldName="NP" Caption="NP" />
                                <dx:GridViewDataTextColumn FieldName="NP8" Caption="NP > 8" />                                
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                            </Columns>                            
                        </dx:ASPxGridView>                                                                         
                    </div>
                </div>                                                                           
            </div>                   
        </div>
    </div>
</div>

</asp:Content>

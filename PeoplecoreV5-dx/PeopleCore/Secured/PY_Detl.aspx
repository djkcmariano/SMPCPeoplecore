<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PY_Detl.aspx.vb" Inherits="Secured_PY_Detl" %>

<asp:Content id="cntNo" contentplaceholderid="cphBody" runat="server">
  
  <script type="text/javascript">
       $(document).ready(function () {

       });

       function getselectedvalue_none(dval) {
           $('#' + dval).css({ 'display': 'none' });
       }

       function getselectedvalue_display(dval) {
           $('#' + dval).removeAttr("style");
       }
  </script>   

<asp:Panel runat="server" ID="Panel1">
    <fieldset class="form" id="fsd">
            <div  class="form-horizontal">

            <div class="form-group" id="pyactivitytypenox">
                <label class="col-md-4 control-label  has-required">Activity Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cboPYActivityTypeNo" runat="server" CssClass="required form-control" DataMember="EPYActivityType" AutoPostBack="true" OnSelectedIndexChanged="cbopyactivitytype_SelectedIndexChanged" ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group" id="pyactivitytypedetino">
                <label class="col-md-4 control-label  has-required"> Sub Activity Type :</label>
                <div class="col-md-7">
                    <asp:Dropdownlist ID="cbopyactivitytypedetino" runat="server" CssClass="required form-control" ></asp:Dropdownlist>
                </div>
            </div> 

            <div class="form-group" id="pydetinox" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtPYDetiNo" CssClass="form-control" runat="server" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group" id="codedetino">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtcode" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                    <asp:CheckBox ID="txtIsposted" CssClass="form-control" runat="server" Visible="false" ></asp:CheckBox>
                 </div>
            </div>
            
            <div class="form-group" id="dcoumentcode">
                <label class="col-md-4 control-label has-space">Document Number :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtdocumentcode"  runat="server" CssClass="form-control"></asp:Textbox>
                 </div>
            </div>
           
            <div class="form-group" id="employeeno">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" EnableCaching="false" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                     <script type="text/javascript">
                         function Split(obj, index) {
                             var items = obj.split("|");
                             for (i = 0; i < items.length; i++) {
                                 if (i == index) {
                                     return items[i];
                                 }
                             }
                         }

                         function getRecord(source, eventArgs) {
                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                         }
                            </script>
                </div>
            </div>
            
            
            
            <div class="form-group" id="quantity">
                <label class="col-md-4 control-label has-space" id="lblquantity">Quantity :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtQuantity" runat="server" CssClass="form-control number"></asp:Textbox>
                    </div>
            </div>
            <div class="form-group" id="hrs">
                <label class="col-md-4 control-label has-space" id="Label2">Hrs :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtHrs" runat="server" CssClass="form-control number"></asp:Textbox>
                    </div>
            </div>

            <div class="form-group" id="daytypeno">
                <label class="col-md-4 control-label has-space" id="Label1">Day Type :</label>
                <div class="col-md-3">
                    <asp:Dropdownlist ID="cboDayTypeNo" runat="server" CssClass="form-control number"></asp:Dropdownlist>
                    </div>
            </div>
            <asp:Panel runat="server" ID="Panel2">
            </asp:Panel>
           <br />
           <br />

            <div class="form-group">
                <label class="col-md-4 control-label has-space"></label>
                <div class="col-md-6">
                    <asp:Button runat="server" ID="lnkSave" CssClass="btn btn-default submit fsd" OnClick="lnkSave_Click" Text="Save"></asp:Button>
                    <asp:Button runat="server" ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" UseSubmitBehavior="false"></asp:Button>
                </div>
            </div> 
        </div>
        </fieldset >
</asp:Panel>

<div class="panel-heading">
        <div class="col-md-4 panel-title">
                            
        </div>
        <div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>
                    <ul class="panel-controls">
                             
                        <li><asp:LinkButton runat="server" ID="lnkDeleteDetl" OnClick="lnkDeleteDetl_Click" Text="Delete" CssClass="control-primary" /></li>
                    </ul>
                    <uc:ConfirmBox runat="server" ID="cfbDeleteDetl" TargetControlID="lnkDeleteDetl" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                </ContentTemplate>
                <Triggers>
                   
                </Triggers>
            </asp:UpdatePanel>
        </div>
</div>

<div class="panel-body">
    <div class="row">
        <asp:Label ID="lblDetl" runat="server"> </asp:Label>
    </div>
    <div class="row">
        <div class="table-responsive">
            <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" KeyFieldName="PYDetiNo"  Width="100%">                                                                                   
                <Columns>                            
                    
                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                    <dx:GridViewDataTextColumn FieldName="PYActivityTypeDesc" Caption="Activity Type" />
                    <dx:GridViewDataTextColumn FieldName="PYActivityTypeDetiDesc" Caption="Sub Activity Type" />
                    <dx:GridViewDataTextColumn FieldName="Quantity" Caption="Quantity" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Employee Pay Out" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                    <dx:GridViewDataTextColumn FieldName="BillingAmount" Caption="Billing Amount" PropertiesTextEdit-DisplayFormatString="{0:N2}" />
                    <dx:GridViewDataTextColumn FieldName="DayTypeDesc" Caption="Day Type" />
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                </Columns>    
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="Quantity" SummaryType="Sum" />
                    <dx:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
                    <dx:ASPxSummaryItem FieldName="BillingAmount" SummaryType="Sum" />
                                    
                </TotalSummary>                        
            </dx:ASPxGridView>

        </div>
    </div>
</div>
</asp:Content>
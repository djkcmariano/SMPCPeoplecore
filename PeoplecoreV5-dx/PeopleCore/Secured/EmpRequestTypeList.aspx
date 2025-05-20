<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpRequestTypeList.aspx.vb" Inherits="Secured_EmpRequestTypeList" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
    <%--<script type="text/javascript">
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
            spanChk : spanChk.children.item[0];
            xState = theBox.checked
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



        $(window).resize(function () {
            adjustPanelEntry();

        });

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (args, sender) {
                adjustPanelEntry();
            });

        });        
</script> 
 
 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                            
                    </div>
                    <div>
                        <uc:Filter runat="server" ID="Filter1" EnableContent="false"> 
                        </uc:Filter>
                        
                    </div>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <!-- Gridview here -->
                        <mcn:DataPagerGridView ID="grdMain" runat="server" DataKeyNames="RequestTypeNo">
                        <Columns>
                            <asp:TemplateField HeaderText="Id"  Visible="False"  >
                       
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server"    Text='<%# Bind("RequestTypeNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                
                            <asp:TemplateField  ShowHeader="false" >
                                <ItemTemplate >
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="grdEditbtn" OnClick="lnkEdit_Click" CausesValidation="false"  />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="3%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Trans. No." >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="8%" />
                            </asp:BoundField>
                    
                            <asp:BoundField DataField="RequestTypeCode" SortExpression="RequestTypeCode" HeaderText="Code" HeaderStyle-HorizontalAlign="left"     >
                                <HeaderStyle Width="10%"  />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="RequestTypeDesc" SortExpression="RequestTypeDesc" HeaderText="Description" HeaderStyle-HorizontalAlign="left"     >
                                <HeaderStyle Width="30%"  />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fullname" SortExpression="Fullname" HeaderText="Recipient 1" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fullname1" SortExpression="Fullname1" HeaderText="Recipient 2" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fullname2" SortExpression="Fullname2" HeaderText="Recipient 3" >
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Select"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="txtIsSelect" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                        </asp:TemplateField>
                   
                        </Columns>
                    <PagerSettings Mode="NextPreviousFirstLast" />
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
                
                                <asp:button runat="server" cssClass="btn btn-default"  ID="lnkAdd" OnClick="lnkAdd_Click"  UseSubmitBehavior="false" CausesValidation="false" Text="Add">
                                </asp:button>
                                <asp:Button ID="lnkDelete" runat="server" CausesValidation="false"  UseSubmitBehavior="false" 
                                        cssClass="btn btn-default"  OnClick="lnkDelete_Click" Text="Delete">
                                </asp:Button>
                            </div>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </div>
                    </div>       
                </div>
            </div>
       </div>
 </div>
 --%>
 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <ul class="panel-controls">
                                    <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                                                    
                                    <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>
                                    <li><asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" Text="Export" CssClass="control-primary" /></li>
                                </ul>
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
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
                           <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="RequestTypeNo">                                                                                   
                                <Columns>                            
                                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                    <dx:GridViewDataTextColumn FieldName="RequestTypeCode" Caption="Code" />
                                    <dx:GridViewDataTextColumn FieldName="RequestTypeDesc" Caption="Description" />
                                    <dx:GridViewDataTextColumn FieldName="FullName" Caption="Recepient 1" />
                                    <dx:GridViewDataTextColumn FieldName="FullName1" Caption="Recepient 2" />
                                    <dx:GridViewDataTextColumn FieldName="FullName2" Caption="Recepient 3" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" Caption="Select" /> 
                                </Columns>                            
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />    
                        </div>
                    </div>
                </div>
                   
            </div>
       </div>
 </div>

<asp:Button ID="btnShowRate" runat="server" style="display:none" />
        <ajaxtoolkit:ModalPopupExtender ID="mdlRate" runat="server" TargetControlID="btnShowRate" PopupControlID="pnlPopupRate"
            CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
        </ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopupRate" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Add/Edit Transaction</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                        <asp:Textbox ID="txtRequestTypeNo" runat="server"  
                         ></asp:Textbox>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Transaction No. :</label>
                <div class="col-md-7">
                        <asp:Textbox ID="txtCode"  ReadOnly="true" runat="server" CssClass="form-control" 
                         ></asp:Textbox>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Code :</label>
                <div class="col-md-7">
                        <asp:Textbox ID="txtRequestTypeCode" runat="server" CssClass="required form-control"
                            ></asp:Textbox>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7">
                        <asp:Textbox ID="txtRequestTypeDesc" runat="server" CssClass="required form-control"
                            ></asp:Textbox>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Recipient 1 :</label>
                <div class="col-md-7">

                        <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                        <asp:HiddenField runat="server" ID="hifRecepientNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                         <script type="text/javascript">
                             function getRecord(source, eventArgs) {
                                 document.getElementById('<%= hifRecepientNo.ClientID %>').value = eventArgs.get_value();
                             }
                            </script>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label ">Recipient 2 :</label>
                <div class="col-md-7">

                        <asp:TextBox runat="server" ID="txtfullName1" CssClass="form-control " style="display:inline-block;" /> 
                        <asp:HiddenField runat="server" ID="hifRecepientNo1"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                        TargetControlID="txtfullName1" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                         <script type="text/javascript">
                             function getRecord1(source, eventArgs) {
                                 document.getElementById('<%= hifRecepientNo1.ClientID %>').value = eventArgs.get_value();
                             }
                            </script>
                    </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Recipient 3 :</label>
                <div class="col-md-7">

                        <asp:TextBox runat="server" ID="txtfullName2" CssClass="form-control " style="display:inline-block;" /> 
                        <asp:HiddenField runat="server" ID="hifRecepientNo2"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                        TargetControlID="txtfullName2" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord2" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                         <script type="text/javascript">
                             function getRecord2(source, eventArgs) {
                                 document.getElementById('<%= hifRecepientNo2.ClientID %>').value = eventArgs.get_value();
                             }
                            </script>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Body :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtemailbody" TextMode="MultiLine" Rows="10"  runat="server" CssClass="required form-control"
                        ></asp:Textbox>
                   
                </div>
            </div>
            <br />
            </div>
          <!-- Footer here -->
         
         </fieldset>
</asp:Panel>

</asp:Content> 

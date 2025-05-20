<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="ERDAOwnerList.aspx.vb" Inherits="Secured_ERDAOwnerList" %>



<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">
<script type="text/javascript">
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


    $(window).resize(function () {
        adjustPanelEntry();

    });

    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (args, sender) {
            adjustPanelEntry();
        });

    });

    function cbCheckAll_CheckedChanged(s, e) {
        grdMain.PerformCallback(s.GetChecked().toString());
    }  
</script>
    
<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <div class="col-md-2">
                            <%--<asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkGo_Click" style="width:200px;" CssClass="form-control" runat="server" 
                                    ></asp:Dropdownlist>--%>
                                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkGo_Click" CssClass="form-control" runat="server" />
                        </div>
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
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
<%--                    <div class="table-responsive">
                        <!-- Gridview here -->
                        <mcn:DataPagerGridView ID="grdMain" runat="server"  DataKeyNames="DAOwnerNo" >
                        <Columns>
                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server"   Text='<%# Bind("DAOwnerNo") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false"   >
                            <ItemTemplate > 
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="grdEditbtn" OnClick="lnkEdit_Click" CausesValidation="false" />
                    
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Reference No."     >
                            <HeaderStyle Width="8%" HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
        
                        <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Employee No."     >
                            <HeaderStyle Width="15%" HorizontalAlign="Left"  />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                                                                            
                        <asp:BoundField DataField="FullName" SortExpression="FullName"  HeaderText="Employee Name" >
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Receiver?"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="txtIsReceiver" Enabled="false"  Checked='<%# bind("IsReceiver") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Approver?"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="txtIsApprover" Enabled="false"  Checked='<%# bind("IsApprover") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Evaluator?"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="txtIsEvaluator" Enabled="false"  Checked='<%# bind("IsEvaluator") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Recommendator?"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="txtIsRecommendator" Enabled="false"  Checked='<%# bind("IsRecommendator") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Active?"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="txtIsActive" Enabled="false"  Checked='<%# bind("IsActive") %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                                                                                   
                        <asp:TemplateField HeaderText="Select"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="txtIsSelect" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="2%" />
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
                                
                                <asp:Button runat="server" cssClass="btn btn-default"  ID="lnkAdd" OnClick="lnkAdd_Click" CausesValidation="false"  Text="Add">
                                </asp:Button>
                                <asp:Button ID="lnkDelete" runat="server" CausesValidation="false" 
                                    cssClass="btn btn-default" OnClick="lnkDelete_Click" 
                                    Text="Delete" UseSubmitBehavior="false" />
                            </div>
                            <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                        </div>
                    </div> --%>     
                    <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="DAOwnerNo"
                        OnCommandButtonInitialize="grdMain_CommandButtonInitialize" OnCustomCallback="gridMain_CustomCallback">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name" />                                                                           
                                <dx:GridViewDataCheckColumn FieldName="IsReceiver" Caption="Receiver?" /> 
                                <dx:GridViewDataCheckColumn FieldName="IsApprover" Caption="Approver?" /> 
                                <dx:GridViewDataCheckColumn FieldName="IsEvaluator" Caption="Evaluator?" /> 
                                <dx:GridViewDataCheckColumn FieldName="IsRecommendator" Caption="Recommendator?" />         
                                <dx:GridViewDataCheckColumn FieldName="IsActive" Caption="Active?" />                                                                                                                                                
                                <dx:GridViewDataTextColumn FieldName="EncodeBy" Caption="Encoder"/>                       
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%">
					                    <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                                            </dx:ASPxCheckBox>
                                        </HeaderTemplate>
				                    </dx:GridViewCommandColumn>
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
    <ajaxtoolkit:ModalPopupExtender ID="mdlMain" runat="server" TargetControlID="btnShowMain" PopupControlID="pnlpopupMain"
        CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel id="pnlpopupMain" runat="server" CssClass="entryPopup" style="display:none" >
        <fieldset class="form" id="fsMain">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                <asp:LinkButton runat="server" ID="btnSave" CssClass="fa fa-floppy-o submit fsMain btnSave" OnClick="btnSave_Click"  />   
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
           <div class="form-group">
                <label class="col-md-4 control-label">Reference no. :</label>
                <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" Enabled="false" CssClass="form-control" PlaceHolder = "Autonumber"></asp:Textbox>
                    </div>
            </div> 

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Employee :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="0" 
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
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
            <div class="form-group">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="chkIsReceiver" runat="server" />
                        &nbsp;
                        <span> if the employee has a Receiver Function.</span>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="chkIsApprover" runat="server" />
                        &nbsp;
                        <span> if the employee has a Approval Function.</span>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="chkIsEvaluator" runat="server" />
                        &nbsp;
                        <span> if the employee has a Evaluation Function.</span>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="chkIsRecommendator" runat="server" />
                        &nbsp;
                        <span> if the employee has a Recommendation Function.</span>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="chkIsActive" runat="server" />
                        &nbsp;
                        <span> if the employee is still Active.</span>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label">Please check here</label>
                <div class="col-md-7">
                        <asp:CheckBox ID="chkIsArchive" runat="server" />
                        &nbsp;
                        <span> to Archive.</span>
                    </div>
            </div>
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Company Name :</label>
                    <div class="col-md-7">
                        <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                        </asp:Dropdownlist>
                    </div>
             </div>
         </div>
         <br />
          <!-- Footer here -->
         
    </fieldset>
</asp:Panel>
  
</asp:Content>
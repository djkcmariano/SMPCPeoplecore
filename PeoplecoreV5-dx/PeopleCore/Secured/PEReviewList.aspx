<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PEReviewList.aspx.vb" Inherits="Secured_PENormsList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<%--<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <div>
                        <uc:Filter runat="server" ID="Filter1" EnableContent="false">
                            <Content>
                               
                            </Content>
                        </uc:Filter>
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="pereviewno,Isposted,performanceStatno,code" >
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEdit_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("pereviewno") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="code" HeaderText="Trans. No." SortExpression="code">
                                             <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="EmployeeCode" HeaderText="Employee No.">
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="fullname" HeaderText="Employee Name">
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="PositionDesc" HeaderText="Position">
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="DepartmentDesc" HeaderText="Department">
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="PECycleDesc" HeaderText="Performance Cycle">
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                         </asp:BoundField>

                                        <asp:TemplateField HeaderText="Details" >
                                            <ItemTemplate >
                                                <asp:ImageButton ID="btnPreview" runat="server" SkinID="grdDetail"  OnClick="lnkView_Click" CausesValidation="false" />                                   
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </mcn:DataPagerGridView>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-4">
                            <!-- Paging here -->
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
                        <div class="col-md-6 col-md-offset-2">
                            <!-- Button here btn-group -->
                            <div class="pull-right">
                                <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAdd_Click" ToolTip="Click here to add" ></asp:Button>
                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDelete_Click" ToolTip="Click here to delete" ></asp:Button>                       
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDelete" />
                        </div>
                    </div> 
                      
                </div>
                   
            </div>
       </div>
 </div>
 
 
 <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                        <h4 class="panel-title"><asp:Label ID="lblDetl" CssClass="lbltextsmall11-color"  runat="server" /></h4>
                         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <mcn:DataPagerGridView ID="grdDetl" runat="server" AllowSorting="true" OnSorting="grdDetl_Sorting" OnPageIndexChanging="grdDetl_PageIndexChanging" DataKeyNames="PEReviewEvaluatorNo, pestandardmainno, pereviewno, PECateTypeNo" >
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditDetl" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEditDetl_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("PEReviewEvaluatorNo") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id" Visible="False">
                                         <ItemTemplate>
                                             <asp:Label ID="lblstandardmainno" runat="server" Text='<%# Bind("pestandardmainno") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Id" Visible="False">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblpeevaluatorno" runat="server" Text='<%# Bind("peevaluatorno") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Id" Visible="False">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblEvaluatorNo" runat="server" Text='<%# Bind("evaluatorno") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                        <asp:BoundField DataField="code"  SortExpression="code" HeaderText="Detail No."    >
                                            <HeaderStyle Width="10%"  HorizontalAlign  ="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="PEEvaluatorDesc" HeaderText="Evaluator">
                                             <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="Fullname" HeaderText="Name of Evaluator">
                                             <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="Weighted" HeaderText="Weighted" Visible="true">
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="Rating" HeaderText="Average Rating" Visible="false">
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="WeightedAve" HeaderText="Rating" Visible="false">
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                         </asp:BoundField>

                                         <asp:BoundField DataField="WeightedAve" HeaderText="Total Score" Visible="false">
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                         </asp:BoundField>

                                         <asp:TemplateField HeaderText="Approval Status" Visible="false" >
                                            <ItemTemplate >
                                                <asp:ImageButton ID="lnkStatus" runat="server" CausesValidation="false" SkinID="grdPreviewbtn" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Evaluation Form">
                                             <ItemTemplate>
                                                 <asp:ImageButton ID="lnkCaptionR" runat="server" CausesValidation="false" SkinID="grdPreviewbtn" OnClick="lnkForm_Click" />
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Report" Visible="false">
                                             <ItemTemplate>
                                                 <asp:ImageButton ID="lnkCaptionX" runat="server" CausesValidation="false" SkinID="grdPreviewbtnRpt" />
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="6%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </mcn:DataPagerGridView>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-4">
                            <!-- Paging here -->
                            <asp:DataPager ID="dpDetl" runat="server" PagedControlID="grdDetl" PageSize="10">
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
                            <!-- Button here btn-group -->
                            <div class="pull-right">
                                <asp:Button ID="btnAddDetl" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAddDetl_Click" ToolTip="Click here to add" ></asp:Button>
                                <asp:Button ID="btnDeleteDetl" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDeleteDetl_Click" ToolTip="Click here to delete" ></asp:Button>                       
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox2" runat="server" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDeleteDetl" />
                        </div>
                    </div> 
                      
                </div>
                   
            </div>
       </div>
 </div>  --%>        
 
 <div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    
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
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEReviewNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Review No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." />    
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="PositionDesc" Caption="Position" />                                                                             
                                <dx:GridViewDataTextColumn FieldName="DepartmentDesc" Caption="Department" />                                                   
                                <dx:GridViewDataTextColumn FieldName="PECycleDesc" Caption="Performance Cycle" />                                                                                                                              
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Details" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDetail" CssClass="fa fa-list" Font-Size="Medium" OnClick="lnkDetail_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                                                                                     
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


 <div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-6">
                   <h4 class="panel-title">Review No. : &nbsp;<asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
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
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" Width="100%" KeyFieldName="PEReviewEvaluatorNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEditDetl_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn> 
                                <dx:GridViewDataTextColumn FieldName="PEEvaluatorNo" Caption="PEEvaluatorNo" Visible="false" /> 
                                <dx:GridViewDataTextColumn FieldName="PEReviewNo" Caption="PEReviewNo" Visible="false" />   
                                <dx:GridViewDataTextColumn FieldName="PEStandardMainNo" Caption="PEStandardMainNo" Visible="false" />                          
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="PEEvaluatorDesc" Caption="Evaluator" />  
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Name of Evaluator" />                                                                                                                                                                                 
                                <dx:GridViewDataTextColumn FieldName="Weighted" Caption="Weighted" />   
                                <dx:GridViewDataTextColumn FieldName="Rating" Caption="Average Rating" /> 
                                <dx:GridViewDataColumn Caption="Evaluation Form"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkForm" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkForm_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
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
<asp:Panel id="pnlPopupMain" runat="server" CssClass="entryPopup" style="display:none">
    <fieldset class="form" id="fsMain">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit fsMain lnkSave" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Review No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPEReviewNo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Review No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPEReviewCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                 </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Employee Name :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                    <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                    <ajaxToolkit:AutoCompleteExtender ID="aceEmployee" runat="server"
                    TargetControlID="txtFullName" MinimumPrefixLength="2" EnableCaching="true"                    
                    CompletionSetCount="1" CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getEmployee" FirstRowSelected="true" UseContextKey="true" />
                         <script type="text/javascript">
                                 function Split(obj, index) {
                                     var items = obj.split("|");
                                     for (i = 0; i < items.length; i++) {
                                         if (i == index) {
                                             return items[i];
                                         }
                                     }
                                 }

                                 function getEmployee(source, eventArgs) {
                                     document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                                 }
                        </script>

                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Date From :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDateFrom" SkinID="txtdate" runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDateFrom" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />     
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" /> 
                </div>
            </div>

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Date To :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtDateTo" SkinID="txtdate" runat="server" CssClass="form-control"></asp:Textbox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDateTo" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDateTo" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                       
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator2" /> 

                </div>
            </div>  

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Performance Cycle :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPECycleNo" runat="server" DataMember="EPECycle" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>


                                     
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>




<asp:Button ID="btnShowDetl" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlDetl" runat="server" TargetControlID="btnShowDetl" PopupControlID="pnlPopupDetl" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" ></ajaxtoolkit:ModalPopupExtender>
<asp:Panel id="pnlPopupDetl" runat="server" CssClass="entryPopup2" style="display:none">
    <fieldset class="form" id="fsDetl">
         <div class="cf popupheader">
              <h4>&nbsp;</h4>
              <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetl lnkSaveDetl" ToolTip="Save" />
         </div>
        <div  class="entryPopupDetl2 form-horizontal">

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPEReviewEvaluatorNo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                 </div>
            </div>
            
           <div class="form-group">
                <label class="col-md-4 control-label has-required">Evaluator Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboPEEvaluatorNo" runat="server" DataMember="EPEEvaluator" CssClass="form-control required"></asp:DropDownList>
                </div>
           </div>

<%--           <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Evaluator :</label>
                <div class="col-md-7">
                    <asp:DropDownList ID="cboEmployeeLNo" runat="server" DataMember="EEmployeeL" CssClass="form-control required"></asp:DropDownList>
                </div>
            </div>--%>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Name of Evaluator :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtEvalFullname" CssClass="form-control required" style="display:inline-block;" Placeholder="Autonumber" /> 
                    <asp:HiddenField runat="server" ID="hifEvaluatorNo" />
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                    TargetControlID="txtEvalFullname" MinimumPrefixLength="2" EnableCaching="true"                    
                    CompletionSetCount="1" CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                    CompletionListCssClass="autocomplete_completionListElement" 
                    CompletionListItemCssClass="autocomplete_listItem" 
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    OnClientItemSelected="getEvaluator" FirstRowSelected="true" UseContextKey="true" />
                         <script type="text/javascript">
                             function Split(obj, index) {
                                 var items = obj.split("|");
                                 for (i = 0; i < items.length; i++) {
                                     if (i == index) {
                                         return items[i];
                                     }
                                 }
                             }

                             function getEvaluator(source, eventArgs) {
                                 document.getElementById('<%= hifEvaluatorNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                             }
                        </script>

                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Weighted :</label>
                <div class="col-md-3">
                    <asp:Textbox ID="txtWeighted" CssClass="form-control required"  runat="server"></asp:Textbox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" TargetControlID="txtWeighted" ValidChars=".-" />
                </div>
            </div>  
                    
        </div>
        <div class="cf popupfooter">
         </div> 
    </fieldset>
</asp:Panel>

</asp:content>

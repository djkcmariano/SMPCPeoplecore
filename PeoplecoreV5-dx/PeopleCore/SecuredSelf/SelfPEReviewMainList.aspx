<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SelfPEReviewMainList.aspx.vb" Inherits="Secured_SelfPEReviewMainList" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <%--<div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" ></asp:Dropdownlist>
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
                           <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="PEReviewEvaluatorNo, pestandardmainno, pereviewno, PECateTypeNo" >
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false" Visible="false" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="cancel" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("PEReviewEvaluatorNo") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdEval" runat="server"   Text='<%# Bind("PEReviewEvaluatorNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id" Visible="False">
                                         <ItemTemplate>
                                             <asp:Label ID="lblstandardmainno" runat="server" Text='<%# Bind("pestandardmainno") %>'></asp:Label>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPEReviewMainNo" runat="server"   Text='<%# Bind("PEReviewMainNo") %>'></asp:Label>
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

                                         <asp:TemplateField HeaderText="Id"  Visible="False"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPECycleNo" runat="server" Text='<%# Bind("PECycleNo") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="code" HeaderText="PE Review No."  SortExpression="code"   >
                                            <HeaderStyle Width="10%"  HorizontalAlign  ="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>   

                                        <asp:BoundField DataField="applicableyear" SortExpression="applicableyear" HeaderText="Applicable Year" >
                                            <HeaderStyle Width="10%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                                                                          
                    
                                        <asp:BoundField DataField="PEPeriodDesc" SortExpression="PEPeriodDesc" HeaderText="Performance Period"     >
                                            <HeaderStyle Width="13%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField> 

                                        <asp:BoundField DataField="PETemplateDesc" SortExpression="PETemplateDesc" HeaderText="PE Template"     >
                                            <HeaderStyle Width="18%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>    
                                        

                                        <asp:BoundField DataField="PEEvalPeriodDesc" SortExpression="PEEvalPeriodDesc" HeaderText="Evaluation Type"  Visible="false"   >
                                            <HeaderStyle Width="15%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField> 

                                        <asp:BoundField DataField="ApprovalRemark" HeaderText="Approver Remarks"     >
                                            <HeaderStyle Width="15%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="tstatus" HeaderText="Status"  >
                                            <HeaderStyle Width="15%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                                                                                            
                                        <asp:BoundField DataField="PECycleDesc" SortExpression="PECycleDesc" HeaderText="Performance Cycle"     >
                                            <HeaderStyle Width="12%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField> 

                                        <asp:TemplateField HeaderText="Evaluation Form">
                                             <ItemTemplate>
                                                 <asp:ImageButton ID="lnkCaptionR" runat="server" CausesValidation="false" SkinID="grdPreviewbtn" OnClick="lnkForm_Click" />
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Left" Width="10%" />
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
                                <asp:Button ID="btnSubmit" Text="Submit for approval" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnSubmit_Click" ToolTip="Click here to submit" ></asp:Button>
                            </div>
                                                 
                         </div>
                    </div> 
                      
                </div>
                   
            </div>
       </div>--%>



           <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" />
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                                                
                    <ul class="panel-controls">                                                        
                        <li><asp:LinkButton runat="server" ID="lnkSubmit" Text="Submit" CssClass="control-primary" OnClick="lnkSubmit_Click" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkAdd" Text="Add" CssClass="control-primary" Visible="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkDelete" Text="Delete" CssClass="control-primary" Visible="false" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkExport" Text="Export" CssClass="control-primary" /></li>                                                                                
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEReviewNo">                                                                                   
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="CodeDeti" Caption="Review No." />
                                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee No." Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="FullName" Caption="Employee Name" />
                                <dx:GridViewDataTextColumn FieldName="PETemplateDesc" Caption="Title" />
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year"  CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataTextColumn FieldName="PEPeriodDesc" Caption="Period" />
                                <dx:GridViewDataTextColumn FieldName="PEEvalPeriodDesc" Caption="Evaluation Type" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="PECycleDesc" Caption="Performance Cycle" /> 
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" />
                                <dx:GridViewDataColumn Caption="Evaluation Form"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkForm" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkForm_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>                         
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" Width="2%" />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMain" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
    </div>

 </div>
 
 

</asp:content>

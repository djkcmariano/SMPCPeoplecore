<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PEReviewMainList.aspx.vb" Inherits="Secured_PEReviewMainList" %>


<%--<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                    </div>
                    <div>
                        <uc:Filter runat="server" ID="Filter1" EnableContent="true">
                            <Content>
                                 <div class="form-group">
                                    <label class="col-md-4 control-label">Applicable Year :</label>
                                    <div class="col-md-8">
                                        <asp:Textbox ID="txtFilterApplicableYear" SkinID="txtdate" runat="server" CssClass="form-control" ></asp:Textbox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtFilterApplicableYear" />
                                     </div>
                                 </div>

                                 <div class="form-group">
                                    <label class="col-md-4 control-label">Period Type :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="cboFilterPeriodType" CssClass="form-control" />
                                     </div>
                                 </div>

                                 <div class="form-group">
                                    <label class="col-md-4 control-label">Position :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="cboFilterPosition"  CssClass="form-control" />
                                     </div>
                                 </div>
                            </Content>
                        </uc:Filter>
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="table-responsive">
                           <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="pereviewmainno, IsEnabled" >
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="cancel" OnClick="lnkEdit_Click" SkinID="grdEditbtn" ToolTip="Click here to edit" CommandArgument='<%# Bind("pereviewmainno") %>'  />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="code" HeaderText="PE Review No."  SortExpression="code"   >
                                            <HeaderStyle Width="10%"  HorizontalAlign  ="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>   

                                        <asp:BoundField DataField="applicableyear" SortExpression="applicableyear" HeaderText="Applicable Year" >
                                            <HeaderStyle Width="8%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField> 

                                        <asp:BoundField DataField="PEPeriodDesc" SortExpression="PEPeriodDesc" HeaderText="Performance Period Type"     >
                                            <HeaderStyle Width="8%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>  

                                        <asp:BoundField DataField="PETemplateDesc" SortExpression="PETemplateDesc" HeaderText="PE Template"     >
                                            <HeaderStyle Width="15%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>    

                                        <asp:BoundField DataField="PositionDesc" SortExpression="PositionDesc" HeaderText="Position" Visible="false" >
                                            <HeaderStyle Width="15%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                                                                                                                 
                                  
                                        <asp:BoundField DataField="PEEvalPeriodDesc" SortExpression="PEEvalPeriodDesc" HeaderText="Evaluation Type"     >
                                            <HeaderStyle Width="10%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField> 
                                                                                             
                                        <asp:BoundField DataField="PENormsDesc" SortExpression="PENormsDesc"  Visible="True" HeaderText="Performance Norms"     >
                                            <HeaderStyle Width="10%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField> 

                                         <asp:BoundField DataField="PECycleDesc" SortExpression="PECycleDesc" HeaderText="Performance Cycle"   >
                                            <HeaderStyle Width="10%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>  

                                        <asp:BoundField DataField="tstatus" SortExpression="tstatus" HeaderText="Status">
                                            <HeaderStyle Width="10%" HorizontalAlign="Left"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="List of Employee">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnPreview" runat="server" OnClick="lnkView_Click" CausesValidation="false" SkinID="grdPreviewbtn" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                          </asp:TemplateField>   

                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txtIsSelect" runat="server" Enabled='<%# Bind("IsEnabled") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="4%" />
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
                            <!-- Button here  -->
                            <div class="pull-right">
                                <asp:Button ID="btnPost" Text="Post" runat="server" CausesValidation="false"  cssClass="btn btn-default" OnClick="btnPost_Click"  ToolTip="Click here to post"></asp:Button>
                                <asp:Button ID="btnProccess" Text="Process" runat="server" CausesValidation="false" cssClass="btn btn-default" OnClick="lnkPRocess_Click" ToolTip="Click here to process"></asp:Button>
                                <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAdd_Click" ToolTip="Click here to add" ></asp:Button>
                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnDelete_Click" ToolTip="Click here to delete" ></asp:Button>                       
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox2" runat="server" ConfirmMessage="Posted record cannot be modified, Proceed?" TargetControlID="btnPost" />
                            <uc:ConfirmBox ID="ConfirmBox3" runat="server" ConfirmMessage="Are you sure you want to process?" TargetControlID="btnProccess" />
                            <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDelete" />
                        </div>
                    </div> 
                      
                </div>
                   
            </div>
       </div>
 </div>
                


</asp:content>--%>

<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">
<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" AutoPostBack="true" OnSelectedIndexChanged="lnkSearch_Click" CssClass="form-control" runat="server" ></asp:Dropdownlist>
                </div>
                <div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>                    
                        <ul class="panel-controls">
                        <li><asp:LinkButton runat="server" ID="lnkPost" OnClick="lnkPost_Click" Text="Post" CssClass="control-primary" /></li>
                        <li><asp:LinkButton runat="server" ID="lnkProcess" OnClick="lnkProcess_Click" Text="Process" CssClass="control-primary" /></li>                        
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="PEReviewMainNo" OnCustomButtonCallback="lnkEdit_Click">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataComboBoxColumn FieldName="PETemplateDesc" Caption="Title" />
                                <dx:GridViewDataTextColumn FieldName="ApplicableYear" Caption="Applicable Year" CellStyle-HorizontalAlign="Left" />                                                                           
                                <dx:GridViewDataComboBoxColumn FieldName="PEPeriodDesc" Caption="Period" />                  
                                <dx:GridViewDataComboBoxColumn FieldName="PEEvalPeriodDesc" Caption="Evaluation Type" Visible="false"  />                                                                
                                <dx:GridViewDataComboBoxColumn FieldName="PENormsDesc" Caption="Performance Norms" Visible="false" />    
                                <dx:GridViewDataComboBoxColumn FieldName="PECycleDesc" Caption="Performance Cycle" />  
                                <dx:GridViewDataTextColumn FieldName="tstatus" Caption="Status" />
                                <dx:GridViewDataColumn Caption="Employee List"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                    <DataItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkOtherIncome" CssClass="fa fa-external-link control-primary" Font-Size="Medium" OnClick="lnkView_Click" />
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
</asp:content>
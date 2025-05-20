<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CarCompList.aspx.vb" Inherits="Secured_CarCompList" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<%--<div class="page-content-wrap" >         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    
                </div>
                <div>
                    <uc:Filter runat="server" ID="Filter1" />                                                                             
                </div>                           
            </div>
            <div class="panel-body">                
                <div class="row">
                        <div class="table-responsive">
                            <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging" DataKeyNames="CompNo">
                                <Columns>                                                                        
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEdit_Click" CommandArgument='<%# Bind("CompNo") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="CompNo">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CompCode" HeaderText="Code" SortExpression="CompCode">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>                                    
                                    <asp:BoundField DataField="CompTypeDesc" HeaderText="Type" SortExpression="CompTypeDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="CompClusterDesc" HeaderText="Cluster" SortExpression="CompClusterDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>                                                                                                           
                                    <asp:BoundField DataField="CompDesc" HeaderText="Competency" SortExpression="CompDesc">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="40%" />
                                    </asp:BoundField>                                                                                                                                                                                                                                                                                                                                                                                                           
                                    <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>                                            
                                            <asp:ImageButton ID="btnDetails" runat="server" CausesValidation="false" SkinID="grdDetail" CssClass="cancel" OnClick="btnDetails_Click" CommandArgument='<%# Bind("CompNo") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>
                                </Columns>
                            </mcn:DataPagerGridView>                           
                        </div>
                    </div>                    
                <div class="row">
                        <div class="col-md-4">
                            <!-- Paging here -->
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
                            <!-- Button here -->
                            <div class="pull-right">                                                                            
                                <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false"  UseSubmitBehavior="false" CssClass="btn btn-default" OnClick="btnDelete_Click" />                                
                            </div>
                            <uc:ConfirmBox ID="ConfirmBox1" runat="server" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDelete" />
                        </div>                        
                    </div>                                       
            </div>                                                                 
        </div>
    </div>    
    <div class="row">
        <div class="panel panel-default"> 
            <div class="panel-heading">
                <div class="col-md-2">
                    <h4>Proficiency</h4>
                </div>
                <div>                                    
                </div>                           
            </div>                           
            <div class="panel-body">
                <div class="row">
                    <div class="table-responsive">
                        <mcn:DataPagerGridView ID="grdDetail" runat="server" AllowSorting="true" OnSorting="grdDetail_Sorting" OnPageIndexChanging="grdDetail_PageIndexChanging" DataKeyNames="CompDetiNo">
                            <Columns>                                                                        
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEditDetl_Click" CommandArgument='<%# Bind("CompDetiNo") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>                                                                                             
                                <asp:BoundField DataField="Code" HeaderText="Transaction No." SortExpression="Code">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CompScaleDesc" HeaderText="Description" SortExpression="CompScaleDesc">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                </asp:BoundField>                                               
                                <asp:BoundField DataField="Anchor" HeaderText="Indicator" SortExpression="Anchor">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                </asp:BoundField>                                                                                                                       
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                            </Columns>
                        </mcn:DataPagerGridView>                           
                    </div>
                </div>                    
                <div class="row">
                    <div class="col-md-4">
                        <!-- Paging here -->
                        <asp:DataPager ID="DataPager2" runat="server" PagedControlID="grdDetail" PageSize="10">
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
                        <!-- Button here -->
                        <div class="pull-right">                                                                            
                            <asp:Button ID="btnAddDetl" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnAddDetl_Click" />
                            <asp:Button ID="btnDeleteDetl" Text="Delete" runat="server" CausesValidation="false"  UseSubmitBehavior="false" CssClass="btn btn-default" OnClick="btnDeleteDetl_Click" />                                
                        </div>
                        <uc:ConfirmBox ID="ConfirmBox2" runat="server" ConfirmMessage="Seleted items will be permanently deleted and cannot be recovered. Proceed?" TargetControlID="btnDeleteDetl" />
                    </div>                        
                </div>                       
            </div>                   
        </div>
    </div>
</div>   --%> 

<div class="page-content-wrap">         
<div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:Dropdownlist ID="cboTabNo" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTab_Click" ></asp:Dropdownlist>
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
                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDXTotal" KeyFieldName="CompNo"
                        OnCustomCallback="grdMain_CustomCallback" OnCustomColumnSort="grdMain_CustomColumnSort" OnCustomColumnDisplayText="grdMain_CustomColumnDisplayText">                                                                                   
                            <Columns>
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>                            
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Transaction No." />
                                <dx:GridViewDataTextColumn FieldName="CompTypeDesc" Caption="Competency Type" />
                                <%--    <Settings SortMode="Custom" />
					            </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="CompClusterDesc" Caption="Cluster" />     
                                <dx:GridViewDataTextColumn FieldName="CompCode" Caption="Code" />                                                                                                                          
                                <dx:GridViewDataTextColumn FieldName="CompDesc" Caption="Competency" />   
                                <dx:GridViewDataTextColumn FieldName="OrderByDeti" Caption="Order By" CellStyle-HorizontalAlign="Left" />                                                                                                                           
                                <dx:GridViewDataTextColumn FieldName="tStatus" Caption="Status" Visible="false" /> 
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
                   <h4 class="panel-title">Transaction No. : <asp:Label ID="lblDetl" runat="server"></asp:Label></h4>
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
                        <dx:ASPxGridView ID="grdDetl" ClientInstanceName="grdDetl" runat="server" Width="100%" KeyFieldName="CompDetiNo">                                                                                   
                            <Columns>  
                                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton runat="server" ID="btnEditDetl" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="btnEditDetl_Click" CommandArgument='<%# Bind("CompDetiNo") %>' />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>     
                                
                                <dx:GridViewDataTextColumn FieldName="Code" Caption="Detail No." />
                                <dx:GridViewDataTextColumn FieldName="CompScaleDesc" Caption="Proficiency" />  
                                <dx:GridViewDataTextColumn FieldName="Anchor" Caption="Indicator" />                                                                                                                                                                                 
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" SelectAllCheckboxMode="Page"  />
                            </Columns>                            
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="grdExportDetl" runat="server" GridViewID="grdDetl" />
                    </div>
                </div>                                                           
            </div>                   
        </div>
</div>
</div>


<asp:Button ID="Button1" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsMain">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control"  Placeholder="Autonumber" />
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Code :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCompCode" CssClass="form-control" />                        
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Competency :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCompDesc" CssClass="form-control required" TextMode="MultiLine" Rows="3" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Competency Type :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboCompTypeNo" CssClass="form-control required" DataMember="ECompType" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Competency Cluster :</label>
                <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="cboCompClusterNo" CssClass="form-control" DataMember="ECompCluster" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Description :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtCompDetl" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>
            </div>          
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Order By :</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtOrderBy" CssClass="form-control" />     
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" TargetControlID="txtOrderBy" />                   
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                </div>
            </div>
                          
        </div>                    
    </fieldset>
</asp:Panel>


<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2" PopupControlID="Panel2" CancelControlID="lnkCloseDetl" BackgroundCssClass="modalBackground" />
<asp:Panel id="Panel2" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="fsDetail">                    
        <div class="cf popupheader">
            <h4>&nbsp;</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseDetl" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveDetl" OnClick="lnkSaveDetl_Click" CssClass="fa fa-floppy-o submit fsDetail" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">                        
            <div class="form-group">
                <label class="col-md-4 control-label has-space">Detail No. :</label>
                <div class="col-md-7">
                    <asp:Textbox ID="txtCodeDetl" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Proficiency :</label>
                <div class="col-md-7">
                    <asp:DropdownList ID="cboCompScaleNo" DataMember="ECompScale" runat="server" CssClass="form-control required" />                        
                </div>
            </div>                                     
            <div class="form-group">
                <label class="col-md-4 control-label has-required">Indicator :</label>
                <div class="col-md-7">
                    <asp:TextBox runat="server" ID="txtAnchor" CssClass="form-control required" TextMode="MultiLine" Rows="4" />                        
                </div>
            </div>
        </div>                    
    </fieldset>
</asp:Panel>

</asp:Content>


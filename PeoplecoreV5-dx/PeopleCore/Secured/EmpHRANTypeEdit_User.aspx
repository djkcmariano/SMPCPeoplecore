<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpHRANTypeEdit_User.aspx.vb" Inherits="Secured_EmpHRANTypeEdit_User" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<script type="text/javascript">    

        function Split(obj, index) {
            var items = obj.split("|");
            for (i = 0; i < items.length; i++) {
                if (i == index) {
                    return items[i];
                }
            }
        }

        function getMain(source, eventArgs) {
            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
        }
  
</script>

    <uc:Tab runat="server" ID="Tab">
        <Header>               
            <asp:Label runat="server" ID="lbl" /> 
        </Header>        
        <Content>
        <br />
        <%--<div class="page-content-wrap">         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            <h4>&nbsp;</h4>                                
                        </div>
                        <div>
                                
                        </div>                           
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <mcn:DataPagerGridView ID="grdMain" runat="server" AllowSorting="true" OnSorting="grdMain_Sorting" OnPageIndexChanging="grdMain_PageIndexChanging">
                                    <Columns>                                                                        
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="grdEditbtn" CssClass="cancel" OnClick="btnEdit_Click" CommandArgument='<%# Bind("HRANTypeUserNo") %>' />                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Code" HeaderText="Reference No."  HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign ="LEFT" ItemStyle-HorizontalAlign="left" SortExpression="Code" />
                                    

                                    <asp:BoundField DataField="fullname" HeaderText="User Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" Width="40%" />
                                    </asp:BoundField>


                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <center>
                                                Select<br />
                                                <asp:CheckBox ID="txtIsSelectAll" onclick="SelectAllCheckboxes(this);" runat="server" />
                                            </center>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="txtIsSelect" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    </asp:TemplateField>

                                </Columns>    
                                </mcn:DataPagerGridView>                                
                            </div>
                        </div>                    
                        <div class="row">
                            <div class="col-md-4">                                
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
                                <div class="pull-right">
                                    <asp:Button ID="btnAdd" Text="Add" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnDelete" Text="Delete" runat="server" CausesValidation="false"  CssClass="btn btn-default" OnClick="btnDelete_Click" />                                    
                                </div>
                                <uc:ConfirmBox runat="server" ID="ConfirmBox1" TargetControlID="btnDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?" />
                            </div>
                        </div>                       
                    </div>                   
                </div>
            </div>
        </div> --%>   
        <div class="page-content-wrap" >         
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="col-md-2">
                            &nbsp;
                        </div>
                        <div>                                                
                            <ul class="panel-controls">                                    
                                <li><asp:LinkButton runat="server" ID="lnkAdd" OnClick="lnkAdd_Click" Text="Add" CssClass="control-primary" /></li>                            
                                <li><asp:LinkButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" Text="Delete" CssClass="control-primary" /></li>                                                        
                                <uc:ConfirmBox runat="server" ID="cfbDelete" TargetControlID="lnkDelete" ConfirmMessage="Selected items will be permanently deleted and cannot be recovered. Proceed?"  />
                            </ul>                                                                                                                                                     
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive">
                                <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" SkinID="grdDX" KeyFieldName="HRANTypeUserNo">                                                                                   
                                    <Columns>
                                        <dx:GridViewDataColumn CellStyle-HorizontalAlign="Center" Caption="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <DataItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CssClass="fa fa-pencil" Font-Size="Medium" OnClick="lnkEdit_Click" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Reference No." />
                                        <dx:GridViewDataTextColumn FieldName="FullName" Caption="User Name" />                                        
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" />
                                    </Columns>                            
                                </dx:ASPxGridView>                                
                            </div>
                        </div>                                                           
                    </div>                   
                </div>
            </div>
        </div>                            
    </Content>        
    </uc:Tab>
    <asp:Button ID="Button1" runat="server" style="display:none" />
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel1" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
    <asp:Panel id="Panel1" runat="server" CssClass="entryPopup">
        <fieldset class="form" id="fsMain">                    
            <div class="cf popupheader">
                <h4>&nbsp;</h4>
                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave" OnClick="lnkSave_Click" CssClass="fa fa-floppy-o submit lnkSave" ToolTip="Save" />
            </div>                                            
            <div class="entryPopupDetl form-horizontal">    
             
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtHRANTypeuserNo" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>      
                                   
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Transaction No. :</label>
                    <div class="col-md-7">
                        <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                    </div>
                </div>                        

                <div class="form-group">
                    <label class="col-md-4 control-label has-required">User Name :</label>
                    <div class="col-md-7">
                       <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                        TargetControlID="txtFullName" MinimumPrefixLength="2" 
                        CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getMain" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                    </div>

                </div>  

                <br />
            </div>                    
        </fieldset>
    </asp:Panel>
</asp:Content>

